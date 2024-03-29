# demo docs
- [Installation](#installation)
- [Names](#names)
    - [Folder](#folder)
    - [Interface](#interface)
    - [Class](#class)
    - [Function](#funcation)
- [Directory Structure](#directory-structure)
    - [Api](#api)
    - [Service](#service)
    - [Outside Api](#outside-api)
        - [Service Client](#service-client)
        - [Service Event](#service-event)
- [Model](#model)
    - [Request Model](#model-request)
    - [Db Model](#db-model)
    - [Response Model](#response-model)
- [Error](#error)    
- [Pkg](#pkg) 
    - [AppSettings](#AppSettings)
    - [Service Discovery](#service-discovery)
 
## Installation
对dotnet core项目的命名规则、目录结构、错误处理给出了一些建议，并编写了可以运行的代码示例。 
一个优秀的程序需要简单的问题解决方案并且考虑易于维护，本文仅仅对单个微服务项目进行了示例，建议一次调用的过程是PostRequestModel-->Route--Api--Service[ServiceClient,Model,Pkg]-->ResponseModel.项目进行了分层，但是一个实现的函数调用链路尽量扁平，调用链路太长不易维护。 错误信息的暴漏。
一个稳定运行的项目不只是代码，还要综合考虑网络结构、网络带宽、存储结构、存储空间、数据库读写分离、程序发布与线上版本管理。
## Names
首字母缩写词使用相同的大小写，如htmlEscape、EscapeHTML
### package
[style-packages](https://rakyll.org/style-packages/)  
golang中优雅的同名包处理，./service/xxx,./client/xxxx,的包名都是xxx，在xxx_service中调用xxx_client时用xxx.function,虽然同名但是不冲突。虽然支持但是建用xxxclient,xxxservice的double word的命名方式。
1. 简短但具有代表性的名称,仅从软件包的名称中掌握其用途,避免使用"common", "util", "shared", or "lib"等宽泛的命名。
2. 无复数。
3. 
### Folder
文件夹用英文小写命名(参考了Java和Golang包名都是小写).

### Interface
接口不以“I”开头,在构造注入时只写接口名称，不写接口实现名称，若接口用I开始，每次都要先写一个与业务无关的I有点累赘，所以参考了Java的命名。golang的接口用er结尾。

### Class
类名大写。

### Funcation
属性和函数名公有大写，私有小写。

## Directory Structure
```shell
# use “tree /f” create tree
.
├── api
├── pkg
├── modeles
│   ├── request
│   ├── db
│   ├── response
│   ├── wrapper
├── service
│   ├── ServiceRegister.cs
├── service_client
│   ├── ServiceDiscovery.cs
│   ├── hcis_client
│   │   ├── hcis_model
│   │   │   ├── hcis_wrapper_model
│   │   ├── read
│   │   ├── sign
│   │   └── write
│   ├── sis_client
│   │   ├── sis_model
│   │   │   ├── sis_wrapper_model
│   │   ├── read
│   │   ├── sign
│   │   └── write
│   └── tms_client
│   │   ├── tms_model
│   │   │   ├── tms_wrapper_model
│   │   ├── read
│   │   ├── sign
│   │   └── write
└── service_event
```
### Api

### Service
本系统的服务层，调用提供服务给前端使用。这一层不关系api_sign,在api层Request的middleware中做sign验证，无需传到service层。

### Outside Api
本系统依赖的外部系统接口。

#### ServiceClient
outside-supply-api-service,使用第三方提供的outside-supply-api-services-read的数据(read),给第三方系统回写数据outside-supply-api-services-write。因为不同提供方命名规范不同，接口风格不同、header中的sign不同。本系统做为消费者无法改变，只能被动接受。所以服务消费者端目录结构需要按照提供方名称放入client文件夹，client包含read和write操作。
```
└── client
    ├── ServiceDiscovery.cs
    ├── hcis_client
│   │   ├── hcis_model    
    │   ├──read
    │   ├──sign
    │   └──write(EventRequest)
    ├── sis_client
│   │   ├── sis_model    
    │   ├──read
    │   ├──sign
    │   └──write(EventRequest)
    └── tms_client
│   │   ├── tms_model    
        ├──read
        ├──sign
        └──write(EventRequest)
```

#### ServiceEvent(callback)
其他系统同步数据给当前系统，由于改变数据，需要严格控制消费者。给服务消费者颁发token，在排查错误时，可以更换token来对未知调用者进行限制，也需要记录日志来标记是那些消费者调用的服务，比如sis的交管正约callback。  
callback与event的区别是callback是服务提供者(eventService)，event事件是双向的(eventRequest\eventService)，eventRequest通知其他系统，eventService其他系统通知当前系统。eventService需要严格控制消费者。

## Model
实体的常用分类方法是
1. VO(View Object)：界面显示对象。
2. DTO(Data Transfer Object)：数据传输对象。
3. DO(Domain Object)：领域对象。
4. PO(Persistent Object)：持久化对象。 

对于前后端分离项目，VO是前端的工作，所以Micro service项目没有VO，DTO数据传输对象包含RequestDTO和ResponseDTO两个方向的对象，DO领域对象是针对具体业务的对象，DO对象可能是通过多个Service、ServiceClient组成的聚合对象，并通过ResponseDTO返回给消费者，所以合并到了ResponseModel，PO的概念其实大于DB-Model，比如可以持久化到数据库、日志、ES等多种。这里是狭义的一个概念，但是更好理解。
### Request-Model
服务消费者请求的数据。
### Db-Model
与数据库对应的实体。
### Response-Model    
返回给服务消费者的数据模型。包含ResponseDTO和Domain Object.

## Error
1. 把错误透传给调用者，不要到处打印日志。


# Pkg
第三方工具包Helpers
### AppSettings
1.实体注入
 - AppSettings.cs
 - Dependency Injection
	```c#
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		// 配置文件映射
		services.Configure<AppSettings>(Configuration);
	}
  	```
	```
    private readonly AppSettings _settings;
    public UserImpl(IOptionsSnapshot<AppSettings> settings)
    {
        _settings = settings.Value;
    }
	```
### Service Discovery
1.appsettings.json
```c#
  "ServiceDiscovery": {
    "ServiceA": "http://127.0.0.1:63210/",
    "ServiceB": "http://127.0.0.1:63211/",
  },
```
2./clientes/ServiceDiscovery.cs
```c#
 interface ServiceDiscovery
    {
        string GetServiceA();
		string GetServiceB();
    }
```
### global exception

### Redis

### Swagger

### Mq

### Elasticsearch

	
