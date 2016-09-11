using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI.Model;
using Kuretru.CloudXNSAPI.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kuretru.CloudXNSAPI.Controller
{
    /// <summary>
    /// 执行主机记录相关操作
    /// </summary>
    public class HostController
    {
        HttpUtility _httpUtility;

        public HostController(HttpUtility httpUtility)
        {
            _httpUtility = httpUtility;
        }

        /// <summary>
        /// 获取指定域名下存在的主机记录的总数，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <returns>返回记录的总数</returns>
        public int GetCount(int domainID)
        {
            string url = string.Format("host/{0}?offset=0&row_num=0", domainID);
            string result = _httpUtility.PostAPIRequest(url);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                return Convert.ToInt32(jo["total"]);
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取指定域名下存在的主机记录列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="index">分页查询，页码(第1页为1)</param>
        /// <param name="count">分页查询，每页的记录数，最大每页为2000条</param>
        /// <returns>主机记录列表</returns>
        public List<CloudXNSHost> GetList(int domainID, int index, int count)
        {
            string url = string.Format("host/{0}?offset={1}&row_num={2}", domainID, (index - 1) * count, count);
            string result = _httpUtility.PostAPIRequest(url);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["hosts"].ToString();
                List<CloudXNSHost> list = JsonConvert.DeserializeObject<List<CloudXNSHost>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取指定域名下前2000条主机记录列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <returns>主机记录列表</returns>
        public List<CloudXNSHost> GetList(int domainID)
        {
            return GetList(domainID, 1, GetCount(domainID));
        }

        /// <summary>
        /// 在CloudXNS账号下移除某个主机记录。
        /// </summary>
        /// <param name="hostID">主机记录ID</param>
        /// <returns>API响应状态</returns>
        public APIResponse Remove(int hostID)
        {
            string result = _httpUtility.PostAPIRequest("DELETE", string.Format("host/{0}", hostID), null);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }
    }
}
