using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceStockUpdateCrawler
{
    public partial class Description : Form
    {
        List<Int64> _prodcutsIDs = new List<long>();
        Int64 ID = 0;
        Int64 MasterProductID = 0;


        public Description()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {


            lblMessage.Text = "Please wait price update is in-process .......";
            lblMessage.Refresh();

            PriceStockUpdateCrawler.EF.G2S101Entities _context = new EF.G2S101Entities();
            _prodcutsIDs = (from p in _context.tblProducts
                            where p.MemberID == 1
                            select p.ID).ToList();


            lblTotalProducts.Text = "Total Products Need to Update :- " + Convert.ToString(_prodcutsIDs.Count());
            lblTotalProducts.Refresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblDate.Text = "Following is the start date and time :- " + DateTime.Now.ToString();
            lblDate.Refresh();

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            PriceStockUpdateCrawler.EF.G2S101Entities _context = new EF.G2S101Entities();
            _context.Database.CommandTimeout = 18000000;

            string product_id = "";
            string master_product_id = "";


            foreach (var id in _prodcutsIDs)
            {
                try
                {
                    var productInfo = (from p in _context.tblProducts
                                           //where p.ID == id && p.MemberID == 1
                                       where p.MasterProductID == 46 && p.MemberID == 1
                                       select p).FirstOrDefault();

                    product_id = Convert.ToString(productInfo.ID);
                    master_product_id = Convert.ToString(productInfo.MasterProductID);

                    ID = productInfo.ID;
                    MasterProductID = Convert.ToInt64(productInfo.MasterProductID);

                    var productURL = productInfo.ProductUrl;

                    backgroundWorker1.ReportProgress(1, "Loading Data From File");


                    HtmlWeb web = new HtmlWeb();

                    HtmlAgilityPack.HtmlDocument document = web.Load(productURL);

                    HtmlNode node = document.DocumentNode.SelectNodes("//ul[@class='dtls-list clear']").First();

                    String highlights = "<ul>" + node.InnerHtml.ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\t1", "") + "</ul>";

                    var masterProductInfo = (from p in _context.tblMasterProducts
                                             where p.ID == MasterProductID
                                             select p).FirstOrDefault();

                    if (masterProductInfo != null)
                    {
                        masterProductInfo.Highlights = highlights;
                        masterProductInfo.UpdatedOn = DateTime.Now;

                        _context.Entry(masterProductInfo).State = System.Data.Entity.EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    CalLog.AddtoLogFile(ex.Message.ToString(), "Snapdeal Desc Update Issue" + " Product_ID :- " + product_id + " Master_Product_ID:- " + master_product_id);
                }
            }

        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblID.Text = Convert.ToString(ID);
            lblMasterProductID.Text = Convert.ToString(MasterProductID);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblMessage.Text = "Price Update has been done. Please check the Gateway2Sites prices. Date & Time :-" + DateTime.Now.ToString();
            lblMessage.Refresh();
        }
    }
}
