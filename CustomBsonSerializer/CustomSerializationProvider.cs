using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CustomBsonSerializer;

public class CustomSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer GetSerializer(Type type)
    {
        if (type == typeof(decimal)) return new DecimalSerializer(BsonType.Decimal128);
        if (type == typeof(double)) return new CustomDoubleSerializer();
        if (type == typeof(MyClass)) return new CustomMyClassSerializer();
        return null!; // falls back to Mongo defaults
    }
}