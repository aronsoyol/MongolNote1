using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace MongolNote
{
    partial class TextBoxV
    {
        public class CSLogLine//逻辑列，不皝E恍蟹模琇ines的元素，可皝E舾筛觯导氏允镜牧?
        {
            public CSLogLine()
            {

                RealLine aRealLine = new RealLine( );
                LineList = new List<RealLine>();
                LineList.Add(aRealLine);

                WordLink = new CSWord();
                WordLink.isNewLine = true;

                CSWord tail = new CSWord();
                tail.isNewLine = true;
                WordLink.Next = tail;
                tail.Last = WordLink;

                LineList[0].HeadWord = WordLink;
                LineList[0].TailWord = tail;
            }
            public CSLogLine(string Line, int MaxHeight, Graphics g, Font MyFont, StringFormat aStringFormat)//Line不能皝E恍蟹丒
            {
                WordLink = new CSWord();
                WordLink.isNewLine = true;
                WordLink.Next = CreateWordLink(Line, MaxHeight, g, MyFont, aStringFormat);
                WordLink.Next.Last = WordLink;
            }
            //LineInformation LineList;

            public List<RealLine> LineList;
            public CSWord WordLink;//单词链眮E

            public int GetHead(int LineIndex)
            {
                int tmp = 0;
                for (int i = 0; i < LineIndex; i++)
                {
                    tmp += LineList[i].Length;
                }
                return tmp;
            }

            public  static void   InsertAfter(CSWord Position, CSWord aWord)
            {
                CSWord Next = Position.Next;
                Position.Next = aWord;
                aWord.Last = Position;
                aWord.Next = Next;
                if (Next != null)
                    Next.Last = aWord;    
            }
            public  static void Insert(CSWord Position, CSWord aWord)
            {
                CSWord Last = Position.Last;
                aWord.Next = Position;
                Position.Last = aWord;

                aWord.Last = Last;
                if(Last !=null)
                    Last.Next = aWord;
            }
            //{
            //    get
            //    {
            //        if (LineList.Count == 0)
            //            return new CSWord();
            //        return LineList[0].HeadWord;
            //    }
            //    set
            //    {
            //        mHeadWord = value;
            //        if (LineList!=null&& LineList .Count >0)
            //        LineList[0].HeadWord  = mHeadWord;
            //    }
            //}
            //private CSWord mHeadWord;

            //private void  Initialize()
            //{
            //    RealLine aRealLine = new RealLine(0, 0, 0);
            //    LineList = new List<RealLine>();
            //    LineList.Add(aRealLine);

            //    WordLink = new CSWord(0, 0);
            //    WordLink.isNewLine = true;
            //    //CSWord tail = new CSWord(0, 0);
            //    //tail.isNewLine = true;
            //    //WordLink.Next = tail;
            //    //tail.Next = WordLink;

            //}
            public override string ToString()
            {
                CSWord point = WordLink;
                string strLogLine = "";
                do
                {
                    strLogLine += point.Text;
                    point = point.Next;
                } while (point != null);
                return strLogLine;
            }
            public int Length
            {
                get
                {
                    CSWord point = WordLink;
                    int len = 0;
                    do
                    {
                        len += point.Length;
                        point = point.Next;
                    } while (point != null);
                    return len;
                }
            }
            static CSWord CreateWordLink(string strNokori, int MaxHeight, Graphics g, Font MyFont, StringFormat aStringFormat)
            {
                //int lenth ;
                CSWord aWordLink;
                int count = 0;
                aWordLink = GetAWord(strNokori, MaxHeight, g, MyFont, aStringFormat);
                //aWordLink.Text = strNokori.Substring(0, aWordLink.Lenth);
                //aWordLink.Index = 0;
                CSWord LastWord = aWordLink;
                strNokori = strNokori.Substring(aWordLink.Length, strNokori.Length - aWordLink.Length);

                while (!string.IsNullOrEmpty(strNokori))
                {
                    CSWord aWord = GetAWord(strNokori, MaxHeight, g, MyFont, aStringFormat);
                    //string Word = strNokori.Substring(0, aWord.Lenth );
                    //int height=MeasureWord (Word,g ,  MyFont,  aStringFormat); //测量
                    //aWord.Text = strNokori.Substring(0, aWord.Lenth);
                    //aWord.Index = count;
                    count++;
                    LastWord.Next = aWord;  //上一个的下个是这竵E
                    aWord.Last = LastWord;   //这个的上一个是上一竵E
                    LastWord = aWord;          //上一个是这竵E

                    //aWordLink.Add(aWord);
                    strNokori = strNokori.Substring(aWord.Length, strNokori.Length - aWord.Length);
                }
                return aWordLink;
            }
            static public CSWord GetAWord(string MyString, int MaxHeight, Graphics g, Font MyFont, StringFormat aStringFormat)//计量单词的尺磥E
            {
                //返回一个单词中的字母个数,臁1
                //一个单词中不能有换行，空格，眮E惴?
                //不同觼E缘牡ゴ剩谂虐媸辈豢煞指恼澹ㄈ绻ゴ矢叨瘸懈撸虺猓?
                //应该皝E舾浜蟮谋丒惴?
                //MaxHeight单词的畜尺寸，如果大于该尺寸，则拆分
                int Lenth = MyString.Length;
                char[] CharArray = MyString.ToCharArray();
                string text = "";
                //CSWord aWord = new CSWord();
                int WordWidth=0;
                int aCharWidth=0;
                bool isNewLine = false;
                //int CharCount = 0;
                int i;
                for (i = 0; i < Lenth; i++)
                {
                    int CharCode = (int)CharArray[i];

                    string aChar = MyString.Substring(i, 1);
                    SizeF sChar = g.MeasureString(aChar, MyFont, 1000, aStringFormat);//计算尺磥E1000没有实际意襾E
                    aCharWidth = Convert.ToInt32(sChar.Width);

                    if (WordWidth + aCharWidth > MaxHeight)
                    {
                        break;
                    }
                    if (CharCode > 0x3000 && CharCode < 0x9FFF)//中日韩字符；
                    {
                        if (i == 0)
                        {
                            CSWord aWord = new CSWord(CharArray[i].ToString(), aCharWidth, true);
                            aWord.Chinese = true;
                            return aWord;
                        }
                        break;
                    }
                    if (CharCode == 32) //空格，
                    {
                        if (i == 0)
                        {
                            CSWord aWord = new CSWord(" ", aCharWidth);
                            //aWord.Text += CharArray[i].ToString();
                            aWord.isSpace = true;
                            return aWord;
                        }
                        break;
                    }else  if (CharArray[i] == '\r')
                    {
                        isNewLine = true ;
                    }
                    else if (CharArray[i] == '\n' && isNewLine)
                    {
                        return null;//遇到换行穪E
                    }
                    //else//当前字符没有被过滤
                    //{
                    //aWord.Height += width;
                    //aWord .Lenth ++;
                    text += CharArray[i].ToString();
                    WordWidth+=aCharWidth;
                    //}

                }

                return new CSWord (text ,WordWidth);
            }
            static public int MeasureString(string Word, Graphics g, Font MyFont, StringFormat aStringFormat)//计量单词的尺磥E
            {
                //超过MaxHeight返回索引


                //Graphics g = this.CreateGraphics();
                int len = 0;
                int height = 0;
                try
                {
                    //两种计算方法，可能会有误瞾E

                    ////第一种计算方法
                    //int i;
                    for (len = 0; len < Word.Length; len++)
                    {
                        string aChar = Word.Substring(len, 1);
                        SizeF sChar = g.MeasureString(aChar, MyFont, 1000, aStringFormat);//计算尺磥E1000没有实际意襾E
                        int width = Convert.ToInt32(sChar.Width);


                        height += width;

                    }

                    ////第二种计算方法
                    //SizeF sChar = g.MeasureString(Word, Font, ClientRectangle.Height, mStringFormat);//计算尺磥E
                    //height = Convert.ToInt32(sChar.Width);


                }

                catch (Exception ex)
                {
                    MessageBox.Show("测量单词尺寸时出代xn" + Word + "\n" + ex.Message, "Error！"
                                                , MessageBoxButtons.OK
                                                , MessageBoxIcon.Error);
                }
                //return new WordSize
                return height;
            }
            public  int ArrangeSomeLine(int index, int MaxHeight)//返回受影蟻E男惺?
            {
                int i=0;
                CSWord point;
                while (LineList[index].Height > MaxHeight)
                {
                    point = LineList[index].TailWord.Last;
                    point.Last.Next = point.Next;
                    point.Next.Last = point.Last;


                    point.Next=LineList[index].HeadWord.Next;
                    point.Last = LineList[index].HeadWord;
                    LineList[index].HeadWord.Next = point;
                    point.Next.Last = point;

                    i++;
                }
                return i;
            }
            public static RealLine ArrangeALine(CSWord aWordLink,int LogLineIndex,int index)
            {
                //aWordLink被看作有一个空行头的行
                //WordPoint.isNewLine = true;
                //WordPoint.Next  = aWordLink;
                //aWordLink.Last = WordPoint;
                CSWord WordPoint = aWordLink;
                RealLine aRealLine = new RealLine(LogLineIndex,index);//本行的索引，在logLine的ReaLineList中的
                if (IsNullOrEmpyty(aWordLink))
                {
                    aRealLine.HeadWord = aWordLink;
                    aRealLine.TailWord = new CSWord(true);
                    aRealLine.HeadWord.Next = aRealLine.TailWord;
                    aRealLine.TailWord.Last = aRealLine.HeadWord;
                    return aRealLine;
                }
                aRealLine.HeadWord = WordPoint;

                WordPoint = WordPoint.Next;
                //aRealLine.Lenth += WordPoint.Lenth;
                //aRealLine.Height += WordPoint.Height;
                //aRealLine.WordCount++;
                //WordPoint = WordPoint.Last;
                int tmpHeight = 0;
                bool shortLine = false;
                string tmp = "";
                //int SpaceCount = 0;
                //CSWord space =new CSWord (false );
                //CSWord spcacePoint = null;
                while (WordPoint != null && tmpHeight + WordPoint.Height  <= RealLine .StandardHeight )
                {
                    tmp += WordPoint.Text;
                    tmpHeight += WordPoint.Height ;//GetHeight(1)空格原本的高度，非1空格调整后的高度
                    if (WordPoint.Next == null)
                    {
                        shortLine = true;
                        break;
                    }

                    WordPoint = WordPoint.Next;
                }
                CSWord TailWord = new CSWord();
                TailWord.isNewLine = true;

                if (shortLine||(WordPoint != null && WordPoint.Height > RealLine.StandardHeight && tmpHeight == 0))
                {
                    //aRealLine.Lenth += WordPoint.Lenth;
                    //aRealLine.Height += WordPoint.Height;
                    //aRealLine.mWordCount++;
                    if (WordPoint.Next != null)
                    {
                        TailWord.Next = WordPoint.Next;
                        TailWord.Next.Last = TailWord;
                    }

                    WordPoint.Next = TailWord;
                    TailWord.Last = WordPoint;
                    //aRealLine.TailWord = WordPoint;

                }
                else
                
                {
                    WordPoint.Last.Next = TailWord;

                    TailWord.Last = WordPoint.Last;
                    TailWord.Next = WordPoint;
                    WordPoint.Last = TailWord;
                }


                aRealLine.TailWord = TailWord;
                //if (aRealLine.Height != tmpHeight)
                //{
                //    MessageBox.Show("出代菟！ArrangeALine");
                //}

                return aRealLine;
            }
            static public bool IsNullOrEmpyty(CSWord WordList)
            {
                CSWord point = WordList;
                while (point != null)
                {
                    if (point.Length > 0) return false;
                    point = point.Next;
                }
                return true;
            }
            public void ArrangeLine(int MaxHeight,int LogLineIndex)
            {

                //LineList
                //List<RealLine> aRealLineList=new List<RealLine> ();
                CSWord WordPoint;
                if (LineList != null)
                {
                    for (int i = 0; i < LineList.Count; i++)//删除所有TailWord
                    {
                        CSWord point = LineList[i].TailWord.Last;
                        point.Next = LineList[i].TailWord.Next;
                        if (LineList[i].TailWord.Next != null)
                            point.Next.Last = point;
                        //LineList[i].TailWord
                    }
                }

                //LineList[LineList.Count-1]
                LineList = new List<RealLine>();
                //if (WordLink.Next == null)
                //{
                //    WordPoint = new CSWord();
                //    WordLink.Next = WordPoint;
                //    WordPoint.Last = WordLink;
                //}

                int iii = 0;//行在逻辑行中的索引
                WordPoint = WordLink;
                do
                {
                    RealLine aRealLine = ArrangeALine(WordPoint,LogLineIndex, iii++);
                    LineList.Add(aRealLine);
                    //if (aRealLine.TailWord.Next == null) WordPoint = null;
                    WordPoint = aRealLine.TailWord;
                } while (WordPoint.Next != null);

            }

            //public void ArrangeLine(int MaxHeight)
            //{
            //    //int LineHeight=0;
            //    LineList = new List<RealLine>();
            //    RealLine aRealLine = new RealLine(0, 0, 0);
            //    LineList.Add(aRealLine);
            //    int Head = 0;
            //    aRealLine.Head = 0;
            //    aRealLine.HeadWord = WordList[0];
            //    for(int i=0;i<WordList .Count ;i++)
            //    {
            //        if (aRealLine.Height  + WordList[i].Height > MaxHeight)
            //        {
            //            aRealLine = new RealLine(0, 0, 0);
            //            LineList.Add(aRealLine);
            //            Head += LineList[LineList.Count  - 1].Lenth;
            //            aRealLine.Head = Head;
            //            aRealLine.HeadWord = WordList[i];
            //            int iii=LineList.Count;
            //        }

            //        aRealLine.Height += WordList[i].Height;//行的高度
            //        aRealLine.Lenth += WordList[i].Lenth;   //行的字符数量
            //        aRealLine.WordCount++;                      //行的单词数量
            //    }
            //}
        }
        public class RealLine
        {

            public RealLine()
            {
                //mHeight = 0;
                //Head = 0;
                //Lenth = 0;
            }
            public RealLine(int iLogLine,int index)
            {
                //mHeight = height;
                //Head = head;
                mIndex = index;
                HeadWord = new CSWord(true);
                TailWord = new CSWord(true);
                HeadWord.Next = TailWord;
                TailWord.Last = HeadWord;
                LogLine = iLogLine;
                //Lenth = lenth;

            }
            public int SpaceWidth = 0;
            //private int mHeight=0;
            public int Length
            {
                get
                {
                    int mLength = 0;
                    CSWord point = this.HeadWord.Next;
                    while (point != TailWord&&point !=null)
                    {
                        mLength += point.Length ;
                        point = point.Next;
                    }
                    return mLength;

                }
            }

            

            //{
            //    //get
            //    //{
            //    //    while()
            //    //}
            //}
            public int LogLine;
            public int Height    //行的高度
            {
                get
                {
                    int h=0;
                    string tmp = "", tmp2 = ""; ;
                    CSWord point = this.HeadWord.Next ;
                    while (point != TailWord&&point!=null)
                    {
                        tmp = this.Text;
                        tmp2 += point.Text;
                        h += point.Height;
                        point = point.Next;
                        
                    }
                    return h;
                }
                //set
                //{
                //    mHeight = value;
                //    MaxHeight = (value > MaxHeight) ? value : MaxHeight;
                //}
            }
            private int mIndex=-1;
            public int Index//行在LogLine中的索引
            {
                get { return mIndex; }
            }
            public static int MaxHeight = 0;//所有行高的实际畜值
            public static int StandardHeight = 0;//行的眮E几叨龋豢筛吖飧龈叨?
            //public static int Count;
            private  int mWordCount = 0;
            public int WordCount
            {
                get
                {
                    return mWordCount;
                }
            }
            public CSWord HeadWord;
            public CSWord TailWord;
            //public void InsertAfter(CSWord Position, CSWord aWord)
            //{
            //    CSLogLine.InsertAfter(Position, aWord);
            //    mWordCount++;

            //}
            //public void Insert(CSWord Position, CSWord aWord)
            //{
            //    CSLogLine.Insert(Position, aWord);
            //    mWordCount++;

            //}
            public CSWord Push(CSWord List)//返回被挤出去的CSWord
            {
                
                //bool ret = false;
                
                CSWord  tmpHead,tmpTail;//传过来的、要返回的单词串的首尾
                CSWord point=null;

                //int ListCount = 0;//传过来的List的Count
                if (List != null)
                {
                    tmpHead = List;//传过来的单词串的首
                    point = List;
                    //ListCount++;
                    while (point.Next != null)
                    {
                        point = point.Next;
                        //mWordCount ++;//单词+1
                    }
                    tmpTail = point;//传过来的单词串的尾

                    CSWord  First = HeadWord.Next;//原来的第一个单词

                    HeadWord.Next = tmpHead;
                    tmpHead.Last = HeadWord;

                    tmpTail.Next = First;
                    First.Last = tmpTail;
                }


                bool ret = false;//是否影蟻E乱恍?
                point = TailWord.Last  ;
                int tmpHeight = this.Height;
                while (tmpHeight > StandardHeight)
                {
                    tmpHeight -= point.Height;
                    point = point.Last;//point煮指向新行的煮一个单词
                    ret = true;
                    //mWordCount--;//单词数-1
                }
                if (ret)
                {
                    tmpHead = point.Next;
                    tmpHead.Last = null;
                    TailWord.Last.Next   = null;

                    point.Next = TailWord;
                    TailWord.Last = point;

                    return tmpHead;
                }
                return null;
            }
            public string Text
            {
                get
                {
                    CSWord point = HeadWord;
                    string str = "";
                    do
                    {
                        str += point.Text;
                        point = point.Next;
                    } while (point != TailWord && point != null);
                    return str;
                }
            }
            public override string ToString()
            {
                CSWord point = HeadWord;
                string str = "";
                do
                {
                    str += point.Text;
                    point = point.Next;
                } while (point != TailWord && point != null);
                return "{Text:"+str +"},{Length:" +str.Length .ToString ()+"}";
            }
            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

        }

        public class CSWord
        {

            public CSWord()
            {

            }
            public CSWord(bool newline)
            {
                isNewLine = newline;

            }
            public CSWord(string text,int height)
            {
                mText = text;
                mHeight = height;
            }
            public CSWord(string text, int height, bool isHorizontal)
            {
                mText = text;
                mHeight = height;
                mHorizontal = true;
            }

            private bool mHorizontal = false;
            public bool Horizontal
            {
                get
                { 
                    return mHorizontal;
                }
            }
            private string mmText = "";
            private string mText
            {
                set
                {
                    mmText = value;
                    mLength = value.Length;

                }
                get { return mmText; }
            }
            public string Text
            {
                get
                {
                    return mText;
                }
                //set
                //{
                //    mText =value ;
                //    //mHeight=CSLogLine.MeasureString (
                //    //this.
                //}
            }
            //private static  Measure()
            //{
            //    TextRenderer.MeasureText(
            //}
            public CSWord Next = null;
            public CSWord Last = null;
            public CSWord LastSpace = null;
            public int SpaceWidth=0;
            //private int mLenth;
            private int mHeight=0;
            public bool Chinese = false;
            private bool IsNewLine=false;
            //public int Index=0;
            public bool isSpace = false;
            public int Length { get { return mLength; } }           //set { mLenth = value; }
            private int mLength = 0;
            public int Height
            {
                get
                {
                        return mHeight;
                }

                //set { mHeight = value; }
            }
            public bool isNewLine
            {
                get { return IsNewLine; }
                set { IsNewLine = value; }
            }
            public override string ToString()
            {
                if (string.IsNullOrEmpty(Text) && isNewLine)
                {
                    string tmpString = "NewLine";
                    return tmpString;
                }
                else
                    return Text;
            }
            public int GetHeight(int flag)
            {
                if (flag == 1)
                    return mHeight;
                else
                    return SpaceWidth;
            }
            public override int GetHashCode()
            {
                //return base.GetHashCode();
                string tmpString = "";
                CSWord point = this;
                while (point != null)
                {
                    tmpString += point.Text + "->";
                    point = point.Next;
                }
                return tmpString.GetHashCode();
            }
            public Bitmap DrawWord(Font aFont, StringFormat aStringFormat, SolidBrush WordBrush, SolidBrush ShadowBrush)
            {
                Bitmap bmp = new Bitmap(aFont.Height, this.Height);
                int tmpIndex = 0;
                int pos = 0;
                string tmpChar;
                SizeF sChar;
                //Rectangle tmpR;
                //SolidBrush shadaw = mShadowBrush2;
                //SolidBrush aBrush = new SolidBrush(ForeColor);
                Graphics g = Graphics.FromImage(bmp);
                g.TranslateTransform(aFont.Height, 0);
                g.RotateTransform(90);
                for (int i = 0; i < Text.Length; i++)
                {

                    tmpChar = Text.Substring(tmpIndex, 1);
                    //if (tmpChr == "\r") tmpChr = "\n";
                    sChar = g.MeasureString(tmpChar, aFont, 1000, aStringFormat);

                    if (ShadowBrush != null)
                        g.DrawString(tmpChar, aFont, ShadowBrush, new Point(pos + 1, 2), aStringFormat);
                    g.DrawString(tmpChar, aFont, WordBrush, new Point(pos, 0), aStringFormat);


                    //g.DrawString(tmpChr, Font, Brushes.Black, new Point(pos, 0), mStringFormat);
                    pos += Convert.ToInt32(sChar.Width);
                    tmpIndex++;
                }
                return bmp;
            }

        }

    
        public class CSTextList
        {

        }
        public class CSChar//字符纴E
        {
            char character;
            EnumLanguge Languge ;
            
        }

        public enum EnumLanguge
        {
            ChineseJapanKorea,
            Mongila,
        }
        public interface IWordOprate
        {
            int Length { get;set;}
            int Height { get;}
            int Index { get;}
        }
    }
}