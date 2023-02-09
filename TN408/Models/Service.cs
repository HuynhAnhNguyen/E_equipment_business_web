using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TN408.Areas.Store.Models;

namespace TN408.Models
{
    public class Service
    {
        private TN408Context _context = new TN408Context();
        // Loại sản phẩm
        // Danh sách loại sản phẩm
        public IQueryable<LoaiSanPham> danhSachLoaiSP()
        {
            return _context.LoaiSanPhams.Where(x => x.MaLoaiSp != null);
        }

        // Lấy loại sản phẩm
        public LoaiSanPham? getLoai(string maLoai)
        {
            return danhSachLoaiSP().Where(x => x.MaLoaiSp == maLoai).FirstOrDefault();
        }

        // Chỉnh sửa loại sản phẩm
        public void suaLoaiSanPham(LoaiSanPham sP)
        {
            var loaiSP = getLoai(sP.MaLoaiSp);
            _context.Update(loaiSP);
            loaiSP.TenLoaiSp = sP.TenLoaiSp;
            _context.SaveChanges();
        }

        // Thêm loại sản phẩm
        public void themLoaiSP(LoaiSanPham loaiSP)
        {
            var new_maloai = "" + (danhSachLoaiSP().Count() + 1);
            LoaiSanPham newLoaiSP = new LoaiSanPham();
            newLoaiSP.MaLoaiSp = loaiSP.MaLoaiSp;
            newLoaiSP.TenLoaiSp = loaiSP.TenLoaiSp;
            _context.LoaiSanPhams.Add(newLoaiSP);
            _context.SaveChanges();
        }

        // Xóa loại sản phẩm
        public int xoaLoaiSP(string maLoaiSP)
        {
            var khoaChinh = _context.SanPhams.Where(x => x.MaLoaiSp == maLoaiSP).ToList();
            if (khoaChinh.Any())
            {
                return 0;
            }
            _context.LoaiSanPhams.Remove(_context.LoaiSanPhams.Find(maLoaiSP));
            _context.SaveChanges();
            return 1;
        }

        // Đơn đặt
        // Danh sách đơn đặt
        public IQueryable<DonDat> danhSachDonDat(int tt = 2, string maKhachHang = null)
        {
            var rs = _context.DonDats.Where(x => x.MaKhachHang == maKhachHang);
            if (tt == 2)
            {
                return _context.DonDats.Where(x => x.MaDonDat!= null).Include(x => x.MaSanPhamNavigation).ThenInclude(x => x.MaDvtNavigation);
            }

            return rs.Where(x => x.TrangThai == tt).Include(x => x.MaSanPhamNavigation).ThenInclude(x => x.MaDvtNavigation);
        }

        // Lấy đơn đặt
        public DonDat getDonDat(string maDonDat)
        {
            if (!string.IsNullOrEmpty(maDonDat))
            {
                return _context.DonDats.Where(x=>x.MaDonDat== maDonDat).Include(x=> x.MaSanPhamNavigation).FirstOrDefault();
            }
            return null;
        }

        // Thêm đơn đặt
        public string themDonDat(string maSP, string maKH = null)
        {
            string maDD = "DD" + (danhSachDonDat(2).Count() + 1);
            var rs = danhSachDonDat(0, maKH).Where(x => x.MaSanPham == maSP);
            if (rs.Any())
            {
                var model = rs.FirstOrDefault();
                _context.Update(model);
                model.SoLuongDat = model.SoLuongDat + 1;
                _context.SaveChanges();
                return model.MaDonDat;
            }

            DonDat dd = new DonDat();
            dd.MaDonDat = maDD;
            dd.MaKhachHang = maKH;
            if (maKH == null)
            {
                dd.TrangThai = 2;
            }
            else
            {
                dd.TrangThai = 0;
            }
            dd.SoLuongDat = 1;
            dd.MaSanPham = maSP;
            _context.DonDats.Add(dd);
            _context.SaveChanges();
            return dd.MaDonDat;
        }

        // Xóa đơn đặt
        public void xoaDonDat(string maDD)
        {
            if (!string.IsNullOrEmpty(maDD))
            {
                var model = _context.DonDats.Find(maDD);
                _context.Update(model);
                if (model.TrangThai == 0 || model.TrangThai == 2)
                {
                    model.TrangThai = -1;
                }
                _context.SaveChanges();
            }
        }

        // Sản phẩm
        // Danh sách sản phẩm
        public IQueryable<SanPham> danhSachSanPham()
        {
            return _context.SanPhams.Where(x => x.MaSanPham != null).Include(x => x.MaDvtNavigation).Take(16);
        }

        // Danh sách sản phẩm theo loại
        public IQueryable<SanPham> danhSachSanPham(string maLoai= null)
        {
            return _context.SanPhams.Where(x => x.MaSanPham != null).Where(x => x.MaLoaiSp == maLoai);
        }

        // Lấy sản phẩm
        public SanPham? getSanPham(string maSanPham)
        {
            return danhSachSanPham().Where(x => x.MaSanPham == maSanPham).FirstOrDefault();
        }

        // Lấy sản phẩm
        public SanPham? GetSanPham(string maSanPham)
        {
            return _context.SanPhams.Find(maSanPham);
        }

        // Xóa sản phẩm
        public void xoaSanPham(string maSP)
        {
            if (!string.IsNullOrEmpty(maSP))
            {
                var model = _context.SanPhams.Find(maSP);
                _context.SanPhams.Remove(model);
                _context.SaveChanges();
            }
        }

        // Sửa sản phẩm
        public void suaSanPham(SanPham sanPham)
        {
            var sP = getSanPham(sanPham.MaSanPham);
            _context.Update(sP);
            _context.SaveChanges();
        }

        // Thêm sản phẩm
        public void themSanPham(SanPham sanPham)
        {
            _context.SanPhams.Add(sanPham);
            _context.SaveChanges();
        }

        public void themSanPham(SanPham sanPham, IFormFile file)
        {
            string maSP = "" + (danhSachSanPham().Count() + 1);
            sanPham.MaSanPham = maSP;
            if(file != null)
            {
                var path = getDataPath(file.FileName);
                using var stream= new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                sanPham.HinhAnh = file.FileName;
            }
            _context.SanPhams.Add(sanPham);
            _context.SaveChanges();
        }

        public void suaSanPham(SanPham sanPham, IFormFile file)
        {
            var sp = getSanPham(sanPham.MaSanPham);
            _context.Update(sp);
            if (file != null)
            {
                var path = getDataPath(file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                sp.HinhAnh = file.FileName;
            }
            
            _context.SaveChanges();
        }
        // Tìm kiếm sản phẩm
        public IQueryable<SanPham> timKiem(string key)
        {
            return danhSachSanPham().Where(x => x.TenSanPham.Contains(key));
        }

        // Đơn vị tính
        // Danh sách đơn vị tính
        public IQueryable<DonViTinh> danhSachDVT()
        {
            return _context.DonViTinhs.Where(x => x.MaDvt != null);
        }

        // Lấy đơn vị tính
        public DonViTinh getDVT(string maDVT)
        {
            return danhSachDVT().Where(x => x.MaDvt == maDVT).FirstOrDefault();
        }

        // Nhà sản xuất
        // Danh sách nhà sản xuất
        public IQueryable<NhaSanXuat> danhSachNSX()
        {
            return _context.NhaSanXuats.Where(x => x.MaNsx != null);
        }

        // Lấy nhà sản xuất
        public NhaSanXuat getNSX(string maNSX)
        {
            return danhSachNSX().Where(x => x.MaNsx == maNSX).FirstOrDefault();
        }

        // Khách hàng
        // Danh sách khách hàng
        public IQueryable<KhachHang> danhSachKH()
        {
            return _context.KhachHangs.Where(x => x.MaKhachHang != null);
        }

        // Lấy khách hàng
        public KhachHang getKH(string maKH)
        {
            return danhSachKH().Where(x => x.MaKhachHang == maKH).FirstOrDefault();
        }

        // Lấy khách hàng
        public KhachHang GetKH(string maKH)
        {
            return _context.KhachHangs.Find(maKH);
        }

        // Thêm khách hàng ( Register)
        public void themKH(RegisterModel model)
        {
            string maKH = "" + (danhSachKH().Count() + 1);
            KhachHang kh = new KhachHang();
            kh.MaKhachHang = maKH;
            kh.HoKhachHang = model.HoKhachHang;
            kh.TenKhachHang = model.TenKhachHang;
            kh.NgaySinh = model.NgaySinh;
            kh.GioiTinh = model.GioiTinh;
            kh.SoDienThoai = model.SoDienThoai;
            kh.DiaChi = model.DiaChi;
            kh.MatKhau = getMD5(model.MatKhau);
            kh.MaNhom = "1";
            _context.KhachHangs.Add(kh);
            _context.SaveChanges();
        }

        // Khách hàng login
        public KhachHang? loginKH(string sDT, string pwd)
        {
            return danhSachKH().Where(x => x.SoDienThoai == sDT).Where(x => x.MatKhau == getMD5(pwd)).FirstOrDefault();
        }

        // Hóa dơn
        // Danh sách hóa đơn
        public IQueryable<HoaDon> danhSachHoaDon(string maKH = null)
        {
            if (maKH == null)
            {
                return _context.HoaDons.Where(x => x.MaHoaDon != null).Include(x => x.ChiTietHds).ThenInclude(y => y.MaDonDatNavigation);
            }
            return _context.HoaDons.Where(x => x.MaKhachHang == maKH).Include(x => x.ChiTietHds).ThenInclude(y => y.MaDonDatNavigation);
        }

        // Lấy hóa đơn
        public HoaDon? getHoaDon(string maHD)
        {
            return danhSachHoaDon().Where(x => x.MaHoaDon == maHD).FirstOrDefault();
        }

        // Thêm hóa đơn
        public string themHoaDon(string maKH = "")
        {
            HoaDon hoaDon = new HoaDon();
            hoaDon.MaHoaDon = "" + (danhSachHoaDon().Count() + 1);
            if (maKH == "")
            {
                hoaDon.MaKhachHang = null;
            }
            else
            {
                hoaDon.MaKhachHang = maKH;
            }
            hoaDon.NgayXuatHd = DateTime.Now;
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();
            return hoaDon.MaHoaDon;
        }

        // Chi tiết hóa đơn
        // Danh sách chi tiết hóa đơn
        public IQueryable<ChiTietHd> danhSachChiTietHD(string maHD = null)
        {
            if (maHD == null)
            {
                return _context.ChiTietHds.Where(x => x.MaChiTietHd != null);
            }
            return _context.ChiTietHds.Where(x => x.MaHoaDon == maHD);
        }

        // Thêm chi tiết hóa đơn
        public string themChiTietHD(string maHD, string maDD)
        {
            ChiTietHd chiTietHD = new ChiTietHd();
            chiTietHD.MaChiTietHd = "" + (danhSachChiTietHD().Count() + 1);
            chiTietHD.MaDonDat = maDD;
            getDonDat(maDD).TrangThai = 1;
            chiTietHD.MaHoaDon = maHD;
            _context.ChiTietHds.Add(chiTietHD);
            _context.SaveChanges();
            return chiTietHD.MaChiTietHd;
        }

        // Tăng số lượng đơn đặt
        public void increase(string maDD, int soLuong)
        {
            var donDat = getDonDat(maDD);
            _context.Update(donDat);
            donDat.SoLuongDat = soLuong;

            _context.SaveChanges();
        }

        // Nhân viên
        // Danh sách nhân viên
        public IQueryable<NhanVien> danhSachNhanVien()
        {
            return _context.NhanViens.Where(x => x.MaNhanVien != null);
        }

        // Lấy nhân viên
        public NhanVien? getNV(string maNV)
        {
            return danhSachNhanVien().Where(x => x.MaNhanVien== maNV).FirstOrDefault();
        }

        // Lấy nhân viên
        public NhanVien getNhanVien(string maNV)
        {
            return _context.NhanViens.Find(maNV);
        }

        // Thêm nhân viên
        public void themNV(string hoNV, string tenNV, DateTime ngaySinh, string gioiTinh, string soDT, string diaChi, string matKhau, string nhom)
        {

            NhanVien nV = new NhanVien();
            string maNV = "" + (danhSachNhanVien().Count() + 1);
            nV.MaNhanVien = maNV;
            nV.HoNhanVien = hoNV;
            nV.TenNhanVien = tenNV;
            nV.NgaySinh = ngaySinh;
            nV.GioiTinh = gioiTinh;
            nV.SoDienThoai = soDT;
            nV.DiaChi = diaChi;
            nV.MatKhau = getMD5(matKhau);
            nV.MaNhom = nhom;
            _context.NhanViens.Add(nV);
            _context.SaveChanges(true);
        }

        // Xóa nhân viên
        public void xoaNhanVien(string maNV)
        {
            if (!string.IsNullOrEmpty(maNV))
            {
                var model = _context.NhanViens.Find(maNV);
                _context.NhanViens.Remove(model);
                _context.SaveChanges();
            }
        }

        // Sửa nhân viên
        public void suaNhanVien(NhanVien nhanVien)
        {
            var nV = getNV(nhanVien.MaNhanVien);
            _context.Update(nV);
            nV.HoNhanVien = nhanVien.HoNhanVien;
            nV.TenNhanVien = nhanVien.TenNhanVien;
            nV.NgaySinh = nhanVien.NgaySinh;
            nV.SoDienThoai = nhanVien.SoDienThoai;
            nV.DiaChi = nhanVien.DiaChi;
            nV.MatKhau = nhanVien.MatKhau;
            nV.MaNhom = nhanVien.MaNhom;

            _context.SaveChanges();

        }

        // Nhân viên (Đăng nhập)
        public NhanVien? loginNV(string sDT, string matKhau)
        {
            return danhSachNhanVien().Where(x => x.SoDienThoai == sDT).Where(x => x.MatKhau == getMD5(matKhau)).FirstOrDefault();
        }

        // Lấy đường dẫn
        public string getDataPath(string file) => $"wwwroot\\images\\product\\{file}";


        // Tổng giá trị hóa đơn
        public long tongGiaTri(string maHD)
        {
            long sum = 0;
            var dds = danhSachChiTietHD(maHD).ToList();
            foreach (var dd in dds)
            {
                sum += (long)dd.MaDonDatNavigation.SoLuongDat * dd.MaDonDatNavigation.MaSanPhamNavigation.GiaBan;
            }
            return sum;
        }

        // Mã hóa mật khẩu MD5
        public static string getMD5(string password)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(password);
            byte[] targetData = mD5.ComputeHash(fromData);

            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        // Nhóm
        // Danh sách nhóm
        public IQueryable<Nhom> danhsachNhom()
        {
            return _context.Nhoms.Where(x => x.MaNhom != null);
        }

        // Lấy nhóm
        public Nhom getNhom(string maNhom)
        {
            return danhsachNhom().Where(x => x.MaNhom == maNhom).FirstOrDefault();
        }

        public void suaNhom(Nhom nhom)
        {
            var nhoms = getNhom(nhom.MaNhom);
            _context.Update(nhoms);
            nhoms.TenNhom = nhom.TenNhom;
            _context.SaveChanges();
        }
        public void themNhom(Nhom nhom)
        {
            var new_manhom = "" + (danhsachNhom().Count() + 1);
            Nhom new_nhom = new Nhom();
            new_nhom.MaNhom = new_manhom;
            new_nhom.TenNhom= nhom.TenNhom;
            _context.Nhoms.Add(new_nhom);
            _context.SaveChanges();
        }

        public int xoaNhom(string maNhom)
        {
            var fk1 = _context.NhanViens.Where(x => x.MaNhom == maNhom).ToList();
            var fk2 = _context.KhachHangs.Where(x => x.MaNhom == maNhom).ToList();
            if (fk1.Any() || fk2.Any())
            {
                return 0;
            }
            _context.Nhoms.Remove(_context.Nhoms.Find(maNhom));
            _context.SaveChanges();
            return 1;
        }
    }
}
