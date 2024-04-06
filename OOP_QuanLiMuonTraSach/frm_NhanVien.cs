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
    public partial class frm_NhanVien : Form
    {
        //Variables
        #region Variables
        bool menuExpandQuanLiSach = false; //Biến hiển thị độ mở rộng của button con
        bool menuExpandQuanLiDocGia = false; //Biến hiển thị độ mở rộng của button con
        private Form activeForm = null; //Biến thể hiện panel cố định đã có form nào được mở chưa
        #endregion

        public frm_NhanVien()
        {
            InitializeComponent();
            panel_ChildQuanLiSach.Height = 0; //Mặc định button con của QLS có height = 0
            panel_ChildQuanLiDocGia.Height = 0; //Mặc định button con của QLDG có height = 0
        }

        //Functions
        #region Functions
        //Hàm để mở rộng và thu nhỏ các button con
        private void MenuExpand_Transition(ref bool menuExpand, Panel panel, Timer timer)
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 100)
                {
                    StopTimer(timer);
                    menuExpand = true;
                }
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    StopTimer(timer);
                    menuExpand = false;
                }
            }
        }

        private void StartTimer(Timer timer) //Hàm để Start Timer
        {
            timer.Start();
        }

        private void StopTimer(Timer timer) //Hàm để stop Timer
        {
            timer.Stop();
        }

        //Hàm mở ChildForm vào 1 panel cố định trong giao diện
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_ChildForm.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
        #endregion

        //Events
        #region Events
        private void timer_QuanLiSachTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandQuanLiSach, panel_ChildQuanLiSach, timer_QuanLiSachTransition);
        }

        private void timer_QuanLiDocGiaTransition_Tick(object sender, EventArgs e)
        {
            MenuExpand_Transition(ref menuExpandQuanLiDocGia, panel_ChildQuanLiDocGia, timer_QuanLiDocGiaTransition);
        }

        private void button_QuanLiSach_Click(object sender, EventArgs e)
        {
            StartTimer(timer_QuanLiSachTransition);
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "";
        }

        private void button_ThongTinSach_Click(object sender, EventArgs e)
        {
            openChildForm(new frm_ThongTinSach());
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "> Quản lí sách > Thông tin sách";
        }

        private void button_MuonTraSach_Click(object sender, EventArgs e)
        {
            //openChildForm(new ff_MuonTraSach());
            label_CurrentPage.Text = "Quản lí sách";
            label_CurrentFunction.Text = "> Quản lí sách > Mượn trả sách";
        }
        #endregion
    }
}
