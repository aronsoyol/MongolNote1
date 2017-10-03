using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MongolNote
{
    public partial class frmFont : Form
    {
        //public frmFont()
        //{
        //    InitializeComponent();
        //    //try
        //    //{
        //    //    MyFont = new Font(
        //    //}
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("Çë°²×°ÃÉ¿ËÁ¢ÃÉÎÄ×ÖÌå\n"+ex.Message );
        //    }
            
        //}
        public frmFont(Font aFont,bool shadow,Color bgColor)
        {
            InitializeComponent();
            mFont = aFont;
            mFontSize = (int)aFont.Size;
            mFontFamily = aFont.FontFamily;
            IsShadow = shadow;
            checkBox1.Checked = IsShadow;
            aBackColor = bgColor;
           
            
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Çë°²×°ÃÉ¿ËÁ¢ÃÉÎÄ×ÖÌå\n"+ex.Message );
            //}
            numericUpDown1.Value  = mFontSize;

        }


        public  Font mFont;
        private FontFamily mFontFamily;
        private int mFontSize;
        private FontStyle mFontStyle;
        public bool IsShadow;
        public Color aBackColor;


        private void frmFont_Load(object sender, EventArgs e)
        {
            System.Drawing.Text.InstalledFontCollection objFont;
            objFont = new System.Drawing.Text.InstalledFontCollection();
            string s = "321654";

            foreach (System.Drawing.FontFamily i in objFont.Families)
            {
                s = i.Name.ToString();

                FontStyle tmpFS = frmMain.GetSampleStyle(mFontFamily);
                if (mFontFamily.IsStyleAvailable(tmpFS))
                {
                    lstFont.Items.Add(s);
                    if (s == mFontFamily.Name.ToString())
                    {
                        lstFont.SelectedIndex = lstFont.Items.Count - 1;

                    }
                    //if (s == string.Empty) s = "Nothing!";

                }
                //mFontFamily=
            }

        }
        //private FontStyle GetSampleStyle(FontFamily aFF)
        //{
        //    FontStyle aFS = new FontStyle();
        //    if (aFF.IsStyleAvailable(FontStyle.Regular))
        //    {
        //        //aFS=new 
        //        aFS = FontStyle.Regular;
        //    }
        //    else if (aFF.IsStyleAvailable(FontStyle.Italic))
        //    {
        //        aFS = FontStyle.Italic;
        //    }
        //    else if (aFF.IsStyleAvailable(FontStyle.Strikeout))
        //    {
        //        aFS = FontStyle.Strikeout;
        //    }

        //    else if (aFF.IsStyleAvailable(FontStyle.Bold))
        //    {
        //        aFS = FontStyle.Bold;
        //    }
        //    else if (aFF.IsStyleAvailable(FontStyle.Underline))
        //    {
        //        aFS = FontStyle.Underline;
        //    }
        //    //else
        //    //{
        //    //    return nu;
        //    //}
        //    return aFS;

        //}
        private void lstFont_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            try
            {
                FontFamily aFF = new FontFamily(lstFont.Items[e.Index].ToString());
                FontStyle aFS = frmMain.GetSampleStyle(aFF);

                System.Drawing.Font objFonts = new Font(aFF, 14, aFS);
                e.ItemHeight = objFonts.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nÇë°²×°ÃÉ¿ÆÁ¢×ÖÌå¡£ ");
            }

        }
        private void lstFont_DrawItem(object sender, DrawItemEventArgs e)
        {
            
            
            FontFamily aFF = new FontFamily(lstFont.Items[e.Index].ToString());
            FontStyle aFS = frmMain.GetSampleStyle(aFF);
            if (!aFF.IsStyleAvailable(aFS)) return;
            System.Drawing.Font objFonts = new Font(aFF, 14f, aFS);
            e.DrawBackground();

            string s = lstFont.Items[e.Index].ToString();
            if (s == string.Empty) s = "Nothing!";
            e.Graphics.DrawString(s
                                                , objFonts
                                                , new SolidBrush(e.ForeColor)
                                                , new Point(e.Bounds.Left, e.Bounds.Top));
            
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "\nÇë°²×°ÃÉ¿ÆÁ¢×ÖÌå¡£ ");
            //}
            //e.Graphics.


        }

        private void lstFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            mFontFamily = new FontFamily(lstFont.SelectedItem.ToString());
            
            panel2.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
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
                g.DrawString(strSapmle, mFont, Brushes.White, new PointF(-1, 1));
            g.DrawString(strSapmle, mFont, Brushes.Black, new PointF(0, 0));
            

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mFontSize =(int) numericUpDown1.Value;
            panel2.Invalidate();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mFontStyle = FontStyle.Regular;
            panel2.Invalidate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mFontStyle = FontStyle.Bold ;
            panel2.Invalidate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mFontStyle = FontStyle.Italic ;
            panel2.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                panel2.BackColor = colorDialog1.Color;
                aBackColor = colorDialog1.Color;

            }
            panel2.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsShadow = checkBox1.Checked;
            panel2.Invalidate();
        }
    }
}