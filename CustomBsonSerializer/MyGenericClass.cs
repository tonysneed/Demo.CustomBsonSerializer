namespace CustomBsonSerializer;

public class MyGenericClass<TId, TX>
{
    public TId Id { get; set; } = default!;
    public TX X { get; set; } = default!;
}