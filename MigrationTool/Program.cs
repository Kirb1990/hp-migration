using System;
using System.Text;
using CommandLine;
using Converter;

namespace MigrationTool
{
    abstract class Program
    {
        static readonly Random _Random = new Random();
        static readonly string _Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        // Der User und password sollte nicht im Klartext in der ODBC hinterlegt werden.
        const string CONNECTION_STRING = "DSN=MYSQL;Uid=root;Pwd=root;";
        
        static void Main(string[] args)
        {
            Migration tool = new(CONNECTION_STRING);
            tool.Use("neuer_bestand_2");
            tool.Migrate();
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
