using System;
using System.Collections.Generic;

namespace QueueAssignment
{
    // ==========================================
    // 1. CÁCH 1: SỬ DỤNG DANH SÁCH LIÊN KẾT
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

    public class LinkedListQueue
    {
        private Node front, rear;

        public LinkedListQueue()
        {
            front = rear = null;
        }

        public void Enqueue(int data)
        {
            Node newNode = new Node(data);
            if (rear == null)
            {
                front = rear = newNode;
                return;
            }
            rear.Next = newNode;
            rear = newNode;
        }

        public void PrintQueue()
        {
            Node current = front;
            Console.Write("Hang doi (Danh sach lien ket): ");
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    // ==========================================
    // 2. CÁCH 2: SỬ DỤNG MẢNG MỘT CHIỀU
    // ==========================================
    public class ArrayQueue
    {
        private int[] arr;
        private int front, rear, capacity;

        public ArrayQueue(int size)
        {
            capacity = size;
            arr = new int[capacity];
            front = 0;
            rear = -1;
        }

        public void Enqueue(int data)
        {
            if (rear == capacity - 1)
            {
                Console.WriteLine("Hang doi day!");
                return;
            }
            arr[++rear] = data;
        }

        public void PrintQueue()
        {
            Console.Write("Hang doi (Mang mot chieu): ");
            for (int i = front; i <= rear; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }
    }

    // ==========================================
    // 3. CÁCH 3: SỬ DỤNG MẢNG VÒNG (CẢI TIẾN)
    // ==========================================
    public class CircularQueue
    {
        private int[] arr;
        private int front, rear, capacity, count;

        public CircularQueue(int size)
        {
            capacity = size;
            arr = new int[capacity];
            front = 0;
            rear = -1;
            count = 0;
        }

        public void Enqueue(int data)
        {
            if (count == capacity)
            {
                Console.WriteLine("Hang doi day!");
                return;
            }
            rear = (rear + 1) % capacity;
            arr[rear] = data;
            count++;
        }

        public void PrintQueue()
        {
            Console.Write("Hang doi (Mang vong): ");
            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % capacity;
                Console.Write(arr[index] + " ");
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
            // Chuỗi số theo yêu cầu đề bài
            int[] numbers = { 234, 77, 0, 113, 404, 94, 900, 113, 15, 300, 13, 135 };

            Console.WriteLine("=== BAI 1: XAY DUNG HANG DOI ===");
            
            // 1. Danh sách liên kết
            LinkedListQueue llQueue = new LinkedListQueue();
            foreach (var num in numbers) llQueue.Enqueue(num);
            llQueue.PrintQueue();

            // 2. Mảng một chiều
            ArrayQueue arrQueue = new ArrayQueue(numbers.Length);
            foreach (var num in numbers) arrQueue.Enqueue(num);
            arrQueue.PrintQueue();

            // 3. Mảng vòng
            CircularQueue circQueue = new CircularQueue(numbers.Length);
            foreach (var num in numbers) circQueue.Enqueue(num);
            circQueue.PrintQueue();


            Console.WriteLine("\n=== BAI 2: TIM SO NHO NHAT VA LON NHAT TRONG HANG DOI ===");
            Queue<int> standardQueue = new Queue<int>(numbers);

            if (standardQueue.Count > 0)
            {
                int min = standardQueue.Peek();
                int max = standardQueue.Peek();

                foreach (var item in standardQueue)
                {
                    if (item < min) min = item;
                    if (item > max) max = item;
                }

                Console.WriteLine($"So nho nhat trong hang doi la: {min}");
                Console.WriteLine($"So lon nhat trong hang doi la: {max}");
            }
        }
    }
}