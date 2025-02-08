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
using System.Xml.Linq;

namespace AtividadeFinal.Views
{
    public partial class FormProducts : Form
    {

        private ProductController controller;

        public FormProducts(ProductController productController = null)
        {
            InitializeComponent();
            if (productController != null)
            {
                controller = productController;
                GetAllProducts(controller);
            }
        }

        #region EventsButtons
        /// <summary>
        /// Retorna para o menu principal <see cref="MainForm"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) ||
               !string.IsNullOrWhiteSpace(txtDescription.Text) ||
               !string.IsNullOrWhiteSpace(txtPrice.Text) ||
               !string.IsNullOrWhiteSpace(txtPathImage.Text))
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
        /// Adiciona um novo Product <see cref="Product"></see>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product = createProductFromFields();
            bool productIsAdded = controller.AddProduct(product);

            if (!productIsAdded)
            {
                MessageBox.Show("Houve um erro durante a inserção do novo produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                clean_fields();
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
            if (dataGridProducts.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int rowIndex = dataGridProducts.SelectedRows[0].Index;
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridProducts.Rows[rowIndex];

                DialogResult result = MessageBox.Show("Tem certeza que deseja editar este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    Product product = createProductFromFields(Convert.ToInt32(row.Cells["Id"].Value));
                    bool isEdited = controller.UpdateProduct(product);
                    if (!isEdited)
                    {
                        MessageBox.Show("Houve algum erro durante a edição do produto.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridProducts.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int rowIndex = dataGridProducts.SelectedRows[0].Index;
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridProducts.Rows[rowIndex];

                DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    controller.RemoveProduct(Convert.ToInt32(row.Cells["Id"].Value));
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
        /// Preenche o DataGridView com todos os produtos
        /// </summary>
        /// <param name="controller"></param>
        private void GetAllProducts(ProductController controller)
        {
            List<Product> products = controller.GetAllProducts();
            if (products.Count > 0)
            {
                dataGridProducts.DataSource = null; // reseta a fonte de dados
                dataGridProducts.DataSource = products; // Atribui a lista ao DataGridView
            }
            else
            {
                btnDel.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        /// <summary>
        /// Preenche os campos de acordo com o produto selecionado do DataGridView <see cref="DataGridView.SelectedRows"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridProducts_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica se alguma linha foi selecionada
            if (dataGridProducts.SelectedRows.Count > 0)
            {
                // Ação que você deseja realizar quando uma linha for selecionada
                DataGridViewRow selectedRow = dataGridProducts.SelectedRows[0];

                fillInFields(selectedRow);
            }
        }

        /// <summary>
        /// Preenche os campos de acordo com o produto selecionado do DataGridView <see cref="DataGridView.Rows">
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha válida (não no cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow row = dataGridProducts.Rows[e.RowIndex];
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
        /// Cria um objeto do tipo <see cref="Product"/> segundo os valores informados
        /// nos campos de entrada de texto.
        /// </summary>
        /// <returns></returns>
        private Product createProductFromFields(int productId = 0)
        {
            Product product = null;

            if (float.TryParse(txtPrice.Text, out float numero))
            {
                product = new Product()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Price = numero,
                    PathImage = txtPathImage.Text,
                };

                if (productId != 0)
                {
                    product.Id = productId;
                }
            }
            else
            {
                MessageBox.Show("Algum dos campos foi digitado um valor inesperado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return product;
        }

        /// <summary>
        /// Preenche os campos de acordo com um <see cref="DataGridViewRow"> selecionado.
        /// </summary>
        /// <param name="row"></param>
        private void fillInFields(DataGridViewRow row)
        {
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtDescription.Text = row.Cells["Description"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
            txtPathImage.Text = row.Cells["pathImage"].Value.ToString();
        }

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

            GetAllProducts(controller);
        }
        #endregion
    }
}
