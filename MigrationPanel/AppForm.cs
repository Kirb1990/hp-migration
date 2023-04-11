using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MigrationPanel.Exceptions;
using MigrationTool;
using Newtonsoft.Json;

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
            ValidateSqlDatabaseAutoConnection();
            ValidatePervasiveDatabaseAutoConnection();
            base.OnLoad(e);
        }

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

        void LoadSqlTableNamesToComboBox()
        {
            if (comboBoxSql.Items.Count > 0)
            {
                return;
            }
            
            List<string> tableNames = _Migrator.LoadMySqlTableNames();

            if (tableNames.Count <= 0)
            {
                return;
            }

            foreach (string tableName in tableNames)
            {
                comboBoxSql.Items.Add(tableName);
            }
        }

        void SwitchComboBox(ComboBox comboBox, string searchValue)
        {
            int index = comboBox.FindStringExact(searchValue);

            if (index >= 0)
            {
                comboBox.SelectedIndex = index;
            }
        }

        void LoadTableFieldToGrid(DataGridView dataGridView, List<Field> fields)
        {
            for (int i = 0; i < fields.Count; i++)
            {
                dataGridView.Rows.Add(fields[i].Index, fields[i].Name);
            }
        }

        void LoadPervasiveTableNamesToComboBox()
        {
            if (comboBoxPervasive.Items.Count > 0)
            {
                return;
            }
            
            List<string> tableNames =  _Migrator.LoadPervasiveTableNames();

            if (tableNames.Count <= 0) return;
           
            comboBoxPervasive.Items.Clear();

            foreach (string tableName in tableNames)
            {
                comboBoxPervasive.Items.Add(tableName);
            }
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
            if (comboBoxPervasive.MaxLength <= 0)
            {
                LoadPervasiveTableNamesToComboBox();
            }

            if (comboBoxSql.MaxLength <= 0)
            {
                LoadSqlTableNamesToComboBox();
            }
        }
        
        void OnComboBoxPervasiveChanged(object sender, EventArgs e)
        {
            string tableName = comboBoxPervasive.SelectedItem.ToString();
            
            dataGridPervasive.Rows.Clear();
            
            if (_Migrator.Mapping.TryGet(tableName, out TablePair tablePair))
            {
                LoadTableFieldToGrid(dataGridSql ,tablePair.PervasiveTable.Fields);
                SwitchComboBox(comboBoxSql, tablePair.SqlTable.Name);
                return;
            }
            
            List<Field> fields = _Migrator.GetPervasiveFields(tableName);
            LoadTableFieldToGrid(dataGridPervasive, fields);
        }
        
        void OnComboBoxSqlChanged(object sender, EventArgs e)
        {
            string tableName = comboBoxSql.SelectedItem.ToString();
            
            dataGridSql.Rows.Clear();
            
            if (_Migrator.Mapping.TryGet(tableName, out TablePair tablePair))
            {
                SwitchComboBox(comboBoxSql, tablePair.PervasiveTable.Name);
                return;
            }
            
            List<Field> fields = _Migrator.GetSqlFields(tableName);
            RemoveSqlIdField(fields);

            LoadTableFieldToGrid(dataGridSql, fields);
        }

        void RemoveSqlIdField(List<Field> fields)
        {
            int index = fields.FindIndex(f => f.Name.Equals("id"));
            if (index >= 0)
            {
                fields.RemoveAt(index);
            }
        }

        void OnDataGridScroll(object sender, ScrollEventArgs e)
        {
            int pervasiveRowIndex = dataGridPervasive.FirstDisplayedScrollingRowIndex;
            if (pervasiveRowIndex > dataGridSql.FirstDisplayedScrollingRowIndex)
            {
                return;
            }
            
            dataGridSql.FirstDisplayedScrollingRowIndex = pervasiveRowIndex;
        }

        void OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            
            if (e.RowIndex >= dataGridSql.Rows.Count)
            {
                dataGridSql.ClearSelection();
                return;
            }
            
            dataGridSql.ClearSelection();
            dataGridSql.Rows[e.RowIndex].Selected = true;
        }

        void btnCopyMapping_Click(object sender, EventArgs e)
        {
            if (dataGridSql.Rows.Count <= 0 || dataGridPervasive.Rows.Count <= 0)
            {
                MessageBox.Show("Sind beide Tabellen ausgefüllt?", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            
            Table pervasiveTable = new Table
            {
                Name = comboBoxPervasive.Text,
                Fields = ExtractFields(dataGridPervasive)
            };
            Table sqlTable = new Table
            {
                Name = comboBoxSql.Text,
                Fields = ExtractFields(dataGridSql)
            };

            Mapping mapping = new Mapping
            {
                TablePairs = new List<TablePair>
                {
                    new TablePair
                    {
                        PervasiveTable = pervasiveTable,
                        SqlTable = sqlTable
                    }
                }
            };

            try
            {
                Clipboard.SetText(JsonConvert.SerializeObject(mapping));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }

            MessageBox.Show("In Zwischenablage gespeichert! Speicher diese json datei in die mapping.json ab.");
        }

        List<Field> ExtractFields(DataGridView dataGridView)
        {
            List<Field> fields = new List<Field>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                fields.Add(new Field {
                    Name = (string)dataGridView.Rows[i].Cells[1].Value,
                    Index =  dataGridView.Rows[i].Index
                });
            }

            return fields;
        }
    }
}