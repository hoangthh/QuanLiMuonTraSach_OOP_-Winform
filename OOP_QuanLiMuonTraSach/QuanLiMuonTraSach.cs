using OOP_QuanLiMuonTraSach;
using OOP_QuanLiMuonTraSach.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class QuanLiMuonTraSach : ISerializable
    {
        //Private Fields
        private int idMuonTra;
        private int idNguoiDung;
        private string hoTen;
        private int idSach;
        private string tenSach;
        private DateTime ngayMuon;
        private DateTime ngayTraDuKien;
        private DateTime? ngayTraThucTe;
        private int soTienPhat;
        private string tinhTrang;

        //Public fields
        public int IdMuonTra 
        { 
            get { return idMuonTra; } 
            set { idMuonTra = value;} 
        }
        
        public int IdNguoiDung
        {
            get { return idNguoiDung; }
            set { idNguoiDung = value; }
        }

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        public int IdSach
        {
            get { return idSach; }
            set { idSach = value; }
        }

        public string TenSach
        {
            get { return tenSach; }
            set { tenSach = value; }
        }

        public DateTime NgayMuon
        {
            get { return ngayMuon; }
            set { ngayMuon = value;}
        }

        public DateTime NgayTraDuKien
        {
            get { return ngayTraDuKien; }
            set { ngayTraDuKien = value; }
        }

        public DateTime? NgayTraThucTe
        {
            get { return ngayTraThucTe; }
            set { ngayTraThucTe = value; }
        }

        public int SoTienPhat
        {
            get { return soTienPhat; }
            set { soTienPhat = value; }
        }

        public string TinhTrang
        {
            get { return tinhTrang; }
            set { tinhTrang = value; }
        }

        //Constructor 
        public QuanLiMuonTraSach() { }
        public QuanLiMuonTraSach(Book book, Student student)
        {
            int id;
            if (ThuVienController.Deserialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach).QuanLiMuonTraSaches == null)
                id = 0;
            else
            {
                id = ThuVienController.Deserialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach).QuanLiMuonTraSaches.Count;
            }
            this.IdMuonTra = ++id;
            this.idNguoiDung = student.IdStudent;
            this.HoTen = student.HoTen;
            this.IdSach = book.IdSach;
            this.TenSach = book.TenSach;
            this.ngayMuon = DateTime.Now.Date;
            this.NgayTraDuKien = DateTime.Now.Date.AddDays(14);
            this.NgayTraThucTe = null;
            this.SoTienPhat = 0;
            this.TinhTrang = "Đang mượn";
        }

        //Method
        //Method Serialize
        public QuanLiMuonTraSach(SerializationInfo info, StreamingContext context)
        {
            IdMuonTra = info.GetInt32("IdMuonTra");
            IdNguoiDung = info.GetInt32("IdNguoiDung");
            IdSach = info.GetInt32("IdSach");
            NgayMuon = info.GetDateTime("NgayMuon");
            NgayTraDuKien = info.GetDateTime("NgayTraDuKien");
            NgayTraThucTe = info.GetDateTime("NgayTraThucTe");
            SoTienPhat = info.GetInt32("SoTienPhat");
            TinhTrang = info.GetString("TinhTrang");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IdMuonTra", IdMuonTra);
            info.AddValue("IdNguoiDung", IdNguoiDung);
            info.AddValue("IdSach", IdSach);
            info.AddValue("NgayMuon", NgayMuon);
            info.AddValue("NgayTraDuKien", NgayTraDuKien);
            info.AddValue("NgayTraThucTe", NgayTraThucTe);
            info.AddValue("SoTienPhat", SoTienPhat);
            info.AddValue("TinhTrang", TinhTrang);
        }
    }
}
