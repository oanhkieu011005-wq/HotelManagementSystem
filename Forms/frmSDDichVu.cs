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
    public partial class frmSDDichVu : Form
    {
        public frmSDDichVu()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadComboBoxes();
            LoadData();
            ClearInputs();
        }
        private void ConfigureDataGridView()
        {
            dgvSDDichVu.AutoGenerateColumns = true;
            dgvSDDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSDDichVu.MultiSelect = false;
            dgvSDDichVu.ReadOnly = true;
            dgvSDDichVu.AllowUserToAddRows = false;
            dgvSDDichVu.RowHeadersVisible = false;
            dgvSDDichVu.BackgroundColor = System.Drawing.Color.White;
            dgvSDDichVu.CellClick += dgvSDDichVu_CellClick;
        }
        private void LoadComboBoxes()
        {
            try
            {
                // Load Mã đặt phòng (cmbMaDatPhong)
                string queryDatPhong = "SELECT idDatPhong FROM DATPHONG";
                DataTable dtDatPhong = DatabaseConnection.ExecuteQuery(queryDatPhong);
                cmbMaDatPhong.DataSource = dtDatPhong;
                cmbMaDatPhong.DisplayMember = "idDatPhong";
                cmbMaDatPhong.ValueMember = "idDatPhong";
                cmbMaDatPhong.SelectedIndex = -1;
                // Load tên dịch vụ (cmbTenDichVu)
                string queryDichVu = "SELECT MaDV, TenDV FROM DICHVU";
                DataTable dtDichVu = DatabaseConnection.ExecuteQuery(queryDichVu);
                cmbTenDichVu.DataSource = dtDichVu;
                cmbTenDichVu.DisplayMember = "TenDV";
                cmbTenDichVu.ValueMember = "MaDV";
                cmbTenDichVu.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải combobox: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadData()
        {
            try
            {
                // Hiển thị dữ liệu sử dụng dịch vụ cùng tên dịch vụ và mã đặt phòng
                string query = @"
                    SELECT S.idSDDV, S.idDatPhong, S.MaDV, D.TenDV, S.SoLuong, S.NgaySD
                    FROM SDDICHVU S
                    INNER JOIN DICHVU D ON S.MaDV = D.MaDV";
                DataTable dt = DatabaseConnection.ExecuteQuery(query);
                dgvSDDichVu.DataSource = dt;
                if (dgvSDDichVu.Columns.Count > 0)
                {
                    dgvSDDichVu.Columns["idSDDV"].HeaderText = "Mã SDDV";
                    dgvSDDichVu.Columns["idSDDV"].Width = 100;

                    dgvSDDichVu.Columns["idDatPhong"].HeaderText = "Mã đặt phòng";
                    dgvSDDichVu.Columns["idDatPhong"].Width = 100;

                    dgvSDDichVu.Columns["MaDV"].Visible = false; // Ẩn cột mã dịch vụ, dùng cột TenDV hiển thị

                    dgvSDDichVu.Columns["TenDV"].HeaderText = "Tên dịch vụ";
                    dgvSDDichVu.Columns["TenDV"].Width = 180;

                    dgvSDDichVu.Columns["SoLuong"].HeaderText = "Số lượng sử dụng";
                    dgvSDDichVu.Columns["SoLuong"].Width = 100;

                    dgvSDDichVu.Columns["NgaySD"].HeaderText = "Ngày sử dụng";
                    dgvSDDichVu.Columns["NgaySD"].Width = 130;

                    dgvSDDichVu.Columns["NgaySD"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txtMaSDDV.Clear();
            cmbMaDatPhong.SelectedIndex = -1;
            cmbTenDichVu.SelectedIndex = -1;
            txtSoLuong.Clear();
            dtpNgaySD.Value = DateTime.Now;
            txtMaSDDV.Enabled = true;
            txtMaSDDV.Focus();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSDDV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sử dụng dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSDDV.Focus();
                return;
            }
            if (cmbMaDatPhong.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Mã đặt phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaDatPhong.Focus();
                return;
            }
            if (cmbTenDichVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Tên dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTenDichVu.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng hợp lệ (số nguyên > 0)!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            string query = @"INSERT INTO SDDICHVU (idSDDV, idDatPhong, MaDV, SoLuong, NgaySD)
                             VALUES (@idSDDV, @idDatPhong, @MaDV, @SoLuong, @NgaySD)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idSDDV", txtMaSDDV.Text.Trim()),
                new SqlParameter("@idDatPhong", cmbMaDatPhong.SelectedValue),
                new SqlParameter("@MaDV", cmbTenDichVu.SelectedValue),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@NgaySD", dtpNgaySD.Value)
            };
            try
            {
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm sử dụng dịch vụ thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // trùng khóa chính
                {
                    MessageBox.Show("Mã sử dụng dịch vụ đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaSDDV.Focus();
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
            if (string.IsNullOrWhiteSpace(txtMaSDDV.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbMaDatPhong.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Mã đặt phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaDatPhong.Focus();
                return;
            }
            if (cmbTenDichVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Tên dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTenDichVu.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng hợp lệ (số nguyên > 0)!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            string query = @"UPDATE SDDICHVU SET
                             idDatPhong = @idDatPhong,
                             MaDV = @MaDV,
                             SoLuong = @SoLuong,
                             NgaySD = @NgaySD
                             WHERE idSDDV = @idSDDV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idSDDV", txtMaSDDV.Text.Trim()),
                new SqlParameter("@idDatPhong", cmbMaDatPhong.SelectedValue),
                new SqlParameter("@MaDV", cmbTenDichVu.SelectedValue),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@NgaySD", dtpNgaySD.Value)
            };
            int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật sử dụng dịch vụ thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Không tìm thấy bản ghi để cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSDDV.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa bản ghi này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string query = "DELETE FROM SDDICHVU WHERE idSDDV = @idSDDV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@idSDDV", txtMaSDDV.Text.Trim())
                };
                try
                {
                    int delResult = DatabaseConnection.ExecuteNonQuery(query, parameters);
                    if (delResult > 0)
                    {
                        MessageBox.Show("Xóa bản ghi thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi để xóa.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa bản ghi này vì đang được tham chiếu.", "Lỗi khóa ngoại",
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
        private void dgvSDDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSDDichVu.Rows[e.RowIndex];
                txtMaSDDV.Text = row.Cells["idSDDV"].Value?.ToString() ?? "";
                // Set cmbMaDatPhong
                object valDatPhong = row.Cells["idDatPhong"].Value;
                if (valDatPhong != null && valDatPhong != DBNull.Value)
                    cmbMaDatPhong.SelectedValue = valDatPhong.ToString();
                else
                    cmbMaDatPhong.SelectedIndex = -1;
                // Set cmbTenDichVu
                object valMaDV = row.Cells["MaDV"].Value;
                if (valMaDV != null && valMaDV != DBNull.Value)
                    cmbTenDichVu.SelectedValue = valMaDV.ToString();
                else
                    cmbTenDichVu.SelectedIndex = -1;
                txtSoLuong.Text = row.Cells["SoLuong"].Value?.ToString() ?? "";
                DateTime ngaySD;
                if (DateTime.TryParse(row.Cells["NgaySD"].Value?.ToString(), out ngaySD))
                    dtpNgaySD.Value = ngaySD;
                else
                    dtpNgaySD.Value = DateTime.Now;
                txtMaSDDV.Enabled = false; // khóa mã khi chỉnh sửa
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
