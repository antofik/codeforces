using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using CodeforcesAddin.Annotations;

namespace CodeforcesAddin
{
    class Codeforces : INotifyPropertyChanged
    {
        #region Singleton

        public static readonly Codeforces Instance = new Codeforces();

        protected Codeforces(){}

        #endregion

        #region Private properties

        private string _login = "helper";

        [PasswordPropertyText(true)]
        private string _password = "helper";

        private readonly CookieCollection _cookies = new CookieCollection();

        private static long _lastSubmission;

        private const string BaseUrl = "http://codeforces.ru";

        #endregion

        #region Methods

        /// <summary>
        /// Assign login and password
        /// </summary>
        public void SetAuthenticationData(string login, string password)
        {
            _login = login;
            _password = password;
        }

        /// <summary>
        /// Returns CRLF token for given page
        /// </summary>
        public string GetCrlf(string url)
        {
            var html = GetHtml(url);
            if (html.Contains("href=\"/enter\""))
                IsLogged = false;
            return Regex.Match(html, @"name='csrf_token' value='(?<csrf>[^']+)?'").Groups["csrf"].Value;
        }

        /// <summary>
        /// Load page by relative url
        /// </summary>
        public string GetHtml(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(BaseUrl + url);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(_cookies);

            var response = (HttpWebResponse)request.GetResponse();
            string html;
            using (var stream = response.GetResponseStream())
            {
                using (var output = new MemoryStream())
                {
                    stream.CopyTo(output);   
                    html = Encoding.UTF8.GetString(output.ToArray());
                }
            }
            _cookies.Add(response.Cookies);
            return html;
        }

        /// <summary>
        /// Submits program
        /// Return submission code if successs
        /// if error:
        ///     -2 - the same program was already sent before
        ///     -1 - submission failed
        ///      0 - submission was successful, but code is unknown
        /// </summary>
        public long SubmitProgram(int contest, char problem, int language, string code)
        {
            Log.Info("\tReceiving csrf");
            var csrf = GetCrlf(string.Format("/contest/{0}/submit", contest));
            Log.Info("\tCSRF={0}", csrf);

            var boundary = "----" + Guid.NewGuid().ToString().Replace("-", "");
            var header = string.Format("--{0}", boundary);
            var footer = header + "--";
            var mask = @"Content-Disposition: form-data; name=""{0}""";

            var contents = new StringBuilder();
            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "csrf_token"));
            contents.AppendLine();
            contents.AppendLine(csrf);

            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "action"));
            contents.AppendLine();
            contents.AppendLine("submitSolutionFormSubmitted");

            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "submittedProblemIndex"));
            contents.AppendLine();
            contents.AppendLine(problem.ToString(CultureInfo.InvariantCulture));

            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "programTypeId"));
            contents.AppendLine();
            contents.AppendLine(language.ToString(CultureInfo.InvariantCulture));

            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "source"));
            contents.AppendLine();
            contents.AppendLine(code);

            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "sourceFile") + ";filename=\"\"");
            contents.AppendLine("Content-Type: application/octet-stream");
            contents.AppendLine();
            contents.AppendLine();

            contents.AppendLine(header);
            contents.AppendLine(string.Format(mask, "_tta"));
            contents.AppendLine();
            contents.AppendLine("740");

            contents.AppendLine(footer);

            var data = contents.ToString();

            Log.Info("\tSending request");

            var request = (HttpWebRequest)WebRequest.Create(BaseUrl + string.Format("/contest/{0}/submit?csrf_token={1}", contest, csrf));
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "multipart/form-data; boundary=" + boundary; ;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(_cookies);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36 OPR/16.0.1196.80";
            request.AllowWriteStreamBuffering = true;
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = false;

            request.Headers.Add("origin", BaseUrl);

            request.Headers.Add("DNT", "1");
            request.Referer = BaseUrl + "/enter";
            var bytes = Encoding.UTF8.GetBytes(data);
            request.ContentLength = bytes.Length;
            {
                var stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            Log.Info("\tResponse received {0} {1}", response.StatusCode, response.StatusDescription);
            string html;
            using (var stream = response.GetResponseStream())
            {
                using (var output = new MemoryStream())
                {
                    stream.CopyTo(output);
                    stream.Flush();
                    html = Encoding.UTF8.GetString(output.ToArray());
                }
            }
            _cookies.Add(response.Cookies);

            if (html.Contains("Ранее вы отсылали абсолютно такой же код") || html.Contains("You have submitted exactly the same code before")) return -2;
            if (html.Contains("class=\"error for")) return -3;

            var contestUrl = string.Format("/contest/{0}/my", contest);
            if (response.StatusCode == HttpStatusCode.Found)
            {
                Log.Info("\tWaiting for submission id...");
                for (var i = 0; i < 5; i++)
                {
                    var match = Regex.Match(GetHtml(contestUrl), @"submissionId=""(?<id>\d+)""");
                    if (match.Success && match.Groups.Count > 1)
                    {
                        var submissionId = long.Parse(match.Groups["id"].Value);
                        if (submissionId > _lastSubmission)
                        {
                            _lastSubmission = submissionId;
                            Log.Info("\tSubmissionId = {0}", submissionId);
                            return submissionId;
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                Log.Info("\tFailed to receive submission id. Please, goto http://codeforces.ru manually and check status");
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// Reads status of the given submission
        /// </summary>
        public SubmissionStatus GetSubmissionStatus(long contest, long submissionId)
        {
            SubmissionStatus status;
            var html = Post("/data/submitSource", "submissionId=" + submissionId, string.Format("/contest/{0}/my", contest), true);
            var serializer = new DataContractJsonSerializer(typeof(SubmissionStatus));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
            {
                status = serializer.ReadObject(stream) as SubmissionStatus;
            }
            if (status != null && !string.IsNullOrEmpty(status.verdict))
                status.verdict = Regex.Replace(status.verdict, "<.*?>", string.Empty);

            const string mask = @"""{0}#{1}"":""(?<value>[^""]+?)""";

            Func<string, string> ClearOfUnicodeValues = value =>
            {
                foreach (Match match in Regex.Matches(value, @"\\u(?<n>\d\d\d\d)"))
                {
                    var n = Convert.ToInt32(match.Groups["n"].Value, 16);
                    value = value.Replace(match.Groups[0].Value, char.ConvertFromUtf32(n));
                }
                return value;
            };
            Func<string, int, string> get = (key, i) =>
            {
                var match = Regex.Match(html, string.Format(mask, key, i));
                return !match.Success ? null : ClearOfUnicodeValues(HttpUtility.HtmlDecode(match.Groups["value"].Value).Replace(@"\r\n", "\r\n").Trim(' ', '\r', '\n'));
            };

            if (status != null)
            {
                status.Tests = new List<SubmissionTestStatus>();
                for (var i = 1; i <= status.testCount; i++)
                {
                    var verdict = get("verdict", i);
                    if (verdict == null) break;
                    int memory;
                    if (int.TryParse(get("memoryConsumed", i), out memory))
                        memory /= 1024;
                    else
                        memory = -1;

                    var item = new SubmissionTestStatus
                    {
                        Number = i,
                        Verdict = verdict,
                        Answer = get("answer", i),
                        Memory = memory != -1 ? memory.ToString(CultureInfo.InvariantCulture) : get("memoryConsumed", i),
                        Time = get("timeConsumed", i),
                        Input = get("input", i),
                        Output = get("output", i),
                        ExitCode = get("exitCode", i),
                        CheckerExitCode = get("checkerExitCode", i),
                        checkerStdoutAndStderr = get("checkerStdoutAndStderr", i),
                    };
                    status.Tests.Insert(0, item);
                }
            }
            return status;
        }

        /// <summary>
        /// Performs POST request
        /// </summary>
        public string Post(string url, string data, string csrfUrl = null, bool loginCheck = false)
        {
            var csrf = GetCrlf(csrfUrl ?? url);
            if (!IsLogged && loginCheck)
            {
                Login();
                csrf = GetCrlf(csrfUrl ?? url);
            }
            data = string.Format("csrf_token={0}&{1}", csrf, data);
            var request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(_cookies);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36 OPR/16.0.1196.80";
            request.AllowWriteStreamBuffering = true;
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = true;
            request.Referer = BaseUrl + "/enter";
            request.Headers.Add("origin", BaseUrl);
            using (var stream = request.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                stream.Write(bytes, 0, bytes.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            string html;
            using (var stream = response.GetResponseStream())
            {
                using (var output = new MemoryStream())
                {
                    stream.CopyTo(output);
                    html = Encoding.UTF8.GetString(output.ToArray());
                }
            }
            _cookies.Add(response.Cookies);
            return html;
        }

        /// <summary>
        /// Performs login
        /// </summary>
        public bool Login()
        {
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password)) return false;
            var html = Post("/enter", string.Format("action=enter&handle={0}&password={1}&_tta=999999&remember=on", _login, _password));
            return (IsLogged = html.Contains("/data/update-online"));
        }

        #endregion

        #region Public properties

        #region IsLogged

        private bool _isLogged;

        public bool IsLogged
        {
            get { return _isLogged; }
            set
            {
                if (value == _isLogged) return;
                _isLogged = value;
                OnPropertyChanged("IsLogged");
                if (OnLogged != null) OnLogged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public event Action OnLogged;
    }
}
