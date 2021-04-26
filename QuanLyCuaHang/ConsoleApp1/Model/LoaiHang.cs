using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public class LoaiHang
    {
        #region Properties
        private string _maLoaiHang;
        private string _tenLoaiHang;
        #endregion Properties

        #region Constant
        public const string VN_MA_LOAI_HANG = "Mã Loại hàng";
        public const string VN_TEN_LOAI_HANG = "Tên Loại hàng";
        #endregion

        #region Getter and Setter
        public string MaLoaiHang
        {
            get { return _maLoaiHang; }
            set { _maLoaiHang = value; }
        }

        public string TenLoaiHang
        {
            get { return _tenLoaiHang; }
            set { _tenLoaiHang = value; }
        }
        #endregion Getter and Setter

        #region Contructors
        public LoaiHang(string maLoaiHang, string tenLoaiHang)
        {
            MaLoaiHang  = maLoaiHang;
            TenLoaiHang = tenLoaiHang;
        }
        #endregion Contructors
    }
}
