using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI;
using Kuretru.CloudXNSAPI.Model;

namespace UnitTest
{
    public class DomainTest
    {
        private APIManager _api;
        private bool _continue = true;

        public DomainTest(APIManager api)
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
            Console.WriteLine("1.获取域名列表");
            Console.WriteLine("2.新建域名");
            Console.WriteLine("3.删除域名");
            Console.WriteLine("4.获取解析量统计信息");
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
                    GetList();
                    break;
                case 2:
                    Create();
                    break;
                case 3:
                    Remove();
                    break;
                case 4:
                    Stat();
                    break;
                default:
                    break;
            }
        }

        public void GetList()
        {
            List<CloudXNSDomain> list = _api.DomainController.GetList();
            foreach (CloudXNSDomain domain in list)
            {
                Console.WriteLine(domain);
                Console.WriteLine(" ID:{0}", domain.ID);
                Console.WriteLine(" 等级:{0}", domain.Level);
                Console.WriteLine(" 域名状态:{0}", domain.Status);
                Console.WriteLine(" 接管状态:{0}", domain.TakeOverStatus);
                Console.WriteLine(" 创建时间:{0}", domain.CreateTime);
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        public void Create()
        {
            Console.WriteLine("请输入要创建的域名：");
            Console.WriteLine(_api.DomainController.Create(Console.ReadLine()));
            Console.ReadLine();
        }

        public void Remove()
        {
            List<CloudXNSDomain> list = _api.DomainController.GetList();
            foreach (CloudXNSDomain domain in list)
            {
                Console.WriteLine(domain);
                Console.WriteLine(" ID:{0}", domain.ID);
                Console.WriteLine();
            }
            Console.WriteLine("请输入要删除的域名ID：");
            try
            {
                Console.WriteLine(_api.DomainController.Remove(Convert.ToInt32(Console.ReadLine())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        public void Stat()
        {
            List<CloudXNSDomain> list = _api.DomainController.GetList();
            foreach (CloudXNSDomain domain in list)
            {
                Console.WriteLine(domain);
                Console.WriteLine(" ID:{0}", domain.ID);
                Console.WriteLine();
            }
            try
            {
                Console.WriteLine("请输入要查询解析量的域名ID：");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入开始日期(格式yyyy-mm-dd)：");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("请输入结束日期(格式yyyy-mm-dd)：");
                DateTime endDate = DateTime.Parse(Console.ReadLine());
                List<CloudXNSDomainStat> statList = _api.DomainController.Stat(id, startDate, endDate);
                foreach (CloudXNSDomainStat stat in statList)
                {
                    Console.WriteLine(stat);
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
