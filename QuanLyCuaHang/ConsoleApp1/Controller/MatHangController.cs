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
                LoaiHangController.GetDefaultLoaiHang()
                )};

        #region Create new MatHang
        public static void ProcessCreateMatHang()
        {
            Console.WriteLine("***** NHẬP MẶT HÀNG MỚI *****");

            // Nhap Ma loai Hang
            string maMatHangInput = InputAndValidate_MaMatHang(false);

            // Nhap Ten Loai Hang
            string tenMatHangInput = InputAndValidate_TenMatHang(false);

            Console.WriteLine("================= PHẦN XÁC NHẬN THÔNG TIN VỪA NHẬP =================");

            Console.WriteLine("Xin vui lòng xác nhận lại thông tin loại hàng bạn vừa nhập dưới đây.");
            Console.WriteLine("Trong trường hợp đúng, vui lòng nhấn bất kỳ phím nào để lưu thông tin");
            Console.WriteLine("Trong trường hợp sai, vui lòng nhấn phím s để thoát ra");

            Console.WriteLine("========= THÔNG TIN LOẠI HÀNG VỪA NHẬP =========");
            Console.WriteLine("Mã mặt hàng: {0}", maMatHangInput);
            Console.WriteLine("Tên mặt hàng: {0}", tenMatHangInput);
            Console.WriteLine("========= KẾT THÚC THÔNG TIN LOẠI HÀNG VỪA NHẬP =========");

            string userInput = Console.ReadLine();

        }

        private static string InputAndValidate_MaMatHang(bool isEdit)
        {
            // Input Ma loai hang
            Console.WriteLine("==================================");
            Console.WriteLine("Xin vui lòng nhập vào Mã mặt hàng");
            string userInput = Console.ReadLine().Trim();

            while (true)
            {
                if (!isEdit && string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Mã mặt hàng không chấp nhận giá trị rỗng, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                else if (userInput.Length > 20)
                {
                    Console.WriteLine("Mã mặt hàng chỉ chấp nhận chuỗi có độ dài tối đa 20 ký tự, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                else if (IsMaMatHangDuplicated(userInput))
                {
                    Console.WriteLine("Mã mặt hàng này đã tồn tại. Vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                // In case all validations is pass
                else
                {
                    Console.WriteLine("Mã mặt hàng bạn vừa nhập là {0}", userInput);
                    break;
                }
            }

            return userInput;
        }

        private static string InputAndValidate_TenMatHang(bool isEdit)
        {
            // Input Ten loai hang
            Console.WriteLine("==================================");
            Console.WriteLine("Xin vui lòng nhập vào Tên mặt hàng");
            string userInput = Console.ReadLine().Trim();

            while (true)
            {
                if (!isEdit && string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Tên mặt hàng không chấp nhận giá trị rỗng, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                else if (userInput.Length > 50)
                {
                    Console.WriteLine("Tên mặt hàng chỉ chấp nhận chuỗi có độ dài tối đa 50 ký tự, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                // In case all validations is pass
                else
                {
                    Console.WriteLine("Tên loại hàng bạn vừa nhập là {0}", userInput);
                    break;
                }
            }

            return userInput;
        }

        private static DateTime InputAndValidate_HanDung()
        {
            // Input Ma loai hang
            Console.WriteLine("==================================");
            Console.WriteLine("Xin vui lòng nhập vào Hạn Dùng (Ngày)");
            string userInput = Console.ReadLine().Trim();
        }

        #endregion

        #region Common functions
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
