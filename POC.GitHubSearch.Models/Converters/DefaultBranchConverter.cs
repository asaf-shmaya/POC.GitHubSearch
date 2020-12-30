using System;
using Newtonsoft.Json;


namespace POC.GitHubSearch.Models
{
    internal class DefaultBranchConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DefaultBranch) || t == typeof(DefaultBranch?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "master")
            {
                return DefaultBranch.Master;
            }
            throw new Exception("Cannot unmarshal type DefaultBranch");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DefaultBranch)untypedValue;
            if (value == DefaultBranch.Master)
            {
                serializer.Serialize(writer, "master");
                return;
            }
            throw new Exception("Cannot marshal type DefaultBranch");
        }

        public static readonly DefaultBranchConverter Singleton = new DefaultBranchConverter();
    }
}
