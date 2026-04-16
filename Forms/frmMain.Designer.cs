namespace HotelManagementSystem.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mnuThanhToan = new System.Windows.Forms.Button();
            this.mnuCTPhuThu = new System.Windows.Forms.Button();
            this.mnuNhanVien = new System.Windows.Forms.Button();
            this.mnuSDDichVu = new System.Windows.Forms.Button();
            this.mnuQLDichVu = new System.Windows.Forms.Button();
            this.mnuKhachHang = new System.Windows.Forms.Button();
            this.mnuLoaiPhong = new System.Windows.Forms.Button();
            this.mnuQLPhong = new System.Windows.Forms.Button();
            this.mnuDatPhong = new System.Windows.Forms.Button();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlAdminMenu = new System.Windows.Forms.Panel();
            this.btnPhanQuyen = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnSuaThongTin = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.pnlAdminMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.mnuThanhToan);
            this.panel1.Controls.Add(this.mnuCTPhuThu);
            this.panel1.Controls.Add(this.mnuNhanVien);
            this.panel1.Controls.Add(this.mnuSDDichVu);
            this.panel1.Controls.Add(this.mnuQLDichVu);
            this.panel1.Controls.Add(this.mnuKhachHang);
            this.panel1.Controls.Add(this.mnuLoaiPhong);
            this.panel1.Controls.Add(this.mnuQLPhong);
            this.panel1.Controls.Add(this.mnuDatPhong);
            this.panel1.Location = new System.Drawing.Point(0, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 436);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 68);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // mnuThanhToan
            // 
            this.mnuThanhToan.Location = new System.Drawing.Point(26, 356);
            this.mnuThanhToan.Name = "mnuThanhToan";
            this.mnuThanhToan.Size = new System.Drawing.Size(108, 26);
            this.mnuThanhToan.TabIndex = 14;
            this.mnuThanhToan.Text = "Thanh toán";
            this.mnuThanhToan.UseVisualStyleBackColor = true;
            // 
            // mnuCTPhuThu
            // 
            this.mnuCTPhuThu.Location = new System.Drawing.Point(26, 324);
            this.mnuCTPhuThu.Name = "mnuCTPhuThu";
            this.mnuCTPhuThu.Size = new System.Drawing.Size(108, 26);
            this.mnuCTPhuThu.TabIndex = 13;
            this.mnuCTPhuThu.Text = "CT Phụ thu";
            this.mnuCTPhuThu.UseVisualStyleBackColor = true;
            // 
            // mnuNhanVien
            // 
            this.mnuNhanVien.Location = new System.Drawing.Point(26, 289);
            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Size = new System.Drawing.Size(108, 30);
            this.mnuNhanVien.TabIndex = 12;
            this.mnuNhanVien.Text = "Nhân viên";
            this.mnuNhanVien.UseVisualStyleBackColor = true;
            // 
            // mnuSDDichVu
            // 
            this.mnuSDDichVu.Location = new System.Drawing.Point(26, 257);
            this.mnuSDDichVu.Name = "mnuSDDichVu";
            this.mnuSDDichVu.Size = new System.Drawing.Size(108, 26);
            this.mnuSDDichVu.TabIndex = 11;
            this.mnuSDDichVu.Text = "CT Dịch vụ";
            this.mnuSDDichVu.UseVisualStyleBackColor = true;
            this.mnuSDDichVu.Click += new System.EventHandler(this.mnuSDDichVu_Click);
            // 
            // mnuQLDichVu
            // 
            this.mnuQLDichVu.Location = new System.Drawing.Point(26, 221);
            this.mnuQLDichVu.Name = "mnuQLDichVu";
            this.mnuQLDichVu.Size = new System.Drawing.Size(108, 30);
            this.mnuQLDichVu.TabIndex = 10;
            this.mnuQLDichVu.Text = "QL Dịch vụ";
            this.mnuQLDichVu.UseVisualStyleBackColor = true;
            this.mnuQLDichVu.Click += new System.EventHandler(this.mnuQLDichVu_Click);
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Location = new System.Drawing.Point(26, 189);
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(108, 26);
            this.mnuKhachHang.TabIndex = 9;
            this.mnuKhachHang.Text = "Khách hàng";
            this.mnuKhachHang.UseVisualStyleBackColor = true;
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuKhachHang_Click);
            // 
            // mnuLoaiPhong
            // 
            this.mnuLoaiPhong.Location = new System.Drawing.Point(26, 154);
            this.mnuLoaiPhong.Name = "mnuLoaiPhong";
            this.mnuLoaiPhong.Size = new System.Drawing.Size(108, 30);
            this.mnuLoaiPhong.TabIndex = 8;
            this.mnuLoaiPhong.Text = "Loại phòng";
            this.mnuLoaiPhong.UseVisualStyleBackColor = true;
            this.mnuLoaiPhong.Click += new System.EventHandler(this.mnuLoaiPhong_Click);
            // 
            // mnuQLPhong
            // 
            this.mnuQLPhong.Location = new System.Drawing.Point(26, 122);
            this.mnuQLPhong.Name = "mnuQLPhong";
            this.mnuQLPhong.Size = new System.Drawing.Size(108, 26);
            this.mnuQLPhong.TabIndex = 7;
            this.mnuQLPhong.Text = "QL phòng";
            this.mnuQLPhong.UseVisualStyleBackColor = true;
            this.mnuQLPhong.Click += new System.EventHandler(this.mnuQLPhong_Click);
            // 
            // mnuDatPhong
            // 
            this.mnuDatPhong.Location = new System.Drawing.Point(26, 86);
            this.mnuDatPhong.Name = "mnuDatPhong";
            this.mnuDatPhong.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mnuDatPhong.Size = new System.Drawing.Size(108, 30);
            this.mnuDatPhong.TabIndex = 6;
            this.mnuDatPhong.Text = "Đặt phòng";
            this.mnuDatPhong.UseVisualStyleBackColor = true;
            this.mnuDatPhong.Click += new System.EventHandler(this.mnuDatPhong_Click);
            // 
            // picUser
            // 
            this.picUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picUser.BackgroundImage")));
            this.picUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picUser.Location = new System.Drawing.Point(192, 3);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(31, 36);
            this.picUser.TabIndex = 3;
            this.picUser.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUsername.Location = new System.Drawing.Point(228, 8);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(45, 19);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "admin";
            this.lblUsername.Click += new System.EventHandler(this.lblUsername_Click);
            // 
            // pnlAdminMenu
            // 
            this.pnlAdminMenu.Controls.Add(this.btnPhanQuyen);
            this.pnlAdminMenu.Controls.Add(this.btnDangXuat);
            this.pnlAdminMenu.Controls.Add(this.btnSuaThongTin);
            this.pnlAdminMenu.Controls.Add(this.pictureBox2);
            this.pnlAdminMenu.Location = new System.Drawing.Point(231, 26);
            this.pnlAdminMenu.Name = "pnlAdminMenu";
            this.pnlAdminMenu.Size = new System.Drawing.Size(129, 66);
            this.pnlAdminMenu.TabIndex = 5;
            this.pnlAdminMenu.Visible = false;
            // 
            // btnPhanQuyen
            // 
            this.btnPhanQuyen.Location = new System.Drawing.Point(0, 43);
            this.btnPhanQuyen.Name = "btnPhanQuyen";
            this.btnPhanQuyen.Size = new System.Drawing.Size(129, 23);
            this.btnPhanQuyen.TabIndex = 7;
            this.btnPhanQuyen.Text = "Phân quyền";
            this.btnPhanQuyen.UseVisualStyleBackColor = true;
            this.btnPhanQuyen.Click += new System.EventHandler(this.btnPhanQuyen_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(0, 22);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(129, 23);
            this.btnDangXuat.TabIndex = 7;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnSuaThongTin
            // 
            this.btnSuaThongTin.Location = new System.Drawing.Point(0, 0);
            this.btnSuaThongTin.Name = "btnSuaThongTin";
            this.btnSuaThongTin.Size = new System.Drawing.Size(129, 23);
            this.btnSuaThongTin.TabIndex = 6;
            this.btnSuaThongTin.Text = "Sửa thông tin";
            this.btnSuaThongTin.UseVisualStyleBackColor = true;
            this.btnSuaThongTin.Click += new System.EventHandler(this.btnSuaThongTin_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(-51, -23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(518, 416);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 422);
            this.Controls.Add(this.pnlAdminMenu);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.picUser);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.pnlAdminMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlAdminMenu;
        private System.Windows.Forms.Button btnPhanQuyen;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnSuaThongTin;
        private System.Windows.Forms.Button mnuThanhToan;
        private System.Windows.Forms.Button mnuCTPhuThu;
        private System.Windows.Forms.Button mnuNhanVien;
        private System.Windows.Forms.Button mnuSDDichVu;
        private System.Windows.Forms.Button mnuQLDichVu;
        private System.Windows.Forms.Button mnuKhachHang;
        private System.Windows.Forms.Button mnuLoaiPhong;
        private System.Windows.Forms.Button mnuQLPhong;
        private System.Windows.Forms.Button mnuDatPhong;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}