using OOP_QuanLiMuonTraSach;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ThemDocGia : Form
    {
        //Variables
        #region Variables
        string[] gioiTinh;
        private bool checkInputInsert = true; //Biến check đầu vào khi Insert
        #endregion

        public frm_ThemDocGia()
        {
            InitializeComponent();
            gioiTinh = new string[] { "Nam", "Nữ" };
            AddComboBoxGioiTinh();
            dateTimePicker_NgaySinhInput.Value = DateTime.Now;
        }

        //Functions
        #region Functions
        public void AddComboBoxGioiTinh()
        {
            comboBox_GioiTinhInput.DataSource = gioiTinh;
            comboBox_GioiTinhInput.SelectedIndex = -1;
        }

        //Hàm check input khi Insert
        private void CheckInputForInsertDocGia(ref bool check)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_HoVaTenInput.Text))
                missingFields.Add("Họ tên");
            if (string.IsNullOrWhiteSpace(comboBox_GioiTinhInput.Text))
                missingFields.Add("Giới tính");
            if (dateTimePicker_NgaySinhInput.Value == null)
                missingFields.Add("Ngày sinh");
            if (string.IsNullOrWhiteSpace(textBox_EmailInput.Text))
                missingFields.Add("Email");
            if (string.IsNullOrWhiteSpace(textBox_SoDienThoaiInput.Text))
                missingFields.Add("Số điện thoại");

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

        private void AddDocGia() //Hàm insert độc giả
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn thêm độc giả {textBox_HoVaTenInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Student student = new Student();
                student.IdStudent = ThuVien.GetInstance().Employee.StudentList.Students.Count() + 1;
                student.HoTen = textBox_HoVaTenInput.Text;
                student.GioiTinh = comboBox_GioiTinhInput.Text;
                student.NgaySinh = dateTimePicker_NgaySinhInput.Value.Date;
                student.Email = textBox_EmailInput.Text;
                student.SoDienThoai = textBox_SoDienThoaiInput.Text;
                student.LichSuMuon = "Lịch sử mượn";
                ThuVien.GetInstance().Employee.AddDocGia(student);
                ThuVienController.Serialize<StudentList>(FilePath.User, ThuVien.GetInstance().Employee.StudentList);
                MessageBox.Show("Thêm độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { MessageBox.Show("Thêm độc giả chưa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void ResetControl() //Hàm đặt các input về null
        {
            textBox_HoVaTenInput.Text = null;
            comboBox_GioiTinhInput.Text = null;
            dateTimePicker_NgaySinhInput.Value = DateTime.Now;
            textBox_EmailInput.Text = null;
            textBox_SoDienThoaiInput.Text = null;
        }
        #endregion

        //Events
        #region Events
        private void comboBox_GioiTinhInput_Leave(object sender, EventArgs e)
        {
            foreach (string item in gioiTinh)
            {
                if (comboBox_GioiTinhInput.Text == item)
                {
                    return;
                }
            }
            comboBox_GioiTinhInput.Text = "";
        }

        private void textBox_HoVaTenInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập số vào TextBox
            }
        }

        private void dateTimePicker_NgaySinhInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NgaySinhInput.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại");
                dateTimePicker_NgaySinhInput.Value = DateTime.Now;
            }
        }

        private void textBox_SoDienThoaiInput_KeyDown(object sender, KeyEventArgs e)
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
            CheckInputForInsertDocGia(ref checkInputInsert);
            if (checkInputInsert == true) AddDocGia();
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void dateTimePicker_NamXuatBanInsertInput_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_NgaySinhInput.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh hông được lớn hơn ngày hiện tại");
                dateTimePicker_NgaySinhInput.Value = DateTime.Now;
            }
        }
        #endregion
    }
}
