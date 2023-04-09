using System.ComponentModel;

namespace MigrationPanel
{
    partial class AppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            this.label1 = new System.Windows.Forms.Label();
            this.migrationControl = new System.Windows.Forms.TabControl();
            this.connectionPage = new System.Windows.Forms.TabPage();
            this.forceCreateSqlDatabase = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
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
            this.mappingPage = new System.Windows.Forms.TabPage();
            this.dataGridmysql = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridPervasiveFields = new System.Windows.Forms.DataGridView();
            this.Felder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pervasiveComboBox = new System.Windows.Forms.ComboBox();
            this.mysqlComboBox = new System.Windows.Forms.ComboBox();
            this.importPage = new System.Windows.Forms.TabPage();
            this.migrationControl.SuspendLayout();
            this.connectionPage.SuspendLayout();
            this.mappingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridmysql)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPervasiveFields)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 47);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // migrationControl
            // 
            this.migrationControl.Controls.Add(this.connectionPage);
            this.migrationControl.Controls.Add(this.mappingPage);
            this.migrationControl.Controls.Add(this.importPage);
            this.migrationControl.Location = new System.Drawing.Point(0, 0);
            this.migrationControl.Name = "migrationControl";
            this.migrationControl.SelectedIndex = 0;
            this.migrationControl.Size = new System.Drawing.Size(482, 410);
            this.migrationControl.TabIndex = 5;
            // 
            // connectionPage
            // 
            this.connectionPage.Controls.Add(this.forceCreateSqlDatabase);
            this.connectionPage.Controls.Add(this.label9);
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
            // forceCreateSqlDatabase
            // 
            this.forceCreateSqlDatabase.Location = new System.Drawing.Point(259, 133);
            this.forceCreateSqlDatabase.Name = "forceCreateSqlDatabase";
            this.forceCreateSqlDatabase.Size = new System.Drawing.Size(209, 20);
            this.forceCreateSqlDatabase.TabIndex = 14;
            this.forceCreateSqlDatabase.Text = "Neuanlage erzwingen";
            this.forceCreateSqlDatabase.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Datenbank:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSqlDatabase
            // 
            this.textBoxSqlDatabase.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.textBoxSqlDatabase.Location = new System.Drawing.Point(76, 133);
            this.textBoxSqlDatabase.MaxLength = 32;
            this.textBoxSqlDatabase.Name = "textBoxSqlDatabase";
            this.textBoxSqlDatabase.Size = new System.Drawing.Size(177, 20);
            this.textBoxSqlDatabase.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(245, 23);
            this.label8.TabIndex = 11;
            this.label8.Text = "Pervasive Verbindung";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSqlTest
            // 
            this.labelSqlTest.Location = new System.Drawing.Point(76, 167);
            this.labelSqlTest.Name = "labelSqlTest";
            this.labelSqlTest.Size = new System.Drawing.Size(392, 23);
            this.labelSqlTest.TabIndex = 10;
            this.labelSqlTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSqlConnectionTest
            // 
            this.btnSqlConnectionTest.Location = new System.Drawing.Point(8, 167);
            this.btnSqlConnectionTest.Name = "btnSqlConnectionTest";
            this.btnSqlConnectionTest.Size = new System.Drawing.Size(62, 23);
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "MySQL Verbindung";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mappingPage
            // 
            this.mappingPage.Controls.Add(this.dataGridmysql);
            this.mappingPage.Controls.Add(this.dataGridPervasiveFields);
            this.mappingPage.Controls.Add(this.pervasiveComboBox);
            this.mappingPage.Controls.Add(this.mysqlComboBox);
            this.mappingPage.Controls.Add(this.label1);
            this.mappingPage.Location = new System.Drawing.Point(4, 22);
            this.mappingPage.Name = "mappingPage";
            this.mappingPage.Padding = new System.Windows.Forms.Padding(3);
            this.mappingPage.Size = new System.Drawing.Size(474, 384);
            this.mappingPage.TabIndex = 1;
            this.mappingPage.Text = "Mapping";
            this.mappingPage.UseVisualStyleBackColor = true;
            // 
            // dataGridmysql
            // 
            this.dataGridmysql.AllowUserToAddRows = false;
            this.dataGridmysql.AllowUserToDeleteRows = false;
            this.dataGridmysql.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridmysql.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn1 });
            this.dataGridmysql.Location = new System.Drawing.Point(201, 127);
            this.dataGridmysql.Name = "dataGridmysql";
            this.dataGridmysql.ReadOnly = true;
            this.dataGridmysql.RowHeadersVisible = false;
            this.dataGridmysql.Size = new System.Drawing.Size(145, 250);
            this.dataGridmysql.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Felder";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridPervasiveFields
            // 
            this.dataGridPervasiveFields.AllowUserToAddRows = false;
            this.dataGridPervasiveFields.AllowUserToDeleteRows = false;
            this.dataGridPervasiveFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPervasiveFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Felder });
            this.dataGridPervasiveFields.Location = new System.Drawing.Point(6, 127);
            this.dataGridPervasiveFields.Name = "dataGridPervasiveFields";
            this.dataGridPervasiveFields.ReadOnly = true;
            this.dataGridPervasiveFields.RowHeadersVisible = false;
            this.dataGridPervasiveFields.Size = new System.Drawing.Size(145, 250);
            this.dataGridPervasiveFields.TabIndex = 4;
            // 
            // Felder
            // 
            this.Felder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Felder.HeaderText = "Felder";
            this.Felder.Name = "Felder";
            this.Felder.ReadOnly = true;
            this.Felder.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Felder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pervasiveComboBox
            // 
            this.pervasiveComboBox.FormattingEnabled = true;
            this.pervasiveComboBox.Location = new System.Drawing.Point(201, 100);
            this.pervasiveComboBox.Name = "pervasiveComboBox";
            this.pervasiveComboBox.Size = new System.Drawing.Size(145, 21);
            this.pervasiveComboBox.TabIndex = 3;
            // 
            // mysqlComboBox
            // 
            this.mysqlComboBox.FormattingEnabled = true;
            this.mysqlComboBox.Location = new System.Drawing.Point(6, 100);
            this.mysqlComboBox.Name = "mysqlComboBox";
            this.mysqlComboBox.Size = new System.Drawing.Size(145, 21);
            this.mysqlComboBox.TabIndex = 2;
            // 
            // importPage
            // 
            this.importPage.Location = new System.Drawing.Point(4, 22);
            this.importPage.Name = "importPage";
            this.importPage.Padding = new System.Windows.Forms.Padding(3);
            this.importPage.Size = new System.Drawing.Size(474, 384);
            this.importPage.TabIndex = 2;
            this.importPage.Text = "Import";
            this.importPage.UseVisualStyleBackColor = true;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.migrationControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppForm";
            this.Text = "Migration Panel";
            this.migrationControl.ResumeLayout(false);
            this.connectionPage.ResumeLayout(false);
            this.connectionPage.PerformLayout();
            this.mappingPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridmysql)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPervasiveFields)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dataGridmysql;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

        private System.Windows.Forms.DataGridViewTextBoxColumn Felder;

        private System.Windows.Forms.DataGridView dataGridPervasiveFields;

        private System.Windows.Forms.ComboBox mysqlComboBox;

        private System.Windows.Forms.ComboBox pervasiveComboBox;

        private System.Windows.Forms.TextBox textBoxSqlDatabase;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox forceCreateSqlDatabase;

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

        private System.Windows.Forms.TabPage importPage;

        private System.Windows.Forms.TabControl migrationControl;
        private System.Windows.Forms.TabPage connectionPage;
        private System.Windows.Forms.TabPage mappingPage;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}