using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Gas_Monitoring2.Models;
using MySql.Data.MySqlClient;
using Gas_Monitoring2.Views;
using System.Collections.ObjectModel;
using DevExpress.Xpf.LayoutControl;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.IO;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class HomeViewModel
    {
        public virtual ObservableCollection<SimpleView> Items { get; set; }
        public virtual string Img_src { get; set; }
        public virtual string Banner_src { get; set; }
        public void HomeVIewModel()
        {
        }
        public void Init()
        {
            Console.WriteLine("HomeVM Init");
            Banner_src = Path.GetFullPath("../../Images/banner.png");
            Img_src = Path.GetFullPath("../../Images/ci.png");
            Console.WriteLine(Img_src);
            Items = new ObservableCollection<SimpleView>();
            //디바이스 수만큼 오버뷰 생성
            foreach (TabViewModel vm in MainViewModel.Dev_list)
            {
                SimpleView tmp_view = new SimpleView();
                tmp_view.DataContext = vm;
                Items.Add(tmp_view);
            }

        }
        public void Pop_Help()
        {
            try
            {
                Console.WriteLine("Help Click");
                System.Diagnostics.Process.Start(Path.GetFullPath("../../help.html"));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void Pop_Setting()
        {
            Console.WriteLine("Set Click");
            Messenger.Default.Send<bool, MainViewModel>(true);
        }
    }
}