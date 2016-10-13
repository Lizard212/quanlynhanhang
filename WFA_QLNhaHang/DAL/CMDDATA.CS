using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace DAL
{
    public class CMDDATA
    {
        DBDataContext DB = new DBDataContext();

        #region thao tac KHUVUC

        public  List<KHUVUC_> layKV()
        {
            List<KHUVUC_> a = new List<KHUVUC_>();
            var temp = from p in DB.KHUVUCs select p;
            foreach(KHUVUC i in temp)
            {
                a.Add(new KHUVUC_(i.MAKV,i.TENKV,(int)i.CP_TOITHIEU));
            }
            return a;
        }

        public void themKV(KHUVUC_ kv)
        {
            KHUVUC a = new KHUVUC();
            a.MAKV = kv.MAKV;
            a.TENKV = kv.TENKV;
            a.CP_TOITHIEU = kv.CPTOITHIEU;
            DB.KHUVUCs.InsertOnSubmit(a);
            DB.SubmitChanges();
        }

        public void suaKV(KHUVUC_ kv)
        {
            KHUVUC a = (from p in DB.KHUVUCs where p.MAKV == kv.MAKV select p).FirstOrDefault();
            a.TENKV = kv.TENKV;
            a.CP_TOITHIEU = kv.CPTOITHIEU;
            DB.SubmitChanges();
        }

        public void xoaKV(string makv)
        {
            KHUVUC a = (from p in DB.KHUVUCs where p.MAKV == makv select p).FirstOrDefault();
            DB.KHUVUCs.DeleteOnSubmit(a);
            DB.SubmitChanges();
        }

        public List<KHUVUC_> timkiemKV(string tenkv)
        {
            List<KHUVUC_> a = new List<KHUVUC_>();
            tenkv = string.Format("{0}%",tenkv);
            List<searchKhuvucResult> temp = DB.searchKhuvuc(tenkv).ToList();
            foreach (searchKhuvucResult i in temp)
            {
                a.Add(new KHUVUC_(i.MAKV, i.TENKV, (int)i.CP_TOITHIEU));
            }
            return a;
        }

        public bool kiemtraTontaiKhuvuc(string makv, string tenkv)
        {
            var temp = (from p in DB.KHUVUCs where p.MAKV == makv || p.TENKV==tenkv select p).FirstOrDefault();
            if(temp==null)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region thao tac BAN

        public List<BAN_> layBan()
        {
            List<BAN_> a = new List<BAN_>();
            var temp = from p in DB.BANs select p;
            foreach (BAN i in temp)
            {
                a.Add(new BAN_((int)i.MABAN, i.TENBAN, i.MAKV));
            }
            return a;
        }

        public List<BAN_> layBanTrongKV(string tenkv)
        {
            KHUVUC kv = (from p in DB.KHUVUCs where p.TENKV==tenkv select p).FirstOrDefault();
            var temp = from p in DB.BANs where p.MAKV == kv.MAKV select p;
            List<BAN_> a = new List<BAN_>();
            foreach (BAN i in temp)
            {
                a.Add(new BAN_(i.MABAN, i.TENBAN, i.MAKV));
            }
            return a;
        }

        public bool kiemtraTontaiBan(string tenban, string makv)
        {
            var temp = (from p in DB.BANs where p.TENBAN == tenban && p.MAKV == makv select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public bool kiemtraTontaiBan1(int maban,string tenban, string makv)
        {
            var temp = (from p in DB.BANs where p.TENBAN == tenban && p.MAKV == makv && p.MABAN!=maban select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public void themBan(BAN_ a)
        {
            BAN temp = new BAN();
            temp.TENBAN = a.TENBAN;
            temp.MAKV = a.MAKV;
            DB.BANs.InsertOnSubmit(temp);
            DB.SubmitChanges();
        }

        public void suaBan(BAN_ a)
        {
            BAN temp = (from p in DB.BANs where p.MABAN == a.MABAN select p).SingleOrDefault();
            temp.TENBAN = a.TENBAN;
            DB.SubmitChanges();
        }

        public void xoaBan(int maban)
        {
            BAN a = (from p in DB.BANs where  p.MABAN==maban select p).FirstOrDefault();
            DB.BANs.DeleteOnSubmit(a);
            DB.SubmitChanges();
        }

        public List<BAN_> timkiemBan(string tenban, string tenkv)
        {
            List<BAN_> temp = new List<BAN_>();
            tenban = string.Format("{0}%", tenban);
            List<searchBanResult> a = DB.searchBan(tenban, tenkv).ToList();
            foreach (searchBanResult i in a)
            {
                temp.Add(new BAN_(i.MABAN, i.TENBAN, i.MAKV));
            }
            return temp;
        }

        

        #endregion

        #region thao tac NHOMMON

        public List<NHOMMON_> layNhomMon()
        {
            List<NHOMMON_> a = new List<NHOMMON_>();
            var temp = from p in DB.NHOMSPs select p;
            foreach (NHOMSP i in temp)
            {
                a.Add(new NHOMMON_(i.MANHOM, i.TENNHOM));
            }
            return a;
        }

        public bool kiemtraTontaiNhommon(string manhom, string tennhom)
        {
            var temp = (from p in DB.NHOMSPs where p.MANHOM == manhom || p.TENNHOM == tennhom select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public bool kiemtraTontaiNhommon1(string manhom, string tennhom)
        {
            var temp = (from p in DB.NHOMSPs where p.MANHOM != manhom && p.TENNHOM == tennhom select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public void themNhomSP(NHOMMON_ temp)
        {
            NHOMSP a = new NHOMSP();
            a.MANHOM = temp.MANHOM;
            a.TENNHOM = temp.TENNHOM;
            DB.NHOMSPs.InsertOnSubmit(a);
            DB.SubmitChanges();
        }

        public void suaNhomSP(NHOMMON_ temp)
        {
            NHOMSP a = (from p in DB.NHOMSPs where p.MANHOM == temp.MANHOM select p).FirstOrDefault();
            a.TENNHOM = temp.TENNHOM;
            DB.SubmitChanges();
        }

        public void xoaNhomSP(string manhom)
        {
            NHOMSP a = (from p in DB.NHOMSPs where p.MANHOM == manhom select p).FirstOrDefault();
            DB.NHOMSPs.DeleteOnSubmit(a);
            DB.SubmitChanges();
        }

        public List<NHOMMON_> timkiemNhommon(string tennhom)
        {
            List<NHOMMON_> temp = new List<NHOMMON_>();
            List<searchNhomspResult> a = new List<searchNhomspResult>();
            a = DB.searchNhomsp(string.Format("{0}%", tennhom)).ToList();
            foreach (searchNhomspResult i in a)
            {
                temp.Add(new NHOMMON_(i.MANHOM, i.TENNHOM));
            }
            return temp;
        }

        #endregion

        #region thao tac SANPHAM

        public List<SANPHAM_> laySanPham()
        {
            List<SANPHAM_> a = new List<SANPHAM_>();
            var temp = from p in DB.SANPHAMs select p;
            foreach (SANPHAM i in temp)
            {
                a.Add(new SANPHAM_(i.MASP,i.TENSP,(int)i.GIA,(int)i.GIAMGIA,i.DONVI,(bool)i.TINHTRANG,i.MANHOM));
            }
            return a;
        }

        public List<SANPHAM_> laySanPhamTrongNhom(string tennhom)
        {
            NHOMSP nhomspTemp = (from i in DB.NHOMSPs where i.TENNHOM == tennhom select i).First();
            List<SANPHAM_> sp = new List<SANPHAM_>();
            var tempSP = from i in DB.SANPHAMs where i.MANHOM == nhomspTemp.MANHOM select i;
            foreach (SANPHAM i in tempSP)
            {
                sp.Add(new SANPHAM_(i.MASP, i.TENSP, (int)i.GIA, (int)i.GIAMGIA, i.DONVI, (bool)i.TINHTRANG, i.MANHOM));
            }
            return sp;
        }

        public SANPHAM_ layThongtinSP(string nhom, string tensp)
        {
            NHOMSP tempNhom = (from p in DB.NHOMSPs where p.TENNHOM == nhom select p).First();
            SANPHAM tempSP = (from p in DB.SANPHAMs where p.TENSP == tensp && p.MANHOM == tempNhom.MANHOM select p).First();
            return (new SANPHAM_(tempSP.MASP, tempSP.TENSP, (int)tempSP.GIA, (int)tempSP.GIAMGIA, tempSP.DONVI, (bool)tempSP.TINHTRANG, tempSP.MANHOM));
        }

        public bool kiemtraSp(string tensp)
        {
            var temp = (from p in DB.SANPHAMs where p.TENSP==tensp select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public bool kiemtraSp1(int masp, string tensp)
        {
            var temp = (from p in DB.SANPHAMs where p.MASP != masp && p.TENSP == tensp select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public void themSP(SANPHAM_ a)
        {
            SANPHAM temp = new SANPHAM();
            temp.TENSP = a.TENSP;
            temp.GIA = a.GIA;
            temp.GIAMGIA = a.GIAMGIA;
            temp.DONVI = a.DONVI;
            temp.TINHTRANG = a.TINHTRANG;
            temp.MANHOM = a.MANHOM;
            DB.SANPHAMs.InsertOnSubmit(temp);
            DB.SubmitChanges();
        }

        public void suaSP(SANPHAM_ a)
        {
            SANPHAM temp = (from p in DB.SANPHAMs where p.MASP == a.MASP select p).FirstOrDefault();
            temp.TENSP = a.TENSP;
            temp.GIA = a.GIA;
            temp.GIAMGIA = a.GIAMGIA;
            temp.DONVI = a.DONVI;
            temp.TINHTRANG = a.TINHTRANG;
            temp.MANHOM = a.MANHOM;
            DB.SubmitChanges();
        }

        public void xoaSP(int masp)
        {
            SANPHAM temp = (from p in DB.SANPHAMs where p.MASP == masp select p).FirstOrDefault();
            DB.SANPHAMs.DeleteOnSubmit(temp);
            DB.SubmitChanges();
        }

        public List<SANPHAM_> timkiemSP(string tensp, string tennhom)
        {
            List<SANPHAM_> a = new List<SANPHAM_>();
            List<searchSanphamResult> temp = new List<searchSanphamResult>();
            temp = DB.searchSanpham(string.Format("{0}%", tensp), tennhom).ToList();
            foreach (searchSanphamResult i in temp)
            {
                a.Add(new SANPHAM_(i.MASP,i.TENSP,(int)i.GIA,(int)i.GIAMGIA,i.DONVI,(bool)i.TINHTRANG,i.MANHOM));
            }
            return a;
        }

        public bool suaTinhtrangSanpham(int masp, bool tinhtrang)
        {
            SANPHAM temp = (from i in DB.SANPHAMs where i.MASP == masp select i).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            temp.TINHTRANG = tinhtrang;
            DB.SubmitChanges();
            return true;
        }

        #endregion

        #region thao tac NHANVIEN

        public List<NHANVIEN_> layNV()
        {
            List<NHANVIEN_> a = new List<NHANVIEN_>();
            var temp = from p in DB.NHANVIENs select p;
            foreach (NHANVIEN i in temp)
            {
                a.Add(new NHANVIEN_(i.MANV, i.TENNV));
            }
            return a;
        }

        public void themNV(NHANVIEN_ a)
        {
            NHANVIEN temp = new NHANVIEN();
            temp.TENNV = a.TENNV;
            DB.NHANVIENs.InsertOnSubmit(temp);
            DB.SubmitChanges();
        }

        public void suaNV(NHANVIEN_ a)
        {
            NHANVIEN temp = (from p in DB.NHANVIENs where p.MANV == a.MANV select p).FirstOrDefault();
            temp.TENNV = a.TENNV;
            DB.SubmitChanges();
        }

        public void xoaNV(int manv)
        {
            NHANVIEN a = (from p in DB.NHANVIENs where p.MANV == manv select p).FirstOrDefault();
            DB.NHANVIENs.DeleteOnSubmit(a);
            DB.SubmitChanges();
        }

        public List<NHANVIEN_> timkiemNV(string tennv)
        {
            List<searchNVResult> temp = new List<searchNVResult>();
            List<NHANVIEN_> temp1 = new List<NHANVIEN_>();
            temp = DB.searchNV(string.Format("{0}%", tennv)).ToList();
            foreach (searchNVResult i in temp)
            {
                temp1.Add(new NHANVIEN_(i.MANV, i.TENNV));
            }
            return temp1;
        }

        #endregion

        #region thao tac TAIKHOAN

        public List<TAIKHOAN_> layTK()
        {
            var temp = from p in DB.TAIKHOANs select p;
            List<TAIKHOAN_> a = new List<TAIKHOAN_>();
            foreach (TAIKHOAN i in temp)
            {
                a.Add(new TAIKHOAN_(i.USERS, i.PASS, (int)i.MANV));
            }
            return a;
        }

        public bool tontaiTaikhoan(string user, string pass, ref NHANVIEN_ a)
        {
            var temp = (from p in DB.TAIKHOANs where p.USERS == user && p.PASS == pass select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            NHANVIEN nvTemp = (from p in DB.NHANVIENs where p.MANV == temp.MANV select p).First();
            a.MANV = nvTemp.MANV;
            a.TENNV = nvTemp.TENNV;
            return true;
        }

        public void doiMatkhau(string user, string NewPass)
        {
            TAIKHOAN tk = DB.TAIKHOANs.Single(p => p.USERS == user);
            tk.PASS = NewPass;
            DB.SubmitChanges();
            
        }

        public bool kiemtraTK(string tennv)
        {
            NHANVIEN nv = (from p in DB.NHANVIENs where p.TENNV == tennv select p).FirstOrDefault();
            var temp = (from p in DB.TAIKHOANs where p.MANV == nv.MANV select p).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        public void themTK(TAIKHOAN_ a)
        {
            TAIKHOAN temp = new TAIKHOAN();
            temp.USERS = a.USER;
            temp.PASS = a.PASS;
            temp.MANV = a.MANV;
            DB.TAIKHOANs.InsertOnSubmit(temp);
            DB.SubmitChanges();
        }

        public void xoaTK(string user)
        {
            TAIKHOAN tk = (from p in DB.TAIKHOANs where p.USERS == user select p).FirstOrDefault();
            DB.TAIKHOANs.DeleteOnSubmit(tk);
            DB.SubmitChanges();
        }

        public List<TAIKHOAN_> layTKCuaNV(string tennv)
        {
            List<TAIKHOAN_> rt = new List<TAIKHOAN_>();
            NHANVIEN a = (from p in DB.NHANVIENs where p.TENNV == tennv select p).FirstOrDefault();
            var temp  = from p in DB.TAIKHOANs where p.MANV==a.MANV select p;
            foreach (TAIKHOAN i in temp)
            {
                rt.Add(new TAIKHOAN_(i.USERS, i.PASS, (int)i.MANV));
            }
            return rt;
        }

        public bool kiemtraUser(string user)
        {
            var t = (from p in DB.TAIKHOANs where p.USERS == user select p).FirstOrDefault();
            if (t == null)
            {
                return false;
            }
            return true;
        }

        public int getmanvByTennv(string tennv)
        {
            int a = 0;
            a = (from p in DB.NHANVIENs where p.TENNV == tennv select p.MANV).FirstOrDefault();
            return a;
        }

        #endregion

        #region thao tac CTHOADON

        public List<CTHOADON_> layCTHD()
        {
            List<CTHOADON_> a = new List<CTHOADON_>();
            var temp = from p in DB.CTHOADONs select p;
            foreach (CTHOADON i in temp)
            {
                a.Add(new CTHOADON_(i.MAHD, i.MASP, (int)i.SOLUONG, (int)i.GIA, (int)i.GIAMGIA));
            }
            return a;
        }

        public void themCTHD(CTHOADON_ a)
        {
            CTHOADON temp = new CTHOADON();
            temp.MAHD = a.MAHD;
            temp.MASP = a.MASP;
            temp.SOLUONG = a.SOLUONG;
            temp.GIA = a.GIA;
            temp.GIAMGIA = a.GIAMGIA;
            DB.CTHOADONs.InsertOnSubmit(temp);
            DB.SubmitChanges();
        }

        public List<CTHOADON_> layCTHDQuaHD(int maHD)
        {
            List<CTHOADON_> a = new List<CTHOADON_>();
            var temp = from p in DB.CTHOADONs where p.MAHD==maHD select p;
            foreach (CTHOADON i in temp)
            {
                a.Add(new CTHOADON_(i.MAHD,(int)i.MASP,(int)i.SOLUONG,(int)i.GIA,(int)i.GIAMGIA));
            }
            return a;
        }

        #endregion

        #region thao tac HOADON

        public List<HOADON_> layHOADON()
        {
            List<HOADON_> a = new List<HOADON_>();
            var temp = from p in DB.HOADONs select p;
            foreach (HOADON i in temp)
            {
                a.Add(new HOADON_(i.MAHD, i.NGAYLAP.ToString(), (int)i.SLKHACH, (int)i.PHIKHAC, i.GHICHU, (int)i.MANV, (int)i.MABAN));
            }
            return a;
        }

        public void themHD(HOADON_ a)
        {
            HOADON temp = new HOADON();
            temp.NGAYLAP = Convert.ToDateTime(a.NGAYLAP);
            temp.SLKHACH = a.SLKHACH;
            temp.PHIKHAC = a.PHIKHAC;
            temp.GHICHU = a.GHICHU;
            temp.MANV = a.MANV;
            temp.MABAN = a.MABAN;
            DB.HOADONs.InsertOnSubmit(temp);
            DB.SubmitChanges();
        }

        public List<HOADON_> timkiemHD(string mahd)
        {
            List<HOADON_> a = new List<HOADON_>();
            List<searchHDResult> temp = new List<searchHDResult>();
            temp = DB.searchHD(string.Format("{0}%", mahd)).ToList();
            foreach (searchHDResult i in temp)
            {
                a.Add(new HOADON_(i.MAHD, i.NGAYLAP.ToString(), (int)i.SLKHACH, (int)i.PHIKHAC, i.GHICHU, (int)i.MANV, (int)i.MABAN));
            }
            return a;
        }

        public int gettopHD()
        {
            int top = DB.HOADONs.Max(n => n.MAHD);
            return top;
        }

        #endregion
    }
}
