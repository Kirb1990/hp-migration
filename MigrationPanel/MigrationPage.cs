using System;

namespace MigrationPanel
{
    public partial class AppForm
    {
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
            textBoxMigrationLog.Text += $"{e}\r\n";
        }
    }
}