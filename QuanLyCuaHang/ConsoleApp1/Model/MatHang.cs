using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public class MatHang
    {
        #region Properties
        private string _maHang;
        private string _tenHang;
        private DateTime _hanDung;
        private string _congTySanXuat;
        private int _namSanXuat;
        private LoaiHang _loaihang;
        #endregion Properties

        #region Constant
        public const string VN_MA_MAT_HANG = "Mã Mặt hàng";
        public const string VN_TEN_MAT_HANG = "Tên Mặt hàng";
        public const string VN_HAN_DUNG_MAT_HANG = "Hạn dùng Mặt hàng";
        public const string VN_CONG_TY_SX_MAT_HANG = "Công ty Sản xuất Mặt hàng";
        public const string VN_NAM_SAN_XUAT_MAT_HANG = "Năm sản xuất mặt hàng";
        public const string VN_LOAI_HANG_MAT_HANG = "Loại Hàng";
        #endregion

        #region Getter and Setter
        public string MaMatHang
        {
            get { return _maHang; }
            set { _maHang = value; }
        }

        public string TenMatHang
        {
            get { return _tenHang; }
            set { _tenHang = value; }
        }

        public DateTime HanDung
        {
            get { return _hanDung; }
            set { _hanDung = value; }
        }

        public string CongTySanXuat
        {
            get { return _congTySanXuat; }
            set { _congTySanXuat = value; }
        }

        public int NamSanXuat
        {
            get { return _namSanXuat; }
            set { _namSanXuat = value; }
        }

        public LoaiHang LoaiHang
        {
            get { return _loaihang; }
            set { _loaihang = value; }
        }

        #endregion Getter and Setter

        #region Constructors
        public MatHang(string maHang, string tenHang, DateTime hanDung, string congTySanXuat, int namSanXuat, LoaiHang loaiHang)
        {
            MaMatHang       = maHang;
            TenMatHang      = tenHang;
            HanDung         = hanDung;
            CongTySanXuat   = congTySanXuat;
            NamSanXuat      = namSanXuat;
            LoaiHang        = loaiHang;
        }
        #endregion Constructors
    }
}
