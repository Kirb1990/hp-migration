using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MigrationTool
{
    public class Migrator
    {
        public event EventHandler<Migrator> OnSuccessfullyMigrated;
        public event EventHandler<string> OnErrorOccured;
        public string Error => _ErrorMessage;
        public bool HasErrors => _ErrorOccured;
        
        readonly string _ConnectionString;
        readonly char _PathSeparator;
        
        string _CurrentDatabase;
        string _ErrorMessage;
        
        bool _ErrorOccured;

        public Migrator(string connectionString)
        {
            _ConnectionString = connectionString;
            _PathSeparator = Path.DirectorySeparatorChar;
            
            Trace.Listeners.Add(new TimestampedTextWriterTraceListener(".\\trace.log"));
            Trace.Listeners.Add(new TimestampedConsoleTraceListener());

            Trace.AutoFlush = true;
            
            OnErrorOccured += ErrorHandler;
        }

        void ErrorHandler(object sender, string e)
        {
            _ErrorMessage = e;
            _ErrorOccured = true;
            
            Trace.WriteLine(_ErrorMessage);
        }

        public bool TestMySqlConnection(string mysqlConnectionString)
        {
            MySqlConnection connection = new(mysqlConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                OnErrorOccured?.Invoke(this, ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool MySqlDatabaseExists(string database)
        {
            MySqlConnection connection = new (_ConnectionString);

            try
            {
                connection.Open();

                string query = $"SHOW DATABASES LIKE '{database}'";
                MySqlCommand command = new(query, connection);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                OnErrorOccured?.Invoke(this, $"Error executing using database: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
                
            return false;
        }
        public bool TestPervasiveConnection(string pervasiveConnectionString)
        {
            // TODO: IMPLEMENT AT WORK!!
            return false;
        }
        public bool Migrate()
        {
            MySqlConnection connection = new(_ConnectionString);
            
            try
            {
                connection.Open();
                connection.ChangeDatabase(_CurrentDatabase);
                
                string migrationsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Databases{_PathSeparator}Migrations");
                
                List<string> migrations = Directory.GetFiles(migrationsPath, "*.sql")
                    .OrderBy(x => x)
                    .ToList();
                

                foreach (string migration in migrations)
                {
                    string migrationName = Path.GetFileNameWithoutExtension(migration);

                    MySqlCommand selectCommand = new("SELECT * FROM `migrations` WHERE `migration` = ?", connection);
                    selectCommand.Parameters.AddWithValue("@migrationName", migrationName);
                    object result = selectCommand.ExecuteScalar();

                    if (result != null)
                    {
                        // Migration has already been executed.
                        continue;
                    }
                    string script = File.ReadAllText(migration, Encoding.UTF8);

                    MySqlCommand command = new(script, connection);
                    command.ExecuteNonQuery();

                    MySqlCommand insertCommand = new("INSERT INTO `migrations` (`migration`) VALUES (?)", connection);
                    insertCommand.Parameters.AddWithValue("@migrationName", migrationName);
                    insertCommand.ExecuteNonQuery();

                    Trace.WriteLine($"Migration {migration} executed successfully.");
                }
            }
            catch (Exception ex)
            {
                OnErrorOccured?.Invoke(this, $"Error executing migration: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }

            Trace.WriteLine("All migrations executed successfully.");
            
            OnSuccessfullyMigrated?.Invoke(this, this);
            return true;
        }
        public bool Refresh()
        {
            using MySqlConnection connection = new(_ConnectionString);
            try
            {
                connection.Open();
                connection.ChangeDatabase(_CurrentDatabase);

                string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = @DatabaseName AND table_type = 'BASE TABLE'";
                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@DatabaseName", _CurrentDatabase);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    string deleteQuery = $"DELETE FROM `{tableName}`";
                    Trace.WriteLine(deleteQuery);
                    using MySqlCommand deleteCommand = new(deleteQuery);
                    deleteCommand.Connection = connection;
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                OnErrorOccured?.Invoke(this, $"Error executing refresh: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }

            Trace.WriteLine("All tables cleared successfully.");
            return true;
        }

        public bool UseWithCreateDatabaseIfNotExists(string database)
        {
            if (MySqlDatabaseExists(database))
            {
                return Use(database);
            }

            return ExecuteMigrationTable(database) && Use(database);
        }
        
        public bool Use(string database)
        {
            MySqlConnection connection = new (_ConnectionString);

            try
            {
                connection.Open();
                connection.ChangeDatabase(database);
            }
            catch (Exception ex)
            {
                OnErrorOccured?.Invoke(this, $"Error executing using database: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
            
            _CurrentDatabase = database;
            return true;
        }
        bool ExecuteMigrationTable(string database)
        {
            MySqlConnection connection = new (_ConnectionString);

            try
            {
                connection.Open();
                
                string sqlCommandPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Databases{_PathSeparator}SqlCommands");
                string sqlFileName = "create_database.sql";

                string rawSqlCommand = File.ReadAllText($"{sqlCommandPath}{_PathSeparator}{sqlFileName}", Encoding.UTF8);
                string sqlCommand = rawSqlCommand.Replace("%newdb%", database);
            
                MySqlCommand command = new (sqlCommand, connection);
                command.ExecuteNonQueryAsync();
                
                connection.ChangeDatabase(database);
                CreateMigrationTable(connection, sqlCommandPath);
            }
            catch (Exception ex)
            {
                OnErrorOccured?.Invoke(this, $"Error executing using database: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
        void CreateMigrationTable(MySqlConnection connection, string sqlCommandPath)
        {
            string sqlFileName = "create_migration_table.sql";
            string sqlCommand = File.ReadAllText($"{sqlCommandPath}{_PathSeparator}{sqlFileName}", Encoding.UTF8);
            
            MySqlCommand command = new (sqlCommand, connection);
            command.ExecuteNonQueryAsync();
        }
    }
}