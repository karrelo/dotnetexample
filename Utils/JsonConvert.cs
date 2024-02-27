global using dotnetcqstemplate.Utils.Json;
namespace dotnetcqstemplate.Utils.Json;

public class JsonConvert
{
    private static readonly Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings = new() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };

    public static T DeserializeObject<T>(string data) => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
    public static string Serialize<T>(T @object) => Newtonsoft.Json.JsonConvert.SerializeObject(@object, JsonSerializerSettings);
}