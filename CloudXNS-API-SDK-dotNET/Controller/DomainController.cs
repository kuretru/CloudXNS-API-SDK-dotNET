using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI.Model;
using Kuretru.CloudXNSAPI.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kuretru.CloudXNSAPI.Controller
{
    /// <summary>
    /// 执行域名相关操作
    /// </summary>
    public class DomainController
    {
        private HttpUtility _httpUtility;

        public DomainController(HttpUtility httpUtility)
        {
            _httpUtility = httpUtility;
        }

        /// <summary>
        /// 获取CloudXNS账号下存在的域名列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <returns>域名列表</returns>
        public List<CloudXNSDomain> GetList()
        {
            string result = _httpUtility.PostAPIRequest("domain");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                int count = Convert.ToInt32(jobject["total"]);
                string data = jobject["data"].ToString();
                List<CloudXNSDomain> list = JsonConvert.DeserializeObject<List<CloudXNSDomain>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 在CloudXNS账号下新建一个域名。
        /// </summary>
        /// <param name="domainName">域名名称(如cloudxns.net)</param>
        /// <returns>API响应状态</returns>
        public APIResponse Create(string domainName)
        {
            JObject jobject = new JObject(new JProperty("domain", domainName));
            string result = _httpUtility.PostAPIRequest("POST", "domain", jobject.ToString());
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 0)
            {
                response.ChineseMessage = "域名已存在";
            }
            return response;
        }

        /// <summary>
        /// 在CloudXNS账号下移除某个域名。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <returns>API响应状态</returns>
        public APIResponse Remove(int domainID)
        {
            string result = _httpUtility.PostAPIRequest("DELETE", string.Format("domain/{0}", domainID), null);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (string.IsNullOrEmpty(result))
            {
                response = new APIResponse("域名被用户锁定");
            }
            else if (response.Code == 300)
            {
                response.ChineseMessage = "域名ID不正确";
            }
            return response;
        }

        /// <summary>
        /// 获取某域名解析量统计数据
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="host">主机名，查询全部传all</param>
        /// <param name="code">统计某区域ID或某ISP ID解析量，查询全部传all</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>解析量统计数据</returns>
        public List<CloudXNSDomainStat> Stat(int domainID, string host, string code, DateTime startDate, DateTime endDate)
        {
            string result = _httpUtility.PostAPIRequest(string.Format("domain_stat/{0}?host={1}&code={2}&start_date={3}&end_date={4}",
                domainID, host, code, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<CloudXNSDomainStat> list = JsonConvert.DeserializeObject<List<CloudXNSDomainStat>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取某域名解析量统计数据
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>解析量统计数据</returns>
        public List<CloudXNSDomainStat> Stat(int domainID, DateTime startDate, DateTime endDate)
        {
            return Stat(domainID, "all", "all", startDate, endDate);
        }
    }
}
