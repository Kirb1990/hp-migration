using System;
using System.Configuration;
using CommandLine;

namespace MigrationTool
{
    abstract class Program
    {
        static void Main(string[] args)
        {
            string database;
            string connectionString;
            
            try
            {
                LoadAppSettings(out database, out connectionString);
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            try
            {
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed(o =>
                    {
                        if (o.Migrate)
                        {
                            Migrate(database, connectionString);
                        }
                        else if (o.Refresh)
                        {
                            Refresh(database, connectionString);
                        }
                        else if (o.MigrateWithRefresh)
                        {
                            MigrateWithRefresh(database, connectionString);
                        }
                        if (o.Seed)
                        {
                            Seeding(database, connectionString);
                        }
                    });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void LoadAppSettings(out string database, out string connectionString)
        {
            string server = LoadRequiredSetting("server");
            string port = LoadRequiredSetting("port");
            string user = LoadRequiredSetting("uid");
            string password = LoadRequiredSetting("password");
            database = LoadRequiredSetting("database");

            connectionString = $"server={server},{port};uid={user};password={password}";
        }

        static void Seeding(string database, string connectionString)
        {
            throw new NotImplementedException();
        }

        static void MigrateWithRefresh(string database, string connectionString)
        {
            Migrator migrator = new(connectionString);
            migrator.UseWithCreateDatabaseIfNotExists(database);
            migrator.Refresh();
            migrator.Migrate();
        }

        static void Refresh(string database, string connectionString)
        {
            Migrator migrator = new(connectionString);
            migrator.UseWithCreateDatabaseIfNotExists(database);
            migrator.Refresh();
        }

        static void Migrate(string database, string connectionString)
        {
            Migrator migrator = new(connectionString);
            migrator.UseWithCreateDatabaseIfNotExists(database);
            migrator.Migrate();
        }

        static string LoadRequiredSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException($"Wert vom Key [{key}] ist nicht in der App.config gesetzt!");
            }

            return value;
        }
    }
}
