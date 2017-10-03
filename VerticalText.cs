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
        public int Height    //�еĸ߶�
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
        public int Index//����LogLine�е�����
        {
            get { return mIndex; }
        }
        public static int MaxHeight = 0;//�����иߵ�ʵ������ֵ
        public static int StandardHeight = 0;//�еı�E��߶ȣ����ɸ߹�����߶?
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
        public CSWord Push(CSWord List)//���ر�����ȥ��CSWord
        {
            
            //bool ret = false;
            
            CSWord  tmpHead,tmpTail;//�������ġ�Ҫ���صĵ��ʴ�����β
            CSWord point=null;

            //int ListCount = 0;//��������List��Count
            if (List != null)
            {
                tmpHead = List;//�������ĵ��ʴ�����
                point = List;
                //ListCount++;
                while (point.Next != null)
                {
                    point = point.Next;
                    //mWordCount ++;//����+1
                }
                tmpTail = point;//�������ĵ��ʴ���β

                CSWord  First = HeadWord.Next;//ԭ���ĵ�һ������

                HeadWord.Next = tmpHead;
                tmpHead.Last = HeadWord;

                tmpTail.Next = First;
                First.Last = tmpTail;
            }


            bool ret = false;//�Ƿ�ӰρE�һ�?
            point = TailWord.Last  ;
            int tmpHeight = this.Height;
            while (tmpHeight > StandardHeight)
            {
                tmpHeight -= point.Height;
                point = point.Last;//point����ָ�����е�����һ������
                ret = true;
                //mWordCount--;//������-1
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

    public class CSChar//�ַ���E
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

        #region IDisposable ��Ա

        public void Dispose()
        {
            Next = null;
            Last = null;
        }

        #endregion
    }
    public class CStringList : IDisposable
    {
        #region  ˽���ֶ�

        protected TextListNode _Head=null;
        protected TextListNode _Tail = null;
        protected int _Length = 0;
        #endregion

        /// <summary>
        /// ��ԁE��?
        /// </summary>
        /// <param name="text"></param>
        public CStringList(string text)
        {
            _Tail=CreateList(ref _Head, text);
        }

        #region ����
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

        #region ˽�к���
        /// <summary>
        /// ��ԁE���������β�ڵ�E
        /// </summary>
        /// <param name="head">ָ��һ��ͷ�ڵ�E/param>
        /// <param name="text">��ԁE������ı?/param>
        /// <returns>β�ڵ�E/returns>
        private TextListNode CreateList(ref TextListNode head, string text)//��ԁE���E
        {
            TextListNode last, node;
            if (!string.IsNullOrEmpty(text))
            {
                
                char[] CharList = text.ToCharArray();

                node = CreateOneNode(CharList[0]);
                head = node;//������ͷ�ڵ�E
                last = node;
                int len = 0;
                for (len = 1; len < text.Length; len++)
                {
                    node = CreateOneNode(CharList[len]);
                    last.Next = node;
                    node.Last = last;
                    last = node;
                }
                return node;//β�ڵ�E
            }
            return null;//β�ڵ�E
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��һ����ת�ɽڵ�E
        /// </summary>
        /// <param name="aChar"></param>
        /// <returns>�µĽڵ�E/returns>
        public TextListNode CreateOneNode(char aChar)//��һ����ת�ɽڵ�E
        {

            TextListNode Node=new TextListNode ();
            Node.Text = aChar;
            Node .Next =null;
            Node.Last =null;
            return Node;
        }
        /// <summary>
        /// ��һ����ת�ɽڵ�E��string���ַ����ĵ�һ���ַ�����ת��
        /// </summary>
        /// <param name="aChar">string����ַ����ĵ�һ���ַ�E/param>
        /// <returns>�µĽڵ�E/returns>
        public TextListNode CreateOneNode(string aChar)//��һ����ת�ɽڵ�E
        {

            TextListNode Node = new TextListNode();
            Node.Text = aChar.ToCharArray ()[0];
            Node.Next = null;
            Node.Last = null;
            return Node;
        }
        #endregion
        /// <summary>
        /// �ڽڵ�position֮ǰ��ȁE����ڵ�E��������ӵ�Insert,����Insert������ˁE���޸�Lenght
        /// </summary>
        /// <param name="node">Ҫ��ȁEĽڵ�E/param>
        /// <param name="position">�ٴ˽ڵ�֮ǰ��ȁE/param>
        public void Insert(TextListNode node, TextListNode position)
        {
            if (node == null) throw new Exception("���ܲ�ȁEյ�node");
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
                else//��һ��Ϊ���踁E������?
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
        /// ������λ��index֮ǰ��ȁE����ڵ�E
        /// </summary>
        /// <param name="node">Ҫ��ȁEĽڵ�E/param>
        /// <param name="index">�ٴ˽ڵ�֮ǰ��ȁE/param>
        public void Insert(TextListNode node,int index)
        {
            Insert(node, this[index]);
        }
        /// <summary>
        /// �ڽڵ�position֮ǰ��ȁE���ڵ�E
        /// ���ǲ�ȁE��Ԫ�ص�Insert�Ļ���������������ȁE��Ԫ�صĺ���������ˁE
        /// </summary>
        /// <param name="node">Ҫ��ȁEĽڵ�E/param>
        /// <param name="length" >Ҫ��ȁEĳ��?/param>
        /// <param name="position">�ٴ˽ڵ�֮ǰ��ȁE/param>
        /// <returns >��ȁE����?/returns>
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
            else//��һ��Ϊ���踁E������?
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
        /// ��һ��Unicode�ַ���ȁEndex֮ǰ������������ַ���TextListNodeʵ�� 
        /// </summary>
        /// <param name="achar">һ��Unicode�ַ�E/param>
        /// <param name="index">�ڴ�λ��֮ǰ��ȁE/param>
        /// <returns >���ص�ǰ��ȁE��ַ������?/returns>
        public TextListNode Insert(char achar, int index)
        {
            return Insert(achar, this[index]);
        }
        /// <summary>
        /// ��һ��Unicode�ַ���ȁEosition֮ǰ������������ַ���TextListNodeʵ��
        /// </summary>
        /// <param name="achar">һ��Unicode�ַ�E/param>
        /// <param name="position">�ڴ�λ��֮ǰ��ȁE/param>
        /// <returns>����achar��TextListNodeʵ��</returns>
        public TextListNode Insert(char achar, TextListNode position)
        {
            TextListNode node= CreateOneNode(achar);
            Insert(node, position);
            return node;
        }
        /// <summary>
        ///��VeticalStringList����ĸ�����ȁE�λ��index֮ǰ
        /// </summary>
        /// <param name="StringtList">һ��EVeticalStringList���ʵ��</param>
        /// <param name="index">�ڴ�λ��֮ǰ��ȁE/param>
        /// <returns>��ȁE��ַ����ĳ��?/returns>
        public int Insert(CStringList StringtList, int index) 
        {
            string tmp = StringtList.Text;
            if (string.IsNullOrEmpty(tmp)) return 0;
            int i=Insert(tmp, index);
            return i;
        }
        /// <summary>
        /// ��text��ȁEndex֮ǰ
        /// </summary>
        /// <param name="text">Ҫ��ȁE��ı?/param>
        /// <param name="index">�ٴ�λ��֮ǰ��ȁE/param>
        /// <returns>����ȁE��ַ������?/returns>
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
            //if (len != len2) throw new Exception("��ȁE��ַ���������\n");
            return len2;

        }
        /// <summary>
        /// �������������ȁEµĽڵ�E
        /// </summary>
        /// <param name="node">����ȁE��½ڵ�E/param>
        /// <returns >nodeΪ�շ���false</returns>
        public bool Add(TextListNode node)//������Add����Add������ˁE����޸�_Length
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
        /// �������������ȁE���Unicode�ַ�E
        /// </summary>
        /// <param name="achar">Unicode�ַ�E/param>
        /// <returns>�ַ�Ϊ��ʱ����false</returns>
        public bool Add(char achar)
        {
           TextListNode node= CreateOneNode(achar);
           return Add(node);
        }
        /// <summary>
        /// �������������ȁEַ��?
        /// </summary>
        /// <param name="text">����ȁE��ַ��?/param>
        /// <returns>����ȁE��ַ������?/returns>
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
        ///�������������ȁE��EVeticalStringList�ĸ���
        /// </summary>
        /// <param name="StringList">һ��EVeticalStringList���ʵ��</param>
        /// <returns>����ȁE��ַ������?/returns>
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
                    throw new Exception("TextListNode���������E�±�E���Ϊ��ֵ��index="
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
                        throw new Exception("TextListNode���������E�������±�E�Խ�硣i="
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
                    throw new Exception("����꪿�");
                while (i < index)
                {
                    if (node == null)
                    {
                        throw new Exception("TextListNode���������E�������±�E��硣i="
                            + i.ToString() + ",index=" + index.ToString());
                    }
                    node = node.Next;
                    i++;
                }
                node.Text = value.Text;
            }
        }
        /// <summary>
        /// ժ��ĳ��Node����������ժ������������remove������ˁE
        /// </summary>
        /// <param name="node">��ժ���Ľڵ�E/param>
        /// <returns>�ɹ�true��ʧ��false</returns>
        private bool RemoveBase(TextListNode node)
        {
            if (node == null) return false;
            TextListNode last, next;
            next = node.Next;
            last = node.Last;

            _Length--;
            if (last== _Head )//��һ����Ϊ��
            {
                _Head.Connect (ref next);
            }

            if (next ==_Tail )//��һ��Ϊ��
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

            return count;//ʵ�ʱ�ɾ����Ԫ����
        }
        public int Remove(int start, int length)
        {
            TextListNode node = this[start];
            return Remove(node, length);
        }
        /// <summary>
        /// �滻,start2��end2���������滻start��end�����������ر��滻�Ľڵ����,
        /// ��E�Length
        /// </summary>
        /// <param name="start">���滻�Ŀ�ʼλ��</param>
        /// <param name="end">���滻�Ľ���λ��</param>
        /// <param name="start2">�滻�Ŀ�ʼλ��</param>
        /// <param name="end2">�滻�Ľ���λ��</param>
        /// <returns>�滻�Ľڵ����</returns>
        private  int ReplaceBase(TextListNode start,TextListNode end,TextListNode start2,TextListNode end2)
        {
            if (start == null
                || end == null
                || start2 == null
                || end2 == null) throw new Exception("�滻ʱ���󣬲����пն�ρE");

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
        /// ��start��endΪֹɾ��,�Ҳ����ӱ�ɾ�����֡�����ɾ���Ľڵ����
        /// </summary>
        /// <param name="start">��ʼ</param>
        /// <param name="end">��ʁE/param>
        /// <returns>ɾ���Ľڵ����</returns>
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
        /// ����
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
        /// ���ؽڵ�node��ǰ(��E��length���ڵ�E
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
        #region IDisposable ��Ա
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