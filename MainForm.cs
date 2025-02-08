using AtividadeFinal.Controllers;
using AtividadeFinal.Views;
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

        /// <summary>
        /// Abre o form <see cref="FormUsers">
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUser_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController(connectionString);
            FormUsers formUsers = new FormUsers(userController);
            formUsers.ShowDialog();
        }

        /// <summary>
        /// Abre o form <see cref="FormProducts"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController(connectionString);
            FormProducts formProducts = new FormProducts(productController);
            formProducts.ShowDialog();
        }
    }
}
