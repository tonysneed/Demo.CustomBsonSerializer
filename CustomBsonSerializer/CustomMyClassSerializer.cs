using System.Text.Json;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CustomBsonSerializer;

public class CustomMyClassSerializer : SerializerBase<MyClass>
{
    public override MyClass Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var json = context.Reader.ReadString();
        var value = JsonSerializer.Deserialize<MyClass>(json);
        return value!;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, MyClass value)
    {
        // var json = value.ToJson();
        var serializerOptions = new JsonSerializerOptions {WriteIndented = true};
        var json = JsonSerializer.Serialize(value, serializerOptions);
        context.Writer.WriteString(json);
    }
}