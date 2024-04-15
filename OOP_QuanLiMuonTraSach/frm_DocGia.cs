using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_DocGia : Form
    {
        //Variables
        #region Variables
        int idNguoiDung; //Biến chứa idNguoiDung
        #endregion

        public frm_DocGia(int idNguoiDung)
        {
            InitializeComponent();
            this.idNguoiDung = idNguoiDung;
        }

        //Events
        #region Events
        private void button_Searching_Click(object sender, EventArgs e)
        {
            FormController.openChildForm(new frm_Searching(idNguoiDung),panel_ChildForm);
        }

        private void button_History_Click(object sender, EventArgs e)
        {
            FormController.openChildForm(new frm_History(idNguoiDung), panel_ChildForm);
        }
        #endregion
    }
}
