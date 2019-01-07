using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TDH.Common.Caching
{
    public class CacheExtension
    {
        /// <summary>
        /// Add object by key to cache data
        /// </summary>
        /// <typeparam name="T">Object data type</typeparam>
        /// <param name="t">object data</param>
        /// <param name="key">key name</param>
        /// <param name="expiry">expiry time</param>
        public static void Add<T>(T t, string key, DateTime expiry) where T : class
        {
            HttpContext.Current.Cache.Insert(key, t, null, expiry, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Add object by key to cache data
        /// </summary>
        /// <typeparam name="T">Object data type</typeparam>
        /// <param name="t">object data</param>
        /// <param name="key">key name</param>
        /// <param name="hour">default = 1</param>
        public static void Add<T>(T t, string key, int hour = 1) where T : class
        {
            HttpContext.Current.Cache.Insert(key, t, null, DateTime.Now.AddHours(hour), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove object by key from cache data
        /// </summary>
        /// <typeparam name="T">Object data type</typeparam>
        /// <param name="key">key name</param>
        public static void Remove<T>(string key) where T : class
        {
            if (Exists(key))
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }

        /// <summary>
        /// Check exists key in cache data
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns>true/false</returns>
        public static bool Exists(string key)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Cache[key] != null;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get data from key
        /// </summary>
        /// <typeparam name="T">Object data type</typeparam>
        /// <param name="key">key name</param>
        /// <returns>T</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }
    }
}
