using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kuretru.CloudXNSAPI;

namespace UnitTest
{
    class Program
    {
        private static CloudXNSAPI _api;

        static void Main(string[] args)
        {
            while (true)
            {
                PrintMainMenu();
            }
        }

        static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("CloudXNS API SDK on dotNET unit test");
            Console.WriteLine("测试项目：");
            Console.WriteLine("0.退出");
            Console.WriteLine("1.验证API Key(成功后使用该账户进行后续测试)");
            if (_api != null)
            {
                Console.WriteLine("2.获取CloudXNS信息");
                Console.WriteLine("3.域名相关");
                Console.WriteLine("4.主机记录相关");
                Console.WriteLine("5.解析记录相关");
            }
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
            switch (result)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Login();
                    break;
                case 2:
                    if (_api != null)
                        new InformationTest(_api);
                    break;
                case 3:
                    if (_api != null)
                        new DomainTest(_api);
                    break;
                case 4:
                    if (_api != null)
                        new HostTest(_api);
                    break;
                case 5:
                    if (_api != null)
                        new RecordTest(_api);
                    break;
                default:
                    break;
            }
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("输入你的API Key");
            //string apiKey = Console.ReadLine();
            string apiKey = "db468694f7f429d15a21d73f095030fc";
            Console.WriteLine("输入你的Secret Key");
            //string secretKey = Console.ReadLine();
            string secretKey = "8246c667ed2fe08c";
            Kuretru.CloudXNSAPI.Model.APIResponse response = CloudXNSAPI.Login(apiKey, secretKey);
            Console.WriteLine(response);
            Console.ReadLine();
            if (response.Code == 1)
            {
                _api = new CloudXNSAPI(apiKey, secretKey);
            }
        }

        static object GetUserInput(Type t, string title)
        {
            return "";
        }
    }
}
