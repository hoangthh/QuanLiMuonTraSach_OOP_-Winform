using OOP_QuanLiMuonTraSach;
using OOP_QuanLiMuonTraSach.Person;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class ListQuanLiMuonTraSach : ISerializable
    {
        //Private fields
        private List<QuanLiMuonTraSach> listQuanLiMuonTraSach;

        //Public fields
        public List<QuanLiMuonTraSach> QuanLiMuonTraSaches
        {
            get { return listQuanLiMuonTraSach; }
            set { listQuanLiMuonTraSach = value; }
        }

        //Constructor
        public ListQuanLiMuonTraSach()
        {
            QuanLiMuonTraSaches = new List<QuanLiMuonTraSach>();
        }

        public void GetInstance()
        {
            QuanLiMuonTraSaches = new List<QuanLiMuonTraSach>();
            QuanLiMuonTraSaches = ThuVienController.Deserialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach)?.QuanLiMuonTraSaches ?? new List<QuanLiMuonTraSach>();
        }

        //Method
        //Method Serialize
        public ListQuanLiMuonTraSach(SerializationInfo info, StreamingContext context)
        {
            QuanLiMuonTraSaches = (List<QuanLiMuonTraSach>)info.GetValue("QuanLiMuonTraSaches", typeof(List<QuanLiMuonTraSach>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("QuanLiMuonTraSaches", QuanLiMuonTraSaches);
        }



        public QuanLiMuonTraSach Find(int IDMuonTra)
        {
            foreach (QuanLiMuonTraSach qlmts in QuanLiMuonTraSaches)
            {
                if (qlmts.IdMuonTra == IDMuonTra) return qlmts;
            }
            return null;
        }

        public ListQuanLiMuonTraSach UserListQuanLiMuonTraSach(int idnguoidung)
        {
            ListQuanLiMuonTraSach userqlmts = new ListQuanLiMuonTraSach();
            foreach (QuanLiMuonTraSach item in ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.QuanLiMuonTraSaches)
            {
                if (item.IdNguoiDung == idnguoidung)
                {
                    userqlmts.QuanLiMuonTraSaches.Add(item);
                }
            }
            if (userqlmts.QuanLiMuonTraSaches.Count == 0) return null;
            return userqlmts;
        }

        public List<QuanLiMuonTraSach> FindListQuanLiMuonTraSachByIdNguoiDung(int ID)
        {
            List<QuanLiMuonTraSach> result = new List<QuanLiMuonTraSach>();
            foreach (QuanLiMuonTraSach qlmts in QuanLiMuonTraSaches)
            {
                if (qlmts.IdNguoiDung == ID) 
                    result.Add(qlmts);
            }
            return result;
        }

        public List<QuanLiMuonTraSach> FindListQuanLiMuonTraSachByIdSach(int ID)
        {
            List<QuanLiMuonTraSach> result = new List<QuanLiMuonTraSach>();
            foreach (QuanLiMuonTraSach qlmts in QuanLiMuonTraSaches)
            {
                if (qlmts.IdSach == ID)
                    result.Add(qlmts);
            }
            return result;
        }
    }
}
