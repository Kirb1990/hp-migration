using System;
using System.Windows.Forms;
using MigrationTool;

namespace MigrationPanel
{
    internal partial class AppForm
    {
        void OnImportPageEnter(object sender, EventArgs e)
        {
            _Migrator.OnConverterMessage += AppendImportLogText;
            _Migrator.OnErrorOccured += AppendImportLogText;
            
            if (_Migrator.Mapping.TablePairs.Count <= comboBoxMapping.Items.Count)
            {
                return;
            }
            
            comboBoxMapping.Items.Clear();
            foreach (TablePair tablePair in _Migrator.Mapping.TablePairs)
            {
                comboBoxMapping.Items.Add(tablePair.PervasiveTable.Name);
            }
        }

        void OnImportPageLeave(object sender, EventArgs e)
        {
            _Migrator.OnConverterMessage -= AppendImportLogText;
            _Migrator.OnErrorOccured -= AppendImportLogText;
        }

        void AppendImportLogText(object sender, string message)
        {
            textBoxImportLog.Text += message + Environment.NewLine;
        }

        void btnImportStart_Click(object sender, EventArgs e)
        {
            string convertingTable = comboBoxMapping.SelectedItem.ToString().Trim();
            if (string.IsNullOrEmpty(convertingTable))
            {
                _Migrator.StartConvert();
                return;
            }

            if (!_Migrator.Mapping.TryGet(convertingTable, out TablePair tablePair))
            {
                MessageBox.Show($"{convertingTable} konnte nicht im Mapping gefunden werden");
                return;
            }
            
            _Migrator.StartConvert(tablePair);
        }
    }
}