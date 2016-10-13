using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class NHOMMON_
    {
        private string manhom;

        public string MANHOM
        {
            get { return manhom; }
            set { manhom = value; }
        }

        private string tennhom;

        public string TENNHOM
        {
            get { return tennhom; }
            set { tennhom = value; }
        }

        public NHOMMON_(string manhom, string tennhom)
        {
            this.manhom = manhom;
            this.tennhom = tennhom;
        }
        
    }
}
