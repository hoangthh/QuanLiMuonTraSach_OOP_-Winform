using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace OOP_QuanLiMuonTraSach
{
    public class BookList : ISerializable
    {
        //Private fields
        private List<Book> books;

        //Public fields
        public List<Book> Books
        {
            get { return books; }
            set { books = value; }
        }

        //Constructor
        public BookList()
        {
  
        }

        //Method
        //Method Serialize
        public BookList(SerializationInfo info, StreamingContext context)
        {
            Books = (List<Book>)info.GetValue("Books", typeof(List<Book>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Books", Books);
        }

        public void GetInstance()
        {
            Books = new List<Book>();
            Books = ThuVienController.Deserialize<BookList>(FilePath.Book).Books;
        }

        public void DecreaseSoLuong(Book book)
        {
            book.SoLuong--;
        }

        public void IncreaseSoLuong(Book book)
        {
            book.SoLuong++;
        }

        public Book FindBook(int id)
        {
            Book targetBook = null;
            foreach (Book book in ThuVien.GetInstance().Employee.BookList.Books)
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
