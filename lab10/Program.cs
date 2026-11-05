using System;
using System.Text;

namespace BinarySearchTreeDemo
{
    // Khởi tạo lớp Node cho cây nhị phân
    public class Node
    {
        public int Data;
        public Node Left;
        public Node Right;

        public Node(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    // Khởi tạo lớp Cây nhị phân tìm kiếm (BST)
    public class BinarySearchTree
    {
        public Node Root;

        public BinarySearchTree()
        {
            Root = null;
        }

        // Thêm phần tử vào cây
        public void Insert(int data)
        {
            Root = InsertRec(Root, data);
        }

        private Node InsertRec(Node root, int data)
        {
            if (root == null)
            {
                return new Node(data);
            }

            if (data < root.Data)
            {
                root.Left = InsertRec(root.Left, data);
            }
            else if (data > root.Data)
            {
                root.Right = InsertRec(root.Right, data);
            }
            // Nếu data == root.Data thì bỏ qua vì BST không lưu giá trị trùng

            return root;
        }

        // 1.1. Hàm hỗ trợ vẽ cây trên Console (xoay ngang 90 độ: nhánh phải ở trên, nhánh trái ở dưới)
        public void PrintTree(Node root, string indent = "", bool isRight = true)
        {
            if (root != null)
            {
                PrintTree(root.Right, indent + (isRight ? "        " : " |      "), true);
                Console.WriteLine(indent + (isRight ? " /----- " : " \\----- ") + root.Data);
                PrintTree(root.Left, indent + (isRight ? " |      " : "        "), false);
            }
        }

        // 1.2. Duyệt NRL (Node - Right - Left)
        public void PrintNRL(Node root)
        {
            if (root != null)
            {
                Console.Write(root.Data + "  ");
                PrintNRL(root.Right);
                PrintNRL(root.Left);
            }
        }

        // 1.3. Duyệt LRN (Left - Right - Node)
        public void PrintLRN(Node root)
        {
            if (root != null)
            {
                PrintLRN(root.Left);
                PrintLRN(root.Right);
                Console.Write(root.Data + "  ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int[] arr = { 234, 77, 0, 113, 404, 94, 900, 113, 15, 300, 13, 135 };
            BinarySearchTree bst = new BinarySearchTree();

            // Chèn từng phần tử vào cây
            foreach (int num in arr)
            {
                bst.Insert(num);
            }

            // 1.1. Vẽ cây nhị phân tìm kiếm
            Console.WriteLine("=== 1.1. SƠ ĐỒ CÂY NHỊ PHÂN TÌM KIẾM (Xoay ngang) ===");
            bst.PrintTree(bst.Root);

            // 1.2. Xuất dữ liệu theo trình tự NRL
            Console.WriteLine("\n=== 1.2. DUYỆT CÂY THEO TRÌNH TỰ NRL ===");
            bst.PrintNRL(bst.Root);
            Console.WriteLine();

            // 1.3. Xuất dữ liệu theo trình tự LRN
            Console.WriteLine("\n=== 1.3. DUYỆT CÂY THEO TRÌNH TỰ LRN ===");
            bst.PrintLRN(bst.Root);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}