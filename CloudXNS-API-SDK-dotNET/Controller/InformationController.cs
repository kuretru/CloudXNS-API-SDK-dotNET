using System.Collections.Generic;
using Kuretru.CloudXNSAPI.Model;
using Kuretru.CloudXNSAPI.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kuretru.CloudXNSAPI.Controller
{
    /// <summary>
    /// 执行记录类型，线路、区域、ISP等相关操作
    /// </summary>
    public class InformationController
    {
        private HttpUtility _httpUtility;

        public InformationController(HttpUtility httpUtility)
        {
            _httpUtility = httpUtility;
        }

        /// <summary>
        /// 获取记录类型列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <returns>记录类型列表</returns>
        public List<string> GetRecordTypes()
        {
            string result = _httpUtility.PostAPIRequest("type");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<string> list = JsonConvert.DeserializeObject<List<string>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取线路列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <returns>线路列表</returns>
        public List<CloudXNSLine> GetLineList()
        {
            string result = _httpUtility.PostAPIRequest("line");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<CloudXNSLine> list = JsonConvert.DeserializeObject<List<CloudXNSLine>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取区域列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <returns>区域列表</returns>
        public List<CloudXNSRegion> GetRegionList()
        {
            string result = _httpUtility.PostAPIRequest("line/region");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<CloudXNSRegion> list = JsonConvert.DeserializeObject<List<CloudXNSRegion>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取ISP列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <returns>ISP列表</returns>
        public List<CloudXNSISP> GetISPList()
        {
            string result = _httpUtility.PostAPIRequest("line/isp");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<CloudXNSISP> list = JsonConvert.DeserializeObject<List<CloudXNSISP>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取NS服务器列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <returns>NS服务器列表</returns>
        public List<CloudXNSNameServer> GetNSList()
        {
            string result = _httpUtility.PostAPIRequest("ns_server");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<CloudXNSNameServer> list = JsonConvert.DeserializeObject<List<CloudXNSNameServer>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }
    }
}
