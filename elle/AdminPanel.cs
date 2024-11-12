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
        private readonly HomeService _homeService;
        private readonly CityService _cityService;
        private DynamicFieldGenerator _fieldGenerator;

        private readonly List<string> _tables = new List<string>
        {
            "Users",
            "Immovable",
            "Home",
            "City",
        };
        private readonly Dictionary<string, Action<int>> _updateActions;
        private readonly Dictionary<string, Action> _addActions;
        private bool _isUpdating = false;

        public AdminPanel(
            UserService userService,
            ImmovableService immovableService,
            HomeService homeService,
            CityService cityService
        )
        {
            InitializeComponent();
            _userService = userService;
            _immovableService = immovableService;
            _homeService = homeService;
            _cityService = cityService;

            _fieldGenerator = new DynamicFieldGenerator(DynamicFieldsPanel);

            TableSelector.DataSource = _tables;

            _updateActions = new Dictionary<string, Action<int>>
            {
                { "Users", UpdateUser },
                { "Immovable", UpdateImmovable },
                { "Home", UpdateHome },
                { "City", UpdateCity },
            };

            _addActions = new Dictionary<string, Action>
            {
                { "Users", AddUser },
                { "Immovable", AddImmovable },
                { "Home", AddHome },
                { "City", AddCity },
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
                    "Home" => _homeService.GetAll(),
                    "City" => _cityService.GetAll(),
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
                "Home" => _homeService.DeleteById(id),
                "City" => _cityService.DeleteById(id),
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
            var fields = tableName switch
            {
                "Users" => new Dictionary<string, FieldType>
                {
                    { "Login", FieldType.TextBox },
                    { "Password", FieldType.TextBox },
                    { "Role", FieldType.TextBox },
                },
                "Immovable" => new Dictionary<string, FieldType>
                {
                    { "Name", FieldType.TextBox },
                    { "Address", FieldType.TextBox },
                    { "Price", FieldType.TextBox },
                    { "RentEndDate", FieldType.DateTimePicker },
                    { "HomeId", FieldType.TextBox },
                },
                "Home" => new Dictionary<string, FieldType>
                {
                    { "Name", FieldType.TextBox },
                    { "CityId", FieldType.TextBox },
                },
                "City" => new Dictionary<string, FieldType> { { "Name", FieldType.TextBox } },
                _ => throw new ArgumentException("Invalid table name"),
            };

            _fieldGenerator.CreateFields(fields);
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
                HomeId = int.Parse(GetTextBoxValue("HomeIdTextBox")),
                RentEndDate = GetDateTimePickerValue("RentEndDateDateTimePicker"),
            };

            _immovableService.Add(newImmovable);
            LoadData(TableSelector.SelectedItem.ToString());
        }

        private DateTime? GetDateTimePickerValue(string dateTimePickerName)
        {
            var dateTimePicker = DynamicFieldsPanel.Controls[dateTimePickerName] as DateTimePicker;
            return dateTimePicker?.Value.ToUniversalTime();
        }

        private void AddHome()
        {
            var newHome = new Home
            {
                Name = GetTextBoxValue("NameTextBox"),
                CityId = int.Parse(GetTextBoxValue("CityIdTextBox")),
            };

            _homeService.Add(newHome);
            LoadData(TableSelector.SelectedItem.ToString());
        }

        private void AddCity()
        {
            var newCity = new City { Name = GetTextBoxValue("NameTextBox") };

            _cityService.Add(newCity);
            LoadData(TableSelector.SelectedItem.ToString());
        }

        private string GetTextBoxValue(string textBoxName)
        {
            var textBox = DynamicFieldsPanel.Controls[textBoxName] as TextBox;
            return textBox?.Text ?? string.Empty;
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

            DateTime? rentEndDate = (DateTime?)
                TableViewer.Rows[rowIndex].Cells["RentEndDate"].Value;

            if (rentEndDate.HasValue)
            {
                if (rentEndDate.Value.Kind == DateTimeKind.Unspecified)
                {
                    rentEndDate = DateTime.SpecifyKind(rentEndDate.Value, DateTimeKind.Utc);
                }
                else
                {
                    rentEndDate = rentEndDate.Value.ToUniversalTime();
                }
            }

            var immovable = new Immovable
            {
                Id = immovableId,
                Name = TableViewer.Rows[rowIndex].Cells["Name"].Value.ToString(),
                Address = TableViewer.Rows[rowIndex].Cells["Address"].Value.ToString(),
                Price = decimal.Parse(TableViewer.Rows[rowIndex].Cells["Price"].Value.ToString()),
                RentEndDate = rentEndDate,
                HomeId = int.Parse(TableViewer.Rows[rowIndex].Cells["HomeId"].Value.ToString()),
            };

            _immovableService.Update(immovable);
        }

        private void UpdateHome(int rowIndex)
        {
            var homeId = (int)TableViewer.Rows[rowIndex].Cells["Id"].Value;
            var home = new Home
            {
                Id = homeId,
                Name = TableViewer.Rows[rowIndex].Cells["Name"].Value.ToString(),
                CityId = int.Parse(TableViewer.Rows[rowIndex].Cells["CityId"].Value.ToString()),
            };

            _homeService.Update(home);
        }

        private void UpdateCity(int rowIndex)
        {
            var cityId = (int)TableViewer.Rows[rowIndex].Cells["Id"].Value;
            var city = new City
            {
                Id = cityId,
                Name = TableViewer.Rows[rowIndex].Cells["Name"].Value.ToString(),
            };

            _cityService.Update(city);
        }
    }
}
