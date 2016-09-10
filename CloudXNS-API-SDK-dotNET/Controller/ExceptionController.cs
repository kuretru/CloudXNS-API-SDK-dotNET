using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kuretru.CloudXNSAPI.Model;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Controller
{
    internal class ExceptionController
    {
        private static Dictionary<int, APIResponse> _dic;

        static ExceptionController()
        {
            _dic = new Dictionary<int, APIResponse>();
            string path = string.Format("{0}\\code.json", Environment.CurrentDirectory);
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                List<APIResponse> list = JsonConvert.DeserializeObject<List<APIResponse>>(json);
                foreach (APIResponse response in list)
                {
                    _dic.Add(response.Code, response);
                }
            }
        }

        
        public static string GetMessageByCode(int code)
        {
            string result = string.Empty;
            if (_dic.ContainsKey(code))
            {
                result = _dic[code].ChineseMessage;
            }
            return result;
        }
    }
}
