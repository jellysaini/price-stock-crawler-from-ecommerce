namespace PriceStockUpdateCrawler
{
    partial class MainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSnapdeal = new System.Windows.Forms.Button();
            this.btnFlipkart = new System.Windows.Forms.Button();
            this.btnAmazon = new System.Windows.Forms.Button();
            this.btnDesc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSnapdeal
            // 
            this.btnSnapdeal.Location = new System.Drawing.Point(292, 55);
            this.btnSnapdeal.Name = "btnSnapdeal";
            this.btnSnapdeal.Size = new System.Drawing.Size(460, 69);
            this.btnSnapdeal.TabIndex = 0;
            this.btnSnapdeal.Text = "Snapdeal";
            this.btnSnapdeal.UseVisualStyleBackColor = true;
            this.btnSnapdeal.Click += new System.EventHandler(this.btnSnapdeal_Click);
            // 
            // btnFlipkart
            // 
            this.btnFlipkart.Location = new System.Drawing.Point(292, 160);
            this.btnFlipkart.Name = "btnFlipkart";
            this.btnFlipkart.Size = new System.Drawing.Size(460, 69);
            this.btnFlipkart.TabIndex = 1;
            this.btnFlipkart.Text = "Flipkart";
            this.btnFlipkart.UseVisualStyleBackColor = true;
            this.btnFlipkart.Click += new System.EventHandler(this.btnFlipkart_Click);
            // 
            // btnAmazon
            // 
            this.btnAmazon.Location = new System.Drawing.Point(292, 278);
            this.btnAmazon.Name = "btnAmazon";
            this.btnAmazon.Size = new System.Drawing.Size(460, 69);
            this.btnAmazon.TabIndex = 2;
            this.btnAmazon.Text = "Amazon";
            this.btnAmazon.UseVisualStyleBackColor = true;
            this.btnAmazon.Click += new System.EventHandler(this.btnAmazon_Click);
            // 
            // btnDesc
            // 
            this.btnDesc.Location = new System.Drawing.Point(292, 405);
            this.btnDesc.Name = "btnDesc";
            this.btnDesc.Size = new System.Drawing.Size(460, 69);
            this.btnDesc.TabIndex = 3;
            this.btnDesc.Text = "Desc";
            this.btnDesc.UseVisualStyleBackColor = true;
            this.btnDesc.Click += new System.EventHandler(this.btnDesc_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 628);
            this.Controls.Add(this.btnDesc);
            this.Controls.Add(this.btnAmazon);
            this.Controls.Add(this.btnFlipkart);
            this.Controls.Add(this.btnSnapdeal);
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSnapdeal;
        private System.Windows.Forms.Button btnFlipkart;
        private System.Windows.Forms.Button btnAmazon;
        private System.Windows.Forms.Button btnDesc;
    }
}