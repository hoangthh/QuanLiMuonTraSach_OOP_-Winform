using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    public class Book : ISerializable
    {
        //Private fields
        private int idSach;
        private string tenSach;
        private string tacGia;
        private string theLoai;
        private int soLuong;
        private string nhaXuatBan;
        private DateTime namXuatBan;
            
        //Public fields
        public int IdSach
        {
            get { return idSach; } 
            set { idSach = value; }
        }
        public string TenSach 
        {
            get { return tenSach; } 
            set {  tenSach = value; }
        }
        public string TacGia 
        {
            get { return tacGia; } 
            set {  tacGia = value; }
        }
        public string TheLoai 
        { 
            get { return theLoai; } 
            set { theLoai = value; }
        }
        public int SoLuong 
        { 
            get { return soLuong; }
            set { soLuong = value; }
        }
        public string NhaXuatBan 
        { 
            get { return nhaXuatBan; }
            set {  nhaXuatBan = value; }
        }
        public DateTime NamXuatBan 
        { 
            get { return namXuatBan; }
            set { namXuatBan = value; }
        }
        
        //Constructor
        public Book()
        {
        }

        //Method
        //Method Serialize
        public Book(SerializationInfo info, StreamingContext context)
        {
            IdSach = info.GetInt32("IdSach");
            TenSach = info.GetString("TenSach");
            TacGia = info.GetString("TacGia");
            SoLuong = info.GetInt32("SoLuong");
            NhaXuatBan = info.GetString("NhaXuatBan");
            NamXuatBan = info.GetDateTime("NamXuatBan");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IdSach", IdSach);
            info.AddValue("TenSach", TenSach);
            info.AddValue("TacGia", TacGia);
            info.AddValue("SoLuong", SoLuong);
            info.AddValue("NhaXuatBan", NhaXuatBan);
            info.AddValue("NamXuatBan", NamXuatBan);
        }
    }
}
