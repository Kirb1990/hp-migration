using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MigrationTool;
using Newtonsoft.Json;

namespace MigrationPanel
{
    internal partial class AppForm
    {
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

        void LoadTableFieldsToGrid(DataGridView dataGridView, List<string> fields)
        {
            dataGridView.Rows.Clear();
            for (int i = 0; i < fields.Count; i++)
            {
                dataGridView.Rows.Add(i+1, fields[i]);
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
            string tableName = comboBoxPervasive.SelectedItem.ToString().Trim();
            
            List<string> fields;
            if (_Migrator.Mapping.TryGet(tableName, out TablePair tablePair))
            {
                ShowMappingExistsAlert();
                SwitchComboBox(comboBoxSql, tablePair.SqlTable.Name);
                
                LoadTableFieldsToGrid(dataGridSql, tablePair.SqlTable.Fields);
                fields = tablePair.PervasiveTable.Fields;
            }
            else
            {
                HideMappingExistsAlert();
                fields = _Migrator.GetPervasiveFields(tableName);
                if(fields.Count <= 1) fields.Clear();
            }
            LoadTableFieldsToGrid(dataGridPervasive, fields);
        }

        void ShowMappingExistsAlert()
        {
            labelMappingExists.Visible = true;
        }
        
        void HideMappingExistsAlert()
        {
            labelMappingExists.Visible = false;
        }
        
        void OnComboBoxSqlChanged(object sender, EventArgs e)
        {
            string tableName = comboBoxSql.SelectedItem.ToString();
                
            List<string> fields;
            if (_Migrator.Mapping.TryGet(tableName, out TablePair tablePair))
            {
                ShowMappingExistsAlert();
                SwitchComboBox(comboBoxPervasive, tablePair.PervasiveTable.Name);
                LoadTableFieldsToGrid(dataGridPervasive, tablePair.PervasiveTable.Fields);
                fields = tablePair.SqlTable.Fields;
            }
            else
            {
                HideMappingExistsAlert();
                fields = _Migrator.GetSqlFields(tableName);
                if(fields.Count <= 1) fields.Clear();
            }
            LoadTableFieldsToGrid(dataGridSql, fields);
        }
        
        void OnDataGridScroll(object sender, ScrollEventArgs e)
        {
            int pervasiveRowIndex = dataGridPervasive.FirstDisplayedScrollingRowIndex;

            if (dataGridSql.FirstDisplayedScrollingRowIndex < 0)
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

            TablePair tablePair = new TablePair
            {
                PervasiveTable = pervasiveTable,
                SqlTable = sqlTable
            };

            try
            {
                Clipboard.SetText(JsonConvert.SerializeObject(tablePair, Formatting.Indented));
                _Migrator.AddTablePairToMapping(tablePair);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }

            MessageBox.Show("In Zwischenablage gespeichert und zur mapping.json hinzugefügt!");
        }
        
        List<string> ExtractFields(DataGridView dataGridView)
        {
            List<string> fields = new List<string>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                fields.Add((string)dataGridView.Rows[i].Cells[1].Value);
            }

            return fields;
        }

        void btnPervasiveRowUp_Click(object sender, EventArgs e)
        {
            const int OFFSET = -1;
            if (!TryGetRow(out DataGridViewRow dataGridViewRow, out int index, OFFSET))
            {
                return;
            }
            SwapFieldValues(dataGridViewRow, index);
        }

        void btnPervasiveRowDown_Click(object sender, EventArgs e)
        {
            const int OFFSET = 1;
            if (!TryGetRow(out DataGridViewRow dataGridViewRow, out int index, OFFSET))
            {
                return;
            }
            SwapFieldValues(dataGridViewRow, index);
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
            index += offSet;

            return index >= 0 && index < dataGridPervasive.Rows.Count;
        }

        void SwapFieldValues(DataGridViewRow dataGridRow, int index)
        {
            DataGridViewCell dataGridViewCell = dataGridPervasive.Rows[index].Cells["Feldname"];

            // "Tuple Assignment" swapping the values between the two cells
            (dataGridViewCell.Value, dataGridRow.Cells["Feldname"].Value) = (dataGridRow.Cells["Feldname"].Value, dataGridViewCell.Value);

            SelectPervasiveDataRow(index);
        }
    }
}