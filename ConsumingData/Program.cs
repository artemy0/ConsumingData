using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

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
                    case "1":
                    case "write object in json":
                        WriteToJSON(book, "book.json");
                        break;

                    case "2":
                    case "read object from json":
                        ReadFromJSON(book, "book.json");
                        break;

                    case "3":
                    case "write object in xml":
                        WriteToXML(book, "book.xml");
                        break;

                    case "4":
                    case "read object from xml":
                        ReadFormXML(book, "book.xml");
                        break;

                    case "5":
                    case "display information about books":
                        book.PrintInfo();
                        break;

                    case "6":
                    case "content of json file":
                        DisplayFileContents("book.json");
                        break;

                    case "7":
                    case "content of xml file":
                        DisplayFileContents("book.xml");
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

        //methods for working with json and xml formats
        public static void DisplayFileContents(string PathToFile)
        {
            {
                Console.WriteLine();
                try
                {
                    using (StreamReader sr = new StreamReader(PathToFile))
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
        }

        public static void WriteToJSON(Book book, string PathToFile)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Book));

            using (FileStream fs = new FileStream(PathToFile, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, book);
                Console.WriteLine("Object serialized");
            }

            DisplayFileContents("book.json");
        }

        public static void ReadFromJSON(Book book, string PathToFile)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Book));

            using (FileStream fs = new FileStream(PathToFile, FileMode.Open))
            {
                book = (Book)jsonFormatter.ReadObject(fs);

                Console.WriteLine("Object deserialized");
                book.PrintInfo();
            }
        }

        public static void WriteToXML(Book book, string PathToFile)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Book));

            using (FileStream fs = new FileStream(PathToFile, FileMode.Create))
            {
                formatter.Serialize(fs, book);
                Console.WriteLine("Object serialized");
            }

            DisplayFileContents("book.xml");
        }

        public static void ReadFormXML(Book book, string PathToFile)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Book));

            using (FileStream fs = new FileStream(PathToFile, FileMode.Open))
            {
                book = (Book)formatter.Deserialize(fs);

                Console.WriteLine("Object deserialized");
                book.PrintInfo();
            }
        }
    }
}


