using System;
using System.Collections.Generic;
using System.Text;

namespace BaiTapOnline03_BST
{
    // 1. Định nghĩa lớp PhimAnh theo đề bài
    public class PhimAnh
    {
        public int maSo;        
        public string tenPhim;  
        public int soLuotXem;   
        public int danhGia;    
        public int namSanXuat;  

        public PhimAnh(int maSo, string tenPhim, int soLuotXem, int danhGia, int namSanXuat)
        {
            this.maSo = maSo;
            this.tenPhim = tenPhim;
            this.soLuotXem = soLuotXem;
            this.danhGia = danhGia;
            this.namSanXuat = namSanXuat;
        }

        public void XuatThongTin()
        {
            Console.WriteLine($"| {maSo,-6} | {tenPhim,-25} | {soLuotXem,-12:N0} | {danhGia,-4} sao | {namSanXuat,-6} |");
        }
    }

    // 2. Định nghĩa Nút (Node) của Cây nhị phân tìm kiếm
    public class Node
    {
        public PhimAnh Data;
        public Node Left;
        public Node Right;

        public Node(PhimAnh phim)
        {
            Data = phim;
            Left = null;
            Right = null;
        }
    }

    // 3. Định nghĩa Cây nhị phân tìm kiếm (BST)
    public class BinarySearchTree
    {
        public Node Root;

        public BinarySearchTree()
        {
            Root = null;
        }

        // Thêm một bộ phim vào cây (Trả về false nếu trùng maSo)
        public bool Insert(PhimAnh phim)
        {
            return InsertRec(ref Root, phim);
        }

        private bool InsertRec(ref Node root, PhimAnh phim)
        {
            if (root == null)
            {
                root = new Node(phim);
                return true;
            }

            if (phim.maSo < root.Data.maSo)
                return InsertRec(ref root.Left, phim);
            else if (phim.maSo > root.Data.maSo)
                return InsertRec(ref root.Right, phim);
            else
                return false; // Trùng mã số phim
        }

        // YÊU CẦU 2: Xuất danh sách tăng dần theo maSo (Duyệt LNR - Left Node Right)
        public void XuatTangDan()
        {
            InTieuDeBang();
            LNR(Root);
            InDuongKeNgang();
        }

        private void LNR(Node root)
        {
            if (root != null)
            {
                LNR(root.Left);
                root.Data.XuatThongTin();
                LNR(root.Right);
            }
        }

        // YÊU CẦU 3: Xuất danh sách giảm dần theo maSo (Duyệt RNL - Right Node Left)
        public void XuatGiamDan()
        {
            InTieuDeBang();
            RNL(Root);
            InDuongKeNgang();
        }

        private void RNL(Node root)
        {
            if (root != null)
            {
                RNL(root.Right);
                root.Data.XuatThongTin();
                RNL(root.Left);
            }
        }

        // YÊU CẦU 4: Tìm kiếm bộ phim dựa trên data (maSo)
        public Node TimKiem(int maSo)
        {
            return SearchRec(Root, maSo);
        }

        private Node SearchRec(Node root, int maSo)
        {
            if (root == null || root.Data.maSo == maSo)
                return root;

            if (maSo < root.Data.maSo)
                return SearchRec(root.Left, maSo);

            return SearchRec(root.Right, maSo);
        }

        // YÊU CẦU 5: Xóa một bộ phim khỏi cây dựa trên maSo
        public bool XoaPhim(int maSo)
        {
            if (TimKiem(maSo) == null) return false;
            Root = DeleteRec(Root, maSo);
            return true;
        }

        private Node DeleteRec(Node root, int maSo)
        {
            if (root == null) return null;

            // Tìm nút cần xóa
            if (maSo < root.Data.maSo)
            {
                root.Left = DeleteRec(root.Left, maSo);
            }
            else if (maSo > root.Data.maSo)
            {
                root.Right = DeleteRec(root.Right, maSo);
            }
            else
            {
                // Đã tìm thấy nút cần xóa
                // Trường hợp 1 & 2: Nút lá hoặc chỉ có 1 con
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;

                // Trường hợp 3: Nút có 2 con
                // Tìm phần tử thế mạng (nhỏ nhất bên cây con phải)
                root.Data = TimMin(root.Right);

                // Xóa phần tử thế mạng ở cây con phải
                root.Right = DeleteRec(root.Right, root.Data.maSo);
            }
            return root;
        }

        private PhimAnh TimMin(Node root)
        {
            PhimAnh min = root.Data;
            while (root.Left != null)
            {
                min = root.Left.Data;
                root = root.Left;
            }
            return min;
        }

        // Hàm phụ trợ định dạng bảng
        public void InTieuDeBang()
        {
            InDuongKeNgang();
            Console.WriteLine($"| {"Mã Số",-6} | {"Tên Phim",-25} | {"Lượt Xem",-12} | {"Đ.Giá",-8} | {"Năm SX",-6} |");
            InDuongKeNgang();
        }

        public void InDuongKeNgang()
        {
            Console.WriteLine(new string('-', 73));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            BinarySearchTree bst = new BinarySearchTree();
            Random rand = new Random();

            // YÊU CẦU 1: Tạo cây nhị phân tìm kiếm lưu trữ 30 bộ phim
            List<int> danhSachMaDaCo = new List<int>();
            int count = 1;
            while (count <= 30)
            {
                int maNgauNhien = rand.Next(100, 1000); // Mã phim từ 100 đến 999
                PhimAnh phimMoi = new PhimAnh(
                    maSo: maNgauNhien,
                    tenPhim: $"Phim Điện Ảnh Số {count}",
                    soLuotXem: rand.Next(1000, 5000000),
                    danhGia: rand.Next(1, 6), // 1 đến 5 sao
                    namSanXuat: rand.Next(2000, 2026)
                );

                // Nếu thêm thành công (không trùng mã) thì mới tăng biến đếm
                if (bst.Insert(phimMoi))
                {
                    danhSachMaDaCo.Add(maNgauNhien);
                    count++;
                }
            }

            // YÊU CẦU 2: Xuất danh sách tăng dần theo maSo
            Console.WriteLine("\n=== YÊU CẦU 2: DANH SÁCH 30 BỘ PHIM TĂNG DẦN THEO MÃ SỐ ===");
            bst.XuatTangDan();

            // YÊU CẦU 3: Xuất danh sách giảm dần theo maSo
            Console.WriteLine("\n=== YÊU CẦU 3: DANH SÁCH 30 BỘ PHIM GIẢM DẦN THEO MÃ SỐ ===");
            bst.XuatGiamDan();

            // YÊU CẦU 4: Tìm kiếm bộ phim dựa trên maSo
            Console.WriteLine("\n=== YÊU CẦU 4: TÌM KIẾM BỘ PHIM ===");
            // Lấy thử 1 mã số chắc chắn có trong cây để test tìm kiếm thành công
            int maCanTim = danhSachMaDaCo[rand.Next(danhSachMaDaCo.Count)];
            Console.WriteLine($"-> Đang tìm kiếm bộ phim có mã số: {maCanTim}");
            Node ketQuaTim = bst.TimKiem(maCanTim);
            if (ketQuaTim != null)
            {
                Console.WriteLine("Tìm thấy bộ phim:");
                bst.InTieuDeBang();
                ketQuaTim.Data.XuatThongTin();
                bst.InDuongKeNgang();
            }
            else
            {
                Console.WriteLine($"Không tìm thấy bộ phim có mã {maCanTim}.");
            }

            // YÊU CẦU 5: Xóa một bộ phim khỏi cây
            Console.WriteLine("\n=== YÊU CẦU 5: XÓA BỘ PHIM KHỎI CÂY ===");
            Console.WriteLine($"-> Thực hiện xóa bộ phim có mã số: {maCanTim}");
            if (bst.XoaPhim(maCanTim))
            {
                Console.WriteLine($"Đã xóa thành công phim mã {maCanTim}! Danh sách tăng dần sau khi xóa (còn 29 phim):");
                bst.XuatTangDan();
            }
            else
            {
                Console.WriteLine($"Xóa thất bại! Mã {maCanTim} không tồn tại.");
            }

            Console.ReadLine();
        }
    }
}