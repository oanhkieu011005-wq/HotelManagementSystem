using HotelManagementSystem.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementSystem.Forms
{
    public partial class frmQLPhong : Form
    {
        public frmQLPhong()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadData();
            LoadComboBoxes();
        }

        private void ConfigureDataGridView()
        {
            dgvPhong.AutoGenerateColumns = true;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.MultiSelect = false;
            dgvPhong.ReadOnly = true;
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.RowHeadersVisible = false;
            dgvPhong.BackgroundColor = System.Drawing.Color.White;
            dgvPhong.CellClick += dgvPhong_CellClick;
        }

        private void LoadData()
        {
            try
            {
                string query = @"SELECT P.MaP, P.MaLP, LP.TenLP, P.Gia, TT.TrangThai 
                               FROM PHONG P
                               INNER JOIN LOAIPHONG LP ON P.MaLP = LP.MaLP
                               INNER JOIN TTPHONG TT ON P.idTrangThai = TT.idTrangThai";

                DataTable dt = DatabaseConnection.ExecuteQuery(query);
                dgvPhong.DataSource = dt;

                if (dgvPhong.Columns.Count > 0)
                {
                    dgvPhong.Columns["MaP"].HeaderText = "Mã phòng";
                    dgvPhong.Columns["MaP"].Width = 100;

                    dgvPhong.Columns["MaLP"].HeaderText = "Mã loại";
                    dgvPhong.Columns["MaLP"].Width = 80;

                    dgvPhong.Columns["TenLP"].HeaderText = "Tên loại phòng";
                    dgvPhong.Columns["TenLP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvPhong.Columns["Gia"].HeaderText = "Giá";
                    dgvPhong.Columns["Gia"].Width = 120;
                    dgvPhong.Columns["Gia"].DefaultCellStyle.Format = "N0";

                    dgvPhong.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvPhong.Columns["TrangThai"].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                // Load ComboBox Loại Phòng
                string queryLP = "SELECT MaLP, TenLP FROM LOAIPHONG";
                DataTable dtLP = DatabaseConnection.ExecuteQuery(queryLP);
                cmbMaLP.DataSource = dtLP;
                cmbMaLP.DisplayMember = "TenLP";
                cmbMaLP.ValueMember = "MaLP";

                // Load ComboBox Trạng Thái
                string queryTT = "SELECT idTrangThai, TrangThai FROM TTPHONG";
                DataTable dtTT = DatabaseConnection.ExecuteQuery(queryTT);
                cmbTrangThai.DataSource = dtTT;
                cmbTrangThai.DisplayMember = "TrangThai";
                cmbTrangThai.ValueMember = "idTrangThai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải combobox: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaP.Clear();
            txtGia.Clear();
            cmbMaLP.SelectedIndex = -1;
            cmbTrangThai.SelectedIndex = -1;
            txtMaP.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaP.Focus();
                return;
            }

            if (cmbMaLP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loại phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaLP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGia.Text))
            {
                MessageBox.Show("Vui lòng nhập Giá phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }

            if (cmbTrangThai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Trạng thái!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTrangThai.Focus();
                return;
            }

            decimal gia;
            if (!decimal.TryParse(txtGia.Text, out gia) || gia < 0)
            {
                MessageBox.Show("Giá phải là số không âm!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGia.Focus();
                return;
            }

            // ===== BỎ GhiChu - Đặt NULL =====
            string query = "INSERT INTO PHONG (MaP, MaLP, Gia, idTrangThai, GhiChu) VALUES (@MaP, @MaLP, @Gia, @idTrangThai, NULL)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaP", txtMaP.Text.Trim()),
                new SqlParameter("@MaLP", cmbMaLP.SelectedValue),
                new SqlParameter("@Gia", gia),
                new SqlParameter("@idTrangThai", cmbTrangThai.SelectedValue)
            };

            try
            {
                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm phòng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Mã phòng này đã tồn tại!", "Lỗi",
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
            if (string.IsNullOrWhiteSpace(txtMaP.Text))
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMaLP.SelectedValue == null || cmbTrangThai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal gia;
            if (!decimal.TryParse(txtGia.Text, out gia) || gia < 0)
            {
                MessageBox.Show("Giá phải là số không âm!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGia.Focus();
                return;
            }

            // ===== BỎ GhiChu =====
            string query = "UPDATE PHONG SET MaLP = @MaLP, Gia = @Gia, idTrangThai = @idTrangThai WHERE MaP = @MaP";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaP", txtMaP.Text.Trim()),
                new SqlParameter("@MaLP", cmbMaLP.SelectedValue),
                new SqlParameter("@Gia", gia),
                new SqlParameter("@idTrangThai", cmbTrangThai.SelectedValue)
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
                MessageBox.Show("Cập nhật thất bại. Không tìm thấy mã phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaP.Text))
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa phòng này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM PHONG WHERE MaP = @MaP";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaP", txtMaP.Text.Trim())
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
                        MessageBox.Show("Không thể xóa phòng này!\nPhòng đang được sử dụng trong đặt phòng hoặc hóa đơn.",
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

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPhong.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvPhong.Rows[e.RowIndex];

                    txtMaP.Text = row.Cells["MaP"].Value?.ToString() ?? "";
                    cmbMaLP.SelectedValue = row.Cells["MaLP"].Value;
                    txtGia.Text = row.Cells["Gia"].Value?.ToString() ?? "";

                    // Tìm idTrangThai từ TrangThai (text)
                    string trangThaiText = row.Cells["TrangThai"].Value?.ToString() ?? "";
                    for (int i = 0; i < cmbTrangThai.Items.Count; i++)
                    {
                        DataRowView item = (DataRowView)cmbTrangThai.Items[i];
                        if (item["TrangThai"].ToString() == trangThaiText)
                        {
                            cmbTrangThai.SelectedIndex = i;
                            break;
                        }
                    }
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