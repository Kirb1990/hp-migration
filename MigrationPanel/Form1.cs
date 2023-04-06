using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MigrationPanel.Exceptions;

namespace MigrationPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void btnSqlConnectionTest_Click(object sender, EventArgs e)
        {
            string connectionString = string.Empty;
            
            try
            {
                string server = ReadInputBox(textBoxSqlServer);
                string port = ReadInputBox(textBoxSqlPort);
                string user = ReadInputBox(textBoxSqlUser);
                string password = ReadInputBox(textBoxSqlPassword);
            }
            catch (Exception exception)
            {
                
                throw;
            }
            
        }

        string ReadInputBox(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                throw new TextBoxInputException($"[{textBox.Name}] Überprüfe ob die Felder korrekt gefüllt sind. ");
            }

            return textBox.Text;
        }
    }
}