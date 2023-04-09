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
        Migrator _Migrator;
        
        public AppForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            textBoxSqlServer.Text = ConfigurationManager.AppSettings["sql-server"];
            textBoxSqlPort.Text = ConfigurationManager.AppSettings["sql-port"];
            textBoxSqlUser.Text = ConfigurationManager.AppSettings["sql-uid"];
            textBoxSqlPassword.Text = ConfigurationManager.AppSettings["sql-password"];
            textBoxSqlDatabase.Text = ConfigurationManager.AppSettings["sql-database"];
            
            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ConfigurationManager.AppSettings["sql-server"] = textBoxSqlServer.Text;
            ConfigurationManager.AppSettings["sql-port"] = textBoxSqlPort.Text;
            ConfigurationManager.AppSettings["sql-uid"] = textBoxSqlUser.Text;
            ConfigurationManager.AppSettings["sql-password"] = textBoxSqlPassword.Text;
            ConfigurationManager.AppSettings["sql-database"] = textBoxSqlDatabase.Text;
            
            base.OnClosing(e);
        }

        void OnControlPageIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = migrationControl.SelectedIndex;

            if (selectedIndex == 1)
            {
                if (pervasiveComboBox.MaxLength > 0)
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
                
                mysqlComboBox.Items.AddRange(tableNames.ToArray()); ;
            }
        }

        void OnPervasiveComboBoxChanged(object sender, EventArgs e)
        {
            string tableName = pervasiveComboBox.SelectedItem.ToString();
            
            
            dataGridPervasiveFields.Rows.Clear();
            for (int i = 1; i <= 10; i++)
            {
                dataGridPervasiveFields.Rows.Add("Eintrag " + i.ToString());
            }
        }

        void LoadPervasiveTableNamesToComboBox()
        {
            pervasiveComboBox.Items.Add("AD_Adr");
            pervasiveComboBox.Items.Add("OB_Obj");
        }

        void btnSqlConnectionTest_Click(object sender, EventArgs e)
        {
            string connectionString;
            
            try
            {
                string server = ReadInputBox(textBoxSqlServer);
                string port = ReadInputBox(textBoxSqlPort);
                string user = ReadInputBox(textBoxSqlUser);
                string password = ReadInputBox(textBoxSqlPassword);
                
                connectionString = $"server={server},{port};uid={user};password={password}";
            }
            catch (TextBoxInputException exception)
            {
                SetLabelSqlTest(exception.Message, Color.DarkRed);
                return;
            }
            
            _Migrator = new Migrator(connectionString);
            if (_Migrator.TestMySqlConnection())
            {
                SetLabelSqlTest("Verbindung erfolreich!", Color.DarkGreen);
                return;
            }
            
            SetLabelSqlTest("Keine Verbindung möglich!", Color.DarkRed);
        }

        void SetLabelSqlTest(string message, Color color)
        {
            labelSqlTest.Text = message;
            labelSqlTest.ForeColor = color;
        }

        string ReadInputBox(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                throw new TextBoxInputException($"[{textBox.Name}] Überprüfe ob das Feld korrekt gefüllt sind.");
            }

            return textBox.Text;
        }
    }
}