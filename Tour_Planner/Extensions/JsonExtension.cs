using System.Text.Json;

namespace Tour_Planner.Extensions;

public static class JsonExtension {
    public static string Beautify<T>(this T obj) {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }
}