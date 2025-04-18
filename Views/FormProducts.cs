using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace AtividadeFinal.Views
{
    public partial class FormProducts : Form
    {
        private readonly ProductController controller;
        private readonly Product myProduct;
        private readonly bool isEditMode = false;

        public event EventHandler DataChanged;
        
        public FormProducts(Product product = null)
        {
            InitializeComponent();
            controller = new ProductController();

            if (product != null)
            {
                isEditMode = true;
                myProduct = product;

                txtName.Text = product.Name;
                txtDescription.Text = product.Description;
                txtPrice.Text = product.Price.ToString();
                labelPathImage.Text = product.PathImage;

                try
                {
                    pictureBox.Image = Image.FromFile(product.PathImage);
                }
                catch (Exception ex) 
                {
                    pictureBox.Image = null;
                    pictureBox.BackColor = Color.LightGray;

                    using (Graphics g = pictureBox.CreateGraphics())
                    {
                        g.Clear(Color.LightGray);
                        g.DrawString("Erro ao carregar imagem",
                                     new Font("Arial", 10, FontStyle.Bold),
                                     Brushes.Red, new PointF(10, pictureBox.Height / 2 - 10));
                    }
                }
            }

            EnablesAndDisablesButtons();
        }

        #region EventsButtons
        /// <summary>
        /// Retorna para o menu principal <see cref="FormListObjects"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) ||
               !string.IsNullOrWhiteSpace(txtDescription.Text) ||
               !string.IsNullOrWhiteSpace(txtPrice.Text))
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
            bool productIsAdded = controller.AddObject(product);

            if (!productIsAdded)
            {
                MessageBox.Show("Houve um erro durante a inserção do novo produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataChanged?.Invoke(this, EventArgs.Empty);
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
                myProduct.Name = txtName.Text;
                myProduct.Description = txtDescription.Text;
                myProduct.Price = float.Parse(txtPrice.Text, CultureInfo.InvariantCulture.NumberFormat);
                myProduct.PathImage = labelPathImage.Text ?? string.Empty;

                bool isEdited = controller.UpdateObject(myProduct);
                if (!isEdited)
                {
                    MessageBox.Show("Houve algum erro durante a edição do produto.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                controller.RemoveObject(myProduct.Id);
                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Abre um <see cref="OpenFileDialog"/> para selecionar uma imagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                // Configurações opcionais
                Title = "Selecione um arquivo",
                Filter = "Imagens (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoArquivo = openFileDialog.FileName;

                labelPathImage.Text = caminhoArquivo;

                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = Image.FromFile(caminhoArquivo);
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
                    PathImage = labelPathImage.Text ?? string.Empty,
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
        /// Habilita ou desabilita os botões de acordo com o modo de edição
        /// </summary>
        private void EnablesAndDisablesButtons()
        {
            if (isEditMode)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDel.Enabled = true;
                btnSelect.Enabled = true;
                this.Text = "Editar Produto";
                btnEdit.Text = "Salvar Alterações";
            }
            else
            {
                btnAdd.Enabled = true;
                btnSelect.Enabled= true;
                btnEdit.Enabled = false;
                btnDel.Enabled = false;
                this.Text = "Adicionar Novo Produto";
            }

        }
        #endregion
    }
}
