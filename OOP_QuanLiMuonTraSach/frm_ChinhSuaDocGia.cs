using OOP_QuanLiMuonTraSach;
using OOP_QuanLiMuonTraSach.Person;
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
    public partial class frm_ChinhSuaDocGia : Form
    {
        //Variables
        #region Variables
        private int idDocGia; //Biến chứa idDocGia
        private Student student; //Biến student chứa idDocGia
        #endregion

        public frm_ChinhSuaDocGia(int idDocGia)
        {
            InitializeComponent();
            this.idDocGia = idDocGia;
            student = ThuVien.GetInstance().Employee.StudentList.FindStudent(idDocGia);
            BindingDataSelected();
        }

        //Functions
        #region Functions
        private void BindingDataSelected() //Hàm biding dữ liệu có trong dataGridView
        {
            textBox_IDInput.Text = student.IdStudent.ToString();
            textBox_HoVaTenInput.Text = student.HoTen;
            dateTimePicker_NgaySinhInput.Value = (DateTime)student.NgaySinh;
            comboBox_GioiTinhInput.Text = student.GioiTinh;
            textBox_EmailInput.Text = student.Email;
            textBox_SoDienThoaiInput.Text = student.SoDienThoai;
        }

        private void UpdateDocGia() //Hàm update lại độc giả
        {
            List<string> updateFields = new List<string>();

            if (textBox_HoVaTenInput.Text != student.HoTen)
                updateFields.Add("Họ tên");
            if (dateTimePicker_NgaySinhInput.Value != (DateTime)student.NgaySinh)
                updateFields.Add("Ngày sinh");
            if (comboBox_GioiTinhInput.Text != student.GioiTinh)
                updateFields.Add("Giới tính");
            if (textBox_EmailInput.Text != student.Email)
                updateFields.Add("Email");
            if (textBox_SoDienThoaiInput.Text != student.SoDienThoai)
                updateFields.Add("Số điện thoại"); 

            string updateFieldMessage = "Bạn muốn thay đổi \n";
            foreach (string field in updateFields)
            {
                updateFieldMessage += field + "\n";
            }
            updateFieldMessage += $"của độc giả {student.HoTen}";

            DialogResult result = MessageBox.Show(updateFieldMessage, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                student.HoTen = textBox_HoVaTenInput.Text;
                student.Email = textBox_EmailInput.Text;
                student.SoDienThoai = textBox_SoDienThoaiInput.Text;
                student.GioiTinh = comboBox_GioiTinhInput.Text;
                student.NgaySinh = dateTimePicker_NgaySinhInput.Value.Date;
                ThuVienController.Serialize<StudentList>(FilePath.User, ThuVien.GetInstance().Employee.StudentList);

                List<QuanLiMuonTraSach> qlmts = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.FindListQuanLiMuonTraSachByIdNguoiDung(idDocGia);
                foreach (QuanLiMuonTraSach item in qlmts)
                {
                    item.HoTen = textBox_HoVaTenInput.Text;
                }
                ThuVienController.Serialize<ListQuanLiMuonTraSach>(FilePath.QuanLiMuonTraSach, ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach);
                MessageBox.Show("Chỉnh sửa thông tin độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Chỉnh sửa thông tin độc giả thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        //Events
        #region Events
        private void button_SaveUpdate_Click(object sender, EventArgs e)
        {
            UpdateDocGia();
        }

        private void button_ResetUpdate_Click(object sender, EventArgs e)
        {
            BindingDataSelected();
        }

        private void dateTimePicker_NamXuatBanUpdateInput_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
