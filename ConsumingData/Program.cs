using System;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace ConsumingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("Harry Potter and the Philosopher's Stone", "Machaon", 2016, 432,
                "The eleven-year-old orphan boy Harry Potter lives in the family of his aunt and does not even suspect that he is a real wizard. But one day an owl arrives with a letter for him, and Harry Potter's life changes forever ...");


            while (true)
            {
                Console.Write("1.Write object in JSON (and show file contents);" +
                              "\n2.Read object from JSON (and show the resulting objects);" +
                              "\n3.Write object in XML (and show file contents);" +
                              "\n4.Read object from XML (and show the resulting objects);" +
                              "\n5.Display information about books;" +
                              "\n6.Content of JSON file;" +
                              "\n7.Content of XML file;" +
                              "\n8.Exit;" +
                              "\nEnter option number or option itself: ");

                switch (Console.ReadLine().ToLower())
                {
                    //Read and Write the JSON format is done using Newtonsoft.Json namespace
                    case "1":
                    case "write object in json":
                        Console.WriteLine();
                        using (StreamWriter stream = new StreamWriter("book.json", false))
                        {
                            StringBuilder sb = new StringBuilder();
                            using (JsonWriter writer = new JsonTextWriter(new StringWriter(sb)))
                            {
                                writer.Formatting = Newtonsoft.Json.Formatting.Indented;

                                writer.WriteStartObject();
                                writer.WritePropertyName("Name");
                                writer.WriteValue(book.Name);
                                writer.WritePropertyName("Publisher");
                                writer.WriteValue(book.Publisher);
                                writer.WritePropertyName("TheYearOfPublishing");
                                writer.WriteValue(book.TheYearOfPublishing);
                                writer.WritePropertyName("Pages");
                                writer.WriteValue(book.Pages);
                                if (book.Description != null)
                                {
                                    writer.WritePropertyName("Description");
                                    writer.WriteValue(book.Description);
                                }
                                writer.WriteEndObject();
                            }

                            Console.WriteLine(sb.ToString());
                            stream.Write(sb.ToString());
                        }
                        Console.WriteLine();
                        break;

                    case "2":
                    case "read object from json":
                        Console.WriteLine();
                        using (StreamReader stream = new StreamReader("book.json"))
                        {
                            JsonTextReader reader = new JsonTextReader(new StringReader(stream.ReadToEnd()));
                            while (reader.Read())
                            {
                                if (reader.Value != null)
                                {
                                    switch (reader.Value.ToString())
                                    {
                                        case "Name":
                                            reader.Read();
                                            if (reader.Value != null)
                                                book.Name = reader.Value.ToString();
                                            break;
                                        case "Publisher":
                                            reader.Read();
                                            if (reader.Value != null)
                                                book.Publisher = reader.Value.ToString();
                                            break;
                                        case "TheYearOfPublishing":
                                            reader.Read();
                                            if (reader.Value != null)
                                                book.TheYearOfPublishing = Convert.ToInt32(reader.Value.ToString());
                                            break;
                                        case "Pages":
                                            reader.Read();
                                            if (reader.Value != null)
                                                book.Pages = Convert.ToInt32(reader.Value.ToString());
                                            break;
                                        case "Description":
                                            reader.Read();
                                            if (reader.Value != null)
                                                book.Description = reader.Value.ToString();
                                            break;
                                        default:
                                            throw new Exception("developer unintended element");
                                    }
                                }
                            }
                            book.PrintInfo();
                        }
                        Console.WriteLine();
                        break;

                    //Creating an xml document file using the System.Xml namespace
                    case "3":
                    case "write object in xml":
                        Console.WriteLine();
                        using (StreamWriter stream = new StreamWriter("book.xml", false))
                        {
                            StringWriter sw = new StringWriter();
                            using (XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true }))
                            {
                                writer.WriteStartDocument();
                                writer.WriteStartElement("book");
                                writer.WriteAttributeString("name", book.Name);
                                writer.WriteAttributeString("publisher", book.Publisher);
                                writer.WriteElementString("theYearOfPublishing", book.TheYearOfPublishing.ToString());
                                writer.WriteElementString("pages", book.Pages.ToString());
                                if (book.Description != null)
                                    writer.WriteElementString("description", book.Description);
                                writer.WriteEndElement();

                                writer.Flush();
                            }

                            Console.WriteLine(sw.ToString());
                            stream.WriteLine(sw.ToString());
                        }
                        Console.WriteLine();
                        break;

                    case "4":
                    case "read object from xml":
                        Console.WriteLine();
                        using (StreamReader stream = new StreamReader("book.xml"))
                        {
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.LoadXml(stream.ReadToEnd());
                            XmlElement xRoot = xDoc.DocumentElement;

                            book = new Book();

                            XmlNode attr = xRoot.Attributes.GetNamedItem("name");
                            if (attr != null)
                                book.Name = attr.Value;
                            attr = xRoot.Attributes.GetNamedItem("publisher");
                            if (attr != null)
                                book.Publisher = attr.Value;

                            foreach (XmlNode childnode in xRoot.ChildNodes)
                            {
                                if (childnode.Name == "theYearOfPublishing")
                                    book.TheYearOfPublishing = Convert.ToInt32(childnode.InnerText);

                                if (childnode.Name == "pages")
                                    book.Pages = Convert.ToInt32(childnode.InnerText);

                                if (childnode.Name == "description")
                                    book.Description = childnode.InnerText;
                            }
                            book.PrintInfo();
                        }
                        Console.WriteLine();
                        break;

                    case "5":
                    case "display information about books":
                        Console.WriteLine();
                        book.PrintInfo();
                        Console.WriteLine();
                        break;

                    case "6":
                    case "content of json file":
                        {
                            Console.WriteLine();
                            try
                            {
                                using (StreamReader sr = new StreamReader("book.json"))
                                {
                                    Console.WriteLine(sr.ReadToEnd());
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case "7":
                    case "content of xml file":
                        {
                            Console.WriteLine();
                            try
                            {
                                using (StreamReader sr = new StreamReader("book.xml"))
                                {
                                    Console.WriteLine(sr.ReadToEnd());
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case "8":
                    case "exit":
                        return;

                    default:
                        Console.WriteLine("\nSorry. Data entered incorrectly. Try again!\n");
                        break;
                }
            }
        }
    }
}


