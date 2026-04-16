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
    public partial class frmQLDichVu : Form
    {
        public frmQLDichVu()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadData();
            ClearInputs();
        }
        private void ConfigureDataGridView()
        {
            dgvDichVu.AutoGenerateColumns = true;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.MultiSelect = false;
            dgvDichVu.ReadOnly = true;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.RowHeadersVisible = false;
            dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            dgvDichVu.CellClick += dgvDichVu_CellClick;
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT MaDV, TenDV, Gia FROM DICHVU";
                DataTable dt = DatabaseConnection.ExecuteQuery(query);
                dgvDichVu.DataSource = dt;
                if (dgvDichVu.Columns.Count > 0)
                {
                    dgvDichVu.Columns["MaDV"].HeaderText = "Mã dịch vụ";
                    dgvDichVu.Columns["MaDV"].Width = 100;
                    dgvDichVu.Columns["TenDV"].HeaderText = "Tên dịch vụ";
                    dgvDichVu.Columns["TenDV"].Width = 180;
                    dgvDichVu.Columns["Gia"].HeaderText = "Giá";
                    dgvDichVu.Columns["Gia"].Width = 120;
                    dgvDichVu.Columns["Gia"].DefaultCellStyle.Format = "N0"; // Định dạng số không có phần thập phân
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
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtGia.Clear();
            txtMaDV.Enabled = true;
            txtMaDV.Focus();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDV.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtGia.Text))
            {
                MessageBox.Show("Vui lòng nhập Giá dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }
            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số không âm!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGia.Focus();
                return;
            }
            string query = "INSERT INTO DICHVU (MaDV, TenDV, Gia) VALUES (@MaDV, @TenDV, @Gia)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDV", txtMaDV.Text.Trim()),
                new SqlParameter("@TenDV", txtTenDV.Text.Trim()),
                new SqlParameter("@Gia", gia)
            };
            try
            {
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Mã dịch vụ đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaDV.Focus();
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
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtGia.Text))
            {
                MessageBox.Show("Vui lòng nhập Giá dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }
            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số không âm!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGia.Focus();
                return;
            }
            string query = "UPDATE DICHVU SET TenDV = @TenDV, Gia = @Gia WHERE MaDV = @MaDV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDV", txtMaDV.Text.Trim()),
                new SqlParameter("@TenDV", txtTenDV.Text.Trim()),
                new SqlParameter("@Gia", gia)
            };
            int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã dịch vụ để cập nhật.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string query = "DELETE FROM DICHVU WHERE MaDV = @MaDV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaDV", txtMaDV.Text.Trim())
                };
                try
                {
                    int delResult = DatabaseConnection.ExecuteNonQuery(query, parameters);
                    if (delResult > 0)
                    {
                        MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dịch vụ để xóa.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // khóa ngoại
                    {
                        MessageBox.Show("Không thể xóa dịch vụ này.\nDịch vụ đang được sử dụng.", "Lỗi khóa ngoại",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];
                txtMaDV.Text = row.Cells["MaDV"].Value?.ToString() ?? "";
                txtTenDV.Text = row.Cells["TenDV"].Value?.ToString() ?? "";
                txtGia.Text = row.Cells["Gia"].Value?.ToString() ?? "";
                txtMaDV.Enabled = false; // Khóa mã dịch vụ khi sửa hoặc xóa
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
