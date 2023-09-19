using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceStockUpdateCrawler
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void btnSnapdeal_Click(object sender, EventArgs e)
        {
            Snapdeal obj = new Snapdeal();
            obj.Show();
        }

        private void btnFlipkart_Click(object sender, EventArgs e)
        {
            Flipkart obj = new Flipkart();
            obj.Show();
        }

        private void btnAmazon_Click(object sender, EventArgs e)
        {
            Amazon obj = new Amazon();
            obj.Show();
        }

        private void btnDesc_Click(object sender, EventArgs e)
        {
            Description obj = new Description();
            obj.Show();
        }
    }
}
