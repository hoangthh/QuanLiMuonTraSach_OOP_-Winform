using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    /*
    Khi mở rộng dự án chẳng hạn như thêm 1 class Admin
    sẽ có các quyền cơ bản của Employee và các quyền nâng cao khác
    thì sẽ kế thừa lại IController này
    */
    internal interface IController
    {
        void AddBook(Book book);

        Book FindBook(int id);

        void RemoveBook(Book Book);

        void AddDocGia(Student student);

        Student FindDocGia(int id);

        void RemoveDocGia(Student student);

        void AddMuonTraSach(QuanLiMuonTraSach qlmts);

        QuanLiMuonTraSach FindMuonTraSach(int id);

        void RemoveMuonTraSach(QuanLiMuonTraSach qlmts);
    }
}
