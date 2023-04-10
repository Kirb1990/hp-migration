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
            this.migrationControl = new System.Windows.Forms.TabControl();
            this.connectionPage = new System.Windows.Forms.TabPage();
            this.labelPervasvieTest = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxPervasiveDatabase = new System.Windows.Forms.TextBox();
            this.textBoxPervasivePassword = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxPervasiveUser = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxPervasivePort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPervasiveConnectionTest = new System.Windows.Forms.Button();
            this.textBoxPervasiveServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.migrationPage = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.btnStartMigrate = new System.Windows.Forms.Button();
            this.textBoxMigrationLog = new System.Windows.Forms.TextBox();
            this.mappingPage = new System.Windows.Forms.TabPage();
            this.btnCopyMapping = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSql = new System.Windows.Forms.ComboBox();
            this.dataGridSql = new System.Windows.Forms.DataGridView();
            this.comboBoxPervasive = new System.Windows.Forms.ComboBox();
            this.dataGridPervasive = new System.Windows.Forms.DataGridView();
            this.Feldname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importPage = new System.Windows.Forms.TabPage();
            this.migrationControl.SuspendLayout();
            this.connectionPage.SuspendLayout();
            this.migrationPage.SuspendLayout();
            this.mappingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridSql)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridPervasive)).BeginInit();
            this.SuspendLayout();
            // 
            // migrationControl
            // 
            this.migrationControl.Controls.Add(this.connectionPage);
            this.migrationControl.Controls.Add(this.migrationPage);
            this.migrationControl.Controls.Add(this.mappingPage);
            this.migrationControl.Controls.Add(this.importPage);
            this.migrationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.migrationControl.Location = new System.Drawing.Point(0, 0);
            this.migrationControl.Name = "migrationControl";
            this.migrationControl.SelectedIndex = 0;
            this.migrationControl.Size = new System.Drawing.Size(484, 446);
            this.migrationControl.TabIndex = 5;
            // 
            // connectionPage
            // 
            this.connectionPage.Controls.Add(this.labelPervasvieTest);
            this.connectionPage.Controls.Add(this.label13);
            this.connectionPage.Controls.Add(this.textBoxPervasiveDatabase);
            this.connectionPage.Controls.Add(this.textBoxPervasivePassword);
            this.connectionPage.Controls.Add(this.label12);
            this.connectionPage.Controls.Add(this.textBoxPervasiveUser);
            this.connectionPage.Controls.Add(this.label11);
            this.connectionPage.Controls.Add(this.textBoxPervasivePort);
            this.connectionPage.Controls.Add(this.label10);
            this.connectionPage.Controls.Add(this.btnPervasiveConnectionTest);
            this.connectionPage.Controls.Add(this.textBoxPervasiveServer);
            this.connectionPage.Controls.Add(this.label2);
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
            this.connectionPage.Size = new System.Drawing.Size(476, 420);
            this.connectionPage.TabIndex = 0;
            this.connectionPage.Text = "Verbindung";
            this.connectionPage.UseVisualStyleBackColor = true;
            // 
            // labelPervasvieTest
            // 
            this.labelPervasvieTest.Location = new System.Drawing.Point(76, 383);
            this.labelPervasvieTest.Name = "labelPervasvieTest";
            this.labelPervasvieTest.Size = new System.Drawing.Size(392, 23);
            this.labelPervasvieTest.TabIndex = 26;
            this.labelPervasvieTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 348);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 20);
            this.label13.TabIndex = 25;
            this.label13.Text = "Datenbank:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPervasiveDatabase
            // 
            this.textBoxPervasiveDatabase.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPervasiveDatabase.Location = new System.Drawing.Point(76, 348);
            this.textBoxPervasiveDatabase.MaxLength = 32;
            this.textBoxPervasiveDatabase.Name = "textBoxPervasiveDatabase";
            this.textBoxPervasiveDatabase.Size = new System.Drawing.Size(177, 20);
            this.textBoxPervasiveDatabase.TabIndex = 24;
            // 
            // textBoxPervasivePassword
            // 
            this.textBoxPervasivePassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPervasivePassword.Location = new System.Drawing.Point(76, 322);
            this.textBoxPervasivePassword.MaxLength = 32;
            this.textBoxPervasivePassword.Name = "textBoxPervasivePassword";
            this.textBoxPervasivePassword.PasswordChar = '*';
            this.textBoxPervasivePassword.Size = new System.Drawing.Size(177, 20);
            this.textBoxPervasivePassword.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 322);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 20);
            this.label12.TabIndex = 22;
            this.label12.Text = "Passwort:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPervasiveUser
            // 
            this.textBoxPervasiveUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPervasiveUser.Location = new System.Drawing.Point(76, 296);
            this.textBoxPervasiveUser.MaxLength = 32;
            this.textBoxPervasiveUser.Name = "textBoxPervasiveUser";
            this.textBoxPervasiveUser.Size = new System.Drawing.Size(177, 20);
            this.textBoxPervasiveUser.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 296);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 20);
            this.label11.TabIndex = 20;
            this.label11.Text = "User:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPervasivePort
            // 
            this.textBoxPervasivePort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPervasivePort.Location = new System.Drawing.Point(76, 270);
            this.textBoxPervasivePort.MaxLength = 5;
            this.textBoxPervasivePort.Name = "textBoxPervasivePort";
            this.textBoxPervasivePort.Size = new System.Drawing.Size(48, 20);
            this.textBoxPervasivePort.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 270);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Port:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPervasiveConnectionTest
            // 
            this.btnPervasiveConnectionTest.Location = new System.Drawing.Point(6, 383);
            this.btnPervasiveConnectionTest.Name = "btnPervasiveConnectionTest";
            this.btnPervasiveConnectionTest.Size = new System.Drawing.Size(65, 25);
            this.btnPervasiveConnectionTest.TabIndex = 17;
            this.btnPervasiveConnectionTest.Text = "Testen";
            this.btnPervasiveConnectionTest.UseVisualStyleBackColor = true;
            this.btnPervasiveConnectionTest.Click += new System.EventHandler(this.btnPervasiveConnectionTest_Click);
            // 
            // textBoxPervasiveServer
            // 
            this.textBoxPervasiveServer.Location = new System.Drawing.Point(76, 244);
            this.textBoxPervasiveServer.Name = "textBoxPervasiveServer";
            this.textBoxPervasiveServer.Size = new System.Drawing.Size(177, 20);
            this.textBoxPervasiveServer.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Server:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.label9.Location = new System.Drawing.Point(6, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Datenbank:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSqlDatabase
            // 
            this.textBoxSqlDatabase.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxSqlDatabase.Location = new System.Drawing.Point(76, 133);
            this.textBoxSqlDatabase.MaxLength = 32;
            this.textBoxSqlDatabase.Name = "textBoxSqlDatabase";
            this.textBoxSqlDatabase.Size = new System.Drawing.Size(177, 20);
            this.textBoxSqlDatabase.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label8.Location = new System.Drawing.Point(8, 220);
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
            this.btnSqlConnectionTest.Size = new System.Drawing.Size(65, 25);
            this.btnSqlConnectionTest.TabIndex = 9;
            this.btnSqlConnectionTest.Text = "Testen";
            this.btnSqlConnectionTest.UseVisualStyleBackColor = true;
            this.btnSqlConnectionTest.Click += new System.EventHandler(this.btnSqlConnectionTest_Click);
            // 
            // textBoxSqlPassword
            // 
            this.textBoxSqlPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
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
            this.textBoxSqlUser.Cursor = System.Windows.Forms.Cursors.IBeam;
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
            this.textBoxSqlPort.Cursor = System.Windows.Forms.Cursors.IBeam;
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
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "MySQL Verbindung";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // migrationPage
            // 
            this.migrationPage.Controls.Add(this.label14);
            this.migrationPage.Controls.Add(this.btnStartMigrate);
            this.migrationPage.Controls.Add(this.textBoxMigrationLog);
            this.migrationPage.Location = new System.Drawing.Point(4, 22);
            this.migrationPage.Name = "migrationPage";
            this.migrationPage.Padding = new System.Windows.Forms.Padding(3);
            this.migrationPage.Size = new System.Drawing.Size(476, 420);
            this.migrationPage.TabIndex = 1;
            this.migrationPage.Text = "Migration";
            this.migrationPage.UseVisualStyleBackColor = true;
            this.migrationPage.Enter += new System.EventHandler(this.MigrationPage_Enter);
            this.migrationPage.Leave += new System.EventHandler(this.MigrationPage_Leave);
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(3, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(470, 70);
            this.label14.TabIndex = 2;
            this.label14.Text = "Hier wir ddie Migratiuon durchgeführt...";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartMigrate
            // 
            this.btnStartMigrate.Location = new System.Drawing.Point(177, 76);
            this.btnStartMigrate.Name = "btnStartMigrate";
            this.btnStartMigrate.Size = new System.Drawing.Size(100, 25);
            this.btnStartMigrate.TabIndex = 1;
            this.btnStartMigrate.Text = "Start Migration";
            this.btnStartMigrate.UseVisualStyleBackColor = true;
            this.btnStartMigrate.Click += new System.EventHandler(this.btnStartMigrate_Click);
            // 
            // textBoxMigrationLog
            // 
            this.textBoxMigrationLog.Location = new System.Drawing.Point(0, 105);
            this.textBoxMigrationLog.Multiline = true;
            this.textBoxMigrationLog.Name = "textBoxMigrationLog";
            this.textBoxMigrationLog.Size = new System.Drawing.Size(476, 315);
            this.textBoxMigrationLog.TabIndex = 0;
            // 
            // mappingPage
            // 
            this.mappingPage.Controls.Add(this.btnCopyMapping);
            this.mappingPage.Controls.Add(this.label1);
            this.mappingPage.Controls.Add(this.comboBoxSql);
            this.mappingPage.Controls.Add(this.dataGridSql);
            this.mappingPage.Controls.Add(this.comboBoxPervasive);
            this.mappingPage.Controls.Add(this.dataGridPervasive);
            this.mappingPage.Location = new System.Drawing.Point(4, 22);
            this.mappingPage.Name = "mappingPage";
            this.mappingPage.Padding = new System.Windows.Forms.Padding(3);
            this.mappingPage.Size = new System.Drawing.Size(476, 420);
            this.mappingPage.TabIndex = 3;
            this.mappingPage.Text = "Zuweisung";
            this.mappingPage.UseVisualStyleBackColor = true;
            this.mappingPage.Enter += new System.EventHandler(this.MappingPage_Enter);
            // 
            // btnCopyMapping
            // 
            this.btnCopyMapping.Location = new System.Drawing.Point(368, 62);
            this.btnCopyMapping.Name = "btnCopyMapping";
            this.btnCopyMapping.Size = new System.Drawing.Size(100, 23);
            this.btnCopyMapping.TabIndex = 5;
            this.btnCopyMapping.Text = "Zwischenablage";
            this.btnCopyMapping.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 45);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hier wird die beiden Datenbanken  zugewiesen";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxSql
            // 
            this.comboBoxSql.FormattingEnabled = true;
            this.comboBoxSql.Location = new System.Drawing.Point(251, 103);
            this.comboBoxSql.Name = "comboBoxSql";
            this.comboBoxSql.Size = new System.Drawing.Size(225, 21);
            this.comboBoxSql.TabIndex = 3;
            // 
            // dataGridSql
            // 
            this.dataGridSql.AllowUserToAddRows = false;
            this.dataGridSql.AllowUserToDeleteRows = false;
            this.dataGridSql.AllowUserToResizeColumns = false;
            this.dataGridSql.AllowUserToResizeRows = false;
            this.dataGridSql.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSql.Location = new System.Drawing.Point(251, 130);
            this.dataGridSql.Name = "dataGridSql";
            this.dataGridSql.Size = new System.Drawing.Size(225, 290);
            this.dataGridSql.TabIndex = 2;
            // 
            // comboBoxPervasive
            // 
            this.comboBoxPervasive.FormattingEnabled = true;
            this.comboBoxPervasive.Location = new System.Drawing.Point(0, 103);
            this.comboBoxPervasive.Name = "comboBoxPervasive";
            this.comboBoxPervasive.Size = new System.Drawing.Size(225, 21);
            this.comboBoxPervasive.TabIndex = 1;
            this.comboBoxPervasive.SelectedIndexChanged += new System.EventHandler(this.OnPervasiveComboBoxChanged);
            // 
            // dataGridPervasive
            // 
            this.dataGridPervasive.AllowUserToAddRows = false;
            this.dataGridPervasive.AllowUserToDeleteRows = false;
            this.dataGridPervasive.AllowUserToResizeColumns = false;
            this.dataGridPervasive.AllowUserToResizeRows = false;
            this.dataGridPervasive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPervasive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.Feldname});
            this.dataGridPervasive.Location = new System.Drawing.Point(0, 130);
            this.dataGridPervasive.Name = "dataGridPervasive";
            this.dataGridPervasive.ReadOnly = true;
            this.dataGridPervasive.RowHeadersVisible = false;
            this.dataGridPervasive.Size = new System.Drawing.Size(225, 290);
            this.dataGridPervasive.TabIndex = 0;
            // 
            // Feldname
            // 
            this.Feldname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Feldname.HeaderText = "Feldname";
            this.Feldname.Name = "Feldname";
            this.Feldname.ReadOnly = true;
            this.Feldname.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Feldname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // importPage
            // 
            this.importPage.Location = new System.Drawing.Point(4, 22);
            this.importPage.Name = "importPage";
            this.importPage.Padding = new System.Windows.Forms.Padding(3);
            this.importPage.Size = new System.Drawing.Size(476, 420);
            this.importPage.TabIndex = 4;
            this.importPage.Text = "Import";
            this.importPage.UseVisualStyleBackColor = true;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(484, 446);
            this.Controls.Add(this.migrationControl);
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppForm";
            this.migrationControl.ResumeLayout(false);
            this.connectionPage.ResumeLayout(false);
            this.connectionPage.PerformLayout();
            this.migrationPage.ResumeLayout(false);
            this.migrationPage.PerformLayout();
            this.mappingPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridSql)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridPervasive)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridViewTextBoxColumn Feldname;

        private System.Windows.Forms.Button btnCopyMapping;

        private System.Windows.Forms.ComboBox comboBoxPervasive;
        private System.Windows.Forms.TabPage mappingPage;
        private System.Windows.Forms.DataGridView dataGridSql;
        private System.Windows.Forms.ComboBox comboBoxSql;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.DataGridView dataGridPervasive;

        private System.Windows.Forms.Label label14;

        private System.Windows.Forms.TextBox textBoxMigrationLog;
        private System.Windows.Forms.Button btnStartMigrate;

        private System.Windows.Forms.TabPage tabPage1;

        private System.Windows.Forms.Label labelPervasvieTest;

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxPervasiveDatabase;

        private System.Windows.Forms.TextBox textBoxPervasivePort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxPervasiveUser;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxPervasivePassword;
        private System.Windows.Forms.Label label12;

        private System.Windows.Forms.Button btnPervasiveConnectionTest;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPervasiveServer;

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
        private System.Windows.Forms.TabPage migrationPage;

        #endregion
    }
}