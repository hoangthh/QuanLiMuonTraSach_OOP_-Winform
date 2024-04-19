using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ThongTinDocGiaChiTiet : Form
    {
        //Variables
        #region Variables
        private int idNguoiDung; //Biến chứa idNguoiDung
        private Student nguoidung; //Biến student chứa idNguoiDung
        #endregion

        public frm_ThongTinDocGiaChiTiet(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            nguoidung = ThuVien.GetInstance().Employee.FindDocGia(idNguoiDung);
            LoadData(idNguoiDung);
        }

        //Functions
        #region Functions
        private void LoadData(int idNguoiDung)
        {
            label_UserName.Text = nguoidung.HoTen;
            label_IDInfo.Text = nguoidung.IdStudent.ToString();
            label_GioiTinhInfo.Text = nguoidung.GioiTinh;
            label_NgaySinhInfo.Text = nguoidung.NgaySinh.ToString();
            label_EmailInfo.Text = nguoidung.Email;
            label_SoDienThoaiInfo.Text = nguoidung.SoDienThoai;
        }
        #endregion
    }
}
