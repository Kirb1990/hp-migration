using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MigrationTool;

namespace MigrationPanel
{
    internal partial class AppForm : Form
    {
        readonly byte[] _Key = Encoding.UTF8.GetBytes("xfuix61b6DXbWHhYixko6Pc2t24rqxnX");
        readonly byte[] _Iv = Encoding.UTF8.GetBytes("lOqJ7RMpsStRQn42");

        readonly List<TabPage> _DisabledTabPages = new List<TabPage>();
        readonly Migrator _Migrator;
        
        public AppForm()
        {
            _Migrator = new Migrator();
            InitializeComponent();

            migrationControl.Selecting += TabControl_Selecting;
        }

        protected override void OnLoad(EventArgs e)
        {
            DisableTabPages();
            LoadAppSettings();
            ValidateSqlDatabaseAutoConnection();
            ValidatePervasiveDatabaseAutoConnection();
            base.OnLoad(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            SaveAppSettings();
            base.OnClosing(e);
        }

        void DisableTabPages()
        {
            SetTabEnabled(migrationPage, false);
            SetTabEnabled(mappingPage, false);
            SetTabEnabled(importPage, false);
        }     
        void SetTabEnabled(TabPage tabPage, bool enabled)
        {
            if (enabled)
            {
                if (_DisabledTabPages.Contains(tabPage))
                {
                    _DisabledTabPages.Remove(tabPage);
                }
            }
            else
            {
                if (!_DisabledTabPages.Contains(tabPage))
                {
                    _DisabledTabPages.Add(tabPage);
                }
            }
        }
        void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!_DisabledTabPages.Contains(e.TabPage))
            {
                return;
            }
            
            MessageBox.Show("Nicht alle Bedinungen erfüllt! Sind alle Datenbank Verbindungen aufgebaut?");
            e.Cancel = true;
        }
        
        void LoadAppSettings()
        {
            textBoxSqlServer.Text = ConfigurationManager.AppSettings["sql-server"];
            textBoxSqlPort.Text = ConfigurationManager.AppSettings["sql-port"];
            textBoxSqlUser.Text = ConfigurationManager.AppSettings["sql-uid"];
            textBoxSqlPassword.Text = DecryptPassword(ConfigurationManager.AppSettings["sql-password"], _Key, _Iv);
            textBoxSqlDatabase.Text = ConfigurationManager.AppSettings["sql-database"];

            textBoxPervasiveServer.Text = ConfigurationManager.AppSettings["pervasive-server"];
            textBoxPervasivePort.Text = ConfigurationManager.AppSettings["pervasive-port"];
            textBoxPervasiveUser.Text = ConfigurationManager.AppSettings["pervasive-uid"];
            textBoxPervasivePassword.Text = DecryptPassword(ConfigurationManager.AppSettings["pervasive-password"], _Key, _Iv);
            textBoxPervasiveDatabase.Text = ConfigurationManager.AppSettings["pervasive-database"];
        }
        void SaveAppSettings()
        {
            ConfigurationManager.AppSettings["sql-server"] = textBoxSqlServer.Text;
            ConfigurationManager.AppSettings["sql-port"] = textBoxSqlPort.Text;
            ConfigurationManager.AppSettings["sql-uid"] = textBoxSqlUser.Text;
            ConfigurationManager.AppSettings["sql-password"] = EncryptPassword(textBoxSqlPassword.Text, _Key, _Iv);
            ConfigurationManager.AppSettings["sql-database"] = textBoxSqlDatabase.Text;

            ConfigurationManager.AppSettings["pervasive-server"] = textBoxPervasiveServer.Text;
            ConfigurationManager.AppSettings["pervasive-port"] = textBoxPervasivePort.Text;
            ConfigurationManager.AppSettings["pervasive-uid"] = textBoxPervasiveUser.Text;
            ConfigurationManager.AppSettings["pervasive-password"] = EncryptPassword(textBoxPervasivePassword.Text, _Key, _Iv);
            ConfigurationManager.AppSettings["pervasive-database"] = textBoxPervasiveDatabase.Text;
        }
        
        string EncryptPassword(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedText = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Convert.ToBase64String(encryptedText);
                }
            }
        }

        string DecryptPassword(string encryptedText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(encryptedText)) return string.Empty;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] buffer = Convert.FromBase64String(encryptedText);
                    byte[] decryptedText = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(decryptedText);
                }
            }
        }
    }
}