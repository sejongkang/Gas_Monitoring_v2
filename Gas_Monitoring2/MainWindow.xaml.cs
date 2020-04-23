using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core;
using System.IO;

namespace Gas_Monitoring2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Closing");
            if (DXMessageBox.Show(Application.Current.MainWindow, "정말로 종료하시겠습니까?", "프로그램 종료 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void ThemedWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key);
            switch(e.Key)
            {
                case Key.F1:
                    //System.Diagnostics.Process.Start(Path.GetFullPath("../../help.html"));
                    break;            
            }
        }
    }
}
