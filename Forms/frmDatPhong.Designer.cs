namespace HotelManagementSystem.Forms
{
    partial class frmDatPhong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatPhong));
            this.dgvDatPhong = new System.Windows.Forms.DataGridView();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSoNguoi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThem = new System.Windows.Forms.Button();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDatCoc = new System.Windows.Forms.TextBox();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.cmbMaKH = new System.Windows.Forms.ComboBox();
            this.cmbSoPhong = new System.Windows.Forms.ComboBox();
            this.dtpNgayDat = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayCheckOut = new System.Windows.Forms.DateTimePicker();
            this.txtMaDatPhong = new System.Windows.Forms.TextBox();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDatPhong
            // 
            this.dgvDatPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatPhong.Location = new System.Drawing.Point(294, 224);
            this.dgvDatPhong.Name = "dgvDatPhong";
            this.dgvDatPhong.RowHeadersWidth = 51;
            this.dgvDatPhong.RowTemplate.Height = 24;
            this.dgvDatPhong.Size = new System.Drawing.Size(378, 186);
            this.dgvDatPhong.TabIndex = 33;
            this.dgvDatPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatPhong_CellClick);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(208, 349);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(49, 25);
            this.btnHuy.TabIndex = 32;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(139, 349);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(49, 25);
            this.btnSua.TabIndex = 30;
            this.btnSua.Text = "Nhận";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "CMND/ CCCD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Mã đặt phòng";
            // 
            // txtSoNguoi
            // 
            this.txtSoNguoi.Location = new System.Drawing.Point(139, 148);
            this.txtSoNguoi.Name = "txtSoNguoi";
            this.txtSoNguoi.Size = new System.Drawing.Size(118, 22);
            this.txtSoNguoi.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Mã khách hàng";
            // 
            // txtThem
            // 
            this.txtThem.Location = new System.Drawing.Point(63, 349);
            this.txtThem.Name = "txtThem";
            this.txtThem.Size = new System.Drawing.Size(57, 25);
            this.txtThem.TabIndex = 24;
            this.txtThem.Text = "Đặt";
            this.txtThem.UseVisualStyleBackColor = true;
            this.txtThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(139, 60);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(118, 22);
            this.txtCCCD.TabIndex = 23;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUsername.Location = new System.Drawing.Point(37, 5);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(39, 15);
            this.lblUsername.TabIndex = 22;
            this.lblUsername.Text = "admin";
            // 
            // picUser
            // 
            this.picUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picUser.BackgroundImage")));
            this.picUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picUser.Location = new System.Drawing.Point(1, 0);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(31, 36);
            this.picUser.TabIndex = 21;
            this.picUser.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Số phòng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 35;
            this.label5.Text = "Số người";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 15);
            this.label6.TabIndex = 36;
            this.label6.Text = "Ngày đặt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 15);
            this.label7.TabIndex = 37;
            this.label7.Text = "Ngày Check-in";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 15);
            this.label8.TabIndex = 38;
            this.label8.Text = "Ngày Check-out";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 15);
            this.label9.TabIndex = 39;
            this.label9.Text = "Đặt cọc";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 292);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 15);
            this.label10.TabIndex = 40;
            this.label10.Text = "Trạng thái";
            // 
            // txtDatCoc
            // 
            this.txtDatCoc.Location = new System.Drawing.Point(139, 264);
            this.txtDatCoc.Name = "txtDatCoc";
            this.txtDatCoc.Size = new System.Drawing.Size(118, 22);
            this.txtDatCoc.TabIndex = 47;
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Location = new System.Drawing.Point(139, 292);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(118, 22);
            this.txtTrangThai.TabIndex = 46;
            // 
            // dgvPhong
            // 
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Location = new System.Drawing.Point(294, 10);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.RowTemplate.Height = 24;
            this.dgvPhong.Size = new System.Drawing.Size(378, 188);
            this.dgvPhong.TabIndex = 48;
            this.dgvPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellClick);
            // 
            // cmbMaKH
            // 
            this.cmbMaKH.FormattingEnabled = true;
            this.cmbMaKH.Location = new System.Drawing.Point(140, 88);
            this.cmbMaKH.Name = "cmbMaKH";
            this.cmbMaKH.Size = new System.Drawing.Size(117, 23);
            this.cmbMaKH.TabIndex = 49;
            // 
            // cmbSoPhong
            // 
            this.cmbSoPhong.FormattingEnabled = true;
            this.cmbSoPhong.Location = new System.Drawing.Point(140, 119);
            this.cmbSoPhong.Name = "cmbSoPhong";
            this.cmbSoPhong.Size = new System.Drawing.Size(117, 23);
            this.cmbSoPhong.TabIndex = 50;
            // 
            // dtpNgayDat
            // 
            this.dtpNgayDat.Location = new System.Drawing.Point(139, 180);
            this.dtpNgayDat.MinDate = new System.DateTime(1990, 11, 1, 0, 0, 0, 0);
            this.dtpNgayDat.Name = "dtpNgayDat";
            this.dtpNgayDat.Size = new System.Drawing.Size(118, 22);
            this.dtpNgayDat.TabIndex = 51;
            // 
            // dtpNgayCheckIn
            // 
            this.dtpNgayCheckIn.Location = new System.Drawing.Point(140, 208);
            this.dtpNgayCheckIn.MinDate = new System.DateTime(1990, 11, 1, 0, 0, 0, 0);
            this.dtpNgayCheckIn.Name = "dtpNgayCheckIn";
            this.dtpNgayCheckIn.Size = new System.Drawing.Size(117, 22);
            this.dtpNgayCheckIn.TabIndex = 52;
            // 
            // dtpNgayCheckOut
            // 
            this.dtpNgayCheckOut.Location = new System.Drawing.Point(140, 236);
            this.dtpNgayCheckOut.MinDate = new System.DateTime(1990, 11, 1, 0, 0, 0, 0);
            this.dtpNgayCheckOut.Name = "dtpNgayCheckOut";
            this.dtpNgayCheckOut.Size = new System.Drawing.Size(117, 22);
            this.dtpNgayCheckOut.TabIndex = 53;
            // 
            // txtMaDatPhong
            // 
            this.txtMaDatPhong.Location = new System.Drawing.Point(139, 33);
            this.txtMaDatPhong.Name = "txtMaDatPhong";
            this.txtMaDatPhong.Size = new System.Drawing.Size(118, 22);
            this.txtMaDatPhong.TabIndex = 29;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(123, 387);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 54;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frmDatPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 422);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.dtpNgayCheckOut);
            this.Controls.Add(this.dtpNgayCheckIn);
            this.Controls.Add(this.dtpNgayDat);
            this.Controls.Add(this.cmbSoPhong);
            this.Controls.Add(this.cmbMaKH);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.txtDatCoc);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvDatPhong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaDatPhong);
            this.Controls.Add(this.txtSoNguoi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThem);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.picUser);
            this.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDatPhong";
            this.Text = "S";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDatPhong;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSoNguoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button txtThem;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDatCoc;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.ComboBox cmbMaKH;
        private System.Windows.Forms.ComboBox cmbSoPhong;
        private System.Windows.Forms.DateTimePicker dtpNgayDat;
        private System.Windows.Forms.DateTimePicker dtpNgayCheckIn;
        private System.Windows.Forms.DateTimePicker dtpNgayCheckOut;
        private System.Windows.Forms.TextBox txtMaDatPhong;
        private System.Windows.Forms.Button btnThoat;
    }
}