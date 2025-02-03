using AtividadeFinal.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtividadeFinal
{
    public partial class MainForm : Form
    {
        private readonly string connectionString;

        public MainForm(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController(connectionString);
            FormUsers formUsers = new FormUsers(userController);
            formUsers.ShowDialog();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade em desenvolvimento!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
