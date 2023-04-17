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

                        if (o.Test)
                        {
                            TestSzenario();
                        }
                    });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void TestSzenario()
        {
            Migrator migrator = new();
            
            migrator.OnConverterMessage += WriteToConsole;
            migrator.OnErrorOccured += WriteToConsole;

            //migrator.UseWithDataDirectoryPath(@"D:\Daten\Dev SQL\Blub\Te eäst\");

            migrator.StartConverter("Server=localhost;Port=1583;Database=HAUSPERFEKT;User ID=Master;Password=c4kbg10S;","server=localhost,3306;uid=67tuD6S52eKS;password=nYNQFTsTm9UE;database=hausperfekt");
        }

        static void WriteToConsole(object sender, string e)
        {
            Console.WriteLine(e);
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
            migrator.UseForceCreatingDatabase(database);
            migrator.Refresh();
            migrator.Migrate();
        }

        static void Refresh(string database, string connectionString)
        {
            Migrator migrator = new(connectionString);
            migrator.UseForceCreatingDatabase(database);
            migrator.Refresh();
        }

        static void Migrate(string database, string connectionString)
        {
            Migrator migrator = new(connectionString);
            migrator.UseForceCreatingDatabase(database);
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
