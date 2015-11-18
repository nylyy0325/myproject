using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetLocation
{
    public partial class Form1 : Form
    {
        MouseHook mouseHook = new MouseHook();
        public Form1()
        {
            InitializeComponent();
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {


            //AddMouseEvent(
            //    "MouseDown",
            //    e.Button.ToString(),
            //    e.X.ToString(),
            //    e.Y.ToString(),
            //    ""
            //    );

            MessageBox.Show(e.X.ToString() + "$" + e.Y.ToString());


        }
        void AddMouseEvent(string eventType, string button, string x, string y, string delta)
        {

            //listView1.Items.Insert(0,
            //    new ListViewItem(
            //        new string[]{  
            //                eventType,   
            //                button,  
            //                x,  
            //                y,  
            //                delta  
            //            }));

        }




    }
}
