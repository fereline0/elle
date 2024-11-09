using System;
using System.Collections.Generic;
using System.Windows.Forms;
using elle.Models;
using elle.Services;

namespace elle
{
    public partial class AdminPanel : Form
    {
        private readonly UserService _userService;
        private readonly ImmovableService _immovableService;

        private readonly List<string> _tables = new List<string> { "Users", "Immovable" };
        private readonly Dictionary<string, Action<int>> _updateActions;
        private bool _isUpdating = false;

        public AdminPanel(UserService userService, ImmovableService immovableService)
        {
            InitializeComponent();
            _userService = userService;
            _immovableService = immovableService;

            TableSelector.DataSource = _tables;

            _updateActions = new Dictionary<string, Action<int>>
            {
                { "Users", UpdateUser },
                { "Immovable", UpdateImmovable },
            };

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

            if (ConfirmDelete())
            {
                bool deleted = DeleteRecord(selectedId, TableSelector.SelectedItem.ToString());
                MessageBox.Show(deleted ? "Record deleted successfully." : "Record not found.");

                if (deleted)
                {
                    LoadData(TableSelector.SelectedItem.ToString());
                }
            }
        }

        private bool ConfirmDelete()
        {
            return MessageBox.Show(
                    "Are you sure you want to delete this record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                ) == DialogResult.Yes;
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

        private void TableViewer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            string tableName = TableSelector.SelectedItem.ToString();
            if (_updateActions.TryGetValue(tableName, out var updateAction))
            {
                updateAction(e.RowIndex);
            }

            _isUpdating = false;
        }

        private void UpdateUser(int rowIndex)
        {
            var userId = (int)TableViewer.Rows[rowIndex].Cells["Id"].Value;
            var user = new User
            {
                Id = userId,
                Login = TableViewer.Rows[rowIndex].Cells["Login"].Value.ToString(),
                Password = TableViewer.Rows[rowIndex].Cells["Password"].Value.ToString(),
                Role = (User.RoleType)
                    Enum.Parse(
                        typeof(User.RoleType),
                        TableViewer.Rows[rowIndex].Cells["Role"].Value.ToString()
                    ),
            };

            _userService.Update(user);
        }

        private void UpdateImmovable(int rowIndex)
        {
            var immovableId = (int)TableViewer.Rows[rowIndex].Cells["Id"].Value;
            var immovable = new Immovable
            {
                Id = immovableId,
                Name = TableViewer.Rows[rowIndex].Cells["Name"].Value.ToString(),
                Address = TableViewer.Rows[rowIndex].Cells["Address"].Value.ToString(),
                Price = decimal.Parse(TableViewer.Rows[rowIndex].Cells["Price"].Value.ToString()),
            };

            _immovableService.Update(immovable);
        }
    }
}
