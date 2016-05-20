using AvensLab.Service;
using AvensLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using System.Text;
using System.Xml;


namespace MyOcrKing
{
    public class MyOrc
    {
        private XmlDocument doc;
        public MyOrc(string ApiKey)
        {
            this.ApiKey = ApiKey;

        }

        public string GetSubString(int x, int y, int width, int height, string url)
        {
            var ocrKing = new OcrKing(ApiKey)
            {
                //Language = Language.Eng, 
                //Service = Service.OcrKingForCaptcha, 
                //Charset = Charset.DigitLowerUpper

                Language = Language.Sim,
                Service = Service.OcrKingForCaptcha,
                Charset = Charset.CommonWords
            };
            Bitmap btm = captureScreen(x, y, width, height);
            btm.Save(url, ImageFormat.Jpeg);
            ocrKing.Type = "http://www.nopreprocess.com";
            ocrKing.FilePath = url;
            ocrKing.DoService();
            string resault = this.ParseResult(ocrKing.OcrResult, ocrKing.ProcessStatus);
            return resault;

        }


        public int GetNumber(int x, int y, int width, int height, string url)
        {
            var ocrKing = new OcrKing(ApiKey)
            {
                //Language = Language.Eng, 
                //Service = Service.OcrKingForCaptcha, 
                //Charset = Charset.DigitLowerUpper

                Language = Language.Eng,
                Service = Service.OcrKingForCaptcha,
                Charset = Charset.DigitLower
            };

            Bitmap btm = captureScreen(x, y, width, height);
            btm.Save(url, ImageFormat.Jpeg);
            ocrKing.Type = "http://www.nopreprocess.com";
            ocrKing.FilePath = url;
            ocrKing.DoService();
            string number = this.ParseResult(ocrKing.OcrResult, ocrKing.ProcessStatus);
            if (number != "")
                return Convert.ToInt32(number);

            return 0;



        }

        public string ApiKey { get; set; }

        public static Bitmap captureScreen(int x, int y, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format64bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(x, y), new Point(0, 0), bmp.Size);
                g.Dispose();
            }
            Bitmap newbm = new Bitmap(bmp, width, height);
            //bit.Save(@"capture2.png");
            return newbm;
        }

        private string ParseResult(string result, bool processStats)
        {
            //if (processStats)
            //{
            //    // 解析结果
            //    //try
            //    //{
            //    //    int startindex = result.IndexOf("<Result>");
            //    //    string resultstring = result.Substring(startindex, 13).Replace("<Result>", "");
            //    //    return resultstring;
            //    //    //this.doc.LoadXml(result);

            //    //    // 识别结果
            //    //    //this.textBox3.Text = this.doc.SelectSingleNode("//Results/ResultList/Item/Result").InnerText;
            //    //}
            //    //catch
            //    //{
            //    //    return "";

            //    //}

            //    // 解析结果
            //    this.doc.LoadXml(result);

            //    // 识别结果
            //    return this.doc.SelectSingleNode("//Results/ResultList/Item/Result").InnerText;

            //}
            //else
            //{
            //    // 识别结果
            //    return result;
            //}

            if (processStats)
            {
                doc = new XmlDocument();
                // 解析结果
                this.doc.LoadXml(result);

                // 识别结果
                return this.doc.SelectSingleNode("//Results/ResultList/Item/Result").InnerText;

                // 原始图片
                //this.pbSrcFile.Image =
                //    GetBitmap(this.doc.SelectSingleNode("//Results/ResultList/Item/SrcFile").InnerText);

                //// 处理后图片
                //this.pbDesFile.Image =
                //    GetBitmap(this.doc.SelectSingleNode("//Results/ResultList/Item/DesFile").InnerText);
            }
            else
            {
                // 识别结果
                return result;
            }

        }

    }


}
