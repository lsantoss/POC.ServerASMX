using Newtonsoft.Json;

namespace POC.ServerASMX.Test.Base.Extensions
{
    public static class JsonFormatter
    {
        public static string Format(this string json)
        {
            var obj = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static string Format(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}