using System;

namespace Kuretru.CloudXNSAPI.Model
{
    /// <summary>
    /// API响应异常
    /// </summary>
    public class APIResponseException : ApplicationException
    {
        private APIResponse _apiResponse;

        /// <summary>
        /// 发生异常的响应
        /// </summary>
        public APIResponse ApiResponse
        {
            get
            {
                return _apiResponse;
            }
        }

        public APIResponseException(APIResponse response)
        {
            _apiResponse = response;
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public override string Message
        {
            get
            {
                return _apiResponse.ToString();
            }
        }
    }
}
