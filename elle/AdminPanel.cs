using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using elle.Services;

namespace elle
{
    public partial class AdminPanel : Form
    {
        private readonly UserService _userService;
        private readonly ImmovableService _immovableService;

        private readonly List<string> _tables = new List<string> { "Users", "Immovable" };

        public AdminPanel(UserService userService, ImmovableService immovableService)
        {
            InitializeComponent();
            _userService = userService;
            _immovableService = immovableService;

            TableSelector.DataSource = _tables;

            LoadData(TableSelector.SelectedItem.ToString());
        }

        private void LoadData(string tableName)
        {
            try
            {
                TableViewer.DataSource = tableName switch
                {
                    "Users" => _userService.GetAll(),
                    "Immovable" => _immovableService.GetAll(),
                    _ => throw new ArgumentException("Invalid table name"),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void HandleDelete_Click(object sender, EventArgs e)
        {
            if (TableViewer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            int selectedId = (int)TableViewer.SelectedRows[0].Cells["id"].Value;

            var result = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                bool deleted = DeleteRecord(selectedId, TableSelector.SelectedItem.ToString());

                MessageBox.Show(deleted ? "Record deleted successfully." : "Record not found.");
                if (deleted)
                {
                    LoadData(TableSelector.SelectedItem.ToString());
                }
            }
        }

        private bool DeleteRecord(int id, string tableName)
        {
            return tableName switch
            {
                "Users" => _userService.DeleteById(id),
                "Immovable" => _immovableService.DeleteById(id),
                _ => throw new ArgumentException("Invalid table name"),
            };
        }

        private void RefreshTable_Click(object sender, EventArgs e)
        {
            LoadData(TableSelector.SelectedItem.ToString());
        }

        private void TableSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(TableSelector.SelectedItem.ToString());
        }
    }
}
