using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class StudentList : IList<Student>,ISerializable
    {
        //Private fields
        private List<Student> students;

        //Public fields
        public List<Student> Students
        {
            get { return students; }
            set { students = value; }
        }
       
        //Constructor
        public StudentList()
        {
           
        }

        public void GetInstance()
        {
            Students = new List<Student>();
            Students = ThuVienController.Deserialize<StudentList>(FilePath.User)?.Students ?? new List<Student>();
        }

        //Method
        //Method Serialize
        public StudentList(SerializationInfo info, StreamingContext context)
        {
            Students = (List<Student>)info.GetValue("Students", typeof(List<Student>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Students", Students);
        }

        public void Add(Student student)
        {
            this.Students.Add(student);
        }

        public Student Find(int id)
        {
            foreach (Student student in ThuVien.GetInstance().Employee.StudentList.Students)
            {
                if (student.IdStudent == id) return student;
            }
            return null;
        }

        public void Remove(Student student)
        {
            this.Students.Remove(student);
        }
    }
}
