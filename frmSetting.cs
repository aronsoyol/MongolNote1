using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MongolNote
{
    public partial class frmSetting : VerticalUIForm
    {
        public frmSetting()
        {
            InitializeComponent();
            
        }
        public frmSetting(Font aFont, bool shadow, Color frColor,Color bgColor,Color ShadowColor)
        {
            InitializeComponent();
            mFont = aFont;
            mFontSize = 16;
            mFontFamily = aFont.FontFamily;
            IsShadow = shadow;
            aBackColor = bgColor;
            panel2.BackColor = bgColor;
            clrBack.BackColor = bgColor;
            checkBox1.Checked = shadow;
            aShadowColor = ShadowColor;
            clrShadow.BackColor =ShadowColor;
            clrFont.BackColor = frColor;
            aForeColor = frColor;
        }
        public Font mFont;
        private FontFamily mFontFamily;
        private int mFontSize;
        private FontStyle mFontStyle;
        public bool IsShadow;
        public Color aBackColor;
        public Color aShadowColor;
        public Color aForeColor;

        private void frmSetting_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            string strSapmle = "ùÐøìúÈøëú¬ùúøâ   úÒøëùÄ   ú¯øþúãú»\nøÓúØøëùûøþùÏú³ù­úÑøþúª ù­ùÄ ú£øþùËùûøþúª";
            StringFormat mStringFormat;
            mStringFormat = StringFormat.GenericTypographic;
            mStringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            if (!mFontFamily.IsStyleAvailable(mFontStyle))
            {
                mFontStyle = frmMain.GetSampleStyle(mFontFamily);
                if (!mFontFamily.IsStyleAvailable(mFontStyle)) return;
            }
            mFont = new Font(mFontFamily, mFontSize, mFontStyle);
            Graphics g = panel2.CreateGraphics();
            SizeF aSize = g.MeasureString(strSapmle, mFont, panel2.ClientRectangle.Height, mStringFormat);

            float x = panel2.Width / 2 - aSize.Height / 2 + aSize.Height;
            float y = panel2.Height / 2 - aSize.Width / 2;

            g.TranslateTransform(x, y);
            g.RotateTransform(90);





            //g.FillRectangle(Brushes.White, new Rectangle(0, 0, panel2.Width, panel2.Height));if
            if(IsShadow )
                g.DrawString(strSapmle, mFont, new SolidBrush  (aShadowColor ), new PointF(-1, 1));
            g.DrawString(strSapmle, mFont, new SolidBrush(aForeColor ), new PointF(0, 0));
            

       }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsShadow = checkBox1.Checked;
            panel2.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrBack.BackColor = colorDialog1.Color;
                panel2.BackColor = colorDialog1.Color;
                aBackColor = colorDialog1.Color;

            }
            panel2.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            IsShadow = checkBox1.Checked;
            panel2.Invalidate();
            clrShadow.Enabled = IsShadow;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrBack.BackColor = colorDialog1.Color;
                panel2.BackColor = colorDialog1.Color;
                aBackColor = colorDialog1.Color;

            }
            panel2.Invalidate();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrShadow.BackColor = colorDialog1.Color;
                aShadowColor  = colorDialog1.Color;

            }
            panel2.Invalidate();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                aForeColor = colorDialog1.Color;
                clrFont.BackColor = colorDialog1.Color;

            }
            panel2.Invalidate();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmSetting_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}