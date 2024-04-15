using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OOP_QuanLiMuonTraSach
{
    internal class ThuVien
    {
        //Singleton design pattern - static ThuVien
        private static ThuVien instance;

        public static ThuVien GetInstance()
        {
            if (instance == null)
            {
                instance = new ThuVien();
            }
            return instance;
        }

        //Private fields
        private StudentList students;
        private Employee employee;

        //Public fields
        public StudentList Students
        {
            get { return students; }
            set { students = value; }
        }

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        //Constructor
        public ThuVien()
        {
            Employee = new Employee();
            Employee.GetInstance();
        }
    }
}
