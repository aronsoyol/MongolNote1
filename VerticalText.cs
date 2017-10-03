using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace MongolNote
{


    public class CRealLine
    {

        public CRealLine()
        {
            //mHeight = 0;
            //Head = 0;
            //Lenth = 0;
        }
        public CRealLine(int iLogLine,int index)
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

    public class CWord
    {

        public CWord(bool newline)
        {
            isNewLine = newline;

        }
        private bool  _Horizontal = false;
        public bool Horizontal
        {
            get
            {
                return _Horizontal;
            }
        }

        private string _Text;

        public string Text
        {
            get
            {
                return _Text;
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
        public CWord Next = null;
        public CWord Last = null;
        public CWord LastSpace = null;
        public int SpaceWidth = 0;
        //private int mLenth;
        private int mHeight = 0;
        public bool Chinese = false;
        private bool IsNewLine = false;
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
            CWord point = this;
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

    public class TextListNode : IDisposable
    {

        public TextListNode Next;
        public char Text;
        public TextListNode Last;

        public void Connect(ref TextListNode next)
        {
            this.Next = next;
            next.Last = this;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            Next = null;
            Last = null;
        }

        #endregion
    }
    public class CStringList : IDisposable
    {
        #region  私有字段

        protected TextListNode _Head=null;
        protected TextListNode _Tail = null;
        protected int _Length = 0;
        #endregion

        /// <summary>
        /// 构詠E?
        /// </summary>
        /// <param name="text"></param>
        public CStringList(string text)
        {
            _Tail=CreateList(ref _Head, text);
        }

        #region 属性
        public string Text
        {
            get
            {
                string t = "";
                TextListNode node=this._Head ;
                while (node != null)
                {
                    t += node.Text.ToString();
                    node = node.Next;
                }
                return t;
            }
            set
            {
                this.Dispose();
                _Tail = CreateList(ref _Head, value);
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 制詠E幢祷匚步诘丒
        /// </summary>
        /// <param name="head">指定一个头节祦E/param>
        /// <param name="text">制詠E幢奈谋?/param>
        /// <returns>尾节祦E/returns>
        private TextListNode CreateList(ref TextListNode head, string text)//制詠E幢丒
        {
            TextListNode last, node;
            if (!string.IsNullOrEmpty(text))
            {
                
                char[] CharList = text.ToCharArray();

                node = CreateOneNode(CharList[0]);
                head = node;//链柄涯头节祦E
                last = node;
                int len = 0;
                for (len = 1; len < text.Length; len++)
                {
                    node = CreateOneNode(CharList[len]);
                    last.Next = node;
                    node.Last = last;
                    last = node;
                }
                return node;//尾节祦E
            }
            return null;//尾节祦E
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 把一个字转成节祦E
        /// </summary>
        /// <param name="aChar"></param>
        /// <returns>新的节祦E/returns>
        public TextListNode CreateOneNode(char aChar)//把一个字转成节祦E
        {

            TextListNode Node=new TextListNode ();
            Node.Text = aChar;
            Node .Next =null;
            Node.Last =null;
            return Node;
        }
        /// <summary>
        /// 把一个字转成节祦E，string类字符串的第一个字符将被转换
        /// </summary>
        /// <param name="aChar">string类的字符串的第一个字穪E/param>
        /// <returns>新的节祦E/returns>
        public TextListNode CreateOneNode(string aChar)//把一个字转成节祦E
        {

            TextListNode Node = new TextListNode();
            Node.Text = aChar.ToCharArray ()[0];
            Node.Next = null;
            Node.Last = null;
            return Node;
        }
        #endregion
        /// <summary>
        /// 在节点position之前插葋E桓鼋诘丒这是瓠子的Insert,其他Insert都调用藖E可修改Lenght
        /// </summary>
        /// <param name="node">要插葋E慕诘丒/param>
        /// <param name="position">再此节点之前插葋E/param>
        public void Insert(TextListNode node, TextListNode position)
        {
            if (node == null) throw new Exception("不能插葋E盏膎ode");
            if (position == null)
            {
                _Head.Next = node;
                _Tail = node;
            }
            else
            {
                TextListNode last;
                last = position.Last;

                if (last != null)
                {
                    last.Next = node;
                }
                else//上一个为空需竵E铝幢?
                {
                    _Head = node;
                }
                node.Last = last;
                node.Next = position;
                position.Last = node;
            }
            _Length++;

        }
        /// <summary>
        /// 在索引位置index之前插葋E桓鼋诘丒
        /// </summary>
        /// <param name="node">要插葋E慕诘丒/param>
        /// <param name="index">再此节点之前插葋E/param>
        public void Insert(TextListNode node,int index)
        {
            Insert(node, this[index]);
        }
        /// <summary>
        /// 在节点position之前插葋E喔鼋诘丒
        /// 这是插葋E喔鲈氐腎nsert的基础函数，其他插葋E喔鲈氐暮嫉饔盟丒
        /// </summary>
        /// <param name="node">要插葋E慕诘丒/param>
        /// <param name="length" >要插葋E某ざ?/param>
        /// <param name="position">再此节点之前插葋E/param>
        /// <returns >插葋E氖?/returns>
        public int Insert(TextListNode node, int length,TextListNode position)
        {
            if (node == null || position == null) return 0;

            TextListNode last,next;

            last = position.Last;
            next = position;

            int i = 1;
            if (last != null)
            {
                last.Next = node;
                node.Last = last;
            }
            else//上一个为空需竵E铝幢?
            {
                _Head = node;
            }
            if (length == 0)
            {
                while (node.Next != null)
                {
                    node = node.Next; i++;
                }
            }
            else
            {
                for ( i = 0; i < length; i++)
                {
                    node = node.Next;
                    if (node.Next == null)
                    {
                        i++;
                        break;
                    }
                }
                
            }
            node.Next = position;
            position.Last = node;
            _Length += i;
            return i;
        }
        /// <summary>
        /// 将一个Unicode字符插葋Endex之前，并返回这个字符的TextListNode实例 
        /// </summary>
        /// <param name="achar">一个Unicode字穪E/param>
        /// <param name="index">在此位置之前插葋E/param>
        /// <returns >返回当前插葋E淖址囊?/returns>
        public TextListNode Insert(char achar, int index)
        {
            return Insert(achar, this[index]);
        }
        /// <summary>
        /// 将一个Unicode字符插葋Eosition之前，并返回这个字符的TextListNode实例
        /// </summary>
        /// <param name="achar">一个Unicode字穪E/param>
        /// <param name="position">在此位置之前插葋E/param>
        /// <returns>返回achar的TextListNode实例</returns>
        public TextListNode Insert(char achar, TextListNode position)
        {
            TextListNode node= CreateOneNode(achar);
            Insert(node, position);
            return node;
        }
        /// <summary>
        ///将VeticalStringList对象的副本插葋E谖恢胕ndex之前
        /// </summary>
        /// <param name="StringtList">一竵EVeticalStringList类的实例</param>
        /// <param name="index">在此位置之前插葋E/param>
        /// <returns>插葋E淖址某ざ?/returns>
        public int Insert(CStringList StringtList, int index) 
        {
            string tmp = StringtList.Text;
            if (string.IsNullOrEmpty(tmp)) return 0;
            int i=Insert(tmp, index);
            return i;
        }
        /// <summary>
        /// 将text插葋Endex之前
        /// </summary>
        /// <param name="text">要插葋E奈谋?/param>
        /// <param name="index">再此位置之前插葋E/param>
        /// <returns>被加葋E淖址ざ?/returns>
        public int Insert(string text, int index)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            TextListNode head=null;
            TextListNode tail = CreateList(ref head, text);
            TextListNode posision = null;
            try
            {
                posision = this[index];
            }
            catch (Exception ex)
            {
                return 0;
            }

            int len = text.Length;
            int len2 = Insert(head, 0, posision);
            //if (len != len2) throw new Exception("插葋E淖址坑形骪n");
            return len2;

        }
        /// <summary>
        /// 在链柄涯煮加葋E碌慕诘丒
        /// </summary>
        /// <param name="node">被加葋E男陆诘丒/param>
        /// <returns >node为空返回false</returns>
        public bool Add(TextListNode node)//座础Add其他Add都调用藖E尚薷腳Length
        {
            if (node == null) return false;
            if (_Tail == null)
            {
                _Head = node;
                _Tail = node;
            }
            else
            {
                this._Tail.Next = node;
                node.Last = _Tail;
                _Tail = node;
                
            }
            _Length++;
            return true;
        }
        /// <summary>
        /// 在链柄涯煮加葋E桓鯱nicode字穪E
        /// </summary>
        /// <param name="achar">Unicode字穪E/param>
        /// <returns>字符为空时返回false</returns>
        public bool Add(char achar)
        {
           TextListNode node= CreateOneNode(achar);
           return Add(node);
        }
        /// <summary>
        /// 在链柄涯煮加葋E址?
        /// </summary>
        /// <param name="text">被加葋E淖址?/param>
        /// <returns>被加葋E淖址ざ?/returns>
        public int Add(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            char[] array = text.ToCharArray();

            int len = text.Length;
            int i = 0;

            while (i<len)
            {
                Add(array[i]);
                i++;
            }
            return len;
        }
        /// <summary>
        ///在链柄涯煮加葋E桓丒VeticalStringList的副本
        /// </summary>
        /// <param name="StringList">一竵EVeticalStringList类的实例</param>
        /// <returns>被加葋E淖址ざ?/returns>
        public int Add(CStringList StringList)
        {
            int i= Add(StringList.Text);
            return i;
        }
        public TextListNode this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new Exception("TextListNode索引器代陙E下眮E荒芪褐怠ndex="
                    + index.ToString());
                    //return null;
                }
                int i=0;
                TextListNode node = _Head;
                if (node == null)
                    return _Head;
                while (i < index)
                {
                    if (node == null)
                    {
                        throw new Exception("TextListNode索引器代陙E可能是下眮E显浇纭="
                            + i.ToString() + ",index=" + index.ToString());
                    }

                    node = node.Next;
                    i++;
                }
                return node;
            }
            set
            {
                int i = 0;

                TextListNode node = _Head;
                if (node == null)
                    throw new Exception("链柄戟空");
                while (i < index)
                {
                    if (node == null)
                    {
                        throw new Exception("TextListNode索引器代陙E可能是下眮E浇纭="
                            + i.ToString() + ",index=" + index.ToString());
                    }
                    node = node.Next;
                    i++;
                }
                node.Text = value.Text;
            }
        }
        /// <summary>
        /// 摘除某个Node，座本的摘除函数，其他remove均调用藖E
        /// </summary>
        /// <param name="node">被摘除的节祦E/param>
        /// <returns>成功true，失败false</returns>
        private bool RemoveBase(TextListNode node)
        {
            if (node == null) return false;
            TextListNode last, next;
            next = node.Next;
            last = node.Last;

            _Length--;
            if (last== _Head )//上一个不为空
            {
                _Head.Connect (ref next);
            }

            if (next ==_Tail )//下一个为空
            {
                _Tail = last;
            }
            node.Dispose();
            return true;
        }
        public bool Remove(int index)
        {
            try
            {
                return RemoveBase(this[index]);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int Remove(TextListNode start, int length)
        {
            TextListNode last, next, delete;
            last = start.Last;

            delete = start;
            next = delete;
            int i,count=0;
            for (i = 0; i < length; i++)
            {
                next = delete.Next;
                delete.Dispose();
                count++;
                if (next == _Tail)
                {
                    last.Connect(ref _Tail);
                    break;
                }
                delete = next;
            }

            return count;//实际被删除的元素数
        }
        public int Remove(int start, int length)
        {
            TextListNode node = this[start];
            return Remove(node, length);
        }
        /// <summary>
        /// 替换,start2到end2的链柄楷替换start到end的链柄楷返回被替换的节点个数,
        /// 竵E翷ength
        /// </summary>
        /// <param name="start">被替换的开始位置</param>
        /// <param name="end">被替换的结束位置</param>
        /// <param name="start2">替换的开始位置</param>
        /// <param name="end2">替换的结束位置</param>
        /// <returns>替换的节点个数</returns>
        private  int ReplaceBase(TextListNode start,TextListNode end,TextListNode start2,TextListNode end2)
        {
            if (start == null
                || end == null
                || start2 == null
                || end2 == null) throw new Exception("替换时代牦，不可有空对蟻E");

            TextListNode last = start.Last;
            TextListNode next = end.Next;

            last.Connect(ref start2);
            end2.Connect(ref next);
            _Length =+Count (start2,end2 );
            int count= Clear (start, end);
            _Length -= count;
            return count;
        }
        public int Replace(TextListNode start1, int Length1, TextListNode start2, int Length2)
        {
            return (ReplaceBase(start1, FindNode(start1, Length1), start2, FindNode(start2, Length2)));
        }
        /// <summary>
        /// 从start到end为止删除,且不链接被删除部分。返回删除的节点个数
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结蕘E/param>
        /// <returns>删除的节点个数</returns>
        private int Clear(TextListNode start, TextListNode end)
        {
            int count = 0;
            TextListNode delete, node, next, last;
            last = start.Last;
            next = end.Next;
            last.Connect(ref next);
            delete = start;
            while (delete != null)
            {
                node = delete.Next;
                delete.Dispose();
                delete = node;
                count++;
            }
            return count;
        }
        /// <summary>
        /// 计数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        int Count(TextListNode start, TextListNode end)
        {
            TextListNode node = start;
            int count=1;
            while (node != end && node != null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }
        /// <summary>
        /// 返回节点node的前(簛E第length个节祦E
        /// </summary>
        /// <param name="node"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public TextListNode FindNode(TextListNode node, int length)
        {
            if (node == null) return null;
            if (length == 0) return node;
            else if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    node = node.Next;
                    if (node == null) return null;
                }
            }
            else
            {
                for (int i = 0; i > length; i--)
                {
                    node = node.Last ;
                    if (node == null) return null;
                }
            }
            return node;
        }
        #region IDisposable 成员
        public void Dispose()
        {
            TextListNode node = _Head;
            TextListNode next ;
            while (node != null)
            {
                next = node.Next;
                node.Dispose();
                node = next;
            }
        }
        #endregion
    }

}