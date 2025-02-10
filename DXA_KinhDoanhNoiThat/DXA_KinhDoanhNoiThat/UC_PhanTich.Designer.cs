namespace DXA_KinhDoanhNoiThat
{
    partial class UC_PhanTich
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            this.pnl_PT = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.dgv_PhanTich = new System.Windows.Forms.DataGridView();
            this.pnt_ChieuPT = new System.Windows.Forms.Panel();
            this.grb_Chieu1 = new System.Windows.Forms.GroupBox();
            this.rdb_Nam = new System.Windows.Forms.RadioButton();
            this.rdb_Quy = new System.Windows.Forms.RadioButton();
            this.rdb_Thang = new System.Windows.Forms.RadioButton();
            this.rdb_Tuan = new System.Windows.Forms.RadioButton();
            this.grb_ChieuThem = new System.Windows.Forms.GroupBox();
            this.btn_Res = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_ReWord = new System.Windows.Forms.Button();
            this.btn_ReExcel = new System.Windows.Forms.Button();
            this.btn_ThucHienPT = new System.Windows.Forms.Button();
            this.lbl_KhuVuc = new System.Windows.Forms.Label();
            this.cbx_KhuVuc = new System.Windows.Forms.ComboBox();
            this.lbl_ChiNhanh = new System.Windows.Forms.Label();
            this.cbx_ChiNhanh = new System.Windows.Forms.ComboBox();
            this.grb_Chieu2 = new System.Windows.Forms.GroupBox();
            this.rdb_SanPham = new System.Windows.Forms.RadioButton();
            this.rdb_LoaSanPham = new System.Windows.Forms.RadioButton();
            this.rdb_Phai = new System.Windows.Forms.RadioButton();
            this.rdb_NgheNghiep = new System.Windows.Forms.RadioButton();
            this.rdb_MucThuNhap = new System.Windows.Forms.RadioButton();
            this.rdb_Tuoi = new System.Windows.Forms.RadioButton();
            this.grb_TinhToan = new System.Windows.Forms.GroupBox();
            this.rdb_GiamGia = new System.Windows.Forms.RadioButton();
            this.rdb_PhiVanChuyen = new System.Windows.Forms.RadioButton();
            this.rdb_LoiNhan = new System.Windows.Forms.RadioButton();
            this.rdb_DoanhThu = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_PhanTich = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_PT.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PhanTich)).BeginInit();
            this.pnt_ChieuPT.SuspendLayout();
            this.grb_Chieu1.SuspendLayout();
            this.grb_ChieuThem.SuspendLayout();
            this.grb_Chieu2.SuspendLayout();
            this.grb_TinhToan.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_PT
            // 
            this.pnl_PT.Controls.Add(this.panel2);
            this.pnl_PT.Controls.Add(this.pnt_ChieuPT);
            this.pnl_PT.Controls.Add(this.panel1);
            this.pnl_PT.Location = new System.Drawing.Point(3, 3);
            this.pnl_PT.Name = "pnl_PT";
            this.pnl_PT.Size = new System.Drawing.Size(681, 512);
            this.pnl_PT.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.Controls.Add(this.chartControl1);
            this.panel2.Controls.Add(this.dgv_PhanTich);
            this.panel2.Location = new System.Drawing.Point(4, 203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 306);
            this.panel2.TabIndex = 2;
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Legend.LegendID = -1;
            this.chartControl1.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.None;
            this.chartControl1.Legend.TextVisible = false;
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(354, 2);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.Size = new System.Drawing.Size(317, 300);
            this.chartControl1.TabIndex = 1;
            // 
            // dgv_PhanTich
            // 
            this.dgv_PhanTich.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PhanTich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PhanTich.Location = new System.Drawing.Point(3, 2);
            this.dgv_PhanTich.Name = "dgv_PhanTich";
            this.dgv_PhanTich.RowHeadersWidth = 51;
            this.dgv_PhanTich.RowTemplate.Height = 24;
            this.dgv_PhanTich.Size = new System.Drawing.Size(345, 300);
            this.dgv_PhanTich.TabIndex = 0;
            // 
            // pnt_ChieuPT
            // 
            this.pnt_ChieuPT.BackColor = System.Drawing.Color.LightCyan;
            this.pnt_ChieuPT.Controls.Add(this.grb_Chieu1);
            this.pnt_ChieuPT.Controls.Add(this.grb_ChieuThem);
            this.pnt_ChieuPT.Controls.Add(this.grb_Chieu2);
            this.pnt_ChieuPT.Controls.Add(this.grb_TinhToan);
            this.pnt_ChieuPT.Location = new System.Drawing.Point(4, 62);
            this.pnt_ChieuPT.Name = "pnt_ChieuPT";
            this.pnt_ChieuPT.Size = new System.Drawing.Size(674, 135);
            this.pnt_ChieuPT.TabIndex = 1;
            // 
            // grb_Chieu1
            // 
            this.grb_Chieu1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grb_Chieu1.Controls.Add(this.rdb_Nam);
            this.grb_Chieu1.Controls.Add(this.rdb_Quy);
            this.grb_Chieu1.Controls.Add(this.rdb_Thang);
            this.grb_Chieu1.Controls.Add(this.rdb_Tuan);
            this.grb_Chieu1.Location = new System.Drawing.Point(4, 38);
            this.grb_Chieu1.Name = "grb_Chieu1";
            this.grb_Chieu1.Size = new System.Drawing.Size(411, 32);
            this.grb_Chieu1.TabIndex = 3;
            this.grb_Chieu1.TabStop = false;
            // 
            // rdb_Nam
            // 
            this.rdb_Nam.AutoSize = true;
            this.rdb_Nam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_Nam.Location = new System.Drawing.Point(321, 8);
            this.rdb_Nam.Name = "rdb_Nam";
            this.rdb_Nam.Size = new System.Drawing.Size(68, 24);
            this.rdb_Nam.TabIndex = 11;
            this.rdb_Nam.TabStop = true;
            this.rdb_Nam.Text = "Năm";
            this.rdb_Nam.UseVisualStyleBackColor = true;
            // 
            // rdb_Quy
            // 
            this.rdb_Quy.AutoSize = true;
            this.rdb_Quy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_Quy.Location = new System.Drawing.Point(203, 8);
            this.rdb_Quy.Name = "rdb_Quy";
            this.rdb_Quy.Size = new System.Drawing.Size(63, 24);
            this.rdb_Quy.TabIndex = 10;
            this.rdb_Quy.TabStop = true;
            this.rdb_Quy.Text = "Quý";
            this.rdb_Quy.UseVisualStyleBackColor = true;
            // 
            // rdb_Thang
            // 
            this.rdb_Thang.AutoSize = true;
            this.rdb_Thang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_Thang.Location = new System.Drawing.Point(101, 8);
            this.rdb_Thang.Name = "rdb_Thang";
            this.rdb_Thang.Size = new System.Drawing.Size(81, 24);
            this.rdb_Thang.TabIndex = 9;
            this.rdb_Thang.TabStop = true;
            this.rdb_Thang.Text = "Tháng";
            this.rdb_Thang.UseVisualStyleBackColor = true;
            // 
            // rdb_Tuan
            // 
            this.rdb_Tuan.AutoSize = true;
            this.rdb_Tuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_Tuan.Location = new System.Drawing.Point(11, 8);
            this.rdb_Tuan.Name = "rdb_Tuan";
            this.rdb_Tuan.Size = new System.Drawing.Size(71, 24);
            this.rdb_Tuan.TabIndex = 8;
            this.rdb_Tuan.TabStop = true;
            this.rdb_Tuan.Text = "Tuần";
            this.rdb_Tuan.UseVisualStyleBackColor = true;
            // 
            // grb_ChieuThem
            // 
            this.grb_ChieuThem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grb_ChieuThem.Controls.Add(this.btn_Res);
            this.grb_ChieuThem.Controls.Add(this.dateTimePicker2);
            this.grb_ChieuThem.Controls.Add(this.dateTimePicker1);
            this.grb_ChieuThem.Controls.Add(this.btn_ReWord);
            this.grb_ChieuThem.Controls.Add(this.btn_ReExcel);
            this.grb_ChieuThem.Controls.Add(this.btn_ThucHienPT);
            this.grb_ChieuThem.Controls.Add(this.lbl_KhuVuc);
            this.grb_ChieuThem.Controls.Add(this.cbx_KhuVuc);
            this.grb_ChieuThem.Controls.Add(this.lbl_ChiNhanh);
            this.grb_ChieuThem.Controls.Add(this.cbx_ChiNhanh);
            this.grb_ChieuThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_ChieuThem.Location = new System.Drawing.Point(417, 6);
            this.grb_ChieuThem.Name = "grb_ChieuThem";
            this.grb_ChieuThem.Size = new System.Drawing.Size(254, 123);
            this.grb_ChieuThem.TabIndex = 2;
            this.grb_ChieuThem.TabStop = false;
            // 
            // btn_Res
            // 
            this.btn_Res.BackgroundImage = global::DXA_KinhDoanhNoiThat.Properties.Resources.icons8_repeat_48;
            this.btn_Res.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Res.Location = new System.Drawing.Point(8, 90);
            this.btn_Res.Name = "btn_Res";
            this.btn_Res.Size = new System.Drawing.Size(30, 30);
            this.btn_Res.TabIndex = 11;
            this.btn_Res.UseVisualStyleBackColor = true;
            this.btn_Res.Click += new System.EventHandler(this.btn_Res_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(130, 9);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(120, 27);
            this.dateTimePicker2.TabIndex = 10;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(4, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 27);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btn_ReWord
            // 
            this.btn_ReWord.BackgroundImage = global::DXA_KinhDoanhNoiThat.Properties.Resources.icons8_word_48;
            this.btn_ReWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ReWord.Location = new System.Drawing.Point(107, 90);
            this.btn_ReWord.Name = "btn_ReWord";
            this.btn_ReWord.Size = new System.Drawing.Size(30, 30);
            this.btn_ReWord.TabIndex = 6;
            this.btn_ReWord.UseVisualStyleBackColor = true;
            this.btn_ReWord.Click += new System.EventHandler(this.btn_ReWord_Click);
            // 
            // btn_ReExcel
            // 
            this.btn_ReExcel.BackgroundImage = global::DXA_KinhDoanhNoiThat.Properties.Resources.icons8_excel_48;
            this.btn_ReExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ReExcel.Location = new System.Drawing.Point(73, 90);
            this.btn_ReExcel.Name = "btn_ReExcel";
            this.btn_ReExcel.Size = new System.Drawing.Size(30, 30);
            this.btn_ReExcel.TabIndex = 5;
            this.btn_ReExcel.UseVisualStyleBackColor = true;
            this.btn_ReExcel.Click += new System.EventHandler(this.btn_ReExcel_Click);
            // 
            // btn_ThucHienPT
            // 
            this.btn_ThucHienPT.Location = new System.Drawing.Point(141, 90);
            this.btn_ThucHienPT.Name = "btn_ThucHienPT";
            this.btn_ThucHienPT.Size = new System.Drawing.Size(107, 30);
            this.btn_ThucHienPT.TabIndex = 4;
            this.btn_ThucHienPT.Text = "Thực hiện";
            this.btn_ThucHienPT.UseVisualStyleBackColor = true;
            this.btn_ThucHienPT.Click += new System.EventHandler(this.btn_ThucHienPT_Click);
            // 
            // lbl_KhuVuc
            // 
            this.lbl_KhuVuc.AutoSize = true;
            this.lbl_KhuVuc.Location = new System.Drawing.Point(4, 40);
            this.lbl_KhuVuc.Name = "lbl_KhuVuc";
            this.lbl_KhuVuc.Size = new System.Drawing.Size(82, 20);
            this.lbl_KhuVuc.TabIndex = 3;
            this.lbl_KhuVuc.Text = "Khu vực:";
            // 
            // cbx_KhuVuc
            // 
            this.cbx_KhuVuc.FormattingEnabled = true;
            this.cbx_KhuVuc.Location = new System.Drawing.Point(76, 35);
            this.cbx_KhuVuc.Name = "cbx_KhuVuc";
            this.cbx_KhuVuc.Size = new System.Drawing.Size(174, 28);
            this.cbx_KhuVuc.TabIndex = 2;
            this.cbx_KhuVuc.SelectedIndexChanged += new System.EventHandler(this.cbo_KhuVuc_SelectedIndexChanged);
            // 
            // lbl_ChiNhanh
            // 
            this.lbl_ChiNhanh.AutoSize = true;
            this.lbl_ChiNhanh.Location = new System.Drawing.Point(4, 67);
            this.lbl_ChiNhanh.Name = "lbl_ChiNhanh";
            this.lbl_ChiNhanh.Size = new System.Drawing.Size(102, 20);
            this.lbl_ChiNhanh.TabIndex = 1;
            this.lbl_ChiNhanh.Text = "Chi Nhánh:";
            // 
            // cbx_ChiNhanh
            // 
            this.cbx_ChiNhanh.FormattingEnabled = true;
            this.cbx_ChiNhanh.Location = new System.Drawing.Point(76, 62);
            this.cbx_ChiNhanh.Name = "cbx_ChiNhanh";
            this.cbx_ChiNhanh.Size = new System.Drawing.Size(174, 28);
            this.cbx_ChiNhanh.TabIndex = 0;
            // 
            // grb_Chieu2
            // 
            this.grb_Chieu2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grb_Chieu2.Controls.Add(this.rdb_SanPham);
            this.grb_Chieu2.Controls.Add(this.rdb_LoaSanPham);
            this.grb_Chieu2.Controls.Add(this.rdb_Phai);
            this.grb_Chieu2.Controls.Add(this.rdb_NgheNghiep);
            this.grb_Chieu2.Controls.Add(this.rdb_MucThuNhap);
            this.grb_Chieu2.Controls.Add(this.rdb_Tuoi);
            this.grb_Chieu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_Chieu2.Location = new System.Drawing.Point(4, 73);
            this.grb_Chieu2.Name = "grb_Chieu2";
            this.grb_Chieu2.Size = new System.Drawing.Size(411, 57);
            this.grb_Chieu2.TabIndex = 1;
            this.grb_Chieu2.TabStop = false;
            // 
            // rdb_SanPham
            // 
            this.rdb_SanPham.AutoSize = true;
            this.rdb_SanPham.Location = new System.Drawing.Point(203, 31);
            this.rdb_SanPham.Name = "rdb_SanPham";
            this.rdb_SanPham.Size = new System.Drawing.Size(113, 24);
            this.rdb_SanPham.TabIndex = 15;
            this.rdb_SanPham.TabStop = true;
            this.rdb_SanPham.Text = "Sản phẩm";
            this.rdb_SanPham.UseVisualStyleBackColor = true;
            // 
            // rdb_LoaSanPham
            // 
            this.rdb_LoaSanPham.AutoSize = true;
            this.rdb_LoaSanPham.Location = new System.Drawing.Point(11, 31);
            this.rdb_LoaSanPham.Name = "rdb_LoaSanPham";
            this.rdb_LoaSanPham.Size = new System.Drawing.Size(153, 24);
            this.rdb_LoaSanPham.TabIndex = 14;
            this.rdb_LoaSanPham.TabStop = true;
            this.rdb_LoaSanPham.Text = "Loại sản phẩm";
            this.rdb_LoaSanPham.UseVisualStyleBackColor = true;
            // 
            // rdb_Phai
            // 
            this.rdb_Phai.AutoSize = true;
            this.rdb_Phai.Location = new System.Drawing.Point(321, 4);
            this.rdb_Phai.Name = "rdb_Phai";
            this.rdb_Phai.Size = new System.Drawing.Size(101, 24);
            this.rdb_Phai.TabIndex = 13;
            this.rdb_Phai.TabStop = true;
            this.rdb_Phai.Text = "Giới tính";
            this.rdb_Phai.UseVisualStyleBackColor = true;
            // 
            // rdb_NgheNghiep
            // 
            this.rdb_NgheNghiep.AutoSize = true;
            this.rdb_NgheNghiep.Location = new System.Drawing.Point(203, 4);
            this.rdb_NgheNghiep.Name = "rdb_NgheNghiep";
            this.rdb_NgheNghiep.Size = new System.Drawing.Size(134, 24);
            this.rdb_NgheNghiep.TabIndex = 12;
            this.rdb_NgheNghiep.TabStop = true;
            this.rdb_NgheNghiep.Text = "Nghề nghiệp";
            this.rdb_NgheNghiep.UseVisualStyleBackColor = true;
            // 
            // rdb_MucThuNhap
            // 
            this.rdb_MucThuNhap.AutoSize = true;
            this.rdb_MucThuNhap.Location = new System.Drawing.Point(101, 4);
            this.rdb_MucThuNhap.Name = "rdb_MucThuNhap";
            this.rdb_MucThuNhap.Size = new System.Drawing.Size(143, 24);
            this.rdb_MucThuNhap.TabIndex = 11;
            this.rdb_MucThuNhap.TabStop = true;
            this.rdb_MucThuNhap.Text = "Mức thu nhập";
            this.rdb_MucThuNhap.UseVisualStyleBackColor = true;
            // 
            // rdb_Tuoi
            // 
            this.rdb_Tuoi.AutoSize = true;
            this.rdb_Tuoi.Location = new System.Drawing.Point(11, 4);
            this.rdb_Tuoi.Name = "rdb_Tuoi";
            this.rdb_Tuoi.Size = new System.Drawing.Size(66, 24);
            this.rdb_Tuoi.TabIndex = 10;
            this.rdb_Tuoi.TabStop = true;
            this.rdb_Tuoi.Text = "Tuổi";
            this.rdb_Tuoi.UseVisualStyleBackColor = true;
            // 
            // grb_TinhToan
            // 
            this.grb_TinhToan.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grb_TinhToan.Controls.Add(this.rdb_GiamGia);
            this.grb_TinhToan.Controls.Add(this.rdb_PhiVanChuyen);
            this.grb_TinhToan.Controls.Add(this.rdb_LoiNhan);
            this.grb_TinhToan.Controls.Add(this.rdb_DoanhThu);
            this.grb_TinhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_TinhToan.Location = new System.Drawing.Point(4, 6);
            this.grb_TinhToan.Name = "grb_TinhToan";
            this.grb_TinhToan.Size = new System.Drawing.Size(411, 30);
            this.grb_TinhToan.TabIndex = 0;
            this.grb_TinhToan.TabStop = false;
            // 
            // rdb_GiamGia
            // 
            this.rdb_GiamGia.AutoSize = true;
            this.rdb_GiamGia.Location = new System.Drawing.Point(321, 1);
            this.rdb_GiamGia.Name = "rdb_GiamGia";
            this.rdb_GiamGia.Size = new System.Drawing.Size(105, 24);
            this.rdb_GiamGia.TabIndex = 3;
            this.rdb_GiamGia.TabStop = true;
            this.rdb_GiamGia.Text = "Giảm giá";
            this.rdb_GiamGia.UseVisualStyleBackColor = true;
            // 
            // rdb_PhiVanChuyen
            // 
            this.rdb_PhiVanChuyen.AutoSize = true;
            this.rdb_PhiVanChuyen.Location = new System.Drawing.Point(203, 1);
            this.rdb_PhiVanChuyen.Name = "rdb_PhiVanChuyen";
            this.rdb_PhiVanChuyen.Size = new System.Drawing.Size(157, 24);
            this.rdb_PhiVanChuyen.TabIndex = 2;
            this.rdb_PhiVanChuyen.TabStop = true;
            this.rdb_PhiVanChuyen.Text = "Phí vận chuyển";
            this.rdb_PhiVanChuyen.UseVisualStyleBackColor = true;
            // 
            // rdb_LoiNhan
            // 
            this.rdb_LoiNhan.AutoSize = true;
            this.rdb_LoiNhan.Location = new System.Drawing.Point(101, 1);
            this.rdb_LoiNhan.Name = "rdb_LoiNhan";
            this.rdb_LoiNhan.Size = new System.Drawing.Size(112, 24);
            this.rdb_LoiNhan.TabIndex = 1;
            this.rdb_LoiNhan.TabStop = true;
            this.rdb_LoiNhan.Text = "Lợi nhuận";
            this.rdb_LoiNhan.UseVisualStyleBackColor = true;
            // 
            // rdb_DoanhThu
            // 
            this.rdb_DoanhThu.AutoSize = true;
            this.rdb_DoanhThu.Location = new System.Drawing.Point(11, 1);
            this.rdb_DoanhThu.Name = "rdb_DoanhThu";
            this.rdb_DoanhThu.Size = new System.Drawing.Size(116, 24);
            this.rdb_DoanhThu.TabIndex = 0;
            this.rdb_DoanhThu.TabStop = true;
            this.rdb_DoanhThu.Text = "Doanh thu";
            this.rdb_DoanhThu.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.lbl_PhanTich);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 52);
            this.panel1.TabIndex = 0;
            // 
            // lbl_PhanTich
            // 
            this.lbl_PhanTich.AutoSize = true;
            this.lbl_PhanTich.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PhanTich.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PhanTich.ForeColor = System.Drawing.Color.White;
            this.lbl_PhanTich.Location = new System.Drawing.Point(178, 14);
            this.lbl_PhanTich.Name = "lbl_PhanTich";
            this.lbl_PhanTich.Size = new System.Drawing.Size(492, 38);
            this.lbl_PhanTich.TabIndex = 0;
            this.lbl_PhanTich.Text = "Phân Tích Dữ Liệu Kinh Doanh";
            // 
            // UC_PhanTich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_PT);
            this.Name = "UC_PhanTich";
            this.Size = new System.Drawing.Size(687, 518);
            this.pnl_PT.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PhanTich)).EndInit();
            this.pnt_ChieuPT.ResumeLayout(false);
            this.grb_Chieu1.ResumeLayout(false);
            this.grb_Chieu1.PerformLayout();
            this.grb_ChieuThem.ResumeLayout(false);
            this.grb_ChieuThem.PerformLayout();
            this.grb_Chieu2.ResumeLayout(false);
            this.grb_Chieu2.PerformLayout();
            this.grb_TinhToan.ResumeLayout(false);
            this.grb_TinhToan.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_PT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_PhanTich;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_PhanTich;
        private System.Windows.Forms.Panel pnt_ChieuPT;
        private System.Windows.Forms.GroupBox grb_ChieuThem;
        private System.Windows.Forms.Button btn_ReWord;
        private System.Windows.Forms.Button btn_ReExcel;
        private System.Windows.Forms.Button btn_ThucHienPT;
        private System.Windows.Forms.Label lbl_KhuVuc;
        private System.Windows.Forms.ComboBox cbx_KhuVuc;
        private System.Windows.Forms.Label lbl_ChiNhanh;
        private System.Windows.Forms.ComboBox cbx_ChiNhanh;
        private System.Windows.Forms.GroupBox grb_Chieu2;
        private System.Windows.Forms.GroupBox grb_TinhToan;
        private System.Windows.Forms.RadioButton rdb_GiamGia;
        private System.Windows.Forms.RadioButton rdb_PhiVanChuyen;
        private System.Windows.Forms.RadioButton rdb_LoiNhan;
        private System.Windows.Forms.RadioButton rdb_DoanhThu;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.RadioButton rdb_SanPham;
        private System.Windows.Forms.RadioButton rdb_LoaSanPham;
        private System.Windows.Forms.RadioButton rdb_Phai;
        private System.Windows.Forms.RadioButton rdb_NgheNghiep;
        private System.Windows.Forms.RadioButton rdb_MucThuNhap;
        private System.Windows.Forms.RadioButton rdb_Tuoi;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox grb_Chieu1;
        private System.Windows.Forms.RadioButton rdb_Nam;
        private System.Windows.Forms.RadioButton rdb_Quy;
        private System.Windows.Forms.RadioButton rdb_Thang;
        private System.Windows.Forms.RadioButton rdb_Tuan;
        private System.Windows.Forms.Button btn_Res;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
    }
}
