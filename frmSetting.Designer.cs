namespace MongolNote
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.panel2 = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clrFont = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clrShadow = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clrBack = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.csButtonV2 = new MongolNote.CSButtonV();
            this.csButtonV1 = new MongolNote.CSButtonV();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrShadow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrBack)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.clrFont);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.clrShadow);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.clrBack);
            this.groupBox1.Controls.Add(this.checkBox1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // clrFont
            // 
            this.clrFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clrFont.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.clrFont, "clrFont");
            this.clrFont.Name = "clrFont";
            this.clrFont.TabStop = false;
            this.clrFont.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // clrShadow
            // 
            this.clrShadow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clrShadow.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.clrShadow, "clrShadow");
            this.clrShadow.Name = "clrShadow";
            this.clrShadow.TabStop = false;
            this.clrShadow.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // clrBack
            // 
            this.clrBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clrBack.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.clrBack, "clrBack");
            this.clrBack.Name = "clrBack";
            this.clrBack.TabStop = false;
            this.clrBack.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // csButtonV2
            // 
            this.csButtonV2.BackColor = System.Drawing.SystemColors.Control;
            this.csButtonV2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.csButtonV2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.csButtonV2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.csButtonV2, "csButtonV2");
            this.csButtonV2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.csButtonV2.Name = "csButtonV2";
            this.csButtonV2.UseVisualStyleBackColor = false;
            // 
            // csButtonV1
            // 
            this.csButtonV1.BackColor = System.Drawing.SystemColors.Control;
            this.csButtonV1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.csButtonV1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.csButtonV1, "csButtonV1");
            this.csButtonV1.Name = "csButtonV1";
            this.csButtonV1.UseVisualStyleBackColor = false;
            // 
            // frmSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.csButtonV2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.csButtonV1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetting";
            this.TextV = "";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSetting_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrShadow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox clrBack;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox clrShadow;
        private System.Windows.Forms.PictureBox clrFont;
        private System.Windows.Forms.Label label3;
        private CSButtonV csButtonV1;
        private CSButtonV csButtonV2;
    }
}