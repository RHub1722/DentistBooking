using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace DentistBooking
{
    public static class Helpers
    {
        public static string GetRequiredString(this RouteData routeData, string keyName)
        {
            object value;
            if (!routeData.Values.TryGetValue(keyName, out value))
            {
                throw new InvalidOperationException($"Could not find key with name '{keyName}'");
            }

            return value?.ToString();
        }

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
