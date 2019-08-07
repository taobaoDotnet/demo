using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace servicedemo.Controllers
{
    using servicedemo.models.dto.wrapper;
    using servicedemo.models.dto.request;
    using servicedemo.models.dto.response;
    using servicedemo.services;
    using System;
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly User _user;
        public UserController(User user)
        {
            _user = user;
        }

        [HttpGet]
        public PageResponseT<UserList> GetFilter(PageRequestT<UserFilter> pageRequest)
        {
            return _user.GetFilter(pageRequest);
        }

        [HttpGet("{id}")]
        public ResponseT<UserDetail> Get(int id)
        {
            return _user.GetById(id);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns>用户Id</returns>
        [HttpPost]
        public ResponseT<long> Create([FromBody] RequestT<UserAdd> request)
        {
            return _user.Create(request);
        }
        
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns>影响行数</returns>
        [HttpPost]
        public ResponseT<int> Edit([FromBody] RequestT<UserUpdate> request)
        {
            return _user.Edit(request);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns>影响行数</returns>
        [HttpPost]
        public ResponseT<int> Delete(RequestT<UserDelete> request)
        {
            return _user.Delete(request);
        }
    }
}
