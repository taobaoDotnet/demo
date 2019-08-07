﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using CSRedis;

namespace servicedemo.helpers
{
    using models.cache;

    public class SessionStoreHelper : ITicketStore
    {
        private const string KeyPrefix = "Token";
        private IDistributedCache _cache;
        public SessionStoreHelper(CSRedisClient redisConn)
        {
            _cache = new CSRedisCache(redisConn);
        }
        public SessionStoreHelper()
        {
            _cache = new CSRedisCache(RedisHelper.Instance);
        }
        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var guid = Guid.NewGuid();
            var key = "-" + KeyPrefix + "-" + Newtonsoft.Json.JsonConvert.DeserializeObject<SessionInfo>(ticket.Principal.Identity.Name).UserName;
            // var key = KeyPrefix + guid.ToString().Replace("-","");
            await RenewAsync(key, ticket);
            return key;
        }
        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new DistributedCacheEntryOptions();
            var expiresUtc = ticket.Properties.ExpiresUtc;
            if (expiresUtc.HasValue)
            {
                options.SetAbsoluteExpiration(expiresUtc.Value);
            }
            byte[] val = SerializeToBytes(ticket);
            _cache.Set(key, val, options);
            return Task.FromResult(0);
        }
        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            AuthenticationTicket ticket;
            byte[] bytes = null;
            bytes = _cache.Get(key);
            ticket = DeserializeFromBytes(bytes);
            return Task.FromResult(ticket);
        }
        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.FromResult(0);
        }
        private static byte[] SerializeToBytes(AuthenticationTicket source)
        {
            return TicketSerializer.Default.Serialize(source);
        }
        private static AuthenticationTicket DeserializeFromBytes(byte[] source)
        {
            return source == null ? null : TicketSerializer.Default.Deserialize(source);
        }
    }
}
