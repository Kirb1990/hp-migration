using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class Migration
    {
        private readonly string _ConnectionString;
        private readonly char _PathSeperator;
        
        private string _CurrentDatabase;

        public Migration(string connectionString)
        {
            _ConnectionString = connectionString;
            _PathSeperator = Path.DirectorySeparatorChar;
        }

        public async void Migrate()
        {
            OdbcConnection connection = new OdbcConnection(_ConnectionString);
            
            try
            {
                connection.Open();
                connection.ChangeDatabase(_CurrentDatabase);
                
                string migrationsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations");
                
                List<string> migrations = Directory.GetFiles(migrationsPath, "*.sql")
                    .OrderBy(x => x)
                    .ToList();

                foreach (var migration in migrations)
                {
                    string migrationName = Path.GetFileNameWithoutExtension(migration);

                    OdbcCommand selectCommand = new OdbcCommand("SELECT * FROM `migrations` WHERE `migration` = ?", connection);
                    selectCommand.Parameters.AddWithValue("@migrationName", migrationName);
                    object result = selectCommand.ExecuteScalar();

                   if (result != null)
                    {
                        // Migration has already been executed.
                        continue;
                    }
                    string script = File.ReadAllText(migration, Encoding.UTF8);

                    OdbcCommand command = new OdbcCommand(script, connection);
                    command.ExecuteNonQuery();

                    OdbcCommand insertCommand = new OdbcCommand("INSERT INTO `migrations` (`migration`) VALUES (?)", connection);
                    insertCommand.Parameters.AddWithValue("@migrationName", migrationName);
                    insertCommand.ExecuteNonQuery();

                    Console.WriteLine($"Migration {migration} executed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing migration: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            Console.WriteLine("All migrations executed successfully.");
            Console.ReadLine();
        }

        public async void Use(string database)
        {
            OdbcConnection connection = new OdbcConnection(_ConnectionString);

            try
            {
                connection.Open();
                await CheckDatabaseExisting(connection, database);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing changing database: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            _CurrentDatabase = database;
        }

        private async Task CheckDatabaseExisting(OdbcConnection connection, string database)
        {
            string query = $"SHOW DATABASES LIKE '{database}'"; 
            OdbcCommand command = new OdbcCommand(query, connection);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                //Database already exists.
                return;
            }   
            
            string sqlCommandPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlCommands");
            string sqlFileName = "create_database.sql";

            string rawSqlCommand = File.ReadAllText($"{sqlCommandPath}{_PathSeperator}{sqlFileName}", Encoding.UTF8);
            string sqlCommand = rawSqlCommand.Replace("%newdb%", database);
            
            command = new OdbcCommand(sqlCommand, connection);
            await command.ExecuteNonQueryAsync();
            
            connection.ChangeDatabase(database);
            
            await CreateMigrationTable(connection, sqlCommandPath);
        }

        private async Task CreateMigrationTable(OdbcConnection connection, string sqlCommandPath)
        {
            string sqlFileName = "create_migration_table.sql";
            string sqlCommand = File.ReadAllText($"{sqlCommandPath}{_PathSeperator}{sqlFileName}", Encoding.UTF8);
            
            OdbcCommand command = new OdbcCommand(sqlCommand, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}