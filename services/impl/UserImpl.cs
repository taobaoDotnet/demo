using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace servicedemo.services
{
    using models.dto.request;
    using models.dto.response;
    using models.dto.wrapper;

    public class UserImpl : User
    {
        // https://github.com/StackExchange/Dapper

        IDbConnection MysqlConn;

        private readonly AppSettings _settings;
        public UserImpl(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings.Value;
            MysqlConn = new MySqlConnection(_settings.ConnectionStrings.MySqlDemo);
         //   MysqlConn.Open();
        }

        //public IConfiguration Configuration { get; }
        //public UserImpl(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //    MysqlConn = new MySqlConnection(Configuration.GetConnectionString("MsSqlDemo"));
        //    MysqlConn.Open();
        //}

        public ResponseT<long> Create(RequestT<UserAdd> userAdd)
        {
            var response = new ResponseT<long>();
            using (MysqlConn)
            {
                response.data = MysqlConn.Execute(@"insert  into `user`(name, sex) values (@name, @sex)", userAdd.data);
            }
            return response;
        }

        public ResponseT<int> Edit(RequestT<UserUpdate> userUpdate)
        {
            var response = new ResponseT<int>();
            using (MysqlConn)
            {
                response.data = MysqlConn.Execute(@"update user set name=@name, sex=@sex where userid=@userid", userUpdate);
            }

            return response;
        }

        public ResponseT<int> Delete(RequestT<UserDelete> userDelete)
        {
            var response = new ResponseT<int>();
            using (MysqlConn)
            {
                response.data = MysqlConn.Execute($"delete from user where userid=@ids", userDelete.data);
            }

            return response;
        }

        public ResponseT<UserDetail> GetById(long id)
        {
         //   System.IO.File.Create("f:/aaa.xyz");
            var response = new ResponseT<UserDetail>();
            using (MysqlConn)
            {
                var users = MysqlConn.Query<UserDetail>($"select * from  user where userid={id}").ToArray();
                if (users.Count() > 0)
                {
                    response.data = users[0];
                }
                else
                {
                    response.msg = "no data";
                }
            }
            return response;
        }

        public PageResponseT<UserList> GetFilter(PageRequestT<UserFilter> userFilter)
        {
            var response = new PageResponseT<UserList>();
            using (MysqlConn)
            {
                response.dataList = MysqlConn.Query<UserList>($"SELECT * FROM `user`").ToList();
            }
            return response;
        }
    }
}
