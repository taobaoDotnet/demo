# names命名
### 文件夹小写
>
参考了Java和Golang包名都是小写。
https://blog.csdn.net/bob_dadoudou/article/details/79476612
### 接口不以“I”开头
>
在构造注入时只写接口名称，不写接口实现名称，若接口用I开始，每次都要先写一个与业务无关的I有点累赘，所以参考了Java的命名。
### 类名
>
大写。
### 属性和函数名
>
公有大写，私有小写。

# 目录结构
### service
>
系统的功能接口，调用提供服务给前端使用。这一层不关系api_sign,在api层Request的middleware中做sign验证，无需传到service层。
### 外部服务依赖
#### client
>
因为不同提供方命名规范不同，接口风格不同，header中的sign也不同，并且做为消费者无法改变，只能被动接受。做为消费者角色的代码需要按照提供方名称放入client文件夹。client包含read和write操作。
- hcis_client(hcis_sign)
- tms_client(tms_sign)
- sis_client(sis_sign)
- contract_client(contract_sign)
#### callback
其他系统同步数据给当前系统，由于改变数据，需要严格控制消费者。给服务消费者颁发token，在排查错误时，可以更换token来对未知调用者进行限制，也需要记录日志来标记是那些消费者调用的服务，比如sis的交管正约callback。  
callback与event的区别是callback是服务提供者(eventService)，event事件是双向的(eventRequest\eventService)，通知其他系统，其他系统通知当前系统。

# model实体
>
- PO:Persistent Object,PO的属性与**数据库字段**一一对应（具有CRUD方法的POCO）。
- DO:Domain Object,DO的属性与**业务属性**一一对应，是对**现实世界**各种业务角色的抽象，比如单据审核、转帐……。
- DTO:Data Transfer Object，DTO是展示层与服务层之间的数据传输对象，展示层向服务层传递的DTO与服务层返回给展示层的DTO在概念上是不同的。
  - 服务提供者：用输入输出命名(I/O)**DtoIn服务层需接收的数据**、**DtoOut服务层返回的数据**。
  - 服务消费者：用HttpRequest命名(请求/响应)，传给服务的数据用DtoRequest，服务返回的数据DtoResponse。
- VO:View Object,VO是展示层需要显示的数据对象即UI Model。

### model传递过程
>
展示层依赖于服务层、服务层依赖于领域层、领域层依赖持久层。
 * 前端/服务消费者(Front/Consumer)(Consumer_VO->Consumer_Request)-->Service_DtoIn-->Service_DtoOut->Consumer_Response-->Consumer_VO：用户注册提交的表单数据VO-->VO转化为Dto_Request(DtoIn)传给服务层。
 
 * 服务提供者->(Service_DtoIn->DO)->Service_DtoOut：服务层(业务操作组合)根据业务逻辑把DtoIn对象构造为DO。调用领域层。
 * 领域层(DO->PO)->DO：领域层(业务操作)把DO转换为持久层对应的PO，调用持久层。
 * 持久层(PO)->DO：对数据库进行操作。
 
### 裁剪
持久层
>>
- 服务层可以根据需要直接持久化数据。
- 领域层可以直接持久化数据。

DTO&VO
>>
DTO代表服务层需要接收的数据和返回的数据，而VO代表展示层需要显示的对象,那用户提交的VO就是DtoIn，DtoOut就是UI显示的对象,但是现在多端展示数据，从职责单一原则来看，服务层只负责业务，与具体的表现形式无关，因此，它返回的DTO，不应该出现与表现形式的耦合。  
  在**实现层面**VO可以退隐，用DTO代替VO做UI MODEL，但是设计层面明确规则：服务层的职责依然不应该与展示层耦合，UI需要定制需要在前端实现。

DTO&DO
>>
为什么不在服务层中直接返回DO呢？这样可以省去DTO的编码和转换工作？
- DTO可能对应多个DO。getRealtyInfo(){PutElasticSerch();getRealty()}
- DO具有一些不应该让展示层知道的数据。如building_name、room_name、floor_name、address。可以通过RealtyInfoDtoOut通过继承RealtyDO，然后把把这四个属性隐藏为私有的。这些敏感信息单独封装一个DtoOut对象。DtoIn对象因为需要查询

- DTO涉及数据在网络上的传输、序列化和反序列化，只返回服务的所需要的属性。

对于一个getUser方法来说，本质上它永远不应该返回用户的密码，因此UserInfo至少比User少一个password的数据。而在领域驱动设计中，正如第一篇系列文章所说，DO不是简单的POJO，它具有领域业务逻辑。

DO&PO
>>
- 不需要持久化的DO属性，没有对应的PO
- PO的对象关系属性，比如机器人稽查结果数据的应用，有多个业务需要使用稽查结果来更新数据，需要在数据库中记录那些稽查结果已经更新，可以记录稽查结果最大ID，但是这个标记对DO没有意义，只存在PO就可以了。
- PO属性的锁对DO没有意义：为了实现“乐观锁”，PO存在一个version的属性。

### 原则
>>
分析设计层面和实现层面完全是两个独立的层面，即使实现层面通过某种技术手段可以把两个完全独立的概念合二为一，在分析设计层面，我们仍然（至少在头脑中）需要把概念上独立的东西清晰的区分开来，这个原则对于做好分析设计非常重要（工具越先进，往往会让我们越麻木）。  
不能永远的理想化的去选择所谓“最好的设计”，在必要的情况下，我们还是要敢于放弃，因为最合适的设计才是最好的设计。

### PODODTO的继承策略
>
当我们在定义一个类并继承了其它类的时候，在派生类中是没有办法删除基类的任何成员，就像我们不能改变父母的基因一样，所能做的只能采用隐藏父类方法，也就像使基因变为隐性基因。具体方式如下：
    屏蔽数据成员：在派生类中声明名称和类型相同的成员
    屏蔽函数成员：在派生类中声明新的函数签名相同的成员
    让编译器知道：在派生类中声明新的函数签名相同的成员前面加上new关键字，否则会有警告。


# Helpers 第三方工具包
### 读取配置
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

### redis

### swagger

### mq

### elasticsearch

	
