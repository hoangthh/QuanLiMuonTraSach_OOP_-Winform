using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Book : ISerializable
    {
        public string IdSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string TheLoai { get; set; }
        public string SoLuong { get; set; }
        public string NhaXuatBan { get; set; }
        public string NamXuatBan { get; set; }

        public Book()
        {
        }
        public Book(SerializationInfo info, StreamingContext context)
        {
            IdSach = info.GetString("IdSach");
            TenSach = info.GetString("TenSach");
            TacGia = info.GetString("TacGia");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IdSach", IdSach);
            info.AddValue("TenSach", TenSach);
            info.AddValue("TacGia", TacGia);
        }

        public override string ToString()
        {
            return "Id Sách" + IdSach + "||" + "Ten Sach" + TenSach + "||" + "Tac Gia" + TacGia;
        }
    }
}
