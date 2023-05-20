using Newtonsoft.Json.Linq;

namespace BeatNetworkAPI;

public static class Utility
{
    public static string GetValueFromJsonFile(string jsonFilePath, string key)
    {
        var json = File.ReadAllText(jsonFilePath);
        var jsonObject = JObject.Parse(json);
        return jsonObject[key].ToString();
    }
}