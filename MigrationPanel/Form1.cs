using System;
using System.Drawing;
using System.Windows.Forms;
using MigrationPanel.Exceptions;

namespace MigrationTool
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
                
                connectionString = $"server={server},{port};uid={user};password={password}";
            }
            catch (TextBoxInputException exception)
            {
                SetLabelSqlTest(exception.Message, Color.Crimson);
                return;
            }
            
            Migrator migrator = new Migrator(connectionString);
            if (migrator.TestMySqlConnection())
            {
                SetLabelSqlTest("Verbindung erfolreich!", Color.GreenYellow);
                return;
            }
            
            SetLabelSqlTest("Keine Verbindung möglich!", Color.Crimson);
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