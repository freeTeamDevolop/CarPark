using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WebFramework.Framework.Utilities.AES;
using WebFramework.Framework.Utilities.HttpMethod;
using WebFramework.Infrastructure.Ulitily.Security;

namespace AlgorihmTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                two.Text = ReversibleAlgorihm.EncryptStringAes(one.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                two.Text = ReversibleAlgorihm.DecryptStringAes(one.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {


                Dictionary<string, string> valuePair = new Dictionary<string, string>();
                valuePair.Add("status", "success");
                valuePair.Add("shid", "");
                valuePair.Add("bb", "1.0");
                valuePair.Add("zftd", "");
                valuePair.Add("ddh", "");
                valuePair.Add("je", "");
                valuePair.Add("ddmc", "");
                valuePair.Add("ddbz", "");
                valuePair.Add("ybtz", "");
                valuePair.Add("tbtz", "");

                valuePair.Add("sign", "");
                string postUrl = "";
                HttpHelper.HttpPost(postUrl, valuePair);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //one.Text = "shid={0}&bb={1}&zftd={2}&ddh={3}&je={4}&ddmc={5}&ddbz={6}&ybtz={7}&tbtz={8}&key={9}&url={10}";
        }

        public Dictionary<string, string> getLinkToPara()
        {
            Dictionary<string, string> valuePair = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(one.Text))
            {

            }
            return valuePair;
        }

        public Dictionary<string, string> ParseUrltoDic(string url)

        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            int QueryIndex = url.IndexOf('?');

            if (QueryIndex > -1)

            {

                string parastr = url.Substring(QueryIndex + 1);

                //开始分析参数  

                Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);

                MatchCollection mc = re.Matches(parastr);

                foreach (Match m in mc)

                {

                    dic.Add(m.Result("$2").ToLower(), m.Result("$3"));
                }
            }

            return dic;

        }


        public Dictionary<string, string> ParseUrltoDics(string url)

        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(url))

            {

                string parastr = url;

                //开始分析参数  

                Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);

                MatchCollection mc = re.Matches(parastr);

                foreach (Match m in mc)

                {

                    dic.Add(m.Result("$2").ToLower(), m.Result("$3"));
                }
            }

            return dic;

        }




        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }


        private void Button_Click_Decrypt(object sender, RoutedEventArgs e)
        {
            try
            {
                AesText.Text = AESHelper.DecryptString(AesText.Text, AESHelper.Key128); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Button_Click_Encrypt(object sender, RoutedEventArgs e)
        {
            try
            {
                AesText.Text = AESHelper.EncryptString(AesText.Text, AESHelper.Key128); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
