using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AtividadeFinal
{
    public partial class FormUsers : Form
    {

        private UserController controller;

        private readonly Dictionary<string, string> columnsDataGridUsers = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "name" , "Nome"},
            { "lastName", "Sobrenome"},
            { "address", "Endereço"},
            { "number", "Número"},
            { "complement", "Complemento"}
        };

        public FormUsers(UserController userController = null)
        {
            InitializeComponent();
            ConfigureDataGridView();

            if (userController != null)
            {
                controller = userController;
                GetAllUsers(controller);
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
                clean_fields();
            }
        }

        /// <summary>
        /// Retorna para o menu principal <see cref="MainForm"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtName.Text) ||
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
            // Verifique se uma linha está selecionada
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int rowIndex = dataGridUsers.SelectedRows[0].Index;
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsers.Rows[rowIndex];

                DialogResult result = MessageBox.Show("Tem certeza que deseja editar este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    User user = createUserFromFields(Convert.ToInt32(row.Cells[$"col-id"].Value));
                    bool isEdited = controller.UpdateObject(user);
                    if (!isEdited)
                    {
                        MessageBox.Show("Houve algum erro durante a edição do usuário.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                clean_fields();
            }
            else
            {
                MessageBox.Show("Selecione uma linha para editar.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Deleta um usuário já selecionado pelo DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            // Verifique se uma linha está selecionada
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int rowIndex = dataGridUsers.SelectedRows[0].Index;
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsers.Rows[rowIndex];

                DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    controller.RemoveObject(Convert.ToInt32(row.Cells["col-id"].Value));
                    this.Close();
                }
                else
                {
                    clean_fields();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region EventsDataGrid
        /// <summary>
        /// Preenche o DataGridView com todos os usuários
        /// </summary>
        /// <param name="controller"></param>
        private void GetAllUsers(UserController controller)
        {
            List<User> users = controller.GetAllRegistry();
            if (users.Count > 0)
            {
                dataGridUsers.DataSource = null; // reseta a fonte de dados
                dataGridUsers.DataSource = users; // Atribui a lista ao DataGridView
            }
            else
            {
                btnDel.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        /// <summary>
        /// Preenche os campos de acordo com o usuário selecionado do DataGridView <see cref="DataGridView.SelectedRows"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridUsers_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica se alguma linha foi selecionada
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                // Ação que você deseja realizar quando uma linha for selecionada
                DataGridViewRow selectedRow = dataGridUsers.SelectedRows[0];

                fillInFields(selectedRow);
            }
        }

        /// <summary>
        /// Preenche os campos de acordo com o usuário selecionado do DataGridView <see cref="DataGridView.Rows">
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha válida (não no cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsers.Rows[e.RowIndex];
                fillInFields(row);
            }
            else
            {
                clean_fields();
            }
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Limpa os campos de textos do form.
        /// </summary>
        private void clean_fields()
        {
            foreach (Control controle in this.Controls)
            {
                if (controle is TextBox)
                {
                    controle.Text = "";
                }
            }

            GetAllUsers(controller);
        }

        /// <summary>
        /// Preenche os campos de acordo com um <see cref="DataGridViewRow"> selecionado.
        /// </summary>
        /// <param name="row"></param>
        private void fillInFields(DataGridViewRow row)
        {
            foreach(KeyValuePair<string, string> valuePair in columnsDataGridUsers)
            {
                switch (valuePair.Key)
                {
                    case "name":
                        txtName.Text = row.Cells[$"col-{valuePair.Key}"].Value.ToString();
                        break;
                    case "lastName":
                        txtLastName.Text = row.Cells[$"col-{valuePair.Key}"].Value.ToString();
                        break;
                    case "address":
                        txtAddress.Text = row.Cells[$"col-{valuePair.Key}"].Value.ToString();
                        break;
                    case "number":
                        txtNumber.Text = row.Cells[$"col-{valuePair.Key}"].Value.ToString();
                        break;
                    case "complement":
                        txtComplement.Text = row.Cells[$"col-{valuePair.Key}"].Value.ToString();
                        break;
                    case "id":
                    default:
                        break;
                }
            }
        }

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

            if(userId != 0)
            {
                user.Id = userId;
            }

            return user;
        }
        #endregion
    }
}
