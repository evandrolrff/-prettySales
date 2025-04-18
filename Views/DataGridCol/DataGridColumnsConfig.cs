using System.Collections.Generic;

namespace AtividadeFinal.Views.DataGridColumns
{
    public static class DataGridColumnsConfig
    {
        public static readonly Dictionary<string, string> Users = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "name" , "Nome"},
            { "lastName", "Sobrenome"},
            { "address", "Endereço"},
            { "number", "Número"},
            { "complement", "Complemento"}
        };

        public static readonly Dictionary<string, string> Products = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "name" , "Nome"},
            { "description", "Descrição"},
            { "price", "Preço"},
            { "pathImage", "URL"}
        };

        public static readonly Dictionary<string, string> Sales = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "UserName" , "Cliente"},
            { "ProductName", "Produto"},
            { "quantity", "Quantidade"},
            { "saleDate", "Data da Venda"}
        };

        public static readonly Dictionary<string, string> Payments = new Dictionary<string, string>
        {
            { "id", "ID"},
            { "UserName" , "Cliente"},
            { "SaleName", "Venda"},
            { "amount", "Valor"},
            { "paymentDate", "Data do Pagamento"}
        };
    }

    public enum ObjectType
    {
        User,
        Product,
        Sale,
        Payment
    }
}
