using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

//using 

namespace MongolNote
{
    public partial class TextBoxV : System.Windows.Forms.TextBox
    {
        public TextBoxV()
        {
            //Text = "sdfsdfsdf";

            //mText = "";
            mSelectionLength = 0;

            //Text = "";
            //mLineCount = 0;
            //LastLinePos = new Point(0, 0);
            //aCaret .Position  = new Point(0, 0);

            mStringFormat = StringFormat.GenericTypographic;
            mStringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            //this.Font.
            FrushCount = 0;
            //LineInf = new LineInformation[1024];
            //LineInf[0] = new LineInformation();

            LogLineList = new List<CSLogLine>();             //////////////////////////
            CSLogLine EmpytyLogLine = new CSLogLine();////初始化逻辑行列表////
            LogLineList.Add(EmpytyLogLine);                     //////////////////////////

            lstLineInfo = new List<RealLine>();
            RealLine alf = new RealLine();
            lstLineInfo.Add(alf);

            mLastCaretPosition = new CarPos();
            mLastCaretPosition.Line = 0;
            mLastCaretPosition.Column = 0;
            mLastCaretPosition.Location = new PointF(0f, 0f);
            Caret = new CSCaret(this, Font.Height, 1);
            //CharCountTotal = 0;

            //aCarPos = new CarPos();
            isMouseDown = false;
            oldMousePos = new Point(0, 0);
            //bmpBg = null;
            Multiline = true;
            mSelectSBrush = new SolidBrush(Color.SteelBlue);
            mShadowBrush1 = new SolidBrush(Color.Black);
            mShadowBrush2 = new SolidBrush(ShadowColor);
            isShawdow = true;

            InitializeComponent();
            this.ContextMenu = new ContextMenu();
            try
            {
                this.Cursor = new Cursor(GetType(), "Cursor1.cur");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n 自定义光标失败\n" + ex.Message + "\n源：" + ex.Source);
            }

            //Text = "dddddddddddd";
        }
        private CSCaret Caret;
        //private CarPos aCarPos;
        private int FrushCount;
        private HScrollBar hScrollBar;
        private CarPos cpSelStart;
        private StringFormat mStringFormat;
        //private int WidthOfLastLine;
        //private LineInformation[] LineInf;
        private List<CSLogLine> LogLineList;//逻辑行信息列表

        //private List<RealLine> mLstLineInfo;
        private List<RealLine> lstLineInfo;    //实际行列表
        //{
        //    get 
        //    { 
        //        return mLstLineInfo; 
        //    }
        //    set 
        //    { 
        //        mLstLineInfo = value;
        //        hScrollBar.Maximum = Font.Height * value.Count;
        //    }
        //}
        //private int CharCountTotal;
        private bool isMouseDown;
        //private System.Drawing.Bitmap bmpBg;
        private int mSelectionLength;
        private int mSelectionStart;
        private int mSelectionEnd;
        private Point oldMousePos;//选择文本时记录旧的鼠标位置
        private int oldMouseIndex;//选择文本时记录鼠标按下时的索引位置
        private CarPos mLastCaretPosition;
        private ContextMenuStrip mPopMenu;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private SolidBrush mSelectSBrush;
        private SolidBrush mShadowBrush1;
        private SolidBrush mShadowBrush2;
        public bool isShawdow;
        private Color mShadowColor = Color.White;
        private int mMinContextWidth;
        public int MinContextWidth
        {
            get
            {
                return mMinContextWidth;
            }
            set
            {
                mMinContextWidth = value;
                this.Width = this.Width > value ? this.Width : value;
            }
        }
        public Color ShadowColor
        {
            get
            {
                return mShadowColor;
            }
            set
            {
                mShadowColor = value;
                mShadowBrush2 = new SolidBrush(value);
            }

        }
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem6;
        private Bitmap BufferBmp;
        //private int mACharWidth;// 判断换行的最小间距
        //private int SelLen;
        //public override se


        private int LineCount
        {
            get
            {
                int mCount = 0;
                for (int i = 0; i < LogLineList.Count; i++)
                {
                    mCount += LogLineList[i].LineList.Count;
                }
                return mCount;
                //LogLineList .m
            }
        }
        public override int TextLength
        {
            get
            {
                return Text.Length;
            }
        }
        public override string SelectedText
        {
            get
            {
                return Text.Substring(SelectionStart, SelectionLength);
            }
        }
        public override int SelectionLength
        {
            get
            {
                return mSelectionLength;
            }
            set
            {
                mSelectionLength = value;
                if (value == 0)
                {
                    mSelectionStart = mSelectionEnd = 0;
                }
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
                //int index = CarPos2Index2(Caret.Position);
                Caret.Hide();
                int index = CarPos2Index3(Caret.Position);
                Caret.Destroy();
                base.Font = value;
                InitializeLogLineList(Text, ClientRectangle.Height);
                ArrangeAll3(ClientRectangle.Height - 5);
                Caret = new CSCaret(this, Font.Height, 1);
                Caret.Position = Index2Carpos2(index, 0);
                //if (Caret.Position.Location.X > 0)
                //{
                //    int dd = 0;
                //}
                Caret.Show();


            }
        }
        //public overrid
        public new int SelectionStart
        {
            get
            {
                return mSelectionStart;
            }
            set
            {
                mSelectionStart = value;
                //this.ScrollBars 
            }
        }
        //public  override lines

        //private CarPos cpSelEnd;
        //public  Bitmap bmp;
        public override bool Multiline
        {
            get
            {
                return base.Multiline;
            }

        }
        //public override selection


        //private string Text;
        public override string Text
        {
            get
            {
                string tmpText = "";
                int l = LogLineList.Count;
                for (int i = 0; i < l - 1; i++)
                {
                    tmpText += LogLineList[i].ToString() + "\r\n";
                }
                tmpText += LogLineList[l - 1].ToString();
                return tmpText;

            }
            set
            {
                //Text = value;

                //string tmpText = value;
                Caret.Hide();
                mSelectionStart = 0;
                mSelectionLength = 0;
                mSelectionEnd = 0;
                InitializeLogLineList(value, 100);//初始化所有逻辑行，制作单词列表
                ArrangeAll3(ClientRectangle.Height - 5); //排列单词列表
                ReDraw(ClientRectangle.Width, ClientRectangle.Height);
                CarPos aCP = new CarPos();
                aCP.Column = 0;
                aCP.Line = 0;
                aCP.Location = new PointF(0f, 0f);

                Caret.Position = aCP;
                Caret.Show();
            }
        }
        private void InitializeLogLineList(string strText, int MaxHeight)
        {
            //初始化单词链表,index<0初始化所有逻辑行，否则指定要初始化的逻辑行
            //int TotalLenth;
            LogLineList = new List<CSLogLine>();
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

                        //tmp = tmp.TrimStart(trimChar);

                        //tmp = "  " + tmp;
                        if (tmp.Substring(tmp.Length - 1, 1) == "\r")
                        {
                            aLogLine = new CSLogLine(tmp.Substring(0, tmp.Length - 1), MaxHeight, this.CreateGraphics(), Font, mStringFormat);
                        }
                        else
                        {
                            aLogLine = new CSLogLine(tmp, MaxHeight, this.CreateGraphics(), Font, mStringFormat);
                        }


                    }
                    LogLineList.Add(aLogLine);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void SplitText(string strText)
        {
            ArrayList myLineList = new ArrayList();
            //string tmpString = "";
            char[] w ={ '\r' };
            int start = 0;
            int l = strText.Length;
            int pos = 0;
            do
            {
                pos = strText.IndexOfAny(w, 0);
                if (pos > 0)
                {
                    string s = strText.Substring(pos + 1, 1);
                    if (s == "\n")
                    {
                        s = strText.Substring(start, pos);
                        myLineList.Add(s);

                    }
                    start = pos + 2;
                    strText = strText.Substring(start, strText.Length - start);
                }
            } while (pos > 0);
            myLineList.Add(strText);
        }

        public new string[] Lines
        {
            get
            {
                //char[] seprator ={ '\r', '\n' };
                string[] tmpLine = new string[LogLineList.Count];
                for (int i = 0; i < LogLineList.Count; i++)
                {
                    tmpLine[i] = LogLineList[i].ToString();
                }
                return tmpLine;

            }

        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int _Left;
            public int _Top;
            public int _Right;
            public int _Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }
        [DllImport("user32.dll")]
        static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);
        [DllImport("user32.dll")]
        static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);
        protected override void WndProc(ref Message m)
        {
            char ascii_code;  //获取按下的ASCII码
            int iKeyCode;
            int key_state;    //获取按下的键状态
            string strBuf;    //字符缓冲
            //CarPos aCarPos;
            //Point MousePoint;
            int MouseX, MouseY;
            Graphics g = this.CreateGraphics();
            //Text += m.Msg.ToString();
            // Listen for operating system messages.
            switch (m.Msg)
            {
                // The WM_ACTIVATEAPP message occurs when the application
                // becomes the active application or becomes inactive.
                //case WM_INPUTLANGCHANGE:
                //    MessageBox.Show("WM_INPUTLANGCHANGE");
                //    break;
                case WM_CREATE:
                    ////注册热键Ctrl+A，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
                    //HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.A );
                    ////注册热键Ctrl+C，Id号为102。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
                    //HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Ctrl, Keys.C);
                    ////注册热键Ctrl+X，Id号为103。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
                    //HotKey.RegisterHotKey(Handle, 103, HotKey.KeyModifiers.Ctrl, Keys.X);
                    ////注册热键Ctrl+V，Id号为104。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
                    //HotKey.RegisterHotKey(Handle, 104, HotKey.KeyModifiers.Ctrl, Keys.V);
                    ////注册热键Ctrl+Z，Id号为105。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
                    //HotKey.RegisterHotKey(Handle, 105, HotKey.KeyModifiers.Ctrl, Keys.Z);   
                    //break;
                    //int i = mText.Length;
                    break;

                case WM_ACTIVATE:
                    Caret = new CSCaret(this, Font.Height, 1);
                    Caret.Position.Location = new PointF(0, 0);


                    Caret.Show();
                    break;

                case WM_SETFOCUS://获得焦点后
                    //Text = "Got Focus";


                    Caret = new CSCaret(this, Font.Height, 1);

                    Caret.Position = mLastCaretPosition;

                    Caret.Show();
                    break;
                //case WM_MDIMAXIMIZE:
                //    break;
                case WM_KILLFOCUS://失去焦点后
                    //Text = "Lost Focus";
                    mLastCaretPosition = Caret.Position;
                    Caret.Destroy();
                    Caret.Dispose();
                    break;
                case WM_LBUTTONDOWN:
                    MouseX = (short)(m.LParam.ToInt32() & 0xffff);
                    MouseY = (short)(m.LParam.ToInt32() >> 16);
                    OnLMouseDown(new Point(MouseX, MouseY));
                    //try
                    //{
                    //    base.WndProc(ref m); 
                    //}
                    //catch(Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message  +ex.TargetSite +"\n"+ex.InnerException+"\n"+ex.HelpLink+ex.Source+ex.GetBaseException().ToString ());
                    //}
                    break;
                case WM_RBUTTONDOWN:
                    MouseX = (short)(m.LParam.ToInt32() & 0xffff);
                    MouseY = (short)(m.LParam.ToInt32() >> 16);
                    mPopMenu.Show(this, new Point(MouseX, MouseY));
                    //try
                    //{
                    //    base.DefWndProc(ref m);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message + ex.StackTrace);
                    //}
                    break;
                case WM_LBUTTONUP:
                    MouseX = (short)(m.LParam.ToInt32() & 0xffff);
                    MouseY = (short)(m.LParam.ToInt32() >> 16);
                    OnLMouseUp(new Point(MouseX, MouseY));
                    //try
                    //{
                    //    base.DefWndProc(ref m);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message + ex.StackTrace);
                    //}
                    break;
                case WM_MOUSEMOVE:
                    MouseX = (short)(m.LParam.ToInt32() & 0xffff);
                    MouseY = (short)(m.LParam.ToInt32() >> 16);
                    OnMouseMove(new Point(MouseX, MouseY));
                    //try
                    //{
                    //    base.DefWndProc(ref m);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message + ex.StackTrace);
                    //}
                    break;
                case WM_LBUTTONDBLCLK:
                    MouseX = (short)(m.LParam.ToInt32() & 0xffff);
                    MouseY = (short)(m.LParam.ToInt32() >> 16);
                    CSWord tmpWord = SearchWord(Caret.Position.Line, Caret.Position.Column);
                    MessageBox.Show(tmpWord.Text);
                    //base.DefWndProc(ref m);
                    break;
                case WM_MOUSEWHEEL:
                    base.DefWndProc(ref m);
                    break;
                case WM_PAINT:
                    PAINTSTRUCT r;
                    IntPtr ip = BeginPaint(this.Handle, out r);
                    RECT rect = r.rcPaint;
                    //int newWidh = rect._Right - rect._Left;
                    //int newHeight = rect._Bottom - rect._Top;
                    Rectangle Rect = new Rectangle(new Point(rect._Left, rect._Top)
                                                                     , new Size(rect._Right - rect._Left, rect._Bottom - rect._Top));


                    Graphics tmpG = Graphics.FromHdc(ip);
                    OnPaintEx(tmpG, Rect);

                    EndPaint(this.Handle, ref r);

                    break;
                case WM_COPY:
                    //MessageBox.Show("Copy");
                    break;
                case WM_PASTE:
                    MessageBox.Show("WM_PASTE");
                    break;
                case WM_SIZING:
                    MessageBox.Show("sizing");
                    break;
                case WM_SIZE:
                    int cx = (short)(m.LParam.ToInt32() & 0xffff);
                    int cy = (short)(m.LParam.ToInt32() >> 16);
                    int bian = cy - RealLine.MaxHeight;
                    //if (cy < ClientRectangle.Width) Invalidate();

                    if (cy - ClientRectangle.Height != 0)//纵方向有变化
                    {
                        if (bian > 5 || bian < -5)
                        {
                            int tmpIndex = CarPos2Index3(Caret.Position);

                            ArrangeAll3(cy - 5);
                            Caret.Position = Index2Carpos2(tmpIndex, 0);
                            Invalidate();
                        }
                    }
                    else
                    {
                    }

                    break;
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 101://注册热键Ctrl+A
                            toolStripMenuItem4_Click((object)this, new EventArgs());

                            break;
                        case 102://注册热键Ctrl+C
                            toolStripMenuItem2_Click((object)this, new EventArgs());

                            break;
                        case 103://注册热键Ctrl+X
                            toolStripMenuItem5_Click((object)this, new EventArgs());

                            break;
                        case 104://注册热键Ctrl+V
                            toolStripMenuItem3_Click((object)this, new EventArgs());

                            break;
                        case 105://注册热键Ctrl+Z
                            toolStripMenuItem2_Click((object)this, new EventArgs());

                            break;
                        default:
                            break;
                    }
                    //MessageBox.Show(m.WParam.ToInt32().ToString ());
                    break;
                //case 0x0021: //当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口 
                //    Text = "asdf";
                //    break;
                //case 0x000C://应用程序发送此消息来设置一个窗口的文本
                //    Text = "asdf";
                //    break;

                case WM_KEYDOWN:
                    if (ReadOnly) return;
                    int tmpKey = m.WParam.ToInt32(); //获取按下的ASCII码
                    key_state = m.LParam.ToInt32(); //获取按下的键状态
                    OnKeyDown(tmpKey, key_state);



                    break;
                case WM_CHAR://按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息 
                    if (ReadOnly) return;
                    iKeyCode = m.WParam.ToInt32();
                    if (iKeyCode < 31) break;
                    //if (iKeyCode < 0x9FFF && iKeyCode > 0x3000)
                    //{
                    //    MessageBox.Show("中日韩文");
                    //}
                    ascii_code = Convert.ToChar(iKeyCode); //获取按下的ASCII码

                    //key_state = m.LParam.ToInt32(); //获取按下的键状态
                    strBuf = ascii_code.ToString();
                    OnChar(strBuf);


                    break;

                default:
                    base.WndProc(ref m);
                    //base.DefWndProc(ref m);
                    break;
            }

        }

        private void TestDraw()
        {
            Graphics g = this.CreateGraphics();
            FrushCount++;
            //g.Clear(Color.White);
            //g.TranslateTranmStringFormatorm(0, 20);
            //Rectangle r = new Rectangle(new Point(10, 10), new Size(100, Font.Height+5));
            Rectangle r = this.ClientRectangle;
            //g.DrawRectangle (Pens.Black , r);
            g.FillRectangle(Brushes.Wheat, r);
            //g.DrawString(FrushCount.ToString(), Font, Brushes.Blue, new Point(10, 10));
            //g.ResetTranmStringFormatorm();
            //g.draw
        }
        private bool isSelected(int index)
        {
            if (index < mSelectionStart)
                return false;
            if (index >= mSelectionEnd)
                return false;
            return true;
        }

        private void ArrangeAll3(int MaxHeight)
        {
            //int IndexOfLogLine=0;
            //InitializeLogLineList();
            //CCC++;
            RealLine.MaxHeight = 0;
            RealLine.StandardHeight = MaxHeight;
            lstLineInfo = new List<RealLine>();
            for (int i = 0; i < LogLineList.Count; i++)
            {
                LogLineList[i].ArrangeLine(MaxHeight, i);

                for (int j = 0; j < LogLineList[i].LineList.Count; j++)
                {
                    lstLineInfo.Add(LogLineList[i].LineList[j]);
                }
            }
            MinContextWidth = lstLineInfo.Count * Font.Height;
        }
        //private void ArrangeALogLine(int index,int MaxHeight)
        //{
        //    if(index>=LogLineList .Count )return ;
        //    CSLogLine aLogLine = LogLineList[index];
        //    if (aLogLine.WordList.Count == 0) return;
        //    List<CSWord >MyWordList= aLogLine .WordList ;

        //    int LineHeight=0;
        //    int i=0;
        //    while (LineHeight + MyWordList[i].Height < MaxHeight)
        //    {

        //    }
        //}
        private int ArrangeString(string strLine, int MaxHeight, Graphics g)
        {
            //返回一行的字符数量/

            if (String.IsNullOrEmpty(strLine))//文本为空则……
            {
                return -1;
            }

            int LenOfLine = 0;//行的长度
            int LenOfWord = 0;//单词长度
            //int start = 0;//单词的起始位置

            int Height = 0;
            int NextWordHeight = 0;
            //bool isTooLongWord = false;//高度超过NextWordHeight的单词的长度
            string strWord;
            while (Height + NextWordHeight < MaxHeight)
            {
                Height += NextWordHeight;//高度
                LenOfLine += LenOfWord;   //长度

                string tmpNokori = strLine.Substring(LenOfLine, strLine.Length - LenOfLine);

                if (string.IsNullOrEmpty(tmpNokori)) break;

                LenOfWord = GetAWord(tmpNokori);

                if (LenOfWord < 0)
                {
                    break;
                }
                else
                {
                    strWord = strLine.Substring(LenOfLine, LenOfWord);
                    WordSize WordSize = MeasureWord(strWord, g, MaxHeight);
                    NextWordHeight = WordSize.Height;
                    LenOfWord = WordSize.Lenth;

                    //if (NextWordHeight > MaxHeight)
                    //{
                    //    //LenOfWord = 1;
                    //    //strWord = strLine.Substring(LenOfLine, LenOfWord);
                    //    //NextWordHeight = MeasureWord(strWord, g);
                    //    break;

                    //}
                }
            }
            //if (isTooLongWord)
            //{
            //    while (NextWordHeight > MaxHeight)
            //    {
            //        strWord = strLine.Substring(LenOfLine, LenOfWord);
            //        NextWordHeight = MeasureWord(strWord, g, MaxHeight);
            //    }
            //}

            return LenOfLine;//返回本行的长度
        }
        //private void ArrangeALine(int LineIndex , int cHeight)//不绘图，只重设lstLineInfo
        //{//重新排版一个逻辑行，一行中不包含换行符

        //    if (String.IsNullOrEmpty(Text))//文本为空则……
        //    {
        //        return;
        //    }


        //    if (LineIndex >= Lines.Length) return;

        //    LineInformation aLineInf;// = new LineInformation();


        //    string aChar;
        //    SizeF sChar;
        //    int tmpWidth = 0;
        //    bool isNoSpace = false;//为true表示字符串没有空格
        //    Graphics g = this.CreateGraphics();
        //    //LineInformation aLineInf = new LineInformation();
        //    int Line = LineIndex;
        //    lstLineInfo[LineIndex].Height = 0;
        //    lstLineInfo[Line].Lenth = 0;
        //    string logLine=Lines[LineIndex];//一个逻辑行，不包含换行符

        //    for (int i =0; i < logLine.Length ; i++)
        //    {
        //        char chrLogLine=logLine.ToCharArray (0,logLine.Length-i);
        //        GetNextWord (
        //        aChar = this.Lines[LineIndex].Substring(i, 1);//获取一个字符

        //        sChar = g.MeasureString(aChar, Font, cHeight, mStringFormat);//计算尺寸
        //        tmpWidth = Convert.ToInt32(sChar.Width);//获取宽度



        //        if (lstLineInfo[Line].Height + tmpWidth > cHeight)//当前行高+字符宽度>指定的高度则换行
        //        {
        //            if (++Line == lstLineInfo.Count)
        //            {
        //                aLineInf = new LineInformation();
        //                lstLineInfo.Add(aLineInf);

        //            }

        //            lstLineInfo[Line].Height = 0;//需要重新计算高度
        //            lstLineInfo[Line].Lenth = 0;
        //            lstLineInfo[Line].Head = i;//text里可能并没有i+1下标
        //            isNewLine = false;

        //        }
        //        else if (aChar == " ")
        //        {
        //            isSpace = true;
        //        }
        //        else if (isSpace && isNoSpace == false && (i + 1) < mText.Length)
        //        {
        //            isSpace = false;
        //            char space = (char)32;
        //            int tmpIndex = mText.IndexOf(space, i);
        //            int l = tmpIndex - i;
        //            //int tmpIndex = mText.IndexOf(''
        //            if (tmpIndex < 0)
        //            {
        //                isNoSpace = true;
        //                l = mText.Length - i;
        //            }


        //            string tmpString = mText.Substring(i, l);
        //            char[] chrChar = tmpString.ToCharArray();
        //            int CharCode = (int)chrChar[0];
        //            int CharCount = 0;
        //            while ((CharCode > 0x9FFF || CharCode < 0x3000) && CharCount < l)
        //            {
        //                CharCount++;//CharCount=有几个非中日韩字符
        //            }
        //            tmpString = mText.Substring(i, CharCount);//tmpString可能是几种文字，为简单期间，先不考虑复杂的情况
        //            sChar = g.MeasureString(tmpString, Font, cHeight, mStringFormat);//计算尺寸
        //            int tmpWidthWord = Convert.ToInt32(sChar.Width);//获取宽度
        //            if (lstLineInfo[Line].Height + tmpWidthWord > cHeight && tmpWidthWord < cHeight)//当前行高+字符宽度>指定的高度则换行
        //            {
        //                if (++Line == lstLineInfo.Count)
        //                {
        //                    aLineInf = new LineInformation();
        //                    lstLineInfo.Add(aLineInf);

        //                }

        //                lstLineInfo[Line].Height = 0;//需要重新计算高度
        //                lstLineInfo[Line].Lenth = 0;
        //                lstLineInfo[Line].Head = i;//text里可能并没有i+1下标
        //                isNewLine = false;
        //            }




        //            lstLineInfo[Line].Height += tmpWidth;
        //            lstLineInfo[Line].Lenth++;
        //        }

        //    }//for
        //    int tmpL=lstLineInfo .Count -Line -1;
        //    while (tmpL>0)
        //    {
        //        lstLineInfo.RemoveAt(lstLineInfo.Count-1);
        //        tmpL--;
        //    }
        //    //lstLineInfo.Add(aLineInf);

        //    g.Dispose();
        //}
        private struct WordSize
        {
            public WordSize(int height, int lenth)
            {
                Height = height;
                Lenth = lenth;
            }
            public int Height;
            public int Lenth;
        }
        private WordSize MeasureWord(string Word, Graphics g, int MaxHeight)//计量单词的尺寸
        {
            //超过MaxHeight返回索引


            //Graphics g = this.CreateGraphics();
            int len = 0;
            int height = 0;
            try
            {
                //两种计算方法，可能会有误差

                ////第一种计算方法
                //int i;
                for (len = 0; len < Word.Length; len++)
                {
                    string aChar = Word.Substring(len, 1);
                    SizeF sChar = g.MeasureString(aChar, Font, ClientRectangle.Height, mStringFormat);//计算尺寸
                    int width = Convert.ToInt32(sChar.Width);

                    if (height + width > MaxHeight)
                    {
                        break;
                    }
                    height += width;

                }

                ////第二种计算方法
                //SizeF sChar = g.MeasureString(Word, Font, ClientRectangle.Height, mStringFormat);//计算尺寸
                //height = Convert.ToInt32(sChar.Width);


            }

            catch (Exception ex)
            {
                MessageBox.Show("计量单词时出错\n" + Word + "\n" + ex.Message, "Error！"
                                            , MessageBoxButtons.OK
                                            , MessageBoxIcon.Error);
            }
            //return new WordSize
            return new WordSize(height, len);
        }
        private int GetAWord(string MyString)
        {
            //返回一个单词中的字母个数最小1
            //一个单词中不能有换行，空格，标点符号
            //不同语言的单词，在排版时不可分割的整体（如果单词高度超过行高，则除外）
            //应该包含紧跟其后的标点符号
            int Lenth = MyString.Length;
            char[] CharArray = MyString.ToCharArray();
            int CharCount = 0;
            int i;
            bool isNewLine = false;
            for (i = 0; i < Lenth; i++)
            {
                int CharCode = (int)CharArray[i];


                if (CharCode > 0x3000 && CharCode < 0x9FFF)//中日韩字符；
                {
                    return 1;
                }
                if (CharCode == 32) //空格，
                {
                    if (i == 0)
                        return 1;
                    break;
                }
                if (CharArray[i] == '\n')
                {
                    isNewLine = false;
                }
                if (CharArray[i] == '\r' && isNewLine)
                {
                    return i - 2;//遇到换行符
                }
            }
            CharCount = i;
            return CharCount;
        }

        //private void ArrangeAll(int cWidth,int cHeight)//不绘图，只重设lstLineInfo
        //{//重新排版
        //    lstLineInfo.Clear();
        //    LineInformation.MaxHeight = 0;
        //    if (String.IsNullOrEmpty(Text))
        //    {
        //        LineInformation aLF = new LineInformation();
        //        aLF.Lenth = 0;
        //        aLF.Height = 0;

        //        lstLineInfo.Add(aLF);
        //        return;
        //    }


        //    string aChar;
        //    SizeF sChar;
        //    int tmpWidth=0;
        //    Graphics g = this.CreateGraphics();
        //    LineInformation aLineInf = new LineInformation();

        //    for (int i = 0; i < Text.Length; i++)
        //    {
        //        aChar = Text.Substring(i, 1);//获取一个字符
        //        sChar = g.MeasureString(aChar, Font, cHeight , mStringFormat);//计算尺寸
        //        tmpWidth = Convert.ToInt32(sChar.Width);//获取宽度
        //        if (aLineInf.Height + tmpWidth > cHeight)//当前行高+字符宽度
        //        {
        //            lstLineInfo.Add(aLineInf);
        //            aLineInf = new LineInformation();
        //        }

        //        aLineInf.Height += tmpWidth;
        //        aLineInf.Lenth++;

        //    }
        //    lstLineInfo.Add(aLineInf);

        //    g.Dispose();
        //}
        int DrawTextWithIndex(string MyString, Graphics g, bool blSelected)//先不考虑换行的情况
        {//画小于等于一行长度字符
            //CarPos tmpCarPos = Index2Carpos2(start,0);


            int lenth = MyString.Length;
            string aChar;
            SizeF sChar;
            int pos = 0;
            Rectangle tmpR;
            Brush aBrush;
            Brush shadaw;

            g.ResetTransform();
            g.TranslateTransform(Font.Height, 0);
            g.RotateTransform(90);
            if (blSelected)
            {
                g.Clear(mSelectSBrush.Color);
                aBrush = new SolidBrush(Color.White);
                shadaw = mShadowBrush1;

            }
            else
            {
                g.Clear(BackColor);
                aBrush = new SolidBrush(ForeColor);
                shadaw = mShadowBrush2;
            }
            for (int i = 0; i < lenth; i++)
            {
                aChar = MyString.Substring(i, 1);
                sChar = g.MeasureString(aChar, Font, this.Width, mStringFormat);
                tmpR = new Rectangle(pos, 0
                           , Convert.ToInt32(sChar.Width), Font.Height);

                if (isShawdow)
                    g.DrawString(aChar, Font, shadaw, new Point(pos + 1, 2), mStringFormat);
                g.DrawString(aChar, Font, aBrush, new Point(pos, 0), mStringFormat);


                pos += Convert.ToInt32(sChar.Width);
            }
            g.ResetTransform();
            return pos;
        }

        void DrawAllLine(Graphics g)//重画所有行
        {
            //mLineCount =lstLineInfo .Count ;
            for (int i = 0; i < LineCount; i++)
            {
                //DrawALine2(g, i, Font.Height * i);
                DrawALine2(g, i, Font.Height * i);
            }
        }
        void RePaintSomeLine(int start, int len, Graphics g)
        {
            for (int i = 0; i < len; i++)
            {
                DrawALine2(g, start + i, Font.Height * i);
            }
        }
        private void RePaint2(Graphics g)//重画客户区
        {
            int line = ClientRectangle.Width / Font.Height + 1;
            line = LineCount < line ? LineCount : line;

            for (int i = 0; i < line; i++)
            {
                DrawALine2(g, i, Font.Height * i);
            }
        }
        void ReDraw(int Width, int Height/*,int OffsetX //x方向的偏移量*/ )//并使用双缓冲区重画所有行
        {

            Graphics g = this.CreateGraphics();
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics bmpG = Graphics.FromImage(bmp);
            bmpG.Clear(BackColor);
            DrawAllLine(bmpG);
            g.DrawImage(bmp, ClientRectangle.Location);
            //Caret.Position = Index2Carpos(CarPos2Index(Caret.Position), 1);
            g.Dispose();
            bmpG.Dispose();
        }

        //void DrawALine(Graphics g, int LineIndex,int x)//画一行
        //{
        //    if (String.IsNullOrEmpty(Text)) return;
        //    g.TranslateTransform(x+Font.Height , 0);
        //    g.RotateTransform(90);

        //    string tmpChr;
        //    SizeF sChar;
        //    int pos = 0;
        //    int head=0;
        //    Rectangle tmpR;
        //    SolidBrush  aBrush;
        //    SolidBrush shadaw;
        //    int maxIndex = LineCount;
        //    for(int i=0;i<LineIndex ;i++)
        //        head += lstLineInfo[i].Length;
        //    if (head >= Text.Length) return;
        //    for (int i = 0; i < lstLineInfo[LineIndex].Length; i++)
        //    {
        //        int tmpIndex = head + i;
        //        tmpChr = Text.Substring(tmpIndex, 1);
        //        sChar = g.MeasureString(tmpChr, Font, this.Width, mStringFormat);
        //        tmpR = new Rectangle(pos, 0
        //                   , Convert.ToInt32(sChar.Width), Font.Height);
        //        if (isSelected(tmpIndex))
        //        {

        //            g.FillRectangle(mSelectSBrush, tmpR);
        //            aBrush = new SolidBrush(Color .White );
        //            shadaw = mShadowBrush1;
        //        }
        //        else
        //        {
        //            //SolidBrush bgBrush = new SolidBrush(BackColor);
        //            //g.FillRectangle(bgBrush, tmpR);
        //            shadaw = mShadowBrush2;
        //            aBrush = new SolidBrush(ForeColor);
        //        }
        //        if (isShawdow )
        //            g.DrawString(tmpChr, Font, shadaw, new Point(pos + 1, 2), mStringFormat);
        //        g.DrawString(tmpChr, Font, aBrush, new Point(pos, 0), mStringFormat);


        //        //g.DrawString(tmpChr, Font, Brushes.Black, new Point(pos, 0), mStringFormat);

        //        if (tmpChr == " ")
        //            pos += lstLineInfo[LineIndex].SpaceWidth;
        //        else
        //            pos += Convert.ToInt32(sChar.Width);
        //    }
        //    g.ResetTransform();
        //}

        //int DrawALineEx(Graphics g, int LineIndex, int x,string strLine,int cHeight)//画一行
        //{
        //    if (String.IsNullOrEmpty(strLine)) return -1;
        //    if (LineIndex > lstLineInfo.Count - 1 || LineIndex < 0) return-1;
        //    g.TranslateTransform(x + Font.Height, 0);
        //    g.RotateTransform(90);

        //    string aChar;
        //    SizeF sChar;
        //    Rectangle tmpR;
        //    SolidBrush aBrush;
        //    SolidBrush shadaw;

        //    //int maxIndex = LineCount;
        //    int pos = 0;
        //    int head = 0;
        //    int lenth = strLine.Length;
        //    int tmpWidth=0;
        //    bool isNewLine=false;
        //    bool isSpace=false;
        //    //if (LineIndex < lstLineInfo.Count - 1)
        //    //{
        //    //    if (lstLineInfo[LineIndex + 1].Head - head > lenth)
        //    //    {

        //    //        行末有不可见字符。换行符等……
        //    //    }

        //    //}

        //    for (int i = 0; i < lenth; i++)
        //    {
        //        aChar = strLine.Substring(i, 1);//获取一个字符

        //        sChar = g.MeasureString(aChar, Font, cHeight, mStringFormat);//计算尺寸
        //        tmpWidth = Convert.ToInt32(sChar.Width);//获取宽度
        //        if (aChar == "\r")//换行符
        //        {
        //            isNewLine = true;//1
        //        }
        //        else if (aChar == "\n" && isNewLine)
        //        {
        //            return 0;
        //        }
        //        else
        //        {

        //            if (lstLineInfo[LineIndex].Height + tmpWidth > cHeight)//当前行高+字符宽度>指定的高度则换行
        //            {
        //                return i;
        //            }
        //            else if (aChar == " ")
        //            {
        //                isSpace = true;
        //            }
        //            else if (isSpace  && (i + 1) < mText.Length)
        //            {
        //                isSpace = false;
        //                char space = (char)32;
        //                int tmpIndex = strLine.IndexOf(space, i);
        //                int l = tmpIndex - i;
        //                //int tmpIndex = mText.IndexOf(''
        //                if (tmpIndex < 0)
        //                {
        //                    //isNoSpace = true;
        //                    l = strLine.Length - i;
        //                }


        //                string tmpString = mText.Substring(i, l);
        //                char[] chrChar = tmpString.ToCharArray();
        //                int CharCode = (int)chrChar[0];
        //                int CharCount = 0;
        //                while ((CharCode > 0x9FFF || CharCode < 0x3000) && CharCount < l)
        //                {
        //                    CharCount++;//CharCount=有几个非中日韩字符
        //                }
        //                tmpString = strLine.Substring(i, CharCount);//strLine可能是几种文字，为简单期间，先不考虑复杂的情况
        //                sChar = g.MeasureString(tmpString, Font, cHeight, mStringFormat);//计算尺寸
        //                int tmpWidthWord = Convert.ToInt32(sChar.Width);//获取宽度
        //                if (lstLineInfo[LineIndex].Height + tmpWidthWord > cHeight && tmpWidthWord < cHeight)//当前行高+字符宽度>指定的高度则换行
        //                {
        //                    //Line = lstLineInfo.Count;
        //                    //aLineInf = new LineInformation();
        //                    //lstLineInfo.Add(aLineInf);
        //                    //lstLineInfo[Line].Head = i;
        //                }



        //            }
        //            lstLineInfo[LineIndex].Height += tmpWidth;
        //            lstLineInfo[LineIndex].Lenth++;
        //        }

        //    }//for

        //    for (int i = 0; i < lenth; i++)
        //    {
        //        int tmpIndex = head + i;
        //        string tmpChr;
        //        tmpChr = Text.Substring(tmpIndex, 1);
        //        //if (tmpChr == "\r") tmpChr = "\n";
        //        sChar = g.MeasureString(tmpChr, Font, this.Width, mStringFormat);
        //        tmpR = new Rectangle(pos, 0
        //                   , Convert.ToInt32(sChar.Width), Font.Height);
        //        if (isSelected(tmpIndex))
        //        {

        //            g.FillRectangle(mSelectSBrush, tmpR);
        //            aBrush = new SolidBrush(Color.White);
        //            shadaw = mShadowBrush1;
        //        }
        //        else
        //        {
        //            //SolidBrush bgBrush = new SolidBrush(BackColor);
        //            //g.FillRectangle(bgBrush, tmpR);
        //            shadaw = mShadowBrush2;
        //            aBrush = new SolidBrush(ForeColor);
        //        }
        //        if (isShawdow)
        //            g.DrawString(tmpChr, Font, shadaw, new Point(pos + 1, 2), mStringFormat);
        //        g.DrawString(tmpChr, Font, aBrush, new Point(pos, 0), mStringFormat);


        //        //g.DrawString(tmpChr, Font, Brushes.Black, new Point(pos, 0), mStringFormat);


        //        pos += Convert.ToInt32(sChar.Width);
        //    }
        //    g.ResetTransform();
        //    return 
        //}
        //void DrawARealLine(Graphics g, int LineIndex, int Offset)//画一行,Offset偏移量
        //{
        //    if (String.IsNullOrEmpty(Text)) return;
        //    if (LineIndex > lstLineInfo.Count - 1 || LineIndex < 0) return;
        //    g.ResetTransform();
        //    g.TranslateTransform(Offset + Font.Height, 0);
        //    g.RotateTransform(90);


        //    string strRealLine="";
        //    CSWord aWord = lstLineInfo[LineIndex].HeadWord;

        //    for(int i=0;i<lstLineInfo[LineIndex].WordCount ;i++)
        //    {
        //        strRealLine += aWord.Text;
        //        aWord = aWord.Next;
        //    }


        //    string tmpChr;

        //    SizeF sChar;
        //    Rectangle tmpR;
        //    SolidBrush aBrush;
        //    SolidBrush shadaw;

        //    //int maxIndex = LineCount;
        //    int pos = 0;
        //    int head =0;
        //    int lenth = strRealLine.Length ;
        //    //lstLineInfo[LineIndex].HeadWord .
        //    //string strLogLine = Lines[lstLineInfo[LineIndex].LogLine];//逻辑行


        //    for (int i = 0; i < lenth; i++)
        //    {
        //        int tmpIndex = head + i;
        //        int tmpL = strRealLine.Length;
        //        tmpChr = strRealLine.Substring(tmpIndex, 1);
        //        //if (tmpChr == "\r") tmpChr = "\n";
        //        sChar = g.MeasureString(tmpChr, Font, this.Width, mStringFormat);
        //        tmpR = new Rectangle(pos, 0
        //                   , Convert.ToInt32(sChar.Width), Font.Height);
        //        if (isSelected(tmpIndex))
        //        {

        //            g.FillRectangle(mSelectSBrush, tmpR);
        //            aBrush = new SolidBrush(Color.White);
        //            shadaw = mShadowBrush1;
        //        }
        //        else
        //        {
        //            //SolidBrush bgBrush = new SolidBrush(BackColor);
        //            //g.FillRectangle(bgBrush, tmpR);
        //            shadaw = mShadowBrush2;
        //            aBrush = new SolidBrush(ForeColor);
        //        }
        //        if (isShawdow)
        //            g.DrawString(tmpChr, Font, shadaw, new Point(pos + 1, 2), mStringFormat);
        //        g.DrawString(tmpChr, Font, aBrush, new Point(pos, 0), mStringFormat);

        //        pos += Convert.ToInt32(sChar.Width);
        //    }
        //    g.ResetTransform();

        //}
        void DrawALine2(Graphics g, int LineIndex, int x)//画一行
        {
            //if (String.IsNullOrEmpty(Text)) return;
            if (LineIndex > lstLineInfo.Count - 1 || LineIndex < 0) return;
            g.TranslateTransform(x + Font.Height, 0);
            g.RotateTransform(90);

            string tmpChr = "";

            SizeF sChar;
            Rectangle tmpR;
            SolidBrush aBrush;
            SolidBrush shadaw;

            //int maxIndex = LineCount;
            int pos = 0;
            int sss = LogLineList.Count;
            int head = Index_Logline2Text(lstLineInfo[LineIndex].LogLine, Index_Line2LogLine(0, LineIndex));
            int lenth = lstLineInfo[LineIndex].Length;

            string strLine = lstLineInfo[LineIndex].Text;
            //string strLin2 = Text.Substring(head, lenth);
            //string strLin3 = Text.Substring(567, lenth);
            //int k = CarPos2Index3(Caret.Position);
            //string strLine3 = Text.Substring(k, lenth);


            for (int i = 0; i < lenth; i++)
            {
                //int tmpIndex = head + i;
                //int tmpL = strLine.Length;

                tmpChr = strLine.Substring(i, 1);

                //if (tmpChr == "\r") tmpChr = "\n";
                sChar = g.MeasureString(tmpChr, Font, this.Width, mStringFormat);
                tmpR = new Rectangle(pos, 0, Convert.ToInt32(sChar.Width), Font.Height);

                if (isSelected(i + head))
                {

                    g.FillRectangle(mSelectSBrush, tmpR);
                    aBrush = new SolidBrush(Color.White);
                    shadaw = mShadowBrush1;
                }
                else
                {
                    //SolidBrush bgBrush = new SolidBrush(BackColor);
                    //g.FillRectangle(bgBrush, tmpR);
                    shadaw = mShadowBrush2;
                    aBrush = new SolidBrush(ForeColor);
                }

                if (isShawdow)
                    g.DrawString(tmpChr, Font, shadaw, new Point(pos + 1, 2), mStringFormat);
                g.DrawString(tmpChr, Font, aBrush, new Point(pos, 0), mStringFormat);


                //g.DrawString(tmpChr, Font, Brushes.Black, new Point(pos, 0), mStringFormat);


                //if (tmpChr == " ")
                //    pos += lstLineInfo[LineIndex].SpaceWidth;
                //else
                pos += Convert.ToInt32(sChar.Width);
            }
            g.ResetTransform();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error in DrawLine2\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }




        //private this.



        private void clsTextBox_Resize(object sender, EventArgs e)
        {
            MessageBox.Show("On Sizing");
        }


        private CarPos LCtoOffset2(int nLine, int nCol)
        {
            //col的最大值等于本行的长度+1

            CarPos tmpCarPos = new CarPos();
            if (lstLineInfo.Count == 0)
            {
                return tmpCarPos;
            }

            //nCol < 0&&nLine >0
            if (nLine < 0)//下越界
            {
                nLine = 0;
            }
            else if (nLine >= LineCount)//上越界
            {
                nLine = LineCount - 1;
            }

            if (nCol < 0)
            {
                if (nLine > 0)
                {
                    nLine--;
                    nCol = lstLineInfo[nLine].Length;
                }
                else//0
                {
                    nCol = 0;
                }

            }
            else if (nCol > lstLineInfo[nLine].Length)
            {
                if (nLine < LineCount - 1)
                {
                    nLine++;
                    nCol = 0;
                }
                else
                {
                    nCol = lstLineInfo[nLine].Length;
                }

            }

            int iLastCharWidth = 0;
            int dY = 0;
            Graphics g = this.CreateGraphics();
            SizeF LasCharSize;
            string tmpStr = "";
            string strLine = lstLineInfo[nLine].Text;
            //int head =0;//= LogLineList[lstLineInfo[nLine].LogLine].GetHead();

            for (int i = 0; i < nCol; i++)
            {
                //Text .Length 
                try
                {
                    tmpStr = strLine.Substring(i, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\n" + ex.Message);
                }
                LasCharSize = g.MeasureString(tmpStr, Font, this.Height, mStringFormat);
                iLastCharWidth = Convert.ToInt32(LasCharSize.Width);
                dY += iLastCharWidth;
            }



            tmpCarPos.Line = nLine;
            tmpCarPos.Column = nCol;
            tmpCarPos.Location = new PointF(nLine * Font.Height, dY);
            return tmpCarPos;

        }


        private CarPos MouseToOffset2(Point MousePos)
        {
            CarPos tmpCarPos = new CarPos();
            if (lstLineInfo.Count == 0) return tmpCarPos;
            int tmpX = MousePos.X;
            int tmpY = MousePos.Y;
            if (tmpX < 0)
            {
                tmpX = 0;//暂不考虑越界
            }
            if (tmpY < 0)
            {
                tmpY = 0;
            }

            int line = tmpX / Font.Height;
            int col = 0;


            if (line > LineCount - 1)//上越界
            {
                line = LineCount - 1;
            }
            tmpCarPos.Line = line;

            if (tmpY <= 0 || lstLineInfo[line].Length == 0)//鼠标位置下越界
            {
                tmpCarPos.Column = 0;
                tmpCarPos.Location
                    = new PointF(tmpCarPos.Line * Font.Height, 0);
                return tmpCarPos;
            }


            string tmpStr;
            int dY = 0;
            int iLastCharWidth = 0;
            Graphics g = this.CreateGraphics();
            SizeF LasCharSize;
            int head = LogLineList[lstLineInfo[line].LogLine].GetHead(lstLineInfo[line].Index);

            string strLine = lstLineInfo[line].Text;
            while (dY < tmpY && col < lstLineInfo[line].Length)
            {
                tmpStr = strLine.Substring(col, 1);
                LasCharSize = g.MeasureString(tmpStr, Font, this.Height, mStringFormat);
                iLastCharWidth = Convert.ToInt32(LasCharSize.Width);
                dY += iLastCharWidth;
                col++;
            }


            if (tmpY > dY - iLastCharWidth / 2)
            {
                tmpCarPos.Column = col;
                tmpCarPos.Location
                    = new PointF(tmpCarPos.Line * Font.Height, dY);
            }
            else
            {
                tmpCarPos.Column = col - 1;
                tmpCarPos.Location
                    = new PointF(tmpCarPos.Line * Font.Height, dY - iLastCharWidth);
                if (tmpCarPos.Column < 0)
                {
                    MessageBox.Show("Error!\n line:1120");
                }
            }
            g.Dispose();
            return tmpCarPos;

        }

        void SetSel1(CarPos pos1, CarPos pos2)
        {
            if (pos1 == pos2)
            {

                return;
            }
            if (pos1 > pos2)
            {
                CarPos tmpPos;
                tmpPos = pos1;
                pos1 = pos2;
                pos2 = tmpPos;

            }

            Point startPos;
            Point endPos;

            if (pos1.Line == pos2.Line)
            {

                startPos = PointF2Point(pos1.Location);
                endPos = PointF2Point(pos2.Location);


                int tmpH = endPos.Y - startPos.Y;

                int tmpW = Font.Height;
                Size tmpS = new Size(tmpW, tmpH);

                Rectangle tmpR = new Rectangle(PointToScreen(startPos), tmpS);
                ControlPaint.FillReversibleRectangle(tmpR, Color.Yellow);

            }
            else
            {
                startPos = new Point(Convert.ToInt32(pos1.Location.X)
                                    , Convert.ToInt32(pos1.Location.Y));
                endPos = new Point(Convert.ToInt32(pos2.Location.X) + Font.Height
                                    , Convert.ToInt32(pos2.Location.Y));



                int tmpH = Height - startPos.Y;

                int tmpW = Font.Height;

                Size tmpS = new Size(tmpW, tmpH);

                Rectangle tmpR = new Rectangle(PointToScreen(startPos), tmpS);
                ControlPaint.FillReversibleRectangle(tmpR, Color.Yellow);
                Point tmpPoint;
                if (pos2.Line - pos1.Line > 2)
                {
                    tmpH = ClientSize.Height;
                    tmpW = Font.Height * (pos2.Line - pos1.Line - 2);
                    tmpS = new Size(tmpW, tmpH);
                    tmpPoint = new Point(startPos.X + Font.Height, 0);
                    tmpR = new Rectangle(PointToScreen(tmpPoint), tmpS);
                    ControlPaint.FillReversibleRectangle(tmpR, Color.Yellow);
                }
                tmpH = endPos.Y;
                tmpW = Font.Height;
                tmpS = new Size(tmpW, tmpH);
                tmpPoint = new Point(endPos.X - Font.Height, 0);
                tmpR = new Rectangle(PointToScreen(tmpPoint), tmpS);
                ControlPaint.FillReversibleRectangle(tmpR, Color.Yellow);
            }

        }
        private Point PointF2Point(PointF aPf)
        {
            return new Point(Convert.ToInt32(aPf.X),
               Convert.ToInt32(aPf.Y));
        }

        //flag=0返回index之前的插入符位置,//flag=1返回index之后的插入符位置
        //private CarPos Index2Carpos(int index,int flag)
        //{                                                                      
        //    if(string.IsNullOrEmpty (Text ))
        //    {
        //        return LCtoOffset(0, 0);
        //    }
        //    if (index >= Text.Length)
        //    {
        //        index = Text.Length-1;
        //        flag = 1;
        //    }
        //    if (index < 0) index = 0;
        //    Graphics g=CreateGraphics ();
        //    int tmpCount=0;
        //    int line=0;

        //    //判断当前index位于哪行
        //   for(int i=0;i<lstLineInfo .Count ;i++)
        //   {
        //       if (index < tmpCount + lstLineInfo[i].Lenth)//
        //       {
        //           line = i;
        //           break;
        //       }
        //       tmpCount += lstLineInfo[i].Lenth;
        //       //得到的tmpCount永远是index之前所有行的字符总数
        //   }

        //   return LCtoOffset(line, index - tmpCount+flag );

        //}

        private CarPos Index2Carpos2(int index, int flag)
        {
            //flag=0返回index之前的插入符位置,//flag=1返回index之后的插入符位置
            if (string.IsNullOrEmpty(Text))
            {
                return LCtoOffset2(0, 0);
            }
            //try
            //{
            //    string tmp = Text.Substring(0, index);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message );
            //}
            if (index >= Text.Length)//如果index上越界则返回最后一个字符后的位置
            {
                index = Text.Length - 1;
                flag = 1;
            }
            if (index < 0)//如果index下越界则返回最第一个字符前面的位置
            {
                index = 0;
                flag = 0;
            }

            //int head
            Point IndexConvert = Index_Text2Logline(index);
            int index_in_logLine = IndexConvert.X;
            int logLine = IndexConvert.Y;
            int tmpLength = 0;
            int line = 0;
            for (int i = 0; i < LogLineList[logLine].LineList.Count; i++)
            {
                int tmp = 0;//逻辑行中的最后一行的长度应该=Length+2
                if (LogLineList[logLine].LineList.Count - 1 == i) tmp = 2;
                if (tmp + tmpLength + LogLineList[logLine].LineList[i].Length - 1 >= index_in_logLine)
                {
                    line = i;
                    break;
                }
                tmpLength += LogLineList[logLine].LineList[i].Length;//i-1行的length
            }
            int col = index_in_logLine - tmpLength;

            for (int i = 0; i < logLine; i++)
            {
                line += LogLineList[i].LineList.Count;
            }

            //判断当前index位于哪行
            //int line =0;
            //while (index > LogLineList  [lstLineInfo[line].LogLine ].GetHead () + lstLineInfo[line].Length-1)//
            //{
            //    line ++;
            //}

            //int col = index - lstLineInfo[line].Head;
            //return LCtoOffset(line - 1, col + flag);//
            return LCtoOffset2(line, col + flag);//
        }
        int Index_Line2LogLine(int IndexInLine, int iLine)
        {
            int tmpLength = 0;

            for (int i = 0; i < lstLineInfo[iLine].Index; i++)//LogLine内的索引
            {
                tmpLength += LogLineList[lstLineInfo[iLine].LogLine].LineList[i].Length;

            }
            return tmpLength + IndexInLine;
        }
        Point Index_Text2Logline(int IndexInText)//返回（索引，逻辑行索引）
        {
            int logline = 0;
            int tmpTotalLength = 0;//
            int tmpLineLength = LogLineList[0].Length + 2;
            try
            {

                while (tmpLineLength + tmpTotalLength <= IndexInText)//+"\r\n"
                {
                    tmpTotalLength += tmpLineLength;
                    logline++;
                    tmpLineLength = LogLineList[logline].Length + 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Index_Text2Logline \n LogLine :" + logline.ToString() + ex.Message);
            }
            return new Point(IndexInText - tmpTotalLength, logline);
        }

        int Index_Logline2Text(int logLineIndex, int IndexInLogLine)
        {
            int tmp = 0;
            for (int i = 0; i < logLineIndex; i++)
            {
                tmp += LogLineList[i].Length + 2;//"\r\n"
            }

            return tmp + IndexInLogLine;
        }
        //private void  SelectionDraw(int start,int lenth)
        //{

        //}
        //private void SelectionClear(int start, int lenth)
        //{
        //}
        //private int CarPos2Index(CarPos cpPos)//插入符位置转换为text的索引位置
        //{
        //    int index=0;
        //    int i=0;
        //    if (cpPos.Line > lstLineInfo.Count - 1)
        //    {
        //        return Text.Length - 1;
        //    }
        //    for ( i = 0; i < cpPos.Line; i++)
        //    {
        //        index += lstLineInfo[i].Lenth;
        //    }
        //    if (cpPos.Column > lstLineInfo[i].Lenth)
        //    {
        //        index += lstLineInfo[i].Lenth;
        //    }
        //    else
        //    {
        //        index += cpPos.Column;
        //    }
        //    //if (index > Text.Length) index = Text.Length - 1;
        //    return index;
        //}
        //private int CarPos2Index2(CarPos cpPos)
        //{
        //    //插入符位置转换为text的索引位置,返回值为当前光标后的字符的索引
        //    //一行中插入符的位置个数等于行中的字符数量+1,插入符位置作引的最大值=字符个数
        //    //光标在一行的最下面时返回下一行第一个字符的索引
        //    //光标在文本的最后时返回值为文本的长度，注意越界！！
        //    int index = 0;
        //    int line=cpPos.Line ;
        //    int col = cpPos.Column < lstLineInfo[line].Lenth ? cpPos.Column : lstLineInfo[line].Lenth;//防止col越界

        //    if (line > lstLineInfo.Count - 1)//如果右越界则返回最后的字符的索引
        //    {
        //        return Text.Length - 1;
        //    }

        //    index = lstLineInfo[line].Head + col;

        //    return index;
        //}

        //private int CarPos2Index2(CarPos cpPos)
        //{
        //    //插入符位置转换为text的索引位置,返回值为当前光标后的字符的索引
        //    //一行中插入符的位置个数等于行中的字符数量+1,插入符位置作引的最大值=字符个数
        //    //光标在一行的最下面时返回下一行第一个字符的索引
        //    //光标在文本的最后时返回值为文本的长度，注意越界！！
        //    int index = 0;
        //    int line = cpPos.Line;
        //    if (lstLineInfo.Count == 0) return 0;
        //    int col = cpPos.Column < lstLineInfo[line].Length ? cpPos.Column : lstLineInfo[line].Length;//防止col越界

        //    if (line > lstLineInfo.Count - 1)//如果右越界则返回最后的字符的索引
        //    {
        //        return Text.Length - 1;
        //    }

        //    //index = lstLineInfo[line].Head + col;

        //    return index;
        //}

        private int CarPos2Index3(CarPos cpPos)
        {
            //插入符位置转换为text的索引位置,返回值为当前光标后的字符的索引
            //一行中插入符的位置个数等于行中的字符数量+1,插入符位置作引的最大值=字符个数
            //光标在一行的最下面时返回下一行第一个字符的索引
            //光标在文本的最后时返回值为文本的长度，注意越界！！

            int line = cpPos.Line;
            int logLine = lstLineInfo[cpPos.Line].LogLine;
            int index = Index_Logline2Text(logLine, 0);//获取本logline的第一个字符在text中的索引

            for (int i = 0; i < lstLineInfo[cpPos.Line].Index; i++)//获取光标在本logline中的索引
            {
                index += LogLineList[logLine].LineList[i].Length;
            }
            index += cpPos.Column;
            return index;
        }



        private void DrawAChar(Graphics g, int index)
        {
            g.ResetTransform();

            CarPos tmpCarPos = Index2Carpos2(index, 0);
            g.TranslateTransform(tmpCarPos.Location.X + Font.Height, tmpCarPos.Location.Y);
            g.RotateTransform(90);

            string aChar = Text.Substring(index, 1);
            SizeF sChar = g.MeasureString(aChar, Font, this.Width, mStringFormat);
            Rectangle tmpR = new Rectangle(0, 0
                       , Convert.ToInt32(sChar.Width), Font.Height);
            Brush aBrush, shadaw;

            if (isSelected(index))
            {
                g.FillRectangle(mSelectSBrush, tmpR);
                aBrush = new SolidBrush(Color.White);
                shadaw = mShadowBrush1;
            }
            else
            {
                //SolidBrush bgBrush = new SolidBrush(BackColor);
                //g.FillRectangle(bgBrush, tmpR);
                aBrush = new SolidBrush(ForeColor);
                shadaw = mShadowBrush2;
            }
            if (isShawdow)
                g.DrawString(aChar, Font, shadaw, new Point(1, 2), mStringFormat);
            g.DrawString(aChar, Font, aBrush, new Point(0, 0), mStringFormat);

            g.ResetTransform();
        }
        private int DrawAChar(Graphics g, CarPos tmpCarPos, string aChar)
        {
            g.ResetTransform();


            g.TranslateTransform(tmpCarPos.Location.X + Font.Height, tmpCarPos.Location.Y);
            g.RotateTransform(90);

            //string aChar = Text.Substring(index, 1);
            SizeF sChar = g.MeasureString(aChar, Font, this.Width, mStringFormat);
            //Rectangle tmpR = new Rectangle(0, 0
            //           , Convert.ToInt32(sChar.Width), Font.Height);
            Brush aBrush, shadaw;


            //SolidBrush bgBrush = new SolidBrush(BackColor);
            //g.FillRectangle(bgBrush, tmpR);
            aBrush = new SolidBrush(ForeColor);
            shadaw = mShadowBrush2;

            if (isShawdow)
                g.DrawString(aChar, Font, shadaw, new Point(1, 2), mStringFormat);
            g.DrawString(aChar, Font, aBrush, new Point(0, 0), mStringFormat);

            g.ResetTransform();
            return Convert.ToInt32(sChar.Width);
        }
        //bool isNewLine(int line, int width)
        //{

        //}
        //private void OnChar2(string aChar)
        //{
        //    //try
        //    //{
        //    if (isMouseDown) return;
        //    Caret.Hide();
        //    Graphics g = CreateGraphics();
        //    string tmpStr1, tmpStr2;
        //    int LenthOfNewChar=aChar.Length ;
        //    int curIndex;
        //    if (SelectionLength > 0)//如果有选择的文本，删除
        //    {

        //        curIndex = mSelectionStart;
        //        tmpStr1 = Text.Substring(0, mSelectionStart);
        //        tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
        //        mText = tmpStr1 + aChar + tmpStr2;
        //        mSelectionLength = 0;
        //        mSelectionStart = 0;
        //        mSelectionEnd = 0;
        //        ArrangeAll3(ClientRectangle.Height);
        //    }
        //    else
        //    {
        //        curIndex = CarPos2Index2(Caret.Position);

        //        //被插入的行原来的长度
        //        int tmpLenth=lstLineInfo[Caret.Position.Line].Lenth ;
        //        //被插入行的副本
        //        string strLine=mText.Substring(lstLineInfo[Caret.Position.Line].Head, tmpLenth);

        //        strLine = strLine.Insert(Caret.Position.Column, aChar);//先把内容插入到行的副本里


        //        int tmpLenthSource = strLine.Length;//需要重排文本的总长度
        //        int LenthOfNewLine = ArrangeString(strLine, ClientRectangle.Height, g);//重排此行，并得到行的长度

        //        lstLineInfo[Caret.Position.Line].Lenth = LenthOfNewLine;//长度变化，行头位置不变


        //        int tmpLineIndex = Caret.Position.Line + 1;
        //        //if (lstLineInfo.Count <= tmpLineIndex)//加行
        //        //{
        //        //    LineInformation aLineInf = new LineInformation();
        //        //    lstLineInfo.Add(aLineInf);
        //        //}
        //        while (LenthOfNewLine < tmpLenthSource)//循环，重排受影响的所有行
        //        {

        //            if (lstLineInfo.Count <= tmpLineIndex)//加行
        //            {
        //                RealLine aLineInf = new RealLine();
        //                lstLineInfo.Add(aLineInf);
        //            }

        //            string strNokori = strLine.Substring(LenthOfNewLine, strLine.Length - LenthOfNewLine);//上一行剩下的文本
        //            strLine = mText.Substring(lstLineInfo[tmpLineIndex].Head
        //                , lstLineInfo[tmpLineIndex].Lenth);//取下一行原来的文本
        //            strLine = strLine.Insert(0, strNokori);//把上一行剩下的文本插入到本行头

        //            tmpLenthSource = strLine.Length; //需要重排文本的总长度
        //            LenthOfNewLine = ArrangeString(strLine, ClientRectangle.Height, g);//重排此行，并得到行的长度


        //            lstLineInfo[tmpLineIndex].Head  //行头索引
        //                = lstLineInfo[tmpLineIndex - 1].Head
        //                + lstLineInfo[tmpLineIndex - 1].Lenth ;
        //            lstLineInfo[tmpLineIndex].Lenth  = LenthOfNewLine;//更新行的长度


        //            tmpLineIndex++;//下一行的索引
        //        }
        //        int tmpCount = tmpLineIndex - Caret.Position.Line;//需要重绘的行数
        //        while (tmpLineIndex < lstLineInfo.Count)
        //        {
        //            lstLineInfo[tmpLineIndex].Head  += aChar.Length;
        //            tmpLineIndex++;

        //        }

        //        mText=mText.Insert(curIndex, aChar);//把aChar插入到text
        //        //this.Invalidate();
        //        //tmpStr1 = Text.Substring(0, curIndex);
        //        //tmpStr2 = Text.Substring(curIndex, Text.Length - curIndex);
        //        //mText = tmpStr1 + aChar + tmpStr2;
        //        //}


        //        BufferBmp = new Bitmap(Font.Height * tmpCount, ClientRectangle.Height);
        //        Graphics bmpG = Graphics.FromImage(BufferBmp);
        //        //DrawTextWithIndex(tmpString, bmpG, false);
        //        bmpG.Clear(BackColor);
        //        //bool isContinue=true ;
        //        int j = 0;
        //        while ( j < tmpCount)
        //        {
        //             DrawALine(bmpG, Caret.Position.Line + j,Font.Height *j);
        //             j++;
        //        }
        //        //ArrangeSomeLine(Caret.Position.Line, ClientRectangle.Height);

        //        //int offset=DrawAChar(g, Caret.Position, aChar);
        //        g.DrawImage(BufferBmp, new Point(Font.Height * Caret.Position.Line, 0));

        //        //g.DrawImage(
        //    }

        //        //ReDraw();

        //        //

        //        //if (len == 1 && curIndex==mText .Length -1)
        //        //{

        //        //    DrawAChar(CreateGraphics(), curIndex);
        //        //}
        //        //else
        //        //{
        //        //    ReDraw(ClientRectangle.Width, ClientRectangle.Height);
        //        //}
        //        //ArrangeAll();
        //        //Graphics g = this.CreateGraphics();
        //        //Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        //        //Graphics bmpG = Graphics.FromImage(bmp);
        //        //bmpG.Clear(BackColor );
        //        //DrawAll(bmpG);
        //        //g.DrawImage(bmp , ClientRectangle.Location);
        //        Caret.Position = Index2Carpos2(curIndex+LenthOfNewChar-1, 1);

        //        //g.Dispose();
        //        //bmpG.Dispose();
        //        Caret.Show();
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons .OK , MessageBoxIcon.Error);
        //    //}
        //}
        //private void OnChar2(string aChar)
        //{
        //    //try
        //    //{
        //    if (isMouseDown) return;
        //    Caret.Hide();
        //    Graphics g = CreateGraphics();
        //    string tmpStr1, tmpStr2;
        //    int LenthOfNewChar = aChar.Length;
        //    int curIndex;
        //if (SelectionLength > 0)//如果有选择的文本，删除
        //{

        //    curIndex = mSelectionStart;
        //    tmpStr1 = Text.Substring(0, mSelectionStart);
        //    tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
        //    mText = tmpStr1 + aChar + tmpStr2;

        //    mSelectionLength = 0;
        //    mSelectionStart = 0;
        //    mSelectionEnd = 0;
        //    ArrangeAll3(ClientRectangle.Height);
        //}
        //else//没有选择的文本
        //{
        //    curIndex = CarPos2Index2(Caret.Position);

        //被插入的行原来的长度
        //int tmpLenth = lstLineInfo[Caret.Position.Line].Lenth;
        //被插入行的副本
        //string strLine = mText.Substring(lstLineInfo[Caret.Position.Line].Head, tmpLenth);

        //strLine = strLine.Insert(Caret.Position.Column, aChar);//先把内容插入到行的副本里


        //    int tmpLenthSource = strLine.Length;//需要重排文本的总长度
        //    int LenthOfNewLine = ArrangeString(strLine, ClientRectangle.Height, g);//重排此行，并得到行的长度

        //    lstLineInfo[Caret.Position.Line].Lenth = LenthOfNewLine;//长度变化，行头位置不变


        //    int tmpLineIndex = Caret.Position.Line + 1;
        //    //if (lstLineInfo.Count <= tmpLineIndex)//加行
        //    //{
        //    //    LineInformation aLineInf = new LineInformation();
        //    //    lstLineInfo.Add(aLineInf);
        //    //}
        //    while (LenthOfNewLine < tmpLenthSource)//循环，重排受影响的所有行
        //    {

        //        if (lstLineInfo.Count <= tmpLineIndex)//加行
        //        {
        //            RealLine aLineInf = new RealLine();
        //            lstLineInfo.Add(aLineInf);
        //        }

        //        string strNokori = strLine.Substring(LenthOfNewLine, strLine.Length - LenthOfNewLine);//上一行剩下的文本
        //        strLine = mText.Substring(lstLineInfo[tmpLineIndex].Head
        //            , lstLineInfo[tmpLineIndex].Lenth);//取下一行原来的文本
        //        strLine = strLine.Insert(0, strNokori);//把上一行剩下的文本插入到本行头

        //        tmpLenthSource = strLine.Length; //需要重排文本的总长度
        //        LenthOfNewLine = ArrangeString(strLine, ClientRectangle.Height, g);//重排此行，并得到行的长度


        //        lstLineInfo[tmpLineIndex].Head  //行头索引
        //            = lstLineInfo[tmpLineIndex - 1].Head
        //            + lstLineInfo[tmpLineIndex - 1].Lenth;
        //        lstLineInfo[tmpLineIndex].Lenth = LenthOfNewLine;//更新行的长度


        //        tmpLineIndex++;//下一行的索引
        //    }
        //    int tmpCount = tmpLineIndex - Caret.Position.Line;//需要重绘的行数
        //    while (tmpLineIndex < lstLineInfo.Count)
        //    {
        //        lstLineInfo[tmpLineIndex].Head += aChar.Length;
        //        tmpLineIndex++;

        //    }

        //mText = mText.Insert(curIndex, aChar);//把aChar插入到text
        //    //this.Invalidate();
        //    //tmpStr1 = Text.Substring(0, curIndex);
        //    //tmpStr2 = Text.Substring(curIndex, Text.Length - curIndex);
        //    //mText = tmpStr1 + aChar + tmpStr2;
        //    //}


        //    BufferBmp = new Bitmap(Font.Height * tmpCount, ClientRectangle.Height);
        //    Graphics bmpG = Graphics.FromImage(BufferBmp);
        //    //DrawTextWithIndex(tmpString, bmpG, false);
        //    bmpG.Clear(BackColor);
        //    //bool isContinue=true ;
        //    int j = 0;
        //    while (j < tmpCount)
        //    {
        //        DrawALine(bmpG, Caret.Position.Line + j, Font.Height * j);
        //        j++;
        //    }
        //    //ArrangeSomeLine(Caret.Position.Line, ClientRectangle.Height);

        //    //int offset=DrawAChar(g, Caret.Position, aChar);
        //    g.DrawImage(BufferBmp, new Point(Font.Height * Caret.Position.Line, 0));

        //    //g.DrawImage(
        //}

        ////ReDraw();

        ////

        ////if (len == 1 && curIndex==mText .Length -1)
        ////{

        ////    DrawAChar(CreateGraphics(), curIndex);
        ////}
        ////else
        ////{
        ////    ReDraw(ClientRectangle.Width, ClientRectangle.Height);
        ////}
        ////ArrangeAll();
        ////Graphics g = this.CreateGraphics();
        ////Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        ////Graphics bmpG = Graphics.FromImage(bmp);
        ////bmpG.Clear(BackColor );
        ////DrawAll(bmpG);
        ////g.DrawImage(bmp , ClientRectangle.Location);
        //Caret.Position = Index2Carpos2(curIndex + LenthOfNewChar - 1, 1);

        //Caret.Show();
        //    }
        //}
        private void OnKeyDown(int KeyCode, int KeyState)
        {
            CarPos aCarPos = Caret.Position;
            int curIndex = 0;//光标的索引位置
            switch (KeyCode)
            {
                case 8://退格

                    if (String.IsNullOrEmpty(Text)) return;

                    string tmpStr1 = "";
                    string tmpStr2 = "";

                    int CarPosIndex = 0;
                    if (SelectionLength > 0)//如果有选择的文本，删除
                    {
                        CarPosIndex = mSelectionStart;
                        //Caret.Position = Index2Carpos(mSelectionStart, 0);
                        tmpStr1 = Text.Substring(0, mSelectionStart);
                        tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
                        Text = tmpStr1 + tmpStr2;
                        mSelectionLength = 0;
                        mSelectionStart = 0;
                        mSelectionEnd = 0;

                    }
                    else //没有选择的文本，删除
                    {

                        curIndex = CarPos2Index3(Caret.Position);
                        if (curIndex == 0) return;//curIndex=0什么都不做
                        int k = 1;
                        if (curIndex > 1)
                        {
                            string tmpStr = Text.Substring(curIndex - 2, 2);
                            if (tmpStr == "\r\n")//有换行符
                            {
                                k = 2;

                            }//有换行符

                        }//(curIndex > 0)

                        CarPosIndex = curIndex - k;

                        tmpStr1 = Text.Substring(0, curIndex - k);
                        if (curIndex < Text.Length)
                        {
                            tmpStr2 = Text.Substring(curIndex, Text.Length - curIndex);
                        }
                        Text = tmpStr1 + tmpStr2;
                    }
                    Caret.Hide();
                    //ArrangeAll3(ClientRectangle.Height - 5);

                    //ReDraw(ClientRectangle.Width, ClientRectangle.Height);

                    Caret.Position = Index2Carpos2(CarPosIndex, 0);
                    //int tmpEnd1 = (ClientRectangle.Width / Font.Height);
                    //int tmpEnd2 = lstLineInfo.Count - 1;
                    //int endLine = tmpEnd1 < tmpEnd2 ? tmpEnd1 : tmpEnd2;
                    //int len = endLine - Caret.Position.Line + 2;

                    //Bitmap bmp = new Bitmap(len * Font.Height, ClientRectangle.Height);
                    //Graphics bmpG = Graphics.FromImage(bmp);
                    //bmpG.Clear(BackColor);
                    //RePaintSomeLine(Caret.Position.Line, len, bmpG);

                    //Graphics g = CreateGraphics();
                    //g.DrawImage(bmp, new Point((int)Caret.Position.Location.X ,0));

                    //bmp.Dispose();
                    //bmpG.Dispose();
                    //g.Dispose();
                    Caret.Show();






                    break;
                case 46:
                    Delete();

                    break;
                case 13://回车
                    //ascii_code = Convert.ToChar(KeyCode); //获取按下的ASCII码
                    //key_state = m.LParam.ToInt32(); //获取按下的键状态
                    OnKey_RETURN();
                    break;
                case 37://左
                    Caret.Position = LCtoOffset2(aCarPos.Line - 1
                                           , aCarPos.Column);
                    if (mSelectionLength > 0)
                    {
                        mSelectionLength = 0;
                        mSelectionStart = 0;
                        mSelectionEnd = 0;
                        Caret.Hide();
                        //ArrangeAll2(ClientRectangle.Width, ClientRectangle.Height);
                        ReDraw(ClientRectangle.Width, ClientRectangle.Height);

                        Caret.Show();
                    }


                    break;
                case 38://上
                    Caret.Position = LCtoOffset2(aCarPos.Line, aCarPos.Column - 1);
                    if (mSelectionLength > 0)
                    {
                        mSelectionLength = 0;
                        mSelectionStart = 0;
                        mSelectionEnd = 0;
                        Caret.Hide();

                        ReDraw(ClientRectangle.Width, ClientRectangle.Height);

                        Caret.Show();
                    }

                    break;
                case 39://右
                    Caret.Position = LCtoOffset2(aCarPos.Line + 1
                                           , aCarPos.Column);
                    if (mSelectionLength > 0)
                    {
                        mSelectionLength = 0;
                        mSelectionStart = 0;
                        mSelectionEnd = 0;
                        Caret.Hide();

                        ReDraw(ClientRectangle.Width, ClientRectangle.Height);

                        Caret.Show();
                    }
                    break;
                case 40://下
                    Caret.Position = LCtoOffset2(aCarPos.Line
                                           , aCarPos.Column + 1);
                    if (mSelectionLength > 0)
                    {
                        mSelectionLength = 0;
                        mSelectionStart = 0;
                        mSelectionEnd = 0;
                        Caret.Hide();

                        ReDraw(ClientRectangle.Width, ClientRectangle.Height);

                        Caret.Show();
                    }

                    break;

                default:
                    break;
            }


        }
        private void OnKey_RETURN()
        {
            string tmpStr1 = "";
            string tmpStr2 = "";

            int CarPosIndex = 0;//光标位置的索引
            CarPos aCarPos = Caret.Position;
            if (SelectionLength > 0)//如果有选择的文本，删除
            {
                CarPosIndex = mSelectionStart;
                //Caret.Position = Index2Carpos(mSelectionStart, 0);
                tmpStr1 = Text.Substring(0, mSelectionStart);
                tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
                Text = tmpStr1 + "\r\n" + tmpStr2;
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;

            }
            else //没有选择的文本，删除
            {

                CarPosIndex = CarPos2Index3(Caret.Position);

                tmpStr1 = Text.Substring(0, CarPosIndex);
                if (CarPosIndex < Text.Length)
                {
                    tmpStr2 = Text.Substring(CarPosIndex, Text.Length - CarPosIndex);
                }
                Text = tmpStr1 + "\r\n" + tmpStr2;
            }

            //ArrangeAll3(ClientRectangle.Height - 5);

            //ReDraw(ClientRectangle.Width, ClientRectangle.Height);



            //int tmpEnd1 = (ClientRectangle.Width / Font.Height);
            //int tmpEnd2 = lstLineInfo.Count - 1;
            //int endLine = tmpEnd1 < tmpEnd2 ? tmpEnd1 : tmpEnd2;
            //int len = endLine - Caret.Position.Line + 2;

            //Bitmap bmp = new Bitmap(len * Font.Height, ClientRectangle.Height);
            //Graphics bmpG = Graphics.FromImage(bmp);
            //bmpG.Clear(BackColor);
            //RePaintSomeLine(Caret.Position.Line, len, bmpG);

            //Graphics g = CreateGraphics();
            //g.DrawImage(bmp, new Point((int)Caret.Position.Location.X, 0));

            //bmp.Dispose();
            //bmpG.Dispose();
            //g.Dispose();
            aCarPos.ToNewLine(Font.Height);
            Caret.Hide();
            Caret.Position = aCarPos;
            Caret.Show();
        }
        private void OnPaintEx(Graphics g, Rectangle Rect)
        {

            //if (Rect.Width == 0 || Rect.Height  == 0) return;
            //rect.
            //if (newHeight < 100)
            //{
            //    int newTop = rect._Top;
            //    int newLeft = rect._Left;
            //}

            int lStart = Rect.Left / Font.Height;//需要重绘的行的开始索引
            int lCount = (Rect.Width + Rect.Left) / Font.Height - lStart + 1;//需要重绘的行数
            Bitmap bmp = new Bitmap(Font.Height * lCount, ClientRectangle.Height);
            Graphics bmpG = Graphics.FromImage(bmp);
            bmpG.Clear(BackColor);
            RePaintSomeLine(lStart, lCount, bmpG);
            g.DrawImage(bmp, new PointF((float)Font.Height * lStart, 0f));
            bmp.Dispose();
        }

        //Caret.Position = Index2Carpos(i, 0);
        private void OnChar(string aChar)
        {
            //try
            //{
            if (isMouseDown) return;
            //string tmpStr1, tmpStr2;


            ClearSelection();
            int CarPosIndex = CarPos2Index3(Caret.Position);
            int len = aChar.Length;


            int WidthOfChar = CSLogLine.MeasureString(aChar, CreateGraphics(), Font, mStringFormat);



            //=SearchWord(Caret.Position .Line ,Caret .Position .Column );
            CSWord tmpWord = lstLineInfo[Caret.Position.Line].HeadWord.Next;
            if (tmpWord == lstLineInfo[Caret.Position.Line].TailWord)//第一次
            {
                tmpWord = new CSWord();
                tmpWord.Last = lstLineInfo[Caret.Position.Line].HeadWord;
                lstLineInfo[Caret.Position.Line].HeadWord.Next = tmpWord;

                tmpWord.Next = lstLineInfo[Caret.Position.Line].TailWord;
                lstLineInfo[Caret.Position.Line].TailWord.Last = tmpWord;
            }
            int tmpLenth = 0;
            //int count = 0;
            while (tmpLenth + tmpWord.Length < Caret.Position.Column)//tmpLenth是这个字之前所有字的总长度
            {
                tmpLenth += tmpWord.Length;
                tmpWord = tmpWord.Next;
                //count++;
            }
            CSWord LastWord = tmpWord.Last;//第一次是HeadWord
            CSWord NextWord = tmpWord.Next;//第一次是TailWord
            CSWord CurWordPoint;
            string tmpString = "";//临时的字符串，
            int tmpPos;//插入的位置
            if (Caret.Position.Column == 0)
            {
                tmpString = aChar + tmpWord.Text;
            }
            else if (Caret.Position.Column == lstLineInfo[Caret.Position.Line].Length)
            {
                tmpString = tmpWord.Text + aChar;
            }
            else if (tmpLenth + tmpWord.Length == Caret.Position.Column)//当前位置在两个字之间,tmpWord是当前插入符上第一个字符
            {
                //LastWord = tmpWord.Last;
                tmpString = tmpWord.Text + aChar;
                if (NextWord != null)
                {
                    LastWord = tmpWord.Last;
                    tmpString += NextWord.Text;
                    NextWord = NextWord.Next;
                    //NextWord = tmpWord.Next ;
                }
                //tmpWord .Word +tmpWord .Word 
            }
            else//当前位置在一个字的里面
            {
                LastWord = tmpWord.Last;
                NextWord = tmpWord.Next;

                //tmpLenth此时等于当前插入符位置上的第一个字在本行的索引位置；

                tmpString = tmpWord.Text;
                tmpPos = Caret.Position.Column - tmpLenth;
                tmpString = tmpString.Insert(tmpPos, aChar);
            }


            CSWord aNewWord = CSLogLine.GetAWord(tmpString, RealLine.StandardHeight
                , CreateGraphics(), Font, mStringFormat);
            tmpString = tmpString.Substring(aNewWord.Length, tmpString.Length - aNewWord.Length);
            //if (string.IsNullOrEmpty(aNewWord.Text))
            //{
            //    string s = "  ";
            //}
            //bool isTailChange = false;
            //if (Caret.Position.Column >= lstLineInfo[Caret.Position.Line].Lenth - lstLineInfo[Caret.Position.Line].TailWord.Lenth)
            //    isTailChange = true;
            //if (Caret.Position.Column <= lstLineInfo[Caret.Position.Line].HeadWord.Lenth)//插入字符影响行头??????????????????????????
            //{
            //    lstLineInfo[Caret.Position.Line].HeadWord = aNewWord;
            //}

            if (LastWord != null)
            {
                LastWord.Next = aNewWord;
                aNewWord.Last = LastWord;
            }
            //if (tmpLenth == 0)//本行的第一个字
            //{
            //    lstLineInfo[Caret.Position.Line].HeadWord = aNewWord;
            //}
            //else if (tmpLenth + lstLineInfo[Caret.Position.Line].TailWord == lstLineInfo[Caret.Position.Line].Lenth )
            //{

            //}
            //else
            //{

            //}

            CurWordPoint = aNewWord;
            while (tmpString.Length > 0)
            {
                aNewWord = CSLogLine.GetAWord(tmpString, RealLine.StandardHeight
                , CreateGraphics(), Font, mStringFormat);
                tmpString = tmpString.Substring(aNewWord.Length, tmpString.Length - aNewWord.Length);
                CurWordPoint.Next = aNewWord;
                aNewWord.Last = CurWordPoint;
                CurWordPoint = aNewWord;

            }

            //CurWordPoint.Next = aNewWord;
            //aNewWord.Last = CurWordPoint;
            //CurWordPoint = aNewWord;
            aNewWord.Next = NextWord;
            if (NextWord != null)
            {
                try
                {
                    NextWord.Last = aNewWord;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            int hhh = lstLineInfo[Caret.Position.Line].Height;
            Graphics g = this.CreateGraphics();
            //lstLineInfo[Caret.Position.Line].Height += WidthOfChar;
            if (lstLineInfo[Caret.Position.Line].Height > RealLine.StandardHeight)
            {
                //需要换行的场合
                ////不处理Tail直接从本行开始重排行
                //  Invalidate();
                bool flag = false;//是否影响其他逻辑行
                int j = lstLineInfo[Caret.Position.Line].Index;
                //CSLogLine aLogline = LogLineList[lstLineInfo[Caret.Position.Line].LogLine];
                List<RealLine> tmpLineList = LogLineList[lstLineInfo[Caret.Position.Line].LogLine].LineList;
                try
                {
                    CSWord last = tmpLineList[j].Push(null);
                    j++;
                    while (last != null)//&& j < tmpLineList.Count )
                    {

                        if (j == tmpLineList.Count)//需要增加新行
                        {
                            RealLine aRealLine = new RealLine(lstLineInfo[Caret.Position.Line].LogLine, j);
                            tmpLineList.Add(aRealLine);
                            aRealLine.HeadWord = tmpLineList[j - 1].TailWord;
                            aRealLine.HeadWord.Next = aRealLine.TailWord;
                            aRealLine.TailWord.Last = aRealLine.HeadWord;
                            //int tmpNewLine=0;//

                            //for(int i=0;i<=lstLineInfo[Caret.Position.Line].LogLine;i++)
                            //{
                            //    tmpNewLine+=LogLineList [i].LineList .Count ;
                            //}
                            flag = true;
                            //lstLineInfo.Insert(tmpNewLine, aRealLine);
                        }
                        last = tmpLineList[j].Push(last);
                        int h = tmpLineList[j].Height;

                        j++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //RealLine =CSLogLine.ArrangeALine (
                    //tmpLineList.Add(new RealLine );
                }

                if (flag)
                {
                    lstLineInfo.Clear();
                    for (int i = 0; i < LogLineList.Count; i++)
                    {
                        for (int kk = 0; kk < LogLineList[i].LineList.Count; kk++)
                        {
                            lstLineInfo.Add(LogLineList[i].LineList[kk]);
                            MinContextWidth = lstLineInfo.Count * Font.Height;
                        }
                    }

                    Invalidate();
                }
                else
                {
                    Caret.Hide();
                    //lstLineInfo[Caret.Position.Line].Height += WidthOfChar;
                    //lstLineInfo[Caret.Position.Line].Lenth += aChar.Length;
                    int count = j - lstLineInfo[Caret.Position.Line].Index + 1;
                    BufferBmp = new Bitmap(Font.Height * count, ClientRectangle.Height);
                    Graphics bmpG = Graphics.FromImage(BufferBmp);
                    bmpG.Clear(BackColor);
                    for (int i = 0; i < count; i++)
                    {
                        DrawALine2(bmpG, Caret.Position.Line + i, i * Font.Height);
                    }
                    g.DrawImage(BufferBmp, new Point(Caret.Position.Line * Font.Height, 0));
                }
                //Invalidate();

            }
            else//不用换行的场合
            {
                Caret.Hide();

                //lstLineInfo[Caret.Position.Line].Lenth += aChar.Length;
                BufferBmp = new Bitmap(Font.Height, ClientRectangle.Height);
                Graphics bmpG = Graphics.FromImage(BufferBmp);
                bmpG.Clear(BackColor);
                DrawALine2(bmpG, Caret.Position.Line, 0);

                g.DrawImage(BufferBmp, new Point(Caret.Position.Line * Font.Height, 0));
            }

            //tmpWord.Lenth += aChar.Length;
            //tmpWord.Height += WidthOfChar; 
            //ReDraw();

            //ArrangeAll3( ClientRectangle.Height);
            //ReDraw(ClientRectangle.Width, ClientRectangle.Height);
            //ArrangeAll();
            //Graphics g = this.CreateGraphics();
            //Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            //Graphics bmpG = Graphics.FromImage(bmp);
            //bmpG.Clear(BackColor );
            //DrawAll(bmpG);
            //g.DrawImage(bmp , ClientRectangle.Location);
            //LCtoOffset2(Caret.Position.Line, Caret.Position.Column + len);
            //Caret.Position = LCtoOffset2(Caret.Position.Line, Caret.Position.Column + len);

            Caret.Position = Index2Carpos2(CarPosIndex + len - 1, 1);

            //Invalidate(
            //g.Dispose();
            //bmpG.Dispose();
            Caret.Show();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons .OK , MessageBoxIcon.Error);
            //}
        }
        private CSWord SearchWord(int nLine, int nCol)
        {
            int tmpLenth = 0;
            CSWord tmpWord = lstLineInfo[nLine].HeadWord;
            while (tmpLenth + tmpWord.Length < nCol)
            {

                tmpLenth += tmpWord.Length;
                if (tmpWord.Next == null) { MessageBox.Show("NULL"); break; }
                tmpWord = tmpWord.Next;
            }
            return tmpWord;
        }
        private void OnMouseMove(Point MousePoint)
        {

            if (isMouseDown && !String.IsNullOrEmpty(Text))
            {
                CarPos oldCarPos = MouseToOffset2(oldMousePos);

                //Caret.Hide();
                CarPos newCarPos = MouseToOffset2(MousePoint);
                Caret.Position = newCarPos;
                oldMousePos = MousePoint;
                if (oldCarPos == newCarPos) return;

                int tmp = CarPos2Index3(newCarPos);
                if (tmp < oldMouseIndex)
                {
                    mSelectionStart = tmp;
                    mSelectionEnd = oldMouseIndex;
                }
                else
                {
                    mSelectionStart = oldMouseIndex;
                    mSelectionEnd = tmp;
                }

                SelectionLength = mSelectionEnd - mSelectionStart;
                int start, len;
                //CarPos endCarPos,startCarPos;
                if (oldCarPos < newCarPos)
                {
                    start = oldCarPos.Line;
                    len = newCarPos.Line - start;

                }
                else
                {
                    start = newCarPos.Line;
                    len = oldCarPos.Line - start;
                }
                //ReDraw();
                //Graphics g = this.CreateGraphics();
                //Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                //Graphics bmpG = Graphics.FromImage(bmp);
                ////Rectangle r = new Rectangle(0, 0, 200, 30);
                ////g.FillRectangle(Brushes.White, r);
                ////g.DrawString(MousePoint.ToString (),Font ,Brushes.Black ,ClientRectangle .Location );
                ////LineCount = endCarPos - startCarPos+1;

                //Caret.Hide();
                //for (int j = 0; j < l; j++)
                //{
                //    DrawTextWithIndex(i + j, 1, bmpG);
                //}
                //g.DrawImage(bmp,ClientRectangle .Location );
                //g.Dispose();
                //bmpG.Dispose();


                //Caret.Show();
                //Invalidate();
                Bitmap bmp = new Bitmap(Font.Height * (len + 1), ClientRectangle.Height);
                Graphics bmpG = Graphics.FromImage(bmp);
                Graphics g = this.CreateGraphics();

                bmpG.Clear(BackColor);

                for (int i = 0; i < len + 1; i++)
                {

                    DrawALine2(bmpG, start + i, Font.Height * i);

                }
                Caret.Hide();
                g.DrawImage(bmp, Font.Height * start, 0);
                //Invalidate();
                Caret.Show();
            }
        }
        private void OnLMouseUp(Point MousePoint)
        {
            Capture = false;
            isMouseDown = false;

        }
        private void OnLMouseDown(Point MousePoint)
        {
            isMouseDown = true;
            this.Capture = true;
            CarPos aCarPos;
            if (mSelectionLength > 0)
            {
                //Caret.Hide();
                //int lenth = mSelectionLength;
                //int start=mSelectionStart ;

                CarPos startPos = Index2Carpos2(mSelectionStart - 1, 0);
                CarPos endPos = Index2Carpos2(mSelectionEnd, 0);
                mSelectionLength = 0;
                mSelectionEnd = 0;
                mSelectionStart = 0;

                Bitmap bmp = new Bitmap(Font.Height * (endPos.Line - startPos.Line + 1), ClientRectangle.Height);
                Graphics bmpG = Graphics.FromImage(bmp);
                Graphics g = this.CreateGraphics();



                bmpG.Clear(BackColor);
                int l = endPos.Line - startPos.Line + 1;
                int l2 = ClientRectangle.Width / Font.Height + 1;
                l = l2 < l ? l2 : l;
                for (int i = 0; i < l; i++)
                {

                    DrawALine2(bmpG, startPos.Line + i, Font.Height * i);

                }
                Caret.Hide();
                g.DrawImage(bmp, Font.Height * startPos.Line, 0);
                //Invalidate();
                Caret.Show();

                //Caret.Show();
            }
            oldMousePos = MousePoint;//选择文本时记录旧的鼠标位置

            aCarPos = MouseToOffset2(MousePoint);

            Caret.Position = aCarPos;
            cpSelStart = aCarPos;

            mSelectionStart = CarPos2Index3(aCarPos);
            oldMouseIndex = mSelectionStart;
            mSelectionEnd = mSelectionStart;
            mSelectionLength = 0;
            //




            //Caret.Show();
            if (!Focused) this.Focus();


        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hScrollBar = new HScrollBar();
            this.mPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mPopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mPopMenu
            // 
            this.mPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripSeparator1,
            this.toolStripMenuItem4});
            this.mPopMenu.Name = "contextMenuStrip1";
            this.mPopMenu.Size = new System.Drawing.Size(146, 142);
            this.mPopMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mPopMenu_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem1.Text = "撤销";
            this.toolStripMenuItem1.Visible = false;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem2.Text = "复制";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem3.Text = "粘贴";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItem5.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem5.Text = "剪切";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem6.Text = "删除";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStripMenuItem4.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem4.Text = "全选";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            this.mPopMenu.ResumeLayout(false);
            //
            //hScrollBar;
            //
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Dock = DockStyle.Bottom;
            //
            this.ResumeLayout(false);
            //try
            //{
            //    //this.Cursor = 
            //     Cursor s=   new Cursor(GetType(),);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message );
            //}
        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        public new void Copy()
        {
            if (string.IsNullOrEmpty(SelectedText)) return;
            //Clipboard .SetData (SelectedText,
            Clipboard.SetDataObject(SelectedText);

        }
        public new void Paste()
        {
            IDataObject iData = Clipboard.GetDataObject();

            // Determines whether the data is in a format you can use.
            if (iData.GetDataPresent(DataFormats.Text))
            {
                // Yes it is, so display it in a text box.
                string StrBuffer;
                StrBuffer = (String)iData.GetData(DataFormats.Text);
                if (string.IsNullOrEmpty(StrBuffer)) return;
                Insert(StrBuffer);
            }
        }
        public new void SelectAll()
        {
            if (string.IsNullOrEmpty(Text)) return;
            mSelectionStart = 0;
            mSelectionLength = Text.Length;
            mSelectionEnd = Text.Length;
            //RePaint2(this.CreateGraphics());

            Graphics tmpG = this.CreateGraphics();
            Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics bmpG = Graphics.FromImage(bmp);
            bmpG.Clear(BackColor);
            RePaint2(bmpG);
            tmpG.DrawImage(bmp, ClientRectangle.Location);
            tmpG.Dispose();
            bmpG.Dispose();
            bmp.Dispose();
        }
        public new void Cut()
        {
            if (string.IsNullOrEmpty(SelectedText)) return;
            //Clipboard .SetData (SelectedText,
            Clipboard.SetDataObject(SelectedText);

            string tmpStr1, tmpStr2;
            Caret.Hide();
            int CarPosIndex = 0;
            if (SelectionLength > 0)//如果有选择的文本，删除
            {
                CarPosIndex = mSelectionStart;
                //Caret.Position = Index2Carpos(mSelectionStart, 0);
                tmpStr1 = Text.Substring(0, mSelectionStart);
                tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
                Text = tmpStr1 + tmpStr2;
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;

            }

            Caret.Position = Index2Carpos2(CarPosIndex, 0);
            Caret.Show();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {//复制
            Copy();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {//粘贴
            Paste();
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {//全选

            SelectAll();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {//剪切

            Cut();

        }
        public void ClearSelection()
        {
            string tmpStr1 = "";
            string tmpStr2 = "";
            int CarPosIndex = 0;

            if (SelectionLength > 0)//如果有选择的文本，删除
            {
                Caret.Hide();
                CarPosIndex = mSelectionStart;
                //Caret.Position = Index2Carpos(mSelectionStart, 0);
                tmpStr1 = Text.Substring(0, mSelectionStart);
                tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
                Text = tmpStr1 + tmpStr2;
                Caret.Position = Index2Carpos2(CarPosIndex, 0);
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;
                Caret.Show();

            }
        }

        public void Delete()
        {
            //if (string.IsNullOrEmpty(SelectedText)) return;
            //Clipboard .SetData (SelectedText,
            //Clipboard.SetDataObject(SelectedText);
            Caret.Hide();
            string tmpStr1 = "";
            string tmpStr2 = "";
            int CarPosIndex = 0;
            if (SelectionLength > 0)//如果有选择的文本，删除
            {
                CarPosIndex = mSelectionStart;
                //Caret.Position = Index2Carpos(mSelectionStart, 0);
                tmpStr1 = Text.Substring(0, mSelectionStart);
                tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
                Text = tmpStr1 + tmpStr2;
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;

            }
            else
            {
                CarPosIndex = CarPos2Index3(Caret.Position);

                tmpStr1 = Text.Substring(0, CarPosIndex);
                int len = 1;//要删除的字符数

                if (Text.Length > CarPosIndex + 1)
                {
                    if (Text.Substring(CarPosIndex, 2) == "\r\n")
                        len = 2;
                    tmpStr2 = Text.Substring(CarPosIndex + len, Text.Length - CarPosIndex - len);
                }
                Text = tmpStr1 + tmpStr2;
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;
            }

            //Caret.Hide();
            //ArrangeAll3(ClientRectangle.Height - 5);
            //ReDraw(ClientRectangle.Width, ClientRectangle.Height);
            Caret.Position = Index2Carpos2(CarPosIndex, 0);
            Caret.Show();
        }
        public void Insert(string newString)
        {
            string tmpStr1 = "";
            string tmpStr2 = "";
            int CarPosIndex = 0;
            if (SelectionLength > 0)//如果有选择的文本，删除
            {
                CarPosIndex = mSelectionStart;
                //Caret.Position = Index2Carpos(mSelectionStart, 0);
                tmpStr1 = Text.Substring(0, mSelectionStart);
                tmpStr2 = Text.Substring(mSelectionEnd, Text.Length - mSelectionEnd);
                Text = tmpStr1 + newString + tmpStr2;
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;

            }
            else
            {
                CarPosIndex = CarPos2Index3(Caret.Position);

                tmpStr1 = Text.Substring(0, CarPosIndex);
                if (Text.Length > CarPosIndex + 1)
                    tmpStr2 = Text.Substring(CarPosIndex, Text.Length - CarPosIndex);
                Text = tmpStr1 + newString + tmpStr2;
                mSelectionLength = 0;
                mSelectionStart = 0;
                mSelectionEnd = 0;
            }
            Caret.Position = Index2Carpos2(CarPosIndex + newString.Length, 0);
            Caret.Show();
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {//删除
            Delete();
        }
        private void mPopMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {


            if (mSelectionLength > 0)
            {
                toolStripMenuItem2.Enabled = true;
                toolStripMenuItem5.Enabled = true;
                toolStripMenuItem6.Enabled = true;
            }
            else
            {
                toolStripMenuItem2.Enabled = false;
                toolStripMenuItem5.Enabled = false;
                toolStripMenuItem6.Enabled = false;
            }
            toolStripMenuItem3.Enabled = !ReadOnly;
            toolStripMenuItem5.Enabled = !ReadOnly;
            toolStripMenuItem6.Enabled = !ReadOnly;

        }
        ////public static int Measure()
        ////{
        ////    //Graphics g= this.CreateGraphics();
        ////}
    }


}
