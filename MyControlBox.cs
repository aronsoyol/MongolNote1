using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace MongolNote
{
    class MyControlBoxButton:Panel 
    {
        public MyControlBoxButton( )
        {
            Height = 18;
            Width = 26;
        }
        //private ControlButtonType ControlButton;
        private StatusStrip statusStrip1;
        private MouseStateType MouseState= MouseStateType .Normal ;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (MouseState)
            {
                case MouseStateType .Normal :
                    MyUI.DrawRoundButton("", Font, g, ClientRectangle, buttonStyle.ButtonNormal);
                    break;
                case MouseStateType .MouseOver :
                    MyUI.DrawRoundButton("", Font, g, ClientRectangle, buttonStyle.ButtonMouseOver);
                    break;

                default :
                    break ;
            }

            base.OnPaint(e);
        }

        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(200, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";

            this.ResumeLayout(false);

        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseState = MouseStateType.MouseOver;
            base.OnMouseMove(e);
            this.Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            MouseState = MouseStateType.Normal;
            base.OnMouseLeave(e);
            this.Invalidate();
        }
        //private void MyControlBoxButton_MouseLeave(object sender, EventArgs e)
        //{
            
        //}
        //protected override  
    }

    enum ControlButtonType
    {
        MinmizeButton,
        MaximizeButton,
        CloseButton
    }
    enum MouseStateType
    {
        Normal,
        MouseOver,
        MouseDown,
        MouseUp
    }
}
