namespace CustomBsonSerializer;

public class MyNestedClass
{
    public int Id;
    public double X;
    public MyClass MyClass { get; set; } = null!;
}