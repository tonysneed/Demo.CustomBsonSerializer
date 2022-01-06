using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CustomBsonSerializer;

public class CustomDoubleSerializer : SerializerBase<double>
{
    public override double Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var rep = context.Reader.ReadInt64();
        return rep / 100.0;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, double value)
    {
        var rep = (long)(value * 100);
        context.Writer.WriteInt64(rep);
    }
}