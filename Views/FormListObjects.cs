using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using AtividadeFinal.Views.DataGridColumns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AtividadeFinal.Views
{
    public partial class FormListObjects : Form
    {
        private readonly ObjectType objectType;

        public FormListObjects(ObjectType objectType)
        {
            this.objectType = objectType;

            InitializeComponent();
            ConfigureDataGridView();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch (objectType)
            {
                case ObjectType.User:
                    using (FormUsers form = new FormUsers())
                    {
                        form.DataChanged += (s, args) => RefreshData();
                        form.ShowDialog();
                    }
                    break;
                case ObjectType.Product:
                    new FormProducts().ShowDialog();
                    break;
                case ObjectType.Sale:
                    new FormSales().ShowDialog();
                    break;
                case ObjectType.Payment:
                    new FormPayments().ShowDialog();
                    break;
                default:
                    throw new ArgumentException("Tipo de objeto inválido");
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                dataGridViewObjects.Columns.Clear();

                Dictionary<string, string> columnsConfig = GetColumnsConfig();
                foreach (KeyValuePair<string, string> column in columnsConfig)
                {
                    var dataColumn = new DataGridViewTextBoxColumn
                    {
                        Name = $"col-{column.Key}",
                        HeaderText = column.Value,
                        DataPropertyName = column.Key,
                        Visible = column.Key != "id"
                    };
                    dataGridViewObjects.Columns.Add(dataColumn);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void LoadData()
        {
            IList data = null;

            switch (objectType)
            {
                case ObjectType.User:
                    data = new UserController().GetAllRegistry();
                    break;
                case ObjectType.Product:
                    data = new ProductController().GetAllRegistry();
                    break;
                case ObjectType.Sale:
                    data = new SalesController().GetAllRegistry();
                    break;
                case ObjectType.Payment:
                    data = new PaymentsController().GetAllRegistry();
                    break;
                default:
                    throw new ArgumentException("Tipo de objeto inválido");
            }

            if (data == null || data.Count == 0)
            {
                dataGridViewObjects.Enabled = false;
                return;
            }

            dataGridViewObjects.DataSource = data;
        }

        public void RefreshData()
        {
            try
            {
                var firstDisplayedRow = dataGridViewObjects.FirstDisplayedScrollingRowIndex;

                // Limpa o binding para forçar recarregamento
                dataGridViewObjects.DataSource = null;

                // Recarrega os dados
                LoadData();

                // Restaura a posição de rolagem
                if (firstDisplayedRow >= 0 && firstDisplayedRow < dataGridViewObjects.Rows.Count)
                {
                    dataGridViewObjects.FirstDisplayedScrollingRowIndex = firstDisplayedRow;
                }

                // Habilita o grid se estiver desabilitado
                dataGridViewObjects.Enabled = true;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private Dictionary<string, string> GetColumnsConfig()
        {
            switch(objectType)
            {
                case ObjectType.User:
                    return DataGridColumnsConfig.Users;
                case ObjectType.Product:
                    return DataGridColumnsConfig.Products;
                case ObjectType.Sale:
                    return DataGridColumnsConfig.Sales;
                case ObjectType.Payment:
                    return DataGridColumnsConfig.Payments;
                default:
                    throw new ArgumentException("Tipo de objeto inválido"); 
            }
        }

        private void HandleError(Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            MessageBox.Show("Erro ao carregar a exibição!");
            Close();
        }

        /// <summary>
        /// Preenche os campos de acordo com o registro selecionado do DataGridView <see cref="DataGridView.Rows">
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridObjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha válida (não no cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow selectedRow = dataGridViewObjects.Rows[e.RowIndex];

                CreateObjectAndInitilizeForm(selectedRow);
            }
        }

        private void CreateObjectAndInitilizeForm(DataGridViewRow row)
        {
            // Should be the same of all structres of <see cref="DataGridColumnsConfig"/>
            int id = Convert.ToInt32(row.Cells[$"col-id"].Value); 
            
            switch (objectType)
            {
                case ObjectType.User:
                    User user = new UserController().GetObjectById(id);
                    using (FormUsers form = new FormUsers(user))
                    {
                        form.DataChanged += (s, args) => RefreshData();
                        form.ShowDialog();
                    }
                    break;
                case ObjectType.Product:
                    Product product = new ProductController().GetObjectById(id);
                    break;
                case ObjectType.Sale:
                    Sales sale = new SalesController().GetObjectById(id);
                    break;
                case ObjectType.Payment:
                    Payments payment = new PaymentsController().GetObjectById(id);
                    break;
                default:
                    throw new ArgumentException("Tipo de objeto inválido");
            }
        }

    }
}
