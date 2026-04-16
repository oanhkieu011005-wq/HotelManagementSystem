using System;
using System.Windows.Forms;
using System.Linq;
using HotelManagementSystem.Forms;

namespace HotelManagementSystem.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            lblUsername.Text = frmLogin.LoggedInUser;
            pnlAdminMenu.Visible = false;

            // ===== ẨN CÁC MENU CHƯA LÀM =====
            //HideUnfinishedMenus();

            // Phân quyền theo chức vụ
            switch (frmLogin.UserRole)
            {
                case "Admin":
                    // Admin thấy tất cả form đã làm
                    break;
                case "Kế toán":
                    btnPhanQuyen.Visible = false;
                    break;
                case "Lễ tân":
                    btnPhanQuyen.Visible = false;
                    // Nếu có menu nhân viên thì ẩn
                    if (mnuNhanVien != null) mnuNhanVien.Visible = false;
                    break;
                default:
                    btnPhanQuyen.Visible = false;
                    break;
            }
        }

        // ===== HELPER: ẨN CÁC MENU CHƯA HOÀN THÀNH =====
        //private void HideUnfinishedMenus()
        //{
        //    // Ẩn các menu chưa làm (nếu tồn tại trong designer)
        //    try
        //    {
        //        // Ẩn menu Khách hàng
        //        if (mnuKhachHang != null) mnuKhachHang.Visible = false;

        //        // Ẩn menu QL Dịch vụ
        //        if (mnuQLDichVu != null) mnuQLDichVu.Visible = false;

        //        // Ẩn menu Sử dụng Dịch vụ
        //        if (mnuSDDichVu != null) mnuSDDichVu.Visible = false;

        //        // Ẩn menu Nhân viên
        //        if (mnuNhanVien != null) mnuNhanVien.Visible = false;

        //        // Ẩn menu Phụ thu
        //        //if (mnuPhuThu != null) mnuPhuThu.Visible = false;

        //        // Ẩn menu Thanh toán
        //        if (mnuThanhToan != null) mnuThanhToan.Visible = false;
        //    }
        //    catch { }
        //}

        // ===== HELPER METHOD: Mở form và ẩn frmMain =====
        private void OpenFormAndHideMain<T>() where T : Form, new()
        {
            var existingForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (existingForm != null)
            {
                existingForm.Activate();
                existingForm.BringToFront();
            }
            else
            {
                T newForm = new T();
                this.Hide();

                newForm.FormClosed += (s, args) =>
                {
                    this.Show();
                    this.Activate();
                };

                newForm.Show();
            }
        }

        // ===== MENU EVENTS - CHỈ CÁC FORM ĐÃ LÀM =====

        // ✅ Form Đặt phòng (ĐÃ HOÀN THÀNH)
        private void mnuDatPhong_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmDatPhong>();
        }

        // ✅ Form Quản lý Phòng (ĐÃ HOÀN THÀNH)
        private void mnuQLPhong_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmQLPhong>();
        }

        // ✅ Form Loại phòng (ĐÃ HOÀN THÀNH)
        private void mnuLoaiPhong_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmLoaiPhong>();
        }

        // ❌ CÁC MENU CHƯA LÀM - ĐÃ COMMENT (Bỏ comment khi đã tạo form)

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmKhachHang>();
        }

        private void mnuQLDichVu_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmQLDichVu>();
        }

        private void mnuSDDichVu_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmSDDichVu>();
        }

        //private void mnuNhanVien_Click(object sender, EventArgs e)
        //{
        //    OpenFormAndHideMain<frmNhanVien>();
        //}

        //private void mnuPhuThu_Click(object sender, EventArgs e)
        //{
        //    OpenFormAndHideMain<frmPhuThu>();
        //}

        //private void mnuThanhToan_Click(object sender, EventArgs e)
        //{
        //    OpenFormAndHideMain<frmThanhToan>();
        //}


        // ===== ADMIN MENU =====

        private void lblUsername_Click(object sender, EventArgs e)
        {
            pnlAdminMenu.Visible = !pnlAdminMenu.Visible;
        }

        // ✅ Form Sửa thông tin (ĐÃ HOÀN THÀNH)
        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmSuaThongTin>();
            pnlAdminMenu.Visible = false;
        }

        // ✅ Form Phân quyền (ĐÃ HOÀN THÀNH)
        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            OpenFormAndHideMain<frmPhanQuyen>();
            pnlAdminMenu.Visible = false;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                frmLogin.LoggedInUser = "";
                frmLogin.LoggedInUserID = "";
                frmLogin.UserRole = "";

                frmLogin loginForm = new frmLogin();
                loginForm.Show();
                this.Close();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (pnlAdminMenu.Visible)
                pnlAdminMenu.Visible = false;
        }
    }
}
