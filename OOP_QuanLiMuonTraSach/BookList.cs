using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ConsoleApp1
{
    public class BookList : ISerializable
    {
        private List<Book> books;
        public List<Book> Books 
        { 
            get {  return books; } 
            set { books = value; }
        }
        public BookList()
        {
            Books = new List<Book>();
        }
        public BookList(SerializationInfo info, StreamingContext context)
        {
            Books = (List<Book>)info.GetValue("Books",typeof(List<Book>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Books", Books);
        }
        public void Add(Book book)
        {
            Books.Add(book);
        }
        public void Print()
        {
            foreach(Book books in Books)
            {
                Console.WriteLine(books);
            }

        }
        public void Print(string ID)
        {
            foreach (Book books in Books)
            {
                if(books.IdSach == ID)
                Console.WriteLine(books);
            }
        }
        public void Remove(Book book)
        {
            Books.Remove(book);
        }
        public List<Book> GetListBook()
        {
            return Books;
        }
        public void Serialize<T>(string path, T bookLists)
        {
            try
            {
                string json = JsonSerializer.Serialize(bookLists, new JsonSerializerOptions { WriteIndented = true,Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All) });
                File.WriteAllText(path, json, Encoding.UTF8);
            }
            catch (Exception e) { throw new Exception(); }
        }
        public T Deserialize<T>(string path)
        {
            try
            {
                string jsonFromFile = File.ReadAllText(path);
                T deserializedObject = JsonSerializer.Deserialize<T>(jsonFromFile);
                return deserializedObject;
            }
            catch (Exception e) { throw new Exception(); }
        }
        public Book FindBook(BookList bookList, string id)
        {
            Book targetBook = null;
            foreach (Book book in bookList.Books)
            {
                if (book.IdSach == id)
                {
                    targetBook = book;
                    return targetBook;
                }
            }
            return null;
        }
    }
}
