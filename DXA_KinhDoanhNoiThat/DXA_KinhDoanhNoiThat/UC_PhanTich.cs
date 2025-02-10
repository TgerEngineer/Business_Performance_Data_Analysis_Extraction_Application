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
using Microsoft.AnalysisServices.AdomdClient;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using DevExpress.XtraCharts;


namespace DXA_KinhDoanhNoiThat
{
    public partial class UC_PhanTich : UserControl
    {
        //Kết nối SQL Server
        private string connectionString = "Data Source=TGER\\TGER;Initial Catalog=KinhDoanhNoiThat_SnowflakeSchema;User ID=sa;Password=1234;";

        //Khởi tạo biểu đồ
        ChartControl MychartControl1 = new ChartControl();

        public UC_PhanTich()
        {
            InitializeComponent();
            LoadKhuVuc();
            LoadLastDateIntoDateTimePicker1();
            LoadLastDateIntoDateTimePicker2();

            // Gán sự kiện ValueChanged cho cả hai DateTimePicker
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);

            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip1.SetToolTip(btn_Res, "Làm mới");
            toolTip2.SetToolTip(btn_ReExcel, "Xuất Word");
            toolTip3.SetToolTip(btn_ReWord, "Xuất Excel");

        }

        //Load dữ liệu khu vực
        private void LoadKhuVuc()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT DISTINCT ten_khu_vuc FROM Dim_KhuVuc";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Thêm mục "All" vào DataTable
                    DataRow allRow = dataTable.NewRow();
                    allRow["ten_khu_vuc"] = "Tất Cả";
                    dataTable.Rows.InsertAt(allRow, 0);

                    cbx_KhuVuc.DisplayMember = "ten_khu_vuc";
                    cbx_KhuVuc.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        //Load dữ liệu chi nhanh theo khu vực
        private void cbo_KhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbx_KhuVuc.SelectedValue != null)
                {
                    string selectedKhuVucTen = cbx_KhuVuc.Text;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "SELECT DISTINCT ten_chi_nhanh FROM Dim_ChiNhanh WHERE ma_khu_vuc = (SELECT ma_khu_vuc FROM Dim_KhuVuc WHERE ten_khu_vuc = @KhuVuc)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@KhuVuc", selectedKhuVucTen);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Thêm mục "All" vào DataTable
                            DataRow allRow = dataTable.NewRow();
                            allRow["ten_chi_nhanh"] = "Tất Cả";
                            dataTable.Rows.InsertAt(allRow, 0);

                            cbx_ChiNhanh.DisplayMember = "ten_chi_nhanh";
                            cbx_ChiNhanh.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                // Ghi log lỗi hoặc xử lý lỗi khác tùy theo ngữ cảnh
            }
        }

        private void LoadLastDateIntoDateTimePicker2()
        {
            LoadDateIntoDateTimePicker(dateTimePicker2, true); // Load ngày mới nhất vào dateTimePicker2
        }

        private void LoadLastDateIntoDateTimePicker1()
        {
            LoadDateIntoDateTimePicker(dateTimePicker1, false); // Load ngày cũ nhất vào dateTimePicker1
        }

        private void LoadDateIntoDateTimePicker(DateTimePicker dateTimePicker, bool getLatestDate)
        {
            string query = getLatestDate ? "SELECT MAX([Ngay]) FROM [Dim_ThoiGian]" : "SELECT MIN([Ngay]) FROM [Dim_ThoiGian]";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    DateTime dateWithData = DateTime.MinValue;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            dateWithData = Convert.ToDateTime(result);
                        }
                    }

                    if (dateWithData != DateTime.MinValue)
                    {
                        // Thiết lập giá trị cho dateTimePicker
                        dateTimePicker.Value = dateWithData;

                        // Thiết lập MinDate hoặc MaxDate tương ứng
                        if (getLatestDate)
                        {
                            dateTimePicker.MaxDate = dateWithData; // Nếu là dateTimePicker2 thì thiết lập MaxDate
                        }
                        else
                        {
                            dateTimePicker.MinDate = dateWithData; // Nếu là dateTimePicker1 thì thiết lập MinDate
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu hợp lệ.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải ngày: {ex.Message}");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Đảm bảo dateTimePicker1 nhỏ hơn dateTimePicker2
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-1);
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (dateTimePicker1.Value < dateTimePicker1.MinDate)
            {
                MessageBox.Show("Bạn không thể chọn ngày nhỏ hơn ngày đã load.");
                dateTimePicker1.Value = dateTimePicker1.MinDate;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Đảm bảo dateTimePicker2 lớn hơn dateTimePicker1
            if (dateTimePicker2.Value <= dateTimePicker1.Value)
            {
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (dateTimePicker2.Value > dateTimePicker2.MaxDate)
            {
                MessageBox.Show("Bạn không thể chọn ngày lớn hơn ngày đã load.");
                dateTimePicker2.Value = dateTimePicker2.MaxDate;
            }
        }


        //Lấy giá trị không dấu
        private string GetSelectedRadioButtonText(GroupBox groupBox)
        {
            foreach (RadioButton radioButton in groupBox.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    return RemoveDiacritics(radioButton.Text);
                }
            }
            return null;
        }

        //Lấy giá trị có dấu
        private string GetSelectedRadioButtonText1(GroupBox groupBox)
        {
            foreach (RadioButton radioButton in groupBox.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    return radioButton.Text;
                }
            }
            return null;
        }

        //Xóa dấu
        public static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        //-----------------------------------------------------------------------Vẽ biều đồ từ DataGridView---------------------------------------------------------------
        private void VeBieuDo(DataGridView dataGridView)
        {
            try
            {
                //Kiểm tra xem DataGridView có dữ liệu không
                if (dataGridView != null && dataGridView.Rows.Count > 0)
                {
                    //Xóa tất cả các series cũ trong biểu đồ
                    chartControl1.Series.Clear();

                    //Duyệt qua từng dòng trong DataGridView để vẽ biểu đồ cho từng trường hợp
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        //Kiểm tra giá trị của cột "Năm" không phải là NULL trước khi sử dụng
                        if (dataGridView.Rows[i].Cells[0].Value != null)
                        {
                            //Tạo series mới cho từng trường hợp
                            Series series = new Series($"Series {i + 1}", ViewType.Bar);

                            //Lấy dữ liệu từ các cột của dòng hiện tại
                            for ( int j = 0; j < dataGridView.Columns.Count; j++)
                            {
                                //Kiểm tra xem giá trị của ô dữ liệu có null không trước khi truy cập
                                if (dataGridView.Rows[i].Cells[j].Value != null)
                                {
                                    //Bỏ qua cột đầu tiên
                                    if (j != 0)
                                    {
                                        //Lấy giá trị từ cột hiện tại của dòng hiện tại
                                        object xValue = dataGridView.Rows[i].Cells[0].Value;
                                        object yValue = dataGridView.Rows[i].Cells[j].Value;

                                        //Thiết lập tên của trục y cho series
                                        series.ArgumentDataMember = dataGridView.Columns[j].HeaderText;

                                        //Thêm điểm dữ liệu vào series
                                        series.Points.Add(new SeriesPoint(xValue, yValue));
                                    }
                                }
                            }

                            //Thêm series vào biểu đồ
                            chartControl1.Series.Add(series);
                        }
                    }

                    //Chỉnh sửa hiển thị cho trường hợp có 3 cột
                    if (dataGridView.Columns.Count == 3)
                    {
                        //Thiết lập hiển thị biểu đồ dạng cột
                        chartControl1.SeriesTemplate.ChangeView(ViewType.Bar);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để vẽ biểu đồ.");
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        //--------------------------------------------------------------------Phân tích----------------------------------------------------------------------
        private void btn_ThucHienPT_Click(object sender, EventArgs e)
        {
            try
            {
                //Lấy giá trị đã chọn từ ComboBox Khu Vực và Chi Nhánh
                string selectedKhuVuc = cbx_KhuVuc.Text;
                string selectedChiNhanh = cbx_ChiNhanh.Text;

                //Lấy giá trị thời gian bắt đầu (selectedStart) và thời gian kết thúc (selectedEnd)
                string selectedStart = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string selectedEnd = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                //Lấy giá trị của Measure và Chiều đã chọn có dấu
                string selectedMeasure1 = GetSelectedRadioButtonText1(grb_TinhToan);
                string selectedDimension3 = GetSelectedRadioButtonText1(grb_Chieu1);
                string selectedDimension1 = GetSelectedRadioButtonText1(grb_Chieu2);

                //Lấy giá trị của Measure và Chiều đã chọn không dấu
                string selectedMeasure = GetSelectedRadioButtonText(grb_TinhToan);
                string selectedDimension4 = GetSelectedRadioButtonText(grb_Chieu1);
                string selectedDimension = GetSelectedRadioButtonText(grb_Chieu2);

                //Kiểm tra xem đã chọn đầy đủ thông tin cần thiết chưa
                if (!string.IsNullOrEmpty(selectedMeasure))
                {
                    string mdxQuery = "";

                    //Trường hợp chọn: Năm (xong)
                    if (selectedDimension4 == "Nam" && selectedDimension == null)
                    {
                        if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER([Dim Thoi Gian].[{selectedDimension4}].Members, NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER([Dim Thoi Gian].[{selectedDimension4}].Members, NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Khu Vuc].&[{selectedKhuVuc}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER([Dim Thoi Gian].[{selectedDimension4}].Members, NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Chi Nhanh].&[{selectedChiNhanh}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = "Năm"; 
                                    dgv_PhanTich.Columns[1].HeaderText = selectedMeasure;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: Năm với khách hàng (xong)
                    else if (selectedDimension4 == "Nam" && (selectedDimension == "Tuoi" || selectedDimension == "Gioi tinh" || selectedDimension == "Muc thu nhap" || selectedDimension == "Nghe nghiep"))
                    {
                        if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER([Dim Thoi Gian].[{selectedDimension4}].Members * EXCEPT([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].MEMBERS, {{[Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[Unknown], [Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[null]}}), NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";

                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER([Dim Thoi Gian].[{selectedDimension4}].Members * EXCEPT([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].MEMBERS, {{[Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[Unknown], [Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[null]}}), NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Khu Vuc].&[{selectedKhuVuc}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER([Dim Thoi Gian].[{selectedDimension4}].Members * EXCEPT([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].MEMBERS, {{[Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[Unknown], [Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[null]}}), NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Chi Nhanh].&[{selectedChiNhanh}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = "Năm";
                                    dgv_PhanTich.Columns[1].HeaderText = selectedDimension1;
                                    dgv_PhanTich.Columns[2].HeaderText = selectedMeasure;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: Năm với sản phẩm, loại sản phẩm (xong)
                    else if (selectedDimension4 == "Nam" && (selectedDimension == "Loai san pham" || selectedDimension == "San pham"))
                    {
                        string selectedDimensionSP = "Ten " + selectedDimension;

                        if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[{selectedDimension4}].Members * EXCEPT([Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].MEMBERS, {{[Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[Unknown], [Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[null]}})), NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All') }} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER(([Dim Thoi Gian].[{selectedDimension4}].Members * EXCEPT([Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].MEMBERS, {{[Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[Unknown], [Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[null]}})), NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Khu Vuc].&[{selectedKhuVuc}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, NON EMPTY {{FILTER(([Dim Thoi Gian].[{selectedDimension4}].Members * EXCEPT([Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].MEMBERS, {{[Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[Unknown], [Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[null]}})), NOT ISEMPTY([Measures].[{selectedMeasure}]) AND [Dim Thoi Gian].[{selectedDimension4}].CurrentMember.Properties('Key') <> 'NULL' AND [Dim Thoi Gian].[Nam].CurrentMember.Name <> 'All')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Chi Nhanh].&[{selectedChiNhanh}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = "Năm";
                                    dgv_PhanTich.Columns[1].HeaderText = selectedDimension1;
                                    dgv_PhanTich.Columns[2].HeaderText = selectedMeasure;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: Tuần, tháng, quý 
                    else if ((selectedDimension4 == "Tuan" || selectedDimension4 == "Thang" || selectedDimension4 == "Quy") && selectedDimension == null)
                    {
                        if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS ), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM (SELECT ({ "[Dim Chi Nhanh].[Ten Chi Nhanh].&[" + selectedChiNhanh + "]" }) ON COLUMNS FROM [Kinh Doanh Noi That Snowflake Schema]) WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS ), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM (SELECT ({ "[Dim Chi Nhanh].[Ten Khu Vuc].&[" + selectedKhuVuc + "]" }) ON COLUMNS FROM [Kinh Doanh Noi That Snowflake Schema]) WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS ), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = "Năm";
                                    dgv_PhanTich.Columns[1].HeaderText = selectedDimension3;
                                    dgv_PhanTich.Columns[2].HeaderText = selectedMeasure1;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: Tuần, Tháng, Quý và KhachHang (xong)
                    else if ((selectedDimension4 == "Tuan" || selectedDimension4 == "Thang" || selectedDimension4 == "Quy") && (selectedDimension == "Tuoi" || selectedDimension == "Gioi tinh" || selectedDimension == "Muc thu nhap" || selectedDimension == "Nghe nghiep"))
                    {
                        if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS * EXCEPT([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].MEMBERS, {{[Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[Unknown], [Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[null]}})), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM (SELECT ({ "[Dim Chi Nhanh].[Ten Chi Nhanh].&[" + selectedChiNhanh + "]" }) ON COLUMNS FROM [Kinh Doanh Noi That Snowflake Schema]) WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS * EXCEPT([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].MEMBERS, {{[Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[Unknown], [Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[null]}})), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM (SELECT ({ "[Dim Chi Nhanh].[Ten Khu Vuc].&[" + selectedKhuVuc + "]" }) ON COLUMNS FROM [Kinh Doanh Noi That Snowflake Schema]) WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS * EXCEPT([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].MEMBERS, {{[Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[Unknown], [Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].[null]}})), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0) }} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = "Năm";
                                    dgv_PhanTich.Columns[1].HeaderText = selectedDimension3;
                                    dgv_PhanTich.Columns[2].HeaderText = selectedDimension1;
                                    dgv_PhanTich.Columns[3].HeaderText = selectedMeasure1;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: quý, tuần, tháng với sản phẩm, loại sản phẩm (xong)
                    else if ((selectedDimension4 == "Tuan" || selectedDimension4 == "Thang" || selectedDimension4 == "Quy") && (selectedDimension == "Loai san pham" || selectedDimension == "San pham"))
                    {
                        string selectedDimensionSP = "Ten " + selectedDimension;

                        if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS * EXCEPT([Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].MEMBERS, {{[Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[Unknown], [Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[null]}})), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM (SELECT ({ "[Dim Chi Nhanh].[Ten Chi Nhanh].&[" + selectedChiNhanh + "]" }) ON COLUMNS FROM [Kinh Doanh Noi That Snowflake Schema]) WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS * EXCEPT([Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].MEMBERS, {{[Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[Unknown], [Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[null]}})), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0)}} ON ROWS FROM (SELECT ({ "[Dim Chi Nhanh].[Ten Khu Vuc].&[" + selectedKhuVuc + "]" }) ON COLUMNS FROM [Kinh Doanh Noi That Snowflake Schema]) WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT NON EMPTY {{ [Measures].[Formatted {selectedMeasure}] }} ON COLUMNS, NON EMPTY {{ FILTER(([Dim Thoi Gian].[Nam].[Nam].MEMBERS * [Dim Thoi Gian].[{selectedDimension4}].[{selectedDimension4}].MEMBERS * EXCEPT([Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].MEMBERS, {{[Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[Unknown], [Dim San Pham].[{selectedDimensionSP}].[{selectedDimensionSP}].[null]}})), NOT ISNULL([Measures].[{selectedMeasure}]) AND [Measures].[{selectedMeasure}] <> 0) }} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = "Năm";
                                    dgv_PhanTich.Columns[1].HeaderText = selectedDimension3;
                                    dgv_PhanTich.Columns[2].HeaderText = selectedDimension1;
                                    dgv_PhanTich.Columns[3].HeaderText = selectedMeasure1;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: Tuổi, Mức thu nhập, Nghề Nghiệp, Giới tính (xong)
                    else if (selectedDimension == "Tuoi" || selectedDimension == "Muc thu nhap" || selectedDimension == "Nghe nghiep" || selectedDimension == "Gioi tinh")
                    {
                        if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, {{FILTER([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].Members, NOT ISNULL([Measures].[Doanh thu]) AND NOT ISNULL([Dim Khach Hang].[{selectedDimension}].CurrentMember.MemberValue) AND [Dim Khach Hang].[{selectedDimension}].CurrentMember.Name <> '')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Chi Nhanh].&[{selectedChiNhanh}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, {{FILTER([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].Members, NOT ISNULL([Measures].[Doanh thu]) AND NOT ISNULL([Dim Khach Hang].[{selectedDimension}].CurrentMember.MemberValue) AND [Dim Khach Hang].[{selectedDimension}].CurrentMember.Name <> '')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Khu Vuc].&[{selectedKhuVuc}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, {{FILTER([Dim Khach Hang].[{selectedDimension}].[{selectedDimension}].Members, NOT ISNULL([Measures].[Doanh thu]) AND NOT ISNULL([Dim Khach Hang].[{selectedDimension}].CurrentMember.MemberValue) AND [Dim Khach Hang].[{selectedDimension}].CurrentMember.Name <> '')}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = selectedDimension1;
                                    dgv_PhanTich.Columns[1].HeaderText = selectedMeasure1;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                    //Trường hợp chọn: Sản phẩm, Loại sản phẩm (xong)
                    else
                    {
                        string selectedTimeDimensionSP = "Ten " + selectedDimension;

                        if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text != "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, {{FILTER([Dim San Pham].[{selectedTimeDimensionSP}].[{selectedTimeDimensionSP}].Members, NOT ISNULL([Measures].[Doanh thu]) AND NOT ISNULL([Dim San Pham].[{selectedTimeDimensionSP}].CurrentMember.MemberValue) AND [Dim San Pham].[{selectedTimeDimensionSP}].CurrentMember.Name <> '' AND [Measures].[Doanh thu] <> NULL)}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Chi Nhanh].&[{selectedChiNhanh}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text != "Tất Cả" && cbx_ChiNhanh.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, {{FILTER([Dim San Pham].[{selectedTimeDimensionSP}].[{selectedTimeDimensionSP}].Members, NOT ISNULL([Measures].[Doanh thu]) AND NOT ISNULL([Dim San Pham].[{selectedTimeDimensionSP}].CurrentMember.MemberValue) AND [Dim San Pham].[{selectedTimeDimensionSP}].CurrentMember.Name <> '' AND [Measures].[Doanh thu] <> NULL)}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Chi Nhanh].[Ten Khu Vuc].&[{selectedKhuVuc}] * [Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }
                        else if (cbx_KhuVuc.Text == "Tất Cả")
                        {
                            mdxQuery = $@"WITH MEMBER [Measures].[Formatted {selectedMeasure}] AS FORMAT([Measures].[{selectedMeasure}], '#,##0') SELECT {{[Measures].[Formatted {selectedMeasure}]}} ON COLUMNS, {{FILTER([Dim San Pham].[{selectedTimeDimensionSP}].[{selectedTimeDimensionSP}].Members, NOT ISNULL([Measures].[Doanh thu]) AND NOT ISNULL([Dim San Pham].[{selectedTimeDimensionSP}].CurrentMember.MemberValue) AND [Dim San Pham].[{selectedTimeDimensionSP}].CurrentMember.Name <> '' AND [Measures].[Doanh thu] <> NULL)}} ON ROWS FROM [Kinh Doanh Noi That Snowflake Schema] WHERE {{[Dim Thoi Gian].[Ngay].&[{selectedStart}T00:00:00] : [Dim Thoi Gian].[Ngay].&[{selectedEnd}T00:00:00]}}";
                        }

                        // Xóa dữ liệu và cấu trúc cột trước khi cập nhật dữ liệu mới
                        dgv_PhanTich.DataSource = null;
                        dgv_PhanTich.Columns.Clear();

                        //Thực hiện truy vấn MDX từ Analyst Server và load dữ liệu vào DataGridView
                        using (AdomdConnection conn = new AdomdConnection(@"Data Source=TGER\TGER;Catalog=SSAS_DDS;Integrated Security=SSPI;"))
                        {
                            conn.Open();

                            using (AdomdCommand cmd = new AdomdCommand(mdxQuery, conn))
                            {
                                using (AdomdDataAdapter da = new AdomdDataAdapter(cmd))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);

                                    //Gán dữ liệu vào DataGridView
                                    dgv_PhanTich.DataSource = ds.Tables[0];
                                    dgv_PhanTich.Columns[0].HeaderText = selectedDimension1;
                                    dgv_PhanTich.Columns[1].HeaderText = selectedMeasure1;

                                    //Vẽ biểu đồ dựa trên dữ liệu trong DataGridView
                                    VeBieuDo(dgv_PhanTich);

                                    //Refresh DataGridView
                                    dgv_PhanTich.Refresh();
                                }
                            }

                            conn.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ thông tin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        //---------------------------Xuất data phân tích--------------------------------------
        private void btn_ReExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ DataGridView
                DataTable dt = (DataTable)dgv_PhanTich.DataSource;

                // Kiểm tra nếu DataGridView không có dữ liệu
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất ra Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Khởi tạo ứng dụng Microsoft Excel
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Hiển thị ứng dụng Excel

                // Tạo Workbook mới
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Phân tích dữ liệu";

                // Copy tên cột từ DataGridView
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv_PhanTich.Columns[i].HeaderText;
                }

                // Copy dữ liệu từ DataGridView vào Excel
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
                    }
                }

                // Lưu file Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.FileName = "DuLieuPhanTich.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất dữ liệu sang Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Giải phóng tài nguyên
                workbook.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xuất Excel!");
            }
        }

        private void btn_ReWord_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ DataGridView
                DataTable dt = (DataTable)dgv_PhanTich.DataSource;

                // Kiểm tra nếu DataGridView không có dữ liệu
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất ra Word!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Khởi tạo ứng dụng Microsoft Word
                Word.Application wordApp = new Word.Application();
                wordApp.Visible = true; // Hiển thị ứng dụng Word

                // Tạo Document mới
                Word.Document document = wordApp.Documents.Add();
                Word.Table table = document.Tables.Add(document.Range(), dt.Rows.Count + 1, dt.Columns.Count);

                // Ghi tên cột từ DataGridView vào bảng Word
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dgv_PhanTich.Columns[i].HeaderText;
                }

                // Ghi dữ liệu từ DataGridView vào bảng Word
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dt.Rows[i][j].ToString();
                    }
                }

                // Lưu file Word
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Files|*.docx|All Files|*.*";
                saveFileDialog.Title = "Save Word File";
                saveFileDialog.FileName = "DuLieuPhanTich.docx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                    MessageBox.Show("Xuất dữ liệu sang Word thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Giải phóng tài nguyên
                document.Close();
                wordApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(table);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xuất word!");
            }
        }


        //--------------------------------------------------------------------------------------------------------------------- 

        private void btn_Res_Click(object sender, EventArgs e)
        {
            rdb_MucThuNhap.Checked = false;
            rdb_Nam.Checked = false;
            rdb_NgheNghiep.Checked = false;
            rdb_Quy.Checked = false;
            rdb_SanPham.Checked = false;
            rdb_Thang.Checked = false;
            rdb_Tuan.Checked = false;
            rdb_Tuoi.Checked = false;
            rdb_Phai.Checked = false;
            rdb_LoaSanPham.Checked = false;

            rdb_LoiNhan.Checked = false;
            rdb_GiamGia.Checked = false;
            rdb_PhiVanChuyen.Checked = false;
            rdb_DoanhThu.Checked = false;
        }

    }
}
