using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MongolNote
{
    public partial class CustomControl1 : Control
    {
        public CustomControl1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }

        private void CustomControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
