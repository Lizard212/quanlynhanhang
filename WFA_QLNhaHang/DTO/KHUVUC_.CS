using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class KHUVUC_
    {
        private string makv;

        public string MAKV
        {
            get { return makv; }
            set { makv = value; }
        }

        private string  tenkv;

        public string  TENKV
        {
            get { return tenkv; }
            set { tenkv = value; }
        }

        private int cpToithieu;

        public int CPTOITHIEU
        {
            get { return cpToithieu; }
            set { cpToithieu = value; }
        }

        public KHUVUC_(string makv, string tenkv, int cptoithieu)
        {
            this.makv = makv;
            this.tenkv = tenkv;
            this.cpToithieu = cptoithieu;
        }
    }
}
