using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class BAN_
    {
        private int maban;

        public int MABAN
        {
            get { return maban; }
            set { maban = value; }
        }

        private string tenban;

        public string TENBAN
        {
            get { return tenban; }
            set { tenban = value; }
        }


        private string makv;

        public string MAKV
        {
            get { return makv; }
            set { makv = value; }
        }

        public BAN_(int maban, string tenban, string makv)
        {
            this.maban = maban;
            this.tenban = tenban;
            this.makv = makv;
        }
        
    }
}
