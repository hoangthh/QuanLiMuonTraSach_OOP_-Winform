using OOP_QuanLiMuonTraSach;
using OOP_QuanLiMuonTraSach.Person;
using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class Employee : IPerson
    {
        //Private fields
        private BookList bookList;
        private StudentList studentList;
        private ListQuanLiMuonTraSach listQuanLiMuonTraSach;

        //Public fields
        public BookList BookList
        {
            get { return bookList; }
            set { bookList = value; }
        }

        public StudentList StudentList
        {
            get { return studentList; }
            set { studentList = value; }
        }

        public ListQuanLiMuonTraSach ListQuanLiMuonTraSach
        {
            get { return listQuanLiMuonTraSach; }
            set { listQuanLiMuonTraSach = value; }
        }

        //Constructor
        public Employee()
        {
           
        }

        //Method
        public void GetInstance()
        {
            BookList = new BookList();
            BookList.GetInstance();

            StudentList = new StudentList();
            StudentList.GetInstance();

            ListQuanLiMuonTraSach = new ListQuanLiMuonTraSach();
            ListQuanLiMuonTraSach.GetInstance();
        }

        public void AddBook(Book book)
        {
            this.BookList.Books.Add(book);
        }

        public void AddDocGia(Student student)
        {
            this.StudentList.Students.Add(student);
        }

        public void AddMuonTraSach(QuanLiMuonTraSach qlmts)
        {
            this.ListQuanLiMuonTraSach.QuanLiMuonTraSaches.Add(qlmts);
        }
    }
}
