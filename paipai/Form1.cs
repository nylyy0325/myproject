using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Runtime.InteropServices;
using System.Text;

using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using AvensLab.Service;
using AvensLab.Service.Models;
using System.Xml;
using KeyboardHook;
using System.Collections;
using System.Timers;
using ConsoleApplication1;
using System.Security.Cryptography;
using PointType;
namespace paipai
{

    public partial class Form1 : Form
    {

        KeyboardHook.KeyboardHook kh;
        public int myprice;
        string url;
        List<string> stringlist;
        private const string ApiKey = "32d16eda539883457e61ufwlsORme81I3XQMAGl0e7iK5vpD6Q2ZHkvaAXmKvmDIUdvGm7RHAvOg";
        public delegate void PriceAfter();
        public event PriceAfter handler;
        MyOcrKing.MyOrc myorc;
        Dictionary<char, Keys> keylist;
        static bool flag48 = false;
        static string resultstring = "";
        public Form1()
        {
            myorc = new MyOcrKing.MyOrc(ApiKey);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1000, 0);
            InitializeComponent();

            handler += PriceInsert;
            handler += PriceConfirm;
            handler += SumbitConfirm;


            Thread trupdate = new Thread(new ThreadStart(CheckVersion));
            trupdate.IsBackground = true;
            trupdate.Start();

            Thread tr = new Thread(new ThreadStart(showweb));
            tr.SetApartmentState(ApartmentState.STA);
            tr.IsBackground = true;
            tr.Start();
            stringlist = new List<string>();
            keylist = new Dictionary<char, Keys>();
            keylist.Add('0', Keys.D0);
            keylist.Add('1', Keys.D1);
            keylist.Add('2', Keys.D2);
            keylist.Add('3', Keys.D3);
            keylist.Add('4', Keys.D4);
            keylist.Add('5', Keys.D5);
            keylist.Add('6', Keys.D6);
            keylist.Add('7', Keys.D7);
            keylist.Add('8', Keys.D8);
            keylist.Add('9', Keys.D9);



            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Enabled = true;
            timer.Interval = 100;//执行间隔时间,单位为毫秒
            timer.Elapsed += new ElapsedEventHandler(TimeCheck);
            timer.Start();



        }


        public static void TimeCheck(object source, ElapsedEventArgs e)
        {

            if (DateTime.Now.Second % 20 == 0 && flag48)
            {
                
                flag48 = false;
                keybd_event((byte)Keys.F3, 0, 0, 0);
                keybd_event((byte)Keys.F3, 0, 0x2, 0);


            }

        }


        public void CheckVersion()
        {
            while (true)
            {
                string xmlurl = System.Windows.Forms.Application.StartupPath + @"\Version.xml";
                int sqlVersion = GetNowVersion();
                int nowVersion = 0;
                bool flag = true;
                if (File.Exists(xmlurl))//存在xml
                {
                    XmlHelper xh = new XmlHelper(xmlurl);
                    DataTable xmltable = xh.GetData("NowVersion");
                    nowVersion = Convert.ToInt32(xmltable.DefaultView[0][0]);
                    if (nowVersion < sqlVersion)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;

                    }
                    //xh.Replace("NowVersion/Version", sqlVersion.ToString());
                    //xh.Save();


                }
                else//不存在xml
                {
                    //CreateXmlFile(xmlurl, sqlVersion);
                    flag = true;

                }

                if (flag)
                {

                    string url = System.Windows.Forms.Application.StartupPath + @"\updatefile.exe";//@"D:\拍牌价格监测\CheckPrice\CheckPrice\bin\x86\Debug\CheckPrice.exe";//System.Windows.Forms.Application.StartupPath.Replace(@"paipai\paipai", @"paipai\\CheckPrice") + @"\CheckPrice.exe";
                    Process.Start(url);
                    Application.Exit();
                    Environment.Exit(0);

                }
                Thread.Sleep(1000);
            }


        }

        public void CreateXmlFile(string xmlpath, int version)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("NowVersion");
            xmlDoc.AppendChild(root);
            CreateNode(xmlDoc, root, "Version", version.ToString());
            try
            {
                xmlDoc.Save(xmlpath);
            }
            catch (Exception e)
            {
                //显示错误信息  
                Console.WriteLine(e.Message);
            }
            //Console.ReadLine();  

        }
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        public int GetNowVersion()
        {
            string sql = "select * from VersionControl";
            DataTable dt = DBHelper.GetTable(sql);
            return Convert.ToInt32(dt.DefaultView[0][1]);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kh = new KeyboardHook.KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
        }


        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {


            if (e.KeyData == Keys.F3)
            {


                FirstPullPrice();
                //InitPrice();
            }

            else if (e.KeyData == Keys.F4)
            {
                flag48 = !flag48;

            }



            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {

                //Thread tr = new Thread(new ThreadStart(ShowMessage));
                //tr.SetApartmentState(ApartmentState.STA);
                //tr.Start();

                //ShowMessage(myprice);


                //MessageBox.Show(System.Windows.Forms.Application.StartupPath);
                //return;
                string url = System.Windows.Forms.Application.StartupPath + @"\CheckPrice.exe";//@"D:\拍牌价格监测\CheckPrice\CheckPrice\bin\x86\Debug\CheckPrice.exe";//System.Windows.Forms.Application.StartupPath.Replace(@"paipai\paipai", @"paipai\\CheckPrice") + @"\CheckPrice.exe";


                //Process.Start("D:\\拍牌插件\\paipai\\ConsoleApplication1\\bin\\Debug\\ConsoleApplication1.exe", myprice.ToString());
                Process.Start(url, myprice.ToString());

                //Thread.Sleep(300);

                //int[] randomtextboxpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomTextBox);
                //SetCursorPos(randomtextboxpoint[0], randomtextboxpoint[1]);
                ////SetCursorPos(730, 410);
                //mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);



            }

        }



        public void FirstPullPrice()
        {
            int[] firstpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.FirstPricePoint);
            #region 移至价格输入框,并鼠标点击一下
            //SetCursorPos(760, 430);
            SetCursorPos(firstpoint[0], firstpoint[1]);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            #endregion

            #region 截屏并产生数字


            string url = "d:\\" + Guid.NewGuid() + ".jpg";
            int[] lowprice = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.LowestPricePoint);
            Bitmap lowpic = captureScreen(lowprice[0], lowprice[1], lowprice[2], lowprice[3], 3);
            lowpic.Save(url, ImageFormat.Jpeg);
            string lowpricestring = Marshal.PtrToStringAnsi(OCRpart(url, -1, 0, 0, lowprice[2] * 3, lowprice[3] * 3));
            lowpricestring = ReplaceStringToInt(lowpricestring);

            int thisprice = int.Parse(lowpricestring); //int.Parse(Marshal.PtrToStringAnsi(OCRpart(url, -1, 0, 0, lowprice[2], lowprice[3])).Replace("T", "7").Replace("l", "1")); //myorc.GetNumber(lowprice[0], lowprice[1], lowprice[2], lowprice[3], url);//myorc.GetNumber(680, 280, 57, 20, url);


            thisprice += PointType.PointType.GetPointValue(PointType.PointType.Coordinate.AddMoney)[0];//600;
            myprice = thisprice;
            #endregion
            handler();


            //delegate1();

            //delegate2();


        }


        public string ReplaceStringToInt(string vaule)
        {
            return vaule.Replace("T", "7").Replace(@"\r\n", "").Replace("l", "1").Replace("O", "0").Replace("o", "0").Replace("$", "4").Replace("s", "3").Replace("S", "3").Replace("t", "4").Replace(" ", "4");



        }




        private string ParseResult(string result, bool processStats)
        {
            if (processStats)
            {
                // 解析结果
                try
                {
                    int startindex = result.IndexOf("<Result>");
                    string resultstring = result.Substring(startindex, 13).Replace("<Result>", "");
                    return resultstring;
                    //this.doc.LoadXml(result);

                    // 识别结果
                    //this.textBox3.Text = this.doc.SelectSingleNode("//Results/ResultList/Item/Result").InnerText;
                }
                catch
                {
                    return "";

                }

            }
            else
            {
                // 识别结果
                return result;
            }
        }

        public void PriceInsert()
        {

            #region 数字加600，并输入输入框

            keybd_event((byte)Keys.LControlKey, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0x2, 0);
            keybd_event((byte)Keys.LControlKey, 0, 0x2, 0);
            keybd_event((byte)Keys.Delete, 0, 0, 0);

            foreach (char onekey in myprice.ToString())
            {
                keybd_event((byte)keylist[onekey], 0, 0, 0);
                keybd_event((byte)keylist[onekey], 0, 0x2, 0);
            }
            Thread.Sleep(200);

            #endregion


        }


        public void PriceConfirm()
        {



            #region 移动至出价并点击
            int[] pullprice = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.FirstPullPricePoint);
            SetCursorPos(pullprice[0], pullprice[1]);
            //SetCursorPos(850, 430);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            #endregion
        }

        public void SumbitConfirm()
        {
            Thread.Sleep(1000);

            //Wrapper.uu_setSoftInfo(108048, "fdaeff5504864589bea7a929334fb69f");
            //Wrapper.uu_login("nylyy0325", "19850325nyl");
            //AutoNumber();

            keybd_event((byte)Keys.LControlKey, 0, 0, 0);
            keybd_event((byte)Keys.Enter, 0, 0, 0);
            keybd_event((byte)Keys.Enter, 0, 0x2, 0);
            keybd_event((byte)Keys.LControlKey, 0, 0x2, 0);


        }

        public Bitmap captureScreen(int x, int y, int width, int height, int bei = 1)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format64bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(x, y), new Point(0, 0), bmp.Size);
                g.Dispose();
            }

            Bitmap newbm = new Bitmap(bmp, width * bei, height * bei);
            //bit.Save(@"capture2.png");
            return newbm;
        }


        public string MD5Encoding(string rawPass)
        {
            // 创建MD5类的默认实例：MD5CryptoServiceProvider
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public string CheckResult(string result, int softId, int codeId, string checkKey)
        {
            //对验证码结果进行校验，防止dll被替换
            if (string.IsNullOrEmpty(result))
                return result;
            else
            {
                if (result[0] == '-')
                    //服务器返回的是错误代码
                    return result;

                string[] modelReult = result.Split('_');
                //解析出服务器返回的校验结果
                string strServerKey = modelReult[0];
                string strCodeResult = modelReult[1];
                //本地计算校验结果
                string localInfo = softId.ToString() + checkKey + codeId.ToString() + strCodeResult.ToUpper();
                string strLocalKey = MD5Encoding(localInfo).ToUpper();
                //相等则校验通过
                if (strServerKey.Equals(strLocalKey))
                    return strCodeResult;
                return "结果校验不正确";
            }
        }

        public void showweb()
        {
            Form2 f2 = new Form2();
            f2.Location = new Point(0, 0);
            f2.ShowDialog();

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


        private void button1_Click(object sender, EventArgs e)
        {
            //SetCursorPos(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));

            Bitmap bmp = captureScreen(660, 444, 175, 25);  //captureScreen(715, 340, 50, 17);
            string counturl = "d:\\" + Guid.NewGuid() + ".jpg";
            bmp.Save(counturl, ImageFormat.Jpeg);

            string lowpricestring = Marshal.PtrToStringAnsi(OCRpart(counturl, -1, 0, 0, 175, 25));
            //lowpricestring = ReplaceStringToInt(lowpricestring);

            MessageBox.Show(lowpricestring);




        }

        //[DllImport("AspriseOCR.dll", EntryPoint = "OCR", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr OCR(string file, int type);


        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        private static void SetPos()
        {
            int dx = 1000;
            int dy = 100;
            SetCursorPos(dx, dy);
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);


        private const string OCR_DLL_NAME_32 = "aocr.dll";
        [DllImport(OCR_DLL_NAME_32, CharSet = CharSet.Ansi)]
        public static extern int com_asprise_ocr_setup(int queryOnly);
        [DllImport(OCR_DLL_NAME_32, CharSet = CharSet.Ansi)]
        public static extern IntPtr com_asprise_ocr_start(string lang, string speed);
        [DllImport(OCR_DLL_NAME_32, EntryPoint = "com_asprise_ocr_recognize")]
        public static extern IntPtr com_asprise_ocr_recognize(Int64 handle, string imgFiles, int pageIndex, int startX, int startY, int width, int height, string recognizeType, string outputFormat, string propSpec, string propSeparator, string propKeyValueSpeparator);
        [DllImport(OCR_DLL_NAME_32, CharSet = CharSet.Ansi)]
        public static extern void com_asprise_ocr_util_delete(Int64 handle, bool isArray);


        #region AspriseOCR插件引用

        [DllImport("AspriseOCR.dll", EntryPoint = "OCR", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OCR(string file, int type);

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRpart", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr OCRpart(string file, int type, int startX, int startY, int width, int height);

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRBarCodes", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr OCRBarCodes(string file, int type);

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRpartBarCodes", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr OCRpartBarCodes(string file, int type, int startX, int startY, int width, int height);

        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            //this.Height = 1000;
        }





    }
}
