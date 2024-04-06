using ConsoleApp1.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StudentList
    {
        List<Student> students;
        public StudentList()
        {
            students = new List<Student>();
        }
        public void Add(Student student)
        {
            students.Add(student);
        }
        public Student GetStudent(string ID)
        {
            foreach(Student student in students)
            {
                if(student.ID == ID) return student;
            }
            return null;
        }
        public List<Student> GetStudents() {  return students; }
    }
}
