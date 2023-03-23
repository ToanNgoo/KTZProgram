namespace ManageMaterialPBA
{
    partial class StockWaitPDxn
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_qty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_tx = new System.Windows.Forms.Button();
            this.btn_svStkPDxn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbx_lot = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbx_mkrprt = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_maker = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_maNVL = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_ngaythang = new System.Windows.Forms.ComboBox();
            this.dgv_stockPDxn = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_stockPDxn)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_qty);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(859, 47);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 108);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Số lượng";
            // 
            // txt_qty
            // 
            this.txt_qty.Location = new System.Drawing.Point(10, 58);
            this.txt_qty.Name = "txt_qty";
            this.txt_qty.Size = new System.Drawing.Size(137, 20);
            this.txt_qty.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tổng số lượng";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_tx);
            this.groupBox2.Controls.Add(this.btn_svStkPDxn);
            this.groupBox2.Location = new System.Drawing.Point(740, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(112, 108);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phím chức năng";
            // 
            // btn_tx
            // 
            this.btn_tx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_tx.Location = new System.Drawing.Point(18, 65);
            this.btn_tx.Name = "btn_tx";
            this.btn_tx.Size = new System.Drawing.Size(75, 30);
            this.btn_tx.TabIndex = 3;
            this.btn_tx.Text = "Truy xuất";
            this.btn_tx.UseVisualStyleBackColor = false;
            this.btn_tx.Click += new System.EventHandler(this.btn_tx_Click);
            // 
            // btn_svStkPDxn
            // 
            this.btn_svStkPDxn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_svStkPDxn.Location = new System.Drawing.Point(18, 24);
            this.btn_svStkPDxn.Name = "btn_svStkPDxn";
            this.btn_svStkPDxn.Size = new System.Drawing.Size(75, 30);
            this.btn_svStkPDxn.TabIndex = 2;
            this.btn_svStkPDxn.Text = "Xuất file";
            this.btn_svStkPDxn.UseVisualStyleBackColor = false;
            this.btn_svStkPDxn.Click += new System.EventHandler(this.btn_svStkPDxn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbx_lot);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbx_mkrprt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbx_maker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbx_maNVL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbx_ngaythang);
            this.groupBox1.Location = new System.Drawing.Point(13, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(719, 108);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc thông tin";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Red;
            this.button6.Location = new System.Drawing.Point(684, 23);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(24, 23);
            this.button6.TabIndex = 18;
            this.button6.Text = "X";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Red;
            this.button5.Location = new System.Drawing.Point(442, 68);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(24, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "X";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(442, 23);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 23);
            this.button4.TabIndex = 16;
            this.button4.Text = "X";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(198, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(198, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(493, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Lot";
            // 
            // cbx_lot
            // 
            this.cbx_lot.FormattingEnabled = true;
            this.cbx_lot.Location = new System.Drawing.Point(521, 24);
            this.cbx_lot.Name = "cbx_lot";
            this.cbx_lot.Size = new System.Drawing.Size(160, 21);
            this.cbx_lot.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Maker_Part";
            // 
            // cbx_mkrprt
            // 
            this.cbx_mkrprt.FormattingEnabled = true;
            this.cbx_mkrprt.Location = new System.Drawing.Point(318, 69);
            this.cbx_mkrprt.Name = "cbx_mkrprt";
            this.cbx_mkrprt.Size = new System.Drawing.Size(121, 21);
            this.cbx_mkrprt.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Maker";
            // 
            // cbx_maker
            // 
            this.cbx_maker.FormattingEnabled = true;
            this.cbx_maker.Location = new System.Drawing.Point(318, 24);
            this.cbx_maker.Name = "cbx_maker";
            this.cbx_maker.Size = new System.Drawing.Size(121, 21);
            this.cbx_maker.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ma_NVL";
            // 
            // cbx_maNVL
            // 
            this.cbx_maNVL.FormattingEnabled = true;
            this.cbx_maNVL.Location = new System.Drawing.Point(74, 69);
            this.cbx_maNVL.Name = "cbx_maNVL";
            this.cbx_maNVL.Size = new System.Drawing.Size(121, 21);
            this.cbx_maNVL.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ngay_thang";
            // 
            // cbx_ngaythang
            // 
            this.cbx_ngaythang.FormattingEnabled = true;
            this.cbx_ngaythang.Location = new System.Drawing.Point(74, 24);
            this.cbx_ngaythang.Name = "cbx_ngaythang";
            this.cbx_ngaythang.Size = new System.Drawing.Size(121, 21);
            this.cbx_ngaythang.TabIndex = 5;
            // 
            // dgv_stockPDxn
            // 
            this.dgv_stockPDxn.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgv_stockPDxn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_stockPDxn.Location = new System.Drawing.Point(13, 170);
            this.dgv_stockPDxn.Name = "dgv_stockPDxn";
            this.dgv_stockPDxn.Size = new System.Drawing.Size(1180, 423);
            this.dgv_stockPDxn.TabIndex = 13;
            this.dgv_stockPDxn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_stockPDxn_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Stock chờ PD xác nhận";
            // 
            // StockWaitPDxn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 607);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_stockPDxn);
            this.Controls.Add(this.label1);
            this.Name = "StockWaitPDxn";
            this.Text = "StockWaitPDxn";
            this.Load += new System.EventHandler(this.StockWaitPDxn_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_stockPDxn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_qty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_tx;
        private System.Windows.Forms.Button btn_svStkPDxn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbx_lot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbx_mkrprt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_maker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_maNVL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_ngaythang;
        private System.Windows.Forms.DataGridView dgv_stockPDxn;
        private System.Windows.Forms.Label label1;
    }
}