using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MuonSachList
    {
        List<Book> books;
        public MuonSachList()
        {
            books = new List<Book>();
        }
        public void Borrow(Book book)
        {
            books.Add(book);
        }
        public void Remove(Book book)
        {
            books.Remove(book);
        }
        public void Print()
        {
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }
        }
        public List<Book> GetBookList()
        {
            return books;
        }
    }
}
