using AvensLab.Service;
using AvensLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

using System.Timers;
using System.Xml;
using System.IO;
namespace ConsoleApplication1
{
    class Program
    {
        static int myprice = 0;
        static string starturl = "";
        static string endturl = "";
        static List<string> messagelist;
        private const string ApiKey = "ad6be37ba1c990d2faY7WmCyKfGkRcAA90nwglVA4V84JynHFE9lyPIosVFb0PijEwMP9BWgKciII";//"89389a70d59335ae32yMHiIwpIuQfSPocOv2HMb1j7p2fdqHjff76t5fIOUB7i4nxa3753jhvLxpVCcw";
        static Dictionary<char, System.Windows.Forms.Keys> keylist;
        static string resultstring = "";
        static bool canconfirm = false;
        static OcrKing ocrKing = new OcrKing(ApiKey)
        {
            //Language = Language.Eng, 
            //Service = Service.OcrKingForCaptcha, 
            //Charset = Charset.DigitLowerUpper

            Language = Language.Eng,
            Service = Service.OcrKingForCaptcha,
            Charset = Charset.Digit
        };
        static void Main(string[] args)
        {
            //Console.Title = "WAHAHA";
            //IntPtr intptr = FindWindow("ConsoleWindowClass", "WAHAHA");
            //if (intptr != IntPtr.Zero)
            //{
            //    ShowWindow(intptr, 0);//隐藏这个窗口
            //}
            keylist = new Dictionary<char, System.Windows.Forms.Keys>();
            keylist.Add('0', System.Windows.Forms.Keys.D0);
            keylist.Add('1', System.Windows.Forms.Keys.D1);
            keylist.Add('2', System.Windows.Forms.Keys.D2);
            keylist.Add('3', System.Windows.Forms.Keys.D3);
            keylist.Add('4', System.Windows.Forms.Keys.D4);
            keylist.Add('5', System.Windows.Forms.Keys.D5);
            keylist.Add('6', System.Windows.Forms.Keys.D6);
            keylist.Add('7', System.Windows.Forms.Keys.D7);
            keylist.Add('8', System.Windows.Forms.Keys.D8);
            keylist.Add('9', System.Windows.Forms.Keys.D9);

            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(80, 10);


            #region 初始化打码平台

            Wrapper.uu_setSoftInfo(108048, "fdaeff5504864589bea7a929334fb69f");
            Wrapper.uu_login("nylyy0325", "19850325nyl");

            //string strSoftID = "108048";
            //int softId = int.Parse(strSoftID);
            //string softKey = "fdaeff5504864589bea7a929334fb69f";
            //Guid guid = Guid.NewGuid();
            //string strGuid = guid.ToString().Replace("-", "").Substring(0, 32).ToUpper();
            //string DLLPath = System.Environment.CurrentDirectory + "\\UUWiseHelper.dll";
            ////string DLLPath = "E:\\work\\UUWiseHelper 新版http协议\\输出目录\\UUWiseHelper.dll";
            //string strDllMd5 = GetFileMD5(DLLPath);
            //CRC32 objCrc32 = new CRC32();
            //string strDllCrc = String.Format("{0:X}", objCrc32.FileCRC(DLLPath));
            ////CRC不足8位，则前面补0，补足8位
            //int crcLen = strDllCrc.Length;
            //if (crcLen < 8)
            //{
            //    int miss = 8 - crcLen;
            //    for (int i = 0; i < miss; ++i)
            //    {
            //        strDllCrc = "0" + strDllCrc;
            //    }
            //}


            //string strCheckKey = "840D0F4A-CE73-486D-A914-2DE4502BE64E".ToUpper();
            //string yuanshiInfo = strSoftID + strCheckKey + strGuid + strDllMd5.ToUpper() + strDllCrc.ToUpper();
            ////richTextBox1.Text += yuanshiInfo + "\n";
            //string localInfo = MD5Encoding(yuanshiInfo);
            //StringBuilder checkResult = new StringBuilder();
            //Wrapper.uu_CheckApiSign(softId, softKey, strGuid, strDllMd5, strDllCrc, checkResult);
            //string strCheckResult = checkResult.ToString();


            
            #endregion

            System.Threading.Thread tnumber = new System.Threading.Thread(new System.Threading.ThreadStart(AutoNumber));
            tnumber.IsBackground = true;
            tnumber.Start();



            myprice = Convert.ToInt32(args[0]);
            if (myprice == 0)
            {
                GetFirstPrice();

            }
            Timer t = new Timer();
            t.Elapsed += new ElapsedEventHandler(EveryTimeRun);
            t.Interval = 300;
            t.Enabled = true;
            t.Start();

            Console.ReadLine();
        }

        /// <summary>
        /// 获取文件MD5校验值
        /// </summary>
        /// <param name="filePath">校验文件路径</param>
        /// <returns>MD5校验字符串</returns>
        private static string GetFileMD5(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5byte = md5.ComputeHash(fs);
            int i, j;
            StringBuilder sb = new StringBuilder(16);
            foreach (byte b in md5byte)
            {
                i = Convert.ToInt32(b);
                j = i >> 4;
                sb.Append(Convert.ToString(j, 16));
                j = ((i << 4) & 0x00ff) >> 4;
                sb.Append(Convert.ToString(j, 16));
            }
            return sb.ToString();
        }
        public static void AutoNumber()
        {
            int[] randompic = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomPic);

            Bitmap bmp = captureScreen(randompic[0], randompic[1], randompic[2], randompic[3], false); //captureScreen(780, 400, 120, 30, false);
            string firsturl = "d:\\" + Guid.NewGuid() + ".jpg";
            bmp.Save(firsturl);


            for (int i = 0; i < 20; i++)
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    StringBuilder result = new StringBuilder(50);
                    int codeId = Wrapper.uu_recognizeByCodeTypeAndPath(firsturl, 1106, result);
                    string resultCode = CheckResult(result.ToString(), 108048, codeId, "840D0F4A-CE73-486D-A914-2DE4502BE64E");
                    resultstring = resultCode.ToString();

                }));
                t.IsBackground = true;
                t.Start();

            }

            while (true)
            {
                // string resultCode = CheckResult(result.ToString(), 108048, codeId, "840D0F4A-CE73-486D-A914-2DE4502BE64E");
                //if (resultCode != "")
                //{
                if (resultstring == "")
                    continue;

                #region 验证码截取多少数字
                //int[] randomsubhowmuchpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomSubHowMuch);
                //Bitmap bmpindex = captureScreen(randomsubhowmuchpoint[0], randomsubhowmuchpoint[1], randomsubhowmuchpoint[2], randomsubhowmuchpoint[3], false);//captureScreen(600, 445, 220, 20);
                //string indexurl = "d:\\" + Guid.NewGuid() + ".jpg";
                //bmpindex.Save(indexurl);
                //string subvalue = GetValue(indexurl);
                //if (subvalue == "")
                //{ 

                //}
                //else if (subvalue == "")
                //{ 


                //}

                #endregion


                Console.WriteLine("{0}", resultstring);
                int[] randomtextboxpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.RandomTextBox);
                //SetCursorPos(700, 420);
                SetCursorPos(randomtextboxpoint[0], randomtextboxpoint[1]);
                mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                foreach (char onekey in resultstring)
                {
                    keybd_event((byte)keylist[onekey], 0, 0, 0);
                    keybd_event((byte)keylist[onekey], 0, 0x2, 0);
                }
                canconfirm = true;
                break;
                // }


            }


            //StringBuilder result = new StringBuilder(50);
            //int codeId = Wrapper.uu_recognizeByCodeTypeAndPath(firsturl, 1006, result);


        }


        public static string CheckResult(string result, int softId, int codeId, string checkKey)
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

        public static string MD5Encoding(string rawPass)
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

        public static void GetFirstPrice()
        {


            //MyOcrKing.MyOrc myorc = new MyOcrKing.MyOrc(ApiKey);
            //myprice = myorc.GetNumber(715, 340, 50, 17, firsturl);
            int[] mypricepoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.MyPrice);

            Bitmap bmp = captureScreen(mypricepoint[0], mypricepoint[1], mypricepoint[2], mypricepoint[3]);  //captureScreen(715, 340, 50, 17);
            string firsturl = "d:\\" + Guid.NewGuid() + ".jpg";
            bmp.Save(firsturl);
            myprice = GetNumber(firsturl);



        }


        public static void EveryTimeRun(object source, ElapsedEventArgs e)
        {


            //MyOcrKing.MyOrc myorc = new MyOcrKing.MyOrc(ApiKey);

            //int startnumber = myorc.GetNumber(266, 459, 41, 13, starturl);

            //int endnumber = myorc.GetNumber(321, 459, 41, 13, endturl);



            starturl = "d:\\" + Guid.NewGuid() + ".jpg";
            int[] startprice = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.PriceStart);
            Bitmap bmpstart = captureScreen(startprice[0], startprice[1], startprice[2], startprice[3]);//captureScreen(266, 459, 41, 13);
            bmpstart.Save(starturl);
            int startnumber = GetNumber(starturl);

            endturl = "d:\\" + Guid.NewGuid() + ".jpg";
            int[] endprice = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.PriceEnd);
            Bitmap bmpend = captureScreen(startprice[0], startprice[1], startprice[2], startprice[3]);//captureScreen(321, 459, 41, 13);
            bmpend.Save(endturl);
            int endnumber = GetNumber(endturl);


            if (myprice <= endnumber && canconfirm)
            {

                int[] finallpullpoint = PointType.PointType.GetPointValue(PointType.PointType.Coordinate.FinallyPullPrice);
                //SetCursorPos(600, 500);
                SetCursorPos(finallpullpoint[0], finallpullpoint[1]);
                mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
                Environment.Exit(0);
            }

            string showmessage = "目前出价范围为" + startnumber + "至" + endnumber + ",距离你的出价，还差" + (endnumber - myprice) + "元\r\n";


            Console.WriteLine(showmessage);




        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        public static Bitmap captureScreen(int x, int y, int width, int height, bool flag = true)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format64bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(x, y), new Point(0, 0), bmp.Size);
                g.Dispose();
            }
            int bei = 2;
            if (flag)
                bei = 4;
            Bitmap newbm = new Bitmap(bmp, width * bei, height * bei);
            //bit.Save(@"capture2.png");
            return newbm;
        }

        public static int GetNumber(string url)
        {


            ocrKing.Type = "http://www.unknown.com";
            ocrKing.Language = Language.Eng;
            ocrKing.Charset = Charset.Digit;
            ocrKing.FilePath = url;
            ocrKing.DoService();
            string number = ParseResult(ocrKing.OcrResult, ocrKing.ProcessStatus);
            if (number != "")
            {
                return int.Parse(number);
            }

            return 0;

        }

        public static string GetValue(string url)
        {
            ocrKing.Type = "http://www.unknown.com";
            ocrKing.Language = Language.Eng;
            ocrKing.Charset = Charset.Digit;
            ocrKing.FilePath = url;
            ocrKing.DoService();
            string value = ParseResult(ocrKing.OcrResult, ocrKing.ProcessStatus);
            return value;

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

        private static string ParseResult(string result, bool processStats)
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
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(result);
                    xdoc.Save("d:\\log\\" + Guid.NewGuid() + ".xml");
                    return "";

                }

            }
            else
            {
                // 识别结果
                return result;
            }
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    }
}
