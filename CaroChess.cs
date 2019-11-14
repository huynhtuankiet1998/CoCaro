using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cocaro
{
    class CaroChess
    {
        public enum KETTHUC
        {
            HoaCo,
            Player,
            COM,
        }
        public static Pen pen;
        public static SolidBrush sbwhite;
        public static SolidBrush sbblack;
        private OCo[,] _MangOCo;
        private BanCo _BanCo;
        private Stack<OCo> stkCacNuocDaDi;
        private int _LuotDi;
        private bool _SanSang;
  
        private KETTHUC _ketthuc;

        public bool SanSang
        {
            get
            {
                return _SanSang;
            }

            set
            {
                _SanSang = value;
            }
        }


        public CaroChess()
        {
            pen = new Pen(Color.Purple);
            sbwhite = new SolidBrush(Color.White);
            sbblack = new SolidBrush(Color.Black);
            _BanCo = new BanCo(20, 20);
            _MangOCo = new OCo[_BanCo.SoDong, _BanCo.SoCot];
            stkCacNuocDaDi = new Stack<OCo>();
            _LuotDi = 1;
        }
        public void VeBanCo(Graphics g)
        {
            _BanCo.VeBanCo(g);
        }
        public void KhoiTaoMangOCo()
        {
            for (int i=0;i<_BanCo.SoDong;i++)
            {
                for(int j=0;j<_BanCo.SoCot;j++)
                {
                    _MangOCo[i, j] = new OCo(i, j,new Point(j*OCo._ChieuRong,i*OCo._ChieuCao),0);
                }
            }
        }
        public bool DanhCo(int MouseX,int MouseY,Graphics g)
        {
            if (MouseX % OCo._ChieuRong == 0||MouseY%OCo._ChieuCao==0)
                return false;
            int Cot = MouseX / OCo._ChieuRong;
            int Dong = MouseY / OCo._ChieuCao;
            if (_MangOCo[Dong, Cot].SoHuu != 0)
                return false;
            switch(_LuotDi)
            {
                case 1:
                    _MangOCo[Dong, Cot].SoHuu = 1;
                    _BanCo.VeQuanCo(g, _MangOCo[Dong, Cot].ViTri, sbblack);
                    _LuotDi = 2;
                    break;
                case 2:
                    _MangOCo[Dong, Cot].SoHuu = 2;
                    _BanCo.VeQuanCo(g, _MangOCo[Dong, Cot].ViTri, sbwhite);
                    _LuotDi = 1;
                    break;

            }

            stkCacNuocDaDi.Push(_MangOCo[Dong, Cot]);
            return true;

        }
        public void VeLaiQuanCo(Graphics g)
        {
            foreach (OCo oco in stkCacNuocDaDi)
            {
                if (oco.SoHuu == 1)
                    _BanCo.VeQuanCo(g, oco.ViTri, sbblack);
                else if (oco.SoHuu == 2)
                    _BanCo.VeQuanCo(g, oco.ViTri, sbwhite);

            }

        }
        public void StartPlayerVsCom(Graphics g)
        {
            _SanSang = true;
            stkCacNuocDaDi = new Stack<OCo>();
            KhoiTaoMangOCo();
            VeBanCo(g);
            KhoiDongComputer(g);

        }
        #region Duyệt Chiến Thắng
        public void KetThucTroChoi()
        {
            switch (_ketthuc)
            {
                case KETTHUC.HoaCo:
                    MessageBox.Show("Hòa");
                    break;
                case KETTHUC.COM:
                    MessageBox.Show("Máy Thắng!");
                    break;
                case KETTHUC.Player:
                    MessageBox.Show("Người chơi thắng!");
                    break;

            }
            _SanSang = false;
        }


        public bool KiemTraChienThang()
        {
            if (stkCacNuocDaDi.Count==_BanCo.SoCot*_BanCo.SoDong)
            {
                _ketthuc = KETTHUC.HoaCo;
                return true;
            }

            foreach(OCo oco in stkCacNuocDaDi)
            {
                if(DuyetDoc(oco.Dong,oco.Cot,oco.SoHuu)||DuyetNgang(oco.Dong,oco.Cot,oco.SoHuu)
                    ||DuyetCheoXuoi(oco.Dong, oco.Cot, oco.SoHuu)||DuyetCheoNguoc(oco.Dong, oco.Cot, oco.SoHuu)
                    )
                {
                    _ketthuc = oco.SoHuu == 1 ? KETTHUC.COM : KETTHUC.Player;
                    return true;
                }

            }
            return false;

        }
        private bool DuyetDoc(int currDong, int currCot,int currSoHuu)
        {
            if (currDong > _BanCo.SoDong - 5)
                return false;
            int Dem;
            for(Dem=1;Dem<5;Dem++)
            {
                if (_MangOCo[currDong + Dem, currCot].SoHuu != currSoHuu)
                    return false;
            }
            if (currDong == 0 || currCot + Dem == _BanCo.SoDong)
                return true;


            if (_MangOCo[currDong - 1, currCot].SoHuu == 0||_MangOCo[currDong+Dem,currCot].SoHuu==0)
                return true;
            return false;
        }


        private bool DuyetNgang(int currDong, int currCot, int currSoHuu)
        {
            if (currCot > _BanCo.SoCot - 5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOCo[currDong , currCot + Dem].SoHuu != currSoHuu)
                    return false;
            }
            if (currDong == 0 || currCot + Dem == _BanCo.SoCot)
                return true;


            if (_MangOCo[currDong , currCot - 1].SoHuu == 0 || _MangOCo[currDong , currCot + Dem].SoHuu == 0)
                return true;
            return false;
        }

        private bool DuyetCheoXuoi(int currDong, int currCot, int currSoHuu)
        {
            if (currDong>_BanCo.SoDong-5|| currCot > _BanCo.SoCot - 5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOCo[currDong+Dem, currCot + Dem].SoHuu != currSoHuu)
                    return false;
            }
            if (currCot==0||currDong+Dem==_BanCo.SoDong||currDong == 0 || currCot + Dem == _BanCo.SoCot)
                return true;


            if (_MangOCo[currDong-1, currCot - 1].SoHuu == 0 || _MangOCo[currDong+Dem , currCot + Dem].SoHuu == 0)
                return true;
            return false;
        }

        private bool DuyetCheoNguoc(int currDong, int currCot, int currSoHuu)
        {
            if (currDong<4 || currCot>_BanCo.SoCot-5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOCo[currDong - Dem, currCot + Dem].SoHuu != currSoHuu)
                    return false;
            }
            if (currDong==4||currDong==_BanCo.SoDong-1||currCot==0||currCot+Dem==_BanCo.SoCot)
                return true;


            if (_MangOCo[currDong +1, currCot - 1].SoHuu == 0 || _MangOCo[currDong - Dem, currCot + Dem].SoHuu == 0)
                return true;
            return false;
        }
        #endregion
        #region AI
        private long[] MangDiemTanCong = new long[7] { 0,9,54,162,1458,13112,118008};
        private long[] MangDiemPhongNgu = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };

        public void KhoiDongComputer(Graphics g)
        {
            if(stkCacNuocDaDi.Count==0)
            {
                DanhCo(_BanCo.SoDong/2*OCo._ChieuCao+1,_BanCo.SoCot/2*OCo._ChieuRong+1,g);

            }
            else
            {
                OCo oco = TimKiemNuocDi();
                DanhCo(oco.ViTri.X + 1, oco.ViTri.Y + 1, g);

            }

        }
        private OCo TimKiemNuocDi()
        {
            OCo oCoResult = new OCo();
            long DiemMax = 0;
            for(int i=0;i<_BanCo.SoDong;i++)
            {
                for(int j=0;j<_BanCo.SoCot;j++)
                {
                    if(_MangOCo[i,j].SoHuu==0)
                    {
                        long DiemTanCong=DiemTanCong_DuyetDoc(i,j)+DiemTanCong_DuyenNgang(i, j) +DiemTanCong_DuyetCheoNguoc(i, j) +DiemTanCong_DuyetCheoXuoi(i, j);
                        long DiemPhongNgu= DiemPhongNgu_DuyetDoc(i,j) + DiemPhongNgu_DuyenNgang(i, j) + DiemPhongNgu_DuyetCheoNguoc(i, j) + DiemPhongNgu_DuyetCheoXuoi(i, j);
                        long DiemTam = DiemTanCong > DiemPhongNgu ? DiemTanCong : DiemPhongNgu;
                        if(DiemMax<DiemTam)
                        {
                            DiemMax = DiemTam;
                            oCoResult = new OCo(_MangOCo[i, j].Dong, _MangOCo[i, j].Cot, _MangOCo[i, j].ViTri, _MangOCo[i, j].SoHuu);

                        }
                    }

                }
            }

            return oCoResult;

        }
        #region tấn công
        private long DiemTanCong_DuyetDoc(int currDong,int currCot)
        {
            long DiemTong = 0;
      
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for(int Dem=1;Dem<6&&currDong+Dem<_BanCo.SoDong;Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong + Dem, currCot].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong + Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currDong - Dem >=0; Dem++)
            {
                if (_MangOCo[currDong - Dem, currCot].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong - Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich+1];       
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }
        private long DiemTanCong_DuyenNgang(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currCot + Dem < _BanCo.SoCot; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong , currCot+Dem].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong , currCot+Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currCot - Dem >= 0; Dem++)
            {
                if (_MangOCo[currDong , currCot - Dem].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong , currCot-Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1];
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }
        private long DiemTanCong_DuyetCheoNguoc(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currCot + Dem < _BanCo.SoCot&&currDong-Dem>=0; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong-Dem, currCot + Dem].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong-Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currCot - Dem >= 0&&currDong+Dem<_BanCo.SoDong; Dem++)
            {
                if (_MangOCo[currDong+Dem, currCot - Dem].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong+Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1];
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }
        private long DiemTanCong_DuyetCheoXuoi(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong + Dem <_BanCo.SoDong; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong + Dem, currCot + Dem].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong + Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currCot - Dem >= 0 && currDong - Dem >=0; Dem++)
            {
                if (_MangOCo[currDong - Dem, currCot - Dem].SoHuu == 1)
                    SoQuanTa++;
                else if (_MangOCo[currDong - Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1];
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }
        #endregion
        #region Phòng ngự
        private long DiemPhongNgu_DuyetDoc(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currDong + Dem < _BanCo.SoDong; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong + Dem, currCot].SoHuu == 1)
                {
                    SoQuanTa++;break;
                }
                else if (_MangOCo[currDong + Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currDong - Dem >= 0; Dem++)
            {
                if (_MangOCo[currDong - Dem, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_MangOCo[currDong - Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            
            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }
        private long DiemPhongNgu_DuyenNgang(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currCot + Dem < _BanCo.SoCot; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong, currCot + Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                        }
                else if (_MangOCo[currDong, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currCot - Dem >= 0; Dem++)
            {
                if (_MangOCo[currDong, currCot - Dem].SoHuu == 1)

                {
                    SoQuanTa++;break;
                }
                else if (_MangOCo[currDong, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    
                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }
        private long DiemPhongNgu_DuyetCheoNguoc(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong - Dem >= 0; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong - Dem, currCot + Dem].SoHuu == 1)
                { SoQuanTa++; break;
                }
                else if (_MangOCo[currDong - Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currCot - Dem >= 0 && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                if (_MangOCo[currDong + Dem, currCot - Dem].SoHuu == 1)
                { SoQuanTa++; break; }
                else if (_MangOCo[currDong + Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }
        private long DiemPhongNgu_DuyetCheoXuoi(int currDong, int currCot)
        {
            long DiemTong = 0;

            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem=1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong + Dem < _BanCo.SoDong; Dem++)//Trách bị tràn mảng
            {
                if (_MangOCo[currDong + Dem, currCot + Dem].SoHuu == 1)
                { SoQuanTa++; break; }
                else if (_MangOCo[currDong + Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }

            for (int Dem=1; Dem < 6 && currCot - Dem >= 0 && currDong - Dem >= 0; Dem++)
            {
                if (_MangOCo[currDong - Dem, currCot - Dem].SoHuu == 1)
                { SoQuanTa++; break; }
                else if (_MangOCo[currDong - Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }

        #endregion
        #endregion
    }

}
