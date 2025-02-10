using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;



namespace DXA_KinhDoanhNoiThat
{
    public partial class UC_NapDL : UserControl
    {
        private string connectionString1 = "Data Source=TGER\\TGER;Initial Catalog=KinhDoanhNoiThat_SnowflakeSchema;User ID=sa;Password=1234;";
        private string connectionString = "Data Source=TGER\\TGER;Initial Catalog=KinhDoanhNoiThat_NDS;User ID=sa;Password=1234;";

        public UC_NapDL()
        {
            InitializeComponent();

            comboBox1.Items.Add("Kho dữ liệu chuẩn hóa");
            comboBox1.Items.Add("Kho dữ liệu theo chiều");
        }

        private async Task StartJobAndLoadData(string jobName, string successMessage, string dataSourceType)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand($"EXEC msdb.dbo.sp_start_job N'{jobName}'", connection))
                        {
                            command.ExecuteNonQuery();

                            if (dataSourceType == "Kho dữ liệu chuẩn hóa") {await Task.Delay(3500);}
                            else {await Task.Delay(5000);}
                            
                            MessageBox.Show(successMessage);
                            comboBox1.SelectedItem = dataSourceType;

                            if ((string)comboBox1.SelectedItem == "Kho dữ liệu chuẩn hóa") {Dulieu_Load_NDS();}
                            else {Dulieu_Load_DDS();}
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi làm sạch: {ex.Message}");
                }
            }
        }

        //Nạp NDS
        private async void btn_NapNDS_Click(object sender, EventArgs e)
        {
            string jobName = "";

            if (ckb_DB.Checked && ckb_Web.Checked && ckb_EX.Checked) jobName = "SSIS_NDS";
            else if (ckb_DB.Checked && ckb_Web.Checked && !ckb_EX.Checked) jobName = "SSIS_NDS_D_W";
            else if (ckb_DB.Checked && !ckb_Web.Checked && ckb_EX.Checked) jobName = "SSIS_NDS_D_E";
            else if (!ckb_DB.Checked && ckb_Web.Checked && ckb_EX.Checked) jobName = "SSIS_NDS_W_E";
            else if (ckb_DB.Checked && !ckb_Web.Checked && !ckb_EX.Checked) jobName = "SSIS_NDS_D";
            else if (!ckb_DB.Checked && ckb_Web.Checked && !ckb_EX.Checked) jobName = "SSIS_NDS_W";
            else if (!ckb_DB.Checked && !ckb_Web.Checked && ckb_EX.Checked) jobName = "SSIS_NDS_E";
            else
            {
                MessageBox.Show("Bạn chưa chọn nguồn nạp!");
                return;
            }

            await StartJobAndLoadData(jobName, "Đã làm sạch thành công.", "Kho dữ liệu chuẩn hóa");
        }

        //Nạp DDS
        private async void btn_NapDDS_Click(object sender, EventArgs e)
        {
            await StartJobAndLoadData("SSIS_DDS", "Đã nạp thành công.", "Kho dữ liệu theo chiều");
        }

        //Load data kho
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "Kho dữ liệu chuẩn hóa")
            {
                Dulieu_Load_NDS();
            }
            else
            {
                Dulieu_Load_DDS();
            }
        }   

        private void Dulieu_Load_NDS()
        {
            string mdxQuery = "Select * From KhuVuc";
            string mdxQuery1 = "Select * From ChiNhanh";
            string mdxQuery2 = "Select * From LoaiSanPham";
            string mdxQuery3 = "Select * From SanPham";
            string mdxQuery4 = "Select * From LoaiKhachHang";
            string mdxQuery5 = "Select * From KhachHang";
            string mdxQuery6 = "Select * From ChuongTrinhGiamGia";
            string mdxQuery7 = "Select * From NhanVien";
            string mdxQuery8 = "Select * From NhaPhanPhoi";
            string mdxQuery9 = "Select * From HoaDonBanHang";
            string mdxQuery10 = "Select * From HoaDonNhapHang";
            string mdxQuery11 = "Select * From ChiTietHoaDonBanHang";
            string mdxQuery12 = "Select * From ChiTietDonNhapHang";

            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapKinhDoanh.DataSource = null;
            dgv_NapKinhDoanh.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapCTHD.DataSource = null;
            dgv_NapCTHD.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhapHang.DataSource = null;
            dgv_NapNhapHang.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapCTNH.DataSource = null;
            dgv_NapCTNH.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapSanPham.DataSource = null;
            dgv_NapSanPham.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapLoaiSP.DataSource = null;
            dgv_NapLoaiSP.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapKhachHang.DataSource = null;
            dgv_NapKhachHang.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapLoaiKH.DataSource = null;
            dgv_NapLoaiKH.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhanVien.DataSource = null;
            dgv_NapNhanVien.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapCTGiamGia.DataSource = null;
            dgv_NapCTGiamGia.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapKhuVuc.DataSource = null;
            dgv_NapKhuVuc.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapChiNhanh.DataSource = null;
            dgv_NapChiNhanh.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhaPP.DataSource = null;
            dgv_NapNhaPP.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhaPP.DataSource = null;
            dgv_NapNhaPP.Columns.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery9, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapKinhDoanh.DataSource = dataTable;
                        dgv_NapKinhDoanh.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery11, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapCTHD.DataSource = dataTable;
                        dgv_NapCTHD.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery10, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapNhapHang.DataSource = dataTable;
                        dgv_NapNhapHang.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery12, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapCTNH.DataSource = dataTable;
                        dgv_NapCTNH.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery3, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapSanPham.DataSource = dataTable;
                        dgv_NapSanPham.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery2, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapLoaiSP.DataSource = dataTable;
                        dgv_NapLoaiSP.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery5, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapKhachHang.DataSource = dataTable;
                        dgv_NapKhachHang.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery4, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapLoaiKH.DataSource = dataTable;
                        dgv_NapLoaiKH.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery7, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapNhanVien.DataSource = dataTable;
                        dgv_NapNhanVien.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery6, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapCTGiamGia.DataSource = dataTable;
                        dgv_NapCTGiamGia.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapKhuVuc.DataSource = dataTable;
                        dgv_NapKhuVuc.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery1, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapChiNhanh.DataSource = dataTable;
                        dgv_NapChiNhanh.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery8, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapNhaPP.DataSource = dataTable;
                        dgv_NapNhaPP.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Dulieu_Load_DDS()
        {
            string mdxQuery = "Select * From Dim_KhuVuc";
            string mdxQuery1 = "Select * From Dim_ChiNhanh";
            string mdxQuery2 = "Select * From Dim_LoaiSanPham";
            string mdxQuery3 = "Select * From Dim_SanPham";
            string mdxQuery4 = "Select * From Dim_LoaiKhachHang";
            string mdxQuery5 = "Select * From Dim_KhachHang";
            string mdxQuery6 = "Select * From Dim_ThoiGian";
            string mdxQuery7 = "Select * From Dim_NhanVien";
            string mdxQuery8 = "Select * From Dim_NhaPhanPhoi";
            string mdxQuery12 = "Select * From Fact_KinhDoanh";

            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapKinhDoanh.DataSource = null;
            dgv_NapKinhDoanh.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapCTHD.DataSource = null;
            dgv_NapCTHD.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhapHang.DataSource = null;
            dgv_NapNhapHang.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapCTNH.DataSource = null;
            dgv_NapCTNH.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapSanPham.DataSource = null;
            dgv_NapSanPham.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapLoaiSP.DataSource = null;
            dgv_NapLoaiSP.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapKhachHang.DataSource = null;
            dgv_NapKhachHang.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapLoaiKH.DataSource = null;
            dgv_NapLoaiKH.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhanVien.DataSource = null;
            dgv_NapNhanVien.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapCTGiamGia.DataSource = null;
            dgv_NapCTGiamGia.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapKhuVuc.DataSource = null;
            dgv_NapKhuVuc.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapChiNhanh.DataSource = null;
            dgv_NapChiNhanh.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhaPP.DataSource = null;
            dgv_NapNhaPP.Columns.Clear();
            // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
            dgv_NapNhaPP.DataSource = null;
            dgv_NapNhaPP.Columns.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery12, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapKinhDoanh.DataSource = dataTable;
                        dgv_NapKinhDoanh.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery3, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapSanPham.DataSource = dataTable;
                        dgv_NapSanPham.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery2, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapLoaiSP.DataSource = dataTable;
                        dgv_NapLoaiSP.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery5, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapKhachHang.DataSource = dataTable;
                        dgv_NapKhachHang.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery4, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapLoaiKH.DataSource = dataTable;
                        dgv_NapLoaiKH.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery7, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapNhanVien.DataSource = dataTable;
                        dgv_NapNhanVien.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapKhuVuc.DataSource = dataTable;
                        dgv_NapKhuVuc.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery1, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapChiNhanh.DataSource = dataTable;
                        dgv_NapChiNhanh.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery8, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_NapNhaPP.DataSource = dataTable;
                        dgv_NapNhaPP.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery6, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dgv_ThoiGian.DataSource = dataTable;
                        dgv_ThoiGian.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
