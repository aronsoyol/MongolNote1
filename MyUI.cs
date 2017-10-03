using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace MongolNote
{
    enum buttonStyle
    {
        /// <summary>
        /// ﾕｳ｣ﾎｪﾑ｡ﾖﾐｰｴﾅ･
        /// </summary>
        ButtonNormal,
        /// <summary>
        /// ｻﾃｽｹｵ羞ﾄｰｴﾅ･
        /// </summary>
        ButtonFocuse,
        /// <summary>
        /// ﾊ・ｭｹﾑｽ
        /// </summary>
        ButtonMouseOver,
        /// <summary>
        /// ｻﾃｽｹｵ羇｢ﾊ・ｭｹ
        /// </summary>
        ButtonFocuseAndMouseOver,
        /// <summary>
        /// ﾊ・ｴﾏﾂﾗｴﾌｬ
        /// </summary>
        ButtonClick
    }
    /// <summary>
    /// ﾗﾔｶｨﾒ薑DIｹ､ｾﾟ｣ｬｻ贍ﾆｰｴﾅ･
    /// </summary>
    class MyUI
    {
        /// <summary>
        /// ｻ贍ﾆﾔｲﾐﾎｰｴﾅ･｣ｨﾓﾃｷｨﾍｬｾﾘﾐﾎｰｴﾅ･｣ｩ
        /// </summary>
        /// <param name="text"></param>
        /// <param name="g"></param>
        /// <param name="Location"></param>
        /// <param name="r"></param>
        /// <param name="btnStyle"></param>
        public static void DrawCircleButton(string text, Graphics g, Point Location, int r, buttonStyle btnStyle)
        {
            Graphics Gcircle = g;
            Rectangle rect = new Rectangle(Location.X, Location.Y, r, r);
            Pen p = new Pen(new SolidBrush(Color.Black));
            Gcircle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Gcircle.DrawEllipse(p, rect);
            if (btnStyle == buttonStyle.ButtonFocuse)
            {
                Gcircle.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#338FCC")), rect);
            }
                
            else if (btnStyle == buttonStyle.ButtonMouseOver)
            {
                Gcircle.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#EAC100")), rect);
            }
            else if (btnStyle == buttonStyle.ButtonFocuseAndMouseOver)
            {
                Gcircle.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#EAC100")), rect);
            }

            p.DashStyle = DashStyle.Dash;
            if (btnStyle != buttonStyle.ButtonNormal)
            {

                Gcircle.DrawEllipse(p, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4));//ﾐ鰕ﾟｿ・
            }
            Gcircle.FillEllipse(new SolidBrush(Color.WhiteSmoke), new Rectangle(rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Height - 6));
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Gcircle.DrawString(text, new Font(new FontFamily("ﾋﾎﾌ・ "), 10), new SolidBrush(Color.Black), rect, sf);
            p.Dispose();
        }
        /// <summary> 
        /// ｻ贍ﾆﾔｲｽﾇｰｴﾅ･
        /// </summary> 
        /// <param name="Text">ﾒｪｻ贍ﾆｵﾄﾎﾄﾗﾖ</param>
        /// <param name="g">Graphics ｶﾔﾏ・/param> 
        /// <param name="rect">ﾒｪﾌ鋧莊ﾄｾﾘﾐﾎ</param> 
        /// <param name="btnStyle"></param>
        public static void DrawRoundButton(string Text,Font aFont, Graphics g, Rectangle rect, buttonStyle btnStyle)
        {
            //g.Clear(Color. );
            g.SmoothingMode = SmoothingMode.AntiAlias;//ﾏ﨤ｾ箋ﾝ
            Rectangle rectangle = rect;
            Brush b  = new SolidBrush(Color.Black);
            Brush Back;
            //Region r =new Region (
            GraphicsPath gp = GetRoundRectangle(rect, 2);
            if (btnStyle == buttonStyle.ButtonFocuse)
            {
                b = new SolidBrush(ColorTranslator.FromHtml("#338FCC"));
                //g.FillRectangle(new SolidBrush(Color.AliceBlue), rectangle);//
                Back=new SolidBrush(Color.AliceBlue);
                g.FillPath (Back ,gp);
            }
            else if (btnStyle == buttonStyle.ButtonMouseOver)
            {
                b = new SolidBrush(ColorTranslator.FromHtml("#C6A300"));
                //g.FillRectangle(new SolidBrush(Color.Bisque), rectangle);//
                Back=new SolidBrush(Color.Bisque);
                g.FillPath (Back ,gp);
            }
            else if (btnStyle == buttonStyle.ButtonFocuseAndMouseOver)
            {
                b = new SolidBrush(ColorTranslator.FromHtml("#C6A300"));
                //g.FillRectangle(new SolidBrush(Color.DimGray), rectangle);//ｰﾗﾉｫｱｳｾｰ
            }else if(btnStyle == buttonStyle.ButtonClick )
            {
                b = new SolidBrush(ColorTranslator.FromHtml("#C6A3FF"));
                //g.FillRectangle(new SolidBrush(Color.CadetBlue), rectangle);//ｰﾗﾉｫｱｳｾｰ
                Back=new SolidBrush(Color.CadetBlue);
                g.FillPath (Back ,gp);
            }
            
            g.DrawPath(new Pen(b), gp);
            rectangle = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
            Pen p = new Pen(Color.Black, 0.5f);
            p.DashStyle = DashStyle.Dash;
            if (btnStyle == buttonStyle.ButtonFocuse || btnStyle == buttonStyle.ButtonFocuseAndMouseOver)
            {
                g.DrawRectangle(p, rectangle);//ﾐ鰕ﾟｿ・
            }
            
            StringFormat sf = new StringFormat();

            
            Font tmpFont;
            try
            {
                tmpFont = new Font("Menk Amgalang", 12);
            }
            catch
            {
                tmpFont = aFont;
            }
            SizeF s = g.MeasureString(Text, aFont, 1000, sf);
            g.TranslateTransform((rectangle.Width + tmpFont.Height) / 2, (rectangle.Height - (int)s.Width) / 2);
            g.RotateTransform(90);
            g.DrawString(Text, tmpFont, new SolidBrush(Color.Black), new PointF(0f, 0f), sf);
            p.Dispose();
            b.Dispose();
            g.SmoothingMode = SmoothingMode.Default;
            g.ResetTransform();
        }

        /// <summary> 
        /// ｸﾝﾆﾕﾍｨｾﾘﾐﾎｵﾃｵｽﾔｲｽﾇｾﾘﾐﾎｵﾄﾂｷｾｶ 
        /// </summary> 
        /// <param name="rectangle">ﾔｭﾊｼｾﾘﾐﾎ</param> 
        /// <param name="r">ｰ・ｶ</param> 
        /// <returns>ﾍｼﾐﾎﾂｷｾｶ</returns> 
        public  static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            // ｰﾑﾔｲｽﾇｾﾘﾐﾎｷﾖｳﾉｰﾋｶﾎﾖｱﾏﾟ｡｢ｻ｡ｵﾄﾗ鮗ﾏ｣ｬﾒﾀｴﾎｼﾓｵｽﾂｷｾｶﾖﾐ  
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);
            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);
            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);
            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            return gp;
        }
        public static  void DrawCaptionV(string Caption ,Graphics g,Font font)
        {
            //StringFormat sf = StringFormat.GenericTypographic;
            //sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            //sf.LineAlignment = StringAlignment.Center;
            Font tmpFont;
            try
            {
                tmpFont = new Font("Menksoft Qagan", 16);
            }
            catch
            {
                tmpFont = font;
            }
            g.TranslateTransform(tmpFont.Height, 10);
            g.RotateTransform(90);
            g.DrawString(Caption, tmpFont, Brushes.Black, new RectangleF(0, 0, 1000, 35));
            g.DrawString(Caption, tmpFont, Brushes.Black, new RectangleF(0, 0, 1000, 35));
            g.ResetTransform();
        }
        public static void DrawCaption(string Caption, Graphics g, Font font)
        {
            StringFormat sf =new StringFormat ();
            sf= StringFormat.GenericTypographic;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;


            //sf.Alignment = StringAlignment.Center;//ﾎｪﾊｲﾃｴﾆﾃﾋ・瞠ｰﾏ・extBoxV｣ｿ
            //sf.LineAlignment = StringAlignment.Center;

            font = new Font(font.FontFamily, 12);
            //font.
            g.DrawString(Caption, font, Brushes.Black, new RectangleF(40, 0, 200, 30), sf);
            
        }
        public static void SetVerticalUI(Rectangle ClientRectangle, Graphics g, Color BackColor)
        {

            Rectangle InnerRect = new Rectangle(ClientRectangle.X + 5 + 30, ClientRectangle.Y + 5+20
                    , ClientRectangle.Width - 42, ClientRectangle.Height - 32);

            Region ClientRegion = new Region(ClientRectangle);
            ClientRegion.Exclude(InnerRect);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRegion(new SolidBrush(Color.FromName("ActiveCaption")), ClientRegion);//ｱ・ｩﾀｸｺﾍﾍ篩・
            g.FillRectangle(new SolidBrush(BackColor), InnerRect);//ﾄﾚｲｿｱｳｾｲ

            Pen border1 = new Pen(Color.Black  , 1);
            Pen border2 = new Pen(Color.White , 1);
            Draw3DInnerBorder(InnerRect, g);//ﾄﾚｲｿｵﾄｿ・

            ClientRectangle.Width -= 1;
            ClientRectangle.Height -= 1;
            GraphicsPath outGp= GetRoundRectangle(ClientRectangle, 8);
            g.DrawPath(border1, outGp);


            ClientRectangle.Location =new Point (ClientRectangle.Top  + 1,ClientRectangle.Left  +1);
            ClientRectangle.Width -= 2;
            ClientRectangle.Height -= 2;
             outGp = GetRoundRectangle(ClientRectangle, 7);
            g.DrawPath(border2, outGp);

        }
        public static void Draw3DInnerBorder(Rectangle rect,Graphics g)
        {
            Pen p3= new Pen(Color.FromName ("ControlDark") );
            Pen p4 = new Pen(Color.FromName("ControlDarkDark"));
            Pen p1=new Pen(Color.White );
            Pen p2=new Pen(Color.FromName ("ControlLight"));

            //g.DrawLine(p1,rect.Location, new Point(rect.Right-1, rect.Top));
            //g.DrawLine(p2, new Point(rect.Left +1, rect.Top+1), new Point(rect.Right-2, rect.Top+1));

            g.DrawLine(p3, new Point(rect.Right - 1, rect.Top), new Point(rect.Right - 1, rect.Bottom - 1));
            g.DrawLine(p4, new Point(rect.Right - 2, rect.Top + 1), new Point(rect.Right - 2, rect.Bottom - 2));

            g.DrawLine(p3, new Point(rect.Left, rect.Bottom - 1), new Point(rect.Right - 1, rect.Bottom - 1));
            g.DrawLine(p4, new Point(rect.Left + 1, rect.Bottom - 2), new Point(rect.Right - 2, rect.Bottom - 2));

            //g.DrawLine(p1, new Point(rect.Left , rect.Top ), new Point(rect.Left  , rect.Bottom-1));
            //g.DrawLine(p2, new Point(rect.Left + 1, rect.Top + 1), new Point(rect.Left + 1, rect.Bottom - 2));

        }
    }
}