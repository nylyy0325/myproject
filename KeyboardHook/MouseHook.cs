using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeyboardHook
{
    public class MouseHook
    {
        [DllImport("user32.dll")]
        public static extern int GetDoubleClickTime();
        static DateTime lastClickTime;
        static int clickCount;
        private Win32Api.POINT point;
        private Win32Api.POINT Point
        {
            get { return point; }
            set
            {
               // point = value;
                if (point != value)
                {
                    point = value;
                    if (MouseMoveEvent != null)
                    {
                        var e = new MouseEventArgs(MouseButtons.None, 0, point.X, point.Y, 0);
                        MouseMoveEvent(this, e);


                    }

                }



            }
        }
        private int hHook;
        public const int WH_MOUSE_LL = 14;
        public Win32Api.HookProc hProc;
        public MouseHook() { this.Point = new Win32Api.POINT(); }
        public int SetHook()
        {
            hProc = new Win32Api.HookProc(MouseHookProc);
            hHook = Win32Api.SetWindowsHookEx(WH_MOUSE_LL, hProc, IntPtr.Zero, 0);
            return hHook;
        }
        public void UnHook()
        {
            Win32Api.UnhookWindowsHookEx(hHook);
        }
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            Win32Api.MouseHookStruct MyMouseHookStruct = (Win32Api.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.MouseHookStruct));
            if (nCode < 0)
            {
                return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
            }
            else
            {
                this.Point = new Win32Api.POINT(MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y);
                return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);

                //Win32Api.MouseHookStruct mouseHookStruct = (Win32Api.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.MouseHookStruct));
                //MouseButtons mouseButton = MouseButtons.None;

                //switch (wParam)
                //{
                //    case (int)MsgType.WM_LBUTTONDOWN:
                //        mouseButton = MouseButtons.Left;
                //        break;
                //    case (int)MsgType.WM_RBUTTONDOWN:
                //        mouseButton = MouseButtons.Right;
                //        break;
                //    case (int)MsgType.WM_MOUSEMOVE:
                //        clickCount = 0;
                //        break;
                //    default:
                //        break;
                //}

                //if (mouseButton == MouseButtons.Left)
                //{
                //    System.TimeSpan deltaMs = DateTime.Now - lastClickTime;
                //    lastClickTime = DateTime.Now;

                //    if (deltaMs.TotalMilliseconds <= GetDoubleClickTime())
                //    {
                //        clickCount++;
                //    }
                //    else
                //    {
                //        clickCount = 1;
                //    }

                //    if (clickCount == 2)
                //    {
                //        MouseEventArgs e = new MouseEventArgs(
                //            mouseButton,
                //            clickCount,
                //            mouseHookStruct.pt.X,
                //            mouseHookStruct.pt.Y,
                //            0);

                //        clickCount = 0;
                //        MouseDBClickEvent(this, e);
                //    }
                //}
                //return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
                //return CallNextHookEx(mshHook, nCode, wParam, lParam);



            }
        }
        //委托+事件（把钩到的消息封装为事件，由调用者处理）
        public delegate void MouseMoveHandler(object sender, MouseEventArgs e);
        public event MouseMoveHandler MouseMoveEvent;

        public delegate void MouseDBClickHandler(object sender, MouseEventArgs e);
        public event MouseDBClickHandler MouseDBClickEvent;

    }


}
