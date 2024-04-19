using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ChinhSuaSach : Form
    {
        //Variables
        #region Variables
        private int idSach; //Biến chứa idSach
        private Book sach; //Biến Book chứa idSach
        #endregion

        public frm_ChinhSuaSach(int idSach)
        {
            InitializeComponent();
            this.idSach = idSach;
            sach = ThuVien.GetInstance().Employee.FindBook(idSach);
            dateTimePicker_NamXuatBanUpdateInput.Value = DateTime.Now;
            BindingDataSelected();
        }

        //Functions
        #region Functions
        private void BindingDataSelected() //Hàm biding dữ liệu có trong dataGridView
        {
            textBox_IDSachUpdateInput.Text = sach.IdSach.ToString();
            textBox_TenSachUpdateInput.Text = sach.TenSach;
            textBox_TacGiaUpdateInput.Text = sach.TacGia;
            textBox_SoLuongUpdateInput.Text = sach.SoLuong.ToString();
            textBox_TheLoaiUpdateInput.Text = sach.TheLoai;
            textBox_NhaXuatBanUpdateInput.Text = sach.NhaXuatBan;
            dateTimePicker_NamXuatBanUpdateInput.Value = (DateTime)sach.NamXuatBan;
        }

        private void UpdateSach() //Hàm update lại sách
        {
            List<string> updateFields = new List<string>();

            if (textBox_TenSachUpdateInput.Text != sach.TenSach)
                updateFields.Add("Tên sách");
            if (textBox_TacGiaUpdateInput.Text != sach.TacGia)
                updateFields.Add("Tác giả");
            if (textBox_TheLoaiUpdateInput.Text != sach.TheLoai)
                updateFields.Add("Thể loại");
            if (textBox_SoLuongUpdateInput.Text != sach.SoLuong.ToString())
                updateFields.Add("Số lượng");
            if (textBox_NhaXuatBanUpdateInput.Text != sach.NhaXuatBan)
                updateFields.Add("Nhà xuất bản");
            if (dateTimePicker_NamXuatBanUpdateInput.Value != (DateTime)sach.NamXuatBan)
                updateFields.Add("Năm xuất bản");

            string updateFieldMessage = "Bạn muốn thay đổi \n";
            foreach (string field in updateFields)
            {
                updateFieldMessage += field + "\n";
            }
            updateFieldMessage += $"của sách {sach.TenSach}";

            DialogResult result = MessageBox.Show(updateFieldMessage, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sach.TenSach = textBox_TenSachUpdateInput.Text;
                sach.TacGia = textBox_TacGiaUpdateInput.Text;
                sach.TheLoai = textBox_TheLoaiUpdateInput.Text;
                sach.SoLuong = Convert.ToInt32(textBox_SoLuongUpdateInput.Text);
                sach.NhaXuatBan = textBox_NhaXuatBanUpdateInput.Text;
                sach.NamXuatBan = dateTimePicker_NamXuatBanUpdateInput.Value;
                ThuVienController.Serialize<BookList>(FilePath.Book, ThuVien.GetInstance().Employee.BookList);

                List<QuanLiMuonTraSach> qlmts = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.Find(sach);
                foreach (QuanLiMuonTraSach item in qlmts)
                {
                    item.TenSach = textBox_TenSachUpdateInput.Text;
                }
                ThuVienController.Serialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach, ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach);
                MessageBox.Show("Chỉnh sửa thông tin sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Chỉnh sửa thông tin sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        //Events
        #region Events
        private void button_SaveUpdate_Click(object sender, EventArgs e)
        {
            UpdateSach();
        }
        private void button_ResetUpdate_Click(object sender, EventArgs e)
        {
            BindingDataSelected();
        }

        private void dateTimePicker_NamXuatBanUpdateInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NamXuatBanUpdateInput.Value > DateTime.Now)
            {
                MessageBox.Show("Năm xuất bản không được lớn hơn ngày hiện tại");
                dateTimePicker_NamXuatBanUpdateInput.Value = DateTime.Now;
            }
        }

        private void textBox_TacGiaUpdateInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập số vào TextBox
            }
        }

        private void textBox_TheLoaiUpdateInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập số vào TextBox
            }
        }

        private void textBox_SoLuongUpdateInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
          (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
          e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                // Nếu không phải là phím số, Backspace, Delete, Left hoặc Right, chặn sự kiện
                e.SuppressKeyPress = true;
            }
        }
        #endregion      
    }
}
