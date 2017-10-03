using System;
using System.Windows;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using WinformLx.Class; //引用之前工具类的命名空间
namespace MongolNote
{
    public class CSButtonV : System.Windows.Forms.Button
    {
        private bool mouseover = false;//鼠标经过
        private bool isMouseDown = false;
        public CSButtonV()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Height = 100;
            this.Width = 30;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //在这里用自己的方法来绘制Button的外观(其实也就是几个框框)
            Graphics g = e.Graphics;
            g.Clear(this.Parent .BackColor  );
            //this.CreateGraphics().Clear(Color.Blue);
            Rectangle rect = this.ClientRectangle;
            rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 2);
            //g.ReleaseHdc();
            if (isMouseDown)
            {
                MyUI.DrawRoundButton(this.Text,Font , g, rect, buttonStyle.ButtonClick );
                return;
            
            }
            if (mouseover)
            {
                if (Focused)
                {
                    MyUI.DrawRoundButton(this.Text, Font, g, rect, buttonStyle.ButtonFocuseAndMouseOver);
                    return;
                }
                MyUI.DrawRoundButton(this.Text, Font, g, rect, buttonStyle.ButtonMouseOver);
                return;
            }
            if (Focused)
            {
                MyUI.DrawRoundButton(this.Text, Font, g, rect, buttonStyle.ButtonFocuse);
                return;
            }
            MyUI.DrawRoundButton(this.Text, Font, g, rect, buttonStyle.ButtonNormal);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            mouseover = true;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            mouseover = false;
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            isMouseDown = true ;
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            isMouseDown = false;
            base.OnMouseUp(mevent);
        }
    }

}