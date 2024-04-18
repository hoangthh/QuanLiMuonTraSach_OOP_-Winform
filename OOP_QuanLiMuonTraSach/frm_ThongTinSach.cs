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
        #region
        private int pageNumber = 1; //Biến thể hiện trang hiện tại
        private int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        private int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        private int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        private List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        private bool menuExpand = false; //Biến hiển thị độ mở rộng của 1 control
        private bool isFilter = false; //Biến kiểm tra filter được mở hay đóng
        private SortedSet<string> TheLoais; //Các thể loại có trong Sach
        #endregion

        public frm_ThongTinSach()
        {
            InitializeComponent();
            panel_Filter.Height = 0; //ban đầu panel filter = 0
            AddComboBoxTheLoai();
            comboBox_SearchTheLoai.SelectedIndex = -1;
            LoadData();
        }

        //Functions
        #region Functions   
        private void AddComboBoxTheLoai() //Hàm thêm thể loại vào comboBox
        {
            TheLoais = new SortedSet<string>();
            foreach (Book book in ThuVien.GetInstance().Employee.BookList.Books)
            {
                TheLoais.Add(book.TheLoai);
            }
            comboBox_SearchTheLoai.DataSource = TheLoais.ToList();
        }

        //Hàm để mở rộng và thu nhỏ các button con
        private void Sidebar_Transition(ref bool menuExpand, Panel panel, Timer timer)
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 83)
                {
                    StopTimer(timer);
                    menuExpand = true;
                }
                isFilter = true;
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    StopTimer(timer);
                    menuExpand = false;
                }
                isFilter = false;
            }
        }

        private void StartTimer(Timer timer) //Hàm để Start Timer
        {
            timer.Start();
        }

        private void StopTimer(Timer timer) //Hàm để Stop Timer
        {
            timer.Stop();
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
            // Lấy danh sách sách từ BookList
            List<Book> books = ThuVien.GetInstance().Employee.BookList.Books.ToList();

            // Tạo một danh sách kết quả trống
            List<Book> result = new List<Book>();

            // Duyệt qua từng sách và kiểm tra điều kiện tìm kiếm
            foreach (Book book in books)
            {
                if (book.IdSach.ToString().Contains(textBox_SearchName.Text)
                    || book.TenSach.ToLower().Contains(textBox_SearchName.Text.ToLower())
                    || book.TacGia.ToLower().Contains(textBox_SearchName.Text.ToLower())
                    || book.TheLoai.ToLower().Contains(textBox_SearchName.Text.ToLower())
                    || book.NhaXuatBan.ToLower().Contains(textBox_SearchName.Text.ToLower()))
                {
                    result.Add(book);
                }
            }

            if (result.Count > 0)
            {
                // Tính toán lại số trang khi có kết quả mới
                totalRecord = result.Count;
                lastPageNumber = (int)Math.Ceiling((double)result.Count / numberRecord);

                // Hiển thị kết quả trong DataGridView
                List<object> resultList = new List<object>();

                int startIndex = (pageNumber - 1) * numberRecord;
                int count = Math.Min(numberRecord, result.Count - startIndex);

                for (int i = startIndex; i < startIndex + count; i++)
                {
                    Book e = result[i];
                    object newItem = new
                    {
                        e.IdSach,
                        e.TenSach,
                        e.TacGia,
                        e.TheLoai,
                        e.SoLuong,
                        e.NhaXuatBan,
                        e.NamXuatBan
                    };
                    resultList.Add(newItem);
                }
                dataGridView_ChinhSuaSach.DataSource = resultList;
                AdjustRowHeight();
                AdjustColumnWidth();
                ChangeHeader();
            }
        }
        public static List<Book> FilterList(List<Book> resultlist, string namefilter, string content)
        {
            if (resultlist == null || resultlist.Count == 0) return resultlist;
            List<Book> filteredlist;
            switch (namefilter)
            {
                case "TenSach":
                    filteredlist = new List<Book>();
                    for (int i = 0; i < resultlist.Count; i++)
                    {
                        if (resultlist[i].TenSach.IndexOf(content.Trim(), StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            filteredlist.Add(resultlist[i]);
                        }
                    }
                    foreach (Book item in filteredlist)
                    {
                        resultlist.Remove(item);
                    }
                    return resultlist;
                case "TacGia":
                    filteredlist = new List<Book>();
                    for (int i = 0; i < resultlist.Count(); i++)
                    {
                        if (resultlist[i].TacGia.IndexOf(content.Trim(), StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            filteredlist.Add(resultlist[i]);
                        }
                    }
                    foreach (Book item in filteredlist)
                    {
                        resultlist.Remove(item);
                    }
                    return resultlist;
                case "NhaXuatBan":
                    filteredlist = new List<Book>();
                    for (int i = 0; i < resultlist.Count(); i++)
                    {
                        if (resultlist[i].NhaXuatBan.IndexOf(content.Trim(), StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            filteredlist.Add(resultlist[i]);
                        }
                    }
                    foreach (Book item in filteredlist)
                    {
                        resultlist.Remove(item);
                    }
                    return resultlist;
                case "TheLoai":
                    filteredlist = new List<Book>();
                    for (int i = 0; i < resultlist.Count(); i++)
                    {
                        if (resultlist[i].TheLoai.IndexOf(content.Trim(), StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            filteredlist.Add(resultlist[i]);
                        }
                    }
                    foreach (Book item in filteredlist)
                    {
                        resultlist.Remove(item);
                    }
                    return resultlist;
                default:
                    return resultlist;
            }
        }
        private void SearchBooksByFilter() //Hàm tìm kiếm sách theo filter
        {
            List<Book> query = new List<Book>(ThuVien.GetInstance().Employee.BookList.Books);

            // Kiểm tra và thêm điều kiện tìm kiếm cho tên sách
            if (!string.IsNullOrWhiteSpace(textBox_SearchTenSach.Text))
            {
                FilterList(query, "TenSach", textBox_SearchTenSach.Text);
            }

            // Kiểm tra và thêm điều kiện tìm kiếm cho tác giả
            if (!string.IsNullOrWhiteSpace(textBox_SearchTacGia.Text))
            {
                FilterList(query, "TacGia", textBox_SearchTacGia.Text);
            }

            // Kiểm tra và thêm điều kiện tìm kiếm cho nhà xuất bản
            if (!string.IsNullOrWhiteSpace(textBox_SearchNhaXuatBan.Text))
            {
                FilterList(query, "NhaXuatBan", textBox_SearchNhaXuatBan.Text);
            }

            // Kiểm tra và thêm điều kiện tìm kiếm cho thể loại
            if (comboBox_SearchTheLoai.Text != null)
            {
                FilterList(query, "TheLoai", comboBox_SearchTheLoai.Text);
            }

            // Tính tổng số kết quả tìm kiếm
            totalRecord = query.Count();

            // Tính toán số trang và lưu vào biến lastPageNumber
            lastPageNumber = (int)Math.Ceiling((double)query.Count / numberRecord);
            List<object> result = new List<object>();


            int startIndex = (pageNumber - 1) * numberRecord;
            int count = Math.Min(numberRecord, query.Count - startIndex);

            for (int i = startIndex; i < startIndex + count; i++)
            {
                Book s = query[i];
                object newItem = new
                {
                    s.IdSach,
                    s.TenSach,
                    s.TacGia,
                    s.TheLoai,
                    s.SoLuong,
                    s.NhaXuatBan,
                    s.NamXuatBan
                };
                result.Add(newItem);
            }
            // Hiển thị kết quả trong DataGridView
            dataGridView_ChinhSuaSach.DataSource = result;
            AdjustRowHeight();
            AdjustColumnWidth();
            ChangeHeader();
        }

        private void LoadData()
        {
            totalRecord = ThuVien.GetInstance().Employee.BookList.Books.Count;
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong 
            dataGridView_ChinhSuaSach.DataSource = LoadRecord(pageNumber, numberRecord);
            AdjustColumnWidth();
            AdjustRowHeight();
            ChangeHeader();
        }

        private List<object> LoadRecord(int page, int recordNum)
        {
            List<object> result = new List<object>();
            List<Book> books = ThuVien.GetInstance().Employee.BookList.Books;

            int startIndex = (page - 1) * recordNum;
            int count = Math.Min(recordNum, books.Count - startIndex);

            for (int i = startIndex; i < startIndex + count; i++)
            {
                Book e = books[i];
                object newItem = new
                {
                    e.IdSach,
                    e.TenSach,
                    e.TacGia,
                    e.TheLoai,
                    e.SoLuong,
                    e.NhaXuatBan,
                    e.NamXuatBan
                };
                result.Add(newItem);
            }
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
                dataGridView_ChinhSuaSach.Columns[3].Width = dataGridView_ChinhSuaSach.Width * 13 / 100;
                dataGridView_ChinhSuaSach.Columns[4].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[5].Width = dataGridView_ChinhSuaSach.Width * 17 / 100;
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

        private void AddButtonChangePageList() //Hàm thêm các button phân trang vào list phân trang
        {
            buttonChangePageList.Add(button_ChangePage1);
            buttonChangePageList.Add(button_ChangePage2);
            buttonChangePageList.Add(button_ChangePage3);
            buttonChangePageList.Add(button_ReturnFirstPage);
            buttonChangePageList.Add(button_ReturnLastPage);
        }

        //Hàm tạo thứ tự cho các button phân trang căn cứ vào trang hiện tại
        private void CreateOrderForButtonChangePageByPageNumber(int pageNumber)
        {
            button_ChangePage1.Text = (pageNumber - 1).ToString();
            button_ChangePage2.Text = pageNumber.ToString();
            button_ChangePage3.Text = (pageNumber + 1).ToString();
        }

        //Hàm đặt các button phân trang về mặc định: 1 2 3
        private void SetDefaultButtonChangePageText()
        {
            button_ChangePage1.Text = "1";
            button_ChangePage2.Text = "2";
            button_ChangePage3.Text = "3";
        }

        //Hàm tạo thứ tự cho các button phân trang căn cứ vào trang cuối cùng
        private void CreateOrderForButtonChangePageByLastPageNumber(int lastPageNumber)
        {
            button_ChangePage1.Text = (lastPageNumber - 2).ToString();
            button_ChangePage2.Text = (lastPageNumber - 1).ToString();
            button_ChangePage3.Text = lastPageNumber.ToString();
        }

        private void ResetColorButton() //Hàm đặt lại màu của các button phân trang
        {
            AddButtonChangePageList();
            foreach (Button button in buttonChangePageList)
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.Black;
            }
        }

        private void HighlightButtonCurrentPage(object obj) //Hàm hightlight button phân trang được truyền vào
        {
            Button sender = obj as Button;
            sender.BackColor = Color.FromArgb(0, 95, 105);
            sender.ForeColor = Color.White;
        }
        #endregion

        //Events
        #region Events
        private void frm_ThongTinSach_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void pictureBox_Filter_Click(object sender, EventArgs e)
        {
            StartTimer(timer_FilterTransition);
            if (isFilter == true)
            {
                textBox_SearchName.Enabled = true;
                label_SearchName.Enabled = true;
            }
            else
            {
                textBox_SearchName.Enabled = false;
                LoadData();
            }
        }
        private void timer_FilterTransition_Tick(object sender, EventArgs e)
        {
            Sidebar_Transition(ref menuExpand, panel_Filter, timer_FilterTransition);
        }

        private void textBox_SearchName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchName.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchName);//Nếu text trong ô textBox được nhập thì xóa label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SearchBooksByGeneral();
            }
            else
            {
                SetLabelText(label_SearchName, "Search by id, book, author..."); //Nếu text rỗng thì hiện lại label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                LoadData();
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
            if (textBox_SearchName.Text.Length == 0)
                label_SearchName.Text = "Search by id, book, author...";
        }

        private void textBox_SearchTenSach_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchTenSach.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchTenSach);//Nếu text trong ô textBox được nhập thì xóa label Search
            }
            else
            {
                SetLabelText(label_SearchTenSach, "Search by book name..."); //Nếu text rỗng thì hiện lại label Search
            }
        }

        private void textBox_SearchTenSach_Click(object sender, EventArgs e)
        {
            if (textBox_SearchTenSach.Text.Length == 0)
                ResetLabelTextToNull(label_SearchTenSach); //TextBox được click thì xóa label Search
        }

        private void label_SearchTenSach_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchTenSach); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchTenSach); //Xóa label Search
        }

        private void textBox_SearchTacGia_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchTacGia.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchTacGia);//Nếu text trong ô textBox được nhập thì xóa label Search                
            }
            else
            {
                SetLabelText(label_SearchTacGia, "Search by author name..."); //Nếu text rỗng thì hiện lại label Search
            }
        }

        private void textBox_SearchTacGia_Click(object sender, EventArgs e)
        {
            if (textBox_SearchTacGia.Text.Length == 0)
                ResetLabelTextToNull(label_SearchTacGia); //TextBox được click thì xóa label Search
        }

        private void label_SearchTacGia_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchTacGia); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchTacGia); //Xóa label Search
        }

        private void textBox_SearchNhaXuatBan_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchNhaXuatBan.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchNhaXuatBan);//Nếu text trong ô textBox được nhập thì xóa label Search                
            }
            else
            {
                SetLabelText(label_SearchNhaXuatBan, "Search by publisher..."); //Nếu text rỗng thì hiện lại label Search
            }
        }

        private void textBox_SearchNhaXuatBan_Click(object sender, EventArgs e)
        {
            if (textBox_SearchNhaXuatBan.Text.Length == 0)
                ResetLabelTextToNull(label_SearchNhaXuatBan); //TextBox được click thì xóa label Search
        }

        private void label_SearchNhaXuatBan_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchNhaXuatBan); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchNhaXuatBan); //Xóa label Search
        }

        private void comboBox_SearchTheLoai_Leave(object sender, EventArgs e)
        {
            foreach (string item in TheLoais)
            {
                if (comboBox_SearchTheLoai.Text == item) return;
            }
            comboBox_SearchTheLoai.Text = "";
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
                {
                    if (textBox_SearchName.Text != null)
                    {
                        SearchBooksByGeneral();
                        CreateOrderForButtonChangePageByPageNumber(pageNumber);
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage2);
                    }
                    else
                    {
                        LoadData();
                        CreateOrderForButtonChangePageByPageNumber(pageNumber);
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage2);
                    }
                }
            }
            else
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                }
                else
                {
                    if (textBox_SearchName.Text != null)
                    {
                        SearchBooksByGeneral();
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage1);
                    }
                    else
                    {
                        LoadData();
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage1);
                    }
                }
            }
        }

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            if (isFilter == true)
            {
                SearchBooksByFilter();
                ResetColorButton();
                HighlightButtonCurrentPage(sender);
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    ResetColorButton();
                    HighlightButtonCurrentPage(sender);
                }
                else
                {
                    LoadData();
                    ResetColorButton();
                    HighlightButtonCurrentPage(sender);
                }
            }
        }

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);
            if (lastPageNumber > pageNumber)
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
                {
                    if (textBox_SearchName.Text != null)
                    {
                        SearchBooksByGeneral();
                        CreateOrderForButtonChangePageByPageNumber(pageNumber);
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage2);
                    }
                    else
                    {
                        LoadData();
                        CreateOrderForButtonChangePageByPageNumber(pageNumber);
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage2);
                    }
                }
            }
            else
            {
                if (isFilter == true)
                {
                    SearchBooksByFilter();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                }
                else
                {
                    if (textBox_SearchName.Text != null)
                    {
                        SearchBooksByGeneral();
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage3);
                    }
                    else
                    {
                        LoadData();
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage3);
                    }
                }
            }
        }

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            if (isFilter == true)
            {
                SearchBooksByFilter();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SetDefaultButtonChangePageText();
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    SetDefaultButtonChangePageText();
                }
                else
                {
                    LoadData();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    SetDefaultButtonChangePageText();
                }
            }
        }

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 0) return;
            pageNumber = lastPageNumber;
            if (isFilter == true)
            {
                SearchBooksByFilter();
                if (pageNumber == 1)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    return;
                }
                else if (pageNumber == 2)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                    return;
                }
                else
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                    CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                }
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    if (pageNumber == 1)
                    {
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage1);
                        return;
                    }
                    else if (pageNumber == 2)
                    {
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage2);
                        return;
                    }
                    else
                    {
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage3);
                        CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                    }
                }
                else
                {
                    LoadData();
                    if (pageNumber == 1)
                    {
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage1);
                        return;
                    }
                    else if (pageNumber == 2)
                    {
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage2);
                        return;
                    }
                    else
                    {
                        ResetColorButton();
                        HighlightButtonCurrentPage(button_ChangePage3);
                        CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                    }
                }
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            if (isFilter == true)
            {
                pageNumber = 1;
                numberRecord = 5;
                SearchBooksByFilter();
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
            }
        }

        private void button_Insert_Click(object sender, EventArgs e)
        {
            frm_ThemSach form = new frm_ThemSach();
            form.ShowDialog();
            LoadData();
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView_ChinhSuaSach.RowCount == 0) return;
            object idSach = dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IdSach"].Value;
            if (idSach == null) return;
            int selectedID = Convert.ToInt32(idSach.ToString());
            frm_ChinhSuaSach form = new frm_ChinhSuaSach(selectedID);
            form.ShowDialog();
            LoadData();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView_ChinhSuaSach.RowCount == 0) return;
            int selectedID = Convert.ToInt32(dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IdSach"].Value.ToString());
            Book bookFind = ThuVien.GetInstance().Employee.FindBook(selectedID);
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa sách {bookFind.TenSach} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                ThuVien.GetInstance().Employee.RemoveBook(bookFind);
                ThuVienController.Serialize<BookList>(FilePath.Book, ThuVien.GetInstance().Employee.BookList);
                MessageBox.Show("Xóa sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void dataGridView_ChinhSuaSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedID = Convert.ToInt32(dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IdSach"].Value.ToString());
            frm_ThongTinSachChiTiet form = new frm_ThongTinSachChiTiet(selectedID);
            form.Show();
        }
        #endregion
    }
}
