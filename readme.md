# demo docs
- [Names](#names)
    - [Folder](#folder)
    - [Interface](#interface)
    - [Class](#class)
    - [Function](#funcation)
- [Directory Structure](#directory-structure)
    - [Api](#api)
    - [Service](#service)
    - [Outside Api](#outside-api)
        - [Client](#client)
        - [Callback](#callback)
- [Model](#model)
    - [Request Model](#model-request)
    - [Db Model](#db-model)
    - [Response Model](#response-model)    
- [Pkg](#pkg) 
    - [AppSettings](#AppSettings)
    - [Service Discovery](#service-discovery)
 
## Names
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
# 在文件夹生成目录结构
tree /f
```
### Api

### Service
本系统的服务层，调用提供服务给前端使用。这一层不关系api_sign,在api层Request的middleware中做sign验证，无需传到service层。

### Outside Api
本系统依赖的外部系统接口。

#### Client
outside-supply-api-service,使用第三方提供的outside-supply-api-services-read的数据(read),给第三方系统回写数据outside-supply-api-services-write。因为不同提供方命名规范不同，接口风格不同、header中的sign不同。本系统做为消费者无法改变，只能被动接受。所以服务消费者端目录结构需要按照提供方名称放入client文件夹，client包含read和write操作。
```
└── client
    ├── hcis_client
    │   ├──read
    │   ├──sign
    │   └──write
    ├── sis_client
    │   ├──read
    │   ├──sign
    │   └──write
    └── tms_client
        ├──read
        ├──sign
        └──write
```

#### Callback
其他系统同步数据给当前系统，由于改变数据，需要严格控制消费者。给服务消费者颁发token，在排查错误时，可以更换token来对未知调用者进行限制，也需要记录日志来标记是那些消费者调用的服务，比如sis的交管正约callback。  
callback与event的区别是callback是服务提供者(eventService)，event事件是双向的(eventRequest\eventService)，eventRequest通知其他系统，eventService其他系统通知当前系统。eventService需要严格控制消费者。

## Model
### Request-Model

### Db-Model

### Eesponse-Model    

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
### service discovery
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

	
