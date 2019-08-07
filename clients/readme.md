# client
- 服务(网关)名称(sis/kofang/koboss)
	- Client Model
	  - request model
	  - response model
	- Http Client:HttpClientHepler.cs
	  - https://flurl.dev/docs/fluent-url
	- Service Discovery
	  - appsettings.json

统一错误处理无法捕捉消费其他服务发生异常时的地址和数据。
错误消息的链式跟踪和错误消息返回服务消费者很重要、记录日志很重要。
service-->clientRequet其他服务，目前无法形成"调用链"、"请求数据"、"地址"。