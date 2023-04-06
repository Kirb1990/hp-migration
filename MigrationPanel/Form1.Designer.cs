namespace MigrationPanel
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textCreateDatabase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.migrationControl = new System.Windows.Forms.TabControl();
            this.connectionPage = new System.Windows.Forms.TabPage();
            this.textBoxSqlDatabase = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelSqlTest = new System.Windows.Forms.Label();
            this.btnSqlConnectionTest = new System.Windows.Forms.Button();
            this.textBoxSqlPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSqlUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSqlPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSqlServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.migrationPage = new System.Windows.Forms.TabPage();
            this.convertionPage = new System.Windows.Forms.TabPage();
            this.migrationControl.SuspendLayout();
            this.connectionPage.SuspendLayout();
            this.migrationPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // textCreateDatabase
            // 
            this.textCreateDatabase.Location = new System.Drawing.Point(32, 64);
            this.textCreateDatabase.Name = "textCreateDatabase";
            this.textCreateDatabase.Size = new System.Drawing.Size(192, 20);
            this.textCreateDatabase.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 47);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hier kannst du eine neue Datenbank erstellen, indem du einfach den Namen der Date" + "nbank in das untenstehende Feld schreibst und auf Erstellen klickst.";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(242, 64);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 20);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Erstellen";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 55);
            this.label2.TabIndex = 3;
            this.label2.Text = "Datenbank erfolgreich erstellt!";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // migrationControl
            // 
            this.migrationControl.Controls.Add(this.connectionPage);
            this.migrationControl.Controls.Add(this.migrationPage);
            this.migrationControl.Controls.Add(this.convertionPage);
            this.migrationControl.Location = new System.Drawing.Point(0, 0);
            this.migrationControl.Name = "migrationControl";
            this.migrationControl.SelectedIndex = 0;
            this.migrationControl.Size = new System.Drawing.Size(482, 410);
            this.migrationControl.TabIndex = 5;
            // 
            // connectionPage
            // 
            this.connectionPage.Controls.Add(this.textBoxSqlDatabase);
            this.connectionPage.Controls.Add(this.label8);
            this.connectionPage.Controls.Add(this.labelSqlTest);
            this.connectionPage.Controls.Add(this.btnSqlConnectionTest);
            this.connectionPage.Controls.Add(this.textBoxSqlPassword);
            this.connectionPage.Controls.Add(this.label7);
            this.connectionPage.Controls.Add(this.textBoxSqlUser);
            this.connectionPage.Controls.Add(this.label6);
            this.connectionPage.Controls.Add(this.textBoxSqlPort);
            this.connectionPage.Controls.Add(this.label5);
            this.connectionPage.Controls.Add(this.textBoxSqlServer);
            this.connectionPage.Controls.Add(this.label4);
            this.connectionPage.Controls.Add(this.label3);
            this.connectionPage.Location = new System.Drawing.Point(4, 22);
            this.connectionPage.Name = "connectionPage";
            this.connectionPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionPage.Size = new System.Drawing.Size(474, 384);
            this.connectionPage.TabIndex = 0;
            this.connectionPage.Text = "Verbindung";
            this.connectionPage.UseVisualStyleBackColor = true;
            // 
            // textBoxSqlDatabase
            // 
            this.textBoxSqlDatabase.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.textBoxSqlDatabase.Location = new System.Drawing.Point(76, 133);
            this.textBoxSqlDatabase.MaxLength = 32;
            this.textBoxSqlDatabase.Name = "textBoxSqlDatabase";
            this.textBoxSqlDatabase.Size = new System.Drawing.Size(177, 20);
            this.textBoxSqlDatabase.TabIndex = 12;
            this.textBoxSqlDatabase.Text = "(optional)";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Database:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSqlTest
            // 
            this.labelSqlTest.Location = new System.Drawing.Point(89, 166);
            this.labelSqlTest.Name = "labelSqlTest";
            this.labelSqlTest.Size = new System.Drawing.Size(379, 23);
            this.labelSqlTest.TabIndex = 10;
            this.labelSqlTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSqlConnectionTest
            // 
            this.btnSqlConnectionTest.Location = new System.Drawing.Point(8, 166);
            this.btnSqlConnectionTest.Name = "btnSqlConnectionTest";
            this.btnSqlConnectionTest.Size = new System.Drawing.Size(75, 23);
            this.btnSqlConnectionTest.TabIndex = 9;
            this.btnSqlConnectionTest.Text = "Testen";
            this.btnSqlConnectionTest.UseVisualStyleBackColor = true;
            this.btnSqlConnectionTest.Click += new System.EventHandler(this.btnSqlConnectionTest_Click);
            // 
            // textBoxSqlPassword
            // 
            this.textBoxSqlPassword.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.textBoxSqlPassword.Location = new System.Drawing.Point(76, 107);
            this.textBoxSqlPassword.MaxLength = 32;
            this.textBoxSqlPassword.Name = "textBoxSqlPassword";
            this.textBoxSqlPassword.PasswordChar = '*';
            this.textBoxSqlPassword.Size = new System.Drawing.Size(177, 20);
            this.textBoxSqlPassword.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Passwort:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSqlUser
            // 
            this.textBoxSqlUser.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.textBoxSqlUser.Location = new System.Drawing.Point(76, 81);
            this.textBoxSqlUser.MaxLength = 32;
            this.textBoxSqlUser.Name = "textBoxSqlUser";
            this.textBoxSqlUser.Size = new System.Drawing.Size(177, 20);
            this.textBoxSqlUser.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "User:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSqlPort
            // 
            this.textBoxSqlPort.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.textBoxSqlPort.Location = new System.Drawing.Point(76, 55);
            this.textBoxSqlPort.MaxLength = 5;
            this.textBoxSqlPort.Name = "textBoxSqlPort";
            this.textBoxSqlPort.Size = new System.Drawing.Size(48, 20);
            this.textBoxSqlPort.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Port:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSqlServer
            // 
            this.textBoxSqlServer.Location = new System.Drawing.Point(76, 29);
            this.textBoxSqlServer.Name = "textBoxSqlServer";
            this.textBoxSqlServer.Size = new System.Drawing.Size(177, 20);
            this.textBoxSqlServer.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Server:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "MySQL Verbindung";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // migrationPage
            // 
            this.migrationPage.Controls.Add(this.label1);
            this.migrationPage.Controls.Add(this.button1);
            this.migrationPage.Controls.Add(this.textCreateDatabase);
            this.migrationPage.Controls.Add(this.label2);
            this.migrationPage.Controls.Add(this.btnCreate);
            this.migrationPage.Location = new System.Drawing.Point(4, 22);
            this.migrationPage.Name = "migrationPage";
            this.migrationPage.Padding = new System.Windows.Forms.Padding(3);
            this.migrationPage.Size = new System.Drawing.Size(474, 384);
            this.migrationPage.TabIndex = 1;
            this.migrationPage.Text = "Migration";
            this.migrationPage.UseVisualStyleBackColor = true;
            // 
            // convertionPage
            // 
            this.convertionPage.Location = new System.Drawing.Point(4, 22);
            this.convertionPage.Name = "convertionPage";
            this.convertionPage.Padding = new System.Windows.Forms.Padding(3);
            this.convertionPage.Size = new System.Drawing.Size(474, 384);
            this.convertionPage.TabIndex = 2;
            this.convertionPage.Text = "Konverter";
            this.convertionPage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.migrationControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Migration Panel";
            this.migrationControl.ResumeLayout(false);
            this.connectionPage.ResumeLayout(false);
            this.connectionPage.PerformLayout();
            this.migrationPage.ResumeLayout(false);
            this.migrationPage.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox textBoxSqlDatabase;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Button btnSqlConnectionTest;
        private System.Windows.Forms.Label labelSqlTest;

        private System.Windows.Forms.TextBox textBoxSqlServer;
        private System.Windows.Forms.TextBox textBoxSqlUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.TextBox textBoxSqlPort;
        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.TextBox textBoxSqlPassword;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.TabPage convertionPage;

        private System.Windows.Forms.TabControl migrationControl;
        private System.Windows.Forms.TabPage connectionPage;
        private System.Windows.Forms.TabPage migrationPage;

        private System.Windows.Forms.TextBox textCreateDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;

        #endregion
    }
}