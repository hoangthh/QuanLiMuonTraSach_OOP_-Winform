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
    public partial class frm_LichSuMuonTraDocGia : Form
    {
        //Variables
        #region Variables
        int idNguoiDung; //Biến chứa idNguoiDung
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 5; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        #endregion

        public frm_LichSuMuonTraDocGia(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
            LoadData();
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        //Functions
        #region Functions
        private void LoadData()
        {
            totalRecord = ThuVien.GetInstance().Employee.ListQuanLiMuonTraSach.UserListQuanLiMuonTraSach(idNguoiDung).QuanLiMuonTraSaches.Count;
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong bảng
            dataGridView_LichSuMuonTra.DataSource = LoadRecord(pageNumber, numberRecord); //Hiển thị lên dataGridView
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
                QuanLiMuonTraSach mt = qlmts[i];
                object newItem = new
                {
                    mt.IdMuonTra,
                    mt.IdNguoiDung,
                    mt.HoTen,
                    mt.TenSach,
                    mt.IdSach,
                    mt.NgayMuon,
                    mt.NgayTraThucTe,
                    mt.SoTienPhat
                };
                result.Add(newItem);
            }
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_LichSuMuonTra.Height / (dataGridView_LichSuMuonTra.Rows.Count + 1);
            if (dataGridView_LichSuMuonTra.Rows.Count > 0 && dataGridView_LichSuMuonTra.Rows.Count < 5)
            {
                foreach (DataGridViewRow row in dataGridView_LichSuMuonTra.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_LichSuMuonTra.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_LichSuMuonTra.Columns.Count > 0)
            {
                dataGridView_LichSuMuonTra.Columns[0].Width = dataGridView_LichSuMuonTra.Width * 10 / 100;
                dataGridView_LichSuMuonTra.Columns[1].Width = dataGridView_LichSuMuonTra.Width * 10 / 100;
                dataGridView_LichSuMuonTra.Columns[2].Width = dataGridView_LichSuMuonTra.Width * 20 / 100;
                dataGridView_LichSuMuonTra.Columns[3].Width = dataGridView_LichSuMuonTra.Width * 20 / 100;
                dataGridView_LichSuMuonTra.Columns[4].Width = dataGridView_LichSuMuonTra.Width * 10 / 100;
                dataGridView_LichSuMuonTra.Columns[5].Width = dataGridView_LichSuMuonTra.Width * 10 / 100;
                dataGridView_LichSuMuonTra.Columns[6].Width = dataGridView_LichSuMuonTra.Width * 10 / 100;
                dataGridView_LichSuMuonTra.Columns[7].Width = dataGridView_LichSuMuonTra.Width * 10 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_LichSuMuonTra.Columns.Count > 0)
            {
                dataGridView_LichSuMuonTra.Columns[0].HeaderText = "Mã mượn";
                dataGridView_LichSuMuonTra.Columns[1].HeaderText = "ID User";
                dataGridView_LichSuMuonTra.Columns[2].HeaderText = "Họ tên";
                dataGridView_LichSuMuonTra.Columns[3].HeaderText = "Tên sách";
                dataGridView_LichSuMuonTra.Columns[4].HeaderText = "ID sách";
                dataGridView_LichSuMuonTra.Columns[5].HeaderText = "Ngày mượn";
                dataGridView_LichSuMuonTra.Columns[6].HeaderText = "Ngày trả";
                dataGridView_LichSuMuonTra.Columns[7].HeaderText = "Phí phạt";
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
        private void XemLichSuMuonDocGia_Load(object sender, EventArgs e)
        {
            LoadData();
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void XemLichSuMuonDocGia_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                LoadData();
                CreateOrderForButtonChangePageByPageNumber(pageNumber);
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage2);
            }
            else
            {
                LoadData();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
            }
        }

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            LoadData();
            ResetColorButton();
            HighlightButtonCurrentPage(sender);
        }

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);

            if (lastPageNumber > pageNumber)
            {
                LoadData();
                CreateOrderForButtonChangePageByPageNumber(pageNumber);
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage2);
            }
            else
            {
                LoadData();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage3);
            }
        }

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            LoadData();
            ResetColorButton();
            HighlightButtonCurrentPage(button_ChangePage1);
            SetDefaultButtonChangePageText();
        }

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            pageNumber = lastPageNumber;
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
        #endregion
    }
}
