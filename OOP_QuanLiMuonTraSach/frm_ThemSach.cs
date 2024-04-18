using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ThemSach : Form
    {
        //Variables
        #region Variables
        private bool checkInputInsert = true; //Biến check đầu vào khi Insert
        #endregion

        public frm_ThemSach()
        {
            InitializeComponent();
            dateTimePicker_NamXuatBanInsertInput.Value = DateTime.Now;
        }

        //Functions
        #region Functions
        //Hàm check input khi Insert
        private void CheckInputForInsertSach(ref bool check)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_TenSachInsertInput.Text))
                missingFields.Add("Tên sách");
            if (string.IsNullOrWhiteSpace(textBox_TacGiaInsertInput.Text))
                missingFields.Add("Tác giả");
            if (string.IsNullOrWhiteSpace(textBox_TheLoaiInsertInput.Text))
                missingFields.Add("Thể loại");
            if (string.IsNullOrWhiteSpace(textBox_SoLuongInsertInput.Text))
                missingFields.Add("Số lượng");
            if (string.IsNullOrWhiteSpace(textBox_NhaXuatBanInsertInput.Text))
                missingFields.Add("Nhà xuất bản");
            if (dateTimePicker_NamXuatBanInsertInput.Value == null)
                missingFields.Add("Năm xuất bản");

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

        private void AddSach() //Hàm insert sách
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn thêm sách {textBox_TenSachInsertInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Book sach = new Book();
                sach.IdSach = ThuVien.GetInstance().Employee.BookList.Books.Count() + 1;
                sach.TenSach = textBox_TenSachInsertInput.Text;
                sach.TacGia = textBox_TacGiaInsertInput.Text;
                sach.TheLoai = textBox_TheLoaiInsertInput.Text;
                sach.SoLuong = Convert.ToInt32(textBox_SoLuongInsertInput.Text);
                sach.NhaXuatBan = textBox_NhaXuatBanInsertInput.Text;
                sach.NamXuatBan = dateTimePicker_NamXuatBanInsertInput.Value.Date;
                ThuVien.GetInstance().Employee.AddBook(sach);
                ThuVienController.Serialize<BookList>(FilePath.Book, ThuVien.GetInstance().Employee.BookList);
                MessageBox.Show("Thêm sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { MessageBox.Show("Thêm sách chưa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void ResetControl() //Hàm đặt các input về null
        {
            textBox_TenSachInsertInput.Text = null;
            textBox_TacGiaInsertInput.Text = null;
            textBox_NhaXuatBanInsertInput.Text = null;
            textBox_SoLuongInsertInput.Text = null;
            textBox_TheLoaiInsertInput.Text = null;
            dateTimePicker_NamXuatBanInsertInput.Value = DateTime.Now;
        }
        #endregion

        //Events
        #region Events
        private void textBox_TacGiaInsertInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập số vào TextBox
            }
        }

        private void textBox_TheLoaiInsertInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập số vào TextBox
            }
        }

        private void textBox_SoLuongInsertInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                         (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                         e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                // Nếu không phải là phím số, Backspace, Delete, Left hoặc Right, chặn sự kiện
                e.SuppressKeyPress = true;
            }
        }

        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForInsertSach(ref checkInputInsert);
            if (checkInputInsert == true) AddSach();
            this.Close();
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void dateTimePicker_NamXuatBanInsertInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NamXuatBanInsertInput.Value > DateTime.Now)
            {
                MessageBox.Show("Năm xuất bản không được lớn hơn ngày hiện tại");
                dateTimePicker_NamXuatBanInsertInput.Value = DateTime.Now;
            }
        }
        #endregion
    }
}
