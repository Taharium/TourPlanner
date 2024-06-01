
using Newtonsoft.Json;

namespace BusinessLayer.Extensions;

public static class JsonExtension {
    public static string Serialize<T>(this T obj) {
        return JsonConvert.SerializeObject(obj);
    }
}