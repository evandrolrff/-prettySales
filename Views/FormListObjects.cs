using AtividadeFinal.Controllers;
using AtividadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AtividadeFinal.Views
{
    public partial class FormListObjects : Form
    {

        private readonly Dictionary<string, string> columnsDataGridUsers = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "name" , "Nome"},
            { "lastName", "Sobrenome"},
            { "address", "Endereço"},
            { "number", "Número"},
            { "complement", "Complemento"}
        };

        private readonly Dictionary<string, string> columnsDataGridProducts = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "name" , "Nome"},
            { "description", "Descrição"},
            { "price", "Preço"},
            { "pathImage", "URL"}
        };

        private readonly Dictionary<string, string> columnsDataGridSales = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "UserName" , "Cliente"},
            { "ProductName", "Produto"},
            { "quantity", "Quantidade"},
            { "saleDate", "Data da Venda"}
        };

        private readonly Dictionary<string, string> columnsDataGridPayments = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "userId" , "Cliente"},
            { "saleId", "Venda"},
            { "amount", "Valor"},
            { "paymentDate", "Data do Pagamento"}
        };

        public FormListObjects(string typeObject)
        {
            InitializeComponent();
            ConfigureDataGridView(typeObject);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void ConfigureDataGridView(string typeObject)
        {
            DataGridViewTextBoxColumn colId;

            try
            {
                foreach (KeyValuePair<string, string> valuePair in WhatDataShouldLoad(typeObject))
                {
                    colId = new DataGridViewTextBoxColumn();
                    colId.Name = $"col-{valuePair.Key}";
                    colId.HeaderText = valuePair.Value;
                    colId.DataPropertyName = valuePair.Key; // Nome da propriedade no objeto de dados

                    if (colId.DataPropertyName == "id")
                    {
                        colId.Visible = false;
                    }
                    dataGridViewObjects.Columns.Add(colId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao configurar o DataGridView: {ex.Message}");
                MessageBox.Show($"Erro ao carregar a exibição!");
                this.Close();
            }
        }

        private Dictionary<string, string> WhatDataShouldLoad(string typeObject)
        {
            switch (typeObject) 
            {
                case "users":
                    GetAllObjects<User>(typeObject);
                    return columnsDataGridUsers;
                case "products":
                    GetAllObjects<Product>(typeObject);
                    return columnsDataGridProducts;
                case "sales":
                    GetAllObjects<Sales>(typeObject);
                    return columnsDataGridSales;
                case "payments":
                    GetAllObjects<Payments>(typeObject);
                    return columnsDataGridPayments;
                default:
                    return null;
            }
        }

        private void GetAllObjects<T>(string typeObject) where T : class
        {
            T obj = GetTypeOject<T>(typeObject);
            List<T> objects = null;

            switch (typeObject)
            {
                case "users":
                    UserController userController = new UserController();
                    objects = userController.GetAllRegistry() as List<T>;
                    break;
                case "products":
                    ProductController productController = new ProductController();
                    objects = productController.GetAllRegistry() as List<T>;
                    break;
                case "sales":
                    SalesController salesController = new SalesController();
                    objects = salesController.GetAllRegistry() as List<T>;
                    break;
                case "payments":
                    PaymentsController paymentsController = new PaymentsController();
                    objects = paymentsController.GetAllRegistry() as List<T>;
                    break;
                default:
                    MessageBox.Show("Tipo de objeto inválido.");
                    throw new ArgumentException($"Tipo '{typeObject}' não encontrado.");
            }

            if (objects.Count > 0)
            {
                dataGridViewObjects.DataSource = null; // reseta a fonte de dados
                dataGridViewObjects.DataSource = objects; // Atribui a lista ao DataGridView
            }
            else
            {
                dataGridViewObjects.Enabled = false;
                dataGridViewObjects.Enabled = false;
            }
        }

        private T GetTypeOject<T>(string typeObject) where T : class
        {
            switch (typeObject)
            {
                case "users":
                    return new User() as T;
                case "products":
                    return new Product() as T;
                case "sales":
                    return new Sales() as T;
                case "payments":
                    return new Payments() as T;
                default:
                    Console.WriteLine("Tipo de objeto inválido.");
                    throw new ArgumentException($"Tipo '{typeObject}' não encontrado.");
            }
        }
    }
}
