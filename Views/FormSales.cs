using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Windows.Forms;

namespace AtividadeFinal.Views
{
    public partial class FormSales : Form
    {
        private readonly SalesController controller;
        private readonly Sales mySale;
        private readonly bool isEditMode = false;

        public event EventHandler DataChanged;

        public FormSales(Sales sale = null)
        {
            InitializeComponent();
            InitializeComboBox();
            controller = new SalesController();

            if(sale != null)
            {
                isEditMode = true;
                mySale = sale;

                comboBoxUsers.SelectedValue = sale.User;
                comboBoxProducts.SelectedValue = sale.Product;

                numericUpDownQuantity.Text = sale.Quantity.ToString();
                
                monthCalendarSaleDate.SelectionStart = sale.SaleDate;
                monthCalendarSaleDate.SelectionEnd = sale.SaleDate;

                comboBoxUsers.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComboBox()
        {
            comboBoxUsers.DataSource = new UserController().GetAllRegistry();
            comboBoxUsers.DisplayMember = "Name";
            comboBoxUsers.ValueMember = "Id";
            
            comboBoxProducts.DataSource = new ProductController().GetAllRegistry();
            comboBoxProducts.DisplayMember = "Name";
            comboBoxProducts.ValueMember = "Id";
        }
    }
}
