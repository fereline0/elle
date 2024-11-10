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
        private readonly Dictionary<string, Action> _addActions;
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

            _addActions = new Dictionary<string, Action>
            {
                { "Users", AddUser },
                { "Immovable", AddImmovable },
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

        private bool ConfirmDelete() =>
            MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            ) == DialogResult.Yes;

        private bool DeleteRecord(int id, string tableName) =>
            tableName switch
            {
                "Users" => _userService.DeleteById(id),
                "Immovable" => _immovableService.DeleteById(id),
                _ => throw new ArgumentException("Invalid table name"),
            };

        private void RefreshTable_Click(object sender, EventArgs e) =>
            LoadData(TableSelector.SelectedItem.ToString());

        private void TableSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(TableSelector.SelectedItem.ToString());
            CreateDynamicFields(TableSelector.SelectedItem.ToString());
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

        private void HandleAdd_Click(object sender, EventArgs e)
        {
            string tableName = TableSelector.SelectedItem.ToString();

            if (_addActions.TryGetValue(tableName, out var addAction))
            {
                addAction();
            }
        }

        private void CreateDynamicFields(string tableName)
        {
            DynamicFieldsPanel.Controls.Clear();

            var fields = tableName switch
            {
                "Users" => new List<string> { "Login", "Password", "Role" },
                "Immovable" => new List<string> { "Name", "Address", "Price" },
                _ => throw new ArgumentException("Invalid table name"),
            };

            CreateFields(fields);
        }

        private void CreateFields(IEnumerable<string> fields)
        {
            int yOffset = 0;

            foreach (var field in fields)
            {
                var label = new Label
                {
                    Text = field,
                    Location = new Point(0, yOffset),
                    AutoSize = true,
                };
                DynamicFieldsPanel.Controls.Add(label);

                var textBox = new TextBox
                {
                    Name = field + "TextBox",
                    Location = new Point(0, yOffset + 20),
                    Width = 150,
                };
                DynamicFieldsPanel.Controls.Add(textBox);

                yOffset += 50;
            }
        }

        private void AddUser()
        {
            var newUser = new User
            {
                Login = GetTextBoxValue("LoginTextBox"),
                Password = GetTextBoxValue("PasswordTextBox"),
                Role = (User.RoleType)
                    Enum.Parse(typeof(User.RoleType), GetTextBoxValue("RoleTextBox")),
            };

            _userService.Add(newUser);
            LoadData(TableSelector.SelectedItem.ToString());
        }

        private void AddImmovable()
        {
            var newImmovable = new Immovable
            {
                Name = GetTextBoxValue("NameTextBox"),
                Address = GetTextBoxValue("AddressTextBox"),
                Price = decimal.Parse(GetTextBoxValue("PriceTextBox")),
            };

            _immovableService.Add(newImmovable);
            LoadData(TableSelector.SelectedItem.ToString());
        }

        private string GetTextBoxValue(string textBoxName)
        {
            var textBox = DynamicFieldsPanel.Controls[textBoxName] as TextBox;
            return textBox?.Text ?? string.Empty; // Возвращаем пустую строку, если текстовое поле не найдено
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
