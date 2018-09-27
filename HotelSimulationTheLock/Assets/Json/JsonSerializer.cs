//// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
////
////    using QuickType;
////
////    var purpleJsonSerializer = PurpleJsonSerializer.FromJson(jsonString);

//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;


//namespace HotelSimulationTheLock
//{

//    public partial class PurpleJsonSerializer
//    {
//        [JsonProperty("AreaType")]
//        public AreaType AreaType { get; set; }

//        [JsonProperty("Position")]
//        public string Position { get; set; }

//        [JsonProperty("Dimention")]
//        public Dimention Dimention { get; set; }

//        [JsonProperty("Capacity", NullValueHandling = NullValueHandling.Ignore)]
//        public long? Capacity { get; set; }

//        [JsonProperty("Classification", NullValueHandling = NullValueHandling.Ignore)]
//        public string Classification { get; set; }
//    }

//    public enum AreaType { Cinema, Restaurant, Room };

//    public enum Dimention { The11, The21, The22 };
//    public partial class PurpleJsonSerializer
//    {
//        public static PurpleJsonSerializer[] FromJson(string json) => JsonConvert.DeserializeObject<PurpleJsonSerializer[]>(json, QuickType.Converter.Settings);
//    }

//    public static class Serialize
//    {
//        public static string ToJson(this PurpleJsonSerializer[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
//    }

//    internal static class Converter
//    {
//        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//        {
//            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//            DateParseHandling = DateParseHandling.None,
//            Converters = {
//                AreaTypeConverter.Singleton,
//                DimentionConverter.Singleton,
//                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//            },
//        };
//    }

//    internal class AreaTypeConverter : JsonConverter
//    {
//        public override bool CanConvert(Type t) => t == typeof(AreaType) || t == typeof(AreaType?);

//        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//        {
//            if (reader.TokenType == JsonToken.Null) return null;
//            var value = serializer.Deserialize<string>(reader);
//            switch (value)
//            {
//                case "Cinema":
//                    return AreaType.Cinema;
//                case "Restaurant":
//                    return AreaType.Restaurant;
//                case "Room":
//                    return AreaType.Room;
//            }
//            throw new Exception("Cannot unmarshal type AreaType");
//        }

//        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//        {
//            if (untypedValue == null)
//            {
//                serializer.Serialize(writer, null);
//                return;
//            }
//            var value = (AreaType)untypedValue;
//            switch (value)
//            {
//                case AreaType.Cinema:
//                    serializer.Serialize(writer, "Cinema");
//                    return;
//                case AreaType.Restaurant:
//                    serializer.Serialize(writer, "Restaurant");
//                    return;
//                case AreaType.Room:
//                    serializer.Serialize(writer, "Room");
//                    return;
//            }
//            throw new Exception("Cannot marshal type AreaType");
//        }

//        public static readonly AreaTypeConverter Singleton = new AreaTypeConverter();
//    }

//    internal class DimentionConverter : JsonConverter
//    {
//        public override bool CanConvert(Type t) => t == typeof(Dimention) || t == typeof(Dimention?);

//        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//        {
//            if (reader.TokenType == JsonToken.Null) return null;
//            var value = serializer.Deserialize<string>(reader);
//            switch (value)
//            {
//                case "1, 1":
//                    return Dimention.The11;
//                case "2, 1":
//                    return Dimention.The21;
//                case "2, 2":
//                    return Dimention.The22;
//            }
//            throw new Exception("Cannot unmarshal type Dimention");
//        }

//        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//        {
//            if (untypedValue == null)
//            {
//                serializer.Serialize(writer, null);
//                return;
//            }
//            var value = (Dimention)untypedValue;
//            switch (value)
//            {
//                case Dimention.The11:
//                    serializer.Serialize(writer, "1, 1");
//                    return;
//                case Dimention.The21:
//                    serializer.Serialize(writer, "2, 1");
//                    return;
//                case Dimention.The22:
//                    serializer.Serialize(writer, "2, 2");
//                    return;
//            }
//            throw new Exception("Cannot marshal type Dimention");
//        }

//        public static readonly DimentionConverter Singleton = new DimentionConverter();
//    }
//}
