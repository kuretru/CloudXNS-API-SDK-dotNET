using Kuretru.CloudXNSAPI.Controller;
using Kuretru.CloudXNSAPI.Model;
using Kuretru.CloudXNSAPI.Util;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI
{
    /// <summary>
    /// CloudXNS API SDK
    /// </summary>
    public class CloudXNSAPI
    {
        private HttpUtility _httpUtility;
        private DomainController _domainController;
        private InformationController _informationController;
        private HostController _hostController;
        private RecordController _recordController;

        /// <summary>
        /// 执行域名相关操作
        /// </summary>
        public DomainController DomainController
        {
            get
            {
                return _domainController;
            }
        }

        /// <summary>
        /// 获取记录类型、线路、区域、ISP等列表
        /// </summary>
        public InformationController InformationController
        {
            get
            {
                return _informationController;
            }
        }

        /// <summary>
        /// 执行主机记录相关操作
        /// </summary>
        public HostController HostController
        {
            get
            {
                return _hostController;
            }
        }

        /// <summary>
        /// 执行解析记录相关操作
        /// </summary>
        public RecordController RecordController
        {
            get
            {
                return _recordController;
            }
        }

        /// <summary>
        /// 使用API Key，实例化APIController
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="secretKey">Secret Key</param>
        public CloudXNSAPI(string apiKey, string secretKey)
        {
            APIConfiguration configutaion = ConfigurationController.LoadConfigutaion();
            configutaion.APIKey = apiKey;
            configutaion.SecretKey = secretKey;
            _httpUtility = new HttpUtility(configutaion);

            _domainController = new DomainController(_httpUtility);
            _informationController = new InformationController(_httpUtility);
            _hostController = new HostController(_httpUtility);
            _recordController = new RecordController(_httpUtility);
        }

        /// <summary>
        /// 判断API Key是否有效
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="secretKey">Secret Key</param>
        /// <returns>API响应状态，响应状态码为1时有效</returns>
        public static APIResponse Login(string apiKey, string secretKey)
        {
            APIConfiguration configuration = new APIConfiguration(apiKey, secretKey);
            HttpUtility httpUtility = new HttpUtility(configuration);
            string result = httpUtility.PostAPIRequest("domain");
            APIResponse response = JsonConvert.DeserializeObject<APIResponse>(result);
            return response;
        }
    }
}
