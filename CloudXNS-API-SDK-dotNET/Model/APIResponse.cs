using Kuretru.CloudXNSAPI.Controller;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Model
{
    public class APIResponse
    {
        private int _code = -1;
        private string _message = string.Empty;
        private string _chineseMessage = string.Empty;
        private int _httpStatusCode = -1;

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                _chineseMessage = ExceptionController.GetMessageByCode(value);
            }
        }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// 根据CloudXNS异常响应代码，获取的中文异常信息
        /// </summary>
        public string ChineseMessage
        {
            get
            {
                return _chineseMessage;
            }
            set
            {
                _chineseMessage = value;
            }
        }

        /// <summary>
        /// HTTP状态码
        /// </summary>
        public int HTTPStatusCode
        {
            get
            {
                return _httpStatusCode;
            }
            set
            {
                _httpStatusCode = value;
            }
        }

        public APIResponse()
        {

        }

        public APIResponse(string message)
        {
            _message = message;
        }

        public APIResponse(int code, string message)
        {
            _code = code;
            _message = message;
        }

        public APIResponse(int code, string message,int httpStatusCode)
        {
            _code = code;
            _message = message;
            _httpStatusCode = httpStatusCode;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_chineseMessage))
            {
                return string.Format("{0}:{1}", _code, _chineseMessage);
            }
            return string.Format("{0}:{1}", _code, _message);
        }
    }
}
