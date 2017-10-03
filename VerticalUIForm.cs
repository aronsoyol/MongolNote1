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
    public partial class VerticalUIForm : Form
    {
        #region 窗体边框阴影效果变量蓙E丒

        const int GWL_STYLE = (-16);
        const int WS_CAPTION = 0x00C00000;
        //声明Win32 API


        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern int GetWindowLong(IntPtr hWnd, GWLIndex nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        #endregion
        private bool isMouseDown = false;
        private Point oldPoint;
        private string m_TextV = "丒丒 ";
        public string TextV
        {
            get { return m_TextV; }
            set { m_TextV = value; }
        }
        OperatingSystem system = Environment.OSVersion;
        OS MyOS = OS.XP;
        public VerticalUIForm()
        {
            
            //HandleRef handle = new HandleRef(this, this.Handle);
            //SetClassLong(handle, GCL_STYLE, new IntPtr ( GetClassLongPtr(handle, GCL_STYLE).ToInt32 () | CS_DropSHADOW)); //API函数加载，实现窗体边框阴影效箒E
            //SetClassLong (handle ,GCL_STYLE,
            
            InitializeComponent();
            if (system.Version.Major >= 6 && !this.DesignMode)
            {
                MyOS = OS.WIN7;
                try
                {
                    this.Load += new System.EventHandler(VerticalUIForm_LoadEX);
                }
                finally
                {
                    this.Paint += new System.Windows.Forms.PaintEventHandler(VerticalUIForm_WIN7_Paint);
                }
            }
            else
            {
                this.Paint += new System.Windows.Forms.PaintEventHandler(VerticalUIForm_XP_Paint);
                MyOS = OS.XP;
            }
        }
        enum OS
        {
            WIN7,
            XP
        }
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg != 0x85)
        //    base.WndProc(ref m);
        //}
        protected Size Offset = new Size(0, 0);
        private void VerticalUIForm_Load(object sender, EventArgs e)
        {
            int Xoffset = GetBorderSize(this).Width;
            int Yoffset = GetCaptionHeight(this);
            Offset = new Size(Xoffset, Yoffset);

            //this.BackColor = Color.Black;
        }
        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            const int HTCLIENT = 0x01;
            const int WM_NCHITTEST = 0x84;
            switch (msg.Msg)
            {
                case WM_NCHITTEST:
                    if (HTCLIENT == msg.Result.ToInt32())
                    {
                        // it's inside the client area

                        // Parse the WM_NCHITTEST message parameters
                        // get the mouse pointer coordinates (in screen coordinates)
                        Point p = new Point();
                        p.X = (msg.LParam.ToInt32() & 0xFFFF);// low order word
                        p.Y = (msg.LParam.ToInt32() >> 16); // hi order word

                        // convert screen coordinates to client area coordinates
                        p = PointToClient(p);

                        // if it's on glass, then convert it from an HTCLIENT
                        // message to an HTCAPTION message and let Windows handle it from then on
                        if (p.X<35)
                            msg.Result = new IntPtr(2);
                    }
                    break;
                    
            }
        }
        private void VerticalUIForm_LoadEX(object sender, EventArgs e)
        {
            MARGINS m = new MARGINS();
            m.leftWidth = 35;
            m.rightWidth = 0;
            m.topHeight = 0;
            m.bottomHeight = 0;

            int i = DwmExtendFrameIntoClientArea(this.Handle, ref m);
        }
        private void VerticalUIForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > 35&&e.Y >25) return;
            isMouseDown = true;
            oldPoint = e.Location;
        }

        private void VerticalUIForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;
            Point newPoint = PointToScreen(e.Location);
            
            

            Location = new Point(newPoint.X - oldPoint.X-Offset .Width , newPoint.Y - oldPoint.Y-Offset .Height  );
        }

        private void VerticalUIForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }


        private void panel2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void VerticalUIForm_XP_Paint(object sender, PaintEventArgs e)
        {

            DrawVertivalTitleBarXP(e.Graphics);

            DrawCaptionV(TextV, e.Graphics, Font);
        }
        private void VerticalUIForm_WIN7_Paint(object sender, PaintEventArgs e)
        {

            DrawVertivalTitleBarWIN7(e.Graphics);

            DrawCaptionV(TextV, e.Graphics, Font);
        }
        void DrawVertivalTitleBarWIN7(Graphics g)
        {
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(35, ClientRectangle.Height));
            Brush blackBrush = new SolidBrush(Color.Black);
            g.FillRectangle(blackBrush, rect);

        }
        void DrawVertivalTitleBarXP(Graphics g)
        {
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(35, ClientRectangle.Height));

            Brush b = new LinearGradientBrush(rect.Location, new Point(rect.Width, 0), Color.FromName("GradientActiveCaption"), Color.FromName("ActiveCaption"));
            g.FillRectangle(b, rect);
            //Pen p3 = new Pen(Color.FromName("ControlDark"));
            Pen p4 = new Pen(Color.FromName("ControlDarkDark"));
            //Pen p1 = new Pen(Color.White);
            Pen p2 = new Pen(Color.FromName("ControlLight"));


            g.DrawLine(p2, new Point(rect.Right - 1, rect.Top), new Point(rect.Right - 1, rect.Bottom));
            g.DrawLine(p4, new Point(rect.Right - 2, rect.Top), new Point(rect.Right - 2, rect.Bottom));
        }
        public  void DrawCaptionV(string Caption, Graphics g, Font font)
        {
            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            //sf.LineAlignment = StringAlignment.Center;
            Font tmpFont;
            //try
            //{
            //    tmpFont = new Font("Menk Amgalang", 20);
            //}
            //catch
            //{
                tmpFont = font;
            //}
            g.TranslateTransform(25 ,10);
            g.RotateTransform(90);
            g.SmoothingMode = SmoothingMode.HighQuality;
            //g.DrawString(Caption, tmpFont, Brushes.White, new RectangleF(1, 1, 1001, 35));
            GraphicsPath p = new GraphicsPath();
            GraphicsPath p1 = new GraphicsPath();
            p.AddString(Caption, tmpFont.FontFamily, (int)FontStyle.Regular, 18, new RectangleF(0, 0, 1000, 35), sf);
            p1.AddString(Caption, tmpFont.FontFamily, (int)FontStyle.Regular, 18, new RectangleF(1, 1, 1000, 35), sf);

            //g.DrawString(Caption, tmpFont, Brushes.White, new RectangleF(0, 0, 1000, 34));
            g.FillPath(Brushes.White , p1);
            g.FillPath(Brushes.Black, p);
            g.SmoothingMode = SmoothingMode.None;
            g.ResetTransform();
        }
        private void VerticalUIForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
        }

        private void VerticalUIForm_Resize(object sender, EventArgs e)
        {

            //this.SetClientSizeCore(ClientRectangle.Width - 60, ClientRectangle.Height - 50);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.MaximizedBounds = Screen.GetWorkingArea(this);


            WindowState = FormWindowState.Maximized;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal ;
        }
        public static int GetCaptionHeight(Form form)
        {
            return form.FormBorderStyle != FormBorderStyle.SizableToolWindow &&
                   form.FormBorderStyle != FormBorderStyle.FixedToolWindow
                       ? SystemInformation.CaptionHeight + 2
                       : SystemInformation.ToolWindowCaptionHeight + 1;
        }
        public static Size GetBorderSize(Form form)
        {
            Size border = new Size(0, 0);

            // Check for Caption
            int style =  GetWindowLong(form.Handle, GWL_STYLE).ToInt32 ();
            bool caption = (style & (int)(WS_CAPTION)) != 0;
            int factor = SystemInformation.BorderMultiplierFactor - 1;

            OperatingSystem system = Environment.OSVersion;
            bool isVista = system.Version.Major >= 6;
            switch (form.FormBorderStyle)
            {
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.FixedSingle:
                case FormBorderStyle.FixedDialog:
                    border = SystemInformation.FixedFrameBorderSize;
                    break;
                case FormBorderStyle.SizableToolWindow:
                case FormBorderStyle.Sizable:
                    if (isVista)
                        border = SystemInformation.FrameBorderSize;
                    else
                        border = SystemInformation.FixedFrameBorderSize +
                            (caption ? SystemInformation.BorderSize + new Size(factor, factor)
                                : new Size(factor, factor));
                    break;
                case FormBorderStyle.Fixed3D:
                    border = SystemInformation.FixedFrameBorderSize + SystemInformation.Border3DSize;
                    break;
            }

            return border;
        }

        private void VerticalUIForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //MongolNote1 .StringList vsl=new MongolNote1.StringList (textBox2.Text);

            //textBox3.Text = vsl.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //MongolNote1.StringList vsl = new MongolNote1.StringList(textBox2.Text);
            //vsl.Add (textBox1 .Text );
            //textBox3.Text = vsl.Text;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //MongolNote1.StringList vsl1 = new MongolNote1.StringList(textBox1.Text);
            //MongolNote1.StringList vsl2 = new MongolNote1.StringList(textBox2.Text);
            ////vsl1.Add(vsl2);
            //vsl1.Insert(vsl2 ,12);
            //textBox3.Text = vsl1.Text;
        }
    }
}