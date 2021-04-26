using ConsoleApp1.Commons;
using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Controller
{
    public class MatHangController
    {
        private static List<MatHang> MatHangList = new List<MatHang>(){
            new MatHang(CommonConstant.MA_MAT_HANG_DEFAULT,
                CommonConstant.TEN_MAT_HANG_DEFAULT,
                new DateTime(CommonConstant.HAN_DUNG_NAM, CommonConstant.HAN_DUNG_THANG, CommonConstant.HAN_DUNG_NGAY),
                CommonConstant.CONG_TY_SAN_XUAT,
                CommonConstant.NAM_SAN_XUAT,
                LoaiHangController.GetLoaiHangMacDinh()
                )};

        #region Output all Mat hang
        public static void ProcessOutputAllCurrentLoaiHang()
        {
            if (MatHangList.Count > 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("========== DANH SÁCH MẶT HÀNG ==========");
                OutputMatHangOnList(MatHangList);
                Console.WriteLine("========== KẾT THÚC DANH SÁCH MẶT HÀNG ==========");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.WriteLine("\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Không có bất kỳ mặt hàng nào được lưu trong hệ thống");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.ReadLine();
            }
        }

        #endregion

        #region Create new MatHang
        public static void ProcessCreateMatHang()
        {
            Console.WriteLine("***** NHẬP MẶT HÀNG MỚI *****");

            // Nhap Ma loai Hang
            string maMatHangInput = CommonFunctions.InputAndValidate_String(false, MatHang.VN_MA_MAT_HANG, 50, IsMaMatHangDuplicated);

            // Nhap Ten Loai Hang
            string tenMatHangInput = CommonFunctions.InputAndValidate_String(false, MatHang.VN_TEN_MAT_HANG, 50);

            // Nhap Han Su dung
            DateTime hanSuDungInput = CommonFunctions.InputAndValidate_DateTime(MatHang.VN_HAN_DUNG_MAT_HANG);

            // Nhap Cong ty san xuat
            string congTySanXuatInput = CommonFunctions.InputAndValidate_String(false, MatHang.VN_CONG_TY_SX_MAT_HANG, 50);

            // Nhap Nam san xuat
            int namSanXuat = CommonFunctions.InputAndValidate_Year(MatHang.VN_NAM_SAN_XUAT_MAT_HANG);

            // Nhap loai hang
            LoaiHang loaiHangInput = GetOneLoaiHangForMatHang();

            Console.WriteLine("================= PHẦN XÁC NHẬN THÔNG TIN VỪA NHẬP =================");

            Console.WriteLine("Xin vui lòng xác nhận lại thông tin mặt hàng bạn vừa nhập dưới đây.");
            Console.WriteLine("Trong trường hợp đúng, vui lòng nhấn bất kỳ phím nào để lưu thông tin");
            Console.WriteLine("Trong trường hợp sai, vui lòng nhấn phím s để thoát ra");

            Console.WriteLine("========= THÔNG TIN MẶT HÀNG VỪA NHẬP =========");
            Console.WriteLine("Mã mặt hàng: {0}", maMatHangInput);
            Console.WriteLine("Tên mặt hàng: {0}", tenMatHangInput);
            Console.WriteLine("Hạn sử dụng: {0}", hanSuDungInput.ToString("dd/MM/yyyy"));
            Console.WriteLine("Công ty sản xuất: {0}", congTySanXuatInput);
            Console.WriteLine("Năm sản xuất: {0}", namSanXuat);
            Console.WriteLine("Loại Hàng: {0}", LoaiHangController.OutputMotLoaiHang(loaiHangInput));
            Console.WriteLine("========= KẾT THÚC THÔNG TIN MẶT HÀNG VỪA NHẬP =========");

            string userInput = Console.ReadLine();

            if (userInput.ToLower() != "s")
            {
                MatHangList.Add(new MatHang(maMatHangInput, tenMatHangInput, hanSuDungInput, congTySanXuatInput, namSanXuat, loaiHangInput));
                Console.WriteLine("Bạn đã nhập thành công mặt hàng trên");
                Console.WriteLine("==================================");
            }
            else
            {
                Console.WriteLine("Bạn đã từ bỏ các thông tin mặt hàng vừa nhập");
                Console.WriteLine("==================================");
            }
        }


        #endregion

        #region Common functions

        private static void OutputMatHangOnList(List<MatHang> targetList)
        {
            int sothutu = 1;
            for (int i = 0; i < targetList.Count; i++)
            {
                var currentMatHang = targetList[i];
                if (currentMatHang != null)
                {
                    Console.WriteLine("\n{0}. {1} : {2},\n {3} : {4},\n {5} : {6},\n {7} : {8},\n {9} : {10},\n {11} : {12}\n--------------",
                        sothutu, 
                        MatHang.VN_MA_MAT_HANG, currentMatHang.MaMatHang,
                        MatHang.VN_TEN_MAT_HANG, currentMatHang.TenMatHang,
                        MatHang.VN_HAN_DUNG_MAT_HANG, currentMatHang.HanDung.ToString("dd/MM/yyyy"),
                        MatHang.VN_CONG_TY_SX_MAT_HANG, currentMatHang.CongTySanXuat,
                        MatHang.VN_NAM_SAN_XUAT_MAT_HANG, currentMatHang.NamSanXuat,
                        MatHang.VN_LOAI_HANG_MAT_HANG, LoaiHangController.OutputMotLoaiHang(currentMatHang.LoaiHang));

                    sothutu++;
                }
            }
        }

        private static LoaiHang GetOneLoaiHangForMatHang()
        {
            LoaiHang loaiHangInput = LoaiHangController.GetOneLoaiHangToAction(null, CommonConstant.GET_TAG);

            if (loaiHangInput == null)
            {
                Console.WriteLine("LƯU Ý: Nếu bạn để Loại Hàng là giá trị rỗng thì hệ thống sẽ tự gán loại hàng mặc định cho mặt hàng của bạn");
                return LoaiHangController.GetLoaiHangMacDinh();
            }

            return loaiHangInput;
        }

        private static bool IsMaMatHangDuplicated(string maMatHang)
        {
            foreach (var curLoaiHang in MatHangList)
            {
                if (curLoaiHang.MaMatHang == maMatHang.Trim())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
