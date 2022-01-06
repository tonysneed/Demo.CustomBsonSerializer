using System.Text.Json;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CustomBsonSerializer;

public class MyCustomSerializer<TClass> : SerializerBase<TClass>
{
    public override TClass Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var json = context.Reader.ReadString();
        var value = JsonSerializer.Deserialize<TClass>(json);
        return value!;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TClass value)
    {
        var serializerOptions = new JsonSerializerOptions {WriteIndented = true};
        var json = JsonSerializer.Serialize(value, serializerOptions);
        context.Writer.WriteString(json);
    }
}