using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odds_Grabber___dafa888
{
    public partial class Main_Form : Form
    {
        private ChromiumWebBrowser chromeBrowser;
        private string __app = "Odds Grabber";
        private string __app_type = "{edit this}";
        private string __brand_code = "{edit this}";
        private string __brand_color = "#4F0005";
        private string __url = "www.sg88win.com";
        private string __website_name = "dafa888";
        private string __app__website_name = "";
        private string __api_key = "youdieidie";
        private string __running_01 = "dafa888";
        private string __running_11 = "Dafabet";
        private string __app_detect_running = "DAFA888";
        private string __local_ip = "";
        // Settings
        private string __root_url = "";
        private string __root_url_equals = "";
        private string __root_url_login = "";
        private string __DAFA_running = "";
        private string __DAFA_not_running = "";
        private string __username = "";
        private string __password = "";
        // End of Settings
        private int __send = 0;
        private int __r = 255;
        private int __g = 224;
        private int __b = 0;
        private bool __is_close;
        private bool __is_login = false;
        private bool __m_aeroEnabled;
        Form __mainFormHandler;

        // Drag Header to Move
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        // ----- Drag Header to Move

        // Form Shadow
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                __m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!__m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (__m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        // ----- Form Shadow

        public Main_Form()
        {
            InitializeComponent();

            // Settings
            __root_url = Properties.Settings.Default.______root_url.ToString().Replace("amp;", "");
            __root_url_equals = Properties.Settings.Default.______root_url_equals.ToString().Replace("amp;", "");
            __root_url_login = Properties.Settings.Default.______root_url_login.ToString().Replace("amp;", "");
            __DAFA_running = Properties.Settings.Default.______DAFA_running.ToString().Replace("amp;", "");
            __DAFA_not_running = Properties.Settings.Default.______DAFA_not_running.ToString().Replace("amp;", "");
            __username = Properties.Settings.Default.______username.ToString().Replace("amp;", "");
            __password = Properties.Settings.Default.______password.ToString().Replace("amp;", "");

            //MessageBox.Show(Properties.Settings.Default.______is_send_telegram.ToString() + "\n" + __root_url + "\n" + __root_url_equals + "\n" + __root_url_login + "\n" + __DAFA_running + "\n" + __DAFA_not_running + "\n" + __username + "\n" + __password);
            // End of Settings

            timer_landing.Start();
        }

        // Drag to Move
        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_loader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_brand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----- Drag to Move

        // Click Close
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                __is_close = true;
                Environment.Exit(0);
            }
        }

        // Click Minimize
        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        void ___CloseMessageBox()
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "JavaScript Alert - http://mem.sghuatchai.com");

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void timer_close_message_box_Tick(object sender, EventArgs e)
        {
            ___CloseMessageBox();
        }

        private void timer_size_Tick(object sender, EventArgs e)
        {
            __mainFormHandler = Application.OpenForms[0];
            __mainFormHandler.Size = new Size(466, 168);
        }

        // Form Closing
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!__is_close)
            {
                DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            __app__website_name = __app + " - dafa888";
            panel1.BackColor = Color.FromArgb(__r, __g, __b);
            panel2.BackColor = Color.FromArgb(__r, __g, __b);
            label_brand.BackColor = Color.FromArgb(__r, __g, __b);
            Text = __app__website_name;
            label_title.Text = __app__website_name;

            InitializeChromium();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.______is_send_telegram)
            {
                Properties.Settings.Default.______is_send_telegram = false;
                Properties.Settings.Default.Save();
                MessageBox.Show("Telegram Notification is Disabled.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Properties.Settings.Default.______is_send_telegram = true;
                Properties.Settings.Default.Save();
                MessageBox.Show("Telegram Notification is Enabled.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timer_landing_Tick(object sender, EventArgs e)
        {
            panel_landing.Visible = false;
            panel_cefsharp.Visible = false;
            pictureBox_loader.Visible = true;
            label_page_count.Visible = true;
            label_currentrecord.Visible = true;
            timer_size.Start();
            timer_landing.Stop();
        }

        public static void ___FlushMemory()
        {
            Process prs = Process.GetCurrentProcess();
            try
            {
                prs.MinWorkingSet = (IntPtr)(300000);
            }
            catch (Exception err)
            {
                // leave blank
            }
        }

        private void timer_flush_memory_Tick(object sender, EventArgs e)
        {
            ___FlushMemory();
        }

        private void SendMyBot(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "772918363:AAHn2ufmP3ocLEilQ1V-IHcqYMcSuFJHx5g";
                string chatId = "@allandrake";
                string text = "-----" + __app__website_name + "-----%0A%0AIP:%20ABC PC%0ALocation:%20Pacific%20Star%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        __Flag();
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        SendMyBot(message);
                    }
                }
                else
                {
                    SendMyBot(err.ToString());
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void SendABCTeam(string message)
        {
            if (Properties.Settings.Default.______is_send_telegram)
            {
                try
                {
                    string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                    string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                    string apiToken = "651945130:AAGMFj-C4wX0yElG2dBU1SRbfrNZi75jPHg";
                    string chatId = "@odds_bot_abc_team";
                    string text = "Bot:%20-----" + __website_name.ToUpper() + "-----%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20<b>" + message + "</>&parse_mode=html";
                    urlString = String.Format(urlString, apiToken, chatId, text);
                    WebRequest request = WebRequest.Create(urlString);
                    Stream rs = request.GetResponse().GetResponseStream();
                    StreamReader reader = new StreamReader(rs);
                    string line = "";
                    StringBuilder sb = new StringBuilder();
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                            sb.Append(line);
                    }
                    __send = 0;
                }
                catch (Exception err)
                {
                    __send++;

                    if (___CheckForInternetConnection())
                    {
                        if (__send == 5)
                        {
                            __Flag();
                            __is_close = false;
                            Environment.Exit(0);
                        }
                        else
                        {
                            SendABCTeam(message);
                        }
                    }
                    else
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void ___GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    __local_ip = ip.ToString();
                }
            }
        }

        private void timer_detect_running_Tick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(__local_ip))
            {
                ___DetectRunningAsync();
            }
            else
            {
                ___GetLocalIPAddress();
            }
        }

        private async void ___DetectRunningAsync()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __app_detect_running + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["bot_name"] = __app_detect_running,
                        ["last_update"] = datetime,
                        ["token"] = token,
                        ["my_ip"] = __local_ip
                    };

                    var response = wb.UploadValues("http://zeus.ssitex.com:8080/API/updateAppStatusABC", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunning2Async();
                    }
                }
                else
                {
                    SendMyBot(err.ToString());
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___DetectRunning2Async()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __app_detect_running + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["bot_name"] = __app_detect_running,
                        ["last_update"] = datetime,
                        ["token"] = token,
                        ["my_ip"] = __local_ip
                    };

                    var response = wb.UploadValues("http://zeus2.ssitex.com:8080/API/updateAppStatusABC", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunningAsync();
                    }
                }
                else
                {
                    SendMyBot(err.ToString());
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        // CefSharp Initialize
        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser(__root_url);
            panel_cefsharp.Controls.Add(chromeBrowser);
            chromeBrowser.AddressChanged += ChromiumBrowserAddressChanged;
        }

        // CefSharp Address Changed
        private void ChromiumBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            __url = e.Address.ToString();
            Invoke(new Action(() =>
            {
                //panel3.Visible = true;
                panel4.Visible = true;
            }));

            if (e.Address.ToString().Equals(__root_url_equals))
            {
                Invoke(new Action(() =>
                {
                    chromeBrowser.FrameLoadEnd += (sender_, args) =>
                    {
                        if (args.Frame.IsMain)
                        {
                            Invoke(new Action(() =>
                            {
                                __is_login = false;
                                args.Frame.ExecuteJavaScriptAsync("document.getElementsByName('user')[0].value = '" + __username + "';");
                                args.Frame.ExecuteJavaScriptAsync("document.getElementsByName('pwd')[0].value = '" + __password + "';");
                                args.Frame.ExecuteJavaScriptAsync("document.querySelector('#remoteloginformsubmit').click();");
                            }));
                        }
                    };
                }));
            }

            if (e.Address.ToString().Equals(__root_url_login) || e.Address.ToString().Equals("http://mem.sghuatchai.com/Public/Maintenance"))
            {
                Invoke(new Action(() =>
                {
                    chromeBrowser.FrameLoadEnd += (sender_, args) =>
                    {
                        if (args.Frame.IsMain)
                        {
                            Invoke(new Action(() =>
                            {
                                if (!__is_login)
                                {
                                    if (e.Address.ToString().Equals("http://mem.sghuatchai.com/Public/Maintenance"))
                                    {
                                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                                        Properties.Settings.Default.______odds_iswaiting_02 = true;
                                        Properties.Settings.Default.Save();

                                        if (!Properties.Settings.Default.______odds_issend_01 && !Properties.Settings.Default.______odds_issend_02)
                                        {
                                            Properties.Settings.Default.______odds_issend_01 = true;
                                            Properties.Settings.Default.______odds_issend_02 = true;
                                            Properties.Settings.Default.Save();
                                            SendABCTeam("Under Maintenance.");
                                        }
                                    }

                                    __is_login = true;
                                    panel_cefsharp.Visible = false;
                                    pictureBox_loader.Visible = true;
                                    
                                    SendABCTeam("Firing up!");

                                    Task task_01 = new Task(delegate { ___FIRST_RUNNINGAsync(); });
                                    task_01.Start();
                                }
                            }));
                        }
                    };
                }));
            }
        }

        // ----- Functions
        // S-SPORTS -----
        private async void ___FIRST_RUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel4.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                string source_name = __website_name + __running_01;
                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies(__url, true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                byte[] result = wc.DownloadData(__DAFA_running + _epoch + "&uid=m0101nasrii042318&sportID=1&sortbyTime=false&leagues=&matches=&dateAdd=&oddsGroup=A&marketID=1&fixDate=true&lang=en");
                string responsebody = Encoding.UTF8.GetString(result);
                var deserialize_object = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserialize_object.ToString());
                JToken _count = _jo.SelectToken("$.RunningMatches");

                string password = __website_name + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                string _last_ref_id = "";
                int _row_no = 1;

                if (_count.Count() > 0)
                {
                    for (int i = 0; i < _count.Count(); i++)
                    {
                        JToken LeagueName = _jo.SelectToken("$.RunningMatches[" + i + "].LeagueName").ToString();
                        JToken AwayScore = _jo.SelectToken("$.RunningMatches[" + i + "].AwayScore").ToString();
                        JToken AwayTeamName = _jo.SelectToken("$.RunningMatches[" + i + "].AwayTeamName").ToString();
                        JToken HomeScore = _jo.SelectToken("$.RunningMatches[" + i + "].HomeScore").ToString();
                        JToken HomeTeamName = _jo.SelectToken("$.RunningMatches[" + i + "].HomeTeamName").ToString();
                        JToken MatchID = _jo.SelectToken("$.RunningMatches[" + i + "].MatchID").ToString();
                        JToken MatchTimeHalf = _jo.SelectToken("$.RunningMatches[" + i + "].MatchTimeHalf").ToString() + "H";
                        JToken MatchTimeMinute = _jo.SelectToken("$.RunningMatches[" + i + "].MatchTimeMinute").ToString();
                        String MatchStatus = "";
                        if (MatchTimeHalf.ToString() == "5H")
                        {
                            MatchTimeHalf = "HT";
                        }
                        if (MatchTimeHalf.ToString() == "0H")
                        {
                            MatchTimeHalf = "1H";
                        }
                        if (__is_numeric(MatchTimeMinute.ToString()))
                        {
                            if (MatchTimeHalf.ToString() == "2H" && Convert.ToInt32(MatchTimeMinute.ToString()) > 30)
                            {
                                MatchTimeHalf = "FT";
                                MatchStatus = "C";
                            }
                            else
                            {
                                MatchStatus = "R";
                            }
                        }
                        else
                        {
                            MatchStatus = "R";
                        }
                        JToken StatementDate = _jo.SelectToken("$.RunningMatches[" + i + "].StatementDate").ToString();
                        DateTime StatementDate_Replace = DateTime.ParseExact(StatementDate.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        StatementDate = StatementDate_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken KickOffDateTime = _jo.SelectToken("$.RunningMatches[" + i + "].KickOffDateTime").ToString();
                        DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken FTFav = _jo.SelectToken("$.RunningMatches[" + i + "].FTFav").ToString();
                        JToken FTHDP = _jo.SelectToken("$.RunningMatches[" + i + "].FTHDP").ToString();
                        FTHDP = ___Odds(FTHDP.ToString());
                        JToken FTH = _jo.SelectToken("$.RunningMatches[" + i + "].FTH").ToString();
                        FTH = ___ConvertToEU(FTH.ToString());
                        JToken FTA = _jo.SelectToken("$.RunningMatches[" + i + "].FTA").ToString();
                        FTA = ___ConvertToEU(FTA.ToString());
                        String FTHDPH = "";
                        String FTHDPA = "";
                        if (FTFav.ToString() == "1")
                        {
                            FTHDPH = "-" + FTHDP;
                            FTHDPA = "+" + FTHDP;
                        }
                        else if (FTFav.ToString() == "2")
                        {
                            FTHDPA = "-" + FTHDP;
                            FTHDPH = "+" + FTHDP;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(FTFav.ToString()))
                            {
                                SendMyBot(FTFav.ToString());
                            }
                        }
                        JToken BetIDFTOU = _jo.SelectToken("$.RunningMatches[" + i + "].BetIDFTOU").ToString();
                        JToken FTOU = _jo.SelectToken("$.RunningMatches[" + i + "].FTOU").ToString();
                        FTOU = ___Odds(FTOU.ToString());
                        JToken FTO = _jo.SelectToken("$.RunningMatches[" + i + "].FTO").ToString();
                        FTO = ___ConvertToEU(FTO.ToString());
                        JToken FTU = _jo.SelectToken("$.RunningMatches[" + i + "].FTU").ToString();
                        FTU = ___ConvertToEU(FTU.ToString());
                        JToken BetIDFTOE = _jo.SelectToken("$.RunningMatches[" + i + "].BetIDFTOE").ToString();
                        JToken FTOdd = _jo.SelectToken("$.RunningMatches[" + i + "].FTOdd").ToString();
                        JToken FTEven = _jo.SelectToken("$.RunningMatches[" + i + "].FTEven").ToString();
                        JToken BetIDFT1X2 = _jo.SelectToken("$.RunningMatches[" + i + "].BetIDFT1X2").ToString();
                        JToken FT1 = _jo.SelectToken("$.RunningMatches[" + i + "].FT1").ToString();
                        JToken FTX = _jo.SelectToken("$.RunningMatches[" + i + "].FTX").ToString();
                        JToken FT2 = _jo.SelectToken("$.RunningMatches[" + i + "].FT2").ToString();
                        JToken SpecialGame = _jo.SelectToken("$.RunningMatches[" + i + "].SpecialGame").ToString();

                        JToken FHFav = _jo.SelectToken("$.RunningMatches[" + i + "].FHFav").ToString();
                        JToken FHHDP = _jo.SelectToken("$.RunningMatches[" + i + "].FHHDP").ToString();
                        JToken FHH = _jo.SelectToken("$.RunningMatches[" + i + "].FHH").ToString();
                        FHH = ___ConvertToEU(FHH.ToString());
                        JToken FHA = _jo.SelectToken("$.RunningMatches[" + i + "].FHA").ToString();
                        FHA = ___ConvertToEU(FHA.ToString());
                        String FHHDPH = "";
                        String FHHDPA = "";
                        FHHDP = ___Odds(FHHDP.ToString());
                        if (FHFav.ToString() == "1")
                        {
                            FHHDPH = "-" + FHHDP;
                            FHHDPA = "+" + FHHDP;
                        }
                        else if (FHFav.ToString() == "2")
                        {
                            FHHDPA = "-" + FHHDP;
                            FHHDPH = "+" + FHHDP;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(FHFav.ToString()))
                            {
                                SendMyBot(FHFav.ToString());
                            }
                        }
                        JToken FHOU = _jo.SelectToken("$.RunningMatches[" + i + "].FHOU").ToString();
                        FHOU = ___Odds(FHOU.ToString());
                        JToken FHO = _jo.SelectToken("$.RunningMatches[" + i + "].FHO").ToString();
                        FHO = ___ConvertToEU(FHO.ToString());
                        JToken FHU = _jo.SelectToken("$.RunningMatches[" + i + "].FHU").ToString();
                        FHU = ___ConvertToEU(FHU.ToString());
                        JToken FHOdd = _jo.SelectToken("$.RunningMatches[" + i + "].FHOdd").ToString();
                        JToken FHEven = _jo.SelectToken("$.RunningMatches[" + i + "].FHEven").ToString();
                        JToken FH1 = _jo.SelectToken("$.RunningMatches[" + i + "].FH1").ToString();
                        JToken FHX = _jo.SelectToken("$.RunningMatches[" + i + "].FHX").ToString();
                        JToken FH2 = _jo.SelectToken("$.RunningMatches[" + i + "].FH2").ToString();

                        if (MatchID.ToString() == _last_ref_id)
                        {
                            _row_no++;
                        }
                        else
                        {
                            _row_no = 1;
                        }

                        _last_ref_id = MatchID.ToString();

                        if (HomeTeamName.ToString().ToLower().Contains(" vs ") || AwayTeamName.ToString().ToLower().Contains(" vs "))
                        {
                            try
                            {
                                string[] replace = HomeTeamName.ToString().Split(new string[] { "Vs" }, StringSplitOptions.None);
                                HomeTeamName = replace[0].Trim();
                                AwayTeamName = replace[1].Trim();
                            }
                            catch (Exception err)
                            {

                            }
                        }

                        //MessageBox.Show(
                        //                "LeagueName: " + LeagueName + "\n" +
                        //                "MatchID: " + MatchID + "\n" +
                        //                "_row_no: " + _row_no + "\n" +
                        //                "HomeTeamName: " + HomeTeamName + "\n" +
                        //                "HomeTeamName: " + AwayTeamName + "\n" +
                        //                "HomeScore: " + HomeScore + "\n" +
                        //                "AwayScore: " + AwayScore + "\n" +
                        //                "MatchTimeHalf: " + MatchTimeHalf + "\n" +
                        //                "MatchTimeMinute: " + MatchTimeMinute + "\n" +
                        //                "KickOffDateTime: " + KickOffDateTime + "\n" +
                        //                "StatementDate: " + StatementDate + "\n" +
                        //                "\n-FTHDP-\n" +
                        //                "FTHDPH: " + FTHDPH + "\n" +
                        //                "FTHDPA: " + FTHDPA + "\n" +
                        //                "FTH: " + FTH + "\n" +
                        //                "FTA: " + FTA + "\n" +
                        //                "\nFTOU\n" +
                        //                "FTOU: " + FTOU + "\n" +
                        //                "FTO: " + FTO + "\n" +
                        //                "FTU: " + FTU + "\n" +
                        //                "\n1x2\n" +
                        //                "FT1: " + FT1 + "\n" +
                        //                "FT2: " + FT2 + "\n" +
                        //                "FTX: " + FTX + "\n" +
                        //                "\nOdd\n" +
                        //                "FTOdd: " + FTOdd + "\n" +
                        //                "FTEven: " + FTEven + "\n" +
                        //                "\n-FHHDP-\n" +
                        //                "FHHDPH: " + FHHDPH + "\n" +
                        //                "FHHDPA: " + FHHDPA + "\n" +
                        //                "FHH: " + FHH + "\n" +
                        //                "FHA: " + FHA + "\n" +
                        //                "\nFHOU\n" +
                        //                "FHOU: " + FHOU + "\n" +
                        //                "FHO: " + FHO + "\n" +
                        //                "FHU: " + FHU + "\n" +
                        //                "\n1x2\n" +
                        //                "FH1: " + FH1 + "\n" +
                        //                "FH2: " + FH2 + "\n" +
                        //                "FHX: " + FHX + "\n"
                        //                );

                        var reqparm_ = new NameValueCollection
                        {
                            {"source_id", "8"},
                            {"sport_name", ""},
                            {"league_name", LeagueName.ToString().Trim()},
                            {"home_team", HomeTeamName.ToString().Trim()},
                            {"away_team", AwayTeamName.ToString().Trim()},
                            {"home_team_score", HomeScore.ToString()},
                            {"away_team_score", AwayScore.ToString()},
                            {"ref_match_id", MatchID.ToString()},
                            {"odds_row_no", _row_no.ToString()},
                            {"fthdph", (FTHDPH.ToString() != "") ? FTHDPH.ToString() : "0"},
                            {"fthdpa", (FTHDPA.ToString() != "") ? FTHDPA.ToString() : "0"},
                            {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                            {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                            {"betidftou", (BetIDFTOU.ToString() != "") ? BetIDFTOU.ToString() : "0"},
                            {"ftou", (FTOU.ToString() != "") ? FTOU.ToString() : "0"},
                            {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                            {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                            {"betidftoe", (BetIDFTOE.ToString() != "") ? BetIDFTOE.ToString() : "0"},
                            {"ftodd", (FTOdd.ToString() != "") ? FTOdd.ToString() : "0"},
                            {"fteven", (FTEven.ToString() != "") ? FTEven.ToString() : "0"},
                            {"betidft1x2", (BetIDFT1X2.ToString() != "") ? BetIDFT1X2.ToString() : "0"},
                            {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                            {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                            {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                            {"specialgame", (SpecialGame.ToString() != "") ? SpecialGame.ToString() : "0"},
                            {"fhhdph", (FHHDPH.ToString() != "") ? FHHDPH.ToString() : "0"},
                            {"fhhdpa", (FHHDPA.ToString() != "") ? FHHDPA.ToString() : "0"},
                            {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                            {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                            {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                            {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                            {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                            {"fhodd", (FHOdd.ToString() != "") ? FHOdd.ToString() : "0"},
                            {"fheven", (FHEven.ToString() != "") ? FHEven.ToString() : "0"},
                            {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                            {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                            {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                            {"statement_date", StatementDate.ToString()},
                            {"kickoff_date", KickOffDateTime.ToString()},
                            {"match_time", MatchTimeHalf.ToString()},
                            {"match_status", MatchStatus},
                            {"match_minute", MatchTimeMinute.ToString()},
                            {"api_status", "R"},
                            {"token_api", token},
                        };

                        try
                        {
                            WebClient wc_ = new WebClient();
                            byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                            string responsebody_ = Encoding.UTF8.GetString(result_);
                            __send = 0;
                        }
                        catch (Exception err)
                        {
                            __send++;

                            if (___CheckForInternetConnection())
                            {
                                if (__send == 5)
                                {
                                    SendMyBot(err.ToString());
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    WebClient wc_ = new WebClient();
                                    byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                    string responsebody_ = Encoding.UTF8.GetString(result_);
                                }
                            }
                            else
                            {
                                SendMyBot(err.ToString());
                                __is_close = false;
                                Environment.Exit(0);
                            }
                        }
                    }
                }

                __send = 0;
                ___FIRST_NOTRUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_01)
                        {
                            Properties.Settings.Default.______odds_issend_01 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_11 + " Under Maintenance.");
                        }

                        ___FIRST_NOTRUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___FIRST_RUNNINGAsync();
                    }
                }
                else
                {
                    SendMyBot(err.ToString());
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___FIRST_NOTRUNNINGAsync()
        {
            try
            {
                string source_name = __website_name + __running_01;
                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies(__url, true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                byte[] result = wc.DownloadData(__DAFA_not_running + _epoch + "&uid=m0101nasrii042318&sportID=1&sortbyTime=false&leagues=&matches=&runningLeagues=&oddsGroup=A&lang=en");
                string responsebody = Encoding.UTF8.GetString(result);
                var deserialize_object = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserialize_object.ToString());
                JToken _count = _jo.SelectToken("$.Matches");

                string password = __website_name + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                if (_count.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;
                    for (int i = 0; i < _count.Count(); i++)
                    {
                        JToken LeagueName = _jo.SelectToken("$.Matches[" + i + "].LeagueName").ToString();
                        JToken AwayScore = _jo.SelectToken("$.Matches[" + i + "].AwayScore").ToString();
                        JToken AwayTeamName = _jo.SelectToken("$.Matches[" + i + "].AwayTeamName").ToString();
                        JToken HomeScore = _jo.SelectToken("$.Matches[" + i + "].HomeScore").ToString();
                        JToken HomeTeamName = _jo.SelectToken("$.Matches[" + i + "].HomeTeamName").ToString();
                        JToken MatchID = _jo.SelectToken("$.Matches[" + i + "].MatchID").ToString();
                        JToken StatementDate = _jo.SelectToken("$.Matches[" + i + "].StatementDate").ToString();
                        DateTime StatementDate_Replace = DateTime.ParseExact(StatementDate.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        StatementDate = StatementDate_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken KickOffDateTime = _jo.SelectToken("$.Matches[" + i + "].KickOffDateTime").ToString();
                        DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");

                        JToken FTFav = _jo.SelectToken("$.Matches[" + i + "].FTFav").ToString();
                        JToken FTHDP = _jo.SelectToken("$.Matches[" + i + "].FTHDP").ToString();
                        FTHDP = ___Odds(FTHDP.ToString());
                        JToken FTH = _jo.SelectToken("$.Matches[" + i + "].FTH").ToString();
                        FTH = ___ConvertToEU(FTH.ToString());
                        JToken FTA = _jo.SelectToken("$.Matches[" + i + "].FTA").ToString();
                        FTA = ___ConvertToEU(FTA.ToString());
                        String FTHDPH = "";
                        String FTHDPA = "";
                        if (FTFav.ToString() == "1")
                        {
                            FTHDPH = "-" + FTHDP;
                            FTHDPA = "+" + FTHDP;
                        }
                        else if (FTFav.ToString() == "2")
                        {
                            FTHDPA = "-" + FTHDP;
                            FTHDPH = "+" + FTHDP;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(FTFav.ToString()))
                            {
                                SendMyBot(FTFav.ToString());
                            }
                        }
                        JToken BetIDFTOU = _jo.SelectToken("$.Matches[" + i + "].BetIDFTOU").ToString();
                        JToken FTOU = _jo.SelectToken("$.Matches[" + i + "].FTOU").ToString();
                        FTOU = ___Odds(FTOU.ToString());
                        JToken FTO = _jo.SelectToken("$.Matches[" + i + "].FTO").ToString();
                        FTO = ___ConvertToEU(FTO.ToString());
                        JToken FTU = _jo.SelectToken("$.Matches[" + i + "].FTU").ToString();
                        FTU = ___ConvertToEU(FTU.ToString());
                        JToken BetIDFTOE = _jo.SelectToken("$.Matches[" + i + "].BetIDFTOE").ToString();
                        JToken FTOdd = _jo.SelectToken("$.Matches[" + i + "].FTOdd").ToString();
                        JToken FTEven = _jo.SelectToken("$.Matches[" + i + "].FTEven").ToString();
                        JToken BetIDFT1X2 = _jo.SelectToken("$.Matches[" + i + "].BetIDFT1X2").ToString();
                        JToken FT1 = _jo.SelectToken("$.Matches[" + i + "].FT1").ToString();
                        JToken FTX = _jo.SelectToken("$.Matches[" + i + "].FTX").ToString();
                        JToken FT2 = _jo.SelectToken("$.Matches[" + i + "].FT2").ToString();
                        JToken SpecialGame = _jo.SelectToken("$.Matches[" + i + "].SpecialGame").ToString();

                        JToken FHFav = _jo.SelectToken("$.Matches[" + i + "].FHFav").ToString();
                        JToken FHHDP = _jo.SelectToken("$.Matches[" + i + "].FHHDP").ToString();
                        JToken FHH = _jo.SelectToken("$.Matches[" + i + "].FHH").ToString();
                        FHH = ___ConvertToEU(FHH.ToString());
                        JToken FHA = _jo.SelectToken("$.Matches[" + i + "].FHA").ToString();
                        FHA = ___ConvertToEU(FHA.ToString());
                        String FHHDPH = "";
                        String FHHDPA = "";
                        FHHDP = ___Odds(FHHDP.ToString());
                        if (FHFav.ToString() == "1")
                        {
                            FHHDPH = "-" + FHHDP;
                            FHHDPA = "+" + FHHDP;
                        }
                        else if (FHFav.ToString() == "2")
                        {
                            FHHDPA = "-" + FHHDP;
                            FHHDPH = "+" + FHHDP;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(FHFav.ToString()))
                            {
                                SendMyBot(FHFav.ToString());
                            }
                        }
                        JToken FHOU = _jo.SelectToken("$.Matches[" + i + "].FHOU").ToString();
                        FHOU = ___Odds(FHOU.ToString());
                        JToken FHO = _jo.SelectToken("$.Matches[" + i + "].FHO").ToString();
                        FHO = ___ConvertToEU(FHO.ToString());
                        JToken FHU = _jo.SelectToken("$.Matches[" + i + "].FHU").ToString();
                        FHU = ___ConvertToEU(FHU.ToString());
                        JToken FHOdd = _jo.SelectToken("$.Matches[" + i + "].FHOdd").ToString();
                        JToken FHEven = _jo.SelectToken("$.Matches[" + i + "].FHEven").ToString();
                        JToken FH1 = _jo.SelectToken("$.Matches[" + i + "].FH1").ToString();
                        JToken FHX = _jo.SelectToken("$.Matches[" + i + "].FHX").ToString();
                        JToken FH2 = _jo.SelectToken("$.Matches[" + i + "].FH2").ToString();

                        if (MatchID.ToString() == _last_ref_id)
                        {
                            _row_no++;
                        }
                        else
                        {
                            _row_no = 1;
                        }

                        _last_ref_id = MatchID.ToString();

                        if (HomeTeamName.ToString().ToLower().Contains(" vs ") || AwayTeamName.ToString().ToLower().Contains(" vs "))
                        {
                            try
                            {
                                string[] replace = HomeTeamName.ToString().Split(new string[] { "Vs" }, StringSplitOptions.None);
                                HomeTeamName = replace[0].Trim();
                                AwayTeamName = replace[1].Trim();
                            }
                            catch (Exception err)
                            {

                            }
                        }

                        //MessageBox.Show(
                        //                "LeagueName: " + LeagueName + "\n" +
                        //                "MatchID: " + MatchID + "\n" +
                        //                "_row_no: " + _row_no + "\n" +
                        //                "HomeTeamName: " + HomeTeamName + "\n" +
                        //                "HomeTeamName: " + AwayTeamName + "\n" +
                        //                "HomeScore: " + HomeScore + "\n" +
                        //                "AwayScore: " + AwayScore + "\n" +
                        //                "KickOffDateTime: " + KickOffDateTime + "\n" +
                        //                "StatementDate: " + StatementDate + "\n" +
                        //                "\n-FTHDP-\n" +
                        //                "FTHDPH: " + FTHDPH + "\n" +
                        //                "FTHDPA: " + FTHDPA + "\n" +
                        //                "FTH: " + FTH + "\n" +
                        //                "FTA: " + FTA + "\n" +
                        //                "\nFTOU\n" +
                        //                "FTOU: " + FTOU + "\n" +
                        //                "FTO: " + FTO + "\n" +
                        //                "FTU: " + FTU + "\n" +
                        //                "\n1x2\n" +
                        //                "FT1: " + FT1 + "\n" +
                        //                "FT2: " + FT2 + "\n" +
                        //                "FTX: " + FTX + "\n" +
                        //                "\nOdd\n" +
                        //                "FTOdd: " + FTOdd + "\n" +
                        //                "FTEven: " + FTEven + "\n" +
                        //                "\n-FHHDP-\n" +
                        //                "FHHDPH: " + FHHDPH + "\n" +
                        //                "FHHDPA: " + FHHDPA + "\n" +
                        //                "FHH: " + FHH + "\n" +
                        //                "FHA: " + FHA + "\n" +
                        //                "\nFHOU\n" +
                        //                "FHOU: " + FHOU + "\n" +
                        //                "FHO: " + FHO + "\n" +
                        //                "FHU: " + FHU + "\n" +
                        //                "\n1x2\n" +
                        //                "FH1: " + FH1 + "\n" +
                        //                "FH2: " + FH2 + "\n" +
                        //                "FHX: " + FHX + "\n"
                        //                );

                        var reqparm_ = new NameValueCollection
                        {
                            {"source_id", "8"},
                            {"sport_name", ""},
                            {"league_name", LeagueName.ToString().Trim()},
                            {"home_team", HomeTeamName.ToString().Trim()},
                            {"away_team", AwayTeamName.ToString().Trim()},
                            {"home_team_score", HomeScore.ToString()},
                            {"away_team_score", AwayScore.ToString()},
                            {"ref_match_id", MatchID.ToString()},
                            {"odds_row_no", _row_no.ToString()},
                            {"fthdph", (FTHDPH.ToString() != "") ? FTHDPH.ToString() : "0"},
                            {"fthdpa", (FTHDPA.ToString() != "") ? FTHDPA.ToString() : "0"},
                            {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                            {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                            {"betidftou", (BetIDFTOU.ToString() != "") ? BetIDFTOU.ToString() : "0"},
                            {"ftou", (FTOU.ToString() != "") ? FTOU.ToString() : "0"},
                            {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                            {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                            {"betidftoe", (BetIDFTOE.ToString() != "") ? BetIDFTOE.ToString() : "0"},
                            {"ftodd", (FTOdd.ToString() != "") ? FTOdd.ToString() : "0"},
                            {"fteven", (FTEven.ToString() != "") ? FTEven.ToString() : "0"},
                            {"betidft1x2", (BetIDFT1X2.ToString() != "") ? BetIDFT1X2.ToString() : "0"},
                            {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                            {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                            {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                            {"specialgame", (SpecialGame.ToString() != "") ? SpecialGame.ToString() : "0"},
                            {"fhhdph", (FHHDPH.ToString() != "") ? FHHDPH.ToString() : "0"},
                            {"fhhdpa", (FHHDPA.ToString() != "") ? FHHDPA.ToString() : "0"},
                            {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                            {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                            {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                            {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                            {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                            {"fhodd", (FHOdd.ToString() != "") ? FHOdd.ToString() : "0"},
                            {"fheven", (FHEven.ToString() != "") ? FHEven.ToString() : "0"},
                            {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                            {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                            {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                            {"statement_date", StatementDate.ToString()},
                            {"kickoff_date", KickOffDateTime.ToString()},
                            {"match_time", "Upcoming"},
                            {"match_status", "N"},
                            {"api_status", "R"},
                            {"token_api", token},
                        };

                        try
                        {
                            WebClient wc_ = new WebClient();
                            byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                            string responsebody_ = Encoding.UTF8.GetString(result_);
                            __send = 0;
                        }
                        catch (Exception err)
                        {
                            __send++;

                            if (___CheckForInternetConnection())
                            {
                                if (__send == 5)
                                {
                                    SendMyBot(err.ToString());
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    WebClient wc_ = new WebClient();
                                    byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                    string responsebody_ = Encoding.UTF8.GetString(result_);
                                }
                            }
                            else
                            {
                                SendMyBot(err.ToString());
                                __is_close = false;
                                Environment.Exit(0);
                            }
                        }
                    }
                }

                // send dafa 
                if (Properties.Settings.Default.______odds_issend_01)
                {
                    Properties.Settings.Default.______odds_issend_01 = false;
                    Properties.Settings.Default.Save();

                    SendABCTeam(__running_11 + " Back to Normal.");
                }

                // comment detect
                //Properties.Settings.Default.______odds_iswaiting_01 = false;
                //Properties.Settings.Default.Save();

                Invoke(new Action(() =>
                {
                    panel4.BackColor = Color.FromArgb(16, 90, 101);
                }));

                __send = 0;
                await ___TaskWait();
                ___FIRST_RUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_01)
                        {
                            Properties.Settings.Default.______odds_issend_01 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_11 + " Under Maintenance.");
                        }

                        ___FIRST_RUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___FIRST_NOTRUNNINGAsync();
                    }
                }
                else
                {
                    SendMyBot(err.ToString());
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        public static bool ___CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task ___TaskWait()
        {
            Random _random = new Random();
            int _random_number = _random.Next(25, 30);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        private async Task ___TaskWait_Handler(int sec)
        {
            sec++;
            Random _random = new Random();
            int _random_number = _random.Next(sec, sec);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        public bool __is_numeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private void __Flag()
        {
            string _flag = Path.Combine(Path.GetTempPath(), __app + " - " + __website_name + ".txt");
            using (StreamWriter sw = new StreamWriter(_flag, true))
            {
                sw.WriteLine("<<>>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<<>>");
            }
        }

        private string ___ConvertToEU(string num)
        {
            if (num != "0" && !String.IsNullOrEmpty(num))
            {
                if (num.Contains("-"))
                {

                    return Convert.ToDecimal(Math.Round((-1 / Convert.ToDecimal(num.Trim())) + 1, 2).ToString().Trim()).ToString("N2");
                }
                else
                {
                    return Convert.ToDecimal(Math.Round(Convert.ToDecimal(num.Trim()) + 1, 2).ToString().Trim()).ToString("N2");
                }
            }
            else
            {
                return "0";
            }
        }

        private string ___Odds(string odds)
        {
            if (odds.ToString().Trim().Contains("-"))
            {
                string ______odds = "0-0.5|0.25\r\n0.5-1|0.75\r\n1-1.5|1.25\r\n1.5-2|1.75\r\n2-2.5|2.25\r\n2.5-3|2.75\r\n3-3.5|3.2" +
                                    "5\r\n3.5-4|3.75\r\n4-4.5|4.25\r\n4.5-5|4.75\r\n5-5.5|5.25\r\n5.5-6|5.75\r\n6-6.5|6.25\r\n6.5-7" +
                                    "|6.75\r\n7-7.5|7.25\r\n7.5-8|7.75\r\n8-8.5|8.25\r\n8.5-9|8.75\r\n9-9.5|9.25\r\n9.5-10|9.75" +
                                    "\r\n10-10.5|10.25\r\n10.5-11|10.75\r\n11-11.5|11.25\r\n11.5-12|11.75";
                bool _detect = false;
                string[] _odds = ______odds.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var _odd in _odds)
                {
                    String[] _odds_replace = _odd.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                    if (odds.ToString() == _odds_replace[0].Trim())
                    {
                        _detect = true;
                        odds = _odds_replace[1].Trim();
                        break;
                    }
                }

                if (!_detect)
                {
                    SendMyBot(odds.ToString());
                }

                return odds;
            }
            else
            {
                if (!String.IsNullOrEmpty(odds))
                {
                    return odds;
                }
                else
                {
                    return "0";
                }
            }
        }

        // added settings
        private void panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form_Settings form_settings = new Form_Settings();
            form_settings.ShowDialog();
        }
    }
}