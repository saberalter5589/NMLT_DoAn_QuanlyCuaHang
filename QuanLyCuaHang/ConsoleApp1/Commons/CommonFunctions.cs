using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Commons
{
    public class CommonFunctions
    {
        public static string InputAndValidate_String(bool isEdit, string kindName, 
                                                        int maxLength, Predicate<string> checkDuplicate = null)
        {
            Console.WriteLine("==================================");
            Console.WriteLine("Xin vui lòng nhập vào {0}", kindName);
            string userInput = Console.ReadLine().Trim();

            while (true)
            {
                if (!isEdit && string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("{0} không chấp nhận giá trị rỗng, vui lòng nhập lại", kindName);
                    userInput = Console.ReadLine();
                }
                else if (userInput.Length > maxLength)
                {
                    Console.WriteLine("{0} chỉ chấp nhận chuỗi có độ dài tối đa {1} ký tự, vui lòng nhập lại", kindName, maxLength);
                    userInput = Console.ReadLine();
                }
                else if (checkDuplicate != null && checkDuplicate(userInput) == true)
                {
                    Console.WriteLine("{0} này đã tồn tại. Vui lòng nhập lại", kindName);
                    userInput = Console.ReadLine();
                }
                // In case all validations is pass
                else
                {
                    Console.WriteLine("{0} bạn vừa nhập là {1}", kindName, userInput);
                    break;
                }
            }

            return userInput;
        }

        public static DateTime InputAndValidate_DateTime(string kindName)
        {
            // Input and check day
            while (true)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("Xin vui lòng nhập ngày tháng năm của {0}", kindName);
                string userInput = Console.ReadLine().Trim();
                DateTime inputDayTime = IsDayTimeValid(userInput);
                if (inputDayTime != DateTime.MinValue)
                {
                    Console.WriteLine("Bạn vừa nhập ngày tháng năm của {0} là {1}/{2}/{3}", 
                        kindName, inputDayTime.Day, inputDayTime.Month, inputDayTime.Year);
                    return inputDayTime;
                }
            }
        }

        public static int InputAndValidate_Year(string kindName)
        {
            while (true)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("Xin vui lòng nhập năm của {0}", kindName);
                string userInput = Console.ReadLine().Trim();
                int inputYear = IsYearValid(userInput);
                if (inputYear > 0)
                {
                    Console.WriteLine("Bạn vừa nhập năm của {0} là {1}",
                        kindName, inputYear);
                    return inputYear;
                }
            }
        }

        public static DateTime IsDayTimeValid(string daytime)
        {
            try
            {
                string[] arrDayMonthYear = daytime.Split('/', '-');

                if (arrDayMonthYear.Length != 3)
                {
                    throw new Exception();
                }

                // Check day valid
                int day = IsDayValid(arrDayMonthYear[0]);
                int month = IsMonthValid(arrDayMonthYear[1]);
                int year = IsYearValid(arrDayMonthYear[2]);

                if (day == -1 || month == -1 || year == -1)
                {
                    throw new Exception();
                }

                return new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Bạn đã nhập ngày tháng không tồn tại, vui lòng nhập lại");
                return DateTime.MinValue;
            }
            catch (Exception)
            {
                Console.WriteLine("Bạn đã nhập sai format ngày tháng. Xin vui lòng nhập lại theo format sau:");
                Console.WriteLine("format 1: dd-mm-yyyy (vd: 31-01-2021) HOẶC format 2: dd/mm/yyyy (vd: 31/01/2021)");
                return DateTime.MinValue;
            }

     
        }

        public static int IsDayValid(string day) 
        {
            int dayAfterParsed;
            bool isDayParsedSuccess = int.TryParse(day, out dayAfterParsed);
            if (isDayParsedSuccess)
            {
                // Check if day in range 1-31
                if (dayAfterParsed >= 1 && dayAfterParsed <= 31)
                {
                    return dayAfterParsed;
                }
                else
                {
                    Console.WriteLine("NGÀY: Xin vui lòng nhập ngày trong khoảng từ 1 đến 31");
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("NGÀY: Xin vui lòng nhập ngày là ký tự số nguyên");
                return -1;
            }
        }

        public static int IsMonthValid(string month)
        {
            int monthAfterParsed;
            bool isMonthParsedSuccess = int.TryParse(month, out monthAfterParsed);
            if (isMonthParsedSuccess)
            {
                // Check if month in range 1-12
                if (monthAfterParsed >= 1 && monthAfterParsed <= 12)
                {
                    return monthAfterParsed;
                }
                else
                {
                    Console.WriteLine("THÁNG: Xin vui lòng nhập tháng trong khoảng từ 1 đến 12");
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("THÁNG: Xin vui lòng nhập tháng là ký tự số nguyên");
                return -1;
            }
        }

        public static int IsYearValid(string year)
        {
            int yearAfterParsed;
            bool isYearParsedSuccess = int.TryParse(year, out yearAfterParsed);
            if (isYearParsedSuccess && yearAfterParsed > 0)
            {
                return yearAfterParsed;
            }
            else
            {
                Console.WriteLine("NĂM: Xin vui lòng nhập năm là ký tự số nguyên lớn hơn 0");
                return -1;
            }
        }
    }
}
