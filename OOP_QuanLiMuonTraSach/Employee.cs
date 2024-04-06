using ConsoleApp1;
using ConsoleApp1.Person;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosoleApp1.Person
{
    public class Employee : IPerson
    {
        private BookList BookList { get; set; }
        public StudentList StudentList { get; set; }
        public Employee() 
        {
            BookList = new BookList();
        }
        public List<Book> GetBookList()
        {
            return BookList.GetListBook();
        }
        public BookList Get()
        {
            return BookList;
        }
        public Student CreateStudent()
        {
            return new Student();
        }
        public Student UpdateStudent(IPerson student)
        {
            return (Student)student;
        }
        public void DeleteStudent(string ID, StudentList studentList)
        {
            foreach(IPerson student in studentList.GetStudents())
            {
                if(((Student)student).ID == ID && ((Student)student).muonSachList.GetBookList().Count == 0)
                {
                    studentList.GetStudents().Remove((Student)student);
                    return;
                }
            }

        }
        public Book CreateBook()
        {
            return new Book();
        }
        public Book UpdateBook(Book book)
        {
            return book;
        }
        public void DeleteBook(string ID, BookList bookList)
        {
            foreach (Book book in bookList.GetListBook())
            {
                if (book.IdSach == ID)
                {
                    bookList.GetListBook().Remove(book);
                    return;
                }
                Console.WriteLine("Khong co sach nay");
            }

        }
    }
}
