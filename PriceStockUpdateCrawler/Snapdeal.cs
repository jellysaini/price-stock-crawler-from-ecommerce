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
    public partial class Snapdeal : Form
    {
        List<Int64> _prodcutsIDs = new List<long>();
        Int64 ID = 0;
        Int64 MasterProductID = 0;

        public Snapdeal()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

            //Random rnd = new Random();
            //int myRandomNo = rnd.Next(10000000, 999999999);

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

        private string GetWebText(string url)
        {
            try
            {
                UriBuilder ub = new UriBuilder(url);
                System.Net.ServicePointManager.Expect100Continue = false;
                System.Net.ServicePointManager.CheckCertificateRevocationList = false;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ub.Uri);
                request.Proxy = new WebProxy();

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)";
                request.Method = "GET";

                //UriBuilder ub = new UriBuilder(url);
                //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ub.Uri);
                //request.Proxy = null;

                request.UserAgent = "A .NET Web Crawler";

                WebResponse response = request.GetResponse();

                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string htmlText = reader.ReadToEnd();
                return htmlText;
            }
            catch
            {
                return "";
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            PriceStockUpdateCrawler.EF.G2S101Entities _context = new EF.G2S101Entities();

            _context.Database.CommandTimeout = 18000000;

            Regex _firstRegex = new Regex("<span class=\"payBlkBig\" itemprop=\"price\">(.*?)</span>");
            Regex _secondRegex = new Regex("<span class=\"payBlkBig\"  itemprop=\"price\">(.*?)</span>");

            Regex _regexStock = new Regex("<span>Sold Out!</span>");

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

                    backgroundWorker1.ReportProgress(1, "Loading Data From File");

                    var productURL = productInfo.ProductUrl;
                    var htmlText = GetWebText(productURL);
                    if (htmlText != "")
                    {


                        #region Get Data from Snapdeal
                        Match _match = _firstRegex.Match(htmlText);
                        String price = "";
                        if (_match.Success)
                        {
                            price = _match.Groups[1].Value;
                        }
                        else
                        {
                            Match _newMatch = _secondRegex.Match(htmlText);
                            if (_newMatch.Success)
                            {
                                price = _newMatch.Groups[1].Value;
                            }
                        }
                        #endregion

                        if (price != "")
                        {
                            string _newPrice = price.Replace(",", "");
                            productInfo.Price = decimal.Parse(_newPrice);
                            productInfo.UpdatedOn = DateTime.Now;
                            #region InStock/Out Stock
                            Match _matchStock = _regexStock.Match(htmlText);
                            if (_matchStock.Success)
                            {
                                productInfo.InStock = false;
                            }
                            else
                            {
                                productInfo.InStock = true;
                            }
                            #endregion
                            _context.Entry(productInfo).State = System.Data.Entity.EntityState.Modified;
                            _context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    PriceStockUpdateCrawler.CalLog.AddtoLogFile(ex.Message.ToString(), "Snapdeal " + " Product_ID :- " + product_id + " Master_Product_ID:- " + master_product_id);
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
