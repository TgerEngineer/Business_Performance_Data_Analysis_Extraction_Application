using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXA_KinhDoanhNoiThat
{
    public partial class UC_SaoLuu : UserControl
    {
        public UC_SaoLuu()
        {
            InitializeComponent();
            LoadDatabases();
        }

        private string tenCoSoDuLieu;
        private string tenCoSoDuLieu1;
        private string tenCoSoDuLieu2;
        private string tenCoSoDuLieu3;
        private string duongDanSaoLuu;
        private string duongDanKhoiPhuc;

        private void LoadDatabases()
        {
            string[] items = { "Tất cả", "Kho dữ liệu hệ thống", "Dữ liệu nguồn", "Nguồn dữ liệu ứng dụng", "Nguồn dữ liệu Web", "Kho dữ liệu chuẩn hóa", "Kho dữ liệu chính" };
            foreach (string item in items)
            {
                cbx_ChonDL.Items.Add(item);
            }
            cbx_ChonDL.SelectedIndex = 0; // Chọn giá trị đầu tiên làm mặc định (nếu cần)
        }             

        private void btn_ChonDuongDanS_Click(object sender, EventArgs e)
        { 
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    duongDanSaoLuu = dialog.SelectedPath;
                    txt_DuongDanS.Text = duongDanSaoLuu;
                }
            }
        }

        private void btn_LuuFileS_Click(object sender, EventArgs e)
        {
            string lenhSaoLuu = "";

            if (cbx_ChonDL.Text == "Tất cả")
            {
                tenCoSoDuLieu = "KinhDoanhNoiThat_SnowflakeSchema";
                tenCoSoDuLieu1 = "KinhDoanhNoiThat_NDS";
                tenCoSoDuLieu2 = "KinhDoanhNoiThat_Web";
                tenCoSoDuLieu3 = "KinhDoanhNoiThat_Desktop";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu}.bak'; " +
                                    $"BACKUP DATABASE [{tenCoSoDuLieu1}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu1}.bak'; " +
                                    $"BACKUP DATABASE [{tenCoSoDuLieu2}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu2}.bak'; " +
                                    $"BACKUP DATABASE [{tenCoSoDuLieu3}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu3}.bak'; ";
            }
            else if (cbx_ChonDL.Text == "Kho dữ liệu hệ thống")
            {
                tenCoSoDuLieu = "KinhDoanhNoiThat_SnowflakeSchema";
                tenCoSoDuLieu1 = "KinhDoanhNoiThat_NDS";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu}.bak'; " +
                             $"BACKUP DATABASE [{tenCoSoDuLieu1}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu1}.bak'; ";
            }
            else if (cbx_ChonDL.Text == "Dữ liệu nguồn")
            {
                tenCoSoDuLieu2 = "KinhDoanhNoiThat_Web";
                tenCoSoDuLieu3 = "KinhDoanhNoiThat_Desktop";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu2}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu2}.bak'; " +
                             $"BACKUP DATABASE [{tenCoSoDuLieu3}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu3}.bak'; ";
            }
            else if (cbx_ChonDL.Text == "Nguồn dữ liệu úng dụng")
            {
                tenCoSoDuLieu3 = "KinhDoanhNoiThat_Desktop";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu3}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu3}.bak'; ";
            }
            else if (cbx_ChonDL.Text == "Nguồn dữ liệu Web")
            {
                tenCoSoDuLieu2 = "KinhDoanhNoiThat_Web";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu2}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu2}.bak'; ";
            }
            else if (cbx_ChonDL.Text == "Kho dữ liệu chuẩn hóa")
            {
                tenCoSoDuLieu1 = "KinhDoanhNoiThat_NDS";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu1}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu1}.bak'; ";
            }
            else if (cbx_ChonDL.Text == "Kho dữ liệu chính")
            {
                tenCoSoDuLieu = "KinhDoanhNoiThat_SnowflakeSchema";

                // Tạo lệnh sao lưu
                lenhSaoLuu = $"BACKUP DATABASE [{tenCoSoDuLieu}] TO DISK='{duongDanSaoLuu}\\{tenCoSoDuLieu}.bak'; ";
            }

            if (!string.IsNullOrWhiteSpace(tenCoSoDuLieu) && !string.IsNullOrWhiteSpace(duongDanSaoLuu))
            {               
                try
                {
                    // Chuỗi kết nối đến SQL Server với tên người dùng "TGER"
                    string chuoiKetNoi = "Data Source=TGER\\TGER;Initial Catalog=master;User ID=sa;Password=1234;";
                    
                    using (SqlConnection ketNoi = new SqlConnection(chuoiKetNoi))
                    {
                        ketNoi.Open();

                        // Thực thi lệnh sao lưu
                        using (SqlCommand lenh = new SqlCommand(lenhSaoLuu, ketNoi))
                        {
                            lenh.ExecuteNonQuery();
                        }

                        MessageBox.Show("Sao lưu thành công!");
                        ketNoi.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    // Xử lý ngoại lệ, ghi log hoặc thông báo lỗi
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn cơ sở dữ liệu và đường dẫn sao lưu.");
            }
        }

        private void btn_ChonDuongDanO_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Tập tin sao lưu (*.bak)|*.bak|Tất cả các tập tin (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    duongDanKhoiPhuc = dialog.FileName;
                    txt_DuongDanO.Text = duongDanKhoiPhuc;

                    // Trích xuất tên cơ sở dữ liệu từ tên tập tin sao lưu
                    tenCoSoDuLieu = System.IO.Path.GetFileNameWithoutExtension(duongDanKhoiPhuc);
                }
            }
        }

        private void btn_KhoiPhuc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tenCoSoDuLieu) && !string.IsNullOrWhiteSpace(duongDanKhoiPhuc))
            {
                try
                {
                    // Chuỗi kết nối đến SQL Server với tên người dùng "TGER"
                    string chuoiKetNoi = "Data Source=TGER\\TGER;Initial Catalog=master;User ID=sa;Password=1234;";

                    using (SqlConnection ketNoi = new SqlConnection(chuoiKetNoi))
                    {
                        ketNoi.Open();

                        // Tạo lệnh khôi phục
                        string lenhKhoiPhuc = $"RESTORE DATABASE [{tenCoSoDuLieu}] FROM DISK='{duongDanKhoiPhuc}'";

                        // Thực thi lệnh khôi phục
                        using (SqlCommand lenh = new SqlCommand(lenhKhoiPhuc, ketNoi))
                        {
                            lenh.ExecuteNonQuery();
                        }

                        MessageBox.Show("Khôi phục thành công!");
                        ketNoi.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    // Xử lý ngoại lệ, ghi log hoặc thông báo lỗi
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn cơ sở dữ liệu và đường dẫn khôi phục.");
            }
        } 
    }
}
