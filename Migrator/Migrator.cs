using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Pervasive.Data.SqlClient;

namespace MigrationTool
{
    public class Migrator
    {
        public event EventHandler<string> OnSuccessfullyMigrated;
        public event EventHandler<string> OnErrorOccured;
        public string Message => _ErrorMessage;
        public bool HasErrors => _ErrorOccured;
        
        readonly char _PathSeparator;
        
        Mapping _Mapping;
        
        string _SqlConnectionString;
        string _PervasiveConnectionString;
        
        string _CurrentDatabase;
        string _ErrorMessage;
        
        bool _ErrorOccured;

        public Mapping Mapping => _Mapping;

        public Migrator(string sqlSqlConnectionString, string pervasiveConnectionString)
        {
            _SqlConnectionString = sqlSqlConnectionString;
            _PervasiveConnectionString = pervasiveConnectionString;
            _PathSeparator = Path.DirectorySeparatorChar;
            
            Trace.Listeners.Add(new TimestampedTextWriterTraceListener(".\\trace.log"));
            Trace.Listeners.Add(new TimestampedConsoleTraceListener());

            Trace.AutoFlush = true;

            LoadMapping();
            
            OnErrorOccured += ErrorHandler;
            OnSuccessfullyMigrated += SuccessHandler;
        }

        void LoadMapping()
        {
            string mappingString =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mapping.json"), Encoding.UTF8);
            _Mapping = JsonConvert.DeserializeObject<Mapping>(mappingString);
        }

        public void AddTablePairToMapping(TablePair tablePair)
        {
            _Mapping.TablePairs.Add(tablePair);
        }
        
        public void AddTablePairsToMapping(List<TablePair> tablePairs)
        {
            _Mapping.TablePairs.AddRange(tablePairs);
        }

        public Migrator(string sqlSqlConnectionString) : this(sqlSqlConnectionString, string.Empty) { }
        public Migrator() : this(string.Empty) { }

        void ErrorHandler(object sender, string e)
        {
            _ErrorMessage = e;
            _ErrorOccured = true;
            
            Trace.WriteLine(_ErrorMessage);
        }
        
        void SuccessHandler(object sender, string e)
        {
            Trace.WriteLine(e);
        }

        public bool TestMySqlConnection()
        {
            MySqlConnection connection = new(_SqlConnectionString);
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
        public void SetPervasiveConnectionString(string pervasiveConnectionString)
        {
            _PervasiveConnectionString = pervasiveConnectionString;
        }
        public void SetSqlConnectionString(string sqlConnectionString)
        {
            _SqlConnectionString = sqlConnectionString;
        }
        public bool MySqlDatabaseExists(string database)
        {
            MySqlConnection connection = new (_SqlConnectionString);

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
            PsqlConnection connection = new(pervasiveConnectionString);
            try
            {
                if (string.IsNullOrEmpty(pervasiveConnectionString))
                {
                    throw new ArgumentNullException("Pervasive Connection String is Empty!");
                }
                
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
        public bool TestPervasiveConnection()
        {
            return TestPervasiveConnection(_PervasiveConnectionString);
        }
        public bool Migrate()
        {
            MySqlConnection connection = new(_SqlConnectionString);
            
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

                    OnSuccessfullyMigrated?.Invoke(this, $"Migration {migration} executed successfully.");
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

            OnSuccessfullyMigrated?.Invoke(this, "All migrations executed successfully.");
            return true;
        }
        public bool Refresh()
        {
            using MySqlConnection connection = new(_SqlConnectionString);
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
                    OnSuccessfullyMigrated?.Invoke(this, deleteQuery);
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
            
            OnSuccessfullyMigrated?.Invoke(this, "All tables cleared successfully.");
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
            MySqlConnection connection = new (_SqlConnectionString);

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
            MySqlConnection connection = new (_SqlConnectionString);

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

        public List<string> LoadMySqlTableNames()
        {
            List<string> tableNames = new();

            using MySqlConnection connection = new(_SqlConnectionString);
            connection.Open();

            string query = "SHOW TABLES";
            MySqlCommand command = new(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
                
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0));
            }

            reader.Close();

            return tableNames;
        }
        public List<string> LoadPervasiveTableNames()
        {
            List<string> tableNames = new();

            using PsqlConnection connection = new(_PervasiveConnectionString);
            connection.Open();

            string query = "SELECT DISTINCT Xf$Name FROM X$File";
            PsqlCommand command = new(query, connection);
            PsqlDataReader reader = command.ExecuteReader();
                
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0));
            }

            reader.Close();

            return tableNames;
        }

        public List<string> GetPervasiveFields(string tableName)
        {
            List<string> fieldNames = new() { "AUTO_INCREMENT" };

            using PsqlConnection connection = new(_PervasiveConnectionString);
            connection.Open();

            string query = $"SELECT DISTINCT Xe$Name FROM X$Field LEFT JOIN X$File on Xe$File =  Xf$Id WHERE Xf$Name = '{tableName}' and Xe$DataType < 255 and Xe$DataType >= 0";
            PsqlCommand command = new(query, connection);
            PsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                fieldNames.Add(reader.GetString(0)?.Trim());
            }

            reader.Close();

            return fieldNames;
        }

        public List<string> GetSqlFields(string tableName)
        {
            List<string> fieldNames = new();

            using MySqlConnection connection = new(_SqlConnectionString);
            connection.Open();
            
            string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{_CurrentDatabase}' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION";

            using MySqlCommand command = new(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                fieldNames.Add(reader.GetString(0)?.Trim());
            }

            return fieldNames;
        }
    }
}