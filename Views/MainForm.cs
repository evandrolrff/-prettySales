using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using AtividadeFinal.Views;
using System;
using System.Windows.Forms;

namespace AtividadeFinal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Abre o form <see cref="FormUsers">
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUser_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects("users");
            formListObjects.ShowDialog();
            //UserController userController = new UserController();
            //FormUsers formUsers = new FormUsers(userController);
            //formUsers.ShowDialog();
        }

        /// <summary>
        /// Abre o form <see cref="FormProducts"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProducts_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects("products");
            formListObjects.ShowDialog();

            //ProductController productController = new ProductController();
            //FormProducts formProducts = new FormProducts(productController);
            //formProducts.ShowDialog();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects("sales");
            formListObjects.ShowDialog();

            //SalesController salesController = new SalesController();
            //FormSales formSales = new FormSales();
            //formSales.ShowDialog();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects("payments");
            formListObjects.ShowDialog();

            //PaymentsController paymentsController = new PaymentsController();
            //FormPayments formPayments = new FormPayments();
            //formPayments.ShowDialog();
        }
    }
}
