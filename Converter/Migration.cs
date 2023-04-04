﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class Migration
    {
        public event EventHandler<Migration> OnSuccessfullyMigrated;
        
        readonly string _ConnectionString;
        readonly char _PathSeparator;
        
        string _CurrentDatabase;

        public Migration(string connectionString)
        {
            _ConnectionString = connectionString;
            _PathSeparator = Path.DirectorySeparatorChar;
            
            Trace.Listeners.Add(new TimestampedTextWriterTraceListener(".\\trace.log"));
            Trace.Listeners.Add(new TimestampedConsoleTraceListener());

            Trace.AutoFlush = true;
        }

        public Task Migrate()
        {
            OdbcConnection connection = new OdbcConnection(_ConnectionString);
            
            try
            {
                connection.Open();
                connection.ChangeDatabase(_CurrentDatabase);
                
                string migrationsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Databaess{_PathSeparator}Migrations");
                
                List<string> migrations = Directory.GetFiles(migrationsPath, "*.sql")
                    .OrderBy(x => x)
                    .ToList();

                foreach (string migration in migrations)
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

                    Trace.WriteLine($"Migration {migration} executed successfully.");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error executing migration: {ex.Message}");
                return Task.CompletedTask;
            }
            finally
            {
                connection.Close();
            }

            Trace.WriteLine("All migrations executed successfully.");
            
            OnSuccessfullyMigrated?.Invoke(this, this);
            return Task.CompletedTask;
        }

        public Task Refresh()
        {
            OdbcConnection connection = new OdbcConnection(_ConnectionString);
            try
            {
                connection.Open();

                string query = "SELECT migration FROM migrations";
                using (OdbcCommand command = new OdbcCommand(query, connection))
                {
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string migrationName = reader.GetString(0);

                            string deleteQuery = $"DELETE FROM `{migrationName}`";
                            using (OdbcCommand deleteCommand = new OdbcCommand(deleteQuery, connection))
                            {
                                deleteCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error executing refresh: {ex.Message}");
                return Task.CompletedTask;
            }
            finally
            {
                connection.Close();
            }
            
            Trace.WriteLine("All tables cleared successfully.");
            return Task.CompletedTask;
        }
        
        public async void Use(string database)
        {
            OdbcConnection connection = new OdbcConnection(_ConnectionString);

            try
            {
                connection.Open();
                await CheckDatabaseExisting(connection, database);
                _CurrentDatabase = database;
                
                //await Migrate();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error executing changing database: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        async Task CheckDatabaseExisting(OdbcConnection connection, string database)
        {
            string query = $"SHOW DATABASES LIKE '{database}'"; 
            OdbcCommand command = new OdbcCommand(query, connection);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                //Database already exists.
                return;
            }   
            
            string sqlCommandPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Databases{_PathSeparator}SqlCommands");
            string sqlFileName = "create_database.sql";

            string rawSqlCommand = File.ReadAllText($"{sqlCommandPath}{_PathSeparator}{sqlFileName}", Encoding.UTF8);
            string sqlCommand = rawSqlCommand.Replace("%newdb%", database);
            
            command = new OdbcCommand(sqlCommand, connection);
            await command.ExecuteNonQueryAsync();
            
            connection.ChangeDatabase(database);
            
            await CreateMigrationTable(connection, sqlCommandPath);
        }

        async Task CreateMigrationTable(OdbcConnection connection, string sqlCommandPath)
        {
            string sqlFileName = "create_migration_table.sql";
            string sqlCommand = File.ReadAllText($"{sqlCommandPath}{_PathSeparator}{sqlFileName}", Encoding.UTF8);
            
            OdbcCommand command = new OdbcCommand(sqlCommand, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}