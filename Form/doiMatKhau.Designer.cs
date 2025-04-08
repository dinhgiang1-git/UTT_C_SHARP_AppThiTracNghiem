namespace ThiTracNghiem
{
    partial class doiMatKhau
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dmkMatKhauMoi = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dmkNhapLaiMatKhauMoi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dmkbtnXacNhan = new System.Windows.Forms.Button();
            this.dmkbtnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(39, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu mới";
            // 
            // dmkMatKhauMoi
            // 
            this.dmkMatKhauMoi.Location = new System.Drawing.Point(157, 54);
            this.dmkMatKhauMoi.Name = "dmkMatKhauMoi";
            this.dmkMatKhauMoi.Size = new System.Drawing.Size(235, 20);
            this.dmkMatKhauMoi.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // dmkNhapLaiMatKhauMoi
            // 
            this.dmkNhapLaiMatKhauMoi.Location = new System.Drawing.Point(157, 99);
            this.dmkNhapLaiMatKhauMoi.Name = "dmkNhapLaiMatKhauMoi";
            this.dmkNhapLaiMatKhauMoi.Size = new System.Drawing.Size(235, 20);
            this.dmkNhapLaiMatKhauMoi.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(39, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nhạp lại mật khẩu mới";
            // 
            // dmkbtnXacNhan
            // 
            this.dmkbtnXacNhan.Location = new System.Drawing.Point(180, 147);
            this.dmkbtnXacNhan.Name = "dmkbtnXacNhan";
            this.dmkbtnXacNhan.Size = new System.Drawing.Size(75, 23);
            this.dmkbtnXacNhan.TabIndex = 5;
            this.dmkbtnXacNhan.Text = "Xác nhận";
            this.dmkbtnXacNhan.UseVisualStyleBackColor = true;
            this.dmkbtnXacNhan.Click += new System.EventHandler(this.dmkbtnXacNhan_Click);
            // 
            // dmkbtnHuy
            // 
            this.dmkbtnHuy.Location = new System.Drawing.Point(297, 147);
            this.dmkbtnHuy.Name = "dmkbtnHuy";
            this.dmkbtnHuy.Size = new System.Drawing.Size(75, 23);
            this.dmkbtnHuy.TabIndex = 6;
            this.dmkbtnHuy.Text = "Huỷ";
            this.dmkbtnHuy.UseVisualStyleBackColor = true;
            this.dmkbtnHuy.Click += new System.EventHandler(this.dmkbtnHuy_Click);
            // 
            // doiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 226);
            this.Controls.Add(this.dmkbtnHuy);
            this.Controls.Add(this.dmkbtnXacNhan);
            this.Controls.Add(this.dmkNhapLaiMatKhauMoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dmkMatKhauMoi);
            this.Controls.Add(this.label1);
            this.Name = "doiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "doiMatKhau";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dmkMatKhauMoi;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox dmkNhapLaiMatKhauMoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dmkbtnXacNhan;
        private System.Windows.Forms.Button dmkbtnHuy;
    }
}