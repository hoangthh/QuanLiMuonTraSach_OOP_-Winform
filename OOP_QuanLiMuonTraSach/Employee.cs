using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class Employee : Person, IController
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
            this.BookList.Add(book);
        }
        public Book FindBook(int id)
        {
            return this.BookList.Find(id);
        }

        public void RemoveBook(Book book)
        {
            this.BookList.Remove(book);
        }

        public void AddDocGia(Student student)
        {
            this.StudentList.Add(student);
        }
        public Student FindDocGia(int id)
        {
            return this.StudentList.Find(id);
        }

        public void RemoveDocGia(Student student)
        {
            this.StudentList.Remove(student);
        }

        public void AddMuonTraSach(QuanLiMuonTraSach qlmts)
        {
            this.ListQuanLiMuonTraSach.Add(qlmts);
        }

        public QuanLiMuonTraSach FindMuonTraSach(int id)
        {
            return this.ListQuanLiMuonTraSach.Find(id);
        }

        public void RemoveMuonTraSach(QuanLiMuonTraSach qlmts)
        {
            this.ListQuanLiMuonTraSach.Remove(qlmts);
        }
    }
}
