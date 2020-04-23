using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gas_Monitoring2.Models
{ 
    // Attached Property 생성해서 Web Browser 속성에 바인딩 하기 위한 클래스
    public class WebBrowserUtility
    {
        // Source에 Uri를 바인딩
        public static readonly DependencyProperty UriSourceProperty = DependencyProperty.RegisterAttached("UriSource", typeof(string), 
            typeof(WebBrowserUtility), new UIPropertyMetadata(null, UriSourcePropertyChanged));
        // ObjectForScript에 Object를 바인딩
        public static readonly DependencyProperty ObjectForScriptingProperty = DependencyProperty.RegisterAttached("ObjectForScripting", typeof(object), 
            typeof(WebBrowserUtility), new UIPropertyMetadata(OnObjectForScriptingPropertyChanged));

        public static string GetUriSource(DependencyObject obj)
        {
            return (string)obj.GetValue(UriSourceProperty);
        }
        public static void SetUriSource(DependencyObject obj, string value)
        {
            //UriSourcePropertyChanged 호출
            obj.SetValue(UriSourceProperty, value);
        }
        public static void UriSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var browser = o as WebBrowser;
            if (browser != null)
            {
                var uri = e.NewValue as string;
                var url = CorrectionToUri(uri);
                browser.Source = !String.IsNullOrEmpty(url) ? new Uri(url) : null;
            }
        }
        //Uri 사용 프로토콜 명시 여부 확인하는 함수(http://)
        private static string CorrectionToUri(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return "about:blank";
            }
            if (!url.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
            {
                return "http://" + url;
            }
            return url;
        }
        public static string GetObjectForScripting(DependencyObject obj)
        {
            return (string)obj.GetValue(ObjectForScriptingProperty);
        }
        public static void SetObjectForScripting(DependencyObject obj, string value)
        {
            obj.SetValue(ObjectForScriptingProperty, value);
        }
        public static void OnObjectForScriptingPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                browser.ObjectForScripting = e.NewValue;
            }
        }
    }
}

