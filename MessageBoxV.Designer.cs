namespace MongolNote
{
    partial class MessageBoxV
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lableV1 = new MongolNote.LableV();
            this.csButtonV1 = new MongolNote.CSButtonV();
            this.SuspendLayout();
            // 
            // lableV1
            // 
            this.lableV1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lableV1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lableV1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableV1.Location = new System.Drawing.Point(40, 10);
            this.lableV1.Maxheight = 200;
            this.lableV1.Name = "lableV1";
            this.lableV1.Size = new System.Drawing.Size(154, 317);
            this.lableV1.TabIndex = 6;
            this.lableV1.Text = " ertertsdfg岁的法国";
            // 
            // csButtonV1
            // 
            this.csButtonV1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.csButtonV1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.csButtonV1.Location = new System.Drawing.Point(199, 12);
            this.csButtonV1.Margin = new System.Windows.Forms.Padding(2);
            this.csButtonV1.Name = "csButtonV1";
            this.csButtonV1.Size = new System.Drawing.Size(30, 90);
            this.csButtonV1.TabIndex = 5;
            this.csButtonV1.Text = "";
            this.csButtonV1.UseVisualStyleBackColor = true;
            // 
            // MessageBoxV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(242, 337);
            this.ControlBox = false;
            this.Controls.Add(this.lableV1);
            this.Controls.Add(this.csButtonV1);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBoxV";
            this.Padding = new System.Windows.Forms.Padding(40, 10, 45, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MessageBoxV_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MessageBoxV_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MessageBoxV_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MessageBoxV_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MessageBoxV_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private CSButtonV csButtonV1;
        private LableV lableV1;



    }
}