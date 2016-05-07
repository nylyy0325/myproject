namespace paipai
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.moni_min = new System.Windows.Forms.ComboBox();
            this.moni_hour = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.personidy = new System.Windows.Forms.TextBox();
            this.personidx = new System.Windows.Forms.TextBox();
            this.bookpasswordy = new System.Windows.Forms.TextBox();
            this.bookpasswordx = new System.Windows.Forms.TextBox();
            this.bookidy = new System.Windows.Forms.TextBox();
            this.bookidx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_second = new System.Windows.Forms.Button();
            this.btn_first = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox_selectitem = new System.Windows.Forms.ComboBox();
            this.textBox_selectvalue = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.keycount_label = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(75, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 334);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.keycount_label);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.moni_min);
            this.tabPage1.Controls.Add(this.moni_hour);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(355, 308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "策略选择";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(295, 58);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "更新";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "策略选择：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "模拟启动时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "一次性开关：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 4;
            // 
            // moni_min
            // 
            this.moni_min.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moni_min.FormattingEnabled = true;
            this.moni_min.Location = new System.Drawing.Point(177, 145);
            this.moni_min.Name = "moni_min";
            this.moni_min.Size = new System.Drawing.Size(64, 20);
            this.moni_min.TabIndex = 3;
            this.moni_min.SelectedIndexChanged += new System.EventHandler(this.moni_min_SelectedIndexChanged);
            // 
            // moni_hour
            // 
            this.moni_hour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moni_hour.FormattingEnabled = true;
            this.moni_hour.Location = new System.Drawing.Point(101, 145);
            this.moni_hour.Name = "moni_hour";
            this.moni_hour.Size = new System.Drawing.Size(70, 20);
            this.moni_hour.TabIndex = 2;
            this.moni_hour.SelectedIndexChanged += new System.EventHandler(this.moni_hour_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(8, 111);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "模拟";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.personidy);
            this.tabPage2.Controls.Add(this.personidx);
            this.tabPage2.Controls.Add(this.bookpasswordy);
            this.tabPage2.Controls.Add(this.bookpasswordx);
            this.tabPage2.Controls.Add(this.bookidy);
            this.tabPage2.Controls.Add(this.bookidx);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(355, 308);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "登录坐标截取";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // personidy
            // 
            this.personidy.Location = new System.Drawing.Point(194, 130);
            this.personidy.Name = "personidy";
            this.personidy.Size = new System.Drawing.Size(100, 21);
            this.personidy.TabIndex = 8;
            // 
            // personidx
            // 
            this.personidx.Location = new System.Drawing.Point(88, 130);
            this.personidx.Name = "personidx";
            this.personidx.Size = new System.Drawing.Size(100, 21);
            this.personidx.TabIndex = 7;
            // 
            // bookpasswordy
            // 
            this.bookpasswordy.Location = new System.Drawing.Point(194, 88);
            this.bookpasswordy.Name = "bookpasswordy";
            this.bookpasswordy.Size = new System.Drawing.Size(100, 21);
            this.bookpasswordy.TabIndex = 6;
            // 
            // bookpasswordx
            // 
            this.bookpasswordx.Location = new System.Drawing.Point(88, 88);
            this.bookpasswordx.Name = "bookpasswordx";
            this.bookpasswordx.Size = new System.Drawing.Size(100, 21);
            this.bookpasswordx.TabIndex = 5;
            // 
            // bookidy
            // 
            this.bookidy.Location = new System.Drawing.Point(194, 43);
            this.bookidy.Name = "bookidy";
            this.bookidy.Size = new System.Drawing.Size(100, 21);
            this.bookidy.TabIndex = 4;
            // 
            // bookidx
            // 
            this.bookidx.Location = new System.Drawing.Point(88, 43);
            this.bookidx.Name = "bookidx";
            this.bookidx.Size = new System.Drawing.Size(100, 21);
            this.bookidx.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "身份证";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "标书密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标书号";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.btn_second);
            this.tabPage3.Controls.Add(this.btn_first);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.comboBox_selectitem);
            this.tabPage3.Controls.Add(this.textBox_selectvalue);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(355, 308);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "快速登录";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(139, 270);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_second
            // 
            this.btn_second.Location = new System.Drawing.Point(218, 270);
            this.btn_second.Name = "btn_second";
            this.btn_second.Size = new System.Drawing.Size(75, 23);
            this.btn_second.TabIndex = 5;
            this.btn_second.Text = "二次同步";
            this.btn_second.UseVisualStyleBackColor = true;
            this.btn_second.Click += new System.EventHandler(this.btn_second_Click);
            // 
            // btn_first
            // 
            this.btn_first.Location = new System.Drawing.Point(58, 270);
            this.btn_first.Name = "btn_first";
            this.btn_first.Size = new System.Drawing.Size(75, 23);
            this.btn_first.TabIndex = 4;
            this.btn_first.Text = "首次同步";
            this.btn_first.UseVisualStyleBackColor = true;
            this.btn_first.Click += new System.EventHandler(this.btn_first_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(274, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // comboBox_selectitem
            // 
            this.comboBox_selectitem.FormattingEnabled = true;
            this.comboBox_selectitem.Items.AddRange(new object[] {
            "标书号",
            "标书密码"});
            this.comboBox_selectitem.Location = new System.Drawing.Point(6, 34);
            this.comboBox_selectitem.Name = "comboBox_selectitem";
            this.comboBox_selectitem.Size = new System.Drawing.Size(69, 20);
            this.comboBox_selectitem.TabIndex = 2;
            // 
            // textBox_selectvalue
            // 
            this.textBox_selectvalue.Location = new System.Drawing.Point(81, 34);
            this.textBox_selectvalue.Name = "textBox_selectvalue";
            this.textBox_selectvalue.Size = new System.Drawing.Size(187, 21);
            this.textBox_selectvalue.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(343, 188);
            this.dataGridView1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "验证码已触发个数：";
            // 
            // keycount_label
            // 
            this.keycount_label.AutoSize = true;
            this.keycount_label.Location = new System.Drawing.Point(125, 184);
            this.keycount_label.Name = "keycount_label";
            this.keycount_label.Size = new System.Drawing.Size(0, 12);
            this.keycount_label.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 358);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "控制台";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox personidy;
        private System.Windows.Forms.TextBox personidx;
        private System.Windows.Forms.TextBox bookpasswordy;
        private System.Windows.Forms.TextBox bookpasswordx;
        private System.Windows.Forms.TextBox bookidy;
        private System.Windows.Forms.TextBox bookidx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox_selectitem;
        private System.Windows.Forms.TextBox textBox_selectvalue;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_second;
        private System.Windows.Forms.Button btn_first;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox moni_min;
        private System.Windows.Forms.ComboBox moni_hour;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label keycount_label;
        private System.Windows.Forms.Label label8;


    }
}

