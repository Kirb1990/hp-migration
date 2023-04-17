using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using MigrationTool.Exceptions;
using MigrationTool.Extensions;
using MySqlConnector;
using Newtonsoft.Json;
using Pervasive.Data.SqlClient;

namespace MigrationTool
{
    public class Migrator
    {
        const string AUTO_INCREMENT_PLACEHOLDER = "id";
        public event EventHandler<string> OnSuccessfullyMigrated;
        public event EventHandler<string> OnErrorOccured;
        public event EventHandler<string> OnConverterMessage;
        public string Message => _ErrorMessage;
        public bool HasErrors => _ErrorOccured;
        
        readonly char _PathSeparator;
        
        Mapping _Mapping;
        
        string _SqlConnectionString;
        string _PervasiveConnectionString;
        
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

        public bool TestSqlConnection()
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

                    OnSuccessfullyMigrated?.Invoke(this, $"Migration {migrationName} executed successfully.");
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

                string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = @DatabaseName AND table_type = 'BASE TABLE'";
                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@DatabaseName", connection.Database);
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

        public bool UseForceCreatingDatabase(string database)
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
                if (string.IsNullOrEmpty(connection.Database))
                {
                    OnErrorOccured?.Invoke(this, "Datenbank muss vor dem Wechsel angegeben werden!");
                    return false;
                }
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
            
            return true;
        }

        public bool UseWithDataDirectoryPath(string directoryPath)
        {
            if (!TryExtractDatabaseFromPath(directoryPath, out string database))
            {
                return false;
            }
            
            return Use(database);
        }

        bool TryExtractDatabaseFromPath(string directoryPath, out string database)
        {
            database = string.Empty;
            if (string.IsNullOrEmpty(directoryPath))
            {
                return false;
            }
            
            Regex regex = new(@"\\([^\\]+)\\?$");
            Match match = regex.Match(directoryPath);
            string dirtyDatabaseName = match.Groups[match.Groups.Count-1].Value;
            
            database = RemoveSpecialCharacters(dirtyDatabaseName);
            return true;
        }

        public bool UseForceCreatingDatabaseWithDataDirectoryPath(string directoryPath)
        {
            if (!TryExtractDatabaseFromPath(directoryPath, out string database))
            {
                return false;
            }

            return UseForceCreatingDatabase(database);
        }
        
        string RemoveSpecialCharacters(string str)
        {
            string normalizedString = str.Normalize(NormalizationForm.FormKD);
            Regex regex = new("[^a-zA-Z0-9]");
            return regex.Replace(normalizedString, string.Empty);
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

            const string query = "SHOW TABLES";
            MySqlCommand command = new(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
                
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0));
            }

            reader.Close();

            return tableNames;
        }

        public List<string> LoadPervasiveTableNames() => LoadPervasiveTableNames(_PervasiveConnectionString);

        public List<string> LoadPervasiveTableNames(string connectionString)
        {
            List<string> tableNames = new();

            using PsqlConnection connection = new(connectionString);
            connection.Open();

            const string query = "SELECT DISTINCT Xf$Name FROM X$File";
            PsqlCommand command = new(query, connection);
            PsqlDataReader reader = command.ExecuteReader();
                
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0)?.Trim());
            }

            reader.Close();

            return tableNames;
        }

        public List<string> GetPervasiveFields(string tableName)
        {
            List<string> fieldNames = new();
            using PsqlConnection connection = new(_PervasiveConnectionString);
            connection.Open();

            string query = $"SELECT DISTINCT Xe$Name FROM X$Field LEFT JOIN X$File on Xe$File =  Xf$Id WHERE Xf$Name = '{tableName.Trim()}' and Xe$DataType < 255 and Xe$DataType >= 0 ORDER BY XE$Id";
            PsqlCommand command = new(query, connection);
            PsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                fieldNames.Add(reader.GetString(0)?.Trim());
            }

            reader.Close();

            return fieldNames;
        }

        public List<string> GetPervasiveFieldsWithPlaceholder(string tableName)
        {
            List<string> fieldNames = new() { AUTO_INCREMENT_PLACEHOLDER };
            fieldNames.AddRange(GetPervasiveFields(tableName));
            return fieldNames;
        }

        public List<string> GetSqlFields(string tableName)
        {
            List<string> fieldNames = new();

            using MySqlConnection connection = new(_SqlConnectionString);
            connection.Open();
            
            string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{connection.Database}' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION";

            using MySqlCommand command = new(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                fieldNames.Add(reader.GetString(0)?.Trim());
            }

            return fieldNames;
        }

        void ConvertTable(TablePair tablePair)
        {
            if (!SqlTableIsEmpty(tablePair.SqlTable.Name))
            {
                return;
            }
            
            RemoveAutoIncrementPlaceholder(tablePair);
            ConvertTable(tablePair.PervasiveTable.Name, tablePair.PervasiveTable.Fields, tablePair.SqlTable.Name, tablePair.SqlTable.Fields);
        }

        void ConvertTable(string table, List<string> fields) => ConvertTable(table, fields, table, fields);

        void ConvertTable(string pervasiveTable, List<string> pervasiveFields, string sqlTable, List<string> sqlFields)
        {
            if (!SqlTableExists(sqlTable) || !SqlTableIsEmpty(sqlTable))
            {
                return;
            }
            
            int maxRecords = PervasiveTableRecordAmount(pervasiveTable);

            OnConverterMessage?.Invoke(this, $"Pervasive Tabelle {pervasiveTable} nach MySQL Tabelle {sqlTable} mit {maxRecords} Einträgen!");

            Import(pervasiveTable,  sqlTable, maxRecords);
        }

        bool SqlTableExists(string sqlTable)
        {
            using MySqlConnection connection = new(_SqlConnectionString);
            connection.Open();
            
            string query = $"SELECT count(*) FROM information_schema.tables WHERE table_schema = '{connection.Database}' AND table_name = '{sqlTable}';";
            MySqlCommand command = new(query, connection);

            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count > 0) return true;
            
            OnConverterMessage?.Invoke(this, $"Überspringe nicht existierende Tabelle: {connection.Database}.{sqlTable}");
            return false;
        }

        void RemoveAutoIncrementPlaceholder(TablePair tablePair)
        {
            int placeholderIndex = -1;
            
            for (int i = 0; i < tablePair.PervasiveTable.Fields.Count; i++)
            {
                if (!tablePair.PervasiveTable.Fields[i].Equals(AUTO_INCREMENT_PLACEHOLDER))
                {
                    continue;
                }
                
                tablePair.PervasiveTable.Fields.RemoveAt(i);
                placeholderIndex = i;
            }

            if (placeholderIndex >= 0)
            {
                tablePair.SqlTable.Fields.RemoveAt(placeholderIndex);
            }
        }
        
        void Import(string pervasiveTable, string sqlTable, int maxRecords)
        {
            using PsqlConnection pervasiveConnection = new(_PervasiveConnectionString);
            using MySqlConnection mariaDbConnection = new(_SqlConnectionString);
            pervasiveConnection.Open();
            mariaDbConnection.Open();
            
            //int sqlFieldCount =  MySQ
            

            using PsqlCommand pervasiveCommand = new($"SELECT * FROM {pervasiveTable}", pervasiveConnection);
            using PsqlDataReader pervasiveReader = pervasiveCommand.ExecuteReader();

            string valuePlaceholders = string.Join(",", Enumerable.Range(0, pervasiveReader.FieldCount + 1).Select(i => $"@param{i}"));
            using MySqlCommand mariaDbCommand = new($"INSERT INTO {sqlTable} VALUES ({valuePlaceholders})", mariaDbConnection);
            using MySqlTransaction transaction = mariaDbConnection.BeginTransaction();

            mariaDbCommand.Transaction = transaction;

            try
            {
                while (pervasiveReader.Read())
                {
                    mariaDbCommand.Parameters.AddWithValue("@param0", null);
                    
                    for (int i = 0; i < pervasiveReader.FieldCount; i++)
                    {
                        mariaDbCommand.Parameters.AddWithValue($"@param{i+1}", pervasiveReader.GetValue(i));
                    }

                    mariaDbCommand.ExecuteNonQuery();
                    mariaDbCommand.Parameters.Clear();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                OnConverterMessage?.Invoke(this, $"Tabelle '{pervasiveTable}' abgebrochen.");
                OnErrorOccured?.Invoke(this, $"Fehler beim Import: {ex.Message}");
                return;
            }
            OnConverterMessage?.Invoke(this, $"Tabelle '{pervasiveTable}' {maxRecords}/{maxRecords} abgeschlossen.");
        }

        int PervasiveTableRecordAmount(string tableName)
        {
            int maxRecords = 0;
            using PsqlConnection connection = new(_PervasiveConnectionString);
            connection.Open();

            string query = $"SELECT COUNT(*) FROM {tableName}";
            PsqlCommand command = new(query, connection);
            PsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                maxRecords = Convert.ToInt32(reader.GetString(0));
            }

            reader.Close();
            return maxRecords;
        }

        bool SqlTableIsEmpty(string sqlTableName)
        {
            MySqlConnection connection = new(_SqlConnectionString);

            try
            {
                connection.Open();
                MySqlCommand command = new($"SELECT COUNT(*) FROM {sqlTableName} LIMIT 1;", connection);
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    OnConverterMessage?.Invoke(this, $"Es sind bereits Daten in der MySQL: {connection.Database}.{sqlTableName} vorhanden.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ConvertingException("Fehler: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
        
        public void StartConvert(TablePair? single = null)
        {
            Timer timer = new();
            timer.Start();
            OnConverterMessage?.Invoke(this, "Konverter wurde gestartet.");
            
            if (single is null)
            {
                foreach (TablePair tablePair in Mapping.TablePairs)
                {
                    ConvertTable(tablePair);
                }

                return;
            }
            ConvertTable((TablePair) single);
            timer.Stop();
            OnConverterMessage?.Invoke(this, $"Konverter wurde nach {timer.TimeHumanReadable()} erfolgreich beendet.");
        }

        public void StartConverter(string pervasiveConnectionString, string sqlConnectionString)
        {
            _PervasiveConnectionString = pervasiveConnectionString;
            _SqlConnectionString = sqlConnectionString;
            
            Timer timer = new();
            timer.Start();
            OnConverterMessage?.Invoke(this, "Konverter wurde gestartet.");
            
            //List<string> tables = LoadPervasiveTableNames();
            List<string> tables = new()
            {
                "ad_adr",
                "tes",
                "ad_ansprpart"
            };

            foreach (string table in tables)
            {
                ConvertTable(table, null);
            }
            OnConverterMessage?.Invoke(this, $"Konverter wurde nach {timer.TimeHumanReadable()} erfolgreich beendet.");
        }
    }
}