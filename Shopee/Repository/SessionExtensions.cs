using System;
using Newtonsoft.Json;

namespace Shopee.Repository
{
    public static class SessionExtensions
    {
        public static void SetJsonCache(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJsonCache<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }

    }
}

