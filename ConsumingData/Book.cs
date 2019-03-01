using System;
using System.Runtime.Serialization;

namespace ConsumingData
{
    [Serializable]
    public class Book
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int TheYearOfPublishing { get; set; }
        public int Pages { get; set; }
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
