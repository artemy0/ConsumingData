using System;
using System.Runtime.Serialization;

namespace ConsumingData
{
    [Serializable]
    [DataContract] //I use a serialization library that helps me convert objects to JSON
    public class Book
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Publisher { get; set; }
        [DataMember]
        public int TheYearOfPublishing { get; set; }
        [DataMember]
        public int Pages { get; set; }
        [DataMember]
        public string Description { get; set; }

        public Book(string name, string publisher, int theYearOfPublishing, int pages)
        {
            Name = name;
            Publisher = publisher;
            TheYearOfPublishing = theYearOfPublishing;
            Pages = pages;
        }
        public Book(string name, string publisher, int theYearOfPublishing, int pages, string description) : this(name, publisher, theYearOfPublishing, pages)
        {
            Description = description;
        }
        public Book()
        {

        }

        public string GetInfo()
        {
            if (Description == null)
                return $"Title of the book: {Name}\nPublisher: {Publisher}\nThe year of publishing: {TheYearOfPublishing}\nNumber of pages: {Pages}";
            else
                return $"Title of the book: {Name}\nPublisher: {Publisher}\nThe year of publishing: {TheYearOfPublishing}\nNumber of pages: {Pages}\nDescription: {Description}";
        }

        public void PrintInfo() => Console.WriteLine(GetInfo());
    }
}
