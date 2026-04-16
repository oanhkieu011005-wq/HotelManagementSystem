using HotelManagementSystem.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;

namespace HotelManagementSystem.Forms
{
    public partial class frmDatPhong : Form
    {
        private string oldMaP = ""; // Lưu mã phòng cũ khi sửa

        public frmDatPhong()
        {
            InitializeComponent();
            ConfigureDataGridViews();
            LoadMainData();
            LoadHelperData();
        }

        private void ConfigureDataGridViews()
        {
            // Cấu hình DGV Đặt Phòng (chính)
            dgvDatPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatPhong.MultiSelect = false;
            dgvDatPhong.ReadOnly = true;
            dgvDatPhong.AllowUserToAddRows = false;
            dgvDatPhong.AllowUserToDeleteRows = false;
            dgvDatPhong.RowHeadersVisible = false;
            dgvDatPhong.CellClick += dgvDatPhong_CellClick;

            // Cấu hình DGV Phòng (phụ)
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.MultiSelect = false;
            dgvPhong.ReadOnly = true;
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.AllowUserToDeleteRows = false;
            dgvPhong.RowHeadersVisible = false;
            dgvPhong.CellClick += dgvPhong_CellClick;
        }

        private void LoadMainData()
        {
            try
            {
                string query = "SELECT * FROM DATPHONG";
                DataTable dt = DatabaseConnection.ExecuteQuery(query);
                dgvDatPhong.DataSource = dt;

                if (dgvDatPhong.Columns.Count > 0)
                {
                    dgvDatPhong.Columns["idDatPhong"].Width = 80;
                    dgvDatPhong.Columns["MaP"].Width = 80;
                    dgvDatPhong.Columns["MaKH"].Width = 80;

                    dgvDatPhong.Columns["NgayCheckIn"].HeaderText = "Ngày Check-in";
                    dgvDatPhong.Columns["NgayCheckIn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvDatPhong.Columns["NgayCheckOut"].HeaderText = "Ngày Check-out";
                    dgvDatPhong.Columns["NgayCheckOut"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvDatPhong.Columns["NgayDat"].Visible = false;
                    dgvDatPhong.Columns["SoNguoi"].Visible = false;
                    dgvDatPhong.Columns["DatCoc"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHelperData()
        {
            try
            {
                // Tải DGV Phòng (trên)
                string queryPhong = @"SELECT p.MaP, lp.TenLP, lp.SoNguoi, tt.TrangThai 
                                    FROM PHONG p
                                    JOIN LOAIPHONG lp ON p.MaLP = lp.MaLP
                                    JOIN TTPHONG tt ON p.idTrangThai = tt.idTrangThai";
                DataTable dtPhong = DatabaseConnection.ExecuteQuery(queryPhong);
                dgvPhong.DataSource = dtPhong;

                if (dgvPhong.Columns.Count > 0)
                {
                    dgvPhong.Columns["MaP"].HeaderText = "Số Phòng";
                    dgvPhong.Columns["TenLP"].HeaderText = "Tên Loại Phòng";
                    dgvPhong.Columns["SoNguoi"].HeaderText = "Số Người";
                    dgvPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";
                }

                // Tải ComboBox Mã Khách Hàng
                string queryKH = "SELECT MaKH, HoTen FROM KHACHHANG";
                DataTable dtKH = DatabaseConnection.ExecuteQuery(queryKH);
                cmbMaKH.DataSource = dtKH;
                cmbMaKH.DisplayMember = "MaKH";
                cmbMaKH.ValueMember = "MaKH";

                // Tải ComboBox Số Phòng - CHỈ lấy phòng TRỐNG
                LoadAvailableRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAvailableRooms()
        {
            try
            {
                string querySoPhong = "SELECT MaP FROM PHONG WHERE idTrangThai = 'TT03'"; // TT03 = Trống
                DataTable dtSoPhong = DatabaseConnection.ExecuteQuery(querySoPhong);
                cmbSoPhong.DataSource = dtSoPhong;
                cmbSoPhong.DisplayMember = "MaP";
                cmbSoPhong.ValueMember = "MaP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng trống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllRoomsForEdit()
        {
            try
            {
                // Load TẤT CẢ phòng khi đang ở chế độ sửa
                string querySoPhong = "SELECT MaP FROM PHONG";
                DataTable dtSoPhong = DatabaseConnection.ExecuteQuery(querySoPhong);
                cmbSoPhong.DataSource = dtSoPhong;
                cmbSoPhong.DisplayMember = "MaP";
                cmbSoPhong.ValueMember = "MaP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaDatPhong.Clear();
            txtSoNguoi.Clear();
            txtDatCoc.Clear();
            oldMaP = ""; // Reset mã phòng cũ

            cmbMaKH.SelectedIndex = -1;
            cmbSoPhong.SelectedIndex = -1;

            dtpNgayDat.Value = DateTime.Now;
            dtpNgayCheckIn.Value = DateTime.Now.Date;
            dtpNgayCheckOut.Value = DateTime.Now.Date.AddDays(1);

            // Load lại phòng trống cho ComboBox
            LoadAvailableRooms();
            txtMaDatPhong.Focus();
        }

        private bool CheckDuplicateBookingID(string idDatPhong)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM DATPHONG WHERE idDatPhong = @idDatPhong";
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@idDatPhong", idDatPhong) };
                object result = DatabaseConnection.ExecuteScalar(query, param);
                return Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        // Nút Đặt (Thêm)
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu
            if (string.IsNullOrWhiteSpace(txtMaDatPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã đặt phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDatPhong.Focus();
                return;
            }

            if (cmbMaKH.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaKH.Focus();
                return;
            }

            if (cmbSoPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Số phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSoPhong.Focus();
                return;
            }

            // Kiểm tra trùng Mã đặt phòng
            if (CheckDuplicateBookingID(txtMaDatPhong.Text.Trim()))
            {
                MessageBox.Show("Mã đặt phòng này đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDatPhong.Focus();
                return;
            }

            // Kiểm tra ngày
            if (dtpNgayCheckOut.Value <= dtpNgayCheckIn.Value)
            {
                MessageBox.Show("Ngày check-out phải sau ngày check-in!", "Lỗi ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soNguoi = 0;
            decimal datCoc = 0;

            if (!string.IsNullOrWhiteSpace(txtSoNguoi.Text))
            {
                if (!int.TryParse(txtSoNguoi.Text, out soNguoi) || soNguoi < 1)
                {
                    MessageBox.Show("Số người phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoNguoi.Focus();
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtDatCoc.Text))
            {
                if (!decimal.TryParse(txtDatCoc.Text, out datCoc) || datCoc < 0)
                {
                    MessageBox.Show("Đặt cọc phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDatCoc.Focus();
                    return;
                }
            }

            string query = @"INSERT INTO DATPHONG (idDatPhong, MaP, MaKH, NgayDat, NgayCheckIn, NgayCheckOut, SoNguoi, DatCoc) 
                             VALUES (@idDatPhong, @MaP, @MaKH, @NgayDat, @NgayCheckIn, @NgayCheckOut, @SoNguoi, @DatCoc)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idDatPhong", txtMaDatPhong.Text.Trim()),
                new SqlParameter("@MaP", cmbSoPhong.SelectedValue.ToString()),
                new SqlParameter("@MaKH", cmbMaKH.SelectedValue.ToString()),
                new SqlParameter("@NgayDat", dtpNgayDat.Value),
                new SqlParameter("@NgayCheckIn", dtpNgayCheckIn.Value),
                new SqlParameter("@NgayCheckOut", dtpNgayCheckOut.Value),
                new SqlParameter("@SoNguoi", soNguoi),
                new SqlParameter("@DatCoc", datCoc)
            };

            try
            {
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    // Cập nhật trạng thái phòng thành 'Đã đặt' (TT02)
                    string queryUpdatePhong = "UPDATE PHONG SET idTrangThai = 'TT02' WHERE MaP = @MaP";
                    SqlParameter[] paramPhong = new SqlParameter[]
                    {
                        new SqlParameter("@MaP", cmbSoPhong.SelectedValue.ToString())
                    };
                    DatabaseConnection.ExecuteNonQuery(queryUpdatePhong, paramPhong);

                    MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại toàn bộ dữ liệu
                    LoadMainData();
                    LoadHelperData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Nhận (Sửa)
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDatPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn một phiếu đặt phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMaKH.SelectedValue == null || cmbSoPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpNgayCheckOut.Value <= dtpNgayCheckIn.Value)
            {
                MessageBox.Show("Ngày check-out phải sau ngày check-in!", "Lỗi ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soNguoi = 0;
            decimal datCoc = 0;

            if (!string.IsNullOrWhiteSpace(txtSoNguoi.Text))
            {
                if (!int.TryParse(txtSoNguoi.Text, out soNguoi) || soNguoi < 1)
                {
                    MessageBox.Show("Số người phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtDatCoc.Text))
            {
                if (!decimal.TryParse(txtDatCoc.Text, out datCoc) || datCoc < 0)
                {
                    MessageBox.Show("Đặt cọc phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                string newMaP = cmbSoPhong.SelectedValue.ToString();

                // Nếu đổi phòng
                if (!string.IsNullOrEmpty(oldMaP) && oldMaP != newMaP)
                {
                    // Trả phòng cũ về trạng thái Trống (TT03)
                    string queryOldRoom = "UPDATE PHONG SET idTrangThai = 'TT03' WHERE MaP = @MaP";
                    SqlParameter[] paramOldRoom = new SqlParameter[]
                    {
                        new SqlParameter("@MaP", oldMaP)
                    };
                    DatabaseConnection.ExecuteNonQuery(queryOldRoom, paramOldRoom);

                    // Cập nhật phòng mới thành Đã đặt (TT02)
                    string queryNewRoom = "UPDATE PHONG SET idTrangThai = 'TT02' WHERE MaP = @MaP";
                    SqlParameter[] paramNewRoom = new SqlParameter[]
                    {
                        new SqlParameter("@MaP", newMaP)
                    };
                    DatabaseConnection.ExecuteNonQuery(queryNewRoom, paramNewRoom);
                }

                // Cập nhật thông tin đặt phòng
                string query = @"UPDATE DATPHONG SET 
                                    MaP = @MaP, MaKH = @MaKH, NgayDat = @NgayDat, 
                                    NgayCheckIn = @NgayCheckIn, NgayCheckOut = @NgayCheckOut, 
                                    SoNguoi = @SoNguoi, DatCoc = @DatCoc 
                                 WHERE idDatPhong = @idDatPhong";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaP", newMaP),
                    new SqlParameter("@MaKH", cmbMaKH.SelectedValue.ToString()),
                    new SqlParameter("@NgayDat", dtpNgayDat.Value),
                    new SqlParameter("@NgayCheckIn", dtpNgayCheckIn.Value),
                    new SqlParameter("@NgayCheckOut", dtpNgayCheckOut.Value),
                    new SqlParameter("@SoNguoi", soNguoi),
                    new SqlParameter("@DatCoc", datCoc),
                    new SqlParameter("@idDatPhong", txtMaDatPhong.Text.Trim())
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMainData();
                    LoadHelperData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Không tìm thấy mã đặt phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Hủy (Xóa trắng)
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // Click vào DGV Đặt phòng (dưới)
        private void dgvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDatPhong.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvDatPhong.Rows[e.RowIndex];

                    txtMaDatPhong.Text = row.Cells["idDatPhong"].Value?.ToString() ?? "";
                    cmbMaKH.SelectedValue = row.Cells["MaKH"].Value;

                    // Lưu mã phòng cũ
                    oldMaP = row.Cells["MaP"].Value?.ToString() ?? "";

                    // Load TẤT CẢ phòng khi chế độ sửa
                    LoadAllRoomsForEdit();
                    cmbSoPhong.SelectedValue = oldMaP;

                    txtSoNguoi.Text = row.Cells["SoNguoi"].Value?.ToString() ?? "";
                    txtDatCoc.Text = row.Cells["DatCoc"].Value?.ToString() ?? "";

                    if (row.Cells["NgayDat"].Value != null && row.Cells["NgayDat"].Value != DBNull.Value)
                        dtpNgayDat.Value = Convert.ToDateTime(row.Cells["NgayDat"].Value);

                    if (row.Cells["NgayCheckIn"].Value != null && row.Cells["NgayCheckIn"].Value != DBNull.Value)
                        dtpNgayCheckIn.Value = Convert.ToDateTime(row.Cells["NgayCheckIn"].Value);

                    if (row.Cells["NgayCheckOut"].Value != null && row.Cells["NgayCheckOut"].Value != DBNull.Value)
                        dtpNgayCheckOut.Value = Convert.ToDateTime(row.Cells["NgayCheckOut"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Click vào DGV Phòng (trên)
        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPhong.Rows.Count)
            {
                DataGridViewRow row = dgvPhong.Rows[e.RowIndex];
                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";

                // Chỉ cho phép chọn nếu phòng 'Trống'
                if (trangThai == "Trống")
                {
                    cmbSoPhong.SelectedValue = row.Cells["MaP"].Value;
                }
                else
                {
                    MessageBox.Show($"Phòng {row.Cells["MaP"].Value} đang {trangThai}. Vui lòng chọn phòng khác.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // NẾU BẠN MUỐN THÊM NÚT XÓA ĐẶT PHÒNG (Hủy booking)
        /*
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDatPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu đặt phòng cần hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn HỦY đặt phòng này?", 
                                                   "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Lấy mã phòng trước khi xóa
                    string queryGetRoom = "SELECT MaP FROM DATPHONG WHERE idDatPhong = @idDatPhong";
                    SqlParameter[] paramGet = new SqlParameter[] { new SqlParameter("@idDatPhong", txtMaDatPhong.Text.Trim()) };
                    object maP = DatabaseConnection.ExecuteScalar(queryGetRoom, paramGet);

                    // Xóa đặt phòng
                    string queryDelete = "DELETE FROM DATPHONG WHERE idDatPhong = @idDatPhong";
                    SqlParameter[] paramDelete = new SqlParameter[] { new SqlParameter("@idDatPhong", txtMaDatPhong.Text.Trim()) };
                    int deleteResult = DatabaseConnection.ExecuteNonQuery(queryDelete, paramDelete);

                    if (deleteResult > 0 && maP != null)
                    {
                        // Trả phòng về trạng thái Trống
                        string queryUpdateRoom = "UPDATE PHONG SET idTrangThai = 'TT03' WHERE MaP = @MaP";
                        SqlParameter[] paramUpdate = new SqlParameter[] { new SqlParameter("@MaP", maP.ToString()) };
                        DatabaseConnection.ExecuteNonQuery(queryUpdateRoom, paramUpdate);

                        MessageBox.Show("Hủy đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMainData();
                        LoadHelperData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hủy đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        */
    }
}