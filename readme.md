# 命名
## 文件夹小写
>
参考了Java和Golang包名都是小写。
https://blog.csdn.net/bob_dadoudou/article/details/79476612
## 接口不以“I”开头
>
在构造注入时只写接口名称，不写接口实现名称，若接口用I开始，每次都要先写一个与业务无关的I有点累赘，所以参考了Java的命名。
## 类名
>
大写。
## 属性和函数名
>
公有大写，私有小写。
## client文件夹
>
服务消费者代码都按照提供方分别放在client文件夹，因为不同提供方命名规范不同，接口风格不同，并且做为消费者无法改变，只能被动接受。

# 模块
## 读取配置
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
## service discovery
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
## global exception

## redis

## swagger

## mq

## elasticsearch

	
