using AtividadeFinal.Controllers;
using AtividadeFinal.Views;
using System;
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
            UserController userController = new UserController();
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
            ProductController productController = new ProductController();
            FormProducts formProducts = new FormProducts(productController);
            formProducts.ShowDialog();
        }
    }
}
