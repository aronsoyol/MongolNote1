using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace MongolNote
{

    /// <summary>
    /// 插入符
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
        /// 产生一个新的系统插入符，并将其分配给指定的窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="hBitmap">用作插入符的一幅位图的句柄.可以是0或1;在这种情况下,插入符可通过width和height参数创建.如设为1，则新插入符以灰色显示;而不是传统的黑色</param>
        /// <param name="nWidth">插入符宽度</param>
        /// <param name="nHeight">插入符宽度</param>
        /// <returns>成功创建返回True;失败返回False</returns>
        /// <remarks>只有一个窗口得到键盘输入焦点或激活时才能给其创建插入符,在失去键盘输入焦点前应销毁插入符</remarks>
        [DllImport(User32)]
        private static extern bool CreateCaret(IntPtr hWnd, bool hBitmap, int nWidth, int nHeight);

        /// <summary>
        /// 使插入符出现在窗口的在插入符位置上,当插入符出现后即会自动闪烁
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>成功创建返回True;失败返回False</returns>
        /// <remarks>只有窗口自己拥有插入符，且插入符有自己的形状，并且不被隐藏时才会出现,HideCaret是效果叠加的，举例说，如果你在程序中调过3次HideCaret，那就必需再调用3次ShowCaret </remarks>
        [DllImport(User32)]
        private static extern bool ShowCaret(IntPtr hWnd);

        /// <summary>
        /// 隐藏插入符,插入符隐藏后并没有破坏它的形状和插入符位置 
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>成功创建返回True;失败返回False</returns>
        [DllImport(User32)]
        private static extern bool HideCaret(IntPtr hWnd);

        /// <summary>
        /// 得到以毫秒为单位的插入符闪烁间隔 
        /// </summary>
        /// <returns>成功返回闪烁间隔;失败返回0</returns>
        [DllImport(User32)]
        private static extern uint GetCaretBlinkTime();

        /// <summary>
        /// 以毫秒为单位的设置插入符闪烁间隔 
        /// </summary>
        /// <param name="milliseconds">插入符闪烁间隔</param>
        /// <returns>成功创建返回True;失败返回False</returns>
        [DllImport(User32)]
        private static extern bool SetCaretBlinkTime(uint milliseconds);

        /// <summary>
        /// 销毁指定窗口的插入符
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>成功创建返回True;失败返回False</returns>
        /// <remarks>只有窗口拥有插入符时才能销毁,否则该函数会立即返回FALSE</remarks>
        [DllImport(User32)]
        private static extern bool DestroyCaret(IntPtr hWnd);

        /// <summary>
        /// 移动插入符到指定位置
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <returns>成功创建返回True;失败返回False</returns>
        /// <remarks>不管插入符是否可见,此函数都会移动插入符的位置</remarks>
        [DllImport(User32)]
        private static extern bool SetCaretPos(int x, int y);


        /// <summary>
        /// 得到插入符的位置
        /// </summary>
        /// <param name="ppt">指向POINT结构的插入符位置 </param>
        /// <returns>成功创建返回True;失败返回False</returns>
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
        /// 创建一个插入符
        /// </summary>
        /// <param name="ctl">插入符宿主</param>
        /// <remarks>创建完成后插入符默认为隐藏的</remarks>
        /// 
        public CSCaret(Control ctl) : this(ctl, 8, 16) {
            mPos = new CarPos();
        }

        /// <summary>
        /// 使用特定的大小创建一个插入符
        /// </summary>
        /// <param name="ctl">宿主</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <remarks>创建完成后插入符默认为隐藏的</remarks>
        public CSCaret(Control ctl, int width, int height) : this(ctl, true, width, height) {
            mPos = new CarPos();
        }

        /// <summary>
        /// 使用特定的大小创建一个插入符,可以指定黑色或灰色
        /// </summary>
        /// <param name="ctl">插入符宿主</param>
        /// <param name="black">True为使用黑色,False为使用灰色</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <remarks>创建完成后插入符默认为隐藏的</remarks>
        public CSCaret(Control ctl, bool black, int width, int height)
        {
            _HostControl = ctl;
            CSCaret.CreateCaret(this._HostControl.Handle, !black, width, height);
        }


        /// <summary>
        /// 释放该插入符对象
        /// </summary>
        public void Destroy()
        {
            CSCaret.DestroyCaret(_HostControl.Handle);
        }




        /// <summary>
        /// 隐藏插入符,隐藏效果会叠加,即调用了几次Hide,要再次显示时就要调用几次Show
        /// </summary>
        public void Hide()
        {
            if (visible )
            CSCaret.HideCaret(this.HostControl.Handle);
            visible = false;

        }

        /// <summary>
        /// 显示插入符
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
        /// 宿主窗体
        /// </summary>
        public Control HostControl { get { return _HostControl; } }

        /// <summary>
        /// 获得或设置所有插入符闪烁时间间隔
        /// </summary>
        /// <value></value>
        public static uint BlinkTime
        {
            get { return CSCaret.GetCaretBlinkTime(); }
            set { CSCaret.SetCaretBlinkTime(value); }
        }
        
        /// <summary>
        /// 获得或设置当前激活的控件的插入符的相对位置
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