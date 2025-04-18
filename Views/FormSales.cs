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
            InitilizeDateTimePicker();
            controller = new SalesController();

            if(sale != null)
            {
                isEditMode = true;
                mySale = sale;

                comboBoxUsers.SelectedValue = sale.User.Id;
                comboBoxProducts.SelectedValue = sale.Product.Id;

                numericUpDownQuantity.Text = sale.Quantity.ToString();

                dateTimePickerSales.Value = sale.SaleDate;

                comboBoxUsers.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            EnablesAndDisablesButtons();
        }

        #region EventsButtons
        private void btnReturn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja retornar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Sales sale = createSaleFromFields();
            bool saleIsAdded = controller.AddObject(sale);

            if (!saleIsAdded)
            {
                MessageBox.Show("Houve um erro durante a inserção de uma venda!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja editar este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                mySale.User = (User)comboBoxUsers.SelectedItem;
                mySale.Product = (Product)comboBoxProducts.SelectedItem;
                mySale.SaleDate = dateTimePickerSales.Value;
                mySale.Quantity= Int32.Parse(numericUpDownQuantity.Text);

                bool isEdited = controller.UpdateObject(mySale);
                if (!isEdited)
                {
                    MessageBox.Show("Houve algum erro durante a edição da venda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataChanged?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                controller.RemoveObject(mySale.Id);
                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
        #endregion

        #region Utilities

        private void InitilizeDateTimePicker()
        {
            dateTimePickerSales.Format = DateTimePickerFormat.Custom;
            dateTimePickerSales.CustomFormat = "dd/MM/yyyy HH:mm";
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

        private Sales createSaleFromFields(int saleId = 0)
        {
            Sales sale = null;

            if (int.TryParse(numericUpDownQuantity.Text, out int numero))
            {
                sale = new Sales()
                {
                    Product = (Product)comboBoxProducts.SelectedItem,
                    Quantity = numero,
                    User = (User)comboBoxUsers.SelectedItem,
                    SaleDate = dateTimePickerSales.Value,
                };

                if (saleId != 0)
                {
                    sale.Id = saleId;
                }
            }
            else
            {
                MessageBox.Show("Algum dos campos foi digitado um valor inesperado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sale;
        }
        private void EnablesAndDisablesButtons()
        {
            if (isEditMode)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDel.Enabled = true;
                this.Text = "Editar Venda";
                btnEdit.Text = "Salvar Alterações";
            }
            else
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
                this.Text = "Adicionar Nova Venda";
            }

        }
        #endregion
    }
}
