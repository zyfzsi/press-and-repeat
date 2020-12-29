using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace App_csharp
{
    public partial class Form1 : Form
    {
        private CDD dd;
        private int input_keycode = 0;
        private int output_keycode = 0;

        public Form1()
        {
            InitializeComponent();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            this.button_start.Enabled = false;

            //reg_hotkey();                            // 注册热键

            dd = new CDD();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "DD|*.DLL";

            ofd.FileName = Environment.CurrentDirectory + @"\DD94687.64.dll";
            LoadDllFile(ofd.FileName);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //unreg_hotkey();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button_start.Text == "按键启动！")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button_start.Text = "按键关闭！";
                reg_hotkey();
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button_start.Text = "按键启动！";
                unreg_hotkey();
            }
        }

        private void LoadDllFile(string dllfile)
        {
            button_start.Enabled = false;


            int ret = dd.Load(dllfile);
            if (ret !=1) { MessageBox.Show("Load Error"); System.Environment.Exit(0); return; }

            //button_start.Enabled = true;

            return;
        }


        #region "HotKey"
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(
         IntPtr hWnd,
         int id,                            
         KeyModifiers modkey,    
         int vk                         
        );
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(
         IntPtr hWnd,              
         int id                          
        );

        void reg_hotkey()
        {
            RegisterHotKey(this.Handle, 1, 0, input_keycode);
        }

        void unreg_hotkey()
        {
            UnregisterHotKey(this.Handle, 1);
        }

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        public static bool IsKeyPressed(int testKey)
        {
            bool keyPressed = false;
            short result = GetAsyncKeyState(testKey);
            Console.WriteLine(result);
            switch (result)
            {
                case 0:
                    keyPressed = false;
                    break;

                case 1:
                    keyPressed = false;
                    break;

                default:
                    keyPressed = true;
                    break;
            }

            return keyPressed;
        }


        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;                      
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    while (true)
                    {
                        if (!IsKeyPressed(input_keycode))
                        {
                            return;
                        }
                        dd.key(output_keycode, 1);
                        dd.key(output_keycode, 2);
                        System.Threading.Thread.Sleep(Convert.ToInt32(textBox3.Text));

                    }
            }
            base.WndProc(ref m);
        }

        #endregion

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = "";
            if ((int)e.KeyCode == 229)
            {
                MessageBox.Show("获取失败，请切换英文输入法（中文输入法获取有bug）");
                
            } else { 
                textBox1.Text = e.KeyData.ToString();
                input_keycode = e.KeyValue;
                if (input_keycode != 0 && output_keycode != 0)
                {
                    button_start.Enabled = false;
                }
            }
            
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBox2.Text = "";
            if ((int)e.KeyCode == 229)
            {
                MessageBox.Show("获取失败，请切换英文输入法（中文输入法获取有bug）");

            }
            else
            {
                textBox2.Text = e.KeyData.ToString();
                output_keycode = get_keyvalue(e.KeyValue);
                if (output_keycode == 0)
                {
                    MessageBox.Show("键值获取失败，请尝试更换一个快捷键");
                }
                if (input_keycode != 0 && output_keycode!= 0)
                {
                    button_start.Enabled = true;
                } else
                {
                    button_start.Enabled = false;
                }
            }

            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public static int get_keyvalue(int vk_value)
        {
            int rs = 0;
            switch (vk_value)
            {
                case 65:
                    rs = 401;
                    break;
                case 66:
                    rs = 505;
                    break;
                case 67:
                    rs = 503;
                    break;
                case 68:
                    rs = 403;
                    break;
                case 69:
                    rs = 303;
                    break;
                case 70:
                    rs = 404;
                    break;
                case 71:
                    rs = 405;
                    break;
                case 72:
                    rs = 406;
                    break;
                case 73:
                    rs = 308;
                    break;
                case 74:
                    rs = 407;
                    break;
                case 75:
                    rs = 408;
                    break;
                case 76:
                    rs = 409;
                    break;
                case 77:
                    rs = 507;
                    break;
                case 78:
                    rs = 506;
                    break;
                case 79:
                    rs = 309;
                    break;
                case 80:
                    rs = 310;
                    break;
                case 81:
                    rs = 301;
                    break;
                case 82:
                    rs = 304;
                    break;
                case 83:
                    rs = 402;
                    break;
                case 84:
                    rs = 305;
                    break;
                case 85:
                    rs = 307;
                    break;
                case 86:
                    rs = 504;
                    break;
                case 87:
                    rs = 302;
                    break;
                case 88:
                    rs = 502;
                    break;
                case 89:
                    rs = 306;
                    break;
                case 90:
                    rs = 501;
                    break;
                case 48:
                    rs = 210;
                    break;
                case 49:
                    rs = 201;
                    break;
                case 50:
                    rs = 202;
                    break;
                case 51:
                    rs = 203;
                    break;
                case 52:
                    rs = 204;
                    break;
                case 53:
                    rs = 205;
                    break;
                case 54:
                    rs = 206;
                    break;
                case 55:
                    rs = 207;
                    break;
                case 56:
                    rs = 208;
                    break;
                case 57:
                    rs = 209;
                    break;
                case 112:
                    rs = 101;
                    break;
                case 113:
                    rs = 102;
                    break;
                case 114:
                    rs = 103;
                    break;
                case 115:
                    rs = 104;
                    break;
                case 116:
                    rs = 105;
                    break;
                case 117:
                    rs = 106;
                    break;
                case 118:
                    rs = 107;
                    break;
                case 119:
                    rs = 108;
                    break;
                case 120:
                    rs = 109;
                    break;
                case 121:
                    rs = 110;
                    break;
                case 122:
                    rs = 111;
                    break;
                case 123:
                    rs = 112;
                    break;
                case 27:
                    rs = 100;
                    break;
                case 192:
                    rs = 200;
                    break;
                case 9:
                    rs = 300;
                    break;
                case 17:
                    rs = 400;
                    break;
                case 16:
                    rs = 500;
                    break;
                case 20:
                    rs = 600;
                    break;
                case 189:
                    rs = 211;
                    break;
                case 187:
                    rs = 212;
                    break;
                case 220:
                    rs = 213;
                    break;
                case 8:
                    rs = 214;
                    break;
                case 219:
                    rs = 311;
                    break;
                case 221:
                    rs = 312;
                    break;
                case 186:
                    rs = 410;
                    break;
                case 222:
                    rs = 411;
                    break;
                case 13:
                    rs = 313;
                    break;
                case 188:
                    rs = 508;
                    break;
                case 190:
                    rs = 509;
                    break;
                case 191:
                    rs = 510;
                    break;
                case 32:
                    rs = 603;
                    break;
                case 44:
                    rs = 700;
                    break;
                case 145:
                    rs = 701;
                    break;
                case 19:
                    rs = 702;
                    break;
                case 45:
                    rs = 703;
                    break;
                case 36:
                    rs = 704;
                    break;
                case 33:
                    rs = 705;
                    break;
                case 46:
                    rs = 706;
                    break;
                case 35:
                    rs = 707;
                    break;
                case 34:
                    rs = 708;
                    break;
                case 38:
                    rs = 709;
                    break;
                case 37:
                    rs = 710;
                    break;
                case 40:
                    rs = 711;
                    break;
                case 39:
                    rs = 712;
                    break;
                case 96:
                    rs = 800;
                    break;
                case 97:
                    rs = 801;
                    break;
                case 98:
                    rs = 802;
                    break;
                case 99:
                    rs = 803;
                    break;
                case 100:
                    rs = 804;
                    break;
                case 101:
                    rs = 805;
                    break;
                case 102:
                    rs = 806;
                    break;
                case 103:
                    rs = 807;
                    break;
                case 104:
                    rs = 808;
                    break;
                case 105:
                    rs = 809;
                    break;
                case 144:
                    rs = 810;
                    break;
                case 111:
                    rs = 811;
                    break;
                case 106:
                    rs = 812;
                    break;
                case 109:
                    rs = 813;
                    break;
                case 107:
                    rs = 814;
                    break;
                case 108:
                    rs = 815;
                    break;
                case 110:
                    rs = 816;
                    break;


            }
            return rs;
        }
    }
}

