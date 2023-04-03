using System;
using System.Text;
using CommandLine;
using Converter;

namespace MigrationTool
{
    abstract class Program
    {
        private static Random random = new Random();
        private static string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        
        static void Main(string[] args)
        {
            // Der User und password sollte nicht im Klartext in der ODBC hinterlegt werden.
            string connectionString = "DSN=DATENDIENST;Uid=root;Pwd=root;";
        
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
            Migration tool = new Migration(connectionString);
            tool.Use("hausperfekt");
            tool.Migrate();
        }

        static void Seeding()
        {
            throw new System.NotImplementedException();
        }

        static void MigrateWithSeed()
        {
            throw new System.NotImplementedException();
        }

        static void MigrateWithRefresh()
        {
            throw new System.NotImplementedException();
        }

        static void Migrate()
        {
            throw new System.NotImplementedException();
        }
        static string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                builder.Append(chars[index]);
            }
            return builder.ToString();
        }
    }
}
