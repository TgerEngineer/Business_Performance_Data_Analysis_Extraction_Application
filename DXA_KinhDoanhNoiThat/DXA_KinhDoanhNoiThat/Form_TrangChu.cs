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

namespace DXA_KinhDoanhNoiThat
{
    public partial class Form_TrangChu : Form
    {
        private string connectionString = "Data Source=TGER\\TGER_22;Initial Catalog=UngDungPhanTich_KDNT;User ID=sa;Password=1234;";

        public Form_TrangChu()
        {
            InitializeComponent();

            TrangChuUser_Load();
            TrangChuVT_Load();
            SetToolTips();
        }

        public void HideTabQuanTri()
        {
            // Ẩn tab_QuanTri
            tabControl1.TabPages.Remove(tab_QuanTri);
        }

        private void TrangChuVT_Load()
        {
            string mdxQuery = "select TenVaiTro from VaiTro";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    //Gán dữ liệu cho ComboBox
                    cmb_VaiTro.DataSource = dataTable;
                    cmb_VaiTro.DisplayMember = "TenVaiTro";
                    cmb_VaiTro.ValueMember = "TenVaiTro";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while filling ComboBox: " + ex.Message);
                }
            }
        }

        private void TrangChuUser_Load()
        {
            string mdxQuery = "select nd.MaNguoiDung as 'Mã ND', nd.TenNguoiDung as 'Tên người dùng', TenDangNhap as 'Tên đăng nhập', MatKhau as 'Mật khẩu', Email, TenVaiTro as 'Vai trò', TenPhanQuyen as 'Quyền' from NguoiDung nd Inner join NguoiDung_VaiTro ndvt on nd.MaNguoiDung = ndvt.MaNguoiDung Inner join VaiTro vt on ndvt.MaVaiTro = vt.MaVaiTro Inner join VaiTro_PhanQuyen vtpq on vtpq.MaVaiTro = vt.MaVaiTro Inner join PhanQuyen pq on pq.MaPhanQuyen = vtpq.MaPhanQuyen";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(mdxQuery, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dgv_DSNguoiDung.DataSource = dataTable;
                    dgv_DSNguoiDung.DefaultCellStyle.ForeColor = Color.Black;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void dgv_DSNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Kiểm tra xem hàng được chọn có hợp lệ không
            if (e.RowIndex >= 0)
            {
                //Lấy dòng hiện tại
                DataGridViewRow row = dgv_DSNguoiDung.Rows[e.RowIndex];

                //Gán giá trị cho các TextBox từ các ô tương ứng trong dòng
                txt_MaNguoiDung.Text = row.Cells["Mã ND"].Value.ToString();
                txt_TenDangNhap.Text = row.Cells["Tên đăng nhập"].Value.ToString();
                txt_MatKhau.Text = row.Cells["Mật khẩu"].Value.ToString();
                txt_HoTen.Text = row.Cells["Tên người dùng"].Value.ToString();
                txt_Email.Text = row.Cells["Email"].Value.ToString();
                cmb_VaiTro.Text = row.Cells["Vai trò"].Value.ToString();
            }
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();

            Form_DangNhap formDangNhap = new Form_DangNhap();
            formDangNhap.ShowDialog();
        }

        private void btn_CNThongKe_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            UC_ThongKe thongKeControl = new UC_ThongKe();
            thongKeControl.Dock = DockStyle.Fill;
            panel3.Controls.Add(thongKeControl);
        }

        private void btn_CNNap_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            UC_NapDL napDLControl = new UC_NapDL();
            napDLControl.Dock = DockStyle.Fill;
            panel3.Controls.Add(napDLControl);
        }

        private void btn_CNPhanTich_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            UC_PhanTich phantichControl = new UC_PhanTich();
            phantichControl.Dock = DockStyle.Fill;
            panel3.Controls.Add(phantichControl);
        }

        private void btn_CNKhaiPha_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            UC_KhaiPhaDL khaiphaControl = new UC_KhaiPhaDL();
            khaiphaControl.Dock = DockStyle.Fill;
            panel3.Controls.Add(khaiphaControl);
        }

        private void btn_CNSaoLuu_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            UC_SaoLuu saoluuControl = new UC_SaoLuu();
            saoluuControl.Dock = DockStyle.Top;
            panel3.Controls.Add(saoluuControl);
        }

        private void btn_CapNhatND_Click(object sender, EventArgs e)
        {
            txt_MaNguoiDung.Text = "";
            txt_TenDangNhap.Text = "";
            txt_MatKhau.Text = "";
            txt_HoTen.Text = "";
            txt_Email.Text = "";
            cmb_VaiTro.Text = "";

            TrangChuUser_Load();
        }

        private void btn_SuaND_Click(object sender, EventArgs e)
        {
            string maNguoiDung = txt_MaNguoiDung.Text;
            string tenDangNhap = txt_TenDangNhap.Text;
            string matKhau = txt_MatKhau.Text;
            string hoTen = txt_HoTen.Text;
            string email = txt_Email.Text;
            string tenVaiTro = cmb_VaiTro.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    //Lấy mã vai trò từ tên vai trò
                    string maVaiTro = "";
                    string getVaiTroQuery = $"SELECT MaVaiTro FROM VaiTro WHERE TenVaiTro = '{tenVaiTro}'";
                    using (SqlCommand cmd = new SqlCommand(getVaiTroQuery, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            maVaiTro = reader["MaVaiTro"].ToString();
                        }
                        reader.Close();
                    }

                    if (!string.IsNullOrEmpty(maVaiTro))
                    {
                        //Cập nhật bảng NguoiDung
                        string updateNguoiDungQuery = $"UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, TenNguoiDung = @HoTen, Email = @Email WHERE MaNguoiDung = '{maNguoiDung}'";
                        using (SqlCommand cmd = new SqlCommand(updateNguoiDungQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                            cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.ExecuteNonQuery();
                        }

                        //Cập nhật bảng NguoiDung_VaiTro
                        string updateNguoiDungVaiTroQuery = $"UPDATE NguoiDung_VaiTro SET MaVaiTro = @MaVaiTro WHERE MaNguoiDung = '{maNguoiDung}'";
                        using (SqlCommand cmd = new SqlCommand(updateNguoiDungVaiTroQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                            cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmd.ExecuteNonQuery();
                        }

                        TrangChuUser_Load();
                    }
                    else
                    {
                        MessageBox.Show("Vai trò không hợp lệ.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void btn_ThemND_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txt_TenDangNhap.Text;
            string matKhau = txt_MatKhau.Text;
            string hoTen = txt_HoTen.Text;
            string email = txt_Email.Text;
            string tenVaiTro = cmb_VaiTro.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    //Lấy mã vai trò từ tên vai trò
                    string maVaiTro = "";
                    string getVaiTroQuery = $"SELECT MaVaiTro FROM VaiTro WHERE TenVaiTro = '{tenVaiTro}'";
                    using (SqlCommand cmd = new SqlCommand(getVaiTroQuery, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            maVaiTro = reader["MaVaiTro"].ToString();
                        }
                        reader.Close();
                    }

                    if (!string.IsNullOrEmpty(maVaiTro))
                    {
                        //Cập nhật bảng NguoiDung
                        string insertNguoiDungQuery = $"INSERT INTO NguoiDung(TenDangNhap, MatKhau, TenNguoiDung, Email) VALUES('{tenDangNhap}', '{matKhau}', N'{hoTen}', '{email}')";
                        using (SqlCommand cmd = new SqlCommand(insertNguoiDungQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                            cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.ExecuteNonQuery();
                        }

                        //Lấy mã người dùng từ tên người dùng
                        string maNguoiDung = "";
                        string getNguoiDungQuery = $"SELECT MaNguoiDung FROM NguoiDung WHERE TenNguoiDung = N'{hoTen}'";
                        using (SqlCommand cmd = new SqlCommand(getNguoiDungQuery, conn))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                maNguoiDung = reader["MaNguoiDung"].ToString();
                            }
                            reader.Close();
                        }

                        //Cập nhật bảng NguoiDung_VaiTro
                        string updateNguoiDungVaiTroQuery = $"INSERT INTO NguoiDung_VaiTro(MaVaiTro, MaNguoiDung) VALUES('{maVaiTro}', '{maNguoiDung}')";
                        using (SqlCommand cmd = new SqlCommand(updateNguoiDungVaiTroQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                            cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                            cmd.ExecuteNonQuery();
                        }

                        txt_MaNguoiDung.Text = "";
                        txt_TenDangNhap.Text = "";
                        txt_MatKhau.Text = "";
                        txt_HoTen.Text = "";
                        txt_Email.Text = "";
                        cmb_VaiTro.Text = "";

                        TrangChuUser_Load();
                    }
                    else
                    {
                        MessageBox.Show("Vai trò không hợp lệ.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void btn_XoaND_Click(object sender, EventArgs e)
        {
            string maNguoiDung = txt_MaNguoiDung.Text;

            //Định nghĩa câu lệnh DELETE cho bảng NguoiDung_VaiTro
            string deleteNguoiDungVaiTroQuery = $"DELETE FROM NguoiDung_VaiTro WHERE MaNguoiDung = '{maNguoiDung}'";

            //Định nghĩa câu lệnh DELETE cho bảng NguoiDung
            string deleteNguoiDungQuery = $"DELETE FROM NguoiDung WHERE MaNguoiDung = '{maNguoiDung}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Mở kết nối một lần

                //Thực thi câu lệnh DELETE cho NguoiDung_VaiTro
                using (SqlCommand command = new SqlCommand(deleteNguoiDungVaiTroQuery, connection))
                {
                    // Thêm tham số MaNguoiDung và giá trị của nó
                    command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

                    // Thực thi câu lệnh DELETE
                    int rowsAffected = command.ExecuteNonQuery();

                    //Tùy chọn, bạn có thể kiểm tra số hàng ảnh hưởng
                    //Console.WriteLine($"{rowsAffected} hang da duoc xoa tu NguoiDung_VaiTro.");
                }

                // Thực thi câu lệnh DELETE cho NguoiDung
                using (SqlCommand command = new SqlCommand(deleteNguoiDungQuery, connection))
                {
                    //Sử dụng lại cùng một tham số MaNguoiDung
                    command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

                    //Thực thi câu lệnh DELETE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Tùy chọn, bạn có thể kiểm tra số hàng ảnh hưởng
                    //Console.WriteLine($"{rowsAffected} hang da duoc xoa tu NguoiDung.");
                }
            }

            txt_MaNguoiDung.Text = "";
            txt_TenDangNhap.Text = "";
            txt_MatKhau.Text = "";
            txt_HoTen.Text = "";
            txt_Email.Text = "";
            cmb_VaiTro.Text = "";

            TrangChuUser_Load();
        }

        private void SetToolTips()
        {
            toolTip1.SetToolTip(btn_ThemND, "Thêm");
            toolTip2.SetToolTip(btn_XoaND, "Xóa");
            toolTip3.SetToolTip(btn_SuaND, "Sửa");
            toolTip4.SetToolTip(btn_CapNhatND, "Làm mới");

            toolTip5.SetToolTip(btn_DangXuat, "Đăng xuất");
        }


    }
}
