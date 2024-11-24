using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Diagnostics;

namespace NekoTimer
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
        StartWindow StartWindowOBJ = new StartWindow();
        Thread StartClock;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hide();
            StartWindowOBJ.Show();
            InitUI();

            Thread.Sleep(1000);
            StartWindowOBJ.Hide();
            Show();
            StartClock = new Thread(StartClockFunc);
            StartClock.IsBackground = true;
            StartClock.Start();
        }
        void StartClockFunc()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Dispatcher.Invoke(() => {
                    tb_TimeShow.Text = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "  " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
                });
            }
        }
        void InitUI()
        {
            SetBTNAn(btn_Mode_1);
            SetBTNAn(btn_Mode_2);
            SetBTNAn(btn_Mode_3);
            SetBTNAn(btn_Pause);
            SetBTNAn(btn_Reset);
            SetBTNAn(btn_Start);
            SetBTNLGAn(btn_SetBG);
            SetBTNLGAn(btn_EXIT);
            SetBTNAn(btn_s_Sound);

            InitBTNCK(btn_EXIT);
            InitBTNCK(btn_Mode_1);
            InitBTNCK(btn_Mode_2);
            InitBTNCK(btn_Mode_3);
            InitBTNCK(btn_Pause);
            InitBTNCK(btn_Reset);
            InitBTNCK(btn_Start);
            InitBTNCK(btn_s_Sound);
        }
        void InitBTNCK(Border obj)
        {
            obj.MouseDown += (a, b) => BTNCK(obj);
            obj.TouchDown += (a, b) => BTNCK(obj);
        }
        int ModeFlag = 1;
        int RunFlag = 0;
        bool Sound = true;
        bool ISStart = false;
        Thread ClockCore;
        void BTNCK(Border obj)
        {
            try
            {
                if (obj == btn_Mode_1)
                {
                    if (ClockCore != null)
                    {
                        ClockCore.Abort();
                        ClockCore = null;
                    }
                    ISStart = false;
                    ClockCore = null;
                    ModeFlag = 1;
                }
                else if (obj == btn_Mode_2)
                {
                    if (ClockCore != null)
                    {
                        ClockCore.Abort();
                        ClockCore = null;
                    }
                    ISStart = false;
                    ClockCore = null;
                    ModeFlag = 2;
                }
                else if (obj == btn_Mode_3)
                {
                    if (ClockCore != null)
                    {
                        ClockCore.Abort();
                        ClockCore = null;
                    }
                    ISStart = false;
                    ClockCore = null;
                    ModeFlag = 3;
                }
                else if (obj == btn_EXIT)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else if (obj == btn_Start)
                {
                    if (StartClock != null)
                    {
                        StartClock.Abort();
                        StartClock = null;
                    }
                    if (ClockCore != null)
                    {
                        ClockCore.Abort();
                        ClockCore = null;
                    }

                    if (H == 0 && M == 0 && S == 0 && ModeFlag == 2)
                    {
                        isok1 = int.TryParse (tb_i_H.Text, out H);
                        isok2 = int.TryParse (tb_i_M.Text, out M);
                        isok3 = int.TryParse (tb_i_S.Text, out S);
                        if (isok1 == false || isok2 == false || isok3 == false)
                        {
                            MessageBox.Show ("输入类型错误" + isok1 + isok2 + isok3);
                        }
                    }
                    else if (ModeFlag == 3)
                    {
                        H = M = S = 0;
                    }
                    

                    RunFlag = 1;
                    ClockCore = new Thread(ClockCoreFunc);
                    ClockCore.IsBackground = true;
                    ClockCore.Start();
                    ISStart = true;

                }
                else if (obj == btn_Pause)
                {
                    if (ISStart == true)
                    {
                        RunFlag = 2;
                        ClockCore.Abort();
                        ClockCore = null;
                        ISStart = false;
                    }
                    else
                    {

                    }
                }
                else if (obj == btn_Reset)
                {
                    isok1 = int.TryParse(tb_i_H.Text, out H);
                    isok2 = int.TryParse(tb_i_M.Text, out M);
                    isok3 = int.TryParse(tb_i_S.Text, out S);
                    if (isok1 == false || isok2 == false || isok3 == false)
                    {
                        MessageBox.Show("输入类型错误" + isok1 + isok2 + isok3);
                    }
                    RunFlag = 3;
                    ISStart = false;
                    if (ClockCore != null)
                    {
                        ClockCore.Abort();
                        ClockCore = null;
                    }
                    if (ClockCore != null)
                    {
                        ClockCore.Abort();
                        ClockCore = null;
                    }
                    ClockCore = new Thread(ClockCoreFunc);
                    ClockCore.IsBackground = true;
                    ClockCore.Start();
                    ISStart = true;
                }
                else if (obj == btn_SetBG)
                {

                }
                else if (obj == btn_s_Sound)
                {
                    Sound = !Sound;
                }
                SetColorFromMode();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool isok1;
        bool isok2;
        bool isok3;
        int H, M, S = 0;
        void ClockCoreFunc()
        {
            while (true)
            {
                if (ModeFlag == 1)
                {
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() => {
                        tb_TimeShow.Text = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "  " + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
                    });
                }
                else if (ModeFlag == 2)
                {
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() => {
                        tb_TimeShow.Text = H.ToString().PadLeft(2, '0') + ":" + M.ToString().PadLeft(2, '0') + ":" + S.ToString().PadLeft(2, '0');
                    });
                    S--;
                    if (S < 0)
                    {

                        M--;
                        S = 59;
                        
                    }
                    if (M < 0)
                    {
                        H--;
                        M = 59;
                    }
                    if (H < 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Console.Beep();
                            Thread.Sleep(500);
                        }
                        MessageBox.Show("OK");
                        H = M = S = 1;
                        RunFlag = 2;                
                        Dispatcher.Invoke(()=> { 
                            SetColorFromMode();
                            isok1 = int.TryParse(tb_i_H.Text, out H);
                            isok2 = int.TryParse(tb_i_M.Text, out M);
                            isok3 = int.TryParse(tb_i_S.Text, out S);
                            if (isok1 == false || isok2 == false || isok3 == false)
                            {
                                MessageBox.Show("输入类型错误" + isok1 + isok2 + isok3);
                            }
                            if (ClockCore != null)
                            {
                                ClockCore.Abort();
                                ClockCore = null;
                            }
                            ISStart = false;
                        });
                        
                    }
                    
                }
                else if (ModeFlag == 3)
                {
                    Thread.Sleep (1000);
                    Dispatcher.Invoke (() => {
                        tb_TimeShow.Text = H.ToString ().PadLeft (2, '0') + ":" + M.ToString ().PadLeft (2, '0') + ":" + S.ToString ().PadLeft (2, '0');
                    });
                    S++;
                    if (S > 59)
                    {
                        M++;
                        S = 0;
                    }
                    if (M > 59)
                    {
                        H++;
                        M = 0;
                    }
                }
            }
            
        }
        void SetBTNLGAn(Border obj)
        {
            obj.MouseEnter += (a, b) => MouseIN(obj);
            obj.MouseLeave += (a, b) => MouseOUTLG(obj);
        }
        void SetBTNAn(Border obj)
        {
            SetColorFromMode();
        }
        void MouseIN(Border obj)
        {   
            Brush brush = new SolidColorBrush(Color.FromArgb(20, 90, 90, 90));
            obj.Background = brush;
        }
        void MouseOUT()
        {
            SetColorFromMode();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = H.ToString().PadLeft(2, '0') + ":" + M.ToString().PadLeft(2, '0') + ":" + S.ToString().PadLeft(2, '0');
            lb_record.Items.Add(str);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lb_record.Items.Clear();
        }

        private void btn_SetBG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string imgpath = string.Format("pack://application:,,,/计时器Pro;component/Img/{0}.png", (lb_BG.SelectedItem as ListBoxItem).Content);

            img_bg.ImageSource = new BitmapImage(new Uri(imgpath));
        }

        void MouseOUTLG(Border obj)
        {
            Brush brush = new SolidColorBrush(Color.FromArgb(40, 187, 187, 187));
            obj.Background = brush;
        }
        void SetColorFromMode()
        {
            Brush brush = new SolidColorBrush(Color.FromArgb(40, 187, 187, 187));
            Brush brushRD = new SolidColorBrush(Color.FromArgb(40, 255, 0, 0));
            if (ModeFlag == 0)
            {
                btn_Mode_1.Background = brush;
                btn_Mode_2.Background = brush;
                btn_Mode_3.Background = brush;
            }
            else if (ModeFlag == 1)
            {
                btn_Mode_1.Background = brushRD;
                btn_Mode_2.Background = brush;
                btn_Mode_3.Background = brush;
            }
            else if (ModeFlag == 2)
            {
                btn_Mode_2.Background = brushRD;
                btn_Mode_1.Background = brush;
                btn_Mode_3.Background = brush;
            }
            else if (ModeFlag == 3)
            {
                btn_Mode_3.Background = brushRD;
                btn_Mode_1.Background = brush;
                btn_Mode_2.Background = brush;
            }
            if (RunFlag == 1)
            {
                btn_Reset.Background = brush;
                btn_Start.Background = brushRD;
                btn_Pause.Background = brush;
            }
            else if (RunFlag == 2)
            {
                btn_Reset.Background = brush;
                btn_Start.Background = brush;
                btn_Pause.Background = brushRD;
            }
            else if (RunFlag == 3)
            {
                btn_Reset.Background = brushRD;
                btn_Start.Background = brush;
                btn_Pause.Background = brush;
            }
            if (Sound == true)
            {
                btn_s_Sound.Background = brushRD;
            }
            else
            {
                btn_s_Sound.Background = brush;
            }
        }
    }
}
