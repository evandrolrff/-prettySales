namespace AtividadeFinal
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUser = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(230, 53);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(75, 23);
            this.btnUser.TabIndex = 0;
            this.btnUser.Text = "Usuários";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Location = new System.Drawing.Point(230, 95);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(75, 23);
            this.btnProducts.TabIndex = 1;
            this.btnProducts.Text = "Produtos";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 184);
            this.Controls.Add(this.btnProducts);
            this.Controls.Add(this.btnUser);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnProducts;
    }
}

