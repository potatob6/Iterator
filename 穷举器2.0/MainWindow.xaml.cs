using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace 穷举器2._0
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public int inLines = 1;

        public string[,] dim = new string[32767,2];
        private void button_Click(object sender, RoutedEventArgs e)
        {
            inLines = 1;
            string[] a = textBox.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None); 
            for (int i = 0; i < a.Length; i++)
            {
                if(new Regex("^[[]").IsMatch(a[i]))
                {
                    string[] onedim = a[i].Split('=','[',']');
                    dim[i, 0] = onedim[0];
                    dim[i, 1] = onedim[1];
                }
            }

            for (int i = 0; i < a.Length; i++)
            {
                if(new Regex("^for").Match(a[i]).Success)
                {
                    inLines = i + 1;
                    break;
                }
                
            }
            if (inLines >= 1&&a.Length>1)
            {
                if (new Regex("^print:").Match(a[inLines]).Success)
                {

                    string prtCmd = "";
                    string[] print = a[inLines].Split(new string[] { ":", "\r\n" }, StringSplitOptions.None);
                    //合并3个以上的冒号
                    if (print.Length >= 3)
                    {
                        for (int tgtPritnt = 1; tgtPritnt < print.Length; tgtPritnt++)
                        {
                            if (tgtPritnt == print.Length - 1)
                            {
                                prtCmd += print[tgtPritnt];
                            }
                            else
                            {
                                prtCmd += print[tgtPritnt] + ":";
                            }

                        }
                    }
                    else
                    {
                        prtCmd = print[1];
                    }


                    string[] Cmds = a[inLines - 1].Split(new string[] { "for", " ", "step", "to" }, StringSplitOptions.None);
                    if (new Regex("[0-9]+").Match(Cmds[2]).Success && new Regex("[0-9]+").Match(Cmds[5]).Success && new Regex("[0-9]+").Match(Cmds[8]).Success)
                    {
                        textBox.Text += "\n\n\n\n\n\n输出:\n\n";


                        for (double stt = double.Parse(Cmds[2]); stt <= double.Parse(Cmds[5]); stt += double.Parse(Cmds[8]))
                        {
                            
                            string newpttcmd = prtCmd.Replace("#i", stt.ToString());
                            
                            textBox.Text += newpttcmd + "\n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("请检查第一行代码,错误的数字或者空格过多");
                    }

                }
                else
                {
                    MessageBox.Show("对不起，第二行命令错误");
                }

            }
            else
            {
                MessageBox.Show("命令错误");
            }




        }

        public void ChangeSize()
        {
            textBox.Width = this.Width;
            textBox.Height = this.Height - button.Height-30-10;
            button.Width = this.Width;
            button.Margin = new Thickness(0, this.Height - button.Height - 40, 0, 0);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ChangeSize();
        }
    }
}
