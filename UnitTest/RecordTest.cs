using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI;
using Kuretru.CloudXNSAPI.Model;

namespace UnitTest
{
    public class RecordTest
    {
        private APIManager _api;
        private bool _continue = true;
        private int _domainID = -1;
        private string _domainName = string.Empty;

        public RecordTest(APIManager api)
        {
            _api = api;
            while (_continue)
            {
                try
                {
                    PrintMainMenu();
                }
                catch (APIResponseException e)
                {
                    Console.WriteLine(e.ApiResponse);
                    Console.ReadLine();
                }
            }
        }

        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("CloudXNS API SDK on dotNET unit test");
            Console.WriteLine("测试项目：");
            Console.WriteLine("0.返回");
            Console.WriteLine("1.设置操作的域名");
            if (_domainID != -1)
            {
                Console.WriteLine("2.统计域名下解析记录的数量");
                Console.WriteLine("3.获取域名下解析记录列表");
                Console.WriteLine("4.新建解析记录");
                Console.WriteLine("5.新建备用记录");
                Console.WriteLine("6.更新某记录");
                Console.WriteLine("7.删除某记录");
                Console.WriteLine("8.暂停某记录解析");
                Console.WriteLine("9.为某记录启用X优化");
                Console.WriteLine("10.DDNS");
            }
            int result = -1;
            result = GetIntInput("请选择测试项目：");
            Console.Clear();
            if (result == 0)
            {
                _continue = false;
            }
            else if (result == 1)
            {
                _domainID = -1;
                SetOperatingDomain();
            }
            else if(_domainID != -1)
            {
                switch (result)
                {
                    case 2:
                        Count();
                        break;
                    case 3:
                        GetList();
                        break;
                    case 4:
                        Create();
                        break;
                    case 5:
                        CreateSpare();
                        break;
                    case 6:
                        Update();
                        break;
                    case 7:
                        Remove();
                        break;
                    case 8:
                        Pause();
                        break;
                    case 9:
                        AI();
                        break;
                    case 10:
                        DDNS();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 获取int类型的用户输入
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private int GetIntInput(string title)
        {
            int result = -1;
            bool work = true;
            while (work)
            {
                try
                {
                    Console.WriteLine(title);
                    result = Convert.ToInt32(Console.ReadLine());
                    work = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }                
            }
            return result;
        }

        /// <summary>
        /// 获取string类型的用户输入
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private string GetStringInput(string title)
        {
            Console.WriteLine(title);
            return Console.ReadLine();
        }

        /// <summary>
        /// 设置要操作的域名
        /// </summary>
        private void SetOperatingDomain()
        {
            List<CloudXNSDomain> list = _api.DomainController.GetList();
            Dictionary<int, string> _idList = new Dictionary<int, string>();
            foreach (CloudXNSDomain domain in list)
            {
                _idList.Add(domain.ID, domain.DomainName);
                Console.WriteLine(domain);
                Console.WriteLine(" ID：{0}\t状态：{1}\t接管状态：{2}", domain.ID, domain.Status, domain.TakeOverStatus);
                Console.WriteLine();
            }
            while (_domainID == -1)
            {
                int domainID = GetIntInput("请输入进行后续操作的域名ID：");
                if (_idList.ContainsKey(domainID))
                {
                    _domainID = domainID;
                    _domainName = _idList[domainID];
                }
                else
                {
                    Console.WriteLine("你的账号中不存在该域名！");
                }
            }
        }

        public void Count()
        {
            Console.WriteLine(_api.RecordController.GetCount(_domainID));
            Console.ReadLine();
        }

        public void GetList()
        {
            List<CloudXNSRecord> list = _api.RecordController.GetList(_domainID);
            foreach (CloudXNSRecord record in list)
            {
                Console.WriteLine("{0}.{1}", record, _domainName);
                Console.WriteLine(" ID:{0}\tType:{1}  \tValue:{2}", record.RecordID, record.RecordType, record.Value);
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 处理创建、更新解析记录时参数的输入
        /// </summary>
        /// <returns></returns>
        private CloudXNSRecord InputRecordInfor()
        {
            CloudXNSRecord record = new CloudXNSRecord();
            record.DomainID = _domainID;
            #region Host
            string host = GetStringInput("请输入主机记录名称(如www,@)：");
            if (string.IsNullOrEmpty(host))
            {
                host = "@";
            }
            record.Host = host;
            #endregion
            #region Type
            Console.WriteLine("请输入记录类型：");
            List<string> types = _api.InformationController.GetRecordTypes();
            Console.WriteLine(string.Join(",", types));
            string type = Console.ReadLine();
            if (string.IsNullOrEmpty(type))
            {
                type = "A";
            }
            record.RecordType = type;
            #endregion
            #region Value
            record.Value = GetStringInput("请输入记录值：");
            #endregion
            #region MX
            type = type.ToLower();
            if (type == "mx" || type == "ax" || type == "cnamex")
            {
                int mx = GetIntInput("请输入MX优先级：");
                record.MX = mx.ToString();
            }
            #endregion
            #region LineID
            Console.WriteLine("输入y查询线路ID(全网默认为1)，直接输入请按任意键：");
            if (Console.ReadKey().KeyChar == 'y')
            {
                List<CloudXNSLine> list = _api.InformationController.GetLineList();
                if (list != null)
                {
                    int level = 0;
                    InformationTest.CloudXNSLineProcessor(list, ref level);
                }
                Console.ReadLine();
            }
            int lineID = GetIntInput("请输入线路ID：");
            record.LineID = lineID;
            #endregion
            #region TTL
            int ttl = GetIntInput("请输入TTL(60-3600)：");
            if (ttl < 60 || ttl > 3600)
            {
                ttl = 600;
            }
            record.TTL = ttl;
            #endregion
            return record;
        }
        
        public void Create()
        {
            Console.WriteLine(_api.RecordController.Create(InputRecordInfor()));
            Console.ReadLine();
        }

        public void CreateSpare()
        {
            CloudXNSRecord record = new CloudXNSRecord();
            record.DomainID = _domainID;

            List<CloudXNSRecord> list = _api.RecordController.GetList(_domainID);
            Dictionary<int, CloudXNSRecord> _dic = new Dictionary<int, CloudXNSRecord>();
            foreach (CloudXNSRecord r in list)
            {
                _dic.Add(r.RecordID,r);
                Console.WriteLine("{0}.{1}", r, _domainName);
                Console.WriteLine(" ID:{0}\tType:{1}  \tValue:{2}", r.RecordID, r.RecordType, r.Value);
                Console.WriteLine();
            }

            int recordID = -1;
            while (!_dic.ContainsKey(recordID))
            {
                recordID = GetIntInput("请输入要新增备用记录的解析记录ID：");
            }
            record.RecordID = recordID;
            record.HostID = _dic[recordID].HostID;
            
            string value = GetStringInput("请输入备记录值：");
            record.Value = value;

            Console.WriteLine(_api.RecordController.CreateSpare(record));
            Console.ReadLine();
        }

        private int InputSelectedRecord(string title)
        {
            List<CloudXNSRecord> list = _api.RecordController.GetList(_domainID);
            List<int> _dic = new List<int>();
            foreach (CloudXNSRecord r in list)
            {
                _dic.Add(r.RecordID);
                Console.WriteLine("{0}.{1}", r, _domainName);
                Console.WriteLine(" ID:{0}\tType:{1}  \tValue:{2}\t解析状态:{3}", r.RecordID, r.RecordType, r.Value, r.Status);
                Console.WriteLine();
            }

            int recordID = -1;
            while (!_dic.Contains(recordID))
            {
                recordID = GetIntInput(title);
            }
            return recordID;
        }

        public void Update()
        {
            int recordID = InputSelectedRecord("请输入要更新的解析记录ID：");
            CloudXNSRecord record = InputRecordInfor();
            record.RecordID = recordID;
            Console.WriteLine(_api.RecordController.Update(record));
            Console.ReadLine();
        }

        public void Remove()
        {
            int recordID = InputSelectedRecord("请输入要删除的解析记录ID：");
            Console.WriteLine(_api.RecordController.Remove(_domainID, recordID));
            Console.ReadLine();
        }

        public void Pause()
        {
            int recordID = InputSelectedRecord("请输入要暂停的解析记录ID：");
            int status = -1;
            while (status == -1)
            {
                Console.WriteLine("输入y启用该解析记录，n停用该记录：");
                string key = Console.ReadLine().ToLower();
                if (key == "y")
                {
                    status = 1;
                }
                else if (key == "n")
                {
                    status = 0;
                }
            }
            Console.WriteLine(_api.RecordController.Pause(_domainID, recordID, status));
            Console.ReadLine();
        }

        public void AI()
        {
            int recordID = InputSelectedRecord("请输入要启用X优化的解析记录ID：");
            int status = -1;
            while (status == -1)
            {
                Console.WriteLine("输入y启用X优化解析记录，n停用X优化该记录：");
                string key = Console.ReadLine().ToLower();
                if (key == "y")
                {
                    status = 1;
                }
                else if (key == "n")
                {
                    status = 0;
                }
            }
            Console.WriteLine(_api.RecordController.AI(_domainID, recordID, status));
            Console.ReadLine();
        }

        public void DDNS()
        {
            string domain = GetStringInput("请输入需要DDNS的域名(如www.cloudxns.net)：");
            string ip = GetStringInput("请输入记录IP值，多个IP值使用|分割(如8.8.8.8|1.1.1.1)，如果不输入将CloudXNS将自动获取本机公网IP：");
            Console.WriteLine(_api.RecordController.DDNS(domain));
            Console.ReadLine();
        }
    }
}
