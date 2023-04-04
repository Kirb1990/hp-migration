using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using CommandLine;
using Converter;

namespace MigrationTool
{
    abstract class Program
    {
        static readonly Random _Random = new Random();
        static readonly string _Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        static void Main(string[] args)
        {
            string connectionString;
            try
            {
                string server = LoadRequiredSetting("server");
                string port = LoadRequiredSetting("port");
                string user = LoadRequiredSetting("uid");
                string password = LoadRequiredSetting("password");

                connectionString = $"server={server},{port};uid={user};password={password}";
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine(ex);
                return;
            }
            
            /*
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (o.Migrate)
                    {
                        Migrate();
                    }
                    else if (o.Refresh)
                    {
                        MigrateWithRefresh();
                    }
                    else if (o.MigrateWithRefresh)
                    {
                        MigrateWithSeed();
                    }

                    if (o.Seed)
                    {
                        Seeding();
                    }
                });
            */
        
            Migration tool = new(connectionString);
            tool.Use("aewee");
            tool.Migrate();
        }

        private static string LoadRequiredSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException($"Wert vom Key [{key}] ist nicht in der App.config gesetzt!");
            }

            return value;
        }

        static string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = _Random.Next(_Chars.Length);
                builder.Append(_Chars[index]);
            }
            return builder.ToString();
        }
    }
}
