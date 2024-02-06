using System.Xml.Serialization;

namespace XMLData
{

    // Interface all `Document` classes must implement to work with this converter.
    public interface IDocument
    {
        static abstract IDocument New();
        public void Populate(StreamReader fs) { }
    }

    class DocumentConverter<T> where T : IDocument
    {
        private readonly XmlSerializer serializer;
        private readonly T document;

        // Reads a file and populates `xmlDocument` members with values parsed from stream.
        public DocumentConverter(string filepath)
        {
            if (filepath == null)
            {
                throw new Exception("No file provided!");
            }
            document = (T)T.New();
            serializer = new XmlSerializer(typeof(T));
            document.Populate(new StreamReader(filepath));
        }

        public void ToFile(string output = "")
        {
            try
            {
                TextWriter writer = new StreamWriter(output ?? "output.xml");
                serializer.Serialize(writer, document);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}