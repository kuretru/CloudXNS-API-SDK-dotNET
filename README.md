# CloudXNS API SDK on dotNET
使用.NET封装的CloudXNS官方API接口，方便使用，欢迎提Issues。  
[![version](https://img.shields.io/badge/version-v1.0-red.svg)](https://github.com/kuretru/CloudXNS-API-SDK-dotNET/releases/latest)
[![Build](https://img.shields.io/badge/build-20160912-brightgreen.svg)](https://github.com/kuretru/CloudXNS-API-SDK-dotNET/releases/latest)
[![License](https://img.shields.io/badge/License-GPLV3-orange.svg)](LICENSE.md) ![nuget](https://img.shields.io/badge/nuget-v3.5.0-blue.svg)

### 环境要求
* 使用 CloudXNS 官方 API 接口
* 基于 .NET Framework 4.0/2.0
* 使用 Json.NET - Newtonsoft

### 功能说明
使用.NET实现了官方API接口中提供的所有功能，包括：
* 获取记录类型/线路/区域/ISP/NS服务器列表
* 获取域名列表
* 添加/删除域名
* 统计域名解析量
* 获取主机记录列表
* 删除主机记录
* 获取解析记录列表
* 添加/更新/删除解析记录
* 暂停解析记录/启用X优化
* DDNS快速修改

### 使用方法
```
using Kuretru.CloudXNSAPI;
using Kuretru.CloudXNSAPI.Model;

APIResponse response = APIManager.Login(apiKey, secretKey);
APIManager api;
if (response.Code == 1)
  {
      api = new APIManager(apiKey, secretKey);
      List<CloudXNSDomain> domainList = _api.DomainController.GetList();
      CloudXNSDomain domain = domainList.ToArray()[0];
      List<CloudXNSRecord> recordList = api.RecordController.GetList(domain.ID);
      api.RecordController.DDNS("www.cloudxns.net");
  }
```
*更多使用方法参考UnitTest项目。*

### 更新日志
###### 2016-8-12
1. 发布第一个版本

### LICENSE
GNU General Public License v3.0

### 参考资料 :paperclip:
1. https://www.cloudxns.net/Support/detail/id/1361.html -- CloudXNS 官方 API 文档
2. http://git.oschina.net/zhengwei804/DNSPodForNET -- DNSPodForNET
