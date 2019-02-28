using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsumingData
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("Harry Potter and the Philosopher's Stone", "Махаон", 2016, 432, 
                "The eleven-year-old orphan boy Harry Potter lives in the family of his aunt and does not even suspect that he is a real wizard. But one day an owl arrives with a letter for him, and Harry Potter's life changes forever ...");
            Book book2 = new Book("A Brief History of Time From the Big Bang to Black Holes", "АСТ", 2018, 232);
            Book book3 = new Book("C# 4.0: The Complete Reference", "Вильямс", 2019, 1058);

            //using the list collection to store books
            List<Book> books = new List<Book>(); // class Library with (Book[] books + IEnumerable)
            books.AddRange(new Book[] { book1, book2, book3 });

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
                    //Read and Write the JSON format is done using serialization
                    case "1":
                    case "write object in json":
                        {
                            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Book>));
                            using (FileStream fs = new FileStream("books.json", FileMode.OpenOrCreate))
                            {
                                jsonFormatter.WriteObject(fs, books);
                            }

                            using (StreamReader sr = new StreamReader("books.json"))
                            {
                                Console.WriteLine("\n" + sr.ReadToEnd() + "\n");
                            }
                        }
                        break;

                    case "2":
                    case "read object from json":
                        {
                            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Book>));
                            using (FileStream fs = new FileStream("books.json", FileMode.OpenOrCreate))
                            {
                                books = (List<Book>)jsonFormatter.ReadObject(fs);

                                PrintBooks(books);
                            }
                        }
                        break;

                    //It was also possible to implement using serialization, XmlWriter and XmlReader classes.
                    //Creating an xml document file using the System.Xml.Linq namespace )
                    case "3":
                    case "write object in xml":
                        {
                            XDocument xdoc = new XDocument();
                            XElement xbooks = new XElement("books");
                            foreach (var book in books)
                            {
                                XElement xbook = new XElement("book");
                                XAttribute nameAttr = new XAttribute("name", book.Name);
                                XAttribute publisherAttr = new XAttribute("publisher", book.Publisher);

                                XElement theYearOfPublishingElem = new XElement("theYearOfPublishing", book.TheYearOfPublishing);
                                XElement pagesElem = new XElement("pages", book.Pages);
                                XElement descriptionElem = null;
                                if (book.Description != null)
                                    descriptionElem = new XElement("description", book.Description);

                                xbook.Add(nameAttr);
                                xbook.Add(publisherAttr);
                                xbook.Add(theYearOfPublishingElem);
                                xbook.Add(pagesElem);
                                xbook.Add(descriptionElem);

                                xbooks.Add(xbook);
                            }
                            xdoc.Add(xbooks);

                            Console.WriteLine("\n" + xdoc + "\n");

                            xdoc.Save("books.xml");
                        }
                        break;

                    //reading an xml document from a file using the System.Xml namespace and the XmlNode class
                    case "4":
                    case "read object from xml":
                        {
                            books = new List<Book>();
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.Load("books.xml");
                            XmlElement xRoot = xDoc.DocumentElement;
                            foreach (XmlElement xnode in xRoot)
                            {
                                Book book = new Book();

                                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                                if (attr != null)
                                    book.Name = attr.Value;
                                attr = xnode.Attributes.GetNamedItem("publisher");
                                if (attr != null)
                                    book.Publisher = attr.Value;

                                foreach (XmlNode childnode in xnode.ChildNodes)
                                {
                                    if (childnode.Name == "theYearOfPublishing")
                                        book.TheYearOfPublishing = Convert.ToInt32(childnode.InnerText);

                                    if (childnode.Name == "pages")
                                        book.Pages = Convert.ToInt32(childnode.InnerText);

                                    if (childnode.Name == "description")
                                        book.Description = childnode.InnerText;
                                }
                                books.Add(book);
                            }

                            PrintBooks(books);
                        }
                        break;

                    case "5":
                    case "display information about books":
                        {
                            PrintBooks(books);
                        }
                        break;
                    case "6":
                    case "content of json file":
                        {
                            Console.WriteLine();
                            try
                            {
                                using (StreamReader sr = new StreamReader("books.json"))
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
                                using (StreamReader sr = new StreamReader("books.xml"))
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
        public static void PrintBooks(List<Book> books)
        {
            Console.WriteLine();
            foreach (Book book in books)
            {
                book.PrintInfo();
                Console.WriteLine();
            }
        }
    }
}
