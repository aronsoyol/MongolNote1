using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing  ;
using System.Windows.Forms;
namespace MongolNote
{
    class VerticalText 
    {
        //TextBoxV .CSLogLine 
        public static List<CSLogLine> InitializeLogLineList(string strText, int MaxHeight, Graphics g, Font aFont, StringFormat mStringFormat)
        {
            //初始化单词链表,index<0初始化所有逻辑行，否则指定要初始化的逻辑行
            //int TotalLenth;
            List<CSLogLine> aLogLineList = new List<CSLogLine>();
            //strText .IndexOfAny
            CSLogLine aLogLine;

            char[] separetor ={ '\n' };

            string[] lines = strText.Split(separetor);
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(lines[i]))
                    {
                        aLogLine = new CSLogLine();
                    }
                    else
                    {
                        string tmp = lines[i];
                        char[] trimChar ={ ' ' };
                        if (tmp.Substring(tmp.Length - 1, 1) == "\r")
                        {
                            aLogLine = new CSLogLine(tmp.Substring(0, tmp.Length - 1), MaxHeight,g, aFont, mStringFormat);
                        }
                        else
                        {
                            aLogLine = new CSLogLine(tmp, MaxHeight,g, aFont, mStringFormat);
                        }


                    }
                    aLogLineList.Add(aLogLine);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return aLogLineList;
        }

        public  static List<RealLine> ArrangeAll3(int MaxHeight,List<CSLogLine> LogLineList,Font aFont)
        {

            List<RealLine>  lstLineInfo = new List<RealLine>();
            for (int i = 0; i < LogLineList.Count; i++)
            {
                LogLineList[i].ArrangeLine(MaxHeight, i, MaxHeight);

                for (int j = 0; j < LogLineList[i].LineList.Count; j++)
                {
                    lstLineInfo.Add(LogLineList[i].LineList[j]);
                }
            }
            return lstLineInfo;
        }

        public static  int DrawVerticalString(string text,Graphics g,Font font,int height,Point location,StringFormat aSF)//返回宽度
        {
            List<CSLogLine> LogLineList = InitializeLogLineList(text, height, g, font, aSF);
            List<RealLine> RealLineList = ArrangeAll3(height, LogLineList, font);
            //g.Clear(Color.Black);
            g.TranslateTransform (location.X ,location .Y );

            for (int i = 0; i < RealLineList.Count; i++)
            {
                DrawALine(RealLineList[i].Text, g, font, aSF, font.Height * i);
            }
            return RealLineList.Count * font.Height;
        }
        private static void DrawALine(string text, Graphics g, Font font,StringFormat aSF, int x)//画一行
        {
            if (String.IsNullOrEmpty(text)) return;

            g.TranslateTransform(x + font.Height, 0);
            g.RotateTransform(90);

            string tmpChr = "";

            SizeF sChar;
            Rectangle tmpR;
            SolidBrush aBrush;

            int pos = 0;
            int lenth = text.Length;
            for (int i = 0; i < lenth; i++)
            {
                //int tmpIndex = head + i;
                //int tmpL = text.Length;

                tmpChr = text.Substring(i, 1);


                sChar = g.MeasureString(tmpChr, font, 1000, aSF);
                tmpR = new Rectangle(pos, 0, Convert.ToInt32(sChar.Width), font.Height);


                aBrush = new SolidBrush(Color .Black );
                //}
                int CharCode = (int)tmpChr.ToCharArray()[0];
                if (CharCode > 0x3000 && CharCode < 0x9FFF)
                {
                    g.TranslateTransform(pos, font.Height);
                    g.RotateTransform(-90);

                    g.DrawString(tmpChr, font, aBrush, new Point(0, 0), aSF);

                    g.RotateTransform(90);
                    g.TranslateTransform(-pos, -font.Height);
                }
                else
                {
                g.DrawString(tmpChr, font, aBrush, new Point(pos, 0), aSF);
                }

                pos += Convert.ToInt32(sChar.Width);
            }
            //g.RotateTransform (-90);
            g.ResetTransform();

        }
    }
}
