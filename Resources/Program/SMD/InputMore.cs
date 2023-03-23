using System;
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
    public partial class InputMore : Form
    {
        Form1 _frm;

        public InputMore(Form1 frm)
        {
            InitializeComponent();
            _frm = frm;
        }

        private void InputMore_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }

        private void btn_dy_Click(object sender, EventArgs e)
        {
            if (get_RightLogin2(txt_tk.Text, txt_mk.Text) == true)
            {
                _frm.cfrm = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền xác nhận!", "Input More", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool get_RightLogin2(string user, string pass)
        {
            string right_Login = "";
            string strSel = "Select Name_user, part From Login Where u_ser='" + user + "' And pass_word='" + pass + "'";

            DataTable dt = getData(strSel);

            foreach (DataRow dtr in dt.Rows)
            {
                if (dtr.ItemArray[1].ToString() == "CPE")
                {
                    right_Login = dtr.ItemArray[1].ToString();
                }
                else
                {
                    if (dtr.ItemArray[1].ToString() == "KTZ")
                    {
                        right_Login = dtr.ItemArray[0].ToString();
                    }                    
                }
            }

            if (right_Login == "CPE" || right_Login == "Nguyễn Thu Hương")
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
    }
}
