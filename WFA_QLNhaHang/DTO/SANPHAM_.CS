using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SANPHAM_
    {
        private int masp;

        public int MASP
        {
            get { return masp; }
            set { masp = value; }
        }

        private string tensp;

        public string TENSP
        {
            get { return tensp; }
            set { tensp = value; }
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

        private string  donvi;

        public string  DONVI
        {
            get { return donvi; }
            set { donvi = value; }
        }

        private bool tinhtrang;

        public bool TINHTRANG
        {
            get { return tinhtrang; }
            set { tinhtrang = value; }
        }

        private string manhom;

        public string MANHOM
        {
            get { return manhom; }
            set { manhom = value; }
        }


        public SANPHAM_(int masp, string tensp, int gia, int giamgia, string donvi, bool tinhtrang, string manhom)
        {
            this.masp = masp;
            this.tensp = tensp;
            this.gia = gia;
            this.giamgia = giamgia;
            this.donvi = donvi;
            this.tinhtrang = tinhtrang;
            this.manhom = manhom;
        }
        
        
        
    }
}
