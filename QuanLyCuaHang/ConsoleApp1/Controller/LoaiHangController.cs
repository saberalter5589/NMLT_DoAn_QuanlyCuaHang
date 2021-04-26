using ConsoleApp1.Commons;
using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Controller
{
    public class LoaiHangController
    {
        private static List<LoaiHang> LoaiHangList = new List<LoaiHang>(){ 
            new LoaiHang(CommonConstant.MA_LOAI_HANG_DEFAULT, CommonConstant.TEN_LOAI_HANG_DEFAULT),
            new LoaiHang("LH001", "dAO khu tu"),
            new LoaiHang("LH002", "dAO khu Bich"),};

        #region Output all Loai hang
        public static void ProcessOutputAllCurrentLoaiHang()
        {
            if (LoaiHangList.Count > 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("========== DANH SÁCH LOẠI HÀNG ==========");
                OutputLoaiHangOnList(LoaiHangList);
                Console.WriteLine("========== KẾT THÚC DANH SÁCH LOẠI HÀNG ==========");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.WriteLine("\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Không có bất kỳ loại hàng nào được lưu trong hệ thống");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.ReadLine();
            }
        }

        #endregion

        #region Create new Loai hang
        public static void ProcessCreateLoaiHang()
        {
            Console.WriteLine("***** NHẬP LOẠI HÀNG MỚI *****");

            // Nhap Ma loai Hang
            //string maLoaiHangInput = InputAndValidate_MaLoaiHang(false);
            string maLoaiHangInput = CommonFunctions.InputAndValidate_String(false, LoaiHang.VN_MA_LOAI_HANG, 50, IsMaLoaiHangDuplicated);

            // Nhap Ten Loai Hang
            //string tenLoaiHangInput = InputAndValidate_TenloaiHang(false);
            string tenLoaiHangInput = CommonFunctions.InputAndValidate_String(false, LoaiHang.VN_TEN_LOAI_HANG, 50);

            Console.WriteLine("================= PHẦN XÁC NHẬN THÔNG TIN VỪA NHẬP =================");

            Console.WriteLine("Xin vui lòng xác nhận lại thông tin loại hàng bạn vừa nhập dưới đây.");
            Console.WriteLine("Trong trường hợp đúng, vui lòng nhấn bất kỳ phím nào để lưu thông tin");
            Console.WriteLine("Trong trường hợp sai, vui lòng nhấn phím s để thoát ra");

            Console.WriteLine("========= THÔNG TIN LOẠI HÀNG VỪA NHẬP =========");
            Console.WriteLine("Mã loại hàng: {0}", maLoaiHangInput);
            Console.WriteLine("Tên loại hàng: {0}", tenLoaiHangInput);
            Console.WriteLine("========= KẾT THÚC THÔNG TIN LOẠI HÀNG VỪA NHẬP =========");

            string userInput = Console.ReadLine();

            if (userInput.ToLower() != "s")
            {
                LoaiHangList.Add(new LoaiHang(maLoaiHangInput, tenLoaiHangInput));
                Console.WriteLine("Bạn đã nhập thành công loại hàng trên");
                Console.WriteLine("==================================");
            }           
            else
            {
                Console.WriteLine("Bạn đã từ bỏ các thông tin loại hàng vừa nhập");
                Console.WriteLine("==================================");
            }        
        }

        #endregion

        #region Search Loai hang

        public static void ProcessSearchLoaiHang()
        {
            if (LoaiHangList.Count > 0)
            {
                while (true)
                {
                    Console.WriteLine("========== TÌM KIẾM LOẠI HÀNG ==========");
                    Console.WriteLine("Xin vui lòng nhập vào các điều kiện để tìm kiếm, trong trường hợp không cần điều kiện nào đó thì cứ việc bỏ trống");                  
                    Console.WriteLine("Mã loại hàng: ");
                    string userInput_maLoaiHang = Console.ReadLine().Trim();
                    Console.WriteLine("Tên loại hàng: ");
                    string userInput_tenLoaiHang = Console.ReadLine().Trim();

                    var resultList = SearchLoaiHangBasedOnConditions(userInput_maLoaiHang, userInput_tenLoaiHang);

                    if (resultList.Count > 0)
                    {
                        Console.WriteLine("========== KẾT QUẢ DANH SÁCH LOẠI HÀNG ==========");
                        OutputLoaiHangOnList(resultList);
                        Console.WriteLine("========== KẾT THÚC KẾT QUẢ DANH SÁCH LOẠI HÀNG ==========");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Không có bất kỳ loại hàng nào macthed với điều kiện được nhập");
                        Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                        Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                        string userInput = Console.ReadLine();
                        if (userInput == "exit")
                        {
                            break;
                        }
                    }
                }               
            }
            else
            {
                Console.WriteLine("Không có bất kỳ loại hàng nào được lưu trong hệ thống");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.ReadLine();
            }
        }

        private static List<LoaiHang> SearchLoaiHangBasedOnConditions(string maLoaiHang, string tenLoaiHang)
        {
            List<LoaiHang> resultList = new List<LoaiHang>();

            for (int i = 0; i < LoaiHangList.Count; i++)
            {
                var currentLoaiHang = LoaiHangList[i];

                if (currentLoaiHang != null)
                {
                    // Process MaLoaiHang condition
                    bool isMaLoaiHangMatched = !string.IsNullOrWhiteSpace(maLoaiHang) ? currentLoaiHang.MaLoaiHang.ToLower().Contains(maLoaiHang.ToLower()) : true;

                    // Process TenLoaiHang condition
                    bool isTenLoaiHangMatched = !string.IsNullOrWhiteSpace(tenLoaiHang) ? currentLoaiHang.TenLoaiHang.ToLower().Contains(tenLoaiHang.ToLower()) : true;

                    if (isMaLoaiHangMatched && isTenLoaiHangMatched)
                    {
                        resultList.Add(currentLoaiHang);
                    }
                }
            }

            return resultList;
        }

        #endregion

        #region Sua loai hang
        public static void ProcessEditLoaiHang()
        {
            if (LoaiHangList.Count > 0)
            {
                GetOneLoaiHangToAction(EditLoaiHang, CommonConstant.EDIT_TAG);
            }
            else
            {
                Console.WriteLine("Không có bất kỳ loại hàng nào được lưu trong hệ thống");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.ReadLine();
            }
        }

        private static void EditLoaiHang(LoaiHang loaiHang)
        {
            Console.WriteLine("========== TIẾN HÀNH CHỈNH SỬA LOẠI HÀNG ==========");
            Console.WriteLine("LƯU Ý: Trong trường hợp đối với các thuộc tính mà bạn muốn giữ nguyên giá trị hiện tại, vui lòng bỏ trống và nhấn enter");
            Console.WriteLine("Chỉnh sửa thông tin Mã loại hàng");
            string maLoaiHangInput = CommonFunctions.InputAndValidate_String(true, LoaiHang.VN_MA_LOAI_HANG, 50, IsMaLoaiHangDuplicated);
            string tenLoaiHangInput = CommonFunctions.InputAndValidate_String(true, LoaiHang.VN_TEN_LOAI_HANG, 50);

            Console.WriteLine("================= PHẦN XÁC NHẬN THÔNG TIN CHỈNH SỬA VỪA NHẬP =================");

            Console.WriteLine("Xin vui lòng xác nhận lại thông tin loại hàng bạn vừa nhập dưới đây.");
            Console.WriteLine("Trong trường hợp đúng, vui lòng nhấn bất kỳ phím nào để lưu thông tin");
            Console.WriteLine("Trong trường hợp sai, vui lòng nhấn phím s để thoát ra");

            Console.WriteLine("========= THÔNG TIN LOẠI HÀNG CẦN CHỈNH SỬA VỪA NHẬP =========");
            Console.WriteLine("Mã loại hàng: {0}", string.IsNullOrWhiteSpace(maLoaiHangInput) ? " (giữ nguyên)" : maLoaiHangInput);
            Console.WriteLine("Tên loại hàng: {0}", string.IsNullOrWhiteSpace(tenLoaiHangInput) ? " (giữ nguyên)" : tenLoaiHangInput);
            Console.WriteLine("========= KẾT THÚC THÔNG TIN LOẠI HÀNG CẦN CHỈNH SỬA VỪA NHẬP =========");

            string userInput = Console.ReadLine();

            if (userInput.ToLower() != "s")
            {
                // Process edit
                // Edit Ma Loai Hang
                if (!string.IsNullOrWhiteSpace(maLoaiHangInput))
                {
                    loaiHang.MaLoaiHang = maLoaiHangInput;
                }
                // Edit Ten Loai Hang
                if (!string.IsNullOrWhiteSpace(tenLoaiHangInput))
                {
                    loaiHang.TenLoaiHang = tenLoaiHangInput;
                }

                Console.WriteLine("Bạn đã chỉnh sửa thành công loại hàng trên");
                Console.WriteLine("==================================");
            }
            else
            {
                Console.WriteLine("Bạn đã từ bỏ các thông tin loại hàng vừa nhập");
                Console.WriteLine("==================================");
            }
        }

        #endregion

        #region Xoa Loai hang
        public static void ProcessDeleteLoaiHang()
        {
            if (LoaiHangList.Count > 0)
            {
                GetOneLoaiHangToAction(DeleteLoaiHang, CommonConstant.DELETE_TAG);
            }
            else
            {
                Console.WriteLine("Không có bất kỳ loại hàng nào được lưu trong hệ thống");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.ReadLine();
            }
        }

        private static void DeleteLoaiHang(LoaiHang loaiHang)
        {
            LoaiHangList.Remove(loaiHang);
            Console.WriteLine("Bạn đã xóa thành công loại hàng trên");
            Console.WriteLine("==================================");
        }
        
        #endregion

        #region Common functions

        public static string OutputMotLoaiHang(LoaiHang loaiHang)
        {
            return String.Format("{0} : {1}, {2} : {3}", LoaiHang.VN_MA_LOAI_HANG, loaiHang.MaLoaiHang, LoaiHang.VN_TEN_LOAI_HANG, loaiHang.TenLoaiHang);
        }

        public static LoaiHang GetLoaiHangMacDinh()
        {
            return LoaiHangList.FindAll(l => l.MaLoaiHang == CommonConstant.MA_LOAI_HANG_DEFAULT).FirstOrDefault();
        }

        public static LoaiHang GetOneLoaiHangToAction(Action<LoaiHang> action = null, string tag = "")
        { 
            while (true)
            {
                Console.WriteLine("========== TÌM KIẾM LOẠI HÀNG ĐỂ {0}==========", tag);
                Console.WriteLine("Xin vui lòng nhập vào Mã Loại hàng để tìm kiếm");
                Console.WriteLine("Mã loại hàng: ");
                string userInput = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Điều kiện tìm kiếm không được để trống. Xin vui lòng nhập lại");
                    Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                    Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                    userInput = Console.ReadLine();
                    if (userInput == "exit")
                    {
                        return null;
                    }
                    else
                    {
                        continue;
                    }
                }

                var resultList = SearchLoaiHangBasedOnConditions(userInput, null);

                if (resultList.Count == 0)
                {
                    Console.WriteLine("Không có bất kỳ loại hàng nào đáp ứng với điều kiện được nhập");
                    Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                    Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                    userInput = Console.ReadLine();
                    if (userInput == "exit")
                    {
                        return null;
                    }
                }
                else if (resultList.Count > 1)
                {
                    Console.WriteLine("Có nhiều hơn một loại hàng có mã loại hàng vừa nhập, nên không thể {0}.", tag.ToLower());
                    Console.WriteLine("Hệ thống chỉ cho phép {0} một loại mã hàng có mã loại hàng là duy nhất", tag.ToLower());
                    Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                    Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                    userInput = Console.ReadLine();
                    if (userInput == "exit")
                    {
                        return null;
                    }
                }
                else if (resultList[0].MaLoaiHang == CommonConstant.MA_LOAI_HANG_DEFAULT)
                {
                    Console.WriteLine("Không thể {0} Loại hàng default", tag.ToLower());
                    Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                    Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                    userInput = Console.ReadLine();
                    if (userInput == "exit")
                    {
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("========== THÔNG TIN LOẠI HÀNG CẦN {0} ==========", tag);
                    OutputLoaiHangOnList(resultList);
                    Console.WriteLine("========== KẾT THÚC THÔNG TIN  LOẠI HÀNG CẦN {0} ==========", tag);
                    Console.WriteLine("Sau khi kiểm tra thông tin bên trên, nếu đúng loại hàng cần {0}, vui lòng nhấn bất kỳ phím nào để tiến hành {0}", tag.ToLower());
                    Console.WriteLine("Trong trường hợp không đúng thông tin, vui lòng gõ phím S để kết thúc");

                    userInput = Console.ReadLine();

                    if (userInput.ToLower() != "s")
                    {
                        if(action != null)
                            action.Invoke(resultList[0]);
                        return resultList[0];
                    }
                    else
                    {
                        Console.WriteLine("Bạn đã từ bỏ phần {0} loại hàng", tag.ToLower());
                        Console.WriteLine("==================================");
                        return null;
                    }
                }
            }
        }
        
        private static void OutputLoaiHangOnList(List<LoaiHang> targetList)
        {
            int sothutu = 1;
            for (int i = 0; i < targetList.Count; i++)
            {
                var currentLoaiHang = targetList[i];
                if (currentLoaiHang != null)
                {
                    Console.WriteLine("{0}. {1} : {2}, {3} : {4}",
                        sothutu, LoaiHang.VN_MA_LOAI_HANG, currentLoaiHang.MaLoaiHang, 
                        LoaiHang.VN_TEN_LOAI_HANG ,currentLoaiHang.TenLoaiHang);

                    sothutu++;
                }
            }
        }

        private static bool IsMaLoaiHangDuplicated(string maLoaiHang)
        {
            foreach (var curLoaiHang in LoaiHangList)
            {
                if (curLoaiHang.MaLoaiHang == maLoaiHang.Trim())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion 
    }
}
