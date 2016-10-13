using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BUS;

namespace WFA_QLNhaHang
{
    public partial class Form1 : Form
    {
        #region khai bao BIEN

        MID mid = new MID();
        string selectedKV = "", selectedBan = "";

        //KHAI BAO BIEN LUU DU LIEU CHO DATAGRIDVIEW
        List<List<string>> thucdon_listVitri = new List<List<string>>();
        List<List<List<string>>> thucdon_listDulieu = new List<List<List<string>>>();

        //khai báo biến lưu trữ món ăn đã chọn
        string selectedNhom = "", selectedSanpham = "";

        #endregion

        #region thao tac FORM

        public Form1()
        {
            InitializeComponent();

        }

        protected void Form1_Load(object sender, EventArgs e)
        {
            //set thoi gian
            lbNgay.Text = string.Format("{0}-{1}-{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            //thao tac KHU VUC
            getDataKV();
            getDataBan();

            //thao tac SANPHAM
            getDataNhomSP();
            getDataSanPham();

            //hide cac tabpages
            tcForm.TabPages.Remove(tabPage2);
            tcForm.TabPages.Remove(tabPage5);
            tcForm.TabPages.Remove(tabPage6);
            tcForm.TabPages.Remove(tabPage7);

            //thao tac voi QLNH_KHUVUC_khuvuc
            layKhuvuc();

            //thao tac QLNH_NHOMSP
            showDulieuNhomspQLNH();

            //thao tac QLNH_NHANVIEN
            showDulieuNV();

            //XEM HOA DON
            timkiemHD("");

            //set ActiveControl
            this.ActiveControl = dangnhap_txtUser;
        }

        #endregion

        #region DANGNHAP

        private void sosanhDangnhap()
        {
            NHANVIEN_ a = new NHANVIEN_(0, "0");
            if (mid.tontaiTaikhoan(dangnhap_txtUser.Text, dangnhap_txtPassword.Text, ref a))
            {
                //hien thi len form1
                lbFormManv.Text = a.MANV.ToString();
                lbFormTennv.Text = a.TENNV;

                //dang nhap
                tcForm.TabPages.Remove(tabPage1);
                tcForm.TabPages.Insert(0, tabPage2);
                tcForm.TabPages.Insert(1, tabPage5);
                if (dangnhap_txtUser.Text.ToUpper() == "ADMIN")
                {
                    tcForm.TabPages.Insert(2, tabPage6);
                    tcForm.TabPages.Insert(3, tabPage7);
                }
                else
                {
                    tcForm.TabPages.Insert(2, tabPage7);
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công !!!");
            }
        }

        private void dangnhap_btnLogin_Click(object sender, EventArgs e)
        {
            if (dangnhap_txtUser.Text == "" || dangnhap_txtPassword.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ mật khẩu và tài khoản !!!");
                return;
            }
            sosanhDangnhap();
        }

        private void dangnhap_btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        #endregion

        #region TRANGCHU

        #region THUCDON

        private void thucdon_themDulieu()
        {
            thucdon_listVitri.Add(new List<string>());
            thucdon_listVitri[thucdon_listVitri.Count - 1].Add(lbKhuvuc.Text);
            thucdon_listVitri[thucdon_listVitri.Count - 1].Add(lbBan.Text);
            thucdon_listDulieu.Add(new List<List<string>>());
        }

        private void thucdon_xoaDulieu()
        {
            int vitri = 0;
            coThucdonHaykhong(ref vitri);
            thucdon_listVitri.RemoveAt(vitri);
            thucdon_listDulieu.RemoveAt(vitri);
        }

        private bool coThucdonHaykhong(ref int vitri)
        {
            for (int i = 0; i < thucdon_listVitri.Count; i++)
            {
                if (thucdon_listVitri[i][0] == lbKhuvuc.Text && thucdon_listVitri[i][1] == lbBan.Text)
                {
                    vitri = i;
                    return true;
                }
            }
            return false;
        }

        private void saveThucdon()
        {
            int vitriTemp = 0;
            if (coThucdonHaykhong(ref vitriTemp))
            {
                thucdon_listDulieu[vitriTemp].Clear();
                for (int i = 0; i < dgvThucdon.RowCount; i++)
                {
                    thucdon_listDulieu[vitriTemp].Add(new List<string>());
                    for (int j = 0; j < dgvThucdon.ColumnCount; j++)
                    {
                        thucdon_listDulieu[vitriTemp][i].Add(dgvThucdon[j, i].Value.ToString());
                    }
                }
            }
        }

        private void hienthiThucDon()
        {

            int vitriTemp = 0;
            if (coThucdonHaykhong(ref vitriTemp))
            {
                dgvThucdon.Rows.Clear();
                dgvThucdon.DataSource = null;
                for (int i = 0; i < thucdon_listDulieu[vitriTemp].Count; i++)
                {
                    dgvThucdon.Rows.Add(thucdon_listDulieu[vitriTemp][i].ToArray());
                }

            }
            else
            {
                dgvThucdon.DataSource = null;
                dgvThucdon.Rows.Clear();
            }
        }

        private bool coTrongThucdon(SANPHAM_ a)
        {
            for (int i = 0; i < dgvThucdon.RowCount; i++)
            {

                if (dgvThucdon[0, i].Value.ToString() == a.MASP.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnThemsp_Click(object sender, EventArgs e)
        {
            if (selectedSanpham == "")
            {
                MessageBox.Show("Bạn hãy chọn sản phẩm để thêm vào");
                return;
            }
            if (selectedKV == "")
            {
                MessageBox.Show("Bạn hãy chọn bàn để thêm sản phẩm yêu cầu vào thực đơn");
                return;
            }
            if (!banConguoiHaychua())
            {
                MessageBox.Show("Bạn không thể thêm sản phẩm vào bàn không có người !!!");
                return;
            }
            if (lbSelectedSP.Text[0] == '*')
            {
                MessageBox.Show("Sản phẩm đã hết");
                return;
            }
            string thongbao = string.Format("Bạn có muốn thêm sản phẩm {0} thuộc nhóm {1} vào trong thực đơn của {2} thuộc khu vực {3} không ?", selectedSanpham, selectedNhom, selectedBan, selectedKV);
            if (DialogResult.OK == MessageBox.Show(thongbao, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                SANPHAM_ tempSP = mid.layThongtinSP(selectedNhom, selectedSanpham);
                if (coTrongThucdon(tempSP))
                {
                    for (int i = 0; i < dgvThucdon.RowCount; i++)
                    {
                        if (dgvThucdon[0, i].Value.ToString() == tempSP.MASP.ToString())
                        {
                            dgvThucdon[4, i].Value = int.Parse(dgvThucdon[4, i].Value.ToString()) + 1;
                            dgvThucdon[5, i].Value = (int.Parse(dgvThucdon[2, i].Value.ToString()) * int.Parse(dgvThucdon[4, i].Value.ToString()) * (100 - int.Parse(dgvThucdon[3, i].Value.ToString()))) / 100;
                            break;
                        }
                    }
                }
                else
                {
                    dgvThucdon.Rows.Add(tempSP.MASP, tempSP.TENSP, tempSP.GIA, tempSP.GIAMGIA, 1, (tempSP.GIA * (100 - tempSP.GIAMGIA)) / 100);
                }
            }

            //set tongchiphi hien tai
            setTongchiphi();
        }

        private void btnXoasp_Click(object sender, EventArgs e)
        {
            if (dgvThucdon.RowCount == 0)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm để xóa");
                return;
            }
            int a = dgvThucdon.CurrentRow.Index;
            string thongbao = string.Format("Bạn có muốn xóa sản phẩm {0} khỏi thực đơn của {1} khu vực {2} không ?", dgvThucdon[1, a].Value.ToString(), lbBan.Text, lbKhuvuc.Text);
            if (DialogResult.OK == MessageBox.Show(thongbao, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                dgvThucdon.Rows.RemoveAt(a);
                MessageBox.Show("Xóa sản phẩm thành công !!!");
            }

            //set tongchiphi hien tai
            setTongchiphi();
        }

        private void btnXoatatca_Click(object sender, EventArgs e)
        {
            string thongbao = string.Format("Bạn có muốn xóa tất cả sản phẩm khỏi thực đơn của {0} khu vực {1} không ?", lbBan.Text, lbKhuvuc.Text);
            if (DialogResult.OK == MessageBox.Show(thongbao, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                dgvThucdon.Rows.Clear();
                MessageBox.Show("Xóa sản phẩm thành công !!!");
            }

            //set tongchiphi hien tai
            setTongchiphi();
        }

        #endregion

        #region KHUVUC

        private void getDataKV()
        {
            //cai dat cac tabpage KHUVUC
            tcKV.TabPages.Clear();
            List<KHUVUC_> kv = new List<KHUVUC_>();
            kv = mid.layKV();
            foreach (KHUVUC_ i in kv)
            {
                TabPage tp = new TabPage(i.TENKV);
                tcKV.TabPages.Add(tp);
            }
        }

        private void getDataBan()
        {
            //them cac BAN vao KHUVUC
            for (int i = 0; i < tcKV.TabCount; i++)
            {
                //them mot Flow Layout Panel vao
                FlowLayoutPanel flp = new FlowLayoutPanel();
                tcKV.TabPages[i].Controls.Add(flp);
                flp.Dock = DockStyle.Bottom;
                flp.Height = tcKV.TabPages[i].Height - 20;
                flp.AutoScroll = true;
                tcKV.TabPages[i].BackColor = Color.White;

                //
                List<BAN_> ban = new List<BAN_>();
                ban = mid.layBanTrongKV(tcKV.TabPages[i].Text);
                foreach (BAN_ bantemp in ban)
                {
                    //tao BUTTON va giao dien
                    Button btnTemp = new Button();
                    btnTemp.Size = new Size(60, 20);
                    btnTemp.Text = bantemp.TENBAN;
                    toolTip1.SetToolTip(btnTemp, btnTemp.Text);
                    btnTemp.BackColor = Color.Gainsboro;
                    btnTemp.FlatStyle = FlatStyle.Popup;
                    //them su kien
                    btnTemp.Click += ButtonBan_Click;
                    //them vao flowlayout
                    flp.Controls.Add(btnTemp);
                }
            }
        }

        private void ButtonBan_Click(object sender, EventArgs e)
        {
            //save thuc don ban cu
            if (lbBan.Text != "" && lbKhuvuc.Text != "")
                saveThucdon();

            //set cho label hien thi Khuvuc va Ban
            Button a = new Button();
            a = (Button)sender;
            lbKhuvuc.Text = tcKV.SelectedTab.Text;
            lbBan.Text = a.Text;

            //set cho selectedString
            selectedKV = lbKhuvuc.Text;
            selectedBan = lbBan.Text;

            //hien thi thuc don ban hien tai
            hienthiThucDon();
            //hien thi tongchiphi hien tai
            setTongchiphi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbKhuvuc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bàn");
                return;
            }
            string thongbao1 = string.Format("Bạn có chắc {0} thuộc khu vực {1} là có người ngồi", selectedBan, selectedKV);
            string thongbao2 = string.Format("Bạn có chắc {0} thuộc khu vực {1} không có người ngồi", selectedBan, selectedKV);

            foreach (TabPage tp in tcKV.TabPages)
            {
                if (tp.Text == lbKhuvuc.Text)
                {
                    foreach (Control ctl in tp.Controls)
                    {
                        foreach (Control c in ctl.Controls)
                        {
                            if (c.Text == lbBan.Text)
                            {
                                if (c.BackColor == Color.Gainsboro)
                                {
                                    if (MessageBox.Show(thongbao1, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                    {
                                        c.BackColor = Color.Purple;
                                        c.ForeColor = Color.White;
                                        thucdon_themDulieu();
                                    }
                                }
                                else
                                {
                                    if (MessageBox.Show(thongbao2, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                    {
                                        c.BackColor = Color.Gainsboro;
                                        c.ForeColor = Color.Black;
                                        thucdon_xoaDulieu();
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }



        #endregion

        #region NHOM SANPHAM

        private void getDataNhomSP()
        {
            tcNhomsp.TabPages.Clear();
            List<NHOMMON_> nhommonTemp = new List<NHOMMON_>();
            nhommonTemp = mid.layNhomMon();
            foreach (NHOMMON_ i in nhommonTemp)
            {
                TabPage tp = new TabPage(i.TENNHOM);
                tcNhomsp.TabPages.Add(tp);
            }
        }

        private void getDataSanPham()
        {
            for (int i = 0; i < tcNhomsp.TabCount; i++)
            {
                tcNhomsp.TabPages[i].BackColor = Color.White;
                //them mot flow panel layout vao
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Bottom;
                flp.Height = tcNhomsp.TabPages[i].Height - 15;
                flp.AutoScroll = true;
                tcNhomsp.TabPages[i].Controls.Add(flp);

                //them cac button
                List<SANPHAM_> spTemp = mid.laySanPhamTrongNhom(tcNhomsp.TabPages[i].Text);
                foreach (SANPHAM_ sp in spTemp)
                {
                    //cai dat giao dien BUTTON
                    Button a = new Button();
                    a.Size = new Size(100, 30);
                    a.Text = sp.TENSP;
                    a.FlatStyle = FlatStyle.Popup;
                    a.MouseEnter += (s, e) => a.Cursor = Cursors.Hand;
                    a.MouseLeave += (s, e) => a.Cursor = Cursors.Arrow;
                    a.MouseEnter += BtnTrangchu_MouseEnter;
                    a.MouseLeave += BtnTrangchu_MoveLeave;
                    toolTip1.SetToolTip(a, a.Text);
                    if (sp.TINHTRANG == true)
                    {
                        a.ForeColor = Color.White;
                        a.BackColor = Color.Black;
                    }
                    else
                    {
                        a.BackColor = Color.Black;
                        a.ForeColor = Color.Gray;
                    }

                    //them su kien BUTTON
                    a.Click += ButtonSanpham_Click;

                    //add button
                    flp.Controls.Add(a);
                }
            }
        }

        private void BtnTrangchu_MouseEnter(object sender, EventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            float d = a.Font.Size+1;
            a.Font = new Font("Microsoft Sans Serif",d);
        }

        private void BtnTrangchu_MoveLeave(object sender, EventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            float d = a.Font.Size - 1;
            a.Font = new Font("Microsoft Sans Serif", d);
        }

        private List<NHOMMON_> laydulieuNhomsp()
        {
            List<NHOMMON_> temp = new List<NHOMMON_>();
            temp = mid.layNhomMon();
            return temp;
        }

        private void ButtonSanpham_Click(object sender, EventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            selectedNhom = tcNhomsp.SelectedTab.Text;
            selectedSanpham = a.Text;
            if (a.ForeColor == Color.Gray)
            {
                lbSelectedSP.Text = string.Format("*{0}", selectedSanpham);
            }
            else
            {
                lbSelectedSP.Text = selectedSanpham;
            }
        }

        private void btnCaidatSanpham_Click(object sender, EventArgs e)
        {
            if (lbSelectedSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm");
                return;
            }
            foreach (TabPage tp in tcNhomsp.TabPages)
            {
                if (tp.Text == selectedNhom)
                {
                    foreach (Control ctn in tp.Controls)
                    {
                        foreach (Control c in ctn.Controls)
                        {
                            if (c.Text == selectedSanpham)
                            {
                                if (lbSelectedSP.Text[0] == '*')
                                {
                                    if (DialogResult.OK == MessageBox.Show(string.Format("Bạn có chắc sản phẩm {0} đã có", selectedSanpham), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                                    {
                                        mid.suaTinhtrangSanpham(mid.layThongtinSP(selectedNhom, selectedSanpham).MASP, true);
                                        lbSelectedSP.Text = lbSelectedSP.Text.Substring(1, lbSelectedSP.Text.Length - 1);
                                        c.ForeColor = Color.White;
                                    }
                                }
                                else
                                {
                                    if (DialogResult.OK == MessageBox.Show(string.Format("Bạn có chắc sản phẩm {0} đã hết", selectedSanpham), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                                    {
                                        mid.suaTinhtrangSanpham(mid.layThongtinSP(selectedNhom, selectedSanpham).MASP, false);
                                        lbSelectedSP.Text = string.Format("*{0}", lbSelectedSP.Text);
                                        c.ForeColor = Color.Gray;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region THANHTOAN

        private void setTongchiphi()
        {
            int tongchiphi = 0;
            for (int i = 0; i < dgvThucdon.RowCount; i++)
            {
                tongchiphi += int.Parse(dgvThucdon[5, i].Value.ToString());
            }
            lbTongchiphi.Text = tongchiphi.ToString();
        }

        private void themHD()
        {
            int sl = int.Parse(Thanhtoan_txtSonguoi.Text);
            string ghichu = Thanhtoan_txtGhichu.Text;
            //lay phu phi
            List<KHUVUC_> listKV = new List<KHUVUC_>();
            listKV = mid.timkiemKV(lbKhuvuc.Text);
            int phuphi = listKV[0].CPTOITHIEU;
            //lay ma ban
            List<BAN_> listBan = new List<BAN_>();
            listBan = mid.timkiemBan(lbBan.Text, lbKhuvuc.Text);
            int maban = listBan[0].MABAN;
            //
            int manv = int.Parse(lbFormManv.Text);
            //
            string ngaylap = string.Format("{0}-{1}-{2} {3}:{4}:{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //
            HOADON_ a = new HOADON_(1, ngaylap, sl, phuphi, ghichu, manv, maban);
            mid.themHD(a);
        }

        private void themCTHD()
        {
            int mahd = mid.gettopHD();
            for (int i = 0; i < dgvThucdon.RowCount; i++)
            {
                mid.themCTHD(new CTHOADON_(mahd, int.Parse(dgvThucdon[0, i].Value.ToString()), int.Parse(dgvThucdon[4, i].Value.ToString()), int.Parse(dgvThucdon[2, i].Value.ToString()), int.Parse(dgvThucdon[3, i].Value.ToString())));
            }
        }

        private void Thanhtoan_btnThanhtoan_Click(object sender, EventArgs e)
        {
            if (dgvThucdon.RowCount == 0)
            {
                MessageBox.Show("Bàn này chưa dùng sản phẩm nào");
                return;
            }
            if (Thanhtoan_txtSonguoi.Text == "")
            {
                MessageBox.Show("Bạn chưa điền số người");
                return;
            }
            if (int.Parse(Thanhtoan_txtKhachdua.Text) < int.Parse(lbTongchiphi.Text))
            {
                MessageBox.Show("Khách đưa chưa đủ tiền");
                return;
            }
            else
            {
                MessageBox.Show(string.Format("Bạn cần trả lại khách {0}", int.Parse(Thanhtoan_txtKhachdua.Text) - int.Parse(lbTongchiphi.Text)));
                lbTienthua.Text = (int.Parse(Thanhtoan_txtKhachdua.Text) - int.Parse(lbTongchiphi.Text)).ToString();
            }
            themHD();
            themCTHD();
            MessageBox.Show("Thanh toán thành công");

            //resetBansauThanhtoan
            resetBansauThanhtoan();

            //reset cac control
            lbTongchiphi.Text = "0";
            Thanhtoan_txtSonguoi.Text = "";
            Thanhtoan_txtGhichu.Text = "";
            Thanhtoan_txtKhachdua.Text = "0";
            lbTienthua.Text = "0";
        }

        private void resetBansauThanhtoan()
        {
            //reset mau sac
            foreach (TabPage tp in tcKV.TabPages)
            {
                if (tp.Text == lbKhuvuc.Text)
                {
                    foreach (Control ctn in tp.Controls)
                    {
                        foreach (Control c in ctn.Controls)
                        {
                            if (c.Text == lbBan.Text)
                            {
                                c.BackColor = Color.Gainsboro;
                                c.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }

            //xoa du lieu ve no
            int vitri = 0;
            for (int i = 0; i < thucdon_listVitri.Count; i++)
            {
                if (thucdon_listVitri[i][0] == lbKhuvuc.Text && thucdon_listVitri[i][1] == lbBan.Text)
                {
                    vitri = i;
                    break;
                }
            }
            thucdon_listVitri.RemoveAt(vitri);
            thucdon_listDulieu.RemoveAt(vitri);

            //reset datagridview
            dgvThucdon.DataSource = null;
            dgvThucdon.Rows.Clear();
        }

        private bool banConguoiHaychua()
        {
            foreach (TabPage tp in tcKV.TabPages)
            {
                if (tp.Text == selectedKV)
                {
                    foreach (Control ctn in tp.Controls)
                    {
                        foreach (Control c in ctn.Controls)
                        {
                            if (c.Text == selectedBan)
                            {
                                if (c.BackColor == Color.Purple)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #endregion

        #region DOI MAT KHAU

        private void doiMatkhau()
        {
            NHANVIEN_ a = new NHANVIEN_(1, "2");
            if (mid.tontaiTaikhoan(doimatkhau_txtUser.Text, doimatkhau_txtOldPass.Text, ref a))
            {
                mid.doiMatkhau(doimatkhau_txtUser.Text, doimatkhau_txtNewPass.Text);
                MessageBox.Show("Đổi mật khẩu thành công");
                doimatkhau_txtUser.Text = "";
                doimatkhau_txtOldPass.Text = "";
                doimatkhau_txtNewPass.Text = "";
                doimatkhau_txtRetypeNewPass.Text = "";
            }
            else
            {
                MessageBox.Show("USER hoặc PASSWORD sai");
            }
        }

        private void doimatkhau_btnDoimk_Click(object sender, EventArgs e)
        {
            if (doimatkhau_txtUser.Text == "" || doimatkhau_txtOldPass.Text == "" || doimatkhau_txtNewPass.Text == "" || doimatkhau_txtRetypeNewPass.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin");
                return;
            }
            doiMatkhau();
        }

        #endregion

        #region QL NHAHANG

        #region phan KHUVUC

        #region khu vuc

        private void layKhuvuc()
        {
            dgv_QLNH_KHUVUC_khuvuc.Rows.Clear();
            dgv_QLNH_KHUVUC_khuvuc.DataSource = null;
            List<KHUVUC_> listKhuvuc = new List<KHUVUC_>();
            listKhuvuc = mid.layKV();
            foreach (KHUVUC_ a in listKhuvuc)
            {
                dgv_QLNH_KHUVUC_khuvuc.Rows.Add(a.MAKV, a.TENKV, a.CPTOITHIEU);
            }
        }

        public void dgv_QLNH_KHUVUC_khuvuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            //-----------------set cac textbox trong QLNH_KHUVUC_khuvuc-------------------------
            //
            //tabpage SUA
            QLNH_KV_SUA_lbMakv.Text = dgv_QLNH_KHUVUC_khuvuc[0, e.RowIndex].Value.ToString();
            QLNH_KV_SUA_txtTenkv.Text = dgv_QLNH_KHUVUC_khuvuc[1, e.RowIndex].Value.ToString();
            QLNH_KV_SUA_txtCPToithieu.Text = dgv_QLNH_KHUVUC_khuvuc[2, e.RowIndex].Value.ToString();
            //tabpage XOA
            QLNH_KV_XOA_lbMakv.Text = dgv_QLNH_KHUVUC_khuvuc[0, e.RowIndex].Value.ToString();

            //-----------------hien thi cac ban trong-------------------------------------------
            hienthiBan(dgv_QLNH_KHUVUC_khuvuc[1, e.RowIndex].Value.ToString());

            //------------------SET CAC TEXTBOX BEN BAN-----------------------------------------
            QLNH_BAN_THEM_lbMakv.Text = dgv_QLNH_KHUVUC_khuvuc[0, e.RowIndex].Value.ToString();
            QLNH_BAN_SUA_lbMakv.Text = dgv_QLNH_KHUVUC_khuvuc[0, e.RowIndex].Value.ToString();
            QLNH_KHUVUC_ban_txtTimkiem.Text = "";

            //set label selected ten khu vuc
            lbSelectedTenkv.Text = dgv_QLNH_KHUVUC_khuvuc[1, e.RowIndex].Value.ToString();
        }

        public void QLNH_KV_THEM_btnThem_Click(object sender, EventArgs e)
        {

            if (mid.kiemtraTontaiKhuvuc(QLNH_KV_THEM_txtMakv.Text, QLNH_KV_THEM_txtTenkv.Text))
            {
                MessageBox.Show("Mã khu vực hoặc tên khu vực đã tồn tại !!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KHUVUC_ a = new KHUVUC_(QLNH_KV_THEM_txtMakv.Text, QLNH_KV_THEM_txtTenkv.Text, int.Parse(QLNH_KV_THEM_txtCPToithieu.Text));
            mid.themKV(a);
            MessageBox.Show("Thêm thành công");

            //reset lai datagridview
            layKhuvuc();
            //reset lai cac textBox
            QLNH_KV_THEM_txtMakv.Text = "";
            QLNH_KV_THEM_txtTenkv.Text = "";
            QLNH_KV_THEM_txtCPToithieu.Text = "";
            QLNH_KHUVUC_khuvuc_txtTimkiem.Text = "";
        }

        public void QLNH_KV_SUA_btnSua_Click(object sender, EventArgs e)
        {
            if (QLNH_KV_SUA_lbMakv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khu vực cần sửa");
                return;
            }
            KHUVUC_ a = new KHUVUC_(QLNH_KV_SUA_lbMakv.Text, QLNH_KV_SUA_txtTenkv.Text, int.Parse(QLNH_KV_SUA_txtCPToithieu.Text));
            mid.suaKV(a);
            MessageBox.Show("Sửa thành công");

            //reset datagridview
            layKhuvuc();
            //reset cac control
            QLNH_KV_XOA_lbMakv.Text = "";
            QLNH_KV_SUA_lbMakv.Text = "";
            QLNH_KV_SUA_txtTenkv.Text = "";
            QLNH_KV_SUA_txtCPToithieu.Text = "";
            QLNH_KHUVUC_khuvuc_txtTimkiem.Text = "";
        }

        public void QLNH_KV_XOA_btnXoa_Click(object sender, EventArgs e)
        {
            if (QLNH_KV_XOA_lbMakv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Khu vực để xóa");
                return;
            }
            mid.xoaKV(QLNH_KV_XOA_lbMakv.Text);
            MessageBox.Show("Xóa thành công");
            //reset datagridview
            layKhuvuc();
            //reset control
            QLNH_KV_XOA_lbMakv.Text = "";
            QLNH_KV_SUA_lbMakv.Text = "";
            QLNH_KV_SUA_txtTenkv.Text = "";
            QLNH_KV_SUA_txtCPToithieu.Text = "";
            QLNH_KHUVUC_khuvuc_txtTimkiem.Text = "";
        }

        public void QLNH_KHUVUC_khuvuc_txtTimkiem_KeyUp(object sender, KeyEventArgs e)
        {
            dgv_QLNH_KHUVUC_khuvuc.Rows.Clear();
            dgv_QLNH_KHUVUC_khuvuc.DataSource = null;
            List<KHUVUC_> temp = mid.timkiemKV(QLNH_KHUVUC_khuvuc_txtTimkiem.Text);
            foreach (KHUVUC_ i in temp)
            {
                dgv_QLNH_KHUVUC_khuvuc.Rows.Add(i.MAKV, i.TENKV, i.CPTOITHIEU);
            }
        }

        public void dgv_QLNH_KHUVUC_ban_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //phan sua ban
            QLNH_BAN_SUA_lbMaban.Text = dgv_QLNH_KHUVUC_ban[0, e.RowIndex].Value.ToString();
            QLNH_BAN_SUA_txtTenban.Text = dgv_QLNH_KHUVUC_ban[1, e.RowIndex].Value.ToString();

            //phan xoa ban
            QLNH_BAN_XOA_lbMaban.Text = dgv_QLNH_KHUVUC_ban[0, e.RowIndex].Value.ToString();
        }

        #endregion

        #region BAN

        private void hienthiBan(string tenKV)
        {
            List<BAN_> tempBan = new List<BAN_>();
            tempBan = mid.layBanTrongKV(tenKV);
            dgv_QLNH_KHUVUC_ban.Rows.Clear();
            dgv_QLNH_KHUVUC_ban.DataSource = null;
            foreach (BAN_ i in tempBan)
            {
                dgv_QLNH_KHUVUC_ban.Rows.Add(i.MABAN, i.TENBAN, i.MAKV);
            }
        }

        private void QLNH_BAN_THEM_btnThem_Click(object sender, EventArgs e)
        {
            if (QLNH_BAN_THEM_txtTenban.Text == "")
            {
                MessageBox.Show("Bạn cần điền đủ thông tin bàn cần thêm");
                return;
            }
            if (mid.kiemtraTontaiBan(QLNH_BAN_THEM_txtTenban.Text, QLNH_BAN_THEM_lbMakv.Text))
            {
                MessageBox.Show("Tên bàn đã tồn tại trong khu vực bạn chọn");
                return;
            }

            BAN_ temp = new BAN_(1, QLNH_BAN_THEM_txtTenban.Text, QLNH_BAN_THEM_lbMakv.Text);
            mid.themBan(temp);
            MessageBox.Show("Thêm bàn thành công");
            hienthiBan(lbSelectedTenkv.Text);

            //reset control
            QLNH_BAN_THEM_txtTenban.Text = "";
        }

        private void QLNH_BAN_SUA_btnSua_Click(object sender, EventArgs e)
        {
            if (QLNH_BAN_SUA_txtTenban.Text == "")
            {
                MessageBox.Show("Bạn cần điền đủ thông tin để chỉnh sửa");
                return;
            }
            if (mid.kiemtraTontaiBan1(int.Parse(QLNH_BAN_SUA_lbMaban.Text), QLNH_BAN_SUA_txtTenban.Text, QLNH_BAN_SUA_lbMakv.Text))
            {
                MessageBox.Show("Trong khu vực bạn chọn đã có bàn trùng tên");
                return;
            }
            BAN_ temp = new BAN_(int.Parse(QLNH_BAN_SUA_lbMaban.Text), QLNH_BAN_SUA_txtTenban.Text, QLNH_BAN_SUA_lbMakv.Text);
            mid.suaBan(temp);
            MessageBox.Show("Sửa bàn thành công");
            hienthiBan(lbSelectedTenkv.Text);

            //reset control
            QLNH_BAN_SUA_lbMaban.Text = "";
            QLNH_BAN_SUA_txtTenban.Text = "";
            QLNH_BAN_SUA_lbMakv.Text = "";
            QLNH_BAN_XOA_lbMaban.Text = "";
        }

        private void QLNH_BAN_XOA_btnXoa_Click(object sender, EventArgs e)
        {
            if (QLNH_BAN_XOA_lbMaban.Text == "")
            {
                MessageBox.Show("Bạn cần chọn bàn để xóa", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mid.xoaBan(int.Parse(QLNH_BAN_XOA_lbMaban.Text));
            MessageBox.Show("Xóa bàn thành công");
            hienthiBan(QLNH_KV_SUA_txtTenkv.Text);

            //reset control
            QLNH_BAN_SUA_lbMaban.Text = "";
            QLNH_BAN_SUA_txtTenban.Text = "";
            QLNH_BAN_SUA_lbMakv.Text = "";
            QLNH_BAN_XOA_lbMaban.Text = "";

        }

        private void QLNH_KHUVUC_ban_txtTimkiem_KeyUp(object sender, KeyEventArgs e)
        {
            List<BAN_> temp = mid.timkiemBan(QLNH_KHUVUC_ban_txtTimkiem.Text, lbSelectedTenkv.Text);
            dgv_QLNH_KHUVUC_ban.Rows.Clear();
            dgv_QLNH_KHUVUC_ban.DataSource = null;
            foreach (BAN_ a in temp)
            {
                dgv_QLNH_KHUVUC_ban.Rows.Add(a.MABAN, a.TENBAN, a.MAKV);
            }
        }

        #endregion

        #endregion

        #region phan NHANVIEN

        #region NHANVIEN
        private void showDulieuNV()
        {
            List<NHANVIEN_> a = new List<NHANVIEN_>();
            a = mid.layNV();
            dgv_QLNH_NHANVIEN.DataSource = null;
            dgv_QLNH_NHANVIEN.Rows.Clear();
            foreach (NHANVIEN_ i in a)
            {
                dgv_QLNH_NHANVIEN.Rows.Add(i.MANV, i.TENNV);
            }
        }

        private void dgv_QLNH_NHANVIEN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //PHAN SUA
            QLNH_NHANVIEN_SUA_manv.Text = dgv_QLNH_NHANVIEN[0, e.RowIndex].Value.ToString();
            QLNH_NHANVIEN_SUA_tennv.Text = dgv_QLNH_NHANVIEN[1, e.RowIndex].Value.ToString();
            //phan XOA
            QLNH_NHANVIEN_XOA_manv.Text = dgv_QLNH_NHANVIEN[0, e.RowIndex].Value.ToString();
            //selected cho nhanvien
            lbSelectedNV.Text = dgv_QLNH_NHANVIEN[1, e.RowIndex].Value.ToString();
            //hien thi len datagridview ben tai khoan
            showDulieuTKTuNhanvien(dgv_QLNH_NHANVIEN[1, e.RowIndex].Value.ToString());

        }

        private void QLNH_NHANVIEN_timkiem_KeyUp(object sender, KeyEventArgs e)
        {
            List<NHANVIEN_> temp = new List<NHANVIEN_>();
            temp = mid.timkiemNV(QLNH_NHANVIEN_timkiem.Text);
            dgv_QLNH_NHANVIEN.DataSource = null;
            dgv_QLNH_NHANVIEN.Rows.Clear();
            foreach (NHANVIEN_ i in temp)
            {
                dgv_QLNH_NHANVIEN.Rows.Add(i.MANV, i.TENNV);
            }
        }

        private void QLNH_NHANVIEN_THEM_btnThem_Click(object sender, EventArgs e)
        {
            if (QLNH_NHANVIEN_THEM_tennv.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin nhân viên cần thêm");
                return;
            }
            NHANVIEN_ a = new NHANVIEN_(1, QLNH_NHANVIEN_THEM_tennv.Text);
            mid.themNV(a);
            MessageBox.Show("Thêm thành công");

            //reset lai datagridview
            showDulieuNV();

            //reset control
            QLNH_NHANVIEN_THEM_tennv.Text = "";
            QLNH_NHANVIEN_timkiem.Text = "";
        }

        private void QLNH_NHANVIEN_SUA_btnSua_Click(object sender, EventArgs e)
        {
            if (QLNH_NHANVIEN_SUA_tennv.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin nhân viên cần sửa");
                return;
            }

            NHANVIEN_ a = new NHANVIEN_(int.Parse(QLNH_NHANVIEN_SUA_manv.Text), QLNH_NHANVIEN_SUA_tennv.Text);
            mid.suaNV(a);
            MessageBox.Show("Sửa thành công");

            //reset lai datagridview
            showDulieuNV();

            //reset control
            QLNH_NHANVIEN_SUA_manv.Text = "";
            QLNH_NHANVIEN_SUA_tennv.Text = "";
            QLNH_NHANVIEN_timkiem.Text = "";
            QLNH_NHANVIEN_XOA_manv.Text = "";
        }

        private void QLNH_NHANVIEN_XOA_btnXoa_Click(object sender, EventArgs e)
        {
            if (QLNH_NHANVIEN_XOA_manv.Text == "")
            {
                MessageBox.Show("Bạn cần chọn nhân viên để xóa");
                return;
            }
            mid.xoaNV(int.Parse(QLNH_NHANVIEN_XOA_manv.Text));
            MessageBox.Show("Xóa thành công");

            //reset lai datagridview
            showDulieuNV();

            //reset control
            QLNH_NHANVIEN_SUA_manv.Text = "";
            QLNH_NHANVIEN_SUA_tennv.Text = "";
            QLNH_NHANVIEN_timkiem.Text = "";
            QLNH_NHANVIEN_XOA_manv.Text = "";
        }

        #endregion

        #region TAIKHOAN

        private void dgv_QLNH_TAIKHOAN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            QLNH_TAIKHOAN_XOA_USER.Text = dgv_QLNH_TAIKHOAN[0, e.RowIndex].Value.ToString();
        }

        private void QLNH_TAIKHOAN_THEM_btnThem_Click(object sender, EventArgs e)
        {
            if (QLNH_TAIKHOAN_THEM_USER.Text == "" || QLNH_TAIKHOAN_THEM_PASS.Text == "" || QLNH_TAIKHOAN_THEM_RETYPE.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin tài khoản cần thêm");
                return;
            }
            if (mid.kiemtraTK(lbSelectedNV.Text))
            {
                MessageBox.Show("Nhân viên này đã có tài khoản ,bạn không thể thêm nữa");
                return;
            }
            if (mid.kiemtraUser(QLNH_TAIKHOAN_THEM_USER.Text))
            {
                MessageBox.Show("USER này đã có người sử dụng");
                return;
            }
            if (QLNH_TAIKHOAN_THEM_PASS.Text != QLNH_TAIKHOAN_THEM_RETYPE.Text)
            {
                MessageBox.Show("Password nhập lại chưa đúng");
                return;
            }
            mid.themTK(new TAIKHOAN_(QLNH_TAIKHOAN_THEM_USER.Text, QLNH_TAIKHOAN_THEM_PASS.Text, mid.getmanvByTennv(lbSelectedNV.Text)));
            MessageBox.Show("Thêm thành công");
            //reset control
            QLNH_TAIKHOAN_THEM_USER.Text = "";
            QLNH_TAIKHOAN_THEM_PASS.Text = "";
            QLNH_TAIKHOAN_THEM_RETYPE.Text = "";

            //reset datagridview
            showDulieuTKTuNhanvien(lbSelectedNV.Text);

        }

        private void QLNH_TAIKHOAN_XOA_btnXoa_Click(object sender, EventArgs e)
        {
            if (QLNH_TAIKHOAN_XOA_USER.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn tài khoản để xóa");
                return;
            }
            mid.xoaTK(QLNH_TAIKHOAN_XOA_USER.Text);
            MessageBox.Show("Xóa thành công");

            //reset datagridview
            showDulieuTKTuNhanvien(lbSelectedNV.Text);

            //reset control
            QLNH_TAIKHOAN_XOA_USER.Text = "";
        }

        private void showDulieuTKTuNhanvien(string tennv)
        {
            List<TAIKHOAN_> a = new List<TAIKHOAN_>();
            a = mid.layTKCuaNV(tennv);
            dgv_QLNH_TAIKHOAN.DataSource = null;
            dgv_QLNH_TAIKHOAN.Rows.Clear();
            foreach (TAIKHOAN_ i in a)
            {
                dgv_QLNH_TAIKHOAN.Rows.Add(i.USER, i.PASS, i.MANV);
            }

        }

        #endregion

        #endregion

        #region phan NHOMSP

        #region NHOMSP

        private void showDulieuNhomspQLNH()
        {
            List<NHOMMON_> temp = laydulieuNhomsp();
            dgv_QLNH_NHOMSP.DataSource = null;
            dgv_QLNH_NHOMSP.Rows.Clear();
            foreach (NHOMMON_ i in temp)
            {
                dgv_QLNH_NHOMSP.Rows.Add(i.MANHOM, i.TENNHOM);
            }
        }

        private void dgv_QLNH_NHOMSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            //-----------------------------set cac control cua NHOMSP-------------------
            //sua
            QLNH_NHOMSP_SUA_lbManhom.Text = dgv_QLNH_NHOMSP[0, e.RowIndex].Value.ToString();
            QLNH_NHOMSP_SUA_txtTennhom.Text = dgv_QLNH_NHOMSP[1, e.RowIndex].Value.ToString();
            //xoa
            QLNH_NHOMSP_XOA_lbManhom.Text = dgv_QLNH_NHOMSP[0, e.RowIndex].Value.ToString();

            //-----------------------------set cho datagridview ben san pham------------
            showDulieuSanpham(dgv_QLNH_NHOMSP[1, e.RowIndex].Value.ToString());

            //set selectednhomsp
            lbSelectedNhomsp.Text = dgv_QLNH_NHOMSP[1, e.RowIndex].Value.ToString();

            //set manhom cho ben them san pham
            QLNH_SANPHAM_THEM_manhom.Text = dgv_QLNH_NHOMSP[0, e.RowIndex].Value.ToString();
        }

        private void QLNH_NHOMSP_THEM_btnThem_Click(object sender, EventArgs e)
        {
            if (QLNH_NHOMSP_THEM_txtManhom.Text == "" || QLNH_NHOMSP_THEM_txtTennhom.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin về nhóm sản phẩm cần thêm");
                return;
            }
            if (mid.kiemtraTontaiNhommon(QLNH_NHOMSP_THEM_txtManhom.Text, QLNH_NHOMSP_THEM_txtTennhom.Text))
            {
                MessageBox.Show("Nhóm sản phẩm bạn thêm bị trùng MÃ NHÓM hoặc TÊN");
                return;
            }
            mid.themNhomSP(new NHOMMON_(QLNH_NHOMSP_THEM_txtManhom.Text, QLNH_NHOMSP_THEM_txtTennhom.Text));
            MessageBox.Show("Thêm thành công");

            //reset datagridview
            dgv_QLNH_NHOMSP.DataSource = null;
            dgv_QLNH_NHOMSP.Rows.Clear();
            showDulieuNhomspQLNH();
            //reset control
            QLNH_NHOMSP_THEM_txtManhom.Text = "";
            QLNH_NHOMSP_THEM_txtTennhom.Text = "";
            QLNH_NHOMSP_txtTimkiem.Text = "";
        }

        private void QLNH_NHOMSP_SUA_btnSua_Click(object sender, EventArgs e)
        {
            if (QLNH_NHOMSP_SUA_lbManhom.Text == "" || QLNH_NHOMSP_SUA_txtTennhom.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin về nhóm sản phẩm cần sửa");
                return;
            }
            if (mid.kiemtraTontaiNhommon1(QLNH_NHOMSP_SUA_lbManhom.Text, QLNH_NHOMSP_SUA_txtTennhom.Text))
            {
                MessageBox.Show("Nhóm sản phẩm bạn sửa bị trùng TÊN");
                return;
            }
            mid.suaNhomSP(new NHOMMON_(QLNH_NHOMSP_SUA_lbManhom.Text, QLNH_NHOMSP_SUA_txtTennhom.Text));
            MessageBox.Show("Sửa thành công");

            //reset datagridview
            dgv_QLNH_NHOMSP.DataSource = null;
            dgv_QLNH_NHOMSP.Rows.Clear();
            showDulieuNhomspQLNH();
            //reset control
            QLNH_NHOMSP_SUA_lbManhom.Text = "";
            QLNH_NHOMSP_SUA_txtTennhom.Text = "";
            QLNH_NHOMSP_XOA_lbManhom.Text = "";
            QLNH_NHOMSP_txtTimkiem.Text = "";
        }

        private void QLNH_NHOMSP_XOA_btnXoa_Click(object sender, EventArgs e)
        {
            if (QLNH_NHOMSP_XOA_lbManhom.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nhóm sản phẩm cần xóa");
                return;
            }
            mid.xoaNhomSP(QLNH_NHOMSP_XOA_lbManhom.Text);
            MessageBox.Show("Xóa thành công");

            //reset datagridview
            dgv_QLNH_NHOMSP.DataSource = null;
            dgv_QLNH_NHOMSP.Rows.Clear();
            showDulieuNhomspQLNH();
            //reset control
            QLNH_NHOMSP_SUA_lbManhom.Text = "";
            QLNH_NHOMSP_SUA_txtTennhom.Text = "";
            QLNH_NHOMSP_XOA_lbManhom.Text = "";
            QLNH_NHOMSP_txtTimkiem.Text = "";
        }

        private void QLNH_NHOMSP_txtTimkiem_KeyUp(object sender, KeyEventArgs e)
        {
            List<NHOMMON_> temp = new List<NHOMMON_>();
            temp = mid.timkiemNhommon(QLNH_NHOMSP_txtTimkiem.Text);
            dgv_QLNH_NHOMSP.DataSource = null;
            dgv_QLNH_NHOMSP.Rows.Clear();

            foreach (NHOMMON_ i in temp)
            {
                dgv_QLNH_NHOMSP.Rows.Add(i.MANHOM, i.TENNHOM);
            }
        }

        #endregion

        #region SANPHAM

        private void showDulieuSanpham(string tennhom)
        {
            List<SANPHAM_> temp = mid.laySanPhamTrongNhom(tennhom);
            dgv_QLNH_SANPHAM.DataSource = null;
            dgv_QLNH_SANPHAM.Rows.Clear();
            foreach (SANPHAM_ i in temp)
            {
                dgv_QLNH_SANPHAM.Rows.Add(i.MASP, i.TENSP, (int)i.GIA, (int)i.GIAMGIA, i.DONVI, (bool)i.TINHTRANG, i.MANHOM);
            }
        }

        private void dgv_QLNH_SANPHAM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            //set control ben SUA
            QLNH_SANPHAM_SUA_masp.Text = dgv_QLNH_SANPHAM[0, e.RowIndex].Value.ToString();
            QLNH_SANPHAM_SUA_tensp.Text = dgv_QLNH_SANPHAM[1, e.RowIndex].Value.ToString();
            QLNH_SANPHAM_SUA_gia.Text = dgv_QLNH_SANPHAM[2, e.RowIndex].Value.ToString();
            QLNH_SANPHAM_SUA_giamgia.Text = dgv_QLNH_SANPHAM[3, e.RowIndex].Value.ToString();
            QLNH_SANPHAM_SUA_donvi.Text = dgv_QLNH_SANPHAM[4, e.RowIndex].Value.ToString();
            QLNH_SANPHAM_SUA_manhom.Text = dgv_QLNH_SANPHAM[6, e.RowIndex].Value.ToString();
            if (dgv_QLNH_SANPHAM[5, e.RowIndex].Value.ToString() == "True")
            {
                QLNH_SANPHAM_SUA_tinhtrangTrue.Checked = true;
            }
            else
            {
                QLNH_SANPHAM_SUA_tinhtrangFalse.Checked = true;
            }
            //set control ben XOA
            QLNH_SANPHAM_XOA_masp.Text = dgv_QLNH_SANPHAM[0, e.RowIndex].Value.ToString();
        }

        private void QLNH_SANPHAM_XOA_btnxoa_Click(object sender, EventArgs e)
        {
            if (QLNH_SANPHAM_XOA_masp.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm để xóa");
                return;
            }
            mid.xoaSP(int.Parse(QLNH_SANPHAM_XOA_masp.Text));
            MessageBox.Show("Xóa thành công");

            //reset control
            QLNH_SANPHAM_SUA_masp.Text = "";
            QLNH_SANPHAM_SUA_tensp.Text = "";
            QLNH_SANPHAM_SUA_gia.Text = "";
            QLNH_SANPHAM_SUA_giamgia.Text = "";
            QLNH_SANPHAM_SUA_manhom.Text = "";
            QLNH_SANPHAM_SUA_donvi.Text = "";
            QLNH_SANPHAM_txtTimkiem.Text = "";
            QLNH_SANPHAM_XOA_masp.Text = "";

            //show du lieu san pham
            showDulieuSanpham(lbSelectedNhomsp.Text);
        }

        private void QLNH_SANPHAM_THEM_btnThem_Click(object sender, EventArgs e)
        {
            if (QLNH_SANPHAM_THEM_tensp.Text == "" || QLNH_SANPHAM_THEM_gia.Text == "" || QLNH_SANPHAM_THEM_giamgia.Text == "" || QLNH_SANPHAM_THEM_donvi.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin về sản phẩm cần thêm vào");
                return;
            }
            if (mid.kiemtraSp(QLNH_SANPHAM_THEM_tensp.Text))
            {
                MessageBox.Show("Sản phẩm bạn định thêm vào đã tồn tại");
                return;
            }
            bool co = false;
            if (QLNH_SANPHAM_THEM_tinhtrangTrue.Checked == true)
            {
                co = true;
            }
            else
            {
                co = false;
            }
            SANPHAM_ a = new SANPHAM_(1, QLNH_SANPHAM_THEM_tensp.Text, int.Parse(QLNH_SANPHAM_THEM_gia.Text), int.Parse(QLNH_SANPHAM_THEM_giamgia.Text), QLNH_SANPHAM_THEM_donvi.Text, co, QLNH_SANPHAM_THEM_manhom.Text);
            mid.themSP(a);
            MessageBox.Show("Thêm thành công");

            //reset control trong chinh sanpham
            QLNH_SANPHAM_THEM_tensp.Text = "";
            QLNH_SANPHAM_THEM_gia.Text = "";
            QLNH_SANPHAM_THEM_giamgia.Text = "";
            QLNH_SANPHAM_THEM_manhom.Text = "";
            QLNH_SANPHAM_THEM_donvi.Text = "";
            QLNH_SANPHAM_txtTimkiem.Text = "";

            //show du lieu san pham
            showDulieuSanpham(lbSelectedNhomsp.Text);
        }

        private void QLNH_SANPHAM_SUA_btnsua_Click(object sender, EventArgs e)
        {
            if (QLNH_SANPHAM_SUA_tensp.Text == "" || QLNH_SANPHAM_SUA_gia.Text == "" || QLNH_SANPHAM_SUA_giamgia.Text == "" || QLNH_SANPHAM_SUA_donvi.Text == "" || QLNH_SANPHAM_SUA_manhom.Text == "")
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin về sản phẩm cần thêm vào");
                return;
            }
            if (mid.kiemtraSp1(int.Parse(QLNH_SANPHAM_SUA_masp.Text), QLNH_SANPHAM_SUA_tensp.Text))
            {
                MessageBox.Show("Sản phẩm bạn định sửa đã tồn tại");
                return;
            }
            bool co = false;
            if (QLNH_SANPHAM_SUA_tinhtrangTrue.Checked == true)
            {
                co = true;
            }
            else
            {
                co = false;
            }
            SANPHAM_ a = new SANPHAM_(int.Parse(QLNH_SANPHAM_SUA_masp.Text), QLNH_SANPHAM_SUA_tensp.Text, int.Parse(QLNH_SANPHAM_SUA_gia.Text), int.Parse(QLNH_SANPHAM_SUA_giamgia.Text), QLNH_SANPHAM_SUA_donvi.Text, co, QLNH_SANPHAM_SUA_manhom.Text);
            mid.suaSP(a);
            MessageBox.Show("Sửa thành công");

            //reset control
            QLNH_SANPHAM_SUA_masp.Text = "";
            QLNH_SANPHAM_SUA_tensp.Text = "";
            QLNH_SANPHAM_SUA_gia.Text = "";
            QLNH_SANPHAM_SUA_giamgia.Text = "";
            QLNH_SANPHAM_SUA_manhom.Text = "";
            QLNH_SANPHAM_SUA_donvi.Text = "";
            QLNH_SANPHAM_txtTimkiem.Text = "";
            QLNH_SANPHAM_XOA_masp.Text = "";

            //show du lieu san pham
            showDulieuSanpham(lbSelectedNhomsp.Text);
        }

        private void QLNH_SANPHAM_txtTimkiem_KeyUp(object sender, KeyEventArgs e)
        {
            List<SANPHAM_> a = new List<SANPHAM_>();
            a = mid.timkiemSP(QLNH_SANPHAM_txtTimkiem.Text, lbSelectedNhomsp.Text);
            dgv_QLNH_SANPHAM.DataSource = null;
            dgv_QLNH_SANPHAM.Rows.Clear();
            foreach (SANPHAM_ sp in a)
            {
                dgv_QLNH_SANPHAM.Rows.Add(sp.MASP, sp.TENSP, sp.GIA, sp.GIAMGIA, sp.DONVI, sp.TINHTRANG, sp.MANHOM);
            }
        }

        #endregion

        #endregion

        #endregion

        #region XEM HOA DON

        private void timkiemHD(string mahd)
        {
            List<HOADON_> a = new List<HOADON_>();
            a = mid.timkiemHD(string.Format("{0}%",mahd));
            XEMHD_dgvHD.DataSource = null;
            XEMHD_dgvHD.Rows.Clear();
            foreach (HOADON_ i in a)
            {
                XEMHD_dgvHD.Rows.Add(i.MAHD, i.NGAYLAP, i.SLKHACH, i.PHIKHAC, i.GHICHU, i.MANV, i.MABAN);
            }
        }

        private void loadCTHD_Of_HD(int mahd)
        {
            List<CTHOADON_> a = new List<CTHOADON_>();
            a = mid.layCTHDQuaHD(mahd);
            XEMHD_dgvCTHD.DataSource = null;
            XEMHD_dgvCTHD.Rows.Clear();
            foreach (CTHOADON_ i in a)
            {
                int thanhtien = (i.SOLUONG * i.GIA * (100 - i.GIAMGIA)) / 100;
                XEMHD_dgvCTHD.Rows.Add(i.MAHD, i.MASP, i.SOLUONG, i.GIA, i.GIAMGIA,thanhtien);
            }
            int tongtien = 0;
            for (int i = 0; i < XEMHD_dgvCTHD.Rows.Count; i++)
            {
                tongtien += int.Parse(XEMHD_dgvCTHD[5, i].Value.ToString());
            }
            XEMHD_lbTongtien.Text = tongtien.ToString();
        }

        private void XEMHD_dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            loadCTHD_Of_HD(int.Parse(XEMHD_dgvHD[0, e.RowIndex].Value.ToString()));
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            timkiemHD(XEMHD_timkiemHD.Text);
        }

        #endregion

        #region GIAO DIEN

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            a.BackColor = Color.FromArgb(40,40,40);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            a.BackColor = Color.Black;
        }

        private void btnThemsp_MouseMove(object sender, MouseEventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            a.ForeColor = Color.Lime;
        }

        private void btnThemsp_MouseLeave(object sender, EventArgs e)
        {
            Button a = new Button();
            a = (Button)sender;
            a.ForeColor = Color.Black;
        }

        #endregion

        

    }
}
