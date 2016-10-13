using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAL;

namespace BUS
{
    public class MID
    {
        CMDDATA temp = new CMDDATA();

        #region KHUVUC

        public List<KHUVUC_> layKV()
        {
            return temp.layKV();
        }

        public void themKV(KHUVUC_ kv)
        {
            temp.themKV(kv);
        }

        public void suaKV(KHUVUC_ kv)
        {
            temp.suaKV(kv);
        }

        public void xoaKV(string makv)
        {
            temp.xoaKV(makv);
        }

        public List<KHUVUC_> timkiemKV(string tenkv)
        {
            return temp.timkiemKV(tenkv);
        }

        public bool kiemtraTontaiKhuvuc(string makv, string tenkv)
        {
            return temp.kiemtraTontaiKhuvuc(makv, tenkv);
        }

        #endregion

        #region BAN

        public List<BAN_> layBan()
        {
            return temp.layBan();
        }

        public List<BAN_> layBanTrongKV(string tenkv)
        {
            return temp.layBanTrongKV(tenkv);
        }

        public bool kiemtraTontaiBan(string tenban, string makv)
        {
            return temp.kiemtraTontaiBan(tenban, makv);
        }

        public bool kiemtraTontaiBan1(int maban, string tenban, string makv)
        {
            return temp.kiemtraTontaiBan1(maban,tenban, makv);
        }

        public void themBan(BAN_ a)
        {
            temp.themBan(a);
        }

        public void suaBan(BAN_ a)
        {
            temp.suaBan(a);
        }

        public void xoaBan(int maban)
        {
            temp.xoaBan(maban);
        }

        public List<BAN_> timkiemBan(string tenban, string tenkv)
        {
            return temp.timkiemBan(tenban, tenkv);
        }

        #endregion

        #region NHOMMON

        public List<NHOMMON_> layNhomMon()
        {
            return temp.layNhomMon();
        }

        public bool kiemtraTontaiNhommon(string manhom, string tennhom)
        {
            return temp.kiemtraTontaiNhommon(manhom, tennhom);
        }

        public bool kiemtraTontaiNhommon1(string manhom, string tennhom)
        {
            return temp.kiemtraTontaiNhommon1(manhom, tennhom);
        }

        public void themNhomSP(NHOMMON_ temp1)
        {
            temp.themNhomSP(temp1);
        }

        public void suaNhomSP(NHOMMON_ temp1)
        {
            temp.suaNhomSP(temp1);
        }

        public void xoaNhomSP(string manhom)
        {
            temp.xoaNhomSP(manhom);
        }

        public List<NHOMMON_> timkiemNhommon(string tennhom)
        {
            return temp.timkiemNhommon(tennhom);
        }

        #endregion

        #region SANPHAM

        public List<SANPHAM_> laySanPham()
        {
            return temp.laySanPham();
        }

        public List<SANPHAM_> laySanPhamTrongNhom(string tennhom)
        {
            return temp.laySanPhamTrongNhom(tennhom);
        }

        public SANPHAM_ layThongtinSP(string nhom, string tensp)
        {
            return temp.layThongtinSP(nhom, tensp);
        }

        public bool kiemtraSp(string tensp)
        {
            return temp.kiemtraSp(tensp);
        }

        public bool kiemtraSp1(int masp, string tensp)
        {
            return temp.kiemtraSp1(masp, tensp);
        }

        public void themSP(SANPHAM_ a)
        {
            temp.themSP(a);
        }

        public void suaSP(SANPHAM_ a)
        {
            temp.suaSP(a);
        }

        public void xoaSP(int masp)
        {
            temp.xoaSP(masp);
        }

        public List<SANPHAM_> timkiemSP(string tensp, string tennhom)
        {
            return temp.timkiemSP(tensp, tennhom);
        }

        public bool suaTinhtrangSanpham(int masp, bool tinhtrang)
        {
            return temp.suaTinhtrangSanpham(masp, tinhtrang);
        }

        #endregion

        #region NHANVIEN

        public List<NHANVIEN_> layNV()
        {
            return temp.layNV();
        }

        public void themNV(NHANVIEN_ a)
        {
            temp.themNV(a);
        }

        public void suaNV(NHANVIEN_ a)
        {
            temp.suaNV(a);
        }

        public void xoaNV(int manv)
        {
            temp.xoaNV(manv);
        }

        public List<NHANVIEN_> timkiemNV(string tennv)
        {
            return temp.timkiemNV(tennv);
        }

        #endregion

        #region TAI KHOAN

        public List<TAIKHOAN_> layTK()
        {
            return temp.layTK();
        }

        public bool tontaiTaikhoan(string user, string pass, ref NHANVIEN_ a)
        {
            return temp.tontaiTaikhoan(user, pass, ref a);
        }

        public void doiMatkhau(string user, string NewPass)
        {
            temp.doiMatkhau(user, NewPass);
        }

        public bool kiemtraTK(string tennv)
        {
            return temp.kiemtraTK(tennv);
        }

        public void themTK(TAIKHOAN_ a)
        {
            temp.themTK(a);
        }

        public void xoaTK(string user)
        {
            temp.xoaTK(user);
        }

        public List<TAIKHOAN_> layTKCuaNV(string tennv)
        {
            return temp.layTKCuaNV(tennv);
        }

        public bool kiemtraUser(string user)
        {
            return temp.kiemtraUser(user);
        }

        public int getmanvByTennv(string tennv)
        {
            return temp.getmanvByTennv(tennv);
        }

        #endregion

        #region CTHOADON

        public List<CTHOADON_> layCTHD()
        {
            return temp.layCTHD();
        }

        public void themCTHD(CTHOADON_ a)
        {
            temp.themCTHD(a);
        }

        public List<CTHOADON_> layCTHDQuaHD(int maHD)
        {
            return temp.layCTHDQuaHD(maHD);
        }

        #endregion

        #region HOADON

        public List<HOADON_> layHOADON()
        {
            return temp.layHOADON();
        }

        public void themHD(HOADON_ a)
        {
            temp.themHD(a);
        }

        public List<HOADON_> timkiemHD(string mahd)
        {
            return temp.timkiemHD(mahd);
        }

        public int gettopHD()
        {
            return temp.gettopHD();
        }

        #endregion
    }
}
