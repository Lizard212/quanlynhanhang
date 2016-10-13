using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class HOADON_
    {
        private int mahd;

        public int MAHD
        {
            get { return mahd; }
            set { mahd = value; }
        }

        private string  ngaylap;

        public string  NGAYLAP
        {
            get { return ngaylap; }
            set { ngaylap = value; }
        }

        private int slkhach;

        public int SLKHACH
        {
            get { return slkhach; }
            set { slkhach = value; }
        }

        private int phikhac;

        public int PHIKHAC
        {
            get { return phikhac; }
            set { phikhac = value; }
        }

        private string ghichu;

        public string GHICHU
        {
            get { return ghichu; }
            set { ghichu = value; }
        }

        private int manv;

        public int MANV
        {
            get { return manv; }
            set { manv = value; }
        }

        private int maban;

        public int MABAN
        {
            get { return maban; }
            set { maban = value; }
        }

        public HOADON_(int mahd, string ngaylap, int slkhach, int phikhac, string ghichu, int manv, int maban)
        {
            this.mahd = mahd;
            this.ngaylap = ngaylap;
            this.slkhach = slkhach;
            this.phikhac = phikhac;
            this.ghichu = ghichu;
            this.manv = manv;
            this.maban = maban;
        }
        
        
        
        
    }
}
