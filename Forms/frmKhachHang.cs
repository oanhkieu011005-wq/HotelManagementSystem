using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using HotelManagementSystem.Classes;

namespace HotelManagementSystem.Forms
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadData();
            ClearInputs();
        }
        private void ConfigureDataGridView()
        {
            dgvKhachHang.AutoGenerateColumns = true;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
        }
        private void LoadData()
        {
            try
            {
                string query = @"SELECT MaKH, HoTen, NgaySinh, 
                                CASE WHEN GioiTinh = 'M' THEN 'Nam' ELSE 'Nữ' END AS GioiTinh, 
                                CCCD, Sdt, Email
                                FROM KHACHHANG ";
                DataTable dt = DatabaseConnection.ExecuteQuery(query);
                dgvKhachHang.DataSource = dt;
                if (dgvKhachHang.Columns.Count > 0)
                {
                    dgvKhachHang.Columns["MaKH"].HeaderText = "Mã khách hàng";
                    dgvKhachHang.Columns["MaKH"].Width = 100;

                    dgvKhachHang.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvKhachHang.Columns["HoTen"].Width = 150;

                    dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvKhachHang.Columns["NgaySinh"].Width = 100;
                    dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới tính";
                    dgvKhachHang.Columns["GioiTinh"].Width = 70;

                    dgvKhachHang.Columns["CCCD"].HeaderText = "CMND/CCCD";
                    dgvKhachHang.Columns["CCCD"].Width = 120;

                    dgvKhachHang.Columns["Sdt"].HeaderText = "Số điện thoại";
                    dgvKhachHang.Columns["Sdt"].Width = 120;

                    dgvKhachHang.Columns["Email"].HeaderText = "Email";
                    dgvKhachHang.Columns["Email"].Width = 180;
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
            txtMaKH.Clear();
            txtHoTen.Clear();
            rdoNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Today;
            txtSDT.Clear();
            txtEmail.Clear();
            txtCCCD.Clear();
            txtMaKH.Enabled = true; // Cho phép nhập mã mới khi thêm
            txtMaKH.Focus();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            // Các dữ liệu khác có thể optional hoặc thêm kiểm tra tuỳ bạn
            try
            {
                string query = @"INSERT INTO KHACHHANG (MaKH, HoTen, NgaySinh, GioiTinh, CCCD, Sdt, Email) 
                                VALUES (@MaKH, @HoTen, @NgaySinh, @GioiTinh, @CCCD, @Sdt, @Email)";

                string gioiTinh = rdoNam.Checked ? "M" : "F";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", txtMaKH.Text.Trim()),
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@GioiTinh", gioiTinh),
                    new SqlParameter("@CCCD", txtCCCD.Text.Trim()),
                    new SqlParameter("@Sdt", txtSDT.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim())
                };
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Trùng khoá chính
                {
                    MessageBox.Show("Mã khách hàng này đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaKH.Focus();
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
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            string gioiTinh = rdoNam.Checked ? "M" : "F";
            try
            {
                string query = @"UPDATE KHACHHANG SET 
                                 HoTen = @HoTen,
                                 NgaySinh = @NgaySinh,
                                 GioiTinh = @GioiTinh,
                                 CCCD = @CCCD,
                                 Sdt = @Sdt,
                                 Email = @Email
                                 WHERE MaKH = @MaKH";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", txtMaKH.Text.Trim()),
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@GioiTinh", gioiTinh),
                    new SqlParameter("@CCCD", txtCCCD.Text.Trim()),
                    new SqlParameter("@Sdt", txtSDT.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim())
                };
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để cập nhật.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string query = "DELETE FROM KHACHHANG WHERE MaKH = @MaKH";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", txtMaKH.Text.Trim())
                };

                try
                {
                    int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Khóa ngoại
                    {
                        MessageBox.Show("Không thể xóa khách hàng này.\nKhách hàng đang được tham chiếu.", "Lỗi khóa ngoại",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                string gioiTinhStr = row.Cells["GioiTinh"].Value?.ToString() ?? "Nam";
                rdoNam.Checked = gioiTinhStr == "Nam";
                rdoNu.Checked = gioiTinhStr == "Nữ";
                DateTime ngaySinh;
                if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Today;
                }

                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["Sdt"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtMaKH.Enabled = false; // Khóa mã khách hàng khi chỉnh sửa hoặc xóa
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
