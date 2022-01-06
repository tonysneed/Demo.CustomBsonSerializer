// See https://aka.ms/new-console-template for more information

using CustomBsonSerializer;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

Console.WriteLine("Custom Bson serializer ...");

// BsonSerializer.RegisterSerializer(new CustomMyClassSerializer());
BsonSerializer.RegisterSerializationProvider(new CustomSerializationProvider());

var c = new MyClass { Id = 1, X = 29.99 };
var json = c.ToJson();
Console.WriteLine(json);

var r = BsonSerializer.Deserialize<MyClass>(json);
Console.WriteLine($"{r.Id} {r.X}");

Console.WriteLine("\nSerialize nested class as string ...");

var n = new MyNestedClass
{
    Id = 2,
    X = 39.99,
    MyClass = c
};

var json2 = n.ToJson(new JsonWriterSettings { Indent = true });
Console.WriteLine(json2);

var n2 = BsonSerializer.Deserialize<MyNestedClass>(json2);
Console.WriteLine($"MyNestedClass:\n{n2.Id} {n2.X}");
Console.WriteLine($"- MyClass: {n2.MyClass.Id} {n2.MyClass.X}");

Console.WriteLine("\nSerialize nested generic class as string ...");

var g = new MyNestedGenericClass
{
    Id = 2,
    X = 39.99,
    MyClass = new MyGenericClass<int, double>
    {
        Id = 1,
        X = 29.99
    }
};

var json3 = g.ToJson(new JsonWriterSettings { Indent = true });
Console.WriteLine(json3);

var n3 = BsonSerializer.Deserialize<MyNestedGenericClass>(json3);
Console.WriteLine($"MyNestedClass:\n{n3.Id} {n3.X}");
Console.WriteLine($"- MyClass: {n3.MyClass.Id} {n3.MyClass.X}");
