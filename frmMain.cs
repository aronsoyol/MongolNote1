using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

using System.IO;

namespace MongolNote
{
    public partial class frmMain : Form//VerticalUIForm
    {
        public frmMain()
        {
            InitializeComponent();
            mFont = tbv.Font;
            mFontSize = (int)tbv.Font.Size;
            mFontFamily = tbv.Font.FontFamily;
        }
        //protected
        private string strFileName;
        private string AppName;

        public Font mFont;
        private FontFamily mFontFamily;
        private int mFontSize;
        private FontStyle mFontStyle;

        


        private void Form1_Load(object sender, EventArgs e)
        {
 
            AppName = this.Text;
            
            //FontStyle aFS = new FontStyle();
            //aFS = FontStyle.Regular;
            ////tbv.Font = new Font("",12,aFS );
            toolStripComboBox2.ComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            toolStripComboBox2.ComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstFont_DrawItem);
            toolStripComboBox2.ComboBox.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstFont_MeasureItem);
            InitialFontList(toolStripComboBox2.ComboBox);

            for (int i = 5; i < 31; i++)
            {
                

                toolStripComboBox5.Items.Add(i.ToString());
                if (i == tbv.Font.Size)
                    toolStripComboBox5.SelectedIndex = toolStripComboBox5.Items.Count - 1;
            }
            tbv.Width = tbv.Width > this.ClientRectangle.Width ? tbv.Width : this.ClientRectangle.Width;
            //
            

        }
        //protected override void WndProc(ref Message m)
        //{
        //    if( m.Msg ==0x50)//tbv. WM_INPUTLANGCHANGE)
        //    {
        //       string s="d";
        //    }
        //    base.WndProc(ref m);
        //}
        private void lstFont_MeasureItem(object sender, MeasureItemEventArgs e)
        {

            string FontName = toolStripComboBox2.ComboBox.Items[e.Index].ToString();
            FontFamily aFF = new FontFamily(FontName);
            FontStyle aFS = FontStyle.Regular;
            int ItemHeight = 0;
            try
            {
                aFS = GetSampleStyle(aFF);
                System.Drawing.Font objFonts = new Font(aFF, 14, aFS);
                ItemHeight = objFonts.Height;
            }
            catch (Exception ex)
            {
                ItemHeight = 25;
                MessageBox.Show("GetSampleStyle出错了\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBoxV message = new MessageBoxV("GetSampleStyle出错了\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //message.ShowDialog();
            }
            finally
            {

                e.ItemHeight = ItemHeight;
            }
            


        }
        
        private void lstFont_DrawItem(object sender, DrawItemEventArgs e)
        {


            FontFamily aFF = new FontFamily(toolStripComboBox2.ComboBox.Items[e.Index].ToString());
            FontStyle aFS=new FontStyle ();


            
            e.DrawBackground();

            string s = toolStripComboBox2.ComboBox.Items[e.Index].ToString();
            if (String.IsNullOrEmpty(s) ) s = "Nothing!";


            try
            {
                aFS = GetSampleStyle(aFF);
                System.Drawing.Font objFonts = new Font(aFF, 14f, aFS);
                e.Graphics.DrawString(s, objFonts
                                                    , new SolidBrush(e.ForeColor)
                                                    , new Point(e.Bounds.Left, e.Bounds.Top));
            }
            catch(Exception ex)
            {
                s = ex.Message + " ,\n" + ex.Source;


            }

        }
        public static FontStyle GetSampleStyle(FontFamily aFF)
        {
            FontStyle aFS = new FontStyle();
            bool isFound = false;

            if (aFF.IsStyleAvailable(FontStyle.Regular))
            {
                //aFS=new 
                aFS = FontStyle.Regular;
                isFound = true;
            }
            else if (aFF.IsStyleAvailable(FontStyle.Italic))
            {
                aFS = FontStyle.Italic; isFound = true;
            }
            else if (aFF.IsStyleAvailable(FontStyle.Strikeout))
            {
                aFS = FontStyle.Strikeout; isFound = true;
            }

            else if (aFF.IsStyleAvailable(FontStyle.Bold))
            {
                aFS = FontStyle.Bold; isFound = true;
            }
            else if (aFF.IsStyleAvailable(FontStyle.Underline))
            {
                aFS = FontStyle.Underline; isFound = true;
            }
            else if (aFF.IsStyleAvailable((FontStyle)3))
            {
                aFS =(FontStyle)3;
                isFound = true;
            }
            if (isFound == false)
            {
                Exception ex=new Exception ("字体:"+aFF.Name+  "没有可用的字体样式");
                throw (ex);
            }
            return aFS;

        }
        private void InitialFontList(object lstFontCoboBox)
        {
            ComboBox lstFont = (ComboBox)lstFontCoboBox;
            System.Drawing.Text.InstalledFontCollection objFont;
            objFont = new System.Drawing.Text.InstalledFontCollection();
            string s = "321654";

            foreach (System.Drawing.FontFamily i in objFont.Families)
            {
                s = i.Name.ToString();
      
                lstFont.Items.Add(s);
    
                if (s == mFontFamily.Name.ToString())
                    lstFont.SelectedIndex = lstFont.Items.Count - 1;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image.
            //pictureBox1.Invalidate();
            //textBox1.Text = tbv.Lines[0];
        }
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg )
        //    {

        //        case  test .clsTextBox.WM_CHAR :
        //            MessageBox .Show ("!!!!!");
        //            break;
        //        default :
        //            base.WndProc(ref m);
        //            break;
        //    }
            
        //}

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            
            ////g.TranslateTransform(10, 10);
            ////g.DrawLine(Pens.Black, new Point(-100, 0), new Point(100, 0));
            //g.DrawImage(tbv.bmp,new Point (0,0));

        }

        //private void tbv_SizeChanged(object sender, EventArgs e)
        //{

        //}

        //private void tbv_Resize(object sender, EventArgs e)
        //{

        //}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case MongolEditor.TextBoxV .WM_PAINT:
        //            Graphics g = this.CreateGraphics();
        //            g.FillRectangle(Brushes.BlueViolet, this.ClientRectangle);
        //            base.DefWndProc(ref m);
        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }
        //}


        private void tbv_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //textBox2.Text = tbv.Text;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.Text);
        }

        //private void Form1_Resize(object sender, EventArgs e)
        //{
        //    tbv.Width = tbv.MinContextWidth > this.ClientRectangle.Width ? tbv.MinContextWidth : this.ClientRectangle.Width;
        //}

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(string .IsNullOrEmpty ( tbv.Text ))return ;

            DialogResult dr=(MessageBox .Show ("是否保存文件？","保存", MessageBoxButtons.YesNoCancel ,  MessageBoxIcon.Question));
            if(dr==DialogResult .Cancel )return ;
            if(dr== DialogResult .Yes)
            {
                另存为AToolStripMenuItem_Click(sender, e);

            }
            tbv.Text = "";
            strFileName = "";
            this.Text = AppName + "   " + "新建文件1*.txt";
            
            

        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbv.Text))
            {
                //MessageBoxV message=new 
                DialogResult dr = MessageBoxV.Show("是否保存文件？");
                //DialogResult dr = (MessageBox.Show("是否保存文件？", "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));
                if (dr == DialogResult.Cancel) return;
                if (dr == DialogResult.Yes)
                {
                    另存为AToolStripMenuItem_Click(sender, e);

                }
                ofDlg.Filter = "文本文件(*.TXT)|*.TXT";
            }
            string context;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                //try
                //{
                //MessageBox.Show(ofDlg.FileName);
                strFileName = ofDlg.FileName;
                this.Text = AppName + "    " + strFileName;
                StreamReader sr = File.OpenText(strFileName);
                //sr.ReadLine
                context = sr.ReadToEnd();
                //this.fr
                //CreateObjectFroText(context);
                tbv.Text = context;
            }
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strFileName))
            {

                savDlg.Filter = "文本文件(*.TXT)|*.TXT";
                savDlg.FileName = "文件1.txt";
                DialogResult dr1 = savDlg.ShowDialog();
                if (dr1 == DialogResult.OK)
                {
                    

                    strFileName = savDlg.FileName;
                    this.Text = AppName + "    " + strFileName;

                    StreamWriter sw;
                    sw = File.CreateText(savDlg.FileName);
                    sw.Write(tbv.Text);

                    sw.Close();
                }
            }
            else
            {
                StreamWriter sw;
                sw = File.CreateText(strFileName);


                sw.Write(tbv.Text);

                sw.Close();
            }
            
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savDlg.Filter = "文本文件(*.TXT)|*.TXT";
            savDlg.FileName = "文件1.txt";
            DialogResult dr1 = savDlg.ShowDialog();
            if (dr1 == DialogResult.OK)
            {
                StreamWriter sw;
                sw = File.CreateText(savDlg.FileName);


                sw.Write(tbv.Text);

                sw.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbv.Text))
            {
                //DialogResult dr = MessageBox.Show("确实要关闭吗？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult dr = MessageBoxV.Show("确实要关闭吗？");
                if (dr == DialogResult.Yes )
                    e.Cancel = false;
                else e.Cancel = true;
            }
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 选项OToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmFont FontDiolg = new frmFont(tbv .Font ,tbv.isShawdow ,tbv.BackColor );

            DialogResult dr= FontDiolg.ShowDialog();
            if (dr == DialogResult.OK )
            {
                tbv.isShawdow = FontDiolg.IsShadow;
                tbv.BackColor = FontDiolg.aBackColor; 
                tbv.Font = FontDiolg.mFont;
                
                //string str = tbv.Text;
                tbv.Invalidate();
            }
        }

        private void 关于AToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //AboutBox2 MyAboutDlg = new AboutBox2();
            //MyAboutDlg.ShowDialog(); 
            AboutBoxV MyAboutDlg = new AboutBoxV();
            MyAboutDlg.ShowDialog();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbv.Cut();
        }

        private void tbv_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void 编辑EToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbv.Copy();
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbv.Paste();
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbv.SelectAll();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tbv_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            mFontFamily = new FontFamily(toolStripComboBox2.SelectedItem.ToString());
            ChangeFont();
        }
        
        private void toolStripComboBox3_Click(object sender, EventArgs e)
        {

        }
        int Bold = 0;
        int Italic = 0;
        int Underline = 0;
        private void toolStripComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            mFontSize = Convert.ToInt32(toolStripComboBox5.SelectedItem.ToString());
            ChangeFont();

        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            toolStripButton4.Checked = !(toolStripButton4.Checked);
            if (toolStripButton4.Checked)
                Bold = (int)FontStyle.Bold;
            else
                Bold = 0;
            mFontStyle = FontStyle.Bold ;
            ChangeFont();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripButton5.Checked = !(toolStripButton5.Checked);
            if (toolStripButton5.Checked)
                Italic = (int)FontStyle.Italic;
            else
                Italic = 0;
            ChangeFont();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton6.Checked = !(toolStripButton6.Checked);
            if (toolStripButton6.Checked)
                Underline = (int)FontStyle.Underline;
            else
                Underline = 0;

            ChangeFont();
        }


        private void ChangeFont()
        {
            mFontStyle = (FontStyle  )(Bold + Italic + Underline);
            try
            {
                tbv.Font = new Font(mFontFamily, mFontSize, mFontStyle);
            }
            catch (Exception ex)
            {
                MessageBoxV.Show(ex.Message);
                //MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mFontStyle = GetSampleStyle(mFontFamily);
                tbv.Font = new Font(mFontFamily, mFontSize, mFontStyle);
            }

        }

        private void toolStripComboBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void 选项OToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSetting SetDialog = new frmSetting(tbv.Font, tbv.isShawdow,tbv .ForeColor , tbv.BackColor,tbv.ShadowColor );
            DialogResult dr = SetDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                tbv.isShawdow = SetDialog.IsShadow;
                tbv.BackColor = SetDialog.aBackColor;
                tbv.ForeColor = SetDialog.aForeColor;
                tbv.Font = SetDialog.mFont;
                tbv.ShadowColor = SetDialog.aShadowColor;
                panel1.BackColor = tbv.BackColor;
                //string str = tbv.Text;
                tbv.Invalidate();
            }
        }


        //private int ScrollBarValue = 0;

        private void tbv_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == false) return;

        }
        private bool IsMouseDown = false;

        private void tbv_Paint(object sender, PaintEventArgs e)
        {
            int i = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBoxV message = new MessageBoxV(" ", " ");
            message.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            VerticalUIForm a = new VerticalUIForm();
            a.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void tbv_TextChanged_3(object sender, EventArgs e)
        {

        }

    }
      
}