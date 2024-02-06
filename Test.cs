using XMLData;

class Test
{
    public static void Main(string[] args)
    {
        var xmlConversion = new DocumentConverter<XMLData.People.Document>(args[0]);
        xmlConversion.ToFile(args[1]);
    }
}