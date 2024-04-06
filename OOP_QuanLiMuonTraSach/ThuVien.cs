using ConsoleApp1.Person;
using CosoleApp1.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    internal class ThuVien
    {
        private StudentList Students { get; set; }
        private Employee employee { get; set; }
        public ThuVien()
        {
            employee = new Employee();
            Students = new StudentList();
        }
        public Employee GetEmployee()
        {
            return employee;
        }
        public StudentList GetStudentList()
        {
            return Students;
        }
        public void AddStudent(IPerson student)
        {
            Students.Add((Student)student);
        }

        public void MuonSach(IPerson student, Book Book)
        {
            for(int i = 0; i < employee.GetBookList().Count(); i++)
            {
                if(Book == employee.GetBookList()[i]) { 
                    ((Student)student).muonSachList.Borrow(Book);
                    employee.GetBookList().Remove(Book);
                }
            }
        }
        public void TraSach( IPerson student, Book Book)
        {
            for (int i = 0; i < ((Student)student).muonSachList.GetBookList().Count(); i++)
            {
                if (Book == ((Student)student).muonSachList.GetBookList()[i])
                {
                    ((Student)student).muonSachList.Remove(Book);
                    employee.GetBookList().Add(Book);
                }
            }
        }
    }
}
