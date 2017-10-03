using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace MongolNote
{
    public partial class MessageBoxV : Form
    {

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);

        public static IntPtr GetClassLongPtr(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(HandleRef hWnd, int nIndex);


        public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size > 4)
                return SetClassLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetClassLongPtr32(hWnd, nIndex, unchecked((uint)dwNewLong.ToInt32())));
        }

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        public static extern uint SetClassLongPtr32(HandleRef hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        public static extern IntPtr SetClassLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        public MessageBoxV(string text ,string caption)
        {
            Text   = text;
            Caption = caption;
            InitializeComponent();

        }
        private Bitmap bmp;
        //public string Message="";
        private string m_Caption = "";
        public string Caption
        {
            get {
                return m_Caption;
            }
            set
            {
                m_Caption = value;
            }
        }
        private string m_Text = "";
        public override string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                base.Text = "";
            }
        }
        public static DialogResult  Show(string text)
        {
            MessageBoxV m = new MessageBoxV(text, "oshirase");
            return m.ShowDialog();
            //m.ShowDialog (
        }
        private void MessageBoxV_Paint(object sender, PaintEventArgs e)
        {

            Rectangle InnerRect = new Rectangle(ClientRectangle.X + 5+30, ClientRectangle.Y + 5
                , ClientRectangle.Width - 11-30, ClientRectangle.Height - 11);


            
            //GraphicsPath gpInner = Util_GDI.GetRoundRectangle2(InnerRect, 5);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //Color caption = Color.FromArgb(200, Color.FromName("ActiveCaption"));
            Rectangle rect = ClientRectangle;

            rect.Height -= 2;
            rect.Width -= 2;
            Region rgBorder = new Region(ClientRectangle);
            rgBorder.Exclude(InnerRect);

            e.Graphics.FillRegion(new SolidBrush(Color.FromName("ActiveCaption")), rgBorder);


            Pen border1 = new Pen(Color.DimGray , 1);
            Pen border2 = new Pen(Color.DimGray , 1);

            e.Graphics.DrawPath(border1, MyUI .GetRoundRectangle (rect,6));
            e.Graphics.DrawRectangle(border2, InnerRect);
            MyUI.DrawCaptionV(Caption , e.Graphics,this.Font );
        }
        bool isMouseDown = false;
        Point oldPoint;
        private void MessageBoxV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > 35) return;
            isMouseDown = true;
            oldPoint = e.Location;
        }

        private void MessageBoxV_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;
            //int dx=e.X -oldPoint.X ;
            //int dy=e.Y-oldPoint .Y  ;
            //this.Location=new Point (this.Location.X + dx,
            //this.Location.Y + dy);
            Point newPoint = PointToScreen(e.Location);
            Location = new Point( newPoint .X -oldPoint .X ,newPoint.Y -oldPoint .Y );

            //this.loca
            //oldPoint = e.Location;
            //this,Location 
            //cli
            //e.Location 
        }

        private void MessageBoxV_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (bmp == null) return;
            e.Graphics.DrawImage(bmp, new Point(0, 0));
        }

        

        //private void MessageBoxV_Load(object sender, EventArgs e)
        //{
        //    bmp = new Bitmap(panel2.Size.Width, panel2.Height);
        //    Graphics bmpG = Graphics.FromImage(bmp);
        //    bmpG.Clear(panel2.BackColor);
        //    StringFormat mStringFormat = StringFormat.GenericTypographic;
        //    mStringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
        //    //mStringFormat.Alignment = StringAlignment.Center;//这也会影响TextboxV
        //    VerticalText.DrawVerticalString(Text, bmpG, panel2.Font, panel2.Height - 10,
        //        new Point(0, 5), mStringFormat);
        //}

        private void lableV2_Click(object sender, EventArgs e)
        {

        }
        GraphicsPath gpClient;
        private void MessageBoxV_Load(object sender, EventArgs e)
        {
            gpClient = MyUI.GetRoundRectangle(
                new Rectangle(this.ClientRectangle.Location
                                     , new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1)), 6);
            this.Region = new Region(gpClient);
            lableV1.Text = Text;
            HandleRef handle = new HandleRef(this, this.Handle);
            SetClassLong(handle, GCL_STYLE, new IntPtr(GetClassLongPtr(handle, GCL_STYLE).ToInt32() | CS_DropSHADOW)); //API函数加载，实现窗体边框阴影效果
            
        }
    }
}