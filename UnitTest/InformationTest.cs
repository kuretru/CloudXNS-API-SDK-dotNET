using System;
using System.Collections.Generic;
using Kuretru.CloudXNSAPI;
using Kuretru.CloudXNSAPI.Model;


namespace UnitTest
{
    public class InformationTest
    {
        private CloudXNSAPI _api;
        private bool _continue = true;

        public InformationTest(CloudXNSAPI api)
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
            Console.WriteLine("1.获取记录类型列表");
            Console.WriteLine("2.获取线路列表");
            Console.WriteLine("3.获取区域列表");
            Console.WriteLine("4.获取ISP列表");
            Console.WriteLine("5.获取NS服务器");
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
                    GetRecordTypes();
                    break;
                case 2:
                    GetLineList();
                    break;
                case 3:
                    GetRegionLIst();
                    break;
                case 4:
                    GetISPList();
                    break;
                case 5:
                    GetNSList();
                    break;
                default:
                    break;
            }
        }

        private void GetRecordTypes()
        {
            List<string> list = _api.InformationController.GetRecordTypes();
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }

        private void CloudXNSLineProcessor(List<CloudXNSLine> list,ref int level)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CloudXNSLine line = list.ToArray()[i];
                string text = "";
                for (int j = 0; j < level; j++)
                {
                    text += " ";
                }
                text += line;
                Console.WriteLine(text);
                if (line.Children != null)
                {
                    level++;
                    CloudXNSLineProcessor(line.Children, ref level);
                }
                if (i == list.Count - 1)
                {
                    level--;
                }
            }
        }

        private void GetLineList()
        {
            List<CloudXNSLine> list = _api.InformationController.GetLineList();
            if (list != null)
            {
                int level = 0;
                CloudXNSLineProcessor(list, ref level);
            }
            Console.ReadLine();
        }

        private void GetRegionLIst()
        {
            List<CloudXNSRegion> list = _api.InformationController.GetRegionList();
            foreach (CloudXNSRegion region in list)
            {
                Console.WriteLine(region);
            }
            Console.ReadLine();
        }

        private void GetISPList()
        {
            List<CloudXNSISP> list = _api.InformationController.GetISPList();
            foreach (CloudXNSISP isp in list)
            {
                Console.WriteLine(isp);
            }
            Console.ReadLine();
        }

        private void GetNSList()
        {
            List<CloudXNSNameServer> list = _api.InformationController.GetNSList();
            foreach (CloudXNSNameServer ns in list)
            {
                Console.WriteLine(ns);
                foreach (string s in ns.NameServers)
                {
                    Console.WriteLine(" {0}", s);
                }
            }
            Console.ReadLine();
        }
    }
}
