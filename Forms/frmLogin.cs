using HotelManagementSystem.Classes;
using HotelManagementSystem.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public partial class frmLogin : Form
    {
        public static string LoggedInUser = "";
        public static string LoggedInUserID = "";
        public static string UserRole = "";

        public frmLogin()
        {
            InitializeComponent();

            // ===== GÁN EVENTS TRONG CONSTRUCTOR =====
            this.Load += FrmLogin_Load;
            this.FormClosing += FrmLogin_FormClosing; // Gán event FormClosing
            txtPassword.KeyPress += TxtPassword_KeyPress;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (!DatabaseConnection.TestConnection())
            {
                MessageBox.Show("Không thể kết nối database!\nVui lòng kiểm tra lại cấu hình.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Query theo cấu trúc database mới
            string query = @"SELECT U.Username, U.TenHienThi, CV.TenCV 
                           FROM USERS U 
                           INNER JOIN CHUCVU CV ON U.IdChucVu = CV.IdChucVu 
                           WHERE U.Username = @Username AND U.Password = @Password";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                LoggedInUserID = dt.Rows[0]["Username"].ToString();
                LoggedInUser = dt.Rows[0]["TenHienThi"].ToString();
                UserRole = dt.Rows[0]["TenCV"].ToString();

                MessageBox.Show("Đăng nhập thành công!\nXin chào " + LoggedInUser,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                frmMain mainForm = new frmMain();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        // ===== EVENT HANDLER: FormClosing =====
        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // ===== EVENT HANDLER: KeyPress cho Password =====
        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}