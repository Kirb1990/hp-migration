﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MigrationPanel.Exceptions;
using MigrationTool;
using Newtonsoft.Json;

namespace MigrationPanel
{
    public partial class AppForm : Form
    {
        private readonly byte[] Key = Encoding.UTF8.GetBytes("xfuix61b6DXbWHhYixko6Pc2t24rqxnX");
        private readonly byte[] Iv = Encoding.UTF8.GetBytes("lOqJ7RMpsStRQn42");
        
        readonly Migrator _Migrator;

        private List<TabPage> disabledTabPages = new List<TabPage>();
        
        public AppForm()
        {
            _Migrator = new Migrator();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            DisableTabPages();
            LoadAppSettings();
            ValidateSqlDatabaseAutoConnection();
            ValidatePervasiveDatabaseAutoConnection();
            base.OnLoad(e);
        }

        void DisableTabPages()
        {
            SetTabEnabled(migrationPage, false);
            SetTabEnabled(mappingPage, false);
            SetTabEnabled(importPage, false);
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
            textBoxSqlPassword.Text = DecryptPassword(ConfigurationManager.AppSettings["sql-password"], Key, Iv);
            textBoxSqlDatabase.Text = ConfigurationManager.AppSettings["sql-database"];

            textBoxPervasiveServer.Text = ConfigurationManager.AppSettings["pervasive-server"];
            textBoxPervasivePort.Text = ConfigurationManager.AppSettings["pervasive-port"];
            textBoxPervasiveUser.Text = ConfigurationManager.AppSettings["pervasive-uid"];
            textBoxPervasivePassword.Text = DecryptPassword(ConfigurationManager.AppSettings["pervasive-password"], Key, Iv);
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
            ConfigurationManager.AppSettings["sql-password"] = EncryptPassword(textBoxSqlPassword.Text, Key, Iv);
            ConfigurationManager.AppSettings["sql-database"] = textBoxSqlDatabase.Text;

            ConfigurationManager.AppSettings["pervasive-server"] = textBoxPervasiveServer.Text;
            ConfigurationManager.AppSettings["pervasive-port"] = textBoxPervasivePort.Text;
            ConfigurationManager.AppSettings["pervasive-uid"] = textBoxPervasiveUser.Text;
            ConfigurationManager.AppSettings["pervasive-password"] = EncryptPassword(textBoxPervasivePassword.Text, Key, Iv);
            ConfigurationManager.AppSettings["pervasive-database"] = textBoxPervasiveDatabase.Text;
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

            List<string> tableNames = _Migrator.LoadPervasiveTableNames();

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
                LoadTableFieldToGrid(dataGridSql, tablePair.PervasiveTable.Fields);
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

        void OnCellClick(object sender, DataGridViewCellEventArgs e) => SelectPervasiveDataRow(e.RowIndex);

        void SelectPervasiveDataRow(int index)
        {
            if (index < 0)
            {
                return;
            }

            dataGridPervasive.ClearSelection();
            dataGridPervasive.Rows[index].Selected = true;

            if (index >= dataGridSql.Rows.Count)
            {
                dataGridSql.ClearSelection();
                return;
            }

            dataGridSql.Rows[index].Selected = true;
        }

        void btnCopyMapping_Click(object sender, EventArgs e)
        {
            if (dataGridSql.Rows.Count <= 0 || dataGridPervasive.Rows.Count <= 0)
            {
                MessageBox.Show("Sind beide Tabellen ausgefüllt?", "Achtung", MessageBoxButtons.OK,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
                MessageBox.Show(exception.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }

            MessageBox.Show("In Zwischenablage gespeichert! Speicher diese json datei in die mapping.json ab.");
        }

        List<Field> ExtractFields(DataGridView dataGridView)
        {
            List<Field> fields = new List<Field>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                fields.Add(new Field
                {
                    Name = (string)dataGridView.Rows[i].Cells[1].Value,
                    Index = dataGridView.Rows[i].Index
                });
            }

            return fields;
        }

        void btnPervasiveRowUp_Click(object sender, EventArgs e)
        {
            if (!TryGetRow(out DataGridViewRow dataGridViewRow, out int index, 1))
            {
                return;
            }

            SwapFieldValues(dataGridViewRow, index - 1);
        }

        void btnPervasiveRowDown_Click(object sender, EventArgs e)
        {
            if (!TryGetRow(out DataGridViewRow dataGridViewRow, out int index, -1))
            {
                return;
            }

            SwapFieldValues(dataGridViewRow, index + 1);
        }

        bool TryGetRow(out DataGridViewRow dataGridViewRow, out int index, int offSet)
        {
            index = -1;
            dataGridViewRow = null;

            if (dataGridPervasive.SelectedRows.Count <= 0)
            {
                return false;
            }

            dataGridViewRow = dataGridPervasive.SelectedRows[0];
            index = dataGridViewRow.Index;

            return index > 0 && index <= dataGridViewRow.Index;
        }

        void SwapFieldValues(DataGridViewRow dataGridRow, int index)
        {
            DataGridViewCell dataGridViewCell = dataGridPervasive.Rows[index].Cells["Feldname"];

            // "Tuple Assignment" swapping the values between the two cells
            (dataGridViewCell.Value, dataGridRow.Cells["Feldname"].Value) =
                (dataGridRow.Cells["Feldname"].Value, dataGridViewCell.Value);

            SelectPervasiveDataRow(index);
        }
        
        private void SetTabEnabled(TabPage tabPage, bool enabled)
        {
            if (enabled)
            {
                if (disabledTabPages.Contains(tabPage))
                {
                    disabledTabPages.Remove(tabPage);
                }
            }
            else
            {
                if (!disabledTabPages.Contains(tabPage))
                {
                    disabledTabPages.Add(tabPage);
                }
            }
        }
        
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (disabledTabPages.Contains(e.TabPage))
            {
                MessageBox.Show("Nicht alle Bedinungen erfüllt! Sind alle Datenbank Verbindungen aufgebaut?");
                e.Cancel = true;
            }
        }

        private string EncryptPassword(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                {
                    var buffer = Encoding.UTF8.GetBytes(plainText);
                    var encryptedText = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Convert.ToBase64String(encryptedText);
                }
            }
        }
        
        private string DecryptPassword(string encryptedText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(encryptedText)) return string.Empty;
            
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    var buffer = Convert.FromBase64String(encryptedText);
                    var decryptedText = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(decryptedText);
                }
            }
        }
    }
}