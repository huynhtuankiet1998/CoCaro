using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cocaro
{
    public partial class Form1 : Form
    {
        private CaroChess caroChess;
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            caroChess.KhoiTaoMangOCo();
            g = pnlBanco.CreateGraphics();
        }

        private void pnlBanco_Paint(object sender, PaintEventArgs e)
        {
            caroChess.VeBanCo(g);
            caroChess.VeLaiQuanCo(g);
        }

        private void pnlBanco_MouseClick(object sender, MouseEventArgs e)
        {
            if (!caroChess.SanSang) 
                return;
            else
            {
                caroChess.DanhCo(e.X, e.Y, g);
                caroChess.KhoiDongComputer(g);
                if (caroChess.KiemTraChienThang())
                    caroChess.KetThucTroChoi();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có chắc muốn thoát không?",
            "", MessageBoxButtons.YesNo);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            g.Clear(pnlBanco.BackColor);
            caroChess.StartPlayerVsCom(g);
        }
    }
}
