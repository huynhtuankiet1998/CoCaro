using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cocaro
{
    class BanCo
    {
        private int _SoDong;
        private int _SoCot;

        public int SoDong
        {
            get
            {
                return _SoDong;
            }

            set
            {
                _SoDong = value;
            }
        }

        public int SoCot
        {
            get
            {
                return _SoCot;
            }

            set
            {
                _SoCot = value;
            }
        }

        public BanCo()
        {
            SoDong = 0;
            SoCot = 0;

        }
        public BanCo(int soDong,int soCot)
        {
            SoDong = soDong;
            SoCot = soCot;
        }
        public void VeBanCo(Graphics g)
        {
            for(int i=0;i<=SoCot;i++)
            {
                g.DrawLine(CaroChess.pen, i * OCo._ChieuRong, 0, i * OCo._ChieuRong, SoDong * OCo._ChieuCao);
            }

            for (int j = 0; j <= SoDong; j++)
            {
                g.DrawLine(CaroChess.pen, 0 , j*OCo._ChieuCao , SoCot*OCo._ChieuRong , j*OCo._ChieuCao);
            }


        }
        public void VeQuanCo(Graphics g,Point point,SolidBrush sb)
        {
            g.FillEllipse(sb, point.X, point.Y, OCo._ChieuRong, OCo._ChieuCao);

        }
    }
}
