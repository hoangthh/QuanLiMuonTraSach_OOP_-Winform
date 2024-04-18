using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class Student : Person, ISerializable
    {
        //Private fields
        private int idStudent;
        private string lichSuMuon;
        
        //Public fields
        public int IdStudent
        {
            get { return idStudent; }
            set { idStudent = value; }
        }

        public string HoTen
        {
            get { return base.HoTen; }
            set { base.HoTen = value; }
        }

        public string GioiTinh
        {
            get { return base.GioiTinh; }
            set { base.GioiTinh = value; }
        }

        public DateTime NgaySinh
        {
            get { return base.NgaySinh; }
            set { base.NgaySinh = value; }
        }

        public string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        public string SoDienThoai
        {
            get { return base.SoDienThoai; }
            set { base.SoDienThoai = value; }
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
