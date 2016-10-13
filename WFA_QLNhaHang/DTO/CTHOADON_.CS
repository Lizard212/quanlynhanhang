using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class CTHOADON_
    {
        private int mahd;

        public int MAHD
        {
            get { return mahd; }
            set { mahd = value; }
        }

        private int masp;

        public int MASP
        {
            get { return masp; }
            set { masp = value; }
        }

        private int soluong;

        public int SOLUONG
        {
            get { return soluong; }
            set { soluong = value; }
        }

        private int gia;

        public int GIA
        {
            get { return gia; }
            set { gia = value; }
        }

        private int giamgia;

        public int GIAMGIA
        {
            get { return giamgia; }
            set { giamgia = value; }
        }

        public CTHOADON_(int mahd, int masp, int soluong, int gia, int giamgia)
        {
            this.mahd = mahd;
            this.masp = masp;
            this.soluong = soluong;
            this.gia = gia;
            this.giamgia = giamgia;
        }
        
        
        
    }
}
