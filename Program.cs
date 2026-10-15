using System;

namespace StackAssignment
{
    // ==========================================
    // CÁCH 1: NGĂN XẾP SỬ DỤNG DANH SÁCH LIÊN KẾT
    // ==========================================
    public class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedStack
    {
        private Node top;

        public LinkedStack()
        {
            top = null;
        }

        public bool IsEmpty()
        {
            return top == null;
        }

        public void Push(int value)
        {
            Node newNode = new Node(value);
            newNode.Next = top;
            top = newNode;
        }

        public int Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Ngăn xếp trống!");
            }
            int poppedData = top.Data;
            top = top.Next;
            return poppedData;
        }

        public int Peek()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Ngăn xếp trống!");
            }
            return top.Data;
        }

        // Tìm số nhỏ nhất và lớn nhất trong ngăn xếp (danh sách liên kết)
        public void FindMinMax(out int min, out int max)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Ngăn xếp trống!");
            }

            min = top.Data;
            max = top.Data;

            Node current = top.Next;
            while (current != null)
            {
                if (current.Data < min) min = current.Data;
                if (current.Data > max) max = current.Data;
                current = current.Next;
            }
        }

        public void Display()
        {
            Node current = top;
            Console.Write("Stack (Danh sach lien ket - tu Dinh xuong Day): ");
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    // ==========================================
    // CÁCH 2: NGĂN XẾP SỬ DỤNG MẢNG MỘT CHIỀU
    // ==========================================
    public class ArrayStack
    {
        private int[] elements;
        private int top;
        private int capacity;

        public ArrayStack(int size)
        {
            capacity = size;
            elements = new int[capacity];
            top = -1;
        }

        public bool IsEmpty()
        {
            return top == -1;
        }

        public bool IsFull()
        {
            return top == capacity - 1;
        }

        public void Push(int value)
        {
            if (IsFull())
            {
                throw new InvalidOperationException("Ngăn xếp bị tràn (Full)!");
            }
            elements[++top] = value;
        }

        public int Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Ngăn xếp trống!");
            }
            return elements[top--];
        }

        public int Peek()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Ngăn xếp trống!");
            }
            return elements[top];
        }

        // Tìm số nhỏ nhất và lớn nhất trong ngăn xếp (mảng một chiều)
        public void FindMinMax(out int min, out int max)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Ngăn xếp trống!");
            }

            min = elements[0];
            max = elements[0];

            for (int i = 1; i <= top; i++)
            {
                if (elements[i] < min) min = elements[i];
                if (elements[i] > max) max = elements[i];
            }
        }

        public void Display()
        {
            Console.Write("Stack (Mang mot chieu - tu Day len Dinh): ");
            for (int i = 0; i <= top; i++)
            {
                Console.Write(elements[i] + " ");
            }
            Console.WriteLine();
        }
    }

    // ==========================================
    // CHƯƠNG TRÌNH CHÍNH
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập mã hóa tiếng Việt cho Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] chuoiSo = { 234, 77, 0, 113, 404, 94, 900, 113, 15, 300, 13, 135 };

            Console.WriteLine("=== BÀI 1 & BÀI 2: XÂY DỰNG NGĂN XẾP VÀ TÌM MIN/MAX ===");
            Console.WriteLine("Chuỗi số ban đầu: " + string.Join(", ", chuoiSo));
            Console.WriteLine("------------------------------------------------------------------");

            // --- CÁCH 1: DÙNG DANH SÁCH LIÊN KẾT ---
            Console.WriteLine("\n--- CÁCH 1: SỬ DỤNG DANH SÁCH LIÊN KẾT ---");
            LinkedStack linkedStack = new LinkedStack();
            foreach (int num in chuoiSo)
            {
                linkedStack.Push(num);
            }
            linkedStack.Display();

            // Tìm Min/Max cho Danh sách liên kết
            linkedStack.FindMinMax(out int minLinked, out int maxLinked);
            Console.WriteLine($"[Bài 2] Số nhỏ nhất trong Stack (DSLK): {minLinked}");
            Console.WriteLine($"[Bài 2] Số lớn nhất trong Stack (DSLK): {maxLinked}");

            // --- CÁCH 2: DÙNG MẢNG MỘT CHIỀU ---
            Console.WriteLine("\n--- CÁCH 2: SỬ DỤNG MẢNG MỘT CHIỀU ---");
            ArrayStack arrayStack = new ArrayStack(chuoiSo.Length);
            foreach (int num in chuoiSo)
            {
                arrayStack.Push(num);
            }
            arrayStack.Display();

            // Tìm Min/Max cho Mảng một chiều
            arrayStack.FindMinMax(out int minArray, out int maxArray);
            Console.WriteLine($"[Bài 2] Số nhỏ nhất trong Stack (Mảng): {minArray}");
            Console.WriteLine($"[Bài 2] Số lớn nhất trong Stack (Mảng): {maxArray}");

            Console.WriteLine("\n------------------------------------------------------------------");
        }
    }
}