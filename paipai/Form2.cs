using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace paipai
{
    public partial class Form2 : Form
    {


        public static bool getpoint = false;
        public Form2()
        {
            this.Location = new Point(0, 0);
            InitializeComponent();
            //webBrowser1.Navigate("https://paimai2.alltobid.com/bid/2016072301/login.htm");
            webBrowser1.Navigate("http://moni.51hupai.org/?new=2");
            //webBrowser1.Navigate("http://test.alltobid.com/");
        }

        public void ReFlush()
        {
            webBrowser1.Navigate("http://moni.51hupai.org:8081/");

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void Form2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(getpoint ? "true" : "false");
        }
    }
}
