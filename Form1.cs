using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace MongolNote
{
    public partial class Form1 : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern int  DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MARGINS m = new MARGINS();
            m.leftWidth = -1;
            m.rightWidth = -1;
            m.topHeight = -1;
            m.bottomHeight = -1;

            DwmExtendFrameIntoClientArea(this.Handle, ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MongolNote.CStringList list = new MongolNote.CStringList(textBox1.Text);
            if(list.Remove(Convert.ToInt32(textBox2.Text)))
            textBox1.Text = list.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MongolNote.CStringList list = new MongolNote.CStringList(textBox1.Text);
            int i=list.Insert("Ins", Convert.ToInt32(textBox2.Text)) ;
                textBox1.Text = list.Text;
            Console .WriteLine (i);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MongolNote.CStringList list = new MongolNote.CStringList(textBox1.Text);
            int i = list.Add("Add");
            textBox1.Text = list.Text;
            Console.WriteLine(i);
        }
    }
}