﻿namespace MongolNote
{
    partial class VerticalUIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerticalUIForm));
            this.SuspendLayout();
            // 
            // VerticalUIForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "VerticalUIForm";
            this.Load += new System.EventHandler(this.VerticalUIForm_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VerticalUIForm_MouseUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.VerticalUIForm_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VerticalUIForm_MouseDown);
            this.Resize += new System.EventHandler(this.VerticalUIForm_Resize);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VerticalUIForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion






    }
}