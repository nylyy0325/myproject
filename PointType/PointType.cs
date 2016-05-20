using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Data;
namespace PointType
{
    public class PointType
    {

        //static int x = 4;
        //static int y = 11;
        //static int width = 0;
        //static int height = 2;


        //static int x = 6;
        //static int y = 0;
        //static int width = 0;
        //static int height = 2;


        static DataTable dt;
        public static int[] GetPointValue(PointType.Coordinate type)
        {
            if (dt == null)
            {
                string xmlurl = System.Windows.Forms.Application.StartupPath + @"\AllPoint.xml";
                XmlHelper help = new XmlHelper(xmlurl);
                dt = help.GetData("AllPoint");

            }

            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            dt.DefaultView.RowFilter = "TypeId = " + (int)type;
            x = int.Parse(dt.DefaultView[0]["PointX"].ToString());
            y = int.Parse(dt.DefaultView[0]["PointY"].ToString());
            width = int.Parse(dt.DefaultView[0]["Width"].ToString());
            height = int.Parse(dt.DefaultView[0]["Height"].ToString());

            if (width == 0 && height == 0)
            {
                return new int[] { x, y };

            }
            else
            {
                return new int[] { x, y, width, height };

            }

            #region 旧方法
            //if (type == Coordinate.FirstPricePoint)
            //    //return new int[] { 760 - x, 430 - y };
            //    return new int[] { 718, 412 };
            //else if (type == Coordinate.LowestPricePoint)
            //    //return new int[] { 683, 282, 55, 20 };
            //    //return new int[] { 196 - x, 426 - y, 42 - width, 13 + height };
            //    return new int[] { 190, 409, 42, 17 };
            //else if (type == Coordinate.FirstPullPricePoint)
            //    //return new int[] { 850 - x, 430 - y };
            //    return new int[] { 841, 411 };
            //else if (type == Coordinate.AddMoney)
            //    return new int[] { 600 };
            //else if (type == Coordinate.RandomTextBox)
            //    // return new int[] { 730 - x, 410 - y };
            //    return new int[] { 691, 396 };
            //else if (type == Coordinate.MyPrice)
            //    return new int[] { 715 - x, 340 - y, 50 - width, 17 + height };
            //else if (type == Coordinate.PriceStart)
            //    return new int[] { 266 - x, 459 - y, 41 - width, 13 + height };
            //else if (type == Coordinate.PriceEnd)
            //    //return new int[] { 321 - x, 459 - y, 41 - width, 13 + height };
            //    return new int[] { 315, 443, 42, 17 };
            //else if (type == Coordinate.FinallyPullPrice)
            //    //return new int[] { 600 - x, 500 - y };
            //    return new int[] { 603, 491 };
            //else if (type == Coordinate.RandomPic)
            //    //return new int[] { 780 - x, 400 - y, 120 - width, 30 + height };
            //    return new int[] { 774, 387, 120, 30 };
            //else if (type == Coordinate.RandomSubHowMuch)
            //    //return new int[] { 600 - x, 445 - y, 220 - width, 20 + height };
            //    return new int[] { 610, 424, 220, 30 };
            //return new int[] { };
            #endregion
        }
        public enum Coordinate
        {

            //第一页面 首次录入价格的文本框坐标 760, 430
            FirstPricePoint = 1,
            //第一页面 最低成交价截图坐标 680, 280, 57, 20
            LowestPricePoint = 2,
            // 第一页面 出价按钮坐标 850, 430
            FirstPullPricePoint = 3,
            //第一页面 出价按钮坐标 850, 430
            AddMoney = 4,
            //第二页面 验证码输入框
            RandomTextBox = 5,
            //第二页面 我的出价（防止第一页面的出价没有传到控制台）
            MyPrice = 6,
            //价格区间起始价格
            PriceStart = 7,
            //价格区间结束价格
            PriceEnd = 8,
            //最后价格提交按钮
            FinallyPullPrice = 9,
            //第二页面验证码图片
            RandomPic = 10,
            //第二页面截取验证码位数
            RandomSubHowMuch = 11,
            //第二页面验证码位数
            AutoNumberSubString = 12,
            //第一页面自定义加价输入框
            AddMoneyTextBox = 16,
            //第一页面自定义加价按钮
            AddMoneyButton = 17,
            //第二页面提示按钮
            MessageBoxShow = 18

        }




    }
}
