using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal class ListQuanLiMuonTraSach : IList<QuanLiMuonTraSach>, ISerializable
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
            if (ThuVienController.Deserialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach) == null) return;
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

        public void Add(QuanLiMuonTraSach item)
        {
            this.QuanLiMuonTraSaches.Add(item);
        }

        public QuanLiMuonTraSach Find(int IDMuonTra)
        {
            foreach (QuanLiMuonTraSach qlmts in QuanLiMuonTraSaches)
            {
                if (qlmts.IdMuonTra == IDMuonTra) return qlmts;
            }
            return null;
        }

        public void Remove(QuanLiMuonTraSach qlmts)
        {
            this.QuanLiMuonTraSaches.Remove(qlmts);
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
            if (userqlmts.QuanLiMuonTraSaches.Count == 0) return new ListQuanLiMuonTraSach();
            return userqlmts;
        }

        public List<QuanLiMuonTraSach> Find(Student student)
        {
            List<QuanLiMuonTraSach> result = new List<QuanLiMuonTraSach>();
            foreach (QuanLiMuonTraSach qlmts in QuanLiMuonTraSaches)
            {
                if (qlmts.IdNguoiDung == student.IdStudent)
                    result.Add(qlmts);
            }
            return result;
        }

        public List<QuanLiMuonTraSach> Find(Book book)
        {
            List<QuanLiMuonTraSach> result = new List<QuanLiMuonTraSach>();
            foreach (QuanLiMuonTraSach qlmts in QuanLiMuonTraSaches)
            {
                if (qlmts.IdSach == book.IdSach)
                    result.Add(qlmts);
            }
            return result;
        }
    }
}
