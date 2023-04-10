using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MigrationPanel.Exceptions;
using MigrationTool;

namespace MigrationPanel
{
    public partial class AppForm : Form
    {
        readonly Migrator _Migrator;
        
        public AppForm()
        {
            _Migrator = new Migrator();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadAppSettings();
            base.OnLoad(e);
        }

        void LoadAppSettings()
        {
            textBoxSqlServer.Text = ConfigurationManager.AppSettings["sql-server"];
            textBoxSqlPort.Text = ConfigurationManager.AppSettings["sql-port"];
            textBoxSqlUser.Text = ConfigurationManager.AppSettings["sql-uid"];
            textBoxSqlPassword.Text = ConfigurationManager.AppSettings["sql-password"];
            textBoxSqlDatabase.Text = ConfigurationManager.AppSettings["sql-database"];
            
            textBoxPervasiveServer.Text = ConfigurationManager.AppSettings["pervasive-server"];
            textBoxPervasivePort.Text = ConfigurationManager.AppSettings["pervasive-port"];
            textBoxPervasiveUser.Text = ConfigurationManager.AppSettings["pervasive-uid"];
            textBoxPervasivePassword.Text = ConfigurationManager.AppSettings["pervasive-password"];
            textBoxPervasiveDatabase.Text = ConfigurationManager.AppSettings["pervasive-database"];
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveAppSettings();
            base.OnClosing(e);
        }

        void SaveAppSettings()
        {
            ConfigurationManager.AppSettings["sql-server"] = textBoxSqlServer.Text;
            ConfigurationManager.AppSettings["sql-port"] = textBoxSqlPort.Text;
            ConfigurationManager.AppSettings["sql-uid"] = textBoxSqlUser.Text;
            ConfigurationManager.AppSettings["sql-password"] = textBoxSqlPassword.Text;
            ConfigurationManager.AppSettings["sql-database"] = textBoxSqlDatabase.Text;
            
            ConfigurationManager.AppSettings["pervasive-server"] = textBoxPervasiveServer.Text;
            ConfigurationManager.AppSettings["pervasive-port"] =  textBoxPervasivePort.Text;
            ConfigurationManager.AppSettings["pervasive-uid"] = textBoxPervasiveUser.Text;
            ConfigurationManager.AppSettings["pervasive-password"] = textBoxPervasivePassword.Text;
            ConfigurationManager.AppSettings["pervasive-database"] = textBoxPervasiveDatabase.Text;
        }

        void OnControlPageIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = migrationControl.SelectedIndex;

            if (selectedIndex == 1)
            {
                if (comboBoxPervasive.MaxLength > 0)
                {
                    return;
                }

                if (_Migrator is null)
                {
                    return;
                }

                LoadPervasiveTableNamesToComboBox();
                LoadSqlTableNamesToComboBox();
            }
        }

        void LoadSqlTableNamesToComboBox()
        {
            List<string> tableNames = _Migrator.LoadMySqlTableNames();
    
            if (tableNames.Count > 0)
            {
                comboBoxSql.Items.AddRange(tableNames.ToArray()); ;
            }
        }

        void OnPervasiveComboBoxChanged(object sender, EventArgs e)
        {
            string tableName = comboBoxPervasive.SelectedItem.ToString();
            
            // TODO: wenn bereits eine Migrationsmapping für diese tabelle existiert, diese laden, sonst aus der Datenbank die felder laden
            
            
            dataGridPervasive.Rows.Clear();
            for (int i = 1; i <= 10; i++)
            {
                dataGridPervasive.Rows.Add("Eintrag " + i.ToString());
            }
        }

        void LoadPervasiveTableNamesToComboBox()
        {
            comboBoxPervasive.Items.Add("AD_Adr");
            comboBoxPervasive.Items.Add("OB_Obj");
        }

        void btnSqlConnectionTest_Click(object sender, EventArgs e)
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
            SetLabelSqlTest("MySQL Verbindung erfolgreich aufgebaut!", Color.DarkGreen);
        }

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

        void btnPervasiveConnectionTest_Click(object sender, EventArgs e)
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

            SetLabelPervasiveTest("Pervasive Verbindung erfolgreich aufgebaut!", Color.DarkGreen);
        }

        void BuildPervasiveConnectionString(out string connectionString)
        {
            string server = ReadInputBox(textBoxPervasiveServer);
            string port = ReadInputBox(textBoxPervasivePort);
            string user = ReadInputBox(textBoxPervasiveUser);
            string password = ReadInputBox(textBoxPervasivePassword); 
            string database = ReadInputBox(textBoxPervasiveDatabase);
                
            connectionString = $"Server={server};Port={port};Database={database};User ID={user};Password={password};";
        }

        void btnStartMigrate_Click(object sender, EventArgs e)
        {
            textBoxMigrationLog.Clear();
            _Migrator.Migrate();
        }

        void MigrationPage_Enter(object sender, EventArgs e)
        {
            _Migrator.OnSuccessfullyMigrated += AppendMigrateLogText;
            _Migrator.OnErrorOccured += AppendMigrateLogText;
        }
        
        void MigrationPage_Leave(object sender, EventArgs e)
        {
            _Migrator.OnSuccessfullyMigrated -= AppendMigrateLogText;
            _Migrator.OnErrorOccured -= AppendMigrateLogText;
        }
        void AppendMigrateLogText(object sender, string e)
        {
            textBoxMigrationLog.Text += $"{e}\n";
        }

        void MappingPage_Enter(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        void MappingPage_Leave(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}