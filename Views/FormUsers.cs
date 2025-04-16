using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Windows.Forms;

namespace AtividadeFinal
{
    public partial class FormUsers : Form
    {
        private UserController controller;
        private User myUser;

        public event EventHandler DataChanged;

        public FormUsers(User user = null)
        {
            InitializeComponent();
            controller = new UserController();

            if (user != null)
            {
                myUser = user;

                txtName.Text = user.Name;
                txtLastName.Text = user.LastName;
                txtAddress.Text = user.Address;
                txtNumber.Text = user.Number;
                txtComplement.Text = user.Complement;
            }
        }

        #region EventsButtons
        /// <summary>
        /// Adiciona um novo User <see cref="User"></see>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            User user = createUserFromFields();
            bool userIsAdded = controller.AddObject(user);

            if (!userIsAdded)
            {
                MessageBox.Show("Houve um erro durante a inserção do novo usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }

        /// <summary>
        /// Retorna para o menu principal <see cref="MainForm"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) ||
               !string.IsNullOrWhiteSpace(txtLastName.Text) ||
               !string.IsNullOrWhiteSpace(txtAddress.Text) ||
               !string.IsNullOrWhiteSpace(txtNumber.Text) ||
               !string.IsNullOrWhiteSpace(txtComplement.Text))
            {
                DialogResult result = MessageBox.Show("Tem certeza que deseja retornar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Edita um usuário já selecionado pelo DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja editar este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                myUser.Name = txtName.Text;
                myUser.LastName = txtLastName.Text;
                myUser.Address = txtAddress.Text;
                myUser.Number = txtNumber.Text;
                myUser.Complement = txtComplement.Text;

                bool isEdited = controller.UpdateObject(myUser);
                if (!isEdited)
                {
                    MessageBox.Show("Houve algum erro durante a edição do usuário.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataChanged?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Deleta um usuário já selecionado pelo DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                controller.RemoveObject(myUser.Id);
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
        /// <summary>
        /// Cria um objeto do tipo <see cref="User"/> segundo os valores informados
        /// nos campos de entrada de texto.
        /// </summary>
        /// <returns></returns>
        private User createUserFromFields(int userId = 0)
        {
            User user = null;

            user = new User()
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                Address = txtAddress.Text,
                Number = txtNumber.Text,
                Complement = txtComplement.Text,
            };

            if (userId != 0)
            {
                user.Id = userId;
            }

            return user;
        }
        #endregion
    }
}
