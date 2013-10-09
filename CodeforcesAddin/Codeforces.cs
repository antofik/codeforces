using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using CodeforcesAddin.Annotations;

namespace CodeforcesAddin
{
    class Codeforces : INotifyPropertyChanged
    {
        #region Singleton

        public static readonly Codeforces Instance = new Codeforces();

        protected Codeforces(){}

        #endregion

        private string _login = "helper";
        [PasswordPropertyText(true)]
        private string _password = "helper";

        private const string BaseUrl = "http://codeforces.ru";

        /// <summary>
        /// Returns CRLF token for given page
        /// </summary>
        public string GetCrlf(string url)
        {
            var html = GetHtml(url);
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

        public string SubmitProgram(int contest, char problem, int language, string code)
        {
            var csrf = GetCrlf(string.Format("/contest/{0}/submit", contest));

            var boundary = Guid.NewGuid().ToString();
            var header = string.Format("--{0}", boundary);
            var footer = header + "-";
            var mask = header + @"Content-Disposition: form-data; name=""{0}""\r\n";

            var contents = new StringBuilder();
            contents.AppendLine(string.Format(mask, "csrf_token"));
            contents.AppendLine(csrf);

            contents.AppendLine(string.Format(mask, "action"));
            contents.AppendLine("submitSolutionFormSubmitted");

            contents.AppendLine(string.Format(mask, "programTypeId"));
            contents.AppendLine(language.ToString(CultureInfo.InvariantCulture));

            contents.AppendLine(string.Format(mask, "source"));
            contents.AppendLine(code);

            contents.AppendLine(string.Format(mask, "_tta"));
            contents.AppendLine("740");

            contents.AppendLine(footer);

            var data = contents.ToString();

            var request = (HttpWebRequest)WebRequest.Create(BaseUrl + string.Format("/contest/{0}/submit", contest));
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "multipart/form-data; boundary=" + boundary; ;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(_cookies);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36 OPR/16.0.1196.80";
            request.AllowWriteStreamBuffering = true;
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = true;
            request.Referer = "http://codeforces.ru/enter";
            request.Headers.Add("origin", "http://codeforces.ru");
            request.ContentLength = data.Length;
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
        /// Performs POST request
        /// </summary>
        public string Post(string url, string data, string contentType = "application/x-www-form-urlencoded")
        {
            var csrf = GetCrlf(url);
            data = string.Format("csrf_token={0}&{1}", csrf, data);
            var request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = contentType;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(_cookies);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36 OPR/16.0.1196.80";
            request.AllowWriteStreamBuffering = true;
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = true;
            request.Referer = "http://codeforces.ru/enter";
            request.Headers.Add("origin", "http://codeforces.ru");
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
            var html = Post("/enter", string.Format("action=enter&handle={0}&password={1}&_tta=740&remember=on", _login, _password));
            return (IsLogged = html.Contains("/data/update-online"));
        }

        #region Public properties

        #region IsLogged

        private bool _isLogged;
        private CookieCollection _cookies = new CookieCollection();

        public bool IsLogged
        {
            get { return _isLogged; }
            set
            {
                if (value == _isLogged) return;
                _isLogged = value;
                OnPropertyChanged("IsLogged");
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
    }
}
