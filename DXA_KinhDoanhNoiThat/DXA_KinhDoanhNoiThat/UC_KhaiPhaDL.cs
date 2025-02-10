using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;

namespace DXA_KinhDoanhNoiThat
{
    public partial class UC_KhaiPhaDL : UserControl
    {
        // Chuỗi kết nối đến SSAS
        string connectionString = @"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;";

        public UC_KhaiPhaDL()
        {
            InitializeComponent();

            comboBoxMiningStructures.Items.Add("Tất cả");
            comboBoxMiningStructures.Items.Add("Khách hàng");
            comboBoxMiningStructures.Items.Add("Sản phẩm");
            comboBoxMiningStructures.Items.Add("Chi nhánh - Khu vực");
            comboBoxMiningStructures.Items.Add("Nhân viên");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMiningStructures.SelectedItem != null && (string)comboBoxMiningStructures.SelectedItem != "Tất cả")
            {
                string dm = "";

                switch ((string)comboBoxMiningStructures.SelectedItem)
                {
                    case "Khách hàng":
                        dm = "Dim Khach Hang";
                        break;
                    case "Sản phẩm":
                        dm = "Dim San Pham";
                        break;
                    case "Chi nhánh - Khu vực":
                        dm = "Dim Chi Nhanh";
                        break;
                    case "Nhân viên":
                        dm = "Dim Nhan Vien";
                        break;
                    default:
                        MessageBox.Show("Invalid selection");
                        return;
                }

                try
                {
                    // Kết nối đến SSAS
                    using (AdomdConnection conn = new AdomdConnection(connectionString))
                    {
                        conn.Open();

                        // Câu truy vấn để lấy dữ liệu
                        string query = $"SELECT * FROM [{dm}]";

                        // Thực thi câu truy vấn
                        using (AdomdCommand command = new AdomdCommand(query, conn))
                        {
                            AdomdDataAdapter adapter = new AdomdDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Hiển thị dữ liệu trên DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            if (comboBoxMiningStructures.SelectedItem != null && (string)comboBoxMiningStructures.SelectedItem == "Tất cả")
            {
                string dm1 = "Dim Khach Hang";
                string dm2 = "Dim San Pham";
                string dm3 = "Dim Chi Nhanh";
                string dm4 = "Dim Nhan Vien";

                try
                {
                    // Kết nối đến SSAS
                    using (AdomdConnection conn = new AdomdConnection(connectionString))
                    {
                        conn.Open();

                        string query1 = $"SELECT * FROM [{dm1}]";
                        string query2 = $"SELECT * FROM [{dm2}]";
                        string query3 = $"SELECT * FROM [{dm3}]";
                        string query4 = $"SELECT * FROM [{dm4}]";

                        DataTable dataTable1 = new DataTable();
                        DataTable dataTable2 = new DataTable();
                        DataTable dataTable3 = new DataTable();
                        DataTable dataTable4 = new DataTable();

                        // Thực thi câu truy vấn cho bảng dữ liệu 1
                        using (AdomdCommand command1 = new AdomdCommand(query1, conn))
                        {
                            AdomdDataAdapter adapter1 = new AdomdDataAdapter(command1);
                            adapter1.Fill(dataTable1);
                        }

                        dataGridView1.DataSource = dataTable1;

                        // Thực thi câu truy vấn cho bảng dữ liệu 2
                        using (AdomdCommand command2 = new AdomdCommand(query2, conn))
                        {
                            AdomdDataAdapter adapter2 = new AdomdDataAdapter(command2);
                            adapter2.Fill(dataTable2);
                        }

                        dataGridView2.DataSource = dataTable2;

                        // Thực thi câu truy vấn cho bảng dữ liệu 3
                        using (AdomdCommand command3 = new AdomdCommand(query3, conn))
                        {
                            AdomdDataAdapter adapter3 = new AdomdDataAdapter(command3);
                            adapter3.Fill(dataTable3);
                        }

                        dataGridView3.DataSource = dataTable3;

                        // Thực thi câu truy vấn cho bảng dữ liệu 2
                        using (AdomdCommand command4 = new AdomdCommand(query4, conn))
                        {
                            AdomdDataAdapter adapter4 = new AdomdDataAdapter(command4);
                            adapter4.Fill(dataTable4);
                        }

                        dataGridView4.DataSource = dataTable4;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
