using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TAIKHOAN_
    {
        private string user;

        public string USER
        {
            get { return user; }
            set { user = value; }
        }

        private string pass;

        public string PASS
        {
            get { return pass; }
            set { pass = value; }
        }

        private int manv;

        public int MANV
        {
            get { return manv; }
            set { manv = value; }
        }

        public TAIKHOAN_(string user, string pass, int manv)
        {
            this.user = user;
            this.pass = pass;
            this.manv = manv;
        }
        
    }
}
