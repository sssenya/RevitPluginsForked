using System;

using Autodesk.Revit.DB;

using dosymep.Bim4Everyone.ProjectConfigs;

using pyRevitLabs.Json;

namespace dosymep.Serializers {
    internal class ConfigSerializer : IConfigSerializer {
        public T Deserialize<T>(string text) {
            return JsonConvert.DeserializeObject<T>(text,
                new ElementIdConverter());
        }

        public string Serialize<T>(T @object) {
            return JsonConvert.SerializeObject(@object,
                new ElementIdConverter());
        }
    }

    internal class ElementIdConverter : JsonConverter<ElementId> {
        public override void WriteJson(JsonWriter writer, ElementId value, JsonSerializer serializer) {
#if REVIT_2023_OR_LESS
            writer.WriteValue(value.IntegerValue);
#else
            writer.WriteValue(value.Value);
#endif
        }

        public override ElementId ReadJson(
            JsonReader reader,
            Type objectType,
            ElementId existingValue,
            bool hasExistingValue,
            JsonSerializer serializer) {
#if REVIT_2023_OR_LESS
            return new ElementId((int) reader.Value);
#else
            return new ElementId((long) reader.Value);
#endif
        }
    }
}