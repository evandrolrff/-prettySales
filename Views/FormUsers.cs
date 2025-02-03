using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtividadeFinal
{
    public partial class FormUsers : Form
    {

        private UserController controller;

        public FormUsers(UserController userController = null)
        {
            InitializeComponent();
            
            if(userController != null)
            {
                controller = userController;
                GetAllUsers(controller);
            }
        }

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

        private void GetAllUsers(UserController controller)
        {
            List<User> users = controller.GetAllUsers();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            User user = createUserFromFields();
            bool userIsAdded = controller.AddUser(user);

            if (!userIsAdded)
            {
                MessageBox.Show("Houve um erro durante a inserção do novo usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                clean_fields();
            }
        }

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Verifique se uma linha está selecionada
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int rowIndex = dataGridUsers.SelectedRows[0].Index;
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsers.Rows[rowIndex];

                fillInFields(row);

                DialogResult result = MessageBox.Show("Tem certeza que deseja editar este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    User user  = createUserFromFields();
                    bool isEdited = controller.UpdateUser(user);
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            // Verifique se uma linha está selecionada
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int rowIndex = dataGridUsers.SelectedRows[0].Index;
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridUsers.Rows[rowIndex];

                fillInFields(row);

                DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    controller.RemoveUser(Convert.ToInt32(row.Cells["Id"].Value));
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

        private void fillInFields(DataGridViewRow row)
        {
            // Preenche os campos txtNome e txtEmail com os valores da linha selecionada
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtLastName.Text = row.Cells["LastName"].Value.ToString();
            txtAddress.Text = row.Cells["Address"].Value.ToString();
            txtNumber.Text = row.Cells["Number"].Value.ToString();
            txtComplement.Text = row.Cells["Complement"].Value.ToString();
        }

        private User createUserFromFields()
        {
            User user = null;

            if (int.TryParse(txtNumber.Text, out int numero))
            {
                user = new User()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    Address = txtAddress.Text,
                    Number = numero,
                    Complement = txtComplement.Text,
                };
            }
            else
            {
                MessageBox.Show("Algum dos campos foi digitado um valor inesperado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return user;
        }
    }
}
