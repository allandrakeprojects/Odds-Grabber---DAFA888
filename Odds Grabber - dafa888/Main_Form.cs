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
        private string __brand_color = "#FFE000";
        private string __url = "www.dafa888.com";
        private string __website_name = "dafa888";
        private string __app__website_name = "";
        private string __api_key = "youdieidie";
        private string __running_01 = "dafa888";
        private string __running_02 = "";
        private string __running_11 = "";
        private string __running_22 = "";
        private int __send = 0;
        private int __r = 255;
        private int __g = 224;
        private int __b = 0;
        private bool __is_close;
        private bool __is_login = false;
        private bool __is_send = false;
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
            __app__website_name = __app + " - " + __website_name;
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
            if (__is_send)
            {
                __is_send = false;
                MessageBox.Show("Telegram Notification is Disabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                __is_send = true;
                MessageBox.Show("Telegram Notification is Enabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void SendABCTeam(string message)
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
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void timer_detect_running_Tick(object sender, EventArgs e)
        {
            //___DetectRunningAsync();
        }

        private async void ___DetectRunningAsync()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://192.168.10.252:8080/API/updateAppStatus", "POST", data);
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
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssitex.com:8080/API/updateAppStatus", "POST", data);
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
            chromeBrowser = new ChromiumWebBrowser("https://www.sportdafa.net/en/sports-df/sports");
            panel_cefsharp.Controls.Add(chromeBrowser);
            chromeBrowser.AddressChanged += ChromiumBrowserAddressChanged;
        }

        int first = 0;

        // CefSharp Address Changed
        private void ChromiumBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            __url = e.Address.ToString();
            Invoke(new Action(() =>
            {
                //panel3.Visible = true;
                panel4.Visible = true;
            }));

            if (e.Address.ToString().Contains("https://www.sportdafa.net/en/sports-df/sports"))
            {
                Invoke(new Action(() =>
                {
                    chromeBrowser.FrameLoadEnd += (sender_, args) =>
                    {
                        if (args.Frame.IsMain)
                        {

                            chromeBrowser.GetSourceAsync().ContinueWith(taskHtml =>
                            {
                                string html = taskHtml.Result.ToLower();

                                if (html.Contains("restricted page"))
                                {
                                    SendABCTeam("Please setup first VPN to the installed PC.");
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Invoke(new Action(async () =>
                                    {
                                        if (first == 0)
                                        {
                                            first++;
                                            __is_login = true;
                                            panel_cefsharp.Visible = false;
                                            pictureBox_loader.Visible = true;

                                            SendABCTeam("Firing up!");
                                            await ___TaskWait_Handler(10);
                                            Task task_01 = new Task(delegate { ___FIRST_RUNNINGAsync(); });
                                            task_01.Start();
                                        }
                                    }));
                                }
                            });
                        }
                    };
                }));
            }
        }

        // ----- Functions
        // DAFA888 -----
        private async void ___FIRST_RUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel4.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies(__url, true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers["X-Requested-With"] = "XMLHttpRequest";
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                byte[] result = await wc.DownloadDataTaskAsync("https://als.sportdafa.net/xapi/rest/events?hash=57767535ba5573e31334f13ab937f942&l=en");
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                
                JArray _jo = JArray.Parse(deserializeObject.ToString());
                JToken _count = _jo.SelectToken("");

                string password = __running_01 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";
                string _last_ref_id = "";
                int _row_no = 1;
                
                //JToken MatchID = "";
                //JToken HomeTeamName = "";
                //JToken AwayTeamName = "";
                //JToken HomeScore = "";
                //JToken AwayScore = "";
                //JToken MatchTimeHalf = "";
                //JToken KickOffDateTime = "";
                //String StatementDate = "";
                //JToken MatchTimeMinute = "";
                //String MatchStatus = "";

                if (_count.Count() > 0)
                {
                    for (int i = 0; i < _count.Count(); i++)
                    {
                        String HomeTeamName__AwayTeamName = _jo.SelectToken("[" + i + "].description").ToString();
                        JToken Country__LeagueName_Detect = _jo.SelectToken("[" + i + "].eventPaths");
                        String Country = _jo.SelectToken("[" + i + "].eventPaths.[1].description").ToString();
                        String LeagueName = "";
                        if (Country__LeagueName_Detect.Count() == 4)
                        {
                            LeagueName = _jo.SelectToken("[" + i + "].eventPaths.[3].description").ToString();
                        }
                        else
                        {
                            LeagueName = _jo.SelectToken("[" + i + "].eventPaths.[2].description").ToString();
                        }
                        LeagueName = Country + " - " + LeagueName;
                        String[] HomeTeamName__AwayTeamName_Replace = HomeTeamName__AwayTeamName.ToString().Split(new string[] { "vs" }, StringSplitOptions.None);
                        String HomeTeamName = HomeTeamName__AwayTeamName_Replace[0].Trim();
                        String AwayTeamName = HomeTeamName__AwayTeamName_Replace[1].Trim();

                        String Clock_Detect = _jo.SelectToken("[" + i + "].clock").ToString();
                        String MatchTimeMinute = "";
                        String Started = "";
                        String MatchTimeHalf = "";
                        String MatchStatus = "";
                        if (Clock_Detect != "")
                        {
                            MatchTimeMinute = _jo.SelectToken("[" + i + "].clock.minutes").ToString();
                            Started = _jo.SelectToken("[" + i + "].clock.status").ToString().ToLower().Trim();
                            MatchTimeHalf = _jo.SelectToken("[" + i + "].currentPeriod").ToString().ToLower();
                            MatchStatus = "";
                            if (MatchTimeHalf == "first half")
                            {
                                MatchTimeHalf = "1H";
                            }
                            else if (MatchTimeHalf == "second half")
                            {

                                if (Started == "intermission")
                                {
                                    MatchTimeHalf = "HT";
                                    MatchTimeMinute = "0";
                                }
                                else
                                {
                                    MatchTimeHalf = "2H";
                                }
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
                        }
                        String Scores = _jo.SelectToken("[" + i + "].scores").ToString();
                        String HomeScore = "";
                        String AwayScore = "";
                        if (Scores != "")
                        {
                            HomeScore = _jo.SelectToken("[" + i + "].scores.score.[0].points").ToString();
                            AwayScore = _jo.SelectToken("[" + i + "].scores.score.[1].points").ToString();
                        }

                        // Odds
                        JToken Markets = _jo.SelectToken("[" + i + "].markets");
                        String FTHDP = "";
                        String FTHDPH = "";
                        String FTHDPA = "";
                        String FTH = "";
                        String FTA = "";
                        String FTOU = "";
                        String FTO = "";
                        String FTU = "";
                        String FT1 = "";
                        String FT2 = "";
                        String FTX = "";
                        String FHHDP = "";
                        String FHHDPH = "";
                        String FHHDPA = "";
                        String FHH = "";
                        String FHA = "";
                        String FHOU = "";
                        String FHO = "";
                        String FHU = "";
                        String FH1 = "";
                        String FH2 = "";
                        String FHX = "";
                                                
                        for (int ii = 0; ii < Markets.Count(); ii++)
                        {
                            String Description = _jo.SelectToken("[" + i + "].markets.[" + ii + "].description").ToString();
                            if (Description == "Asian Handicap")
                            {
                                String _Description = _jo.SelectToken("[" + i + "].markets.[" + ii + "].period.fullAbbreviation").ToString();
                                if (_Description == "FT")
                                {
                                    FTHDPH = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString();
                                    if (FTHDPH.Contains("-"))
                                    {
                                        FTHDPH = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString().ToLower().Replace(HomeTeamName.ToLower(), "").Replace("-", "").Replace("+", "").Replace(",", "-").Trim();
                                        FTHDPH = "-" + ___Odds(FTHDPH);
                                        FTHDPA = "+" + FTHDPH.Replace("-", "");
                                    }
                                    else
                                    {
                                        FTHDPA = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString().ToLower().Replace(HomeTeamName.ToLower(), "").Replace("-", "").Replace("+", "").Replace(",", "-").Trim();
                                        FTHDPA = "-" + ___Odds(FTHDPA);
                                        FTHDPH = "+" + FTHDPA.Replace("-", "");
                                    }
                                    FTH = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].consolidatedPrice.currentPrice.format").ToString();
                                    FTA = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].consolidatedPrice.currentPrice.format").ToString();
                                }
                                else
                                {
                                    FHHDPH = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString();
                                    FHHDPA = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].description").ToString();
                                    if (FHHDPH.Contains("-"))
                                    {
                                        FHHDPH = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString().ToLower().Replace(HomeTeamName.ToLower(), "").Replace("-", "").Replace("+", "").Replace(",", "-").Trim();
                                        FHHDPH = "-" + ___Odds(FHHDPH);
                                        FHHDPA = "+" + FHHDPH.Replace("-", "");
                                    }
                                    else
                                    {
                                        FHHDPA = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString().ToLower().Replace(HomeTeamName.ToLower(), "").Replace("-", "").Replace("+", "").Replace(",", "-").Trim();
                                        if (FHHDPA != "0")
                                        {
                                            FHHDPA = "-" + ___Odds(FHHDPA);
                                            FHHDPH = "+" + FHHDPA.Replace("-", "");
                                        }
                                        else
                                        {
                                            FHHDPH = "-0";
                                            FHHDPA = "+0";
                                        }
                                    }
                                    FHH = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].consolidatedPrice.currentPrice.format").ToString();
                                    FHA = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].consolidatedPrice.currentPrice.format").ToString();
                                }
                            }
                            if (Description == "Over / Under")
                            {
                                String _Description = _jo.SelectToken("[" + i + "].markets.[" + ii + "].period.fullAbbreviation").ToString();
                                if (_Description == "FT")
                                {
                                    FTOU = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString().ToLower().Replace("over ", "").Replace("-", "").Replace("+", "").Replace(",", "-").Trim();
                                    FTOU = ___Odds(FTOU);
                                    FTO = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].consolidatedPrice.currentPrice.format").ToString();
                                    FTU = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].consolidatedPrice.currentPrice.format").ToString();
                                }
                                else
                                {
                                    FHOU = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].description").ToString().ToLower().Replace("over ", "").Replace("-", "").Replace("+", "").Replace(",", "-").Trim();
                                    FHOU = ___Odds(FHOU);
                                    FHO = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].consolidatedPrice.currentPrice.format").ToString();
                                    FHU = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].consolidatedPrice.currentPrice.format").ToString();
                                }
                            }
                            if (Description == "Win/Draw/Win")
                            {
                                String _Description = _jo.SelectToken("[" + i + "].markets.[" + ii + "].period.fullAbbreviation").ToString();
                                if (_Description == "FT")
                                {
                                    FT1 = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].consolidatedPrice.currentPrice.format").ToString();
                                    FTX = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].consolidatedPrice.currentPrice.format").ToString();
                                    FT2 = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[2].consolidatedPrice.currentPrice.format").ToString();
                                }
                                else
                                {
                                    FH1 = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[0].consolidatedPrice.currentPrice.format").ToString();
                                    FHX = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[1].consolidatedPrice.currentPrice.format").ToString();
                                    FH2 = _jo.SelectToken("[" + i + "].markets.[" + ii + "].outcomes.[2].consolidatedPrice.currentPrice.format").ToString();
                                }
                            }
                        }
                        
                        string ref_id_password = DateTime.Now.ToString("yyyy-MM-dd") + "8" + "Soccer" + LeagueName + HomeTeamName + AwayTeamName;
                        byte[] ref_id_encodedpassword = new UTF8Encoding().GetBytes(ref_id_password.Trim());
                        byte[] ref_hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(ref_id_encodedpassword);
                        string ref_token = BitConverter.ToString(ref_hash).Replace("-", string.Empty).ToLower().Substring(0, 8);
                        ref_match_id = ref_token;

                        if (ref_match_id == _last_ref_id)
                        {
                            _row_no++;
                        }
                        else
                        {
                            _row_no = 1;
                        }

                        _last_ref_id = ref_match_id;
                        
                        String KickOffDateTime = _jo.SelectToken("[" + i + "].eventDate").ToString();
                        String StatementDate = "";
                        if (KickOffDateTime != "")
                        {
                            try
                            {
                                DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                                StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");
                            }
                            catch (Exception err)
                            {
                                DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "M/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                                StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");
                            }
                        }
                        else
                        {
                            KickOffDateTime = "";
                        }
                        
                        //MessageBox.Show(
                        //                "LeagueName: " + LeagueName + "\n" +
                        //                "MatchID: " + ref_match_id + "\n" +
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
                        //                //"FTOdd: " + FTOdd + "\n" +
                        //                //"FTEven: " + FTEven + "\n" +
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
                            {"home_team_score", (HomeScore.ToString() != "") ? HomeScore.ToString() : "0"},
                            {"away_team_score", (AwayScore.ToString() != "") ? AwayScore.ToString() : "0"},
                            {"ref_match_id", ref_match_id},
                            {"odds_row_no", _row_no.ToString()},
                            {"fthdph", (FTHDPH.ToString() != "") ? FTHDPH.ToString() : "0"},
                            {"fthdpa", (FTHDPA.ToString() != "") ? FTHDPA.ToString() : "0"},
                            {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                            {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                            {"betidftou", "0"},
                            {"ftou", "0"},
                            {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                            {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                            {"betidftoe", "0"},
                            {"ftodd", "0"},
                            {"fteven", "0"},
                            {"betidft1x2", "0"},
                            {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                            {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                            {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                            {"specialgame", "0"},
                            {"fhhdph", (FHHDPH.ToString() != "") ? FHHDPH.ToString() : "0"},
                            {"fhhdpa", (FHHDPA.ToString() != "") ? FHHDPA.ToString() : "0"},
                            {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                            {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                            {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                            {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                            {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                            {"fhodd", "0"},
                            {"fheven", "0"},
                            {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                            {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                            {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                            {"statement_date", StatementDate.ToString()},
                            {"kickoff_date", KickOffDateTime.ToString()},
                            {"match_time", (MatchTimeHalf.ToString() != "") ? FH2.ToString() : "Upcoming"},
                            {"match_status", (MatchStatus.ToString() != "") ? MatchStatus.ToString() : "N"},
                            {"match_minute", (MatchTimeMinute.ToString() != "") ? MatchTimeMinute.ToString() : "0"},
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
                                __is_close = false;
                                Environment.Exit(0);
                            }
                        }
                    }
                }

                // send dafa888 
                //if (!Properties.Settings.Default.______odds_iswaiting_01 && Properties.Settings.Default.______odds_issend_01)
                //{
                //    Properties.Settings.Default.______odds_issend_01 = false;
                //    Properties.Settings.Default.Save();

                //    SendABCTeam(__running_11 + " Back to Normal.");
                //}

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
                    if (err.ToString().ToLower().Contains("401"))
                    {
                        chromeBrowser.Load("https://www.sportdafa.net/en/sports-df/sports");
                    }
                    else
                    {
                        await chromeBrowser.GetSourceAsync().ContinueWith(async taskHtml =>
                        {
                            var html = taskHtml.Result.ToString().ToLower();
                            if (html.Contains("dear user"))
                            {
                                SendABCTeam("Please setup first VPN to the installed PC.");
                                __is_close = false;
                                Environment.Exit(0);
                            }
                            else
                            {
                                __send++;
                                if (__send == 5)
                                {
                                    //Properties.Settings.Default.______odds_iswaiting_01 = true;
                                    //Properties.Settings.Default.Save();

                                    //if (!Properties.Settings.Default.______odds_issend_01)
                                    //{
                                    //    Properties.Settings.Default.______odds_issend_01 = true;
                                    //    Properties.Settings.Default.Save();
                                    //    SendABCTeam(__running_11 + " Under Maintenance.");
                                    //}

                                    ___FIRST_RUNNINGAsync();
                                    SendMyBot(err.ToString());
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    ___FIRST_RUNNINGAsync();
                                }
                            }
                        });
                    }
                }
                else
                {
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
            int _random_number = _random.Next(10, 16);
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

        private string ___Odds(string odds)
        {
            if (odds.ToString().Trim().Contains("-"))
            {
                bool _detect = false;
                string[] _odds = Properties.Settings.Default.______odds.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
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
    }
}