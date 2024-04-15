using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach.Person
{
    internal class Student : IPerson, ISerializable
    {
        //Private fields
        private int idStudent;
        private string hoTen;
        private string gioiTinh;
        private DateTime ngaySinh;
        private string email;
        private string soDienThoai;
        private string lichSuMuon;
        
        //Public fields
        public int IdStudent
        {
            get { return idStudent; }
            set { idStudent = value; }
        }

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string SoDienThoai
        {
            get { return soDienThoai; }
            set { soDienThoai = value; }
        }

        public string LichSuMuon
        {
            get { return lichSuMuon; }
            set { lichSuMuon = value; }
        }
        
        //Constructor
        public Student()
        {

        }
        public Student(int idStudent, string hoTen, string gioiTinh, string email, string soDienThoai, string lichSuMuon)
        {
            IdStudent = IdStudent;
            HoTen = HoTen;
            GioiTinh = GioiTinh;
            Email = Email;
            SoDienThoai = SoDienThoai;
            LichSuMuon = LichSuMuon;
        }

        public Student(int idNguoiDung)
        {
            
        }

        //Method
        //Method Serialize
        public Student(SerializationInfo info, StreamingContext context)
        {
            IdStudent = info.GetInt32("IdStudent");
            HoTen = info.GetString("HoTen");
            GioiTinh = info.GetString("GioiTinh");
            Email = info.GetString("Email");
            SoDienThoai = info.GetString("SoDienThoai");
            LichSuMuon = info.GetString("LichSuMuon");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IdStudent", IdStudent);
            info.AddValue("HoTen", HoTen);
            info.AddValue("GioiTinh", GioiTinh);
            info.AddValue("Email", Email);
            info.AddValue("SoDienThoai", SoDienThoai);
            info.AddValue("LichSuMuon", lichSuMuon);
        }
    }
}
