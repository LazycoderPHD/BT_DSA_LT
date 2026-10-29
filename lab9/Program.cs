using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Thiết lập hiển thị tiếng Việt có dấu
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int choice;

        do
        {
            Console.WriteLine("\n================ CHƯƠNG TRÌNH ÔN TẬP TUẦN 09 ================");
            Console.WriteLine("1. Bài 1: Tìm kiếm nhị phân trên mảng giảm dần (Có nhập dữ liệu)");
            Console.WriteLine("2. Bài 2: Sắp xếp dãy số (Bubble Sort & Merge Sort)");
            Console.WriteLine("3. Bài 3: Mô phỏng Ngăn xếp (Stack) tối đa 5 phần tử");
            Console.WriteLine("0. Thoát chương trình");
            Console.Write("Nhập lựa chọn của bạn (0-3): ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    RunBai1();
                    break;
                case 2:
                    RunBai2();
                    break;
                case 3:
                    RunBai3();
                    break;
                case 0:
                    Console.WriteLine("Đang thoát chương trình. Tạm biệt!");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không tồn tại. Vui lòng chọn từ 0 đến 3!");
                    break;
            }

            if (choice != 0)
            {
                Console.WriteLine("\nNhấn phím bất kỳ để quay lại menu chính...");
                Console.ReadKey();
            }

        } while (choice != 0);
    }

    // ==========================================
    // BÀI 1: TÌM KIẾM NHỊ PHÂN TRÊN MẢNG GIẢM DẦN
    // ==========================================
    static void RunBai1()
    {
        Console.WriteLine("\n--- BÀI 1: TÌM KIẾM NHỊ PHÂN (MẢNG GIẢM DẦN) ---");
        Console.Write("Nhập số lượng phần tử của mảng: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Số lượng phần tử không hợp lệ!");
            return;
        }

        int[] arr = new int[n];
        Console.WriteLine($"Nhập {n} phần tử cho mảng:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"  Phần tử thứ {i + 1}: ");
            arr[i] = int.Parse(Console.ReadLine());
        }

        // Tự động sắp xếp mảng giảm dần để thỏa mãn điều kiện tìm kiếm nhị phân giảm dần
        Array.Sort(arr);
        Array.Reverse(arr);

        Console.WriteLine("\n-> Mảng sau khi sắp xếp giảm dần là:");
        Console.WriteLine(string.Join(", ", arr));

        Console.Write("\nNhập số cần tìm trong mảng (x): ");
        int x = int.Parse(Console.ReadLine());

        int result = BinarySearchDescending(arr, x);

        if (result != -1)
            Console.WriteLine($"Kết quả: Tìm thấy {x} tại vị trí chỉ số (index) {result}.");
        else
            Console.WriteLine($"Kết quả: Không tìm thấy {x} trong mảng.");
    }

    static int BinarySearchDescending(int[] arr, int x)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (arr[mid] == x)
                return mid;

            // Vì mảng giảm dần nên nếu x lớn hơn arr[mid], ta tìm ở nửa bên trái
            if (arr[mid] < x)
            {
                right = mid - 1;
            }
            else // x nhỏ hơn arr[mid], tìm ở nửa bên phải
            {
                left = mid + 1;
            }
        }
        return -1;
    }

    // ==========================================
    // BÀI 2: SẮP XẾP BUBBLE SORT VÀ MERGE SORT
    // ==========================================
    static void RunBai2()
    {
        Console.WriteLine("\n--- BÀI 2: SẮP XẾP DÃY SỐ ---");
        Console.WriteLine("Dãy số ban đầu: 14, 87, 3, 42, 95, 28, 61, 76, 19, 50, 33, 81");
        int[] originalArr = { 14, 87, 3, 42, 95, 28, 61, 76, 19, 50, 33, 81 };

        // 1. Chạy Bubble Sort
        int[] arrBubble = (int[])originalArr.Clone();
        Console.WriteLine("\n--- 1. Quá trình BUBBLE SORT (Tăng dần) ---");
        PrintArray(arrBubble, "Ban đầu   ");

        int n = arrBubble.Length;
        int step = 1;
        bool swapped;
        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arrBubble[j] > arrBubble[j + 1])
                {
                    int temp = arrBubble[j];
                    arrBubble[j] = arrBubble[j + 1];
                    arrBubble[j + 1] = temp;
                    swapped = true;

                    PrintArray(arrBubble, $"Đổi chỗ {step++}");
                }
            }
            if (!swapped) break;
        }

        // 2. Chạy Merge Sort
        int[] arrMerge = (int[])originalArr.Clone();
        Console.WriteLine("\n--- 2. Quá trình MERGE SORT ---");
        PrintArray(arrMerge, "Ban đầu   ");
        int mergeStep = 1;
        MergeSort(arrMerge, 0, arrMerge.Length - 1, ref mergeStep);
    }

    static void PrintArray(int[] arr, string message)
    {
        Console.WriteLine($"{message}: [{string.Join(", ", arr)}]");
    }

    static void MergeSort(int[] arr, int left, int right, ref int step)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;
            MergeSort(arr, left, mid, ref step);
            MergeSort(arr, mid + 1, right, ref step);
            Merge(arr, left, mid, right, ref step);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right, ref int step)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        Array.Copy(arr, left, L, 0, n1);
        Array.Copy(arr, mid + 1, R, 0, n2);

        int i = 0, j = 0, k = left;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j]) arr[k++] = L[i++];
            else arr[k++] = R[j++];
        }

        while (i < n1) arr[k++] = L[i++];
        while (j < n2) arr[k++] = R[j++];

        PrintArray(arr, "Trộn bước {step++}");
    }

    // ==========================================
    // BÀI 3: MÔ PHỎNG NGĂN XẾP (STACK) TỐI ĐA 5 PHẦN TỬ
    // ==========================================
    static void RunBai3()
    {
        Console.WriteLine("\n--- BÀI 3: MÔ PHỎNG NGĂN XẾP (STACK) TỐI ĐA 5 PHẦN TỬ ---");
        int maxSize = 5;
        int[] stkArray = new int[maxSize];
        int stkTop = -1;

        // Chuỗi thao tác theo đề bài
        var operations = new List<(string op, int val)>
        {
            ("Push", 14), ("Push", 87), ("Pop", 0), ("Pop", 0), ("Pop", 0),
            ("Push", 3), ("Push", 42), ("Pop", 0), ("Push", 95), ("Push", 28),
            ("Push", 61), ("Push", 76), ("Push", 19), ("Push", 50), ("Pop", 0),
            ("Pop", 0), ("Push", 33), ("Push", 81), ("Pop", 0), ("Pop", 0), ("Pop", 0)
        };

        Console.WriteLine($"\n{"STT",-4} | {"Thao tác",-12} | {"stkTop",-6} | {"N",-4} | {"stkArray (Đáy -> Đỉnh)"}");
        Console.WriteLine(new string('-', 60));

        for (int idx = 0; idx < operations.Count; idx++)
        {
            var (op, val) = operations[idx];
            string opStr = op == "Push" ? $"Push({val})" : "Pop()";

            if (op == "Push")
            {
                if (stkTop < maxSize - 1)
                {
                    stkTop++;
                    stkArray[stkTop] = val;
                }
                // Nếu Stack đầy thì bỏ qua (hoặc báo tràn stack)
            }
            else // Pop
            {
                if (stkTop >= 0)
                {
                    stkTop--;
                }
                // Nếu Stack rỗng thì bỏ qua (hoặc báo thiếu stack)
            }

            int n = stkTop + 1;
            List<int> currentStack = new List<int>();
            for (int i = 0; i <= stkTop; i++)
            {
                currentStack.Add(stkArray[i]);
            }
            string arrStr = currentStack.Count > 0 ? "[" + string.Join(", ", currentStack) + "]" : "[]";

            Console.WriteLine($"{idx + 1,-4} | {opStr,-12} | {stkTop,-6} | {n,-4} | {arrStr}");
        }
    }
}