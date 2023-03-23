using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ManageMaterialPBA
{
    public partial class StockWaitPDxn : Form
    {
        Barcode frm;
        database_1 dtb1 = new database_1();
        database dtb = new database();
        ClsExcel excl = new ClsExcel();
        public string dtim = string.Empty;

        public StockWaitPDxn(Barcode _frm)
        {
            InitializeComponent();
            frm = _frm;
        }

        private void StockWaitPDxn_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            DataTable dt_sl = dtb1.search_stock("PDxacnhanStock_1", true);
            dtb1.show_StockLinee(dgv_stockPDxn, dt_sl);

            dtb.get_inf(cbx_ngaythang, "PDxacnhanStock_1", "Ngay_thang");
            dtb.get_inf(cbx_maNVL, "PDxacnhanStock_1", "Ma_NVL");
            dtb.get_inf(cbx_maker, "PDxacnhanStock_1", "Maker");
            dtb.get_inf(cbx_mkrprt, "PDxacnhanStock_1", "Maker_Part");
            dtb.get_inf(cbx_lot, "PDxacnhanStock_1", "Lot");

            dtim = getYearMonthDay();
        }

        public string getYearMonthDay()
        {
            string str = string.Empty;
            if (DateTime.Now.Month < 10)
                str = DateTime.Now.Year.ToString() + "-0" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            else
                str = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            return str;
        }

        private void btn_svStkPDxn_Click(object sender, EventArgs e)
        {
            SaveFileDialog savDia = new SaveFileDialog();
            savDia.Title = "Excel Save Dialog";
            savDia.InitialDirectory = @"C:\";
            savDia.Filter = "Excel File |*.csv";
            savDia.FilterIndex = 1;
            string fil_name = "";
            if (savDia.ShowDialog() == DialogResult.OK)
            {
                fil_name = savDia.FileName;
            }

            if (fil_name != "")
            {
                bool chek = excl.checkExitLog(fil_name);
                excl.exportStockKTZZ(dgv_stockPDxn, fil_name, chek);                
                MessageBox.Show("Lưu thành công!", "StockWaitPDxn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }        
        }

        private void btn_tx_Click(object sender, EventArgs e)
        {
            dgv_stockPDxn.Columns.Clear();

            string str = string.Empty;

            try
            {
                //all
                if (cbx_ngaythang.Text == "" && cbx_maNVL.Text == "" && cbx_maker.Text == "" && cbx_mkrprt.Text == "" && cbx_lot.Text == "")
                {
                    str = "Select * From PDxacnhanStock_1";
                    goto jump;
                }

                //
                if (cbx_ngaythang.Text != "" && cbx_maNVL.Text != "" && cbx_maker.Text != "" && cbx_mkrprt.Text != "" && cbx_lot.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ngay_thang='" + cbx_ngaythang.Text + "' And Ma_NVL='" + cbx_maNVL.Text + "' And Maker='" + cbx_maker.Text + "' And Maker_Part='" + cbx_mkrprt.Text + "' And Lot='" + cbx_lot.Text + "'";
                    goto jump;
                }

                if (cbx_ngaythang.Text != "" && cbx_maNVL.Text != "" && cbx_maker.Text != "" && cbx_mkrprt.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ngay_thang='" + cbx_ngaythang.Text + "' And Ma_NVL='" + cbx_maNVL.Text + "' And Maker='" + cbx_maker.Text + "' And Maker_Part='" + cbx_mkrprt.Text + "'";
                    goto jump;
                }

                if (cbx_ngaythang.Text != "" && cbx_maNVL.Text != "" && cbx_maker.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ngay_thang='" + cbx_ngaythang.Text + "' And Ma_NVL='" + cbx_maNVL.Text + "' And Maker='" + cbx_maker.Text + "'";
                    goto jump;
                }

                if (cbx_ngaythang.Text != "" && cbx_maNVL.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ngay_thang='" + cbx_ngaythang.Text + "' And Ma_NVL='" + cbx_maNVL.Text + "'";
                    goto jump;
                }

                if (cbx_ngaythang.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ngay_thang='" + cbx_ngaythang.Text + "'";
                    goto jump;
                }

                //
                if (cbx_maNVL.Text != "" && cbx_maker.Text != "" && cbx_mkrprt.Text != "" && cbx_lot.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ma_NVL='" + cbx_maNVL.Text + "' And Maker='" + cbx_maker.Text + "' And Maker_Part='" + cbx_mkrprt.Text + "' And Lot='" + cbx_lot.Text + "'";
                    goto jump;
                }

                if (cbx_maNVL.Text != "" && cbx_maker.Text != "" && cbx_mkrprt.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ma_NVL='" + cbx_maNVL.Text + "' And Maker='" + cbx_maker.Text + "' And Maker_Part='" + cbx_mkrprt.Text + "'";
                    goto jump;
                }

                if (cbx_maNVL.Text != "" && cbx_maker.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ma_NVL='" + cbx_maNVL.Text + "' And Maker='" + cbx_maker.Text + "'";
                    goto jump;
                }

                if (cbx_maNVL.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Ma_NVL='" + cbx_maNVL.Text + "'";
                    goto jump;
                }

                //
                if (cbx_maker.Text != "" && cbx_mkrprt.Text != "" && cbx_lot.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Maker='" + cbx_maker.Text + "' And Maker_Part='" + cbx_mkrprt.Text + "' And Lot='" + cbx_lot.Text + "'";
                    goto jump;
                }

                if (cbx_maker.Text != "" && cbx_mkrprt.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Maker='" + cbx_maker.Text + "' And Maker_Part='" + cbx_mkrprt.Text + "'";
                    goto jump;
                }

                if (cbx_maker.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Maker='" + cbx_maker.Text + "'";
                    goto jump;
                }

                //
                if (cbx_mkrprt.Text != "" && cbx_lot.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Maker_Part='" + cbx_mkrprt.Text + "' And Lot='" + cbx_lot.Text + "'";
                    goto jump;
                }

                if (cbx_mkrprt.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Maker_Part='" + cbx_mkrprt.Text + "'";
                    goto jump;
                }

                //
                if (cbx_lot.Text != "")
                {
                    str = "Select * From PDxacnhanStock_1 where Lot='" + cbx_lot.Text + "'";
                    goto jump;
                }

            jump:
                DataTable dt = dtb.getData(str);
                dtb1.show_StockLinee(dgv_stockPDxn, dt);

                int sum = 0;
                for (int i = 0; i < dgv_stockPDxn.RowCount - 1; i++)
                {
                    if (dgv_stockPDxn.Rows[i].Cells["So_luong_cap"].Value.ToString() != "" && dgv_stockPDxn.Rows[i].Cells["So_luong_cap"].Value.ToString() != null)
                    {
                        sum = sum + int.Parse(dgv_stockPDxn.Rows[i].Cells["So_luong_cap"].Value.ToString());
                    }
                    else
                    {
                        break;
                    }
                }

                txt_qty.Text = sum.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Xảy ra lỗi không thể lọc dữ liệu!", "StockWaitPDxn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbx_ngaythang.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cbx_maNVL.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cbx_maker.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cbx_mkrprt.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cbx_lot.Text = "";
        }

        private void dgv_stockPDxn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string[] str = dgv_stockPDxn.CurrentRow.Cells["Ngay_thang"].Value.ToString().Split('/');
                StreamReader sr = new StreamReader(@Application.StartupPath + "\\Log\\Duplicate\\PDxacnhan.log");
                FileStream fs = new FileStream(@Application.StartupPath + "\\Print\\PDxn\\" + str[2] + str[0] + str[1] + "_NewCode.log", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                string code = dgv_stockPDxn.CurrentRow.Cells["Ma_NVL"].Value.ToString();
                string mkp = dgv_stockPDxn.CurrentRow.Cells["Maker_Part"].Value.ToString();
                string lot = dgv_stockPDxn.CurrentRow.Cells["Lot"].Value.ToString();
                while (sr.EndOfStream == false)
                {
                    string strRead = sr.ReadLine();
                    if (strRead.Contains(code) && strRead.Contains(mkp) && strRead.Contains(lot))
                    {
                        sw.WriteLine(strRead);
                    }
                }
                sw.Close();
                sr.Close();
                System.Threading.Thread.Sleep(500);
                frm.cfrm = true;
                frm.dP = str[2] + str[0] + str[1];
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Xảy ra lỗi!", "StockWaitPDxn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                   
        }
    }
}
