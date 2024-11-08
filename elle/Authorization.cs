using System.Windows.Forms;
using elle.Models;
using elle.Services;
using Microsoft.VisualBasic.Logging;

namespace elle
{
    public partial class Authorization : Form
    {
        private readonly UserService _userService;
        private readonly ImmovableService _immovableService;
        private readonly Context _context;

        public Authorization(
            UserService userService,
            ImmovableService immovableService,
            Context context
        )
        {
            InitializeComponent();
            _userService = userService;
            _immovableService = immovableService;
            _context = context;
        }

        private void HandleAuthorize_Click(object sender, EventArgs e)
        {
            User user = _userService.FindUserByLogin(Login.Text);

            if (user != null && user.Password == Password.Text)
            {
                AdminPanel adminPanel = new AdminPanel(_userService, _immovableService);

                adminPanel.ShowDialog();
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _context.Dispose();
            base.OnFormClosing(e);
        }
    }
}
