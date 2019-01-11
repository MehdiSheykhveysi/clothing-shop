using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Entities;

namespace ShopCenter.Web.Infrastracture {
    public static class Extensions {

        public static void SetJson (this ISession session, string Key, object value) {
            session.SetString(Key,JsonConvert.SerializeObject(value));
        }
        public static T GetObject<T> (this ISession session, string Key) {
            string SessionData = session.GetString (Key);
            return SessionData == null?default (T) : JsonConvert.DeserializeObject<T> (SessionData);
        }
    }
}