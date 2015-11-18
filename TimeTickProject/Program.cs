using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace TimeTickProject
{
    class Program
    {

        static KeyboardHook.KeyboardHook kh;
        private const string ApiKey = "89389a70d59335ae32yMHiIwpIuQfSPocOv2HMb1j7p2fdqHjff76t5fIOUB7i4nxa3753jhvLxpVCcw";
        static void Main(string[] args)
        {
            //Console.Title = "WAHAHA";
            //IntPtr intptr = FindWindow("ConsoleWindowClass", "WAHAHA");
            //if (intptr != IntPtr.Zero)
            //{
            //    ShowWindow(intptr, 0);//隐藏这个窗口
            //}

            kh = new KeyboardHook.KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;

            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Enabled = true;
            timer.Interval = 100;//执行间隔时间,单位为毫秒
            timer.Elapsed += new ElapsedEventHandler(TimeCheck);
            timer.Start();
            Console.ReadLine();

        }


        public static void TimeCheck(object source, ElapsedEventArgs e)
        {
            //if (DateTime.Now.Second == 48)
            //{
            //    SendKeys.Send("{F3}");
            //}
        
        }

        public static void FirstPullPrice()
        {
            #region 移至价格输入框,并鼠标点击一下
            SetCursorPos(760, 430);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            #endregion

            #region 截屏并产生数字

            MyOcrKing.MyOrc myorc = new MyOcrKing.MyOrc(ApiKey);
            string url = "d:\\" + Guid.NewGuid() + ".jpg";
            int thisprice = myorc.GetNumber(680, 280, 57, 20, url);

            #endregion

            #region 数字加600，并输入输入框
            thisprice += 600;
            System.Windows.Forms.SendKeys.SendWait("^a");
            System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
            System.Windows.Forms.SendKeys.SendWait(thisprice.ToString());
            #endregion

            #region 移动至出价并点击
            SetCursorPos(850, 430);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            #endregion

        }


        static void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {


          
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {
                //Thread tr = new Thread(new ThreadStart(ShowMessage));
                //tr.SetApartmentState(ApartmentState.STA);
                //tr.Start();

                //ShowMessage(myprice);

                Process.Start("D:\\拍牌插件\\paipai\\ConsoleApplication1\\bin\\Debug\\ConsoleApplication1.exe", "0");

            }
            else if (e.KeyCode == Keys.F3)
            {

                FirstPullPrice();
            
            }

        }


        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
    
    }
}
