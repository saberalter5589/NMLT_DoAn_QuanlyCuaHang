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
