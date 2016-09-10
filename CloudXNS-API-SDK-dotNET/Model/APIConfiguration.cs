using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// 配置文件实体类
    /// </summary>
    public class APIConfiguration
    {
        private int _timeOut = 5000;
        private string _apiKey = string.Empty;
        private string _secretKey = string.Empty;
        private string _apiURL = string.Empty;
        private string _userAgent = string.Empty;
        private bool _useProxy = false;
        private string _proxyAddress = string.Empty;

        /// <summary>
        /// 超时时间
        /// </summary>
        [JsonProperty(PropertyName = "time_out")]
        public int TimeOut
        {
            get
            {
                return _timeOut;
            }

            set
            {
                _timeOut = value;
            }
        }

        /// <summary>
        /// API Key
        /// </summary>
        [JsonProperty(PropertyName = "api_key")]
        public string APIKey
        {
            get
            {
                return _apiKey;
            }

            set
            {
                _apiKey = value;
            }
        }

        /// <summary>
        /// Secret Key
        /// </summary>
        [JsonProperty(PropertyName = "secret_key")]
        public string SecretKey
        {
            get
            {
                return _secretKey;
            }

            set
            {
                _secretKey = value;
            }
        }

        /// <summary>
        /// API请求地址
        /// </summary>
        [JsonProperty(PropertyName = "api_url")]
        public string APIURL
        {
            get
            {
                if (string.IsNullOrEmpty(_apiURL))
                {
                    _apiURL = "https://www.cloudxns.net/api2";
                }
                return _apiURL;
            }

            set
            {
                _apiURL = value;
            }
        }

        /// <summary>
        /// 请求标识
        /// </summary>
        [JsonProperty(PropertyName = "user_agent")]
        public string UserAgent
        {
            get
            {
                if (string.IsNullOrEmpty(_userAgent))
                {
                    _userAgent = string.Format("CloudXNS API SDK {0}by Kuretru(kuretru@gmail.com) on dotNET", "v1.0 ");
                }
                return _userAgent;
            }

            set
            {
                _userAgent = value;
            }
        }

        /// <summary>
        /// 是否使用代理
        /// </summary>
        [JsonProperty(PropertyName = "user_proxy")]
        public bool UseProxy
        {
            get
            {
                return _useProxy;
            }

            set
            {
                _useProxy = value;
            }
        }

        /// <summary>
        /// 代理地址
        /// </summary>
        [JsonProperty(PropertyName = "proxy_address")]
        public string ProxyAddress
        {
            get
            {
                return _proxyAddress;
            }

            set
            {
                _proxyAddress = value;
            }
        }

        public APIConfiguration()
        {
            
        }

        public APIConfiguration(string apiKey, string secretKey)
        {
            _apiKey = apiKey;
            _secretKey = secretKey;
        }
    }
}
