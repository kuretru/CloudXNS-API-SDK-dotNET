using System;
using System.IO;
using System.Net;
using Kuretru.CloudXNSAPI.Model;

namespace Kuretru.CloudXNSAPI.Util
{
    /// <summary>
    /// 处理HTTP请求工具类
    /// </summary>
    public class HttpUtility
    {
        private APIConfiguration _configuration;
        
        /// <summary>
        /// 使用API配置初始化HttpUtility
        /// </summary>
        /// <param name="configutaion"></param>
        public HttpUtility(APIConfiguration configutaion)
        {
            _configuration = configutaion;
        }

        /// <summary>
        /// 返回API的HTTP请求结果，仅适用于获取各种列表(GET方法)
        /// </summary>
        /// <param name="interfaceURL">接口URL</param>
        /// <returns>API返回的Json格式数据</returns>
        public string PostAPIRequest(string interfaceURL)
        {
            return PostAPIRequest("GET", interfaceURL, null);
        }

        /// <summary>
        /// 返回API的HTTP请求结果
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="interfaceURL">接口URL</param>
        /// <param name="requestParameters">请求参数</param>
        /// <returns>API返回的Json格式数据</returns>
        public string PostAPIRequest(string method, string interfaceURL, string requestParameters)
        {
            //拼接API功能的URL
            string url = string.Format("{0}/{1}", _configuration.APIURL, interfaceURL);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            //设置是否使用代理
            if (_configuration.UseProxy)
            {
                request.Proxy = new WebProxy(_configuration.ProxyAddress);
            }

            //封装HTTP请求参数
            request.ContentType = "text/json";
            request.UserAgent = _configuration.UserAgent;
            request.Timeout = _configuration.TimeOut;
            request.Method = method;

            //写入请求参数
            if (!string.IsNullOrEmpty(requestParameters))
            {
                using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(requestParameters);
                    streamWriter.Flush();
                }
            }

            //将签名验证写入请求头
            string now = DateTime.Now.ToString("R");
            string hmac_raw = string.Format("{0}{1}{2}{3}{4}", _configuration.APIKey, url, requestParameters, now, _configuration.SecretKey);
            string hmac = EncryptionHelper.GetMD5Hash(hmac_raw);
            request.Headers.Add("API-KEY", _configuration.APIKey);
            request.Headers.Add("API-REQUEST-DATE", now);
            request.Headers.Add("API-HMAC", hmac);
            request.Headers.Add("API-FORMAT", "json");

            //发送Web请求
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                //将40X异常按正常流程返回
                response = (HttpWebResponse)e.Response;
            }

            //处理响应数据
            string result = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 通过IP138服务，自动获取本机公网IP
        /// </summary>
        /// <returns>IP地址(如202.101.172.35)</returns>
        public static string GetPublicIP()
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.ip138.com/ips138.asp");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string text = string.Empty;
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    text = sr.ReadToEnd();
                }
                
                int start = text.IndexOf("您的IP地址是：[") + 9;
                int end = text.IndexOf("]", start);
                result = text.Substring(start, end - start);
            }
            catch
            {

            }
            return result;
        }
    }
}
