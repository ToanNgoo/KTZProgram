﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ManageMaterialPBA
{
    public partial class ConfirmInKTZ : Form
    {
        NewCode _frm;

        public ConfirmInKTZ(NewCode frm)
        {
            InitializeComponent();
            _frm = frm;
        }       

        public bool get_RightLogin2(string user, string pass)
        {
            string right_Login = "";
            string strSel = "Select part From Login Where u_ser='" + user + "' And pass_word='" + pass + "'";

            DataTable dt = getData(strSel);

            foreach (DataRow dtr in dt.Rows)
            {
                if (dtr.ItemArray[0].ToString() == "CPE")
                {
                    right_Login = dtr.ItemArray[0].ToString();
                }
                else
                {
                    right_Login = "other";
                }
            }

            if (right_Login == "CPE")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable getData(string str)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(str, @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = " + Application.StartupPath + @"\Database.mdb");
            da.Fill(dt);

            return dt;
        }

        private void btn_dy_Click(object sender, EventArgs e)
        {
            if (txt_lydo.Text != "")
            {
                if (get_RightLogin2(txt_dn.Text, txt_mk.Text) == true)
                {
                    _frm.cfrm = true;
                    _frm.idCPE = txt_dn.Text;
                    _frm.psCPE = txt_mk.Text;
                    _frm.strlydo = txt_lydo.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền xác nhận!", "Confirm Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa điền lý do thực hiện!", "Confirm Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfirmInKTZ_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }
    }
}
