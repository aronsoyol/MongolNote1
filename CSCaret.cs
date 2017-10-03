using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace MongolNote
{

    /// <summary>
    /// �����
    /// </summary>
    public sealed class CSCaret : System.ComponentModel.Component, IDisposable
    {
        #region Native
        private const string User32 = "User32.dll";

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }


        /// <summary>
        /// ����һ���µ�ϵͳ�����������������ָ���Ĵ���
        /// </summary>
        /// <param name="hWnd">���ھ��</param>
        /// <param name="hBitmap">�����������һ��λͼ�ľ��.������0��1;�����������,�������ͨ��width��height��������.����Ϊ1�����²�����Ի�ɫ��ʾ;�����Ǵ�ͳ�ĺ�ɫ</param>
        /// <param name="nWidth">��������</param>
        /// <param name="nHeight">��������</param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        /// <remarks>ֻ��һ�����ڵõ��������뽹��򼤻�ʱ���ܸ��䴴�������,��ʧȥ�������뽹��ǰӦ���ٲ����</remarks>
        [DllImport(User32)]
        private static extern bool CreateCaret(IntPtr hWnd, bool hBitmap, int nWidth, int nHeight);

        /// <summary>
        /// ʹ����������ڴ��ڵ��ڲ����λ����,����������ֺ󼴻��Զ���˸
        /// </summary>
        /// <param name="hWnd">���ھ��</param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        /// <remarks>ֻ�д����Լ�ӵ�в�������Ҳ�������Լ�����״�����Ҳ�������ʱ�Ż����,HideCaret��Ч�����ӵģ�����˵��������ڳ����е���3��HideCaret���Ǿͱ����ٵ���3��ShowCaret </remarks>
        [DllImport(User32)]
        private static extern bool ShowCaret(IntPtr hWnd);

        /// <summary>
        /// ���ز����,��������غ�û���ƻ�������״�Ͳ����λ�� 
        /// </summary>
        /// <param name="hWnd">���ھ��</param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        [DllImport(User32)]
        private static extern bool HideCaret(IntPtr hWnd);

        /// <summary>
        /// �õ��Ժ���Ϊ��λ�Ĳ������˸��� 
        /// </summary>
        /// <returns>�ɹ�������˸���;ʧ�ܷ���0</returns>
        [DllImport(User32)]
        private static extern uint GetCaretBlinkTime();

        /// <summary>
        /// �Ժ���Ϊ��λ�����ò������˸��� 
        /// </summary>
        /// <param name="milliseconds">�������˸���</param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        [DllImport(User32)]
        private static extern bool SetCaretBlinkTime(uint milliseconds);

        /// <summary>
        /// ����ָ�����ڵĲ����
        /// </summary>
        /// <param name="hWnd">���ھ��</param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        /// <remarks>ֻ�д���ӵ�в����ʱ��������,����ú�������������FALSE</remarks>
        [DllImport(User32)]
        private static extern bool DestroyCaret(IntPtr hWnd);

        /// <summary>
        /// �ƶ��������ָ��λ��
        /// </summary>
        /// <param name="x">X����</param>
        /// <param name="y">Y����</param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        /// <remarks>���ܲ�����Ƿ�ɼ�,�˺��������ƶ��������λ��</remarks>
        [DllImport(User32)]
        private static extern bool SetCaretPos(int x, int y);


        /// <summary>
        /// �õ��������λ��
        /// </summary>
        /// <param name="ppt">ָ��POINT�ṹ�Ĳ����λ�� </param>
        /// <returns>�ɹ���������True;ʧ�ܷ���False</returns>
        [DllImport(User32)]
        private static extern bool GetCaretPos(out POINT ppt);


        #endregion

        private Control _HostControl;
        private CarPos mPos;
        private bool visible;
        private CSCaret() 
        { 
            mPos = new CarPos(); 
        }
        
        /// <summary>
        /// ����һ�������
        /// </summary>
        /// <param name="ctl">���������</param>
        /// <remarks>������ɺ�����Ĭ��Ϊ���ص�</remarks>
        /// 
        public CSCaret(Control ctl) : this(ctl, 8, 16) {
            mPos = new CarPos();
        }

        /// <summary>
        /// ʹ���ض��Ĵ�С����һ�������
        /// </summary>
        /// <param name="ctl">����</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <remarks>������ɺ�����Ĭ��Ϊ���ص�</remarks>
        public CSCaret(Control ctl, int width, int height) : this(ctl, true, width, height) {
            mPos = new CarPos();
        }

        /// <summary>
        /// ʹ���ض��Ĵ�С����һ�������,����ָ����ɫ���ɫ
        /// </summary>
        /// <param name="ctl">���������</param>
        /// <param name="black">TrueΪʹ�ú�ɫ,FalseΪʹ�û�ɫ</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <remarks>������ɺ�����Ĭ��Ϊ���ص�</remarks>
        public CSCaret(Control ctl, bool black, int width, int height)
        {
            _HostControl = ctl;
            CSCaret.CreateCaret(this._HostControl.Handle, !black, width, height);
        }


        /// <summary>
        /// �ͷŸò��������
        /// </summary>
        public void Destroy()
        {
            CSCaret.DestroyCaret(_HostControl.Handle);
        }




        /// <summary>
        /// ���ز����,����Ч�������,�������˼���Hide,Ҫ�ٴ���ʾʱ��Ҫ���ü���Show
        /// </summary>
        public void Hide()
        {
            if (visible )
            CSCaret.HideCaret(this.HostControl.Handle);
            visible = false;

        }

        /// <summary>
        /// ��ʾ�����
        /// </summary>
        public void Show(object  some)
        {
            TextBoxV tbv = (TextBoxV)some;
            if (tbv.Focused == true)
            {
                if (!visible) CSCaret.ShowCaret(this.HostControl.Handle);
                visible = true;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public Control HostControl { get { return _HostControl; } }

        /// <summary>
        /// ��û��������в������˸ʱ����
        /// </summary>
        /// <value></value>
        public static uint BlinkTime
        {
            get { return CSCaret.GetCaretBlinkTime(); }
            set { CSCaret.SetCaretBlinkTime(value); }
        }
        
        /// <summary>
        /// ��û����õ�ǰ����Ŀؼ��Ĳ���������λ��
        /// </summary>
        /// <value></value>
        public   CarPos  Position
        {
            get
            {
                POINT p = new POINT();
                if (CSCaret.GetCaretPos(out p))
                {
                    mPos.Location = new PointF(p.x, p.y);
                    return mPos;
                }
                return null;
            }
            set 
            {
                mPos = value;
                CSCaret.SetCaretPos(Convert .ToInt32 ( mPos.Location.X )
                                 ,Convert .ToInt32 ( mPos.Location.Y)); 
            }
        }
        


        
    }
    public class CarPos
    {
        public CarPos()
        {
            pPos=new PointF (0,0);
            Line = 0;
            Column = 0;
        }
        public int Line ;
        public int Column ;

        private PointF pPos;

        public PointF Location
        {
            get
            {
                return pPos;
            }
            set
            {
                pPos = value ;
                //Debug.WriteLine("CarPos is Changed !  In Class" + Location.ToString ());
            }
        }
        public override bool Equals(object cpNew) 
        {
            if (((CarPos)cpNew).Column == Column
            && ((CarPos)cpNew).Line == Line
            && ((CarPos)cpNew).Location == Location) return true; 
            return false; 
        }
        public static bool operator ==(CarPos left, CarPos right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(CarPos left, CarPos right)
        {
            return !left.Equals(right);
        }
        public static bool operator >(CarPos left, CarPos right)
        {
            if (left.Line > right.Line)
                return true;
            if (left.Line < right.Line)
                return false;
            if (left.Line == right.Line&&left.Column > right.Column)
            {
                
                    return true;
                
            }
            else
                return false;
        }
        public static bool operator <(CarPos left, CarPos right)
        {
            if (!(left > right) || left == right)
            {
                return true;
            }
            else
            {
                return false;
            }
             
        }
  
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override string ToString()
        {
            return Location .ToString ()+",{Line:"+Line .ToString ()+",Column:"+Column .ToString ()+"}";
        }
        public void ToNewLine(float LineHeight)
        {
            Line++;
            Column = 0;
            Location = new PointF(Location.X  + LineHeight, 0f);
        }
    }
}