using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Task.Basic
{
    [Serializable]
    [XmlRoot(ElementName = "catalog", Namespace = "http://library.by/catalog")]
    public class Catalog 
    {
        [XmlAttribute("date", DataType = "date")]
        public DateTime Date { get; set; }

        [XmlElement("book")]
        public List<Book> Books { get; set; }
    }
}
