using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
   public class NHANVIEN_
    {
        private string tennv;

        public string TENNV
        {
            get { return tennv; }
            set { tennv = value; }
        }

        private int manv;

        public int MANV
        {
            get { return manv; }
            set { manv = value; }
        }

        public NHANVIEN_(int manv, string tennv)
        {
            this.manv = manv;
            this.tennv = tennv;
        }
        
        
    }
}
