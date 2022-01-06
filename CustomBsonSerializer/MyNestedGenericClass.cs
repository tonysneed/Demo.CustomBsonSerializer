namespace CustomBsonSerializer;

public class MyNestedGenericClass
{
    public int Id;
    public double X;
    public MyGenericClass<int, double> MyClass { get; set; } = null!;
}