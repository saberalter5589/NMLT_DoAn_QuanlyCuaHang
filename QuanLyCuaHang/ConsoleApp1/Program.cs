using ConsoleApp1.Controller;
using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            bool isProgramRunning = true;
            bool isUsingMatHangMenu;
            bool isUsingLoaiHangMenu;

            Console.WriteLine("Chào mừng đến với Chương trình Quản lý Cửa hàng version 1.0");

            while (isProgramRunning)
            {
                Console.WriteLine("*********** MAIN MENU ***********");
                Console.WriteLine("1. Thao tác với mặt hàng");
                Console.WriteLine("2. Thao tác với loại hàng");
                Console.WriteLine("3. Exit");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    // Thao tác với Mặt hàng menu
                    case "1":
                        isUsingMatHangMenu = true;
                        while (isUsingMatHangMenu)
                        {
                            Console.WriteLine("*********** MẶT HÀNG MENU ***********");
                            Console.WriteLine("1. In ra tất cả mặt hàng hiện có");
                            Console.WriteLine("2. Tìm kiếm một mặt hàng hiện có");
                            Console.WriteLine("3. Thêm mới một mặt hàng");
                            Console.WriteLine("4. Sửa một mặt hàng hiện có");
                            Console.WriteLine("5. Xóa một mặt hàng hiện có");
                            Console.WriteLine("6. Exit");

                            userInput = Console.ReadLine();

                            switch (userInput)
                            {
                                case "1":
                                    MatHangController.ProcessOutputAllCurrentLoaiHang();
                                    break;
                                case "2":
                                    MatHangController.ProcessSearchMatHang();
                                    break;
                                case "3":
                                    MatHangController.ProcessCreateMatHang();
                                    break;
                                case "4":
                                    MatHangController.ProcessEditMatHang();
                                    break;
                                case "5":

                                    break;
                                case "6":
                                    isUsingMatHangMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("Vui lòng nhập ký tự số từ 1 -> 6");
                                    break;
                            }
                        }
                        break;

                    // Thao tác với Loại hàng menu
                    case "2":
                        isUsingLoaiHangMenu = true;

                        while (isUsingLoaiHangMenu)
                        {
                            Console.WriteLine("*********** LOẠI HÀNG MENU ***********");
                            Console.WriteLine("1. In ra tất cả loại hàng hiện có");
                            Console.WriteLine("2. Tìm kiếm một loại hàng hiện có");
                            Console.WriteLine("3. Thêm mới một loại hàng");
                            Console.WriteLine("4. Sửa một loại hàng hiện có");
                            Console.WriteLine("5. Xóa một loại hàng hiện có");
                            Console.WriteLine("6. Exit");

                            userInput = Console.ReadLine();

                            switch (userInput)
                            {
                                case "1":
                                    LoaiHangController.ProcessOutputAllCurrentLoaiHang();
                                    break;
                                case "2":
                                    LoaiHangController.ProcessSearchLoaiHang();
                                    break;
                                case "3":
                                    LoaiHangController.ProcessCreateLoaiHang();
                                    break;
                                case "4":
                                    LoaiHangController.ProcessEditLoaiHang();
                                    break;
                                case "5":
                                    LoaiHangController.ProcessDeleteLoaiHang();
                                    break;
                                case "6":
                                    isUsingLoaiHangMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("Vui lòng nhập ký tự số từ 1 -> 6");
                                    break;
                            }
                        }
                        break;

                    // Thoát khỏi chương trình
                    case "3":
                        isProgramRunning = false;
                        break;

                    default:
                        Console.WriteLine("Vui lòng nhập ký tự số từ 1 -> 3");
                        break;
                }
            }
        }
    }
}
