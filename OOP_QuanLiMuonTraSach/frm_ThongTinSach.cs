using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ThongTinSach : Form
    {
        BookList bookList = new BookList();
        string path = "book.txt";
        BookList deseriBL;
        DataTable dataTable = new DataTable();
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng

        public frm_ThongTinSach()
        {
            InitializeComponent();
            //AddBookToFile();

            deseriBL = bookList.Deserialize<BookList>(path);

            bookList.FindBook(deseriBL, "3");

            bookList.Serialize<BookList>(path, deseriBL);

            LoadData(path, bookList);
        }

        //Functions
        #region Functions
        private void AddBookToFile()
        {
            BookList bl = new BookList();
            bl.Add(new Book { IdSach = "1", TenSach = "Sach1", TacGia = "TacGia1", TheLoai = "Tiểu thuyết", SoLuong = "10", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = "20/03/2024"});
            bl.Add(new Book { IdSach = "2", TenSach = "Sach2", TacGia = "TacGia2", TheLoai = "Truyện trinh thám", SoLuong = "5", NhaXuatBan = "NXB Liên Doanh", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });
            bl.Add(new Book { IdSach = "3", TenSach = "Sach3", TacGia = "TacGia3", TheLoai = "Truyện cười", SoLuong = "25", NhaXuatBan = "NXB Trẻ em", NamXuatBan = "20/03/2024" });

            bookList.Serialize<BookList>(path, bl);
        }
        private void ResetLabelTextToNull(Label label) //Đặt text của lable về null
        {
            label.Text = null;
        }

        private void SetLabelText(Label label, string text) //Set text cho label
        {
            label.Text = text;
        }

        private void FocusTextBox(TextBox textBox) //Focus vào textBox
        {
            textBox.Focus();
        }

        private void SearchBooksByGeneral() //Hàm tìm kiếm nhân viên 1 cách tổng quát
        {
                var result = deseriBL.Books.Where(c => c.IdSach.ToString().Contains(textBox_SearchName.Text)
                                                     || c.TenSach.Contains(textBox_SearchName.Text)
                                                     || c.TacGia.Contains(textBox_SearchName.Text)
                                                     || c.TheLoai.Contains(textBox_SearchName.Text)
                                                     //|| c.ChuyenNganh.Contains(textBox_SearchName.Text)
                                                     || c.NhaXuatBan.Contains(textBox_SearchName.Text));
                if (result != null)
                {
                    // Tính toán lại số trang khi có kết quả mới
                    totalRecord = result.Count();
                    lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);

                    // Hiển thị trang đầu tiên
                    result = result.OrderBy(s => s.IdSach)
                                 .Skip((pageNumber - 1) * numberRecord)
                                 .Take(numberRecord);

                    // Hiển thị kết quả trong DataGridView
                    dataGridView_ChinhSuaSach.DataSource = result.Select(s => new
                    {
                        s.IdSach,
                        s.TenSach,
                        s.TacGia,
                        s.TheLoai,
                        s.SoLuong,
                        s.NhaXuatBan,
                        s.NamXuatBan
                    }).ToList();
                    AdjustRowHeight();
                    AdjustColumnWidth();
                    ChangeHeader();
                }
        }

        private void LoadData(string path, BookList bookList)
        {
            BookList deseriBL = bookList.Deserialize<BookList>(path);
            totalRecord = deseriBL.Books.Count;
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong 
            dataGridView_ChinhSuaSach.DataSource = LoadRecord(pageNumber, numberRecord);
            AdjustColumnWidth();
            AdjustRowHeight();
            ChangeHeader();
        }

        private List<object> LoadRecord(int page, int recordNum)
        {
            List<object> result = new List<object>();
            result = deseriBL.Books.Skip((page - 1) * recordNum)
                .Take(recordNum)
                 .Select(e => new
                 {
                     e.IdSach,
                     e.TenSach,
                     e.TacGia,
                     e.TheLoai,
                     e.SoLuong,
                     e.NhaXuatBan,
                     e.NamXuatBan
                 }).ToList<object>();
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_ChinhSuaSach.Height / (dataGridView_ChinhSuaSach.Rows.Count + 1);
            if (dataGridView_ChinhSuaSach.Rows.Count > 0 && dataGridView_ChinhSuaSach.Rows.Count < 5)
            {
                foreach (DataGridViewRow row in dataGridView_ChinhSuaSach.Rows)
                {
                    row.Height = 60;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_ChinhSuaSach.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_ChinhSuaSach.Columns.Count > 0)
            {
                dataGridView_ChinhSuaSach.Columns[0].Width = dataGridView_ChinhSuaSach.Width * 5 / 100;
                dataGridView_ChinhSuaSach.Columns[1].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[2].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[3].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[4].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[5].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[6].Width = dataGridView_ChinhSuaSach.Width * 15 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_ChinhSuaSach.Columns.Count > 0)
            {
                dataGridView_ChinhSuaSach.Columns[0].HeaderText = "ID";
                dataGridView_ChinhSuaSach.Columns[1].HeaderText = "Tên sách";
                dataGridView_ChinhSuaSach.Columns[2].HeaderText = "Tác giả";
                dataGridView_ChinhSuaSach.Columns[3].HeaderText = "Thể loại";
                dataGridView_ChinhSuaSach.Columns[4].HeaderText = "Số lượng";
                dataGridView_ChinhSuaSach.Columns[5].HeaderText = "Nhà xuất bản";
                dataGridView_ChinhSuaSach.Columns[6].HeaderText = "Năm xuất bản";
            }
        }
        #endregion

        //Events
        #region Events
        private void frm_ThongTinSach_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void textBox_SearchName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchName.Text.Length > 0)
            {
                Book findedBook = bookList.FindBook(deseriBL, textBox_SearchName.Text);
                if (findedBook != null)
                {
                    dataTable.Rows.Clear();
                    dataTable.Rows.Add(findedBook.IdSach, findedBook.TenSach, findedBook.TacGia);
                    dataGridView_ChinhSuaSach.DataSource = dataTable;
                } 
            }
        }

        private void label_SearchName_Click(object sender, EventArgs e)
        {
            textBox_SearchName.Focus();
            ResetLabelTextToNull(label_SearchName);
        }

        private void textBox_SearchName_Click(object sender, EventArgs e)
        {
            ResetLabelTextToNull(label_SearchName);
        }

        private void textBox_SearchName_Leave(object sender, EventArgs e)
        {
            label_SearchName.Text = "Search by id, name...";
        }

        #endregion
    }
}
