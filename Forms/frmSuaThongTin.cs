using HotelManagementSystem.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementSystem.Forms
{
    public partial class frmSuaThongTin : Form
    {
        public frmSuaThongTin()
        {
            InitializeComponent();
            LoadUserInfo();

            // Set PasswordChar cho các textbox mật khẩu
            txtMatKhauCu.PasswordChar = '●';
            txtMatKhauMoi.PasswordChar = '●';
            txtXacNhanMK.PasswordChar = '●';
        }

        private void LoadUserInfo()
        {
            try
            {
                // Hiển thị thông tin user hiện tại
                txtUserName.Text = frmLogin.LoggedInUserID;
                txtTenHienThi.Text = frmLogin.LoggedInUser;
                txtChucVu.Text = frmLogin.UserRole;

                // Load thông tin chi tiết từ USERS
                string query = @"SELECT U.TenHienThi, CV.TenCV 
                               FROM USERS U
                               INNER JOIN CHUCVU CV ON U.IdChucVu = CV.IdChucVu
                               WHERE U.Username = @Username";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", frmLogin.LoggedInUserID)
                };

                DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    txtTenHienThi.Text = dt.Rows[0]["TenHienThi"].ToString();
                    txtChucVu.Text = dt.Rows[0]["TenCV"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtMatKhauCu.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (txtMatKhauMoi.Text.Length < 4)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 4 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (txtMatKhauMoi.Text != txtXacNhanMK.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMK.Focus();
                return;
            }

            try
            {
                // Kiểm tra mật khẩu cũ
                string queryCheck = "SELECT COUNT(*) FROM USERS WHERE Username = @Username AND Password = @Password";
                SqlParameter[] paramCheck = new SqlParameter[]
                {
                    new SqlParameter("@Username", frmLogin.LoggedInUserID),
                    new SqlParameter("@Password", txtMatKhauCu.Text.Trim())
                };

                object result = DatabaseConnection.ExecuteScalar(queryCheck, paramCheck);
                int count = Convert.ToInt32(result);

                if (count == 0)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Clear();
                    txtMatKhauCu.Focus();
                    return;
                }

                // Cập nhật mật khẩu mới
                string queryUpdate = "UPDATE USERS SET Password = @NewPassword WHERE Username = @Username";
                SqlParameter[] paramUpdate = new SqlParameter[]
                {
                    new SqlParameter("@NewPassword", txtMatKhauMoi.Text.Trim()),
                    new SqlParameter("@Username", frmLogin.LoggedInUserID)
                };

                int updateResult = DatabaseConnection.ExecuteNonQuery(queryUpdate, paramUpdate);
                if (updateResult > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearPasswordFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearPasswordFields();
        }

        private void ClearPasswordFields()
        {
            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();
            txtXacNhanMK.Clear();
            txtMatKhauCu.Focus();
        }

        // Checkbox hiển thị mật khẩu
        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked)
            {
                txtMatKhauCu.PasswordChar = '\0';
                txtMatKhauMoi.PasswordChar = '\0';
                txtXacNhanMK.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauCu.PasswordChar = '●';
                txtMatKhauMoi.PasswordChar = '●';
                txtXacNhanMK.PasswordChar = '●';
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
