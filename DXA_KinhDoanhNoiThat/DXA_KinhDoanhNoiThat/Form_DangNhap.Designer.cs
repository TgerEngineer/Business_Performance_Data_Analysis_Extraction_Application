namespace DXA_KinhDoanhNoiThat
{
    partial class Form_DangNhap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DangNhap));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDangNhap = new System.Windows.Forms.Label();
            this.btnDangNhap_DN = new System.Windows.Forms.Button();
            this.panelMatKhau = new System.Windows.Forms.Panel();
            this.lbl_QuenMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau_DN = new System.Windows.Forms.TextBox();
            this.lblMatKhau_DN = new System.Windows.Forms.Label();
            this.panelTaiKhoan = new System.Windows.Forms.Panel();
            this.txtTenDangNhap_DN = new System.Windows.Forms.TextBox();
            this.lblTenDangNhap_DN = new System.Windows.Forms.Label();
            this.oDataInstantFeedbackSource1 = new DevExpress.Data.ODataLinq.ODataInstantFeedbackSource();
            this.panel1.SuspendLayout();
            this.panelMatKhau.SuspendLayout();
            this.panelTaiKhoan.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DXA_KinhDoanhNoiThat.Properties.Resources.noi_that_la_gi;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(593, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(491, 456);
            this.panel3.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblDangNhap);
            this.panel1.Controls.Add(this.btnDangNhap_DN);
            this.panel1.Controls.Add(this.panelMatKhau);
            this.panel1.Controls.Add(this.panelTaiKhoan);
            this.panel1.Location = new System.Drawing.Point(41, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 456);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::DXA_KinhDoanhNoiThat.Properties.Resources.noi_that_the_one_logo;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(124, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 100);
            this.panel2.TabIndex = 7;
            // 
            // lblDangNhap
            // 
            this.lblDangNhap.AutoSize = true;
            this.lblDangNhap.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangNhap.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblDangNhap.Location = new System.Drawing.Point(86, 137);
            this.lblDangNhap.Name = "lblDangNhap";
            this.lblDangNhap.Size = new System.Drawing.Size(392, 38);
            this.lblDangNhap.TabIndex = 5;
            this.lblDangNhap.Text = "ĐĂNG NHẬP HỆ THỐNG";
            // 
            // btnDangNhap_DN
            // 
            this.btnDangNhap_DN.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDangNhap_DN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap_DN.Location = new System.Drawing.Point(204, 390);
            this.btnDangNhap_DN.Name = "btnDangNhap_DN";
            this.btnDangNhap_DN.Size = new System.Drawing.Size(143, 52);
            this.btnDangNhap_DN.TabIndex = 3;
            this.btnDangNhap_DN.Text = "Đăng nhập";
            this.btnDangNhap_DN.UseVisualStyleBackColor = false;
            this.btnDangNhap_DN.Click += new System.EventHandler(this.btnDangNhap_DN_Click);
            // 
            // panelMatKhau
            // 
            this.panelMatKhau.Controls.Add(this.lbl_QuenMatKhau);
            this.panelMatKhau.Controls.Add(this.txtMatKhau_DN);
            this.panelMatKhau.Controls.Add(this.lblMatKhau_DN);
            this.panelMatKhau.Location = new System.Drawing.Point(10, 292);
            this.panelMatKhau.Name = "panelMatKhau";
            this.panelMatKhau.Size = new System.Drawing.Size(533, 92);
            this.panelMatKhau.TabIndex = 1;
            // 
            // lbl_QuenMatKhau
            // 
            this.lbl_QuenMatKhau.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.lbl_QuenMatKhau.AutoSize = true;
            this.lbl_QuenMatKhau.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lbl_QuenMatKhau.Location = new System.Drawing.Point(422, 63);
            this.lbl_QuenMatKhau.Name = "lbl_QuenMatKhau";
            this.lbl_QuenMatKhau.Size = new System.Drawing.Size(108, 17);
            this.lbl_QuenMatKhau.TabIndex = 2;
            this.lbl_QuenMatKhau.Text = "Quên mật khẩu!";
            // 
            // txtMatKhau_DN
            // 
            this.txtMatKhau_DN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhau_DN.Location = new System.Drawing.Point(180, 22);
            this.txtMatKhau_DN.Multiline = true;
            this.txtMatKhau_DN.Name = "txtMatKhau_DN";
            this.txtMatKhau_DN.PasswordChar = '*';
            this.txtMatKhau_DN.Size = new System.Drawing.Size(350, 28);
            this.txtMatKhau_DN.TabIndex = 1;
            // 
            // lblMatKhau_DN
            // 
            this.lblMatKhau_DN.AutoSize = true;
            this.lblMatKhau_DN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau_DN.Location = new System.Drawing.Point(58, 26);
            this.lblMatKhau_DN.Name = "lblMatKhau_DN";
            this.lblMatKhau_DN.Size = new System.Drawing.Size(98, 23);
            this.lblMatKhau_DN.TabIndex = 0;
            this.lblMatKhau_DN.Text = "Mật khẩu:";
            // 
            // panelTaiKhoan
            // 
            this.panelTaiKhoan.Controls.Add(this.txtTenDangNhap_DN);
            this.panelTaiKhoan.Controls.Add(this.lblTenDangNhap_DN);
            this.panelTaiKhoan.Location = new System.Drawing.Point(10, 193);
            this.panelTaiKhoan.Name = "panelTaiKhoan";
            this.panelTaiKhoan.Size = new System.Drawing.Size(533, 82);
            this.panelTaiKhoan.TabIndex = 0;
            // 
            // txtTenDangNhap_DN
            // 
            this.txtTenDangNhap_DN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDangNhap_DN.Location = new System.Drawing.Point(180, 22);
            this.txtTenDangNhap_DN.Multiline = true;
            this.txtTenDangNhap_DN.Name = "txtTenDangNhap_DN";
            this.txtTenDangNhap_DN.Size = new System.Drawing.Size(350, 28);
            this.txtTenDangNhap_DN.TabIndex = 1;
            // 
            // lblTenDangNhap_DN
            // 
            this.lblTenDangNhap_DN.AutoSize = true;
            this.lblTenDangNhap_DN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhap_DN.Location = new System.Drawing.Point(12, 25);
            this.lblTenDangNhap_DN.Name = "lblTenDangNhap_DN";
            this.lblTenDangNhap_DN.Size = new System.Drawing.Size(139, 23);
            this.lblTenDangNhap_DN.TabIndex = 0;
            this.lblTenDangNhap_DN.Text = "Tên đăng nhập:";
            // 
            // Form_DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DXA_KinhDoanhNoiThat.Properties.Resources.Nen_DN;
            this.ClientSize = new System.Drawing.Size(1124, 613);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_DangNhap";
            this.Text = "Đăng Nhập";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelMatKhau.ResumeLayout(false);
            this.panelMatKhau.PerformLayout();
            this.panelTaiKhoan.ResumeLayout(false);
            this.panelTaiKhoan.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau_DN;
        private System.Windows.Forms.Label lblMatKhau_DN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDangNhap;
        private System.Windows.Forms.Button btnDangNhap_DN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTaiKhoan;
        private System.Windows.Forms.TextBox txtTenDangNhap_DN;
        private System.Windows.Forms.Label lblTenDangNhap_DN;
        private DevExpress.Data.ODataLinq.ODataInstantFeedbackSource oDataInstantFeedbackSource1;
        private System.Windows.Forms.Label lbl_QuenMatKhau;
    }
}