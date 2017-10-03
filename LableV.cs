using System;
using System.Collections.Generic;
using System.Text;
using System.Windows .Forms ;
using System.Drawing;
namespace MongolNote
{
    class LableV:Control  
    {
        //public LabelV()
        //{

        //}
        public int M_Maxheight = 200;
        public int Maxheight
        {
            get
            {
                return M_Maxheight;
            }
            set
            {
                M_Maxheight = value;
                this.Height = value;
                
            }
        }
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (value.Size < 14)
                {
                    value = new Font(value.FontFamily, 14, value.Style); 
                }
                base.Font = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            e.Graphics.TranslateTransform(2, 10);
            VerticalText.DrawVerticalString(Text, e.Graphics , Font, Maxheight , new Point(0, 0), sf);
            MyUI.Draw3DInnerBorder(ClientRectangle, e.Graphics);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }


        //protected override void OnTextChanged(EventArgs e)
        //{
            
        //    StringFormat sf = StringFormat.GenericTypographic;
        //    sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
        //    VerticalText.InitializeLogLineList(Text, Maxheight, this.CreateGraphics(), Font, sf);
        //}
    }
    
}
