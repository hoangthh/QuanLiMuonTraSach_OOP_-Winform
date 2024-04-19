using OOP_QuanLiMuonTraSach;
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
    public partial class frm_ThongTinSachChiTiet : Form
    {
        //Variables
        #region Variables
        private int idSach; //Biến chứa idSach
        private int idNguoiDung; //Biến chứa idNguoiDung
        private Student student; //Biến student chứa idNguoiDung
        private Book sach; //Biến book chứa idSach
        #endregion

        //Nếu kkhông có tham số idNguoiDung thì form được sử dụng cho NhanVien
        public frm_ThongTinSachChiTiet(int idSach) 
        {
            InitializeComponent();
            button_Booking.Visible = false;
            this.idSach = idSach;
            sach = ThuVien.GetInstance().Employee.FindBook(idSach);
            LoadData();
        }

        //Nếu có tham số idNguoiDung thì form được sử dụng cho NguoiDung
        public frm_ThongTinSachChiTiet(int idSach, int idNguoiDung)
        {
            InitializeComponent();
            this.idSach = idSach;
            this.idNguoiDung = idNguoiDung;
            sach = ThuVien.GetInstance().Employee.FindBook(idSach);
            student = ThuVien.GetInstance().Employee.FindDocGia(idNguoiDung);
            LoadData();
        }

        //Functions
        #region Functions
        private void LoadData() //Hàm hiển thị dữ liệu
        {
            label_IDInfo.Text = sach.IdSach.ToString();
            label_TenSach.Text = sach.TenSach.ToUpper();
            label_TacGiaInfo.Text = sach.TacGia;
            label_TheLoaiInfo.Text = sach.TheLoai;
            label_NhaXuatBanInfo.Text = sach.NhaXuatBan;
            label_NamXuatBanInfo.Text = sach.NamXuatBan.ToString();      
        }
        #endregion

        #region Events
        private void button_Booking_Click(object sender, EventArgs e)
        {
            List<QuanLiMuonTraSach> userList = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.UserListQuanLiMuonTraSach(idNguoiDung).QuanLiMuonTraSaches;
            int countSoTienPhat = 0;
            Book bookFind = ThuVien.GetInstance().Employee.FindBook(idSach);
            bool isYeuCauMuonSach = false;
            foreach (QuanLiMuonTraSach item in userList)
            {
                if (item.SoTienPhat > 0)
                {
                    countSoTienPhat++;
                }
                if (item.TinhTrang == "Yêu cầu" && item.TenSach == bookFind.TenSach)
                {
                    isYeuCauMuonSach = true;
                }
                else isYeuCauMuonSach = false;
            }
            if (countSoTienPhat >= 3)
            {
                MessageBox.Show("Bạn đã trả sách quá hạn nhiều lần \nVui lòng thanh toán để tiếp tục mượn sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            if (bookFind == null || bookFind.SoLuong <= 0)
            {
                MessageBox.Show("Số lượng sách đã hết, vui lòng mượn sách khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (isYeuCauMuonSach == true)
            {
                MessageBox.Show($"Bạn đã tạo yêu cầu mượn sách cho sách {bookFind.TenSach} \nVui lòng liên hệ nhân viên để xác nhận mượn sách","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (isYeuCauMuonSach == false)
            {
                QuanLiMuonTraSach qlmts = new QuanLiMuonTraSach(bookFind, student);
                ThuVien.GetInstance().Employee.AddMuonTraSach(qlmts);
                ThuVienController.Serialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach, ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach);
                MessageBox.Show("Yêu cầu mượn sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
