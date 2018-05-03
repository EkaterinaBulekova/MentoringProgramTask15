using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.Basic;

namespace Task
{
    [TestClass]
    public class BasicXmlSerializerTests
    {
        private const string FileName = @"..\..\..\TestXmlSerializer.xml";
        private const string BooksFileName = @"..\..\..\books.xml";
        private const string BooksFileNameDeserialize = @"..\..\..\booksDeserealize.xml";
        private const string NameSpaceString = "http://library.by/catalog";
        private readonly XmlSerializerNamespaces _nameSpace;

        private readonly XmlSerializer _serializer;

        private readonly Catalog _fakeCatalog = new Catalog
        {
            Date = DateTime.Today,
            Books = new List<Book>{
                new Book
                {
                    Author = "Some Author",
                    Genre = Genre.ScienceFiction,
                    Id = "b123",
                    Isbn = "treeee-rytrt",
                    Description = "Some description",
                    PublishDate = DateTime.Today,
                    Publisher = "SomePublisher",
                    RegistrationDate = DateTime.Today
                },
                new Book
                {
                    Author = "Other Author",
                    Genre = Genre.Computer,
                    Id = "b456",
                    Isbn = "dgfdg-kjhkjh",
                    Description = "Other description",
                    PublishDate = DateTime.Today,
                    Publisher = "OtherPublisher",
                    RegistrationDate = DateTime.Today
                }
            }
        };

        public BasicXmlSerializerTests()
        {
            _nameSpace = new XmlSerializerNamespaces();
            _nameSpace.Add(string.Empty, NameSpaceString);
            _serializer = new XmlSerializer(typeof(Catalog));
        }   
        

        [TestMethod]
        public void Serialize()
        {
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                _serializer.Serialize(stream, _fakeCatalog, _nameSpace);
                stream.Close();
            }
        }

        [TestMethod]
        public void Deserialize()
        {
            using (var stream = new FileStream(FileName, FileMode.Open))
            {
                if (_serializer.Deserialize(stream) is Catalog catalog)
                    catalog.Books.ToList().ForEach(Console.WriteLine);
            }
        }

        [TestMethod]
        public void DeserializeBooks()
        {
            using (var stream = new FileStream(BooksFileName, FileMode.Open))
            {
                if (_serializer.Deserialize(stream) is Catalog catalog)
                    catalog.Books.ToList().ForEach(Console.WriteLine);
            }

        }

        [TestMethod]
        public void SerializeBooks()
        {
            using (var inStream = new FileStream(BooksFileName, FileMode.Open))
            {
                if (_serializer.Deserialize(inStream) is Catalog catalog)
                {
                    catalog.Books.ToList().ForEach(Console.WriteLine);

                    using (var outStream = new FileStream(BooksFileNameDeserialize, FileMode.Create))
                    {
                        _serializer.Serialize(outStream, catalog, _nameSpace);
                    }
                }
            }
        }
    }
}
