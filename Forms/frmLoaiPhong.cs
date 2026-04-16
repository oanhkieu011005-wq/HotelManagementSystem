using HotelManagementSystem.Classes;
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
using HotelManagementSystem.Forms;

namespace HotelManagementSystem.Forms
{
    public partial class frmLoaiPhong : Form
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadData();
        }

        // Cấu hình DataGridView
        private void ConfigureDataGridView()
        {
            dgvLoaiPhong.AutoGenerateColumns = true;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.MultiSelect = false;
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.AllowUserToDeleteRows = false;
            dgvLoaiPhong.RowHeadersVisible = false;
            dgvLoaiPhong.BackgroundColor = System.Drawing.Color.White;

            // ===== QUAN TRỌNG: Gán event CellClick =====
            dgvLoaiPhong.CellClick += dgvLoaiPhong_CellClick;
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT MaLP, TenLP, SoNguoi FROM LOAIPHONG";
                DataTable dt = DatabaseConnection.ExecuteQuery(query);

                dgvLoaiPhong.DataSource = dt;

                if (dgvLoaiPhong.Columns.Count > 0)
                {
                    if (dgvLoaiPhong.Columns.Contains("MaLP"))
                    {
                        dgvLoaiPhong.Columns["MaLP"].HeaderText = "Mã loại";
                        dgvLoaiPhong.Columns["MaLP"].Width = 100;
                    }

                    if (dgvLoaiPhong.Columns.Contains("TenLP"))
                    {
                        dgvLoaiPhong.Columns["TenLP"].HeaderText = "Tên loại phòng";
                        dgvLoaiPhong.Columns["TenLP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    if (dgvLoaiPhong.Columns.Contains("SoNguoi"))
                    {
                        dgvLoaiPhong.Columns["SoNguoi"].HeaderText = "Số người";
                        dgvLoaiPhong.Columns["SoNguoi"].Width = 100;
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaLoai.Clear();
            txtLoaiPhong.Clear();
            txtSoNguoi.Clear();
            txtMaLoai.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoai.Text) ||
                string.IsNullOrEmpty(txtLoaiPhong.Text) ||
                string.IsNullOrEmpty(txtSoNguoi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soNguoi;
            if (!int.TryParse(txtSoNguoi.Text, out soNguoi))
            {
                MessageBox.Show("Số người phải là một con số!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO LOAIPHONG (MaLP, TenLP, SoNguoi) VALUES (@MaLP, @TenLP, @SoNguoi)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLP", txtMaLoai.Text.Trim()),
                new SqlParameter("@TenLP", txtLoaiPhong.Text.Trim()),
                new SqlParameter("@SoNguoi", soNguoi)
            };

            try
            {
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm loại phòng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Mã loại phòng này đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soNguoi;
            if (!int.TryParse(txtSoNguoi.Text, out soNguoi))
            {
                MessageBox.Show("Số người phải là một con số!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "UPDATE LOAIPHONG SET TenLP = @TenLP, SoNguoi = @SoNguoi WHERE MaLP = @MaLP";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLP", txtMaLoai.Text.Trim()),
                new SqlParameter("@TenLP", txtLoaiPhong.Text.Trim()),
                new SqlParameter("@SoNguoi", soNguoi)
            };

            int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại, không tìm thấy mã loại phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa?\nLưu ý: Bạn không thể xóa loại phòng nếu đang có phòng sử dụng loại này.",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM LOAIPHONG WHERE MaLP = @MaLP";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLP", txtMaLoai.Text.Trim())
                };

                try
                {
                    int deleteResult = DatabaseConnection.ExecuteNonQuery(query, parameters);
                    if (deleteResult > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa loại phòng này!\nĐang có phòng thuộc loại này.",
                            "Lỗi khóa ngoại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // ===== QUAN TRỌNG: Xử lý khi click vào dòng DataGridView =====
        private void dgvLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra có click vào dòng hợp lệ không
            if (e.RowIndex >= 0 && e.RowIndex < dgvLoaiPhong.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvLoaiPhong.Rows[e.RowIndex];

                    // Kiểm tra và gán giá trị vào các TextBox
                    if (row.Cells["MaLP"].Value != null)
                    {
                        txtMaLoai.Text = row.Cells["MaLP"].Value.ToString();
                    }

                    if (row.Cells["TenLP"].Value != null)
                    {
                        txtLoaiPhong.Text = row.Cells["TenLP"].Value.ToString();
                    }

                    if (row.Cells["SoNguoi"].Value != null)
                    {
                        txtSoNguoi.Text = row.Cells["SoNguoi"].Value.ToString();
                    }

                    // Đổi màu dòng được chọn (tùy chọn)
                    row.Selected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}