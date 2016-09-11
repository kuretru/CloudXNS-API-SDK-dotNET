using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI.Model;
using Kuretru.CloudXNSAPI.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kuretru.CloudXNSAPI.Controller
{
    /// <summary>
    /// 执行解析记录相关操作
    /// </summary>
    public class RecordController
    {
        private HttpUtility _httpUtility;

        public RecordController(HttpUtility httpUtility)
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
            string url = string.Format("record/{0}?host_id=0&offset=0&row_num=1", domainID);
            string result = _httpUtility.PostAPIRequest(url);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                return Convert.ToInt32(jobject["total"]);
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取指定域名下某个主机的解析记录列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="index">分页查询，页码(第1页为1)</param>
        /// <param name="count">分页查询，每页的记录数，最大每页为2000条</param>
        /// <param name="hostID">主机记录ID(0为查全部)</param>
        /// <returns>解析记录列表</returns>
        public List<CloudXNSRecord> GetList(int domainID, int index, int count, int hostID)
        {
            string url = string.Format("record/{0}?host_id={3}&offset={1}&row_num={2}", domainID, (index - 1) * count, count, hostID);
            string result = _httpUtility.PostAPIRequest(url);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string data = jobject["data"].ToString();
                List<CloudXNSRecord> list = JsonConvert.DeserializeObject<List<CloudXNSRecord>>(data);
                return list;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 获取指定域名下存在的所有解析记录列表，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="index">分页查询，页码(第1页为1)</param>
        /// <param name="count">分页查询，每页的记录数，最大每页为2000条</param>
        /// <returns>解析记录列表</returns>
        public List<CloudXNSRecord> GetList(int domainID, int index, int count)
        {
            return GetList(domainID, index, count, 0);
        }

        /// <summary>
        /// 获取指定域名下前2000条解析记录，若响应状态码不等于1，则抛出APIResponseException异常。
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <returns>解析记录列表</returns>
        public List<CloudXNSRecord> GetList(int domainID)
        {
            return GetList(domainID, 1, GetCount(domainID));
        }

        /// <summary>
        /// 在指定域名下新建一条解析记录
        /// </summary>
        /// <param name="record">解析记录实体，可填参数为DomainID(必)、Host、Value(必)、Type(必)、MX、LineID(必)、TTL</param>
        /// <returns>API响应状态</returns>
        public APIResponse Create(CloudXNSRecord record)
        {
            JsonSerializerSettings jsonSS = new JsonSerializerSettings();
            jsonSS.ContractResolver = new DynamicContractResolver(new string[] { "domain_id", "host",
                "value", "type", "mx", "line_id", "ttl" });
            string data = JsonConvert.SerializeObject(record, jsonSS);
            string result = _httpUtility.PostAPIRequest("POST", "record", data);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }

        /// <summary>
        /// 在指定域名下新建一条备用解析记录
        /// </summary>
        /// <param name="record">解析记录实体，须填参数为DomainID、HostID、RecordID、Value</param>
        /// <returns>API响应状态</returns>
        public APIResponse CreateSpare(CloudXNSRecord record)
        {
            JsonSerializerSettings jsonSS = new JsonSerializerSettings();
            jsonSS.ContractResolver = new DynamicContractResolver(new string[] { "domain_id", "host_id", "record_id", "value" });
            string data = JsonConvert.SerializeObject(record, jsonSS);
            string result = _httpUtility.PostAPIRequest("POST", "record/spare", data);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }

        /// <summary>
        /// 更新指定解析记录
        /// </summary>
        /// <param name="record">解析记录实体，必填参数为DomainID(必)、Host、Value(必)、MX、TTL、Type、LineID、SpareValue</param>
        /// <returns>返回解析实体有效值有记录ID、完整域名、值</returns>
        public CloudXNSRecord Update(CloudXNSRecord record)
        {
            JsonSerializerSettings jsonSS = new JsonSerializerSettings();
            jsonSS.ContractResolver = new DynamicContractResolver(new string[] { "domain_id", "host", "value",
                "type", "mx", "ttl", "line_id" });
            string data = JsonConvert.SerializeObject(record, jsonSS);
            string result = _httpUtility.PostAPIRequest("PUT", string.Format("record/{0}", record.RecordID), data);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            if (response.Code == 1)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string responseData = jobject["data"].ToString();
                CloudXNSRecord responseRecord = JsonConvert.DeserializeObject<CloudXNSRecord>(data);
                return responseRecord;
            }
            else
            {
                throw new APIResponseException(response);
            }
        }

        /// <summary>
        /// 删除指定解析记录
        /// </summary>
        /// <param name="domainID">域名iD</param>
        /// <param name="recordID">解析记录ID</param>
        /// <returns>API响应状态</returns>
        public APIResponse Remove(int domainID, int recordID)
        {
            string result = _httpUtility.PostAPIRequest("DELETE", string.Format("record/{0}/{1}", recordID, domainID), null);
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }

        /// <summary>
        /// 暂停解析记录
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="recordID">记录ID</param>
        /// <param name="status">解析记录状态，0暂停，1启用</param>
        /// <returns>API响应状态</returns>
        public APIResponse Pause(int domainID, int recordID, int status)
        {
            JObject jobject = new JObject(new JProperty("id", recordID));
            jobject.Add(new JProperty("domain_id", domainID));
            jobject.Add(new JProperty("status", status));
            string result = _httpUtility.PostAPIRequest("POST", "record/pause", jobject.ToString());
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }

        /// <summary>
        /// 启用X优化
        /// </summary>
        /// <param name="domainID">域名ID</param>
        /// <param name="recordID">记录ID</param>
        /// <param name="status">X优化状态，0暂停，1启用</param>
        /// <returns>API响应状态</returns>
        public APIResponse AI(int domainID, int recordID, int status)
        {
            JObject jobject = new JObject(new JProperty("id", recordID));
            jobject.Add(new JProperty("domain_id", domainID));
            jobject.Add(new JProperty("status", status));
            string result = _httpUtility.PostAPIRequest("POST", "record/ai", jobject.ToString());
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }

        /// <summary>
        /// DDNS快速修改解析记录值
        /// </summary>
        /// <param name="domain">含主机记录的域名，如www.cloudxns.net</param>
        /// <param name="ip">记录IP值，多个IP值中间用|分割，为空时由API自动获取</param>
        /// <param name="lineID">线路ID，1为全网默认</param>
        /// <returns>API响应状态</returns>
        public APIResponse DDNS(string domain, string ip, int lineID)
        {
            JObject jobject = new JObject(new JProperty("domain", domain));
            if (!string.IsNullOrEmpty(ip))
            {
                jobject.Add(new JProperty("ip", ip));
            }
            jobject.Add(new JProperty("line_id", lineID));
            string result = _httpUtility.PostAPIRequest("POST", "ddns", jobject.ToString());
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }

        /// <summary>
        /// DDNS快速修改解析记录值
        /// </summary>
        /// <param name="domain">含主机记录的域名，如www.cloudxns.net</param>
        /// <param name="ip">记录IP值，多个IP值中间用|分割，为空时由API自动获取</param>
        /// <returns>API响应状态</returns>
        public APIResponse DDNS(string domain, string ip)
        {
            return DDNS(domain, ip, 1);
        }

        /// <summary>
        /// DDNS快速修改解析记录值
        /// </summary>
        /// <param name="domain">含主机记录的域名，如www.cloudxns.net</param>
        /// <returns>API响应状态</returns>
        public APIResponse DDNS(string domain)
        {
            return DDNS(domain, null, 1);
        }
    }
}
