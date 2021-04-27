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
                ),
        };

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
            DateTime hanSuDungInput = CommonFunctions.InputAndValidate_DateTime(false, MatHang.VN_HAN_DUNG_MAT_HANG);

            // Nhap Cong ty san xuat
            string congTySanXuatInput = CommonFunctions.InputAndValidate_String(false, MatHang.VN_CONG_TY_SX_MAT_HANG, 50);

            // Nhap Nam san xuat
            int namSanXuat = CommonFunctions.InputAndValidate_Year(false, MatHang.VN_NAM_SAN_XUAT_MAT_HANG);

            // Nhap loai hang
            LoaiHang loaiHangInput = GetOneLoaiHangForMatHang(false);

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

        #region Search Mat hang
        public static void ProcessSearchMatHang()
        {
            if (MatHangList.Count > 0)
            {
                while (true)
                {
                    Console.WriteLine("========== TÌM KIẾM MẶT HÀNG ==========");
                    Console.WriteLine("Xin vui lòng nhập vào các điều kiện để tìm kiếm, trong trường hợp không cần điều kiện nào đó thì cứ việc bỏ trống");
                    Console.WriteLine(MatHang.VN_MA_MAT_HANG + ":");
                    string userInput_maMatHang = Console.ReadLine().Trim();

                    Console.WriteLine(MatHang.VN_TEN_MAT_HANG + ":");
                    string userInput_tenMatHang = Console.ReadLine().Trim();

                    Console.WriteLine(MatHang.VN_HAN_DUNG_MAT_HANG + ":");
                    DateTime userInput_hanSuDungInput = CommonFunctions.InputAndValidate_DateTime(true, MatHang.VN_HAN_DUNG_MAT_HANG);

                    Console.WriteLine(MatHang.VN_CONG_TY_SX_MAT_HANG + ":");
                    string userInput_congtySX = Console.ReadLine().Trim();

                    Console.WriteLine(MatHang.VN_NAM_SAN_XUAT_MAT_HANG + ":");
                    int userInput_namSX = CommonFunctions.InputAndValidate_Year(true, MatHang.VN_NAM_SAN_XUAT_MAT_HANG);

                    Console.WriteLine("{0} ({1}):", MatHang.VN_LOAI_HANG_MAT_HANG, LoaiHang.VN_MA_LOAI_HANG);
                    string userInput_LoaiHang_MaLoaiHang = Console.ReadLine().Trim();

                    Console.WriteLine("{0} ({1}):", MatHang.VN_LOAI_HANG_MAT_HANG, LoaiHang.VN_TEN_LOAI_HANG);
                    string userInput_LoaiHang_TenLoaiHang = Console.ReadLine().Trim();

                    var resultList = SearchMatHangBasedOnConditions(userInput_maMatHang, userInput_tenMatHang, userInput_hanSuDungInput,
                                            userInput_congtySX, userInput_namSX, userInput_LoaiHang_MaLoaiHang, userInput_LoaiHang_TenLoaiHang);

                    if (resultList.Count > 0)
                    {
                        Console.WriteLine("========== KẾT QUẢ DANH SÁCH MẶT HÀNG ==========");
                        OutputMatHangOnList(resultList);
                        Console.WriteLine("========== KẾT THÚC KẾT QUẢ DANH SÁCH MẶT HÀNG ==========");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Không có bất kỳ mặt hàng nào macthed với điều kiện được nhập");
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

        private static List<MatHang> SearchMatHangBasedOnConditions(string maMatHang, string tenMatHang, DateTime hanSuDung,
                                                                        string congTySanXuat, int namSanXuat, string maLoaiHang, string tenLoaiHang)
        {
            List<MatHang> resultList = new List<MatHang>();

            for (int i = 0; i < MatHangList.Count; i++)
            {
                var currentMatHang = MatHangList[i];

                if (currentMatHang != null)
                {
                    // Process Ma Mat hang condition
                    bool isMaMatHangMatched = !string.IsNullOrWhiteSpace(maMatHang) ? currentMatHang.MaMatHang.ToLower().Contains(maMatHang.ToLower()) : true;

                    // Process Ten Mat hang condition
                    bool isTenMatHangMatched = !string.IsNullOrWhiteSpace(tenMatHang) ? currentMatHang.TenMatHang.ToLower().Contains(tenMatHang.ToLower()) : true;

                    // Process Han su dung condition
                    bool isHanSuDungMatched = hanSuDung != DateTime.MinValue ? hanSuDung == currentMatHang.HanDung : true;

                    // Process Cong ty San xuat mat hang
                    bool isCongtySanxuatMatched = !string.IsNullOrWhiteSpace(congTySanXuat) ? currentMatHang.CongTySanXuat.ToLower().Contains(congTySanXuat.ToLower()) : true;

                    // Process Nam San xuat
                    bool isNamSanXuatMatched = namSanXuat != -1 ? currentMatHang.NamSanXuat == namSanXuat : true;

                    // Process LoaiHang
  
                    var loaiHangResultList = LoaiHangController.SearchLoaiHangBasedOnConditions(maLoaiHang, tenLoaiHang);
                    bool isLoaiHangMatched = loaiHangResultList.Count != 0 ? loaiHangResultList.Contains(currentMatHang.LoaiHang) : true;

                    if (isMaMatHangMatched && isTenMatHangMatched && isHanSuDungMatched 
                        && isCongtySanxuatMatched && isNamSanXuatMatched && isLoaiHangMatched)
                    {
                        resultList.Add(currentMatHang);
                    }
                }
            }

            return resultList;
        }

        #endregion

        #region Edit Mat hang
        public static void ProcessEditMatHang()
        {
            if (MatHangList.Count > 0)
            {
                GetOneMatHangToAction(EditMatHangHang, CommonConstant.EDIT_TAG);
            }
            else
            {
                Console.WriteLine("Không có bất kỳ mặt hàng nào được lưu trong hệ thống");
                Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                Console.ReadLine();
            }
        }

        private static void EditMatHangHang(MatHang matHang)
        {
            Console.WriteLine("========== TIẾN HÀNH CHỈNH SỬA MẶT HÀNG ==========");
            Console.WriteLine("LƯU Ý: Trong trường hợp đối với các thuộc tính mà bạn muốn giữ nguyên giá trị hiện tại, vui lòng bỏ trống và nhấn enter");

            Console.WriteLine("Chỉnh sửa thông tin Mã mặt hàng");
            string maMatHangInput = CommonFunctions.InputAndValidate_String(true, MatHang.VN_MA_MAT_HANG, 50, IsMaMatHangDuplicated);

            Console.WriteLine("Chỉnh sửa thông tin Tên mặt hàng");
            string tenMatHangInput = CommonFunctions.InputAndValidate_String(true, MatHang.VN_TEN_MAT_HANG, 50);

            Console.WriteLine("Chỉnh sửa thông tin Hạn sử dụng");
            DateTime hanSuDungInput = CommonFunctions.InputAndValidate_DateTime(true, MatHang.VN_HAN_DUNG_MAT_HANG);

            Console.WriteLine("Chỉnh sửa thông tin Công ty sản xuất");
            string congTySanXuatInput = CommonFunctions.InputAndValidate_String(true, MatHang.VN_CONG_TY_SX_MAT_HANG, 50);

            // Nhap Nam san xuat
            Console.WriteLine("Chỉnh sửa thông tin Năm sản xuất");
            int namSanXuat = CommonFunctions.InputAndValidate_Year(true, MatHang.VN_NAM_SAN_XUAT_MAT_HANG);

            // Nhap loai hang
            Console.WriteLine("Chỉnh sửa thông tin Loại Hàng");
            LoaiHang loaiHangInput = GetOneLoaiHangForMatHang(true);

            Console.WriteLine("================= PHẦN XÁC NHẬN THÔNG TIN CHỈNH SỬA VỪA NHẬP =================");

            Console.WriteLine("Xin vui lòng xác nhận lại thông tin mặt hàng bạn vừa nhập dưới đây.");
            Console.WriteLine("Trong trường hợp đúng, vui lòng nhấn bất kỳ phím nào để lưu thông tin");
            Console.WriteLine("Trong trường hợp sai, vui lòng nhấn phím s để thoát ra");

            Console.WriteLine("========= THÔNG TIN MẶT HÀNG CẦN CHỈNH SỬA VỪA NHẬP =========");
            Console.WriteLine("Mã mặt hàng: {0}", string.IsNullOrWhiteSpace(maMatHangInput) ? " (giữ nguyên)" : maMatHangInput);
            Console.WriteLine("Tên mặt hàng: {0}", string.IsNullOrWhiteSpace(tenMatHangInput) ? " (giữ nguyên)" : tenMatHangInput);
            Console.WriteLine("Hạn sử dụng: {0}", hanSuDungInput == DateTime.MinValue ? " (giữ nguyên)" : hanSuDungInput.ToString("dd/MM/yyyy"));
            Console.WriteLine("Công ty sản xuất: {0}", string.IsNullOrWhiteSpace(congTySanXuatInput) ? " (giữ nguyên)" : congTySanXuatInput);
            Console.WriteLine("Năm sản xuất: {0}", (namSanXuat == -1 )? " (giữ nguyên)" : namSanXuat.ToString());
            Console.WriteLine("Loại hàng: {0}", loaiHangInput == null ? " (giữ nguyên)" : LoaiHangController.OutputMotLoaiHang(loaiHangInput));

            Console.WriteLine("========= KẾT THÚC THÔNG TIN LOẠI HÀNG CẦN CHỈNH SỬA VỪA NHẬP =========");

            string userInput = Console.ReadLine();

            if (userInput.ToLower() != "s")
            {
                // Process edit
                // Edit Ma Mat Hang
                if (!string.IsNullOrWhiteSpace(maMatHangInput))
                {
                    matHang.MaMatHang = maMatHangInput;
                }

                // Edit Ten Mat Hang
                if (!string.IsNullOrWhiteSpace(tenMatHangInput))
                {
                    matHang.TenMatHang = tenMatHangInput;
                }

                // Edit Han su dung
                if (hanSuDungInput != DateTime.MinValue)
                {
                    matHang.HanDung = hanSuDungInput;
                }

                // Edit Cong ty SX
                if (!string.IsNullOrWhiteSpace(congTySanXuatInput))
                {
                    matHang.CongTySanXuat = congTySanXuatInput;
                }

                // Edit Nam san xuat
                if (namSanXuat != -1)
                {
                    matHang.NamSanXuat = namSanXuat;
                }

                // Edit Nam san xuat
                if (loaiHangInput != null)
                {
                    matHang.LoaiHang = loaiHangInput;
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

        private static LoaiHang GetOneLoaiHangForMatHang(bool isEdit)
        {
            LoaiHang loaiHangInput = LoaiHangController.GetOneLoaiHangToAction(null, CommonConstant.GET_TAG, true);

            if (loaiHangInput == null)
            {
                if (!isEdit)
                {
                    Console.WriteLine("LƯU Ý: Nếu bạn để Loại Hàng là giá trị rỗng thì hệ thống sẽ tự gán loại hàng mặc định cho mặt hàng của bạn");
                    return LoaiHangController.GetLoaiHangMacDinh();
                }
                else
                {
                    return null;
                }
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

        public static MatHang GetOneMatHangToAction(Action<MatHang> action = null, string tag = "")
        {
            while (true)
            {
                Console.WriteLine("========== TÌM KIẾM MẶT HÀNG ĐỂ {0}==========", tag);
                Console.WriteLine("Xin vui lòng nhập vào Mã mặt hàng để tìm kiếm");
                Console.WriteLine("Mã mặt hàng: ");
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

                var resultList = SearchMatHangBasedOnConditions(userInput, null, DateTime.MinValue, null, -1, null, null);

                if (resultList.Count == 0)
                {
                    Console.WriteLine("Không có bất kỳ mặt hàng nào đáp ứng với điều kiện được nhập");
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
                    Console.WriteLine("Có nhiều hơn một mặt hàng có mã loại hàng vừa nhập, nên không thể {0}.", tag.ToLower());
                    Console.WriteLine("Hệ thống chỉ cho phép {0} một loại mã hàng có mã mặt hàng là duy nhất", tag.ToLower());
                    Console.WriteLine("Nhấn một phím bất kỳ để tiếp tục");
                    Console.WriteLine("Trong trường hợp muốn thoát khỏi chức năng này, vui lòng gõ chữ exit và nhấn Enter");
                    userInput = Console.ReadLine();
                    if (userInput == "exit")
                    {
                        return null;
                    }
                }
                else if (resultList[0].MaMatHang == CommonConstant.MA_MAT_HANG_DEFAULT)
                {
                    Console.WriteLine("Không thể {0} mặt hàng default", tag.ToLower());
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
                    Console.WriteLine("========== THÔNG TIN MẶT HÀNG CẦN {0} ==========", tag);
                    OutputMatHangOnList(resultList);
                    Console.WriteLine("========== KẾT THÚC THÔNG TIN MẶT HÀNG CẦN {0} ==========", tag);
                    Console.WriteLine("Sau khi kiểm tra thông tin bên trên, nếu đúng loại hàng cần {0}, vui lòng nhấn bất kỳ phím nào để tiến hành {0}", tag.ToLower());
                    Console.WriteLine("Trong trường hợp không đúng thông tin, vui lòng gõ phím S để kết thúc");

                    userInput = Console.ReadLine();

                    if (userInput.ToLower() != "s")
                    {
                        if (action != null)
                            action.Invoke(resultList[0]);
                        return resultList[0];
                    }
                    else
                    {
                        Console.WriteLine("Bạn đã từ bỏ phần {0} mặt hàng", tag.ToLower());
                        Console.WriteLine("==================================");
                        return null;
                    }
                }
            }
        }

        #endregion
    }
}
