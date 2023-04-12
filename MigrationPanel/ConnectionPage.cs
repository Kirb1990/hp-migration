using System;
using System.Drawing;
using System.Windows.Forms;
using MigrationPanel.Exceptions;

namespace MigrationPanel
{
    public partial class AppForm
    {
        void ValidatePervasiveDatabaseAutoConnection()
        {
            if (string.IsNullOrEmpty(textBoxPervasiveServer.Text) ||
                string.IsNullOrEmpty(textBoxPervasivePort.Text) ||
                string.IsNullOrEmpty(textBoxPervasiveUser.Text) ||
                string.IsNullOrEmpty(textBoxPervasivePassword.Text) ||
                string.IsNullOrEmpty(textBoxPervasiveDatabase.Text)
               ) return;

            TestPervasiveConnection();
        }

        void ValidateSqlDatabaseAutoConnection()
        {
            if (string.IsNullOrEmpty(textBoxSqlServer.Text) ||
                string.IsNullOrEmpty(textBoxSqlPort.Text) ||
                string.IsNullOrEmpty(textBoxSqlUser.Text) ||
                string.IsNullOrEmpty(textBoxSqlPassword.Text) ||
                string.IsNullOrEmpty(textBoxSqlDatabase.Text)
               ) return;

            TestSqlConnection();
        }

        void OnSqlInputBoxChanged(object sender, EventArgs e)
        {
            SetTabEnabled(migrationPage, false);
            labelSqlTest.Text = string.Empty;
        }
        
        void OnPervasiveInputBoxChanged(object sender, EventArgs e)
        {
            SetTabEnabled(mappingPage, false);
            SetTabEnabled(importPage, false);
            labelPervasvieTest.Text = string.Empty;
        }
        
        
        void TestSqlConnection()
        {
            string connectionString;
            string database;

            try
            {
                BuildSqlConnectionString(out connectionString, out database);
            }
            catch (TextBoxInputException exception)
            {
                SetLabelSqlTest(exception.Message, Color.DarkRed);
                return;
            }

            _Migrator.SetSqlConnectionString(connectionString);

            if (!_Migrator.TestMySqlConnection())
            {
                SetLabelSqlTest("Keine Verbindung möglich!", Color.DarkRed);
                return;
            }

            if (forceCreateSqlDatabase.Checked && !_Migrator.UseWithCreateDatabaseIfNotExists(database))
            {
                SetLabelSqlTest("Fehler beim wechseln oder Erstellen der Datenbank!", Color.DarkRed);
                return;
            }

            if (!_Migrator.Use(database))
            {
                SetLabelSqlTest("Fehler beim wechseln der Datenbank, existiert diese?", Color.DarkRed);
                return;
            }

            SetTabEnabled(migrationPage, true);
            SetLabelSqlTest("MySQL Verbindung erfolgreich aufgebaut!", Color.DarkGreen);
        }

        void TestPervasiveConnection()
        {
            string connectionString;

            try
            {
                BuildPervasiveConnectionString(out connectionString);
            }
            catch (TextBoxInputException exception)
            {
                SetLabelPervasiveTest(exception.Message, Color.DarkRed);
                return;
            }

            _Migrator.SetPervasiveConnectionString(connectionString);

            if (!_Migrator.TestPervasiveConnection())
            {
                SetLabelPervasiveTest("Keine Verbindung möglich!", Color.DarkRed);
                return;
            }

            SetTabEnabled(mappingPage, true);
            SetTabEnabled(importPage, true);
            SetLabelPervasiveTest("Pervasive Verbindung erfolgreich aufgebaut!", Color.DarkGreen);
        }
        void btnSqlConnectionTest_Click(object sender, EventArgs e) => TestSqlConnection();

        void BuildSqlConnectionString(out string connectionString, out string database)
        {
            string server = ReadInputBox(textBoxSqlServer);
            string port = ReadInputBox(textBoxSqlPort);
            string user = ReadInputBox(textBoxSqlUser);
            string password = ReadInputBox(textBoxSqlPassword);
            database = ReadInputBox(textBoxSqlDatabase);

            connectionString = $"server={server},{port};uid={user};password={password};";
        }
        
        
        void SetLabelSqlTest(string message, Color color)
        {
            labelSqlTest.Text = message;
            labelSqlTest.ForeColor = color;
        }

        void SetLabelPervasiveTest(string message, Color color)
        {
            labelPervasvieTest.Text = message;
            labelPervasvieTest.ForeColor = color;
        }

        string ReadInputBox(Control textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                throw new TextBoxInputException($"[{textBox.Name}] Überprüfe ob das Feld korrekt gefüllt sind.");
            }

            return textBox.Text;
        }

        void btnPervasiveConnectionTest_Click(object sender, EventArgs e) => TestPervasiveConnection();

        void BuildPervasiveConnectionString(out string connectionString)
        {
            string server = ReadInputBox(textBoxPervasiveServer);
            string port = ReadInputBox(textBoxPervasivePort);
            string user = ReadInputBox(textBoxPervasiveUser);
            string password = ReadInputBox(textBoxPervasivePassword);
            string database = ReadInputBox(textBoxPervasiveDatabase);

            connectionString = $"Server={server};Port={port};Database={database};User ID={user};Password={password};";
        }

    }
}