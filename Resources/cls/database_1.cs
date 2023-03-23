using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Collections;
using Spire.Xls;

namespace ManageMaterialPBA
{
    public class database_1
    {
        public string constr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = " + Application.StartupPath + @"\Database.mdb";
        public string user;       

        public DataTable getData(string str)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(str, constr);
            da.Fill(dt);

            return dt;
        }

        public void get_cbbModel(string dtb, string nacol, ComboBox cbbGetModel)
        {
            cbbGetModel.Items.Clear();
            string str = "select distinct " + nacol + " from " + dtb + ";";
            DataTable dt = new DataTable();
            dt = getData(str);

            // Add model items vào comboBox
            foreach (DataRow dr in dt.Rows)
            {
                cbbGetModel.Items.Add(dr.ItemArray[0].ToString());
            }
        }

        public void get_cbbData(string dtb, string colget, string coldk, string dk, ComboBox cbbGetModel)
        {
            cbbGetModel.Items.Clear();
            string str = "select distinct " + colget + " from " + dtb + " where " + coldk + " ='" + dk + "'";
            DataTable dt = new DataTable();
            dt = getData(str);

            // Add model items vào comboBox
            foreach (DataRow dr in dt.Rows)
            {
                cbbGetModel.Items.Add(dr.ItemArray[0].ToString());
            }
        }

        public void get_part(ComboBox cbbGetModel, string part)
        {
            cbbGetModel.Items.Clear();
            string str = "select Name_user from login where part = '" + part + "'";
            DataTable dt = new DataTable();
            dt = getData(str);

            // Add model items vào comboBox
            foreach (DataRow dr in dt.Rows)
            {
                cbbGetModel.Items.Add(dr.ItemArray[0].ToString());
            }
        }

        public int getData_qty(string model, string code, string temcode, string namCol, string dtb) // hàm lấy data từ sql
        {           
            string st = "Select " + namCol + " from " + dtb + " where Ma_NVL = '" + code + "' and Tem_code = '" + temcode + "'";
            DataTable dt = new DataTable();
            dt = getData(st);
            int ttol = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ttol = ttol + int.Parse(dr.ItemArray[0].ToString());              
            }

            return ttol;
        }

        public int getData_qty2(string code, string mkp, string Lot) // hàm lấy data từ sql
        {
            string st = "Select So_luong from Stock_KTZ where Ma_NVL = '" + code + "' and Maker_Part = '" + mkp + "' and Lot = '" + Lot + "'";
            DataTable dt = new DataTable();
            dt = getData(st);
            int ttol = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ttol = ttol + int.Parse(dr.ItemArray[0].ToString());
            }

            return ttol;
        }

        public int getData_qty2(string code, string mkp, string Lot, string temcode) // hàm lấy data từ sql
        {
            string st = "Select So_luong from Stock_KTZ4 where Ma_NVL = '" + code + "' and Maker_Part = '" + mkp + "' and Lot = '" + Lot + "' and FIFO ='" + temcode + "'";
            DataTable dt = new DataTable();
            dt = getData(st);
            int ttol = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ttol = ttol + int.Parse(dr.ItemArray[0].ToString());
            }

            return ttol;
        }

        public DataTable search_stock(string dtb, bool chk)
        {
            if(chk == true)
            {
                string str = "select * from " + dtb + " order by Ngay_thang DESC, Ma_NVL ASC";
                return getData(str);
            }
            else
            {
                string str = "select * from " + dtb + " order by Ngay_Thang ASC";
                return getData(str);
            }                       
        }

        public DataTable LoadBOM(string model)
        {
            string str = "select Line, Model, Mo_ta, Ma_NVL, Maker, Maker_Part, Diem_gan, So_luong from All_Model1 where Model = '" + model + "' and Su_dung = 'Yes'";
            DataTable dt = getData(str);
            return dt;
        }

        public DataTable LoadStockFIFO(string maNVL, string mkp, string lot, string fifo)
        {
            string str = "select * from Stock_KTZ4 where Ma_NVL = '" + maNVL + "' and Maker_Part = '" + mkp + "' and Lot = '" + lot + "' and FIFO = '" + fifo + "'";
            DataTable dt = getData(str);
            return dt;
        }

        public DataTable LoadDatabase(string dtb, string model, string date)
        {
            string str = "select * from " + dtb + " where Model = '" + model + "' and Ngay_thang = '" + date + "' order by Ma_NVL";
            return getData(str);
        }

        public DataTable LoadDatabase1(string dtb, string model, string date)
        {
            DataTable dt = new DataTable();
            string str = "select * from " + dtb + " where Model = '" + model + "' and Ngay_thang = '" + date + "' order by Ma_NVL";
            dt = getData(str);
            OleDbConnection cnn = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
            cnn.Open();  
            foreach(DataRow dtr in dt.Rows)
            {
                string strIn = "INSERT INTO PDxacnhanStock_1 VALUES ('" + dtr.ItemArray[0].ToString() + "','"
                                                                        + dtr.ItemArray[1].ToString() + "','"
                                                                        + dtr.ItemArray[2].ToString() + "','"
                                                                        + dtr.ItemArray[3].ToString() + "', '"
                                                                        + dtr.ItemArray[4].ToString() + "', '"
                                                                        + dtr.ItemArray[5].ToString() + "', '"
                                                                        + dtr.ItemArray[6].ToString() + "','"
                                                                        + dtr.ItemArray[7].ToString() + "' , '"
                                                                        + dtr.ItemArray[8].ToString() + "', '"
                                                                        + dtr.ItemArray[9].ToString() + "', '"
                                                                        + dtr.ItemArray[10].ToString() + "', '"
                                                                        + dtr.ItemArray[11].ToString() + "', '"
                                                                        + dtr.ItemArray[12].ToString() + "')";// Gán biến str bằng lệnh SQL
                OleDbCommand cmdIn = new OleDbCommand(strIn, cnn);// Khai báo và khởi tạo bộ nhớ biến cmd
                cmdIn.ExecuteNonQuery();   // thực hiện lênh SQL
            }
            cnn.Close();// Ngắt kết nối
            return dt;
        }

        public DataTable LoadDatabase(string dtb)
        {
            string str = "select * from " + dtb + " order by Ma_NVL";
            return getData(str);
        }

        public void delete_Transport(string dtb)
        {
            OleDbConnection cnn = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
            cnn.Open();   //mở kết nối
            string str = "DELETE FROM " + dtb + " ";// Gán biến str bằng lệnh SQL
            OleDbCommand cmd = new OleDbCommand(str, cnn);// Khai báo và khởi tạo bộ nhớ biến cmd
            cmd.ExecuteNonQuery();   // thực hiện lênh SQL
            cnn.Close();// Ngắt kết nối
        }

        public void insert_transKPv2(string stt, string date, string shift, string model, string mter, string cod, string mk, string mkp, string lot, string qtyInp, string ktz, string pd, string temcode)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            //int count;
            try
            {
               string str = string.Empty;
               str = "INSERT INTO Ktz_Pd_tranfer VALUES ('" + stt + "','" + date + "','" + shift + "','"
                        + "SMD" + "', '" + model + "', '" + mter + "', '" + cod + "','" + mk + "' , '" + mkp + "', '" + lot + "', '" + qtyInp + "', '" + temcode + "', '" + ktz + "', '" + pd + "')";
               OleDbCommand cmd = new OleDbCommand(str, cnn);
               cmd.ExecuteNonQuery();   
               cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi Input Data!", "In/Out Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public void del_stockLine_zero(string code, string mkp, string lot, string temCode)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            try
            {
                string str = string.Empty;

                str = "Delete * From KtzGiaoPd1 Where Ma_NVL = '" + code + "' and Maker_Part = '" + mkp + "' and Lot = '" + lot + "' And Tem_code='" + temCode + "'";
                OleDbCommand cmd = new OleDbCommand(str, cnn);
                cmd.ExecuteNonQuery();  
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void insert_trans_PdKtzv2(string stt, string date, string shift, string mol, string mter, string cod, string mk, string mkp, string lot, string pd, string ktz, string temcode)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            //int count;
            try
            {
                string str = string.Empty;
                str = "INSERT INTO PD_Ktz_tranfer VALUES ('" + stt + "','" + date + "','" + shift + "','"
                                                             + "SMD" + "', '" + mol + "', '" 
                                                             + mter + "', '" + cod + "','" 
                                                             + mk + "' , '" + mkp + "', '" 
                                                             + lot + "', '" + "" + "', '" 
                                                             + "" + "', '" + temcode + "','" 
                                                             + "" + "','" + "" + "','" 
                                                             + pd + "', '" + ktz + "')";
                OleDbCommand cmd = new OleDbCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi Input Data!", "In/Out Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        public bool insert_ktzGiaoPd_table2(DataGridView dgv, string date, string shift, string pd, string ktz, string model)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            try
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string str = "INSERT INTO PDxacnhanStock VALUES ( '" + date + "','" + 
                                                                               shift + "', '" + 
                                                                               "SMD" + "', '" + 
                                                                               model + "', '" + 
                                                                               dgr.Cells["Mo_ta"].Value.ToString() + "', '" + 
                                                                               dgr.Cells["Ma_NVL"].Value.ToString() + "', '" + 
                                                                               dgr.Cells["Maker"].Value.ToString() + "','" + 
                                                                               dgr.Cells["Maker_Part"].Value.ToString() + "', '" + 
                                                                               dgr.Cells["Diem_gan"].Value.ToString() + "', '" + 
                                                                               dgr.Cells["Lot"].Value.ToString() + "', '" + 
                                                                               dgr.Cells["So_luong_cap"].Value.ToString() + "', '" + 
                                                                               dgr.Cells["Tem_code"].Value.ToString() + "', '" +  
                                                                               ktz + "','" + 
                                                                               pd + "')";
                        OleDbCommand cmd = new OleDbCommand(str, cnn);
                        cmd.ExecuteNonQuery();
                    }                    
                }
                cnn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool insert_PDxn(DataGridView dgv, string date, string shift, string pd, string ktz, string model)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            try
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string str = "INSERT INTO KtzGiaoPd1 VALUES ( '" + date + "','" +
                                                                           shift + "', '" +
                                                                           "SMD" + "', '" +
                                                                           model + "', '" +
                                                                           dgr.Cells["Mo_ta"].Value.ToString() + "', '" +
                                                                           dgr.Cells["Ma_NVL"].Value.ToString() + "', '" +
                                                                           dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                           dgr.Cells["Maker_Part"].Value.ToString() + "', '" +
                                                                           dgr.Cells["Lot"].Value.ToString() + "', '" +
                                                                           dgr.Cells["So_luong_cap"].Value.ToString() + "', '" +
                                                                           dgr.Cells["Tem_code"].Value.ToString() + "', '" +
                                                                           ktz + "','" +
                                                                           pd + "')";
                        OleDbCommand cmd = new OleDbCommand(str, cnn);
                        cmd.ExecuteNonQuery();
                    }
                }
                cnn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }        

        public bool insert_PdReturn_table(DataGridView dgv, string date, string shift, string ktz, string pd, string model, DataGridView dgv1)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            try
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string str = "INSERT INTO Pd_ktz VALUES ( '" + date + "','" +
                                                                       shift + "', '" +
                                                                       "SMD" + "', '" +
                                                                       model + "', '" +
                                                                       dgr.Cells["Mo_ta"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Ma_NVL"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                       dgr.Cells["Maker_Part"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Lot"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Slg_tra_KTZ"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Slg_ton_Line"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Tem_code"].Value.ToString() + "', '" +
                                                                       dgr.Cells["Giai_thich"].Value.ToString() + "','" +
                                                                       dgr.Cells["Ghi_chu"].Value.ToString() + "','" +
                                                                       pd + "', '" +
                                                                       ktz + "')";
                        OleDbCommand cmd = new OleDbCommand(str, cnn);
                        cmd.ExecuteNonQuery();    
                    }                                   
                }
                cnn.Close();                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool checkPD_Ktz(DataGridView dgv)
        {
            int err = 0;
            bool chekQtInp1, chekQtInp2;
            int qtAct1, qtAct2;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                {
                    chekQtInp1 = int.TryParse(dgr.Cells["Slg_tra_KTZ"].Value.ToString(), out qtAct1);
                    chekQtInp2 = int.TryParse(dgr.Cells["Slg_ton_Line"].Value.ToString(), out qtAct2);
                    if (chekQtInp1 == false || qtAct1 < 0)
                    {
                        err++;
                        dgr.Cells["Slg_tra_KTZ"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Slg_tra_KTZ"].Style.BackColor = Color.White;
                    }
                    if (chekQtInp2 == false || qtAct2 < 0)
                    {
                        err++;
                        dgr.Cells["Slg_ton_Line"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Slg_ton_Line"].Style.BackColor = Color.White;
                    }
                    if (qtAct1 > 0 && qtAct2 > 0)
                    {
                        err++;
                        dgr.Cells["Slg_tra_KTZ"].Style.BackColor = Color.Red;
                        dgr.Cells["Slg_ton_Line"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgr.Cells["Slg_tra_KTZ"].Style.BackColor = Color.White;
                        dgr.Cells["Slg_ton_Line"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Slg_tra_KTZ"].Value.ToString() == "0" && dgr.Cells["Slg_ton_Line"].Value.ToString() != "0" && dgr.Cells["Giai_thich"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Giai_thich"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Giai_thich"].Style.BackColor = Color.White;
                    }
                    if ((dgr.Cells["Giai_thich"].Value.ToString() == "Khác") && (dgr.Cells["Ghi_chu"].Value.ToString() == ""))
                    {
                        err++;
                        dgr.Cells["Ghi_chu"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Ghi_chu"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Tem_code"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.White;
                    }           
                }                    
            }

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void show_ktzGiaoPd(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_stt = new DataGridViewTextBoxColumn();
            col_stt.DataPropertyName = "STT";
            col_stt.HeaderText = "STT";
            col_stt.Name = "STT";
            col_stt.ReadOnly = true;
            col_stt.Width = 50;
            col_stt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_stt);

            DataGridViewTextBoxColumn col_datemonth = new DataGridViewTextBoxColumn();
            col_datemonth.DataPropertyName = "Ngay_thang";
            col_datemonth.HeaderText = "Ngay_thang";
            col_datemonth.Name = "Ngay_thang";
            col_datemonth.ReadOnly = true;
            col_datemonth.Width = 100;
            col_datemonth.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_datemonth);

            DataGridViewTextBoxColumn col_shift = new DataGridViewTextBoxColumn();
            col_shift.DataPropertyName = "Ca_kip";
            col_shift.HeaderText = "Ca_kip";
            col_shift.Name = "Ca_kip";
            col_shift.ReadOnly = true;
            col_shift.Width = 50;
            col_shift.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_shift);

            DataGridViewTextBoxColumn col_line = new DataGridViewTextBoxColumn();
            col_line.DataPropertyName = "Line";
            col_line.HeaderText = "Line";
            col_line.Name = "Line";
            col_line.ReadOnly = true;
            col_line.Width = 50;
            col_line.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_line);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 120;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 140;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);          

            DataGridViewTextBoxColumn col_Lot = new DataGridViewTextBoxColumn();
            col_Lot.DataPropertyName = "Lot";
            col_Lot.HeaderText = "Lot";
            col_Lot.Name = "Lot";
            col_Lot.Width = 200;
            col_Lot.ReadOnly = true;
            col_Lot.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Lot);

            DataGridViewTextBoxColumn col_QtyInPut = new DataGridViewTextBoxColumn();
            col_QtyInPut.DataPropertyName = "So_luong_cap";
            col_QtyInPut.HeaderText = "So_luong_cap";
            col_QtyInPut.Name = "So_luong_cap";
            col_QtyInPut.ReadOnly = false;
            col_QtyInPut.Width = 120;
            col_QtyInPut.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_QtyInPut);

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.Width = 250;
            col_temCd.ReadOnly = true;
            dgv.Columns.Add(col_temCd);

            DataGridViewTextBoxColumn col_ktz = new DataGridViewTextBoxColumn();
            col_ktz.DataPropertyName = "KTZ";
            col_ktz.HeaderText = "KTZ";
            col_ktz.Name = "KTZ";
            col_ktz.ReadOnly = true;
            col_ktz.Width = 150;
            col_ktz.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ktz);

            DataGridViewTextBoxColumn col_pd = new DataGridViewTextBoxColumn();
            col_pd.DataPropertyName = "PD";
            col_pd.HeaderText = "PD";
            col_pd.Name = "PD";
            col_pd.ReadOnly = true;
            col_pd.Width = 150;
            col_pd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_pd);            

            dgv.DataSource = dt;
            dgv.ClearSelection();          
        }

        public void show_Pd_Ktz(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_stt = new DataGridViewTextBoxColumn();
            col_stt.DataPropertyName = "STT";
            col_stt.HeaderText = "STT";
            col_stt.Name = "STT";
            col_stt.ReadOnly = true;
            col_stt.Width = 50;
            col_stt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_stt);

            DataGridViewTextBoxColumn col_datemonth = new DataGridViewTextBoxColumn();
            col_datemonth.DataPropertyName = "Ngay_thang";
            col_datemonth.HeaderText = "Ngay_thang";
            col_datemonth.Name = "Ngay_thang";
            col_datemonth.ReadOnly = true;
            col_datemonth.Width = 100;
            col_datemonth.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_datemonth);

            DataGridViewTextBoxColumn col_shift = new DataGridViewTextBoxColumn();
            col_shift.DataPropertyName = "Ca_kip";
            col_shift.HeaderText = "Ca_kip";
            col_shift.Name = "Ca_kip";
            col_shift.ReadOnly = true;
            col_shift.Width = 50;
            col_shift.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_shift);

            DataGridViewTextBoxColumn col_line = new DataGridViewTextBoxColumn();
            col_line.DataPropertyName = "Line";
            col_line.HeaderText = "Line";
            col_line.Name = "Line";
            col_line.ReadOnly = true;
            col_line.Width = 50;
            col_line.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_line);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 120;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 140;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);

            DataGridViewTextBoxColumn col_Lot = new DataGridViewTextBoxColumn();          
            col_Lot.DataPropertyName = "Lot";
            col_Lot.HeaderText = "Lot";
            col_Lot.Name = "Lot";
            col_Lot.ReadOnly = true;
            col_Lot.Width = 140;
            col_Lot.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Lot);

            DataGridViewTextBoxColumn col_QtyReturn = new DataGridViewTextBoxColumn();
            col_QtyReturn.DataPropertyName = "Slg_tra_KTZ";
            col_QtyReturn.HeaderText = "Slg_tra_KTZ";
            col_QtyReturn.Name = "Slg_tra_KTZ";
            col_QtyReturn.ReadOnly = false;
            col_QtyReturn.Width = 100;
            col_QtyReturn.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_QtyReturn);

            DataGridViewTextBoxColumn col_QtyLine = new DataGridViewTextBoxColumn();
            col_QtyLine.DataPropertyName = "Slg_ton_Line";
            col_QtyLine.HeaderText = "Slg_ton_Line";
            col_QtyLine.Name = "Slg_ton_Line";
            col_QtyLine.ReadOnly = false;
            col_QtyLine.Width = 100;
            col_QtyLine.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_QtyLine);

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.ReadOnly = true;
            col_temCd.Width = 100;
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_temCd);          

            DataGridViewComboBoxColumn col_Remark = new DataGridViewComboBoxColumn();
            col_Remark.Items.AddRange(get_remark());
            col_Remark.FlatStyle = FlatStyle.Popup;
            col_Remark.DataPropertyName = "Giai_thich";
            col_Remark.HeaderText = "Giai_thich";
            col_Remark.Name = "Giai_thich";
            col_Remark.Width = 130;
            col_Remark.ReadOnly = false;
            col_Remark.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            dgv.Columns.Add(col_Remark);

            DataGridViewTextBoxColumn col_Remar2 = new DataGridViewTextBoxColumn();
            col_Remar2.DataPropertyName = "Ghi_chu";
            col_Remar2.HeaderText = "Ghi_chu";
            col_Remar2.Name = "Ghi_chu";
            col_Remar2.ReadOnly = false;
            col_Remar2.Width = 100;
            col_Remar2.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Remar2);            

            DataGridViewTextBoxColumn col_pd = new DataGridViewTextBoxColumn();
            col_pd.DataPropertyName = "PD";
            col_pd.HeaderText = "PD";
            col_pd.Name = "PD";
            col_pd.ReadOnly = true;
            col_pd.Width = 150;
            col_pd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_pd);

            DataGridViewTextBoxColumn col_ktz = new DataGridViewTextBoxColumn();
            col_ktz.DataPropertyName = "KTZ";
            col_ktz.HeaderText = "KTZ";
            col_ktz.Name = "KTZ";
            col_ktz.ReadOnly = true;
            col_ktz.Width = 150;
            col_ktz.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ktz);           

            dgv.DataSource = dt.DefaultView;
            dgv.ClearSelection();            
        }

        public string[] get_remark()
        {           
            string str_re = "Select Remark_Type From Pd_Ktz_Remark";
            DataTable dt = getData(str_re);
            string[] arr_rem = new string[dt.Rows.Count];
            int i = 0;
            foreach(DataRow dtr in dt.Rows)
            {
                arr_rem[i] = dtr.ItemArray[0].ToString();
                i++;
            }
            return arr_rem;
        }

        public DataTable loadtransportWH(string model)
        {
            string str = "select Line, Model, Mo_ta, Ma_NVL, Maker, Maker_Part, Diem_gan, So_luong, Maker_Part_xn from All_Model1 where Model = '" + model + "' And Su_dung = 'Yes'";
            DataTable dt = getData(str);
            return dt;
        }        

        public void insert_transOrderWH2v2(string stt, string  model, string material, string code, string mkr, string mkrp, string lot, string qty, string temCode, string nglay, int dong)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                //for (int i = 0; i < dong; i++)
                //{
                    string str = string.Empty;                              
                    str = "INSERT INTO OrderWH (STT, Model, Mo_ta, Ma_NVL, Maker, Maker_Part, Lot, So_luong_nhap, Tem_Code) VALUES ('" + stt + "','"
                                                                                                                                       + model + "','"
                                                                                                                                       + material + "','"
                                                                                                                                       + code + "','"
                                                                                                                                       + mkr + "','"
                                                                                                                                       + mkrp + "','"
                                                                                                                                       + lot + "','"
                                                                                                                                       + qty + "','"
                                                                                                                                       + temCode + "')";
                    OleDbCommand cmd = new OleDbCommand(str, cnn);
                    cmd.ExecuteNonQuery();
                //}                                   
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi Input Data!", "OrderWH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void insert_tranPDxn(string stt, string date, string shift, string line, string model, 
                                    string material, string code, string mkr, string mkrp, 
                                    string lot, string qty, string temCode, string ktz, string pd)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                string str = string.Empty;

                str = "INSERT INTO PDxacnhan VALUES ('" + stt + "','" 
                                                        + date + "','"
                                                        + shift + "','"
                                                        + line + "','"
                                                        + model + "','"
                                                        + material + "','"
                                                        + code + "','"
                                                        + mkr + "','"
                                                        + mkrp + "','"
                                                        + lot + "','"
                                                        + qty + "','"
                                                        + temCode + "','"
                                                        + ktz + "','"
                                                        + pd + "')";
                OleDbCommand cmd = new OleDbCommand(str, cnn);
                cmd.ExecuteNonQuery();

                cnn.Close();                    
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi Input Data!", "PDxacnhan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        public void insert_transReWH2v2(string stt, string mater, string code, string mkr, string mkrp, string lot, string qty, string temCode, string ngtra)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                string str = string.Empty;                
                str = "INSERT INTO ReturnWH (STT, Mo_ta, Ma_NVL, Maker, Maker_Part, Lot, So_luong_tra, Tem_code) VALUES ('" + stt + "','" + mater + "','" + code + "','" + mkr + "','" + mkrp + "','" + lot + "','" + qty + "','" + temCode + "')";
                OleDbCommand cmd = new OleDbCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi Input Data!", "ReturnWH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void insert_transOther(string stt, string date, string shif, string model, string mater, string code, string mkr, string mkrp, string lot, string qty, string temCode, string ktz, string ngOder)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                string str = string.Empty;
                str = "INSERT INTO KTZother VALUES ('" + stt + "','" 
                                                       + date + "','"
                                                       + shif + "','"
                                                       + "SMD" + "','"
                                                       + model + "','"
                                                       + mater + "','" 
                                                       + code + "','" 
                                                       + mkr + "','" 
                                                       + mkrp + "','" 
                                                       + lot + "','" 
                                                       + qty + "','" 
                                                       + temCode + "','"
                                                       + "" + "','"
                                                       + "" + "','"
                                                       + ktz + "','"
                                                       + ngOder + "')";
                OleDbCommand cmd = new OleDbCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi Input Data!", "KTZother", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
  
        public DataTable loadtransport_tableWH(string model, string tbl)
        {
            string str = string.Empty;
            str = "Select * from "+ tbl + " where Model = '" + model + "' order by STT";
            return getData(str);
        }

        public DataTable loadtransport_tableWH(string tbl)
        {
            string str = string.Empty;
            str = "Select * from " + tbl + " order by Ma_NVL";
            return getData(str);
        }  

        public void show_OrderWH(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_stt = new DataGridViewTextBoxColumn();
            col_stt.DataPropertyName = "STT";
            col_stt.HeaderText = "STT";
            col_stt.Name = "STT";
            col_stt.ReadOnly = true;
            col_stt.Width = 50;
            col_stt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_stt);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 80;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 100;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";          
            col_Code.ReadOnly = true;
            col_Code.Width = 110;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);         

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 130;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);            

            DataGridViewTextBoxColumn col_lOt = new DataGridViewTextBoxColumn();
            col_lOt.DataPropertyName = "Lot";
            col_lOt.HeaderText = "Lot";
            col_lOt.Name = "Lot";
            col_lOt.ReadOnly = false;
            col_lOt.Width = 130;
            col_lOt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_lOt);

            DataGridViewTextBoxColumn col_QtyInp = new DataGridViewTextBoxColumn();
            col_QtyInp.DataPropertyName = "So_luong_nhap";
            col_QtyInp.HeaderText = "So_luong_nhap";
            col_QtyInp.Name = "So_luong_nhap";
            col_QtyInp.ReadOnly = false;
            col_QtyInp.Width = 100;
            col_QtyInp.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_QtyInp);

            DataGridViewTextBoxColumn col_tc = new DataGridViewTextBoxColumn();
            col_tc.DataPropertyName = "Tem_Code";
            col_tc.HeaderText = "Tem_Code";
            col_tc.Name = "Tem_Code";
            col_tc.ReadOnly = true;
            col_tc.Width = 160;
            col_tc.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_tc);

            DataGridViewTextBoxColumn col_iqc = new DataGridViewTextBoxColumn();
            col_iqc.DataPropertyName = "IQC_test";
            col_iqc.HeaderText = "IQC_test";
            col_iqc.Name = "IQC_test";
            col_iqc.ReadOnly = false;
            col_iqc.Width = 80;
            col_iqc.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_iqc);           

            dgv.DataSource = dt;
            dgv.ClearSelection();           
        }      

        public void show_ReturnWH(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_stt = new DataGridViewTextBoxColumn();
            col_stt.DataPropertyName = "STT";
            col_stt.HeaderText = "STT";
            col_stt.Name = "STT";
            col_stt.ReadOnly = true;
            col_stt.Width = 50;
            col_stt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_stt);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 100;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);
           
            DataGridViewTextBoxColumn col_lOt = new DataGridViewTextBoxColumn();
            col_lOt.DataPropertyName = "Lot";
            col_lOt.HeaderText = "Lot";
            col_lOt.Name = "Lot";
            col_lOt.ReadOnly = true;
            col_lOt.Width = 150;
            col_lOt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_lOt);

            DataGridViewTextBoxColumn col_QtyRe = new DataGridViewTextBoxColumn();
            col_QtyRe.DataPropertyName = "So_luong_tra";
            col_QtyRe.HeaderText = "So_luong_tra";
            col_QtyRe.Name = "So_luong_tra";
            col_QtyRe.ReadOnly = false;
            col_QtyRe.Width = 100;
            col_QtyRe.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_QtyRe);

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.ReadOnly = true;
            col_temCd.Width = 100;
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_temCd);

            DataGridViewComboBoxColumn col_Remark = new DataGridViewComboBoxColumn();
            col_Remark.Items.AddRange(get_remark2());
            col_Remark.FlatStyle = FlatStyle.Popup;
            col_Remark.DataPropertyName = "Giai_thich";
            col_Remark.HeaderText = "Giai_thich";
            col_Remark.Name = "Giai_thich";
            col_Remark.Width = 150;
            col_Remark.ReadOnly = false;
            col_Remark.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            dgv.Columns.Add(col_Remark);

            DataGridViewTextBoxColumn col_other = new DataGridViewTextBoxColumn();
            col_other.DataPropertyName = "Ghi_chu";
            col_other.HeaderText = "Ghi_chu";
            col_other.Name = "Ghi_chu";
            col_other.ReadOnly = false;
            col_other.Width = 120;
            col_other.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_other);            

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        public void show_KTZother(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_stt = new DataGridViewTextBoxColumn();
            col_stt.DataPropertyName = "STT";
            col_stt.HeaderText = "STT";
            col_stt.Name = "STT";
            col_stt.ReadOnly = true;
            col_stt.Width = 50;
            col_stt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_stt);

            DataGridViewTextBoxColumn col_datemonth = new DataGridViewTextBoxColumn();
            col_datemonth.DataPropertyName = "Ngay_thang";
            col_datemonth.HeaderText = "Ngay_thang";
            col_datemonth.Name = "Ngay_thang";
            col_datemonth.ReadOnly = true;
            col_datemonth.Width = 80;
            col_datemonth.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_datemonth);

            DataGridViewTextBoxColumn col_shift = new DataGridViewTextBoxColumn();
            col_shift.DataPropertyName = "Ca_kip";
            col_shift.HeaderText = "Ca_kip";
            col_shift.Name = "Ca_kip";
            col_shift.ReadOnly = true;
            col_shift.Width = 50;
            col_shift.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_shift);

            DataGridViewTextBoxColumn col_line = new DataGridViewTextBoxColumn();
            col_line.DataPropertyName = "Line";
            col_line.HeaderText = "Line";
            col_line.Name = "Line";
            col_line.ReadOnly = true;
            col_line.Width = 50;
            col_line.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_line);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 80;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 80;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 80;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 120;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);

            DataGridViewTextBoxColumn col_Lot = new DataGridViewTextBoxColumn();
            col_Lot.DataPropertyName = "Lot";
            col_Lot.HeaderText = "Lot";
            col_Lot.Name = "Lot";
            col_Lot.Width = 150;
            col_Lot.ReadOnly = true;
            col_Lot.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Lot);

            DataGridViewTextBoxColumn col_QtyInPut = new DataGridViewTextBoxColumn();
            col_QtyInPut.DataPropertyName = "So_luong_cap";
            col_QtyInPut.HeaderText = "So_luong_cap";
            col_QtyInPut.Name = "So_luong_cap";
            col_QtyInPut.ReadOnly = false;
            col_QtyInPut.Width = 100;
            col_QtyInPut.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_QtyInPut);

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.Width = 150;
            col_temCd.ReadOnly = true;
            dgv.Columns.Add(col_temCd);

            DataGridViewComboBoxColumn col_Remark = new DataGridViewComboBoxColumn();
            col_Remark.Items.AddRange(get_remark());
            col_Remark.FlatStyle = FlatStyle.Popup;
            col_Remark.DataPropertyName = "Giai_thich";
            col_Remark.HeaderText = "Giai_thich";
            col_Remark.Name = "Giai_thich";
            col_Remark.Width = 120;
            col_Remark.ReadOnly = false;
            col_Remark.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Remark);

            DataGridViewTextBoxColumn col_gc = new DataGridViewTextBoxColumn();
            col_gc.DataPropertyName = "Ghi_chu";
            col_gc.HeaderText = "Ghi_chu";
            col_gc.Name = "Ghi_chu";
            col_gc.ReadOnly = false;
            col_gc.Width = 120;
            col_gc.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_gc);

            DataGridViewTextBoxColumn col_ktz = new DataGridViewTextBoxColumn();
            col_ktz.DataPropertyName = "KTZ";
            col_ktz.HeaderText = "KTZ";
            col_ktz.Name = "KTZ";
            col_ktz.ReadOnly = true;
            col_ktz.Width = 150;
            col_ktz.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ktz);

            DataGridViewTextBoxColumn col_ngor = new DataGridViewTextBoxColumn();
            col_ngor.DataPropertyName = "Nguoi_order";
            col_ngor.HeaderText = "Nguoi_order";
            col_ngor.Name = "Nguoi_order";
            col_ngor.ReadOnly = true;
            col_ngor.Width = 150;
            col_ngor.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ngor);

            dgv.DataSource = dt;
            dgv.ClearSelection();          
        }
       
        public string[] get_remark2()
        {
            string str_re = "Select Remark_Type From ReturnWH_Remark";
            DataTable dt = getData(str_re);
            string[] arr_rem = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dtr in dt.Rows)
            {
                arr_rem[i] = dtr.ItemArray[0].ToString();
                i++;
            }
            return arr_rem;
        }

        public bool checkWH_Ktz(DataGridView dgv)
        {
            int err = 0;
            bool chekQtInp;
            int qtAct;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                {
                    chekQtInp = int.TryParse(dgr.Cells["So_luong_nhap"].Value.ToString(), out qtAct);
                    if (chekQtInp == false)
                    {
                        err++;
                        dgr.Cells["So_luong_nhap"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["So_luong_nhap"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Lot"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Lot"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Lot"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Maker"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Maker"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Maker"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Maker_Part"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Maker_Part"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Maker_Part"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Tem_code"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Ma_NVL"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Ma_NVL"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Ma_NVL"].Style.BackColor = Color.White;
                    }
                }               
            }                     

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }              
        }

        public bool checkReWH(DataGridView dgv)
        {
            int err = 0;
            bool chekQtInp;
            int qtAct;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                {
                    string[] str = dgr.Cells["Tem_code"].Value.ToString().Split('+');
                    int tt_code = getData_qty2(dgr.Cells["Ma_NVL"].Value.ToString(), dgr.Cells["Maker_Part"].Value.ToString(), dgr.Cells["Lot"].Value.ToString(), str[1]);
                    //int tt = getData_qty2(dgr.Cells["Ma_NVL"].Value.ToString(), dgr.Cells["Maker_Part"].Value.ToString(), dgr.Cells["Lot"].Value.ToString());
                    chekQtInp = int.TryParse(dgr.Cells["So_luong_tra"].Value.ToString(), out qtAct);
                    if (chekQtInp == false ||tt_code != qtAct)
                    {
                        err++;
                        dgr.Cells["So_luong_tra"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["So_luong_tra"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Tem_code"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Giai_thich"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Giai_thich"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Giai_thich"].Style.BackColor = Color.White;
                    }
                    if ((dgr.Cells["Giai_thich"].Value.ToString() == "Khác") && (dgr.Cells["Ghi_chu"].Value.ToString() == ""))
                    {
                        err++;
                        dgr.Cells["Ghi_chu"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Ghi_chu"].Style.BackColor = Color.White;
                    }
                }                
            }

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkPDnhan(DataGridView dgv)
        {
            int err = 0;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                {
                    if (dgr.Cells["Ma_NVL"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Ma_NVL"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Ma_NVL"].Style.BackColor = Color.White;
                    }
                    //
                    if (dgr.Cells["Maker_Part"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Maker_Part"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Maker_Part"].Style.BackColor = Color.White;
                    }
                    //
                    if (dgr.Cells["Lot"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Lot"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Lot"].Style.BackColor = Color.White;
                    }
                    //
                    if (dgr.Cells["Tem_code"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.White;
                    }
                }                
            }

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkKtz_PD(DataGridView dgv)
        {
            int err = 0;
            bool chekQtInp;
            int qtAct;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                {
                    chekQtInp = int.TryParse(dgr.Cells["So_luong_cap"].Value.ToString(), out qtAct);
                    string[] str = dgr.Cells["Tem_code"].Value.ToString().Split('+');
                    int tt_code = getData_qty2(dgr.Cells["Ma_NVL"].Value.ToString(), dgr.Cells["Maker_Part"].Value.ToString(), dgr.Cells["Lot"].Value.ToString(), str[1]);
                    if (chekQtInp == false || tt_code != qtAct)
                    {
                        err++;
                        dgr.Cells["So_luong_cap"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["So_luong_cap"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Tem_code"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.White;
                    }       
                }                       
            }

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkKtz_other(DataGridView dgv)
        {
            int err = 0;
            bool chekQtInp;
            int qtAct;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                {
                    chekQtInp = int.TryParse(dgr.Cells["So_luong_cap"].Value.ToString(), out qtAct);
                    string[] str = dgr.Cells["Tem_code"].Value.ToString().Split('+');
                    int tt_code = getData_qty2(dgr.Cells["Ma_NVL"].Value.ToString(), dgr.Cells["Maker_Part"].Value.ToString(), dgr.Cells["Lot"].Value.ToString(), str[1]);
                    if (chekQtInp == false || tt_code != qtAct)
                    {
                        err++;
                        dgr.Cells["So_luong_cap"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["So_luong_cap"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Tem_code"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Tem_code"].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells["Giai_thich"].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells["Giai_thich"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Giai_thich"].Style.BackColor = Color.White;
                    }
                    if ((dgr.Cells["Giai_thich"].Value.ToString() == "Khác") && (dgr.Cells["Ghi_chu"].Value.ToString() == ""))
                    {
                        err++;
                        dgr.Cells["Ghi_chu"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //err = 0;
                        dgr.Cells["Ghi_chu"].Style.BackColor = Color.White;
                    }
                }
            }

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkNwBOM(DataGridView dgv)
        {
            int err = 0;
            bool chekQtInp1, chekQtInp2;
            int qtAct1, qtAct2;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells[0].Value != null && dgr.Cells[0].Value.ToString() != "")
                {
                    if (dgr.Index == 0)
                    {
                        continue;
                    }
                    if (dgr.Cells[0].Value != null)
                    {
                        chekQtInp1 = int.TryParse(dgr.Cells[5].Value.ToString(), out qtAct1);
                        chekQtInp2 = int.TryParse(dgr.Cells[9].Value.ToString(), out qtAct2);
                        if (chekQtInp1 == false)
                        {
                            err++;
                            dgr.Cells[5].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[5].Style.BackColor = Color.White;
                        }
                        if (chekQtInp2 == false)
                        {
                            err++;
                            dgr.Cells[9].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[9].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[8].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[8].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[8].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[6].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[6].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[6].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[7].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[7].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[7].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[10].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[10].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[10].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[4].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[4].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[4].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[2].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[2].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[2].Style.BackColor = Color.White;
                        }
                        if (dgr.Cells[3].Value.ToString() == "")
                        {
                            err++;
                            dgr.Cells[3].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgr.Cells[3].Style.BackColor = Color.White;
                        }
                    }              
                }               
            }

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkupBOM(DataGridView dgv, TextBox txt1, ComboBox cbx1)
        {
            int err = 0;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (dgr.Cells[0].Value != null && dgr.Cells[0].Value.ToString() != "")
                {
                    if (dgr.Cells[6].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells[6].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgr.Cells[6].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells[7].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells[7].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgr.Cells[7].Style.BackColor = Color.White;
                    }
                    if (dgr.Cells[10].Value.ToString() == "")
                    {
                        err++;
                        dgr.Cells[10].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgr.Cells[10].Style.BackColor = Color.White;
                    }
                }                
            }

            if(cbx1.Text == "")
            {
                err++;
                cbx1.BackColor = Color.Red;
            }
            else
            {
                cbx1.BackColor = Color.White;
                if(cbx1.Text == "Khác")
                {
                    if (txt1.Text == "")
                    {
                        err++;
                        txt1.BackColor = Color.Red;
                    }
                    else
                    {
                        txt1.BackColor = Color.White;
                    }
                }
            }         

            if (err != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        

        public bool upStokKtz(DataGridView dgv, string timUp)
        {
            try
            {
                if (dgv.Rows.Count != 0)
                {
                    //Biến xác nhận có Lot tồn tại trong stock
                    bool chekLot;
                    //Biến xác nhận đã update Qty
                    bool chekUp;
                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {
                        if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                        {
                            chekLot = false;
                            chekUp = false;
                            //Tạo dataTable để quét Code, Lot
                            string strSle = "Select Ma_NVL, Lot, So_luong From Stock_KTZ";
                            DataTable dtSle = getData(strSle);
                            foreach (DataRow dr in dtSle.Rows)
                            {
                                chekLot = false;
                                string t = dr[0].ToString();
                                if (dgr.Cells["Ma_NVL"].Value.ToString() == dr[0].ToString())
                                {
                                    if (dgr.Cells["Lot"].Value.ToString() == dr[1].ToString())
                                    {
                                        chekLot = true;
                                        int sumStk = int.Parse(dr[2].ToString());
                                        sumStk = sumStk + int.Parse(dgr.Cells["So_luong_nhap"].Value.ToString());
                                        OleDbConnection cnn1 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                                        cnn1.Open();   //mở kết nối                                                        
                                        string strUp = "Update Stock_KTZ Set So_luong = '" + sumStk + "', Thoi_gian = '" + timUp + "' Where Ma_NVL = '" + dgr.Cells["Ma_NVL"].Value.ToString() + "' and Lot = '" + dgr.Cells["Lot"].Value.ToString() + "'";
                                        OleDbCommand cmd1 = new OleDbCommand(strUp, cnn1);// Khai báo và khởi tạo bộ nhớ biến cmd
                                        cmd1.ExecuteNonQuery();
                                        cnn1.Close();
                                        //getData(strUp);
                                        chekUp = true;
                                    }
                                }
                            }
                            
                            //Add thêm dòng với Lot mới
                            if (chekLot == false && chekUp == false)
                            {
                                OleDbConnection cnn1 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                                cnn1.Open();   //mở kết nối 
                                string strIn = "Insert Into Stock_KTZ Values ('" + timUp + "','" +
                                                                                   dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                                   dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                                   dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                                   dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                                   dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                                   dgr.Cells["So_luong_nhap"].Value.ToString() + "')";
                                OleDbCommand cmd1 = new OleDbCommand(strIn, cnn1);// Khai báo và khởi tạo bộ nhớ biến cmd
                                cmd1.ExecuteNonQuery();
                                cnn1.Close();
                            }
                        }                        
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public bool upStokKtz2(DataGridView dgv, string timUp)
        {
            try
            {
                if (dgv.Rows.Count != 0)
                {
                    //Biến xác nhận có Lot tồn tại trong stock
                    bool chekLot;
                    //Biến xác nhận đã update Qty
                    bool chekUp;
                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {
                        if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                        {
                            if (int.Parse(dgr.Cells["Slg_tra_KTZ"].Value.ToString()) > 0)//slg tra KTZ > 0
                            {
                                chekLot = false;
                                chekUp = false;
                                //Tạo dataTable để quét Code, Lot
                                string strSle = "Select Ma_NVL, Lot, So_luong From Stock_KTZ";
                                DataTable dtSle = getData(strSle);
                                foreach (DataRow dr in dtSle.Rows)
                                {
                                    chekLot = false;
                                    string t = dr[0].ToString();
                                    if (dgr.Cells["Ma_NVL"].Value.ToString() == dr[0].ToString())
                                    {
                                        if (dgr.Cells["Lot"].Value.ToString() == dr[1].ToString())
                                        {
                                            chekLot = true;
                                            int sumStk = int.Parse(dr[2].ToString());
                                            sumStk = sumStk + int.Parse(dgr.Cells["Slg_tra_KTZ"].Value.ToString());
                                            OleDbConnection cnn1 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                                            cnn1.Open();   //mở kết nối                                                        
                                            string strUp = "Update Stock_KTZ Set So_luong = '" + sumStk + "' Where Ma_NVL = '" + dgr.Cells["Ma_NVL"].Value.ToString() + "' and Lot = '" + dgr.Cells["Lot"].Value.ToString() + "'";
                                            OleDbCommand cmd1 = new OleDbCommand(strUp, cnn1);// Khai báo và khởi tạo bộ nhớ biến cmd
                                            cmd1.ExecuteNonQuery();
                                            cnn1.Close();
                                            //getData(strUp);
                                            chekUp = true;
                                        }
                                    }
                                }                               

                                //Add thêm dòng với Lot mới
                                if (chekLot == false && chekUp == false)
                                {
                                    string strIn = "Insert Into Stock_KTZ Values ('" + timUp + "','" +
                                                                                       dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                                       dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                                       dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                                       dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                                       dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                                       dgr.Cells["Slg_tra_KTZ"].Value.ToString() + "')";
                                    getData(strIn);
                                }
                            }
                        }                       
                    }
                }
                return true;
            }
            catch (Exception)
            {               
              return false;
            }            
        }       

        public void upStkKtz2(DataGridView dgv)
        {
            if(dgv.Rows.Count != 0)
            {
                //List code đang có trong stock
                string strSlStK = "Select * From Stock_KTZ order by code, maker";
                DataTable dtStK = getData(strSlStK);

                //DataTable lưu thông tin sau khi đã cộng Stock
                DataTable dtFilInf = new DataTable();
                dtFilInf.Columns.Add("Time_Update");
                dtFilInf.Columns.Add("Material");
                dtFilInf.Columns.Add("Code");
                dtFilInf.Columns.Add("Maker");
                dtFilInf.Columns.Add("Maker_Part");
                dtFilInf.Columns.Add("Qty_Maker");
                dtFilInf.Columns.Add("Qty_Code");

                //Biến trùng code only 1 maker
                bool olyMker = true;
                //Biến same maker
                int count_samMaker = 0;
                //Biến qty theo code
                int qty_code = 0;
                //Biến báo chuyển code khác
                bool next_code = false;
                //Biến qty theo maker
                int[] qt_mker = new int[dtStK.Rows.Count];
                int sam_mk = 0;

                int[] qt_mkerOly = new int[dtStK.Rows.Count];
                int sig_mk = 0;

                for (int i = 0; i <= dtStK.Rows.Count - 1; )
                {
                    count_samMaker = 0;
                    olyMker = true;
                    int qt_rezo = int.Parse(dtStK.Rows[i].ItemArray[6].ToString());

                    for (int j = i + 1; j < dtStK.Rows.Count; j++)
                    {
                        if (dtStK.Rows[i].ItemArray[2].ToString() == dtStK.Rows[j].ItemArray[2].ToString())//trùng code
                        {
                            if (dtStK.Rows[i].ItemArray[3].ToString() == dtStK.Rows[j].ItemArray[3].ToString())//trùng maker
                            {
                                olyMker = true;
                                if (count_samMaker == 0)
                                {
                                    qt_mker[sam_mk] = qt_rezo + int.Parse(dtStK.Rows[j].ItemArray[6].ToString());
                                }
                                else
                                {
                                    qt_mker[sam_mk] = qt_mker[sam_mk] + int.Parse(dtStK.Rows[j].ItemArray[6].ToString());
                                }
                                count_samMaker++;
                            }
                            else//khác maker, có 2 maker trở lên
                            {
                                olyMker = false;
                            }
                        }
                        else
                        {
                            next_code = true;
                            break;
                        }
                    }

                    if (count_samMaker == 0)
                    {
                        //Insert trùng code only 1 maker
                        OleDbConnection cnn1 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                        cnn1.Open();   //mở kết nối
                        string str1 = "Insert Into Stock_KTZ2 (Time_Update, Material, Code, Maker, Maker_Part, Qty_Maker) Values ('" + dtStK.Rows[i].ItemArray[0].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[1].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[2].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[3].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[4].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[6].ToString() + "')";
                        OleDbCommand cmd1 = new OleDbCommand(str1, cnn1);// Khai báo và khởi tạo bộ nhớ biến cmd
                        cmd1.ExecuteNonQuery();
                        cnn1.Close();
                        qt_mkerOly[sig_mk] = int.Parse(dtStK.Rows[i].ItemArray[6].ToString());
                        sig_mk++;
                        i++;
                    }
                    if (count_samMaker != 0)
                    {
                        //Insert trùng code only 1 maker
                        OleDbConnection cnn2 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                        cnn2.Open();   //mở kết nối
                        string str2 = "Insert Into Stock_KTZ2 (Time_Update, Material, Code, Maker, Maker_Part, Qty_Maker) Values ('" + dtStK.Rows[i].ItemArray[0].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[1].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[2].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[3].ToString() + "','" +
                                                                                                                                       dtStK.Rows[i].ItemArray[4].ToString() + "','" +
                                                                                                                                       qt_mker[sam_mk].ToString() + "')";
                        OleDbCommand cmd2 = new OleDbCommand(str2, cnn2);// Khai báo và khởi tạo bộ nhớ biến cmd
                        cmd2.ExecuteNonQuery();
                        cnn2.Close();
                        sam_mk++;
                        i = i + count_samMaker + 1;
                    }
                    if ((next_code == true && olyMker == true) || (i == dtStK.Rows.Count))
                    {
                        int qt_mkerTtOly = 0;
                        for (int k = 0; k <= sig_mk - 1; k++)
                        {
                            qt_mkerTtOly = qt_mkerTtOly + qt_mkerOly[k];
                        }

                        int qt_mkerTtsam = 0;
                        for (int h = 0; h <= sam_mk; h++)
                        {
                            qt_mkerTtsam = qt_mkerTtsam + qt_mker[h];
                        }
                        qty_code = qt_mkerTtsam + qt_mkerTtOly;

                        OleDbConnection cnn3 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                        cnn3.Open();   //mở kết nối

                        string str3 = "Update Stock_KTZ2 Set Qty_Code ='" + qty_code.ToString() + "' Where Code ='" + dtStK.Rows[i - 1].ItemArray[2].ToString() + "'";

                        OleDbCommand cmd3 = new OleDbCommand(str3, cnn3);// Khai báo và khởi tạo bộ nhớ biến cmd
                        cmd3.ExecuteNonQuery();

                        cnn3.Close();
                        Array.Clear(qt_mker, 0, dtStK.Rows.Count);
                        Array.Clear(qt_mkerOly, 0, dtStK.Rows.Count);
                        sam_mk = 0;
                        sig_mk = 0;
                        qty_code = 0;
                        next_code = false;
                    }
                } 
                
                //  
                string strSelectMaker = "Select distinct Maker From Stock_KTZ";
                DataTable dtMaker = getData(strSelectMaker);
                int[] qtyMaker = new int[dtMaker.Rows.Count];
                for (int n = 0; n < dtMaker.Rows.Count; n++)
                {
                    qtyMaker[n] = 0;
                    for (int m = 0; m < dtStK.Rows.Count; m ++)
                    {
                        if(dtMaker.Rows[n].ItemArray[0].ToString() == dtStK.Rows[m].ItemArray[3].ToString())
                        {
                            qtyMaker[n] = qtyMaker[n] + int.Parse(dtStK.Rows[m].ItemArray[6].ToString());
                        }
                    }

                    OleDbConnection cnn4 = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
                    cnn4.Open();   //mở kết nối

                    string str4 = "Update Stock_KTZ2 Set Qty_Maker ='" + qtyMaker[n].ToString() + "' Where Maker ='" + dtMaker.Rows[n].ItemArray[0].ToString() + "'";

                    OleDbCommand cmd4 = new OleDbCommand(str4, cnn4);// Khai báo và khởi tạo bộ nhớ biến cmd
                    cmd4.ExecuteNonQuery();

                    cnn4.Close();
                }
            }            
        }

        public string[] Reduce_StokKtz2(DataGridView dgv, string timUp, string col_code, string col_lot, string col_qty, string cs, DataTable dtS, string pic, string process, string date, string shift, string model, string other)
        {
            if (dgv.Rows.Count != 0)
            {                
                //Biến xác nhận có Lot tồn tại trong stock
                bool chekLot;
                //Biến xác nhận đã update Qty
                bool chekUp;
                //Tạo dataTable để quét Code, Lot
                string strSle = "Select Ma_NVL, Lot, So_luong From Stock_KTZ";
                DataTable dtSle = getData(strSle);
                //Update history step 1
                #region
                var nvls = new List<NVL>() { };
                StreamReader sr = new StreamReader(@Application.StartupPath + "\\History\\HistoryNVL.txt");
                while (sr.EndOfStream == false)
                {
                    string[] str = sr.ReadLine().Split('|');
                    if (str.Length == 20)
                    {
                        nvls.Add(new NVL
                        {
                            model = str[0],
                            codeNVL = str[1],
                            maker = str[2],
                            mkerPart = str[3],
                            lot = str[4],
                            temCode = str[5],
                            ngInTemCode = str[6],
                            tgianInTemCode = str[7],
                            ngNhapKho = str[8],
                            tgianNhapKho = str[9],
                            ngCapNVL = str[10],
                            tgianCapNVL = str[11],
                            PDxacnhan = str[12],
                            tgianxacnhan = str[13],
                            ngTraNVL = str[14],
                            tgianTraNVL = str[15],
                            ghiChuTra = str[16],
                            ngTraWH = str[17],
                            tgianTraWH = str[18],
                            ghiChuTraWH = str[19]
                        });
                    }
                }
                sr.Close();
                #endregion
                //Tạo array return
                string[] arrRet = new string[dtSle.Rows.Count];
                int i = 0;
                for (int iR = 0; iR < dgv.RowCount; iR++)
                {
                    if (dgv.Rows[iR].Cells["Mo_ta"].Value != null && dgv.Rows[iR].Cells["Mo_ta"].Value.ToString() != "")
                    {
                        chekLot = false;
                        chekUp = false;
                        foreach (DataRow dr in dtSle.Rows)
                        {
                            chekLot = false;
                            //string t = dr[0].ToString();
                            if (dgv.Rows[iR].Cells[col_code].Value.ToString() == dr[0].ToString())//trùng code
                            {
                                if (dgv.Rows[iR].Cells[col_lot].Value.ToString() == dr[1].ToString())//trùng lot
                                {
                                    chekLot = true;
                                    int sumStk = int.Parse(dr[2].ToString());
                                    sumStk = sumStk - int.Parse(dgv.Rows[iR].Cells[col_qty].Value.ToString());//update qty
                                    if (sumStk < 0)
                                    {
                                        MessageBox.Show("Stock của Ma NVL " + dgv.Rows[iR].Cells[col_code].Value.ToString() + ", Lot " + dgv.Rows[iR].Cells[col_lot].Value.ToString() + " bị âm (" + sumStk.ToString() + "). Kiểm tra lại!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        arrRet[i] = dgv.Rows[iR].Cells[col_code].Value.ToString();
                                        arrRet[i + 1] = dgv.Rows[iR].Cells[col_lot].Value.ToString();
                                        i = i + 2;
                                        break;
                                    }
                                    else
                                    {
                                        //Update database
                                        //Opne connect database
                                        OleDbConnection cnn1 = new OleDbConnection(constr);
                                        cnn1.Open();
                                        string strUp = "Update Stock_KTZ Set So_luong = '" + sumStk + "' Where Ma_NVL = '" + dgv.Rows[iR].Cells[col_code].Value.ToString() + "' and Lot = '" + dgv.Rows[iR].Cells[col_lot].Value.ToString() + "'";
                                        OleDbCommand cmdUp = new OleDbCommand(strUp, cnn1);
                                        cmdUp.ExecuteNonQuery();
                                        cnn1.Close();
                                        //Select
                                        string strSle1 = "Select Ma_NVL, Lot, So_luong From Stock_KTZ";
                                        dtSle = getData(strSle1);
                                        chekUp = true;
                                        //Update hisstory step 2
                                        string temCode = dgv.Rows[iR].Cells["Tem_code"].Value.ToString();
                                        if (process == "KTZ-PD")
                                        {
                                            foreach (var nn in nvls.Where(x => x.temCode == temCode))
                                            {
                                                nn.ngCapNVL = pic;
                                                nn.tgianCapNVL = DateTime.Now.ToString();
                                            }
                                            //Update logfile
                                            try
                                            {
                                                string strIns = "INSERT INTO PDxacnhanStock VALUES ( '" + date + "','" +
                                                                                                          shift + "', '" +
                                                                                                          "SMD" + "', '" +
                                                                                                          model + "', '" +
                                                                                                          dgv.Rows[iR].Cells["Mo_ta"].Value.ToString() + "', '" +
                                                                                                          dgv.Rows[iR].Cells["Ma_NVL"].Value.ToString() + "', '" +
                                                                                                          dgv.Rows[iR].Cells["Maker"].Value.ToString() + "','" +
                                                                                                          dgv.Rows[iR].Cells["Maker_Part"].Value.ToString() + "', '" +
                                                                                                          dgv.Rows[iR].Cells["Lot"].Value.ToString() + "', '" +
                                                                                                          dgv.Rows[iR].Cells["So_luong_cap"].Value.ToString() + "', '" +
                                                                                                          dgv.Rows[iR].Cells["Tem_code"].Value.ToString() + "', '" +
                                                                                                          pic + "','" +
                                                                                                          other + "')";
                                                //Opne connect database
                                                OleDbConnection cnn2 = new OleDbConnection(constr);
                                                cnn2.Open();
                                                OleDbCommand cmdIns = new OleDbCommand(strIns, cnn2);
                                                cmdIns.ExecuteNonQuery();
                                                cnn2.Close();
                                            }
                                            catch (Exception)
                                            {
                                                MessageBox.Show("Xảy ra lỗi cập nhật Database PDxacnhanStock!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else if (process == "KTZ-WH")
                                        {
                                            string ghiChu = string.Empty;
                                            //if (dgv.Rows[iR].Cells["Giai_thich"].Value.ToString() == "NVL holding")
                                            //{
                                            //    ghiChu = dgv.Rows[iR].Cells["Giai_thich"].Value.ToString();
                                            //    savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                            //    //xoa txt holding
                                            //    //del_filLog("NVL_Holding", dgv.Rows[iR].Cells["Tem_code"].Value.ToString(), 1);
                                            //}
                                            if (dgv.Rows[iR].Cells["Giai_thich"].Value.ToString() == "Khác")
                                            {
                                                ghiChu = dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString();

                                                //if ((dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString().Contains("Hold"))
                                                //|| (dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString().Contains("hold"))
                                                //|| (dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString().Contains("HOLD")))
                                                //{
                                                //    //xoa txt holding
                                                //    //del_filLog("NVL_Holding",dgv.Rows[iR].Cells["Tem_code"].Value.ToString(), 1);
                                                //    savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                                //}
                                                //else
                                                //{
                                                    //savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                                //}
                                            }
                                            else
                                            {
                                                ghiChu = dgv.Rows[iR].Cells["Giai_thich"].Value.ToString();
                                                //savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                            }

                                            foreach (var nn in nvls.Where(x => x.temCode == temCode))
                                            {
                                                nn.ngTraWH = pic;
                                                nn.tgianTraWH = DateTime.Now.ToString();
                                                nn.ghiChuTraWH = ghiChu;
                                            }
                                            //Update logfile
                                            try
                                            {
                                                string strIns = "INSERT INTO ReturnWH_Logfile VALUES ( '" + dgv.Rows[iR].Cells["Mo_ta"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Ma_NVL"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Maker"].Value.ToString() + "','" +
                                                                                                            dgv.Rows[iR].Cells["Maker_Part"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Lot"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["So_luong_tra"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Tem_code"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Giai_thich"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString() + "', '" +
                                                                                                            pic + "')";
                                                //Opne connect database
                                                OleDbConnection cnn3 = new OleDbConnection(constr);
                                                cnn3.Open();
                                                OleDbCommand cmdIns = new OleDbCommand(strIns, cnn3);
                                                cmdIns.ExecuteNonQuery();
                                                cnn3.Close();
                                            }
                                            catch (Exception)
                                            {
                                                MessageBox.Show("Xảy ra lỗi cập nhật Database ReturnWH_Logfile!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Xảy ra lỗi cập nhật history NVL!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        //Xoa Data dã update OK
                                        DataRow dtr = dtS.Rows[iR];
                                        dtS.Rows.Remove(dtr);
                                        iR--;
                                        break;
                                    }
                                }
                            }
                        }
                        //Khong tim thay Lot da nhap
                        if (chekLot == false && chekUp == false)
                        {
                            MessageBox.Show("Mã NVL " + dgv.Rows[iR].Cells[col_code].Value.ToString() + " không có Lot " + dgv.Rows[iR].Cells[col_lot].Value.ToString() + " trong Stock!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //dgv.CurrentCell = dgv.Rows[iR].Cells[col_lot];
                            //dgv.BeginEdit(true);                            
                        }
                    }
                }
                //Update history step 3
                #region
                FileStream fs = new FileStream(@Application.StartupPath + "\\History\\HistoryNVL.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                foreach (var item in nvls)
                {
                    sw.WriteLine(item.model + "|" +
                                 item.codeNVL + "|" +
                                 item.maker + "|" +
                                 item.mkerPart + "|" +
                                 item.lot + "|" +
                                 item.temCode + "|" +
                                 item.ngInTemCode + "|" +
                                 item.tgianInTemCode + "|" +
                                 item.ngNhapKho + "|" +
                                 item.tgianNhapKho + "|" +
                                 item.ngCapNVL + "|" +
                                 item.tgianCapNVL + "|" +
                                 item.PDxacnhan + "|" +
                                 item.tgianxacnhan + "|" +
                                 item.ngTraNVL + "|" +
                                 item.tgianTraNVL + "|" +
                                 item.ghiChuTra + "|" +
                                 item.ngTraWH + "|" +
                                 item.tgianTraWH + "|" +
                                 item.ghiChuTraWH);
                }
                sw.Close();
                fs.Close();
                #endregion
                return arrRet;
            }
            else
            {
                return new string[] {"error"};
            }
        }

        public string[] Reduce_StokKtz3(DataGridView dgv, string timUp, string col_code, string col_lot, string col_qty, string cs, DataTable dtS, string pic, string date, string shift, string model, string ngOr)
        {
            if (dgv.Rows.Count != 0)
            {                
                //Biến xác nhận có Lot tồn tại trong stock
                bool chekLot;
                //Biến xác nhận đã update Qty
                bool chekUp;
                //Tạo dataTable để quét Code, Lot
                string strSle = "Select Ma_NVL, Lot, So_luong From Stock_KTZ";
                DataTable dtSle = getData(strSle);
                //Update history step 1
                #region
                //var nvls = new List<NVL>() { };
                //StreamReader sr = new StreamReader(@Application.StartupPath + "\\History\\HistoryNVL.txt");
                //while (sr.EndOfStream == false)
                //{
                //    string[] str = sr.ReadLine().Split('|');
                //    if (str.Length == 20)
                //    {
                //        nvls.Add(new NVL
                //        {
                //            model = str[0],
                //            codeNVL = str[1],
                //            maker = str[2],
                //            mkerPart = str[3],
                //            lot = str[4],
                //            temCode = str[5],
                //            ngInTemCode = str[6],
                //            tgianInTemCode = str[7],
                //            ngNhapKho = str[8],
                //            tgianNhapKho = str[9],
                //            ngCapNVL = str[10],
                //            tgianCapNVL = str[11],
                //            PDxacnhan = str[12],
                //            tgianxacnhan = str[13],
                //            ngTraNVL = str[14],
                //            tgianTraNVL = str[15],
                //            ghiChuTra = str[16],
                //            ngTraWH = str[17],
                //            tgianTraWH = str[18],
                //            ghiChuTraWH = str[19]
                //        });
                //    }
                //}
                //sr.Close();
                #endregion
                //Tạo array return
                string[] arrRet = new string[dtSle.Rows.Count];
                int i = 0;
                for (int iR = 0; iR < dgv.RowCount; iR++)
                {
                    if (dgv.Rows[iR].Cells["Mo_ta"].Value != null && dgv.Rows[iR].Cells["Mo_ta"].Value.ToString() != "")
                    {
                        chekLot = false;
                        chekUp = false;
                        foreach (DataRow dr in dtSle.Rows)
                        {
                            chekLot = false;
                            //string t = dr[0].ToString();
                            if (dgv.Rows[iR].Cells[col_code].Value.ToString() == dr[0].ToString())//trùng code
                            {
                                if (dgv.Rows[iR].Cells[col_lot].Value.ToString() == dr[1].ToString())//trùng lot
                                {
                                    chekLot = true;
                                    int sumStk = int.Parse(dr[2].ToString());
                                    sumStk = sumStk - int.Parse(dgv.Rows[iR].Cells[col_qty].Value.ToString());//update qty
                                    if (sumStk < 0)
                                    {
                                        MessageBox.Show("Stock của Ma NVL " + dgv.Rows[iR].Cells[col_code].Value.ToString() + ", Lot " + dgv.Rows[iR].Cells[col_lot].Value.ToString() + " bị âm (" + sumStk.ToString() + "). Kiểm tra lại!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        arrRet[i] = dgv.Rows[iR].Cells[col_code].Value.ToString();
                                        arrRet[i + 1] = dgv.Rows[iR].Cells[col_lot].Value.ToString();
                                        i = i + 2;
                                        break;
                                    }
                                    else
                                    {
                                        //Update database
                                        //Opne connect database
                                        OleDbConnection cnn1 = new OleDbConnection(constr);
                                        cnn1.Open();
                                        string strUp = "Update Stock_KTZ Set So_luong = '" + sumStk + "' Where Ma_NVL = '" + dgv.Rows[iR].Cells[col_code].Value.ToString() + "' and Lot = '" + dgv.Rows[iR].Cells[col_lot].Value.ToString() + "'";
                                        OleDbCommand cmdUp = new OleDbCommand(strUp, cnn1);
                                        cmdUp.ExecuteNonQuery();
                                        cnn1.Close();
                                        //Select
                                        string strSle1 = "Select Ma_NVL, Lot, So_luong From Stock_KTZ";
                                        dtSle = getData(strSle1);
                                        chekUp = true;
                                        //Update hisstory step 2
                                        //string temCode = dgv.Rows[iR].Cells["Tem_code"].Value.ToString();
                                        //if (process == "KTZ-PD")
                                        //{
                                        //    foreach (var nn in nvls.Where(x => x.temCode == temCode))
                                        //    {
                                        //        nn.ngCapNVL = pic;
                                        //        nn.tgianCapNVL = DateTime.Now.ToString();
                                        //    }
                                        //    //Update logfile
                                        //    try
                                        //    {
                                        //        string strIns = "INSERT INTO PDxacnhanStock VALUES ( '" + date + "','" +
                                        //                                                                  shift + "', '" +
                                        //                                                                  "SMD" + "', '" +
                                        //                                                                  model + "', '" +
                                        //                                                                  dgv.Rows[iR].Cells["Mo_ta"].Value.ToString() + "', '" +
                                        //                                                                  dgv.Rows[iR].Cells["Ma_NVL"].Value.ToString() + "', '" +
                                        //                                                                  dgv.Rows[iR].Cells["Maker"].Value.ToString() + "','" +
                                        //                                                                  dgv.Rows[iR].Cells["Maker_Part"].Value.ToString() + "', '" +
                                        //                                                                  dgv.Rows[iR].Cells["Lot"].Value.ToString() + "', '" +
                                        //                                                                  dgv.Rows[iR].Cells["So_luong_cap"].Value.ToString() + "', '" +
                                        //                                                                  dgv.Rows[iR].Cells["Tem_code"].Value.ToString() + "', '" +
                                        //                                                                  pic + "','" +
                                        //                                                                  other + "')";
                                        //        OleDbCommand cmdIns = new OleDbCommand(strIns, cnn);
                                        //        cmdIns.ExecuteNonQuery();
                                        //    }
                                        //    catch (Exception)
                                        //    {
                                        //        MessageBox.Show("Xảy ra lỗi cập nhật Database PDxacnhanStock!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //    }
                                        //}
                                        //else if (process == "KTZ-WH")
                                        //{
                                        //    string ghiChu = string.Empty;
                                        //    //if (dgv.Rows[iR].Cells["Giai_thich"].Value.ToString() == "NVL holding")
                                        //    //{
                                        //    //    ghiChu = dgv.Rows[iR].Cells["Giai_thich"].Value.ToString();
                                        //    //    savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                        //    //    //xoa txt holding
                                        //    //    //del_filLog("NVL_Holding", dgv.Rows[iR].Cells["Tem_code"].Value.ToString(), 1);
                                        //    //}
                                        //    if (dgv.Rows[iR].Cells["Giai_thich"].Value.ToString() == "Khác")
                                        //    {
                                        //        ghiChu = dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString();

                                        //        //if ((dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString().Contains("Hold"))
                                        //        //|| (dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString().Contains("hold"))
                                        //        //|| (dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString().Contains("HOLD")))
                                        //        //{
                                        //        //    //xoa txt holding
                                        //        //    //del_filLog("NVL_Holding",dgv.Rows[iR].Cells["Tem_code"].Value.ToString(), 1);
                                        //        //    savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                        //        //}
                                        //        //else
                                        //        //{
                                        //        savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                        //        //}
                                        //    }
                                        //    else
                                        //    {
                                        //        ghiChu = dgv.Rows[iR].Cells["Giai_thich"].Value.ToString();
                                        //        savInput2(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                        //    }

                                        //    foreach (var nn in nvls.Where(x => x.temCode == temCode))
                                        //    {
                                        //        nn.ngTraWH = pic;
                                        //        nn.tgianTraWH = DateTime.Now.ToString();
                                        //        nn.ghiChuTraWH = ghiChu;
                                        //    }
                                        //savInput3(dgv.Rows[iR].Cells["Tem_code"].Value.ToString());
                                            //Update logfile
                                            try
                                            {
                                                string strIns = "INSERT INTO KTZother_Logfile VALUES ( '" + date + "','" +
                                                                                                            shift + "','" +
                                                                                                            "SMD" + "','" +
                                                                                                            model + "','" +
                                                                                                            dgv.Rows[iR].Cells["Mo_ta"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Ma_NVL"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Maker"].Value.ToString() + "','" +
                                                                                                            dgv.Rows[iR].Cells["Maker_Part"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Lot"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["So_luong_cap"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Tem_code"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Giai_thich"].Value.ToString() + "', '" +
                                                                                                            dgv.Rows[iR].Cells["Ghi_chu"].Value.ToString() + "', '" +
                                                                                                            pic + "','" +
                                                                                                            ngOr + "')";
                                                //Opne connect database
                                                OleDbConnection cnn2 = new OleDbConnection(constr);
                                                cnn2.Open();
                                                OleDbCommand cmdIns = new OleDbCommand(strIns, cnn2);
                                                cmdIns.ExecuteNonQuery();
                                                cnn2.Close();
                                            }
                                            catch (Exception)
                                            {
                                                MessageBox.Show("Xảy ra lỗi cập nhật Database KTZother_Logfile!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("Xảy ra lỗi cập nhật history NVL!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //}
                                        //Xoa Data dã update OK
                                        DataRow dtr = dtS.Rows[iR];
                                        dtS.Rows.Remove(dtr);
                                        iR--;
                                        break;
                                    }
                                }
                            }
                        }
                        //Khong tim thay Lot da nhap
                        if (chekLot == false && chekUp == false)
                        {
                            MessageBox.Show("Mã NVL " + dgv.Rows[iR].Cells[col_code].Value.ToString() + " không có Lot " + dgv.Rows[iR].Cells[col_lot].Value.ToString() + " trong Stock!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //dgv.CurrentCell = dgv.Rows[iR].Cells[col_lot];
                            //dgv.BeginEdit(true);
                        }
                    }
                }
                //Update history step 3
                #region
                //FileStream fs = new FileStream(@Application.StartupPath + "\\History\\HistoryNVL.txt", FileMode.Create);
                //StreamWriter sw = new StreamWriter(fs);
                //foreach (var item in nvls)
                //{
                //    sw.WriteLine(item.model + "|" +
                //                 item.codeNVL + "|" +
                //                 item.maker + "|" +
                //                 item.mkerPart + "|" +
                //                 item.lot + "|" +
                //                 item.temCode + "|" +
                //                 item.ngInTemCode + "|" +
                //                 item.tgianInTemCode + "|" +
                //                 item.ngNhapKho + "|" +
                //                 item.tgianNhapKho + "|" +
                //                 item.ngCapNVL + "|" +
                //                 item.tgianCapNVL + "|" +
                //                 item.PDxacnhan + "|" +
                //                 item.tgianxacnhan + "|" +
                //                 item.ngTraNVL + "|" +
                //                 item.tgianTraNVL + "|" +
                //                 item.ghiChuTra + "|" +
                //                 item.ngTraWH + "|" +
                //                 item.tgianTraWH + "|" +
                //                 item.ghiChuTraWH);
                //}
                //sw.Close();
                //fs.Close();
                #endregion
                return arrRet;
            }
            else
            {
                return new string[] { "error" };
            }
        }

        public class NVL
        {
            public string model { set; get; }
            public string codeNVL { set; get; }
            public string maker { set; get; }
            public string mkerPart { set; get; }
            public string lot { set; get; }
            public string temCode { set; get; }
            public string ngInTemCode { set; get; }
            public string tgianInTemCode { set; get; }
            public string ngNhapKho { set; get; }
            public string tgianNhapKho { set; get; }
            public string ngCapNVL { set; get; }
            public string tgianCapNVL { set; get; }
            public string PDxacnhan { set; get; }
            public string tgianxacnhan { set; get; }
            public string ngTraNVL { set; get; }
            public string tgianTraNVL { set; get; }
            public string ghiChuTra { set; get; }
            public string ngTraWH { set; get; }
            public string tgianTraWH { set; get; }
            public string ghiChuTraWH { set; get; }
        }

        public void upExitInf(DataGridView dgv1, string model)
        {
            //Update data đã nhập từ datagridview vào database
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                foreach (DataGridViewRow dgr in dgv1.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string strUp = string.Empty;
                        //strUp = "Update OrderWH Set Maker = '" + dgr.Cells[2].Value.ToString() + "', Maker_Part = '" + dgr.Cells[3].Value.ToString() + "', Lot = '" + dgr.Cells[4].Value.ToString() + "', Qty_Input = '" + dgr.Cells[5].Value.ToString() + "' Where Code = '" + dgr.Cells[1].Value.ToString() + "'";
                        strUp = "Insert Into OrderWH (Model, Mo_ta, Ma_NVL, Maker, Maker_Part, Lot, So_luong_nhap, Tem_Code) Values ('" + model + "','" +
                                                                                                                                          dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                                                                                          dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                                                                                          dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                                                                                          dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                                                                                          dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                                                                                          dgr.Cells["So_luong_nhap"].Value.ToString() + "','" +
                                                                                                                                          dgr.Cells["Tem_code"].Value.ToString() + "')";
                        OleDbCommand cmd = new OleDbCommand(strUp, cnn);
                        cmd.ExecuteNonQuery();
                    }                   
                }
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu database!", "OrderWH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        public void upExitInfRe(DataGridView dgv1)
        {
            //Update data đã nhập từ datagridview vào database
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                foreach (DataGridViewRow dgr in dgv1.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string strUp = string.Empty;
                        //strUp = "Update OrderWH Set Maker = '" + dgr.Cells[2].Value.ToString() + "', Maker_Part = '" + dgr.Cells[3].Value.ToString() + "', Lot = '" + dgr.Cells[4].Value.ToString() + "', Qty_Input = '" + dgr.Cells[5].Value.ToString() + "' Where Code = '" + dgr.Cells[1].Value.ToString() + "'";
                        strUp = "Insert Into ReturnWH Values ('" + dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                   dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                   dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                   dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                   dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                   dgr.Cells["So_luong_tra"].Value.ToString() + "','" +
                                                                   dgr.Cells["Tem_code"].Value.ToString() + "','" +
                                                                   dgr.Cells["Giai_thich"].Value.ToString() + "','" +
                                                                   dgr.Cells["Ghi_chu"].Value.ToString() + "')";
                        OleDbCommand cmd = new OleDbCommand(strUp, cnn);
                        cmd.ExecuteNonQuery();
                    }                    
                }
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu database!", "ReturnWH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void upExitInfKP(DataGridView dgv, string namdata, string date, string shift, string model, string ktz, string pd)
        {
            //Update data đã nhập từ datagridview vào database
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string strUp = string.Empty;
                        //strUp = "Update " + namdata + " Set Maker = '" + dgr.Cells[2].Value.ToString() + "', Maker_Part = '" + dgr.Cells[3].Value.ToString() + "', Lot = '" + dgr.Cells[5].Value.ToString() + "', QtyInput = '" + dgr.Cells[6].Value.ToString() + "', Time_cap ='" + dgr.Cells[8].Value.ToString() + "' Where Code = '" + dgr.Cells[1].Value.ToString() + "'";
                        strUp = "Insert Into " + namdata + " Values ('" + date + "','" +
                                                                          shift + "','" +
                                                                          "SMD" + "','" +
                                                                          model + "','" +
                                                                          dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                          dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                          dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                          dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                          dgr.Cells["Diem_gan"].Value.ToString() + "','" +
                                                                          dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                          dgr.Cells["So_luong_cap"].Value.ToString() + "','" +
                                                                          dgr.Cells["Tem_code"].Value.ToString() + "','" +
                                                                          "" + "','" +
                                                                          ktz + "','" +
                                                                          pd + "')";
                        OleDbCommand cmd = new OleDbCommand(strUp, cnn);
                        cmd.ExecuteNonQuery();
                    }                    
                }
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu database!", "In/Out Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void upExitInfPKK(DataGridView dgv, string namdata, string date, string shift, string model, string pd, string ktz)
        {
            //Update data đã nhập từ datagridview vào database
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            try
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string strUp = string.Empty;
                        //strUp = "Update " + namdata + " Set Maker = '" + dgr.Cells[2].Value.ToString() + "', Maker_Part = '" + dgr.Cells[3].Value.ToString() + "', Lot = '" + dgr.Cells[5].Value.ToString() + "', QtyInput = '" + dgr.Cells[6].Value.ToString() + "', Time_cap ='" + dgr.Cells[8].Value.ToString() + "' Where Code = '" + dgr.Cells[1].Value.ToString() + "'";
                        strUp = "Insert Into " + namdata + " Values ('" + date + "','" +
                                                                          shift + "','" +
                                                                          "SMD" + "','" +
                                                                          model + "','" +
                                                                          dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                          dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                          dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                          dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                          dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                          dgr.Cells["Slg_tra_KTZ"].Value.ToString() + "','" +
                                                                          dgr.Cells["Slg_ton_Line"].Value.ToString() + "','" +
                                                                          dgr.Cells["Tem_code"].Value.ToString() + "','" +
                                                                          dgr.Cells["Giai_thich"].Value.ToString() + "','" +
                                                                          dgr.Cells["Ghi_chu"].Value.ToString() + "','" +
                                                                          pd + "','" +
                                                                          ktz + "')";
                        OleDbCommand cmd = new OleDbCommand(strUp, cnn);
                        cmd.ExecuteNonQuery();
                    }                   
                }
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu database!", "In/Out Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable get_StockKTZ(string namdt)
        {
            string strSle = "Select * from " + namdt + " order by Thoi_gian, Ma_NVL";
            return getData(strSle);
        }              

        public bool get_RightLogin(string user, string pass)
        {
            string right_Login = "";
            string strSel = "Select kind From Login Where u_ser='" + user + "' And pass_word='" + pass + "'";

            DataTable dt = getData(strSel);

            foreach (DataRow dtr in dt.Rows)
            {
                if(dtr.ItemArray[0].ToString() == "admin" || dtr.ItemArray[0].ToString() == "manager")
                {
                    right_Login = dtr.ItemArray[0].ToString();
                }
                else
                {
                    right_Login = "user";
                }
            }

            if( right_Login == "admin" || right_Login == "manager")
            {
                return true;
            }
            else
            {
                return false;
            }
        }       

        public string get_PerLogin(string user, string pass, string col)
        {
            int i = 0;
            string per_Login = "";
            string strSel = "Select " + col + " From Login Where u_ser='" + user + "' And pass_word='" + pass + "'";

            DataTable dt = getData(strSel);

            foreach (DataRow dtr in dt.Rows)
            {
                i++;
                per_Login = dtr.ItemArray[0].ToString();                                                 
            }
            if(i > 1)
            {
                MessageBox.Show("Tìm thấy nhiều hơn 1 user!", "In/Out Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return per_Login;
        }

        public bool get_adLogin(string user, string pass)
        {
            string right_Login = "";
            string strSel = "Select kind From Login Where u_ser='" + user + "' And pass_word='" + pass + "'";

            DataTable dt = getData(strSel);

            foreach (DataRow dtr in dt.Rows)
            {
                if (dtr.ItemArray[0].ToString() == "admin")
                {
                    right_Login = dtr.ItemArray[0].ToString();
                }
                else
                {
                    right_Login = "no admin";
                }
            }

            if (right_Login == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }              

        public bool chekScanMakPrtSame(string makPrt)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> listMakPrt = new List<string>();

            chekInf.LoadList("MakerPart", ref listMakPrt);

            return chekInf.CheckDuplicateInforamation(makPrt, listMakPrt);
        }

        public bool chekNewCodeInputed(string nCode)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> listMakPrt = new List<string>();

            chekInf.LoadList("Input_Line", ref listMakPrt);

            return chekInf.CheckDuplicateInforamation(nCode, listMakPrt);
        }

        public void savMakPrt(string makPrt)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(makPrt, "MakerPart");
        }

        public void savFIFO(string fifo)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(fifo, "FI-FO");
        }       

        public string get_time()
        {
            string strTim = string.Empty;
            if (DateTime.Now.Month >= 10)
            {
                if (DateTime.Now.Day >= 10)
                {
                    strTim = DateTime.Now.ToString("MM/dd/yy") + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                }
                else
                {
                    strTim = DateTime.Now.ToString("MM/dd/yy") + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                }
            }
            else
            {
                if (DateTime.Now.Day >= 10)
                {
                    strTim = DateTime.Now.ToString("MM/dd/yy") + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                }
                else
                {
                    strTim = DateTime.Now.ToString("MM/dd/yy") + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                }
            }
            return strTim;
        }  

        public void savNwCod(string nwCod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            
            chekInf.SaveList(nwCod, "NewCode");                          
        }

        public bool chekNewCode(string nCode)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> listMakPrt = new List<string>();

            chekInf.LoadList("NewCode", ref listMakPrt);

            return chekInf.CheckDuplicateInforamation(nCode, listMakPrt);
        }

        public void savNwCodInputed(string nwCod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(nwCod, "Input_Line");
        }
        
        public string get_InputKp(string sdiCode, string datInp, string mkp, string datInputFull)
        {
            try
            {
                string strSel = "Select * From Stock_KTZ4 Where Ma_NVL ='" + sdiCode + "' And Maker_Part ='" + mkp + "'";
                DataTable dt = getData(strSel);

                if(dt.Rows.Count > 0)
                {
                    int minTim = 0;
                    int sameDateInputFull = 0;
                    foreach (DataRow dtr in dt.Rows)
                    {
                        if ((chekNVLHolding(dtr.ItemArray[1].ToString() + "+" + dtr.ItemArray[5].ToString() + "+" + dtr.ItemArray[3].ToString() + "+" + dtr.ItemArray[4].ToString()) == false)
                            || (chekdoubleCodePDxacnhan(dtr.ItemArray[1].ToString() + "+" + dtr.ItemArray[5].ToString() + "+" + dtr.ItemArray[3].ToString() + "+" + dtr.ItemArray[4].ToString()) == false))//NVL bị holding ko so sanh fifo
                        {
                            continue;
                        }
                        else
                        {
                            if (dtr.ItemArray[5].ToString() == datInputFull)
                            {
                                sameDateInputFull++;
                            }

                            if (Convert.ToDateTime(datInp) > Convert.ToDateTime(dtr.ItemArray[5].ToString().Substring(0, 8)))
                            {
                                minTim++;
                            }
                        }                        
                    }

                    if(sameDateInputFull > 0)
                    {
                        if (minTim == 0)
                        {
                            return "true";
                        }
                        else
                        {
                            return "false";
                        }
                    }
                    else
                    {
                        return "No Date";
                    }                   
                }
                else
                {
                    return "No code";
                }   
            }
            catch (Exception)
            {               
                return "Fail Access";
            }           
        }

        public void Del_StockZero(string namtable, string colDel)
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();

            string str = string.Empty;
            str = "Delete * From " + namtable + " Where " + colDel + " ='0'";
            OleDbCommand cmd = new OleDbCommand(str, cnn);
            cmd.ExecuteNonQuery();

            cnn.Close();      
        }
        
        public void show_StockKTZZ(DataGridView dgv, DataTable dt, string col1, string col2)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_timupdate = new DataGridViewTextBoxColumn();
            col_timupdate.DataPropertyName = "Thoi_gian";
            col_timupdate.HeaderText = "Thoi_gian";
            col_timupdate.Name = "Thoi_gian";
            col_timupdate.ReadOnly = true;
            col_timupdate.Width = 150;
            col_timupdate.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_timupdate);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 150;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 220;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);         

            DataGridViewTextBoxColumn col_col1 = new DataGridViewTextBoxColumn();
            col_col1.DataPropertyName = col1;
            col_col1.HeaderText = col1;
            col_col1.Name = col1;
            col_col1.ReadOnly = true;
            col_col1.Width = 100;
            col_col1.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_col1);

            DataGridViewTextBoxColumn col_col2 = new DataGridViewTextBoxColumn();
            col_col2.DataPropertyName = col2;
            col_col2.HeaderText = col2;
            col_col2.Name = col2;
            col_col2.ReadOnly = true;
            col_col2.Width = 100;
            col_col2.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_col2);

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        public void show_StockKTZZ2(DataGridView dgv, DataTable dt, string col1, string col2)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_timupdate = new DataGridViewTextBoxColumn();
            col_timupdate.DataPropertyName = "Thoi_gian";
            col_timupdate.HeaderText = "Thoi_gian";
            col_timupdate.Name = "Thoi_gian";
            col_timupdate.ReadOnly = true;
            col_timupdate.Width = 150;
            col_timupdate.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_timupdate);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 150;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 130;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 220;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);

            DataGridViewTextBoxColumn col_col1 = new DataGridViewTextBoxColumn();
            col_col1.DataPropertyName = col1;
            col_col1.HeaderText = col1;
            col_col1.Name = col1;
            col_col1.ReadOnly = true;
            col_col1.Width = 200;
            col_col1.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_col1);

            DataGridViewTextBoxColumn col_col2 = new DataGridViewTextBoxColumn();
            col_col2.DataPropertyName = col2;
            col_col2.HeaderText = col2;
            col_col2.Name = col2;
            col_col2.ReadOnly = true;
            col_col2.Width = 100;
            col_col2.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_col2);

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        public void show_StockLinee(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_datemonth = new DataGridViewTextBoxColumn();
            col_datemonth.DataPropertyName = "Ngay_thang";
            col_datemonth.HeaderText = "Ngay_thang";
            col_datemonth.Name = "Ngay_thang";
            col_datemonth.ReadOnly = true;
            col_datemonth.Width = 80;
            col_datemonth.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_datemonth);

            DataGridViewTextBoxColumn col_shift = new DataGridViewTextBoxColumn();
            col_shift.DataPropertyName = "Ca_kip";
            col_shift.HeaderText = "Ca_kip";
            col_shift.Name = "Ca_kip";
            col_shift.ReadOnly = true;
            col_shift.Width = 50;
            col_shift.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_shift);

            DataGridViewTextBoxColumn col_line = new DataGridViewTextBoxColumn();
            col_line.DataPropertyName = "Line";
            col_line.HeaderText = "Line";
            col_line.Name = "Line";
            col_line.ReadOnly = true;
            col_line.Width = 50;
            col_line.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_line);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 100;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 130;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);           

            DataGridViewTextBoxColumn col_lot = new DataGridViewTextBoxColumn();
            col_lot.DataPropertyName = "Lot";
            col_lot.HeaderText = "Lot";
            col_lot.Name = "Lot";
            col_lot.ReadOnly = true;
            col_lot.Width = 230;
            col_lot.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_lot);

            DataGridViewTextBoxColumn col_qtyInp = new DataGridViewTextBoxColumn();
            col_qtyInp.DataPropertyName = "So_luong_cap";
            col_qtyInp.HeaderText = "So_luong_cap";
            col_qtyInp.Name = "So_luong_cap";
            col_qtyInp.ReadOnly = true;
            col_qtyInp.Width = 120;
            col_qtyInp.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_qtyInp);            

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.ReadOnly = true;
            col_temCd.Width = 250;
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_temCd);

            DataGridViewTextBoxColumn col_ktz = new DataGridViewTextBoxColumn();
            col_ktz.DataPropertyName = "KTZ";
            col_ktz.HeaderText = "KTZ";
            col_ktz.Name = "KTZ";
            col_ktz.ReadOnly = true;
            col_ktz.Width = 150;
            col_ktz.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ktz);

            DataGridViewTextBoxColumn col_pd = new DataGridViewTextBoxColumn();
            col_pd.DataPropertyName = "PD";
            col_pd.HeaderText = "PD";
            col_pd.Name = "PD";
            col_pd.ReadOnly = true;
            col_pd.Width = 150;
            col_pd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_pd);

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        public void show_StockTieuHao(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_datemonth = new DataGridViewTextBoxColumn();
            col_datemonth.DataPropertyName = "Ngay_thang";
            col_datemonth.HeaderText = "Ngay_thang";
            col_datemonth.Name = "Ngay_thang";
            col_datemonth.ReadOnly = true;
            col_datemonth.Width = 100;
            col_datemonth.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_datemonth);

            DataGridViewTextBoxColumn col_shift = new DataGridViewTextBoxColumn();
            col_shift.DataPropertyName = "Ca_kip";
            col_shift.HeaderText = "Ca_kip";
            col_shift.Name = "Ca_kip";
            col_shift.ReadOnly = true;
            col_shift.Width = 50;
            col_shift.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_shift);

            DataGridViewTextBoxColumn col_line = new DataGridViewTextBoxColumn();
            col_line.DataPropertyName = "Line";
            col_line.HeaderText = "Line";
            col_line.Name = "Line";
            col_line.ReadOnly = true;
            col_line.Width = 50;
            col_line.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_line);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 120;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 140;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);

            DataGridViewTextBoxColumn col_lot = new DataGridViewTextBoxColumn();
            col_lot.DataPropertyName = "Lot";
            col_lot.HeaderText = "Lot";
            col_lot.Name = "Lot";
            col_lot.ReadOnly = true;
            col_lot.Width = 250;
            col_lot.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_lot);

            DataGridViewTextBoxColumn col_dgan = new DataGridViewTextBoxColumn();
            col_dgan.DataPropertyName = "Diem_gan";
            col_dgan.HeaderText = "Diem_gan";
            col_dgan.Name = "Diem_gan";
            col_dgan.ReadOnly = true;
            col_dgan.Width = 120;
            col_dgan.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_dgan);

            DataGridViewTextBoxColumn col_qtyInp = new DataGridViewTextBoxColumn();
            col_qtyInp.DataPropertyName = "So_luong_cap";
            col_qtyInp.HeaderText = "So_luong_cap";
            col_qtyInp.Name = "So_luong_cap";
            col_qtyInp.ReadOnly = true;
            col_qtyInp.Width = 120;
            col_qtyInp.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_qtyInp);

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.ReadOnly = true;
            col_temCd.Width = 250;
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_temCd);

            DataGridViewTextBoxColumn col_ktz = new DataGridViewTextBoxColumn();
            col_ktz.DataPropertyName = "KTZ";
            col_ktz.HeaderText = "KTZ";
            col_ktz.Name = "KTZ";
            col_ktz.ReadOnly = true;
            col_ktz.Width = 150;
            col_ktz.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ktz);

            DataGridViewTextBoxColumn col_pd = new DataGridViewTextBoxColumn();
            col_pd.DataPropertyName = "PD";
            col_pd.HeaderText = "PD";
            col_pd.Name = "PD";
            col_pd.ReadOnly = true;
            col_pd.Width = 150;
            col_pd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_pd);

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        public bool up_FIFO()
        {
            try
            {
                string strIn = string.Empty;
                FileStream fs_fifo = new FileStream(@Application.StartupPath + "\\Log\\Duplicate\\FI-FO.log", FileMode.Open);
                StreamReader sr_fifo = new StreamReader(fs_fifo);
                OleDbConnection cnn = new OleDbConnection(constr);
                cnn.Open();
                while (sr_fifo.EndOfStream == false)
                {
                    string[] arr_str = sr_fifo.ReadLine().Split('+');
                    strIn = "Insert Into Stock_KTZ4 Values ('" + arr_str[6] + "','" + arr_str[0] + "','" + arr_str[1] + "','" + arr_str[2] + "','" + arr_str[3] + "','" + arr_str[4] + "','" + arr_str[5] + "')";
                    OleDbCommand cmd = new OleDbCommand(strIn, cnn);
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
                sr_fifo.Close();
                fs_fifo.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }                  
        }

        public bool up_FIFO2(DataGridView dgv)
        {
            try
            {
                OleDbConnection cnn = new OleDbConnection(constr);
                cnn.Open();
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        if (int.Parse(dgr.Cells["Slg_tra_KTZ"].Value.ToString()) > 0)//stock KTZ > 0
                        {
                            string strIn = string.Empty;
                            string[] str = dgr.Cells["Tem_code"].Value.ToString().Split('+');
                            strIn = "Insert Into Stock_KTZ4 Values ('" + dgr.Cells["Mo_ta"].Value.ToString() + "','" +
                                                                         dgr.Cells["Ma_NVL"].Value.ToString() + "','" +
                                                                         dgr.Cells["Maker"].Value.ToString() + "','" +
                                                                         dgr.Cells["Maker_Part"].Value.ToString() + "','" +
                                                                         dgr.Cells["Lot"].Value.ToString() + "','" +
                                                                         str[1] + "','" +
                                                                         dgr.Cells["Slg_tra_KTZ"].Value.ToString() + "')";
                            OleDbCommand cmd = new OleDbCommand(strIn, cnn);
                            cmd.ExecuteNonQuery();
                        }
                    }                   
                }
                cnn.Close();
                return true;            
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool del_FIFO(DataGridView dgv)
        {
            try
            {
                //string strDel = string.Empty;
                //FileStream fs_fifo = new FileStream(@Application.StartupPath + "\\Log\\Duplicate\\FI-FO.log", FileMode.Open);
                //StreamReader sr_fifo = new StreamReader(fs_fifo);
                OleDbConnection cnn = new OleDbConnection(constr);
                cnn.Open();
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (dgr.Cells["Mo_ta"].Value != null && dgr.Cells["Mo_ta"].Value.ToString() != "")
                    {
                        string strDel = string.Empty;
                        string[] str = dgr.Cells["Tem_code"].Value.ToString().Split('+');
                        strDel = "Delete * From Stock_KTZ4 Where Ma_NVL ='" + str[0] + "' And FIFO = '" + str[1] + "'";
                        OleDbCommand cmd = new OleDbCommand(strDel, cnn);
                        cmd.ExecuteNonQuery();
                    }
                }
                //while (sr_fifo.EndOfStream == false)
                //{
                //    string[] arr_str = sr_fifo.ReadLine().Split('+');
                //    strDel = "Delete * From Stock_KTZ4 Where FIFO = '" + arr_str[4] + "'";
                //    OleDbCommand cmd = new OleDbCommand(strDel, cnn);
                //    cmd.ExecuteNonQuery();
                //}
                cnn.Close();
                //sr_fifo.Close();
                //fs_fifo.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int count_file(string strpath, string[] historyCheck)
        {
            int numFil = 0;
            for(int i = 0; i < historyCheck.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(strpath + historyCheck[i]);
                if (dir.Exists)
                {
                    numFil = numFil + dir.GetFiles().Length;
                }  
            }                          
            return numFil;
        }

        public string[] get_filOK(int so_file, string strpath, string[] historyCheck, string modl, string time1, string time2, int j)
        {
            j = 0;
            string[] namFilCSV = new string[so_file];

            for(int i = 0; i < historyCheck.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(strpath + historyCheck[i]);
                if (dir.Exists)
                {
                    foreach (FileInfo fIn in dir.GetFiles())
                    {
                        string[] arr_tg1 = fIn.Name.Split('_');
                        string[] arr_tg2 = arr_tg1[1].Split('.');
                        if ((Convert.ToDateTime(time1) <= Convert.ToDateTime(arr_tg1[0])) && (Convert.ToDateTime(arr_tg1[0]) <= Convert.ToDateTime(time2)) && arr_tg2[0] == modl)
                        {
                            namFilCSV[j] = fIn.Name;
                            j++;
                        }
                    }
                }    
            }
                       
            return namFilCSV; 
        }        

        public void savHolding(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(inp_Cod, "NVL_Holding");
        }

        public bool chekNVLHolding(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> litInp = new List<string>();

            chekInf.LoadList("NVL_Holding", ref litInp);

            return chekInf.CheckDuplicateInforamation(inp_Cod, litInp);
        }

        public void savPDxacnhan(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(inp_Cod, "PDxacnhan");
        }

        public bool chekdoubleCodePDxacnhan(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> litInp = new List<string>();

            chekInf.LoadList("PDxacnhan", ref litInp);

            return chekInf.CheckDuplicateInforamation(inp_Cod, litInp);
        }       

        public void savInput1(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(inp_Cod, "Input_Ktz");
        }

        public bool chekdoubleCode1(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> litInp = new List<string>();

            chekInf.LoadList("Input_Ktz", ref litInp);

            return chekInf.CheckDuplicateInforamation(inp_Cod, litInp);
        }        

        public void savInput2(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(inp_Cod, "Return_WH");
        }

        public void savInput3(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();

            chekInf.SaveList(inp_Cod, "KTZ_Other");
        }

        public bool chekdoubleCode2(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> litInp = new List<string>();

            chekInf.LoadList("Return_WH", ref litInp);

            return chekInf.CheckDuplicateInforamation(inp_Cod, litInp);
        }

        public bool chekdoubleCodeOther(string inp_Cod)
        {
            OptionDefine.clsCheckTrungInformation chekInf = new OptionDefine.clsCheckTrungInformation();
            List<string> litInp = new List<string>();

            chekInf.LoadList("KTZ_Other", ref litInp);

            return chekInf.CheckDuplicateInforamation(inp_Cod, litInp);
        }

        public bool checkClearLot(string nCode)
        {
            //int roww = 0;
            //StreamReader srInt = new StreamReader(@Application.StartupPath + "\\Log\\Duplicate\\PDxacnhan.log");
            //while (srInt.EndOfStream == false)
            //{
            //    string strSrI = srInt.ReadLine();
            //    if(strSrI != "")
            //    {
            //        roww++;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //srInt.Close();
            //if (roww == 0)
            //{
            //    return true;//pass
            //}
            //else
            //{
            //    string[] strArr = nCode.Split('+');
            //    //kiem tra da input code NVL va maker part chua?
            //    int same = 0;
            //    string lot = string.Empty, date = string.Empty;
            //    StreamReader sr = new StreamReader(@Application.StartupPath + "\\Log\\Duplicate\\PDxacnhan.log");
            //    while (sr.EndOfStream == false)
            //    {
            //        string strSr = sr.ReadLine();
            //        if (strSr.Contains(strArr[0]) && strSr.Contains(strArr[2]))
            //        {
            //            same++;
            //            string[] strSrArr = strSr.Split('+');
            //            string[] strDate = strSrArr[1].Split('-');
            //            lot = strSrArr[3];
            //            date = strDate[0];
            //            break;
            //        }
            //    }
            //    sr.Close();

            //    if (same == 0)//chưa input ma_NVL, Maker_Part lần nào
            //    {
            //        return true;//pass
            //    }
            //    else//ma_NVL, Maker_Part input r
            //    {
            //        //Clear fifo da input r
            //        DataTable dt = getData("Select * From Stock_KTZ4 where Ma_NVL='" + strArr[0] + "' and Maker_Part='" + strArr[2] + "'");
            //        StreamReader sr1 = new StreamReader(@Application.StartupPath + "\\Log\\Duplicate\\PDxacnhan.log");
            //        while (sr1.EndOfStream == false)
            //        {
            //            string strSr1 = sr1.ReadLine();
            //            for (int i = 0; i < dt.Rows.Count;i++)
            //            {
            //                if (strSr1.Contains(dt.Rows[i].ItemArray[5].ToString()))
            //                {
            //                    dt.Rows.Remove(dt.Rows[i]);
            //                    i--;
            //                }
            //            }
            //        }
            //        sr1.Close();

            //        //Kiem tra clear lot theo fifo
            //        int clearLot = 0;                  
            //        foreach(DataRow dtr in dt.Rows)
            //        {
            //            if(dtr["FIFO"].ToString().Contains(date))
            //            {
            //                if (dtr["Lot"].ToString() == lot && strArr[3] != lot)
            //                {
            //                    clearLot++;
            //                }
            //            }                       
            //        }
            //         if(clearLot > 0)
            //        {
            //            return false;
            //        }
            //        else
            //        {
                        return true;//pass
            //        }
            //    }
            //}
        }

        public void merg_Excel(string strpath, string[] historyCheck, string[] namfil, int numfil, string dtime, string cs, bool whOrInout)
        {
            Workbook wb_sum = new Workbook();
            Worksheet ws_sum = wb_sum.Worksheets[0];
            bool ok = false;
            for(int i = 0; i < numfil; i++)
            {
                Workbook wb = new Workbook();
                try
                {
                    if(namfil[i] != null)
                    {
                        for (int m = 0; m < historyCheck.Length; m++)
                        {
                            if(File.Exists(strpath + "\\" + historyCheck[m] + "\\" + namfil[i]))
                            {
                                wb.LoadFromFile(strpath + "\\" + historyCheck[m] + "\\" + namfil[i], ",", 1, 1, ExcelVersion.Version2007);
                                Worksheet ws = wb.Worksheets[0];
                                DataTable dt = ws.ExportDataTable();

                                if (whOrInout == true)//WH->KTZ, KTZ->WH
                                {
                                    DataTable dt1 = new DataTable();
                                    dt1.Clear();
                                    dt1.Columns.Add("DateTime");
                                    DataRow dtr = dt1.NewRow();
                                    dtr["DateTime"] = namfil[i];
                                    dt1.Rows.Add(dtr);

                                    if (i == 0)
                                    {
                                        ws_sum.InsertDataTable(dt1, false, 1, 1);
                                        ws_sum.InsertDataTable(dt, true, ws_sum.LastRow + 1, 1);
                                    }
                                    else
                                    {
                                        ws_sum.InsertDataTable(dt1, false, ws_sum.LastRow + 3, 1);
                                        ws_sum.InsertDataTable(dt, true, ws_sum.LastRow + 1, 1);
                                    }
                                    ok = true;
                                }
                                else//KTZ->PD, PD->KTZ
                                {
                                    if (i == 0)
                                    {
                                        ws_sum.InsertDataTable(dt, true, 1, 1);
                                    }
                                    else
                                    {
                                        ws_sum.InsertDataTable(dt, true, ws_sum.LastRow + 2, 1);
                                    }
                                    ok = true;
                                }              
                            }                            
                        }                                      
                    }                   
                }
                catch (Exception)
                {                    
                    ok = false;
                }                                                    
            }

            if(ok == true)
            {
                //save and open
                if (File.Exists(@Application.StartupPath + "\\tem\\" + dtime + "_Sum" + cs + ".csv"))
                {
                    File.Delete(@Application.StartupPath + "\\tem\\" + dtime + "_Sum" + cs + ".csv");
                }
                wb_sum.SaveToFile(@Application.StartupPath + "\\tem\\" + dtime + "_Sum" + cs + ".csv", ExcelVersion.Version2007);
                //Excel.Application excel_1 = new Microsoft.Office.Interop.Excel.Application();
                //Excel.Workbook wb1 = excel_1.Workbooks.Open(@System.Windows.Forms.Application.StartupPath + "\\tem\\" + dtime + "_Sum" + cs + ".xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //Microsoft.Office.Interop.Excel.Worksheet ws1 = (Microsoft.Office.Interop.Excel.Worksheet)wb1.Worksheets.get_Item(1);
                //excel_1.Visible = true;   
                System.Diagnostics.Process.Start(@Application.StartupPath + "\\tem\\" + dtime + "_Sum" + cs + ".csv");
            } 
            else
            {
                MessageBox.Show("Không tồn tại data bạn yêu cầu!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }       

        public bool chk_formInput(string code_input)
        {
            try
            {
                string[] arr1 = code_input.Split('+');
                if(arr1.Length == 4)
                {
                    string[] sdicod = arr1[0].Split('-');
                    if(sdicod.Length == 2)
                    {
                        string[] time1 = arr1[1].Split('-');
                        if(time1.Length == 4)
                        {
                            string[] time2 = time1[0].Split('/');
                            if(time2.Length == 3)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool get_NVLLine(string dtb)
        {
            string str = "Select * From " + dtb;
            DataTable dt = getData(str);
            if(dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }        

        public string[] get_modelRun()
        {           
            string str = "Select * From KtzPd_ModelRun";
            DataTable dt = getData(str);
            string[] str1 = new string[dt.Rows.Count];
            if(dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    str1[i] = dr.ItemArray[2].ToString();
                    i++;
                }
                return str1;
            }
            else
            {
                string[] strg = new string[1] {"none"};
                return strg;
            }
        }       

        public void del_filLog(string namFil, string mkp, int dong)
        {
            string strpth = @Application.StartupPath + "\\Log\\Duplicate\\" + namFil + ".log";
            if(File.Exists(strpth))
            {
                try
                {
                    string oldText = string.Empty;
                    string ntext = string.Empty;
                    FileStream fs = new FileStream(strpth, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    int i = 0;
                    while ((oldText = sr.ReadLine()) != null)
                    {
                        if (i < dong)
                        {
                            if (oldText.Contains(mkp) == false)
                            {
                                ntext += oldText + Environment.NewLine;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else
                        {
                            ntext += oldText + Environment.NewLine;
                        }
                    }
                    sr.Close();
                    fs.Close();
                    File.WriteAllText(strpth, ntext);
                }
                catch (Exception)
                {
                    MessageBox.Show("Xảy ra lỗi xóa dữ liệu trong file " + namFil + ".log!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }           
            }  
            else
            {
                MessageBox.Show("Không tồn tại file " + namFil + ".log!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public bool up_filLog(string strpth1, string strpth2, string cs)
        {
            //xoa data strpth1 trong strpth2
            try
            {
                FileStream fs_ktz = new FileStream(strpth1, FileMode.Open);
                StreamReader sr_ktz = new StreamReader(fs_ktz);
                while (sr_ktz.EndOfStream == false)
                //while (sr_ktz.ReadLine() != null)
                {
                    string s1 = sr_ktz.ReadLine();
                    string oldText = string.Empty;
                    string ntext = string.Empty;
                    FileStream fs_line = new FileStream(strpth2, FileMode.Open);
                    StreamReader sr_line = new StreamReader(fs_line);
                    while ((oldText = sr_line.ReadLine()) != null)
                    {
                        if (oldText.Contains(s1) == false)
                        {
                            ntext += oldText + Environment.NewLine;
                        }
                    }
                    sr_line.Close();
                    fs_line.Close();
                    File.WriteAllText(strpth2, ntext);
                }
                sr_ktz.Close();
                fs_ktz.Close();
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Xảy ra lỗi update file .log!", cs, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }           
        }

        public DataTable GetNewPass(string user, string oldPass)
        {
            string str = "select Password_New From ChangePassWord where User_text='" + user + "' And Password_Old='" + oldPass + "'";
            return getData(str);
        }

        public void DeleteDataNewPass()
        {
            OleDbConnection cnn = new OleDbConnection(constr);
            cnn.Open();
            string str = "Delete * From ChangePassWord";
            OleDbCommand cmd = new OleDbCommand(str, cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }       

        public bool get_extinctCode(string codeInput)
        {
            string[] str = codeInput.Split('+');
            try
            {
                string strSel = "Select * From Stock_KTZ4 Where Ma_NVL ='" + str[0] + "' And Maker_Part ='" + str[2] + "' And FIFO = '" + str[1] + "' And Lot = '" + str[3] + "'";
                DataTable dt = getData(strSel);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void get_colorText(TextBox txt)
        {
            if(txt.Text.Length == 0)
            {
                txt.BackColor = Color.Red;
            }
            else
            { 
                int outText;
                bool chk = int.TryParse(txt.Text, out outText);
                if(chk == false)
                {
                    txt.BackColor = Color.Red;
                }
                else
                {
                    txt.BackColor = Color.White;
                }
            }
        }

        public void upModeRun(string dat, string shf, string model)
        {
            //Update data dang nhap va database
            OleDbConnection cnn = new OleDbConnection(constr); //khai báo và khởi tạo biến cnn
            cnn.Open();   //mở kết nối

             string strIn = "Insert Into KtzPd_ModelRun (Ngay_thang, Ca_kip, Model) Values ('" +                                                                                                                              
                                                                                                dat + "','" +
                                                                                                shf + "','" +
                                                                                                model + "')";
            OleDbCommand cmd = new OleDbCommand(strIn, cnn);// Khai báo và khởi tạo bộ nhớ biến cmd
            cmd.ExecuteNonQuery(); // thực hiện lênh SQL
            
            cnn.Close();// Ngắt kết nối  
        }

        public void show_PDxacnhan(DataGridView dgv, DataTable dt)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn col_stt = new DataGridViewTextBoxColumn();
            col_stt.DataPropertyName = "STT";
            col_stt.HeaderText = "STT";
            col_stt.Name = "STT";
            col_stt.ReadOnly = true;
            col_stt.Width = 50;
            col_stt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_stt);

            DataGridViewTextBoxColumn col_datemonth = new DataGridViewTextBoxColumn();
            col_datemonth.DataPropertyName = "Ngay_thang";
            col_datemonth.HeaderText = "Ngay_thang";
            col_datemonth.Name = "Ngay_thang";
            col_datemonth.ReadOnly = true;
            col_datemonth.Width = 100;
            col_datemonth.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_datemonth);

            DataGridViewTextBoxColumn col_shift = new DataGridViewTextBoxColumn();
            col_shift.DataPropertyName = "Ca_kip";
            col_shift.HeaderText = "Ca_kip";
            col_shift.Name = "Ca_kip";
            col_shift.ReadOnly = true;
            col_shift.Width = 50;
            col_shift.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_shift);

            DataGridViewTextBoxColumn col_line = new DataGridViewTextBoxColumn();
            col_line.DataPropertyName = "Line";
            col_line.HeaderText = "Line";
            col_line.Name = "Line";
            col_line.ReadOnly = true;
            col_line.Width = 50;
            col_line.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_line);

            DataGridViewTextBoxColumn col_mol = new DataGridViewTextBoxColumn();
            col_mol.DataPropertyName = "Model";
            col_mol.HeaderText = "Model";
            col_mol.Name = "Model";
            col_mol.ReadOnly = true;
            col_mol.Width = 120;
            col_mol.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_mol);

            DataGridViewTextBoxColumn col_Material = new DataGridViewTextBoxColumn();
            col_Material.DataPropertyName = "Mo_ta";
            col_Material.HeaderText = "Mo_ta";
            col_Material.Name = "Mo_ta";
            col_Material.ReadOnly = true;
            col_Material.Width = 80;
            col_Material.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Material);

            DataGridViewTextBoxColumn col_Code = new DataGridViewTextBoxColumn();
            col_Code.DataPropertyName = "Ma_NVL";
            col_Code.HeaderText = "Ma_NVL";
            col_Code.Name = "Ma_NVL";
            col_Code.ReadOnly = true;
            col_Code.Width = 100;
            col_Code.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Code);

            DataGridViewTextBoxColumn col_Maker = new DataGridViewTextBoxColumn();
            col_Maker.DataPropertyName = "Maker";
            col_Maker.HeaderText = "Maker";
            col_Maker.Name = "Maker";
            col_Maker.ReadOnly = true;
            col_Maker.Width = 100;
            col_Maker.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_Maker);

            DataGridViewTextBoxColumn col_MakerPart = new DataGridViewTextBoxColumn();
            col_MakerPart.DataPropertyName = "Maker_Part";
            col_MakerPart.HeaderText = "Maker_Part";
            col_MakerPart.Name = "Maker_Part";
            col_MakerPart.ReadOnly = true;
            col_MakerPart.Width = 140;
            col_MakerPart.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_MakerPart);            

            DataGridViewTextBoxColumn col_lot = new DataGridViewTextBoxColumn();
            col_lot.DataPropertyName = "Lot";
            col_lot.HeaderText = "Lot";
            col_lot.Name = "Lot";
            col_lot.ReadOnly = true;
            col_lot.Width = 200;
            col_lot.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_lot);

            DataGridViewTextBoxColumn col_qtyInp = new DataGridViewTextBoxColumn();
            col_qtyInp.DataPropertyName = "So_luong_cap";
            col_qtyInp.HeaderText = "So_luong_cap";
            col_qtyInp.Name = "So_luong_cap";
            col_qtyInp.ReadOnly = false;
            col_qtyInp.Width = 120;
            col_qtyInp.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_qtyInp);

            DataGridViewTextBoxColumn col_temCd = new DataGridViewTextBoxColumn();
            col_temCd.DataPropertyName = "Tem_code";
            col_temCd.HeaderText = "Tem_code";
            col_temCd.Name = "Tem_code";
            col_temCd.ReadOnly = true;
            col_temCd.Width = 250;
            col_temCd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_temCd);

            //DataGridViewComboBoxColumn col_PDxn = new DataGridViewComboBoxColumn();
            //col_PDxn.Items.Add("OK");
            //col_PDxn.Items.Add("NG");
            //col_PDxn.FlatStyle = FlatStyle.Popup;
            //col_PDxn.DataPropertyName = "PD_xac_nhan";
            //col_PDxn.HeaderText = "PD_xac_nhan";
            //col_PDxn.Name = "PD_xac_nhan";
            //col_PDxn.ReadOnly = false;
            //col_PDxn.Width = 150;
            //col_PDxn.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns.Add(col_PDxn);

            DataGridViewTextBoxColumn col_ktz = new DataGridViewTextBoxColumn();
            col_ktz.DataPropertyName = "KTZ";
            col_ktz.HeaderText = "KTZ";
            col_ktz.Name = "KTZ";
            col_ktz.ReadOnly = true;
            col_ktz.Width = 150;
            col_ktz.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_ktz);

            DataGridViewTextBoxColumn col_pd = new DataGridViewTextBoxColumn();
            col_pd.DataPropertyName = "PD";
            col_pd.HeaderText = "PD";
            col_pd.Name = "PD";
            col_pd.ReadOnly = true;
            col_pd.Width = 150;
            col_pd.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col_pd);

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        public bool CheckSameCode(string maNVL, string mker, string mkp, DataGridView dgv)
        {
            if(dgv.RowCount == 0)
            {
                return true;//pass
            }
            else
            {
                int same = 0;
                foreach(DataGridViewRow dgr in dgv.Rows)
                {
                    if(dgr.Cells["Ma_NVL"].Value.ToString() == maNVL 
                        && dgr.Cells["Maker"].Value.ToString() == mker
                        && dgr.Cells["Maker_Part"].Value.ToString() == mkp)
                    {
                        same++;
                    }
                    break;
                }

                if(same == 0)//ko trung code dang input
                {
                    return false;
                }
                else
                {
                    return true;//pass
                }
            }
        }
    }
}
