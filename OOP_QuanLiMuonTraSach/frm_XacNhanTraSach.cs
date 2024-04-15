using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_XacNhanTraSach : Form
    {
        //Variables
        #region Variables
        private bool checkInputInsert = true; //Biến check input khi Insert
        private int selectedID; //Biến chứa id được chọn
        private QuanLiMuonTraSach result; //Biến chứa kết quả MuonTra
        #endregion

        public frm_XacNhanTraSach()
        {
            InitializeComponent();
        }

        //Functions
        #region Functions
        //Hàm check input khi mượn sách
        private void BindingDataByIdMuonTra()
        {
            if (string.IsNullOrEmpty(textBox_IDMuonTra.Text))
            {
                ResetControl();
                return;
            }
            selectedID = Convert.ToInt32(textBox_IDMuonTra.Text);
            if (!string.IsNullOrEmpty(textBox_IDMuonTra.Text)) // Kiểm tra xem textbox có giá trị không
            {
                if (int.TryParse(textBox_IDMuonTra.Text, out int IDMuonTra)) // Thử chuyển đổi giá trị sang số nguyên
                {
                    QuanLiMuonTraSach result = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.Find(IDMuonTra);
                    if (result != null)
                    {
                        textBox_IDNguoiDung.Text = result.IdNguoiDung.ToString();
                        textBox_IDSach.Text = result.IdSach.ToString();
                        textBox_HoTen.Text = result.HoTen;
                        textBox_TenSach.Text = result.TenSach;
                    }
                    else
                    {
                        textBox_IDNguoiDung.Text = null;
                        textBox_IDSach.Text = null;
                        textBox_HoTen.Text = null;
                        textBox_TenSach.Text = null;
                    }
                }
                else
                {
                    textBox_IDNguoiDung.Text = null;
                    textBox_IDSach.Text = null; // Nếu không thể chuyển đổi, đặt giá trị textbox kết quả thành null
                    textBox_HoTen.Text = null;
                    textBox_TenSach.Text = null;
                }
            }
            else
            {
                textBox_IDNguoiDung.Text = null;
                textBox_IDSach.Text = null; // Nếu textbox rỗng, đặt giá trị textbox kết quả thành null
                textBox_HoTen.Text = null;
                textBox_TenSach.Text = null;
            }

        }

        private void CheckInputForXacNhanTraSach(ref bool check)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_IDMuonTra.Text))
                missingFields.Add("ID Mượn trả");

            if (missingFields.Count == 0) check = true;
            else
            {
                check = false;
                string missingFieldsMessage = "Các trường sau không được để trống:\n";
                foreach (string field in missingFields)
                {
                    missingFieldsMessage += field + "\n";
                }
                MessageBox.Show(missingFieldsMessage, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TraSach() //Hàm xử lí trả sách
        {
            result = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.Find(selectedID);
            if (result.NgayTraThucTe != null)
            {
                if (MessageBox.Show($"Sinh viên {result.HoTen} đã trả sách \nBạn muốn cập nhật lại ngày trả sách của sinh viên {result.HoTen} ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    result.NgayTraThucTe = dateTimePicker_NgayTraSach.Value;
                    if ((dateTimePicker_NgayTraSach.Value - result.NgayTraDuKien).TotalDays > 0)
                    {
                        result.SoTienPhat = 50000;
                    }
                    else result.SoTienPhat = 0;
                    result.TinhTrang = "Đã trả";
                    ThuVienController.Serialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach, ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach);
                    MessageBox.Show("Xác nhận trả sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Xác nhận trả sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (result.NgayTraThucTe == null)
            {
                DialogResult resultNoti = MessageBox.Show($"Xác nhận trả sách cho sinh viên {result.HoTen} ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultNoti == DialogResult.Yes)
                {
                    result.NgayTraThucTe = dateTimePicker_NgayTraSach.Value;
                    if ((dateTimePicker_NgayTraSach.Value - result.NgayTraDuKien).TotalDays > 0)
                    {
                        result.SoTienPhat = 50000;
                    }
                    else result.SoTienPhat = 0;
                    result.TinhTrang = "Đã trả";
                    ThuVienController.Serialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach, ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach);
                    MessageBox.Show("Xác nhận trả sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Xác nhận trả sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void UpdateSoLuongSachSauKhiTra(int id) //Hàm cập nhật số lượng sách khi mượn trả
        {
            Book bookFind = ThuVien.GetInstance().Employee.BookList.FindBook(id);
            ThuVien.GetInstance().Employee.BookList.IncreaseSoLuong(bookFind);
            ThuVienController.Serialize<BookList>(FilePath.Book, ThuVien.GetInstance().Employee.BookList);
        }

        private void ResetControl() //Hàm đặt input về null
        {
            textBox_IDMuonTra.Text = null;
            textBox_IDNguoiDung.Text = null;
            textBox_IDSach.Text = null;
            textBox_HoTen.Text = null;
            textBox_TenSach.Text = null;
            dateTimePicker_NgayTraSach.Value = DateTime.Now;
        }
        #endregion

        //Events
        #region Events
        private void textBox_IDMuonTra_TextChanged(object sender, EventArgs e)
        {
            BindingDataByIdMuonTra();
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForXacNhanTraSach(ref checkInputInsert);
            if (checkInputInsert == true)
            {
                TraSach();
                UpdateSoLuongSachSauKhiTra(Convert.ToInt32(textBox_IDSach.Text));
            }
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }
        #endregion
    }
}
