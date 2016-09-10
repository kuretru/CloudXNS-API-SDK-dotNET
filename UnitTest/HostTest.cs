using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI;
using Kuretru.CloudXNSAPI.Model;

namespace UnitTest
{
    public class HostTest
    {
        private CloudXNSAPI _api;
        private bool _continue = true;

        public HostTest(CloudXNSAPI api)
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
            Console.WriteLine("1.获取主机记录数量");
            Console.WriteLine("2.获取主机记录列表");
            Console.WriteLine("3.删除主机记录");
            int result = -1;
            try
            {
                result = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("按任意键继续...");
                Console.ReadLine();
            }
            Console.Clear();
            switch (result)
            {
                case 0:
                    _continue = false;
                    break;
                case 1:
                    Count();
                    break;
                case 2:
                    GetList();
                    Console.ReadLine();
                    break;
                case 3:
                    Remove();
                    break;
                default:
                    break;
            }
        }

        public void Count()
        {
            List<CloudXNSDomain> list = _api.DomainController.GetList();
            foreach (CloudXNSDomain domain in list)
            {
                Console.WriteLine(domain);
                Console.WriteLine(" ID:{0}", domain.ID);
                Console.WriteLine();
            }
            Console.WriteLine("请输入要查询的域名ID：");
            try
            {
                Console.WriteLine(_api.HostController.GetCount(Convert.ToInt32(Console.ReadLine())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        public void GetList()
        {
            List<CloudXNSDomain> list = _api.DomainController.GetList();
            foreach (CloudXNSDomain domain in list)
            {
                Console.WriteLine(domain);
                Console.WriteLine(" ID:{0}", domain.ID);
                Console.WriteLine();
            }
            Console.WriteLine("请输入要查询的域名ID：");
            try
            {
                int domainID = Convert.ToInt32(Console.ReadLine());
                int count = _api.HostController.GetCount(domainID);
                List<CloudXNSHost> hostList = _api.HostController.GetList(domainID, 1, count);
                Console.Clear();
                foreach (CloudXNSHost host in hostList)
                {
                    Console.WriteLine(host);
                    Console.WriteLine(" {0}", host.ID);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Remove()
        {
            GetList();
            try
            {
                Console.WriteLine(_api.HostController.Remove(Convert.ToInt32(Console.ReadLine())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
