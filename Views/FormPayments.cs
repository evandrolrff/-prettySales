using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AtividadeFinal.Views
{
    public partial class FormPayments : Form
    {
        private readonly PaymentsController controller;
        private readonly Payments myPayment;
        private readonly bool isEditMode = false;

        public event EventHandler DataChanged;
        private event EventHandler DataChangedComboBox;

        public FormPayments(Payments payment = null)
        {
            InitializeComponent();
            InitializeComboBox();
            InitilizeDateTimePicker();
            controller = new PaymentsController();

            if (payment != null)
            {
                isEditMode = true;
                myPayment = payment;

                comboBoxUsers.SelectedValue = payment.User.Id;
                SelectValueOfComboBoxSales(payment.Sale);

                textBoxAmount.Text = payment.Amount.ToString();
                dateTimePickerPayments.Value = payment.PaymentDate;

                comboBoxUsers.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxSales.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            EnablesAndDisablesButtons();
        }

        /// <summary>
        /// Seleciona o valor de <see cref="comboBoxSales"/> com base no objeto <see cref="Sales"/> passado como parâmetro.
        /// </summary>
        /// <param name="sale"></param>
        private void SelectValueOfComboBoxSales(Sales sale)
        {
            List<Sales> lista = (List<Sales>)comboBoxSales.DataSource;

            Sales correspondingSale = lista.FirstOrDefault(s => s.Id == sale.Id);

            if (correspondingSale != null)
            {
                comboBoxSales.SelectedItem = correspondingSale;
            }
            else
            {
                comboBoxSales.SelectedIndex = -1;
            }
        }


        #region Utilities
        /// <summary>
        /// Cria um objeto <see cref="Payments"/> a partir dos campos preenchidos no formulário.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        private Payments createPaymentFromFields(int paymentId = 0)
        {
            Payments payment = null;

            if (float.TryParse(textBoxAmount.Text, out float numero))
            {
                payment = new Payments()
                {
                    User = (User)comboBoxUsers.SelectedItem,
                    Sale = (Sales)comboBoxSales.SelectedItem,
                    Amount = numero,
                    PaymentDate = dateTimePickerPayments.Value,
                };

                if (paymentId != 0)
                {
                    payment.Id = paymentId;
                }
            }
            else
            {
                MessageBox.Show("Algum dos campos foi digitado um valor inesperado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return payment;
        }

        /// <summary>
        /// Inicializa o DateTimePicker com o formato desejado.
        /// </summary>
        private void InitilizeDateTimePicker()
        {
            dateTimePickerPayments.Format = DateTimePickerFormat.Custom;
            dateTimePickerPayments.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        /// <summary>
        /// Preenche o ComboBox com os usuários e as vendas disponíveis.
        /// </summary>
        private void InitializeComboBox()
        {
            comboBoxUsers.DataSource = new UserController().GetAllRegistry();
            comboBoxUsers.DisplayMember = "Name";
            comboBoxUsers.ValueMember = "Id";
            comboBoxUsers.SelectedIndex = -1;

            comboBoxSales.DataSource = new SalesController().GetAllRegistry();
            comboBoxSales.DisplayMember = null;
            comboBoxSales.ValueMember = null;
            comboBoxSales.SelectedIndex = -1;
        }

        /// <summary>
        /// Habilita ou desabilita os botões de acordo com o modo de edição.
        /// </summary>
        private void EnablesAndDisablesButtons()
        {
            if (isEditMode)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDel.Enabled = true;
                this.Text = "Editar Pagamento";
                btnEdit.Text = "Salvar Alterações";
            }
            else
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
                this.Text = "Adicionar Novo Pagamento";
            }
        }
        #endregion

        #region EventsButtons
        /// <summary>
        /// Fecha o formulário quando o botão de retorno é clicado. Retorna para <see cref="FormListObjects"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja retornar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Adiciona um novo pagamento <see cref="Payments"/> ao banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Payments payment = createPaymentFromFields();
            bool saleIsAdded = controller.AddObject(payment);

            if (!saleIsAdded)
            {
                MessageBox.Show("Houve um erro durante a inserção de um pagamento!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }

        /// <summary>
        /// Edita um pagamento <see cref="Payments"/> existente no banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja editar este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                myPayment.User = (User)comboBoxUsers.SelectedItem;
                myPayment.Sale = (Sales)comboBoxSales.SelectedItem;
                myPayment.PaymentDate = dateTimePickerPayments.Value;
                myPayment.Amount = float.Parse(textBoxAmount.Text);

                bool isEdited = controller.UpdateObject(myPayment);
                if (!isEdited)
                {
                    MessageBox.Show("Houve algum erro durante a edição do pagamento.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataChanged?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Exclui um pagamento <see cref="Payments"/> do banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                controller.RemoveObject(myPayment.Id);
                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
        #endregion

        /// <summary>
        /// Atualiza o ComboBox de vendas com base no usuário selecionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUsers.SelectedItem == null)
            {
                return;
            }

            User user = (User)comboBoxUsers.SelectedItem;

            comboBoxSales.DataSource = new PaymentsController().GetUserSales(user);
            comboBoxSales.DisplayMember = null;
            comboBoxSales.ValueMember = null;
            comboBoxSales.Refresh();
            comboBoxSales.SelectedIndex = -1;
        }
    }
}
