using OOP_QuanLiMuonTraSach.Person;
using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class StudentList : ISerializable
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

        public Student FindStudent(int ID)
        {
            foreach(Student student in Students)
            {
                if(student.IdStudent == ID) return student;
            }
            return null;
        }
    }
}
