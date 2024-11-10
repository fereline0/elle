using System.Windows.Forms;
using elle.Models;
using elle.Services;
using Microsoft.Extensions.DependencyInjection;

namespace elle
{
    public partial class Authorization : Form
    {
        private readonly UserService _userService;
        private readonly Context _context;
        private readonly IServiceProvider _serviceProvider;

        public Authorization(
            UserService userService,
            Context context,
            IServiceProvider serviceProvider
        )
        {
            InitializeComponent();
            _userService = userService;
            _context = context;
            _serviceProvider = serviceProvider;
        }

        private void HandleAuthorize_Click(object sender, EventArgs e)
        {
            User user = _userService.FindUserByLogin(Login.Text);

            if (user != null && user.Password == Password.Text)
            {
                if (user.Role == User.RoleType.Admin)
                {
                    AdminPanel adminPanel = _serviceProvider.GetRequiredService<AdminPanel>();
                    adminPanel.ShowDialog();
                }
                else
                {
                    ImmovableViewer immovableViewer =
                        _serviceProvider.GetRequiredService<ImmovableViewer>();
                    immovableViewer.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(
                    "Invalid login or password",
                    "Critical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
