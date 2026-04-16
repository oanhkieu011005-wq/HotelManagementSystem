using HotelManagementSystem.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementSystem.Forms
{
    public partial class frmPhanQuyen : Form
    {
        public frmPhanQuyen()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadData();
            LoadComboBox();

            txtPassword.PasswordChar = '●';
        }

        private void ConfigureDataGridView()
        {
            dgvUsers.AutoGenerateColumns = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.BackgroundColor = System.Drawing.Color.White;
            dgvUsers.CellClick += dgvUsers_CellClick;
        }

        private void LoadData()
        {
            try
            {
                string query = @"SELECT U.Username, U.TenHienThi, U.Password, CV.TenCV
                               FROM USERS U
                               INNER JOIN CHUCVU CV ON U.IdChucVu = CV.IdChucVu
                               ORDER BY U.Username";

                DataTable dt = DatabaseConnection.ExecuteQuery(query);
                dgvUsers.DataSource = dt;

                if (dgvUsers.Columns.Count > 0)
                {
                    dgvUsers.Columns["Username"].HeaderText = "Username";
                    dgvUsers.Columns["Username"].Width = 100;

                    dgvUsers.Columns["TenHienThi"].HeaderText = "Tên hiển thị";
                    dgvUsers.Columns["TenHienThi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvUsers.Columns["Password"].HeaderText = "Password";
                    dgvUsers.Columns["Password"].Width = 100;

                    dgvUsers.Columns["TenCV"].HeaderText = "Chức vụ";
                    dgvUsers.Columns["TenCV"].Width = 120;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách users: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBox()
        {
            try
            {
                string query = "SELECT IdChucVu, TenCV FROM CHUCVU";
                DataTable dt = DatabaseConnection.ExecuteQuery(query);

                cmbChucVu.DataSource = dt;
                cmbChucVu.DisplayMember = "TenCV";
                cmbChucVu.ValueMember = "IdChucVu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chức vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtUsername.Clear();
            txtTenhienthi.Clear();
            txtPassword.Clear();
            cmbChucVu.SelectedIndex = -1;
            txtUsername.Focus();

            // Enable username cho chế độ thêm mới
            txtUsername.ReadOnly = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập Username!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenhienthi.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên hiển thị!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenhienthi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập Password!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text.Length < 4)
            {
                MessageBox.Show("Password phải có ít nhất 4 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (cmbChucVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Chức vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbChucVu.Focus();
                return;
            }

            // Kiểm tra trùng Username
            string queryCheck = "SELECT COUNT(*) FROM USERS WHERE Username = @Username";
            SqlParameter[] paramCheck = new SqlParameter[]
            {
                new SqlParameter("@Username", txtUsername.Text.Trim())
            };

            object checkResult = DatabaseConnection.ExecuteScalar(queryCheck, paramCheck);
            if (Convert.ToInt32(checkResult) > 0)
            {
                MessageBox.Show("Username này đã tồn tại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }

            // Insert
            string query = "INSERT INTO USERS (Username, TenHienThi, Password, IdChucVu) VALUES (@Username, @TenHienThi, @Password, @IdChucVu)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", txtUsername.Text.Trim()),
                new SqlParameter("@TenHienThi", txtTenhienthi.Text.Trim()),
                new SqlParameter("@Password", txtPassword.Text.Trim()),
                new SqlParameter("@IdChucVu", cmbChucVu.SelectedValue)
            };

            try
            {
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm user thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm user: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng chọn user cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenhienthi.Text) || cmbChucVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Không cho phép sửa thông tin của chính mình
            if (txtUsername.Text.Trim() == frmLogin.LoggedInUserID)
            {
                MessageBox.Show("Không thể sửa thông tin của chính mình!\nVui lòng sử dụng 'Sửa thông tin' từ menu.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc muốn cập nhật thông tin user này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                // Nếu có nhập password mới thì update cả password
                string query;
                SqlParameter[] parameters;

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (txtPassword.Text.Length < 4)
                    {
                        MessageBox.Show("Password phải có ít nhất 4 ký tự!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    query = "UPDATE USERS SET TenHienThi = @TenHienThi, Password = @Password, IdChucVu = @IdChucVu WHERE Username = @Username";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@TenHienThi", txtTenhienthi.Text.Trim()),
                        new SqlParameter("@Password", txtPassword.Text.Trim()),
                        new SqlParameter("@IdChucVu", cmbChucVu.SelectedValue),
                        new SqlParameter("@Username", txtUsername.Text.Trim())
                    };
                }
                else
                {
                    // Chỉ update tên và chức vụ
                    query = "UPDATE USERS SET TenHienThi = @TenHienThi, IdChucVu = @IdChucVu WHERE Username = @Username";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@TenHienThi", txtTenhienthi.Text.Trim()),
                        new SqlParameter("@IdChucVu", cmbChucVu.SelectedValue),
                        new SqlParameter("@Username", txtUsername.Text.Trim())
                    };
                }

                try
                {
                    int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng chọn user cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Không cho phép xóa chính mình
            if (txtUsername.Text.Trim() == frmLogin.LoggedInUserID)
            {
                MessageBox.Show("Không thể xóa tài khoản của chính mình!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn XÓA user '{txtUsername.Text}'?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM USERS WHERE Username = @Username";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", txtUsername.Text.Trim())
                };

                try
                {
                    int deleteResult = DatabaseConnection.ExecuteNonQuery(query, parameters);
                    if (deleteResult > 0)
                    {
                        MessageBox.Show("Xóa user thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa user này!\nUser đang được tham chiếu trong dữ liệu khác.",
                            "Lỗi khóa ngoại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvUsers.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

                    txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
                    txtTenhienthi.Text = row.Cells["TenHienThi"].Value?.ToString() ?? "";
                    txtPassword.Text = row.Cells["Password"].Value?.ToString() ?? "";

                    // Set ComboBox theo TenCV
                    string tenCV = row.Cells["TenCV"].Value?.ToString() ?? "";
                    for (int i = 0; i < cmbChucVu.Items.Count; i++)
                    {
                        DataRowView item = (DataRowView)cmbChucVu.Items[i];
                        if (item["TenCV"].ToString() == tenCV)
                        {
                            cmbChucVu.SelectedIndex = i;
                            break;
                        }
                    }

                    // Disable username khi đang sửa
                    txtUsername.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Checkbox hiển thị password
        private void chkHienPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkHienPassword.Checked ? '\0' : '●';
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
