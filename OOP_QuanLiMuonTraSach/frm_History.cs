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
    public partial class frm_History : Form
    {
        //Variables
        #region Variables
        int idNguoiDung; //Biến chứa idNguoiDung
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        #endregion

        public frm_History(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            LoadData();
            CustomizeColumnTinhTrang();
        }

        //Functions
        #region Functions
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

        private void CustomizeColumnTinhTrang()
        {
            if (dataGridView_History.Columns.Count > 0)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                foreach (QuanLiMuonTraSach qlmts in ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.QuanLiMuonTraSaches)
                {
                    style.ForeColor = Color.FromArgb(0, 95, 105);
                }

                dataGridView_History.Columns["TinhTrang"].DefaultCellStyle = style;
            }
        }

        private void SearchMuonTrasByGeneral() //Hàm tìm kiếm nhân viên 1 cách tổng quát
        {
            // Lấy danh sách sách từ BookList
            List<QuanLiMuonTraSach> qlmts = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.UserListQuanLiMuonTraSach(idNguoiDung).QuanLiMuonTraSaches.ToList();

            // Tạo một danh sách kết quả trống
            List<QuanLiMuonTraSach> result = new List<QuanLiMuonTraSach>();

            // Duyệt qua từng sách và kiểm tra điều kiện tìm kiếm
            foreach (QuanLiMuonTraSach item in qlmts)
            {
                if (item.IdMuonTra.ToString().Contains(textBox_SearchName.Text)
                    || item.IdSach.ToString().Contains(textBox_SearchName.Text)
                    || item.HoTen.ToLower().Contains(textBox_SearchName.Text.ToLower())
                    || item.TenSach.ToLower().Contains(textBox_SearchName.Text.ToLower())
                    || item.TinhTrang.ToLower().Contains(textBox_SearchName.Text.ToLower()))
                {
                    result.Add(item);
                }
            }

            if (result.Count >= 0)
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
                    QuanLiMuonTraSach e = result[i];
                    object newItem = new
                    {
                        e.IdMuonTra,
                        e.TenSach,
                        e.IdSach,
                        e.NgayMuon,
                        e.NgayTraThucTe,
                        e.TinhTrang,
                        e.SoTienPhat
                    };
                    resultList.Add(newItem);
                }
                dataGridView_History.DataSource = resultList;
                AdjustRowHeight();
                AdjustColumnWidth();
                ChangeHeader();
            }
        }

        private void LoadData()
        {
            totalRecord = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.UserListQuanLiMuonTraSach(idNguoiDung).QuanLiMuonTraSaches.Count;
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_History.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
            AdjustRowHeight(); //Customize lại height các dòng
            AdjustColumnWidth(); //Customize lại width các cột
            ChangeHeader(); //Thay đổi tiêu đề hiển thị trên dataGridView
        }

        List<object> LoadRecord(int page, int recordNum) //Hàm phân trang
        {
            List<object> result = new List<object>();
            List<QuanLiMuonTraSach> qlmts = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.UserListQuanLiMuonTraSach(idNguoiDung).QuanLiMuonTraSaches;

            int startIndex = (page - 1) * recordNum;
            int count = Math.Min(recordNum, qlmts.Count - startIndex);

            for (int i = startIndex; i < startIndex + count; i++)
            {
                QuanLiMuonTraSach e = qlmts[i];
                object newItem = new
                {
                    e.IdMuonTra,
                    e.TenSach,
                    e.IdSach,
                    e.NgayMuon,
                    e.NgayTraThucTe,
                    e.TinhTrang,
                    e.SoTienPhat
                };
                result.Add(newItem);
            }
            return result;
        }
        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_History.Height / (dataGridView_History.Rows.Count + 1);
            if (dataGridView_History.Rows.Count > 0 && dataGridView_History.Rows.Count < 10)
            {
                foreach (DataGridViewRow row in dataGridView_History.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_History.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_History.Columns.Count > 0)
            {
                dataGridView_History.Columns[0].Width = dataGridView_History.Width * 10 / 100;
                dataGridView_History.Columns[1].Width = dataGridView_History.Width * 24 / 100;
                dataGridView_History.Columns[2].Width = dataGridView_History.Width * 9 / 100;
                dataGridView_History.Columns[3].Width = dataGridView_History.Width * 16 / 100;
                dataGridView_History.Columns[4].Width = dataGridView_History.Width * 16 / 100;
                dataGridView_History.Columns[5].Width = dataGridView_History.Width * 15 / 100;
                dataGridView_History.Columns[6].Width = dataGridView_History.Width * 10 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_History.Columns.Count > 0)
            {
                dataGridView_History.Columns[0].HeaderText = "ID Mượn trả";
                dataGridView_History.Columns[1].HeaderText = "Tên Sách";
                dataGridView_History.Columns[2].HeaderText = "ID Sách";
                dataGridView_History.Columns[3].HeaderText = "Ngày mượn";
                dataGridView_History.Columns[4].HeaderText = "Ngày trả";
                dataGridView_History.Columns[5].HeaderText = "Tình Trạng";
                dataGridView_History.Columns[6].HeaderText = "Phí phạt";
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
        private void FormHistory_Resize(object sender, EventArgs e)
        {
            AdjustRowHeight();
            AdjustColumnWidth();
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
                SearchMuonTrasByGeneral();
            }
            else
            {
                SetLabelText(label_SearchName, "Search by id, name, email..."); //Nếu text rỗng thì hiện lại label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                LoadData();
            }
        }

        private void textBox_SearchName_Click(object sender, EventArgs e)
        {
            if (textBox_SearchName.Text.Length == 0)
                ResetLabelTextToNull(label_SearchName); //TextBox được click thì xóa label Search
        }

        private void label_SearchName_Click(object sender, EventArgs e)
        {
            FocusTextBox(textBox_SearchName); //Nếu click vào label Search thì chuyển Focus vào textBox
            ResetLabelTextToNull(label_SearchName); //Xóa label Search
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchMuonTrasByGeneral();
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
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchMuonTrasByGeneral();
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

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            if (textBox_SearchName.Text != null)
            {
                SearchMuonTrasByGeneral();
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

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);

            if (lastPageNumber > pageNumber)
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchMuonTrasByGeneral();
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
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchMuonTrasByGeneral();
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

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            if (textBox_SearchName.Text != null)
            {
                SearchMuonTrasByGeneral();
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

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            pageNumber = lastPageNumber;
            if (textBox_SearchName.Text != null)
            {
                SearchMuonTrasByGeneral();
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
        #endregion

        private void dataGridView_History_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột có phải là cột TrinhTrang không và có giá trị không
            if (dataGridView_History.Columns[e.ColumnIndex].Name == "TinhTrang" && e.Value != null)
            {
                // Lấy giá trị của ô
                string value = e.Value.ToString();

                // Thiết lập màu cho ô tùy thuộc vào giá trị của ô
                switch (value)
                {
                    case "Yêu cầu":
                        e.CellStyle.ForeColor = Color.Purple;
                        break;
                    case "Đang mượn":
                        e.CellStyle.ForeColor = Color.Blue;
                        break;
                    case "Đã trả":
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case "Quá hạn":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    default:
                        // Nếu giá trị không phù hợp, sử dụng màu mặc định
                        e.CellStyle.ForeColor = dataGridView_History.DefaultCellStyle.ForeColor;
                        break;
                }
            }
        }
    }
}
