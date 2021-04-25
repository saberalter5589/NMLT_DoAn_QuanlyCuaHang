using ConsoleApp1.Commons;
using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
            string maLoaiHangInput = InputAndValidate_MaLoaiHang(false);

            // Nhap Ten Loai Hang
            string tenLoaiHangInput = InputAndValidate_TenloaiHang(false);

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

        private static string InputAndValidate_MaLoaiHang(bool isEdit)
        {
            // Input Ma loai hang
            Console.WriteLine("==================================");
            Console.WriteLine("Xin vui lòng nhập vào Mã loại hàng");
            string userInput = Console.ReadLine().Trim();

            while (true)
            {
                if (!isEdit && string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Mã loại hàng không chấp nhận giá trị rỗng, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                else if (userInput.Length > 20)
                {
                    Console.WriteLine("Mã loại hàng chỉ chấp nhận chuỗi có độ dài tối đa 20 ký tự, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                else if (IsMaLoaiHangDuplicated(userInput))
                {
                    Console.WriteLine("Mã loại hàng này đã tồn tại. Vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                // In case all validations is pass
                else
                {
                    Console.WriteLine("Mã loại hàng bạn vừa nhập là {0}", userInput);                   
                    break;
                }
            }

            return userInput;
        }

        private static string InputAndValidate_TenloaiHang(bool isEdit)
        {
            // Input Ten loai hang
            Console.WriteLine("==================================");
            Console.WriteLine("Xin vui lòng nhập vào Tên loại hàng");
            string userInput = Console.ReadLine().Trim();

            while (true)
            {
                if (!isEdit && string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Tên loại hàng không chấp nhận giá trị rỗng, vui lòng nhập lại");
                    userInput = Console.ReadLine();
                }
                else if (userInput.Length > 50)
                {
                    Console.WriteLine("Mã loại hàng chỉ chấp nhận chuỗi có độ dài tối đa 50 ký tự, vui lòng nhập lại");
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
                while (true)
                {
                    Console.WriteLine("========== TÌM KIẾM LOẠI HÀNG ĐỂ CHỈNH SỬA==========");
                    Console.WriteLine("Xin vui lòng nhập vào Mã Loại hàng để tìm kiếm");
                    Console.WriteLine("Mã loại hàng: ");
                    string userInput = Console.ReadLine().Trim();
                    
                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        Console.WriteLine("Điều kiện tìm kiếm không được để trống. Xin vui lòng nhập lại");
                        Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục.");
                        Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                        userInput = Console.ReadLine();
                        if (userInput == "exit")
                        {
                            break;
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
                            break;
                        }
                    }
                    else if (resultList.Count > 1)
                    {
                        Console.WriteLine("Có nhiều hơn một loại hàng có mã loại hàng vừa nhập, nên không thể chỉnh sửa.");
                        Console.WriteLine("Hệ thống chỉ cho phép chỉnh sửa một loại mã hàng có mã loại hàng là duy nhất");
                        Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                        Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                        userInput = Console.ReadLine();
                        if (userInput == "exit")
                        {
                            break;
                        }
                    }
                    else if (resultList[0].MaLoaiHang == CommonConstant.MA_LOAI_HANG_DEFAULT)
                    {
                        Console.WriteLine("Không thể chỉnh sửa Loại hàng default");
                        Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                        Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                        userInput = Console.ReadLine();
                        if (userInput == "exit")
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("========== THÔNG TIN LOẠI HÀNG CẦN CHỈNH SỬA ==========");
                        OutputLoaiHangOnList(resultList);
                        Console.WriteLine("========== KẾT THÚC THÔNG TIN  LOẠI HÀNG CẦN CHỈNH SỬA ==========");
                        Console.WriteLine("Sau khi kiểm tra thông tin bên trên, nếu đúng loại hàng cần chỉnh sửa, vui lòng nhấn bất kỳ phím nào để tiến hành chỉnh sửa");
                        Console.WriteLine("Trong trường hợp không đúng thông tin, vui lòng gõ phím S để kết thúc");

                        userInput = Console.ReadLine();

                        if (userInput.ToLower() != "s")
                        {
                            EditLoaiHang(resultList[0]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Bạn đã từ bỏ phần chỉnh sửa loại hàng");
                            Console.WriteLine("==================================");
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

        private static void EditLoaiHang(LoaiHang loaiHang)
        {
            Console.WriteLine("========== TIẾN HÀNH CHỈNH SỬA LOẠI HÀNG ==========");
            Console.WriteLine("LƯU Ý: Trong trường hợp đối với các thuộc tính mà bạn muốn giữ nguyên giá trị hiện tại, vui lòng bỏ trống và nhấn enter");
            Console.WriteLine("Chỉnh sửa thông tin Mã loại hàng");
            string maLoaiHangInput = InputAndValidate_MaLoaiHang(true);
            string tenLoaiHangInput = InputAndValidate_TenloaiHang(true);

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
                while (true)
                {
                    Console.WriteLine("========== TÌM KIẾM LOẠI HÀNG ĐỂ XÓA==========");
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
                            break;
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
                            break;
                        }
                    }
                    else if (resultList.Count > 1)
                    {
                        Console.WriteLine("Có nhiều hơn một loại hàng có mã loại hàng vừa nhập, nên không thể xóa.");
                        Console.WriteLine("Hệ thống chỉ cho phép xóa một loại mã hàng có mã loại hàng là duy nhất");
                        Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                        Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                        userInput = Console.ReadLine();
                        if (userInput == "exit")
                        {
                            break;
                        }
                    }
                    else if (resultList[0].MaLoaiHang == CommonConstant.MA_LOAI_HANG_DEFAULT)
                    {
                        Console.WriteLine("Không thể xóa Loại hàng default");
                        Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                        Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                        userInput = Console.ReadLine();
                        if (userInput == "exit")
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("========== THÔNG TIN LOẠI HÀNG CẦN XÓA ==========");
                        OutputLoaiHangOnList(resultList);
                        Console.WriteLine("========== KẾT THÚC THÔNG TIN  LOẠI HÀNG CẦN XÓA ==========");
                        Console.WriteLine("Sau khi kiểm tra thông tin bên trên, nếu đúng loại hàng cần xóa, vui lòng nhấn bất kỳ phím nào để tiến hành xóa");
                        Console.WriteLine("Trong trường hợp không đúng thông tin, vui lòng gõ phím S để kết thúc");

                        userInput = Console.ReadLine();

                        if (userInput.ToLower() != "s")
                        {
                            DeleteLoaiHang(resultList[0]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Bạn đã từ bỏ phần xóa loại hàng");
                            Console.WriteLine("==================================");
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

        private static void DeleteLoaiHang(LoaiHang loaiHang)
        {
            LoaiHangList.Remove(loaiHang);
        }
        
        #endregion

        #region Common functions
        
        private static void OutputLoaiHangOnList(List<LoaiHang> targetList)
        {
            int sothutu = 1;
            for (int i = 0; i < targetList.Count; i++)
            {
                var currentLoaiHang = targetList[i];
                if (currentLoaiHang != null)
                {
                    Console.WriteLine("{0}. Mã loại hàng: {1}, Tên loại hàng: {2}",
                        sothutu, currentLoaiHang.MaLoaiHang, currentLoaiHang.TenLoaiHang);

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
