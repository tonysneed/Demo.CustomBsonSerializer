# Demo: Customer Bson Serializer

Demo for creating a custom bson serializer.

### Problem

```csharp
public class MyNestedClass
{
    public int Id;
    public double X;
    public MyClass MyClass { get; set; } = null!;
}
```

Serialize `MyClass` as a JSON blob.
Deserialize it from JSON back to `MyClass`.

```json
{
    "_id": 2,
    "X": 39.990000000000002,
    "MyClass": "{\n  \"Id\": 1,\n  \"X\": 29.99\n}"
}
```

### Solution

1. Add package reference to **MongoDB.Bson**.

2. Create class that extends `SerializerBase<MyClass>`.
   - Use `JsonSerializer` to convert `MyClass` to JSON.

```csharp
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
```

3. Register `CustomDoubleSerializer`.

```csharp
BsonSerializer.RegisterSerializer(new CustomDoubleSerializer());
```

4. To use multiple custom serializers, create a class that implements `IBsonSerializationProvider`.

```csharp
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
```

5. Register `CustomSerializationProvider`.

```csharp
BsonSerializer.RegisterSerializationProvider(new CustomSerializationProvider());
```
