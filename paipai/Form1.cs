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
using System.Net.NetworkInformation;
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
        public delegate void ChangeFlag();
        public event ChangeFlag flaghandler;
        public event PriceAfter handler;
        MyOcrKing.MyOrc myorc;
        Dictionary<char, Keys> keylist;
        bool flag48 = true;
        static string resultstring = "";
        int lastsecond = 0;
        int addmoney = 0;
        int pullsecond = 0;
        DataTable dtcarplan;
        DataTable personinfodt;
        int monihour = 0;
        int monimin = 0;
        int monisec = 0;
        bool canconfirm = false;
        Dictionary<char, Keys> numlist;
        Dictionary<int, int> lastsecondlist;
        bool firstflag = false;
        int keycount = 0;
        public Form1()
        {
            myorc = new MyOcrKing.MyOrc(ApiKey);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1000, 0);
            InitializeComponent();

            //handler += PriceInsert;
            handler += PriceConfirm;
            handler += SumbitConfirm;


            Thread trupdate = new Thread(new ThreadStart(CheckVersion));
            trupdate.IsBackground = true;
            trupdate.Start();

            Thread tr = new Thread(new ThreadStart(showweb));
            tr.SetApartmentState(ApartmentState.STA);
            tr.IsBackground = true;
            tr.Start();
            numlist = new Dictionary<char, Keys>();
            numlist.Add('0', Keys.D0);
            numlist.Add('1', Keys.D1);
            numlist.Add('2', Keys.D2);
            numlist.Add('3', Keys.D3);
            numlist.Add('4', Keys.D4);
            numlist.Add('5', Keys.D5);
            numlist.Add('6', Keys.D6);
            numlist.Add('7', Keys.D7);
            numlist.Add('8', Keys.D8);
            numlist.Add('9', Keys.D9);

            lastsecondlist = new Dictionary<int, int>();
            lastsecondlist.Add(50, 800);
            lastsecondlist.Add(51, 800);
            lastsecondlist.Add(52, 700);
            lastsecondlist.Add(53, 700);
            lastsecondlist.Add(54, 600);
            lastsecondlist.Add(55, 500);
            lastsecondlist.Add(56, 400);
            lastsecondlist.Add(57, 300);
            lastsecondlist.Add(58, 300);
            lastsecondlist.Add(59, 300);

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
            keylist.Add('x', Keys.X);
            keylist.Add('X', Keys.X);
            keylist.Add('a', Keys.A);
            keylist.Add('b', Keys.B);
            keylist.Add('c', Keys.C);
            keylist.Add('d', Keys.D);
            keylist.Add('e', Keys.E);
            keylist.Add('f', Keys.F);
            keylist.Add('g', Keys.G);
            keylist.Add('h', Keys.H);
            keylist.Add('i', Keys.I);
            keylist.Add('j', Keys.J);
            keylist.Add('k', Keys.K);
            keylist.Add('l', Keys.L);
            keylist.Add('m', Keys.M);
            keylist.Add('n', Keys.N);
            keylist.Add('o', Keys.O);
            keylist.Add('p', Keys.P);
            keylist.Add('q', Keys.Q);
            keylist.Add('r', Keys.R);
            keylist.Add('s', Keys.S);
            keylist.Add('t', Keys.T);
            keylist.Add('u', Keys.U);
            keylist.Add('v', Keys.V);
            keylist.Add('w', Keys.W);
            keylist.Add('y', Keys.Y);
            keylist.Add('z', Keys.Z);
            keylist.Add('A', Keys.A);
            keylist.Add('B', Keys.B);
            keylist.Add('C', Keys.C);
            keylist.Add('D', Keys.D);
            keylist.Add('E', Keys.E);
            keylist.Add('F', Keys.F);
            keylist.Add('G', Keys.G);
            keylist.Add('H', Keys.H);
            keylist.Add('I', Keys.I);
            keylist.Add('J', Keys.J);
            keylist.Add('K', Keys.K);
            keylist.Add('L', Keys.L);
            keylist.Add('M', Keys.M);
            keylist.Add('N', Keys.N);
            keylist.Add('O', Keys.O);
            keylist.Add('P', Keys.P);
            keylist.Add('Q', Keys.Q);
            keylist.Add('R', Keys.R);
            keylist.Add('S', Keys.S);
            keylist.Add('T', Keys.T);
            keylist.Add('U', Keys.U);
            keylist.Add('V', Keys.V);
            keylist.Add('W', Keys.W);
            keylist.Add('Y', Keys.Y);
            keylist.Add('Z', Keys.Z);
            for (int i = 1; i < 24; i++)
            {
                moni_hour.Items.Add(i);

            }
            moni_hour.SelectedItem = DateTime.Now.Hour;
            for (int i = 0; i < 60; i++)
            {
                moni_min.Items.Add(i);

            }
            moni_min.SelectedItem = DateTime.Now.Minute;



            string sql = "select * from CarPlan";
            dtcarplan = DBHelper.GetTable(sql);
            comboBox1.DisplayMember = "planname";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = dtcarplan;

            InitPersonInfo();


            string[] macurl = GetMacString();//获取mac地址;


            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Enabled = true;
            timer.Interval = 100;//执行间隔时间,单位为毫秒
            timer.Elapsed += new ElapsedEventHandler(TimeCheck);
            timer.Start();
            flaghandler += ChangeLable;
            flaghandler += KeyCountShow;
            tabControl1.TabPages.RemoveAt(1);





            //Wrapper.uu_setSoftInfo(108048, "fdaeff5504864589bea7a929334fb69f");
            //Wrapper.uu_login("nylyy0325", "19850325nyl");

        }


        public void ChangeLable()
        {
            label4.Text = flag48 ? "开启" : "关闭";

        }

        public void KeyCountShow()
        {

            keycount_label.Text = keycount.ToString();
        }

        public void TimeCheck(object source, ElapsedEventArgs e)
        {

            //if (DateTime.Now.Second % 20 == 0 && flag48)
            //{

            //    flag48 = false;
            //    keybd_event((byte)Keys.F3, 0, 0, 0);
            //    keybd_event((byte)Keys.F3, 0, 0x2, 0);


            //}
            // label4.Text = flag48 ? "开启" : "关闭";
            this.BeginInvoke(flaghandler);

            if (flag48)
            {

                if (DateTime.Now.Hour == 11 && DateTime.Now.Minute == 29 && DateTime.Now.Second == lastsecond)
                {

                    flag48 = false;
                    FirstPullPrice();
                    //keybd_event((byte)Keys.F3, 0, 0, 0);
                    //keybd_event((byte)Keys.F3, 0, 0x2, 0);

                }

                if (DateTime.Now.Hour == monihour && DateTime.Now.Minute == monimin && DateTime.Now.Second == lastsecond && checkBox1.Checked)
                {
                    flag48 = false;
                    FirstPullPrice();
                    //keybd_event((byte)Keys.F3, 0, 0, 0);
                    //keybd_event((byte)Keys.F3, 0, 0x2, 0);
                }


            }



        }




        public static string[] GetMacString()
        {
            string strMac = "";
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    strMac += ni.GetPhysicalAddress().ToString() + "|";
                }
            }
            return strMac.Split('|');

        }

        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
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


            if (firstflag)
            {


                if (e.KeyData == Keys.Back && keycount > 0)
                {
                    keycount--;

                }


                if (numlist.ContainsValue(e.KeyData) && keycount < 4)
                {
                    keycount++;
                }

            }
            else
            {
                keycount = 0;

            }



            if (e.KeyData == Keys.Escape)
            {

                flag48 = false;
                FirstPullPrice(true);
                //InitPrice();
            }

            else if (e.KeyData == Keys.F4)
            {
                flag48 = !flag48;

            }
            else if (e.KeyData == Keys.F2)
            {
                SecondPullPrice();

            }
            else if (e.KeyData == Keys.Space)
            {
                int[] picpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomPic);
                SetCursorPos(picpoint[0], picpoint[1]);
                mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

                int[] pictextpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomTextBox);
                SetCursorPos(pictextpoint[0], pictextpoint[1]);
                mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);



            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {
                //string url = System.Windows.Forms.Application.StartupPath + @"\CheckPrice.exe";//@"D:\拍牌价格监测\CheckPrice\CheckPrice\bin\x86\Debug\CheckPrice.exe";//System.Windows.Forms.Application.StartupPath.Replace(@"paipai\paipai", @"paipai\\CheckPrice") + @"\CheckPrice.exe";

                //Process.Start(url, myprice.ToString() + " " + pullsecond);


                System.Timers.Timer t = new System.Timers.Timer();
                t.Elapsed += new ElapsedEventHandler(EveryTimeRun);
                t.Interval = 400;
                t.Enabled = true;
                t.Start();


                //System.Threading.Thread tnumber = new System.Threading.Thread(new System.Threading.ThreadStart(AutoNumber));
                //tnumber.IsBackground = true;
                //tnumber.Start();
                canconfirm = true;

            }

        }



        public void FirstPullPrice(bool f3flag = false)
        {
            if (f3flag)
            {
                int[] messageboxshowpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.MessageBoxShow);
                SetCursorPos(messageboxshowpoint[0], messageboxshowpoint[1]);
                mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                int nowlistkey = DateTime.Now.Second < 50 ? 50 : DateTime.Now.Second;
                addmoney = lastsecondlist[nowlistkey];



            }




            #region 自定义加价输入框选中
            int[] addmoneytextboxpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.AddMoneyTextBox);
            //移至文本输入框，鼠标点击
            SetCursorPos(addmoneytextboxpoint[0], addmoneytextboxpoint[1]);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

            keybd_event((byte)Keys.LControlKey, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0x2, 0);
            keybd_event((byte)Keys.LControlKey, 0, 0x2, 0);
            keybd_event((byte)Keys.Delete, 0, 0, 0);

            foreach (char onekey in addmoney.ToString())
            {
                keybd_event((byte)keylist[onekey], 0, 0, 0);
                keybd_event((byte)keylist[onekey], 0, 0x2, 0);
            }


            Thread.Sleep(400);

            #endregion

            #region 自定义加价按钮点击
            int[] addmoneybuttonpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.AddMoneyButton);
            SetCursorPos(addmoneybuttonpoint[0], addmoneybuttonpoint[1]);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            firstflag = true;
            Thread.Sleep(200);
            #endregion



            #region 移至价格输入框,并鼠标点击一下
            //int[] firstpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.FirstPricePoint);
            //SetCursorPos(firstpoint[0], firstpoint[1]);
            //mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            #endregion

            #region 截屏并产生数字

            //string url = "d:\\" + Guid.NewGuid() + ".jpg";
            //int[] lowprice = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.LowestPricePoint);
            //Bitmap lowpic = captureScreen(lowprice[0], lowprice[1], lowprice[2], lowprice[3], 3);
            //lowpic.Save(url, ImageFormat.Jpeg);
            //string lowpricestring = Marshal.PtrToStringAnsi(OCRpart(url, -1, 0, 0, lowprice[2] * 3, lowprice[3] * 3));
            //lowpricestring = ReplaceStringToInt(lowpricestring);

            //int thisprice = int.Parse(lowpricestring);

            //thisprice += addmoney;
            //myprice = thisprice;
            #endregion

            handler();


            //delegate1();

            //delegate2();


        }

        public void SecondPullPrice()
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


            thisprice += 300;//PointType.PointType.GetPointValue(PointType.PointType.Coordinate.AddMoney)[0];//600;
            myprice = thisprice;
            #endregion
            handler();


            //delegate1();

            //delegate2();

        }


        public string ReplaceStringToInt(string vaule)
        {
            return vaule.Replace("I", "1").Replace("T", "7").Replace(@"\r\n", "").Replace("l", "1").Replace("O", "0").Replace("o", "0").Replace("$", "4").Replace("s", "3").Replace("S", "3").Replace("t", "4").Replace(" ", "4");



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
            Thread.Sleep(600);

            #endregion


        }


        public void PriceConfirm()
        {



            #region 移动至出价并点击
            int[] pullprice = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.FirstPullPricePoint);
            SetCursorPos(pullprice[0], pullprice[1]);

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
        public void showtest()
        {
            Form3 f3 = new Form3();
            f3.Location = new Point(0, 0);
            f3.ShowDialog();

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtcopy = dtcarplan.Copy();
            dtcopy.DefaultView.RowFilter = "id = " + comboBox1.SelectedValue;
            lastsecond = Convert.ToInt32(dtcopy.DefaultView[0]["lastsecond"]);
            addmoney = Convert.ToInt32(dtcopy.DefaultView[0]["addmoney"]);
            pullsecond = Convert.ToInt32(dtcopy.DefaultView[0]["pullsecond"]);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sqlbookid = "update [PointVaule] set PointX='" + bookidx.Text + "',PointY='" + bookidy.Text + "' where typeid=13";
            string sqlbookpassword = "update [PointVaule] set PointX='" + bookpasswordx.Text + "',PointY='" + bookpasswordy.Text + "' where typeid=14";
            string sqlpersonid = "update [PointVaule] set PointX='" + personidx.Text + "',PointY='" + personidy.Text + "' where typeid=15";



            bool bookidflag = DBHelper.ExcuteSQL(sqlbookid) > 0;
            bool bookpasswordflag = DBHelper.ExcuteSQL(sqlbookpassword) > 0;
            bool personidflag = DBHelper.ExcuteSQL(sqlpersonid) > 0;

            if (bookidflag && bookpasswordflag && personidflag)
            {
                MessageBox.Show("更新成功");

            }
            else
            {
                MessageBox.Show("更新失败");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (comboBox_selectitem.SelectedIndex == -1)
            {
                MessageBox.Show("请选择查询项");
                return;
            }
            string selectcolval = "";
            if (comboBox_selectitem.SelectedIndex == 0)
                selectcolval = "bookid";
            else
                selectcolval = "bookpassword";

            //DataTable personinfocopy = personinfodt.Copy();

            if (textBox_selectvalue.Text != "")
            {
                personinfodt = DBHelper.GetTable("select * from personinfo where " + selectcolval + "=" + textBox_selectvalue.Text);
            }
            else
            {
                personinfodt = DBHelper.GetTable("select * from personinfo");

            }



            dataGridView1.DataSource = personinfodt.DefaultView;



        }
        public void InitPersonInfo()
        {
            personinfodt = DBHelper.GetTable("select * from personinfo");
            dataGridView1.DataSource = personinfodt;

        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            int a = -1;
            try
            {
                a = dataGridView1.CurrentRow.Index;
            }
            catch
            {
                MessageBox.Show("数据无选中");
                return;
            }

            Login(true, a);




        }

        public void Login(bool isfirst, int a)
        {
            string bookid = dataGridView1.Rows[a].Cells["bookid"].Value.ToString();
            string bookpassword = dataGridView1.Rows[a].Cells["bookpassword"].Value.ToString();
            string personid = dataGridView1.Rows[a].Cells["personid"].Value.ToString();
            DataTable dtpoint = DBHelper.GetTable("select PointX,PointY,typeid from [PointVaule] where typeid in(13,14,15)");
            dtpoint.DefaultView.RowFilter = "typeid = 13";
            int bookidpointx = int.Parse(dtpoint.DefaultView[0][0].ToString());
            int bookidpointy = int.Parse(dtpoint.DefaultView[0][1].ToString());
            dtpoint.DefaultView.RowFilter = "typeid = 14";
            int bookpasswordx = int.Parse(dtpoint.DefaultView[0][0].ToString());
            int bookpasswordy = int.Parse(dtpoint.DefaultView[0][1].ToString());
            dtpoint.DefaultView.RowFilter = "typeid = 15";
            int personidx = int.Parse(dtpoint.DefaultView[0][0].ToString());
            int personidy = int.Parse(dtpoint.DefaultView[0][1].ToString());


            SetCursorPos(bookidpointx, bookidpointy);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            keybd_event((byte)Keys.LControlKey, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0x2, 0);
            keybd_event((byte)Keys.LControlKey, 0, 0x2, 0);
            keybd_event((byte)Keys.Delete, 0, 0, 0);
            foreach (char onekey in bookid.ToString())
            {
                keybd_event((byte)keylist[onekey], 0, 0, 0);
                keybd_event((byte)keylist[onekey], 0, 0x2, 0);
            }

            SetCursorPos(bookpasswordx, bookpasswordy);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            keybd_event((byte)Keys.LControlKey, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0, 0);
            keybd_event((byte)Keys.A, 0, 0x2, 0);
            keybd_event((byte)Keys.LControlKey, 0, 0x2, 0);
            keybd_event((byte)Keys.Delete, 0, 0, 0);
            foreach (char onekey in bookpassword.ToString())
            {
                keybd_event((byte)keylist[onekey], 0, 0, 0);
                keybd_event((byte)keylist[onekey], 0, 0x2, 0);
            }

            if (!isfirst)
            {
                SetCursorPos(personidx, personidy);
                mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                keybd_event((byte)Keys.LControlKey, 0, 0, 0);
                keybd_event((byte)Keys.A, 0, 0, 0);
                keybd_event((byte)Keys.A, 0, 0x2, 0);
                keybd_event((byte)Keys.LControlKey, 0, 0x2, 0);
                keybd_event((byte)Keys.Delete, 0, 0, 0);
                foreach (char onekey in personid.ToString())
                {
                    keybd_event((byte)keylist[onekey], 0, 0, 0);
                    keybd_event((byte)keylist[onekey], 0, 0x2, 0);
                }


            }


        }

        private void btn_second_Click(object sender, EventArgs e)
        {
            int a = -1;
            try
            {
                a = dataGridView1.CurrentRow.Index;
            }
            catch
            {
                MessageBox.Show("数据无选中");
                return;
            }

            Login(false, a);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(showtest));
            tr.SetApartmentState(ApartmentState.STA);
            tr.IsBackground = true;
            tr.Start();
        }

        private void moni_hour_SelectedIndexChanged(object sender, EventArgs e)
        {
            monihour = Convert.ToInt32(moni_hour.SelectedItem);

        }

        private void moni_min_SelectedIndexChanged(object sender, EventArgs e)
        {
            monimin = Convert.ToInt32(moni_min.SelectedItem);
        }

        public string CheckResult(string result)
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


                //相等则校验通过

                return strCodeResult;

            }
        }
        public void AutoNumber()
        {

            try
            {

                int[] randompic = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomPic);

                Bitmap bmp = captureScreen(randompic[0], randompic[1], randompic[2], randompic[3], 2); //captureScreen(780, 400, 120, 30, false);
                string firsturl = "d:\\" + Guid.NewGuid() + ".jpg";
                bmp.Save(firsturl);


                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                    {
                        StringBuilder result = new StringBuilder(50);
                        int codeId = Wrapper.uu_recognizeByCodeTypeAndPath(firsturl, 5106, result);
                        string resultCode = CheckResult(result.ToString());
                        resultstring = resultCode.ToString();

                        //联众
                        //string returnMess = RecYZM(firsturl, "nylyy0325", "19850325nyl");
                        //resultstring = returnMess.Split('|')[0];


                    }));
                    t.IsBackground = true;
                    t.Start();
                    if (resultstring != "")
                        break;

                }

                while (true)
                {
                    // string resultCode = CheckResult(result.ToString(), 108048, codeId, "840D0F4A-CE73-486D-A914-2DE4502BE64E");
                    //if (resultCode != "")
                    //{

                    if (resultstring == "")
                        continue;


                    #region 验证码截取多少数字

                    string outresultstring = "";
                    if (resultstring.Length > 4)
                    {
                        outresultstring = resultstring;
                        int[] counts = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.AutoNumberSubString);
                        //Bitmap bmpcount = captureScreen(counts[0], counts[1], counts[2], counts[3]);  //captureScreen(715, 340, 50, 17);
                        string counturl = "d:\\" + Guid.NewGuid() + ".jpg";
                        //bmpcount.Save(counturl, ImageFormat.Jpeg);




                        string autonumbercount = myorc.GetSubString(counts[0], counts[1], counts[2], counts[3], counturl); //Marshal.PtrToStringAnsi(OCRpart(counturl, -1, 0, 0, counts[2], counts[3]));

                        string autonumbercountend = Marshal.PtrToStringAnsi(OCRpart(counturl, -1, 0, 0, counts[2], counts[3]));

                        autonumbercount = autonumbercount.Replace("\r\n", "");


                        //DataTable dt = DBHelper.GetTable("select * from PointSubString");
                        //string qian4 = dt.DefaultView[0]["StringValue"].ToString();
                        //string zhong4 = dt.DefaultView[1]["StringValue"].ToString();
                        //string hou4 = dt.DefaultView[2]["StringValue"].ToString();


                        //if (autonumbercount == hou4)//后四位
                        //{
                        //    outresultstring = outresultstring.Substring(2, 4);
                        //}
                        //else if (autonumbercount == zhong4)//第二到五位
                        //{
                        //    outresultstring = outresultstring.Substring(1, 4);

                        //}
                        //else
                        //{
                        //    outresultstring = outresultstring.Substring(0, 4);

                        //}

                        outresultstring = GetSubStringValue(outresultstring, autonumbercount, autonumbercountend);

                    }
                    else
                    {
                        outresultstring = resultstring;

                    }

                    #endregion



                    //DBHelper.ExcuteSQL("insert into ErrorInfo([errormessage]) values('验证码为" + outresultstring + "')");
                    int[] randomtextboxpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomTextBox);
                    //SetCursorPos(700, 420);
                    SetCursorPos(randomtextboxpoint[0], randomtextboxpoint[1]);
                    mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                    foreach (char onekey in outresultstring)
                    {
                        keybd_event((byte)keylist[onekey], 0, 0, 0);
                        keybd_event((byte)keylist[onekey], 0, 0x2, 0);
                    }
                    canconfirm = true;
                    resultstring = "";
                    outresultstring = "";
                    break;
                    // }


                }
            }
            catch (Exception ex)
            {
                DBHelper.ExcuteSQL("insert into ErrorInfo([errormessage]) values('" + resultstring + "和" + ex.Message + "')");
                //WriteError(ex.Message);
                Environment.Exit(0);

            }


            //StringBuilder result = new StringBuilder(50);
            //int codeId = Wrapper.uu_recognizeByCodeTypeAndPath(firsturl, 1006, result);


        }

        public string GetSubStringValue(string outresultstring, string flagstring, string autonumbercountend)
        {

            if (flagstring.IndexOf("前") > -1 || flagstring.IndexOf("一") > -1)
            {
                outresultstring = outresultstring.Substring(0, 4);
            }

            else if (flagstring.IndexOf("后") > -1 || flagstring.IndexOf("三") > -1)
            {
                outresultstring = outresultstring.Substring(2, 4);

            }

            else
            {
                //DataTable dt = DBHelper.GetTable("select * from PointSubString");
                //string qian4 = dt.DefaultView[0]["StringValue"].ToString();
                //string zhong4 = dt.DefaultView[1]["StringValue"].ToString();
                //string hou4 = dt.DefaultView[2]["StringValue"].ToString();


                if (autonumbercountend.IndexOf("1") > -1 && autonumbercountend.IndexOf("4") > -1)//前四位
                {
                    outresultstring = outresultstring.Substring(0, 4);

                }
                else if (autonumbercountend.IndexOf("3") > -1 && autonumbercountend.IndexOf("6") > -1)//第三到六位
                {
                    outresultstring = outresultstring.Substring(2, 4);

                }
                else
                {
                    outresultstring = outresultstring.Substring(1, 4);
                }

                //outresultstring = outresultstring.Substring(1, 4);
            }
            return outresultstring;


        }

        public void EveryTimeRun(object source, ElapsedEventArgs e)
        {

            try
            {

                if (DateTime.Now.Second >= pullsecond && canconfirm && keycount >= 4)
                {

                    int[] finallpullpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.FinallyPullPrice);

                    SetCursorPos(finallpullpoint[0], finallpullpoint[1]);
                    mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

                    canconfirm = false;
                    firstflag = false;


                }




            }
            catch (Exception ex)
            {
                //DBHelper.ExcuteSQL("insert into ErrorInfo([errormessage]) values('" + resultstring + "和" + ex.Message + "')");
                //WriteError(ex.Message);
                //Environment.Exit(0);

            }



        }

        private void button4_Click(object sender, EventArgs e)
        {


            string sql = "select * from CarPlan";
            dtcarplan = DBHelper.GetTable(sql);
            comboBox1.DisplayMember = "planname";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = dtcarplan;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }





    }
}
