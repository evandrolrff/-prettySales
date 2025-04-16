using AtividadeFinal.Views;
using AtividadeFinal.Views.DataGridColumns;
using System;
using System.Windows.Forms;

namespace AtividadeFinal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Abre o form <see cref="FormUsers">
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUser_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects(ObjectType.User);
            formListObjects.ShowDialog();
        }

        /// <summary>
        /// Abre o form <see cref="FormProducts"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProducts_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects(ObjectType.Product);
            formListObjects.ShowDialog();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects(ObjectType.Sale);
            formListObjects.ShowDialog();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            FormListObjects formListObjects = new FormListObjects(ObjectType.Payment);
            formListObjects.ShowDialog();
        }
    }
}
