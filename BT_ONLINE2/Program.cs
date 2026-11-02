using System;
using System.Text;

namespace BaiTapCTDL_So02
{
    // Định nghĩa lớp PhimAnh theo đề bài
    public class PhimAnh
    {
        public int maSo;          // Mã định danh phim
        public string tenPhim;    // Tên phim
        public int soLuotXem;     // Số lượt xem trực tuyến
        public int danhGia;       // Đánh giá từ 1 đến 5 sao
        public int namSanXuat;    // Năm sản xuất

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
            Console.WriteLine($"| {maSo,-6} | {tenPhim,-22} | {soLuotXem,-12} | {danhGia,-8} | {namSanXuat,-12} |");
        }
    }

    // Node dùng chung cho Danh sách liên kết
    public class Node
    {
        public PhimAnh Data;
        public Node Next;

        public Node(PhimAnh data)
        {
            this.Data = data;
            this.Next = null;
        }
    }

    // =========================================================================
    // YÊU CẦU 1, 2, 3: DANH SÁCH LIÊN KẾT ĐƠN
    // =========================================================================
    public class DanhSachLienKet
    {
        public Node Head;
        public Node Tail;

        public DanhSachLienKet()
        {
            Head = Tail = null;
        }

        // Thêm phim vào cuối danh sách
        public void ThemCuoi(PhimAnh phim)
        {
            Node newNode = new Node(phim);
            if (Head == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }
        }

        // Hiển thị danh sách
        public void XuatDanhSach()
        {
            Console.WriteLine(new string('-', 76));
            Console.WriteLine($"| {"Mã số",-6} | {"Tên phim",-22} | {"Lượt xem",-12} | {"Đánh giá",-8} | {"Năm SX",-12} |");
            Console.WriteLine(new string('-', 76));
            Node p = Head;
            while (p != null)
            {
                p.Data.XuatThongTin();
                p = p.Next;
            }
            Console.WriteLine(new string('-', 76));
        }

        // Yêu cầu 2: Tìm thông tin bộ phim theo tên nhập vào
        public void TimPhimTheoTen(string tenCanTim)
        {
            Node p = Head;
            bool timThay = false;
            Console.WriteLine($"\n--- KẾT QUẢ TÌM KIẾM PHIM CÓ TÊN: '{tenCanTim}' ---");
            
            while (p != null)
            {
                // So sánh chuỗi không phân biệt hoa thường
                if (p.Data.tenPhim.IndexOf(tenCanTim, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!timThay)
                    {
                        Console.WriteLine(new string('-', 76));
                        Console.WriteLine($"| {"Mã số",-6} | {"Tên phim",-22} | {"Lượt xem",-12} | {"Đánh giá",-8} | {"Năm SX",-12} |");
                        Console.WriteLine(new string('-', 76));
                    }
                    p.Data.XuatThongTin();
                    timThay = true;
                }
                p = p.Next;
            }

            if (!timThay)
            {
                Console.WriteLine("-> Không tìm thấy bộ phim nào khớp với tên vừa nhập!");
            }
            else
            {
                Console.WriteLine(new string('-', 76));
            }
        }

        // Yêu cầu 3a: Sắp xếp theo maSo tăng dần (Selection Sort trên DSLK)
        public void SapXepMaSoTangDan()
        {
            for (Node p = Head; p != null; p = p.Next)
            {
                Node minNode = p;
                for (Node q = p.Next; q != null; q = q.Next)
                {
                    if (q.Data.maSo < minNode.Data.maSo)
                    {
                        minNode = q;
                    }
                }
                if (minNode != p)
                {
                    PhimAnh temp = p.Data;
                    p.Data = minNode.Data;
                    minNode.Data = temp;
                }
            }
        }

        // Yêu cầu 3b: Sắp xếp theo namSanXuat giảm dần
        public void SapXepNamSXGiamDan()
        {
            for (Node p = Head; p != null; p = p.Next)
            {
                Node maxNode = p;
                for (Node q = p.Next; q != null; q = q.Next)
                {
                    if (q.Data.namSanXuat > maxNode.Data.namSanXuat)
                    {
                        maxNode = q;
                    }
                }
                if (maxNode != p)
                {
                    PhimAnh temp = p.Data;
                    p.Data = maxNode.Data;
                    maxNode.Data = temp;
                }
            }
        }
    }

    // =========================================================================
    // YÊU CẦU 4, 5: CẤU TRÚC HÀNG ĐỢI (QUEUE - Lưu trữ tối đa 30 bộ phim)
    // =========================================================================
    public class HangDoiPhim
    {
        private PhimAnh[] data;
        private int front;
        private int rear;
        private int count;
        private int maxSize;

        public HangDoiPhim(int size = 30)
        {
            maxSize = size;
            data = new PhimAnh[maxSize];
            front = 0;
            rear = -1;
            count = 0;
        }

        public bool IsEmpty() => count == 0;
        public bool IsFull() => count == maxSize;

        public bool Enqueue(PhimAnh phim)
        {
            if (IsFull())
            {
                Console.WriteLine("Hàng đợi đã đầy!");
                return false;
            }
            rear = (rear + 1) % maxSize;
            data[rear] = phim;
            count++;
            return true;
        }

        public PhimAnh Dequeue()
        {
            if (IsEmpty()) return null;
            PhimAnh item = data[front];
            front = (front + 1) % maxSize;
            count--;
            return item;
        }

        // Yêu cầu 5: Xuất bộ phim có soLuotXem cao nhất trong hàng đợi
        public void XuatPhimLuotXemCaoNhat()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Hàng đợi rỗng!");
                return;
            }

            // Tìm số lượt xem lớn nhất
            int maxLuotXem = -1;
            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % maxSize;
                if (data[index].soLuotXem > maxLuotXem)
                {
                    maxLuotXem = data[index].soLuotXem;
                }
            }

            // Xuất tất cả các phim đạt mức lượt xem cao nhất đó
            Console.WriteLine($"\n--- BỘ PHIM CÓ SỐ LƯỢT XEM CAO NHẤT TRONG HÀNG ĐỢI ({maxLuotXem} lượt) ---");
            Console.WriteLine(new string('-', 76));
            Console.WriteLine($"| {"Mã số",-6} | {"Tên phim",-22} | {"Lượt xem",-12} | {"Đánh giá",-8} | {"Năm SX",-12} |");
            Console.WriteLine(new string('-', 76));
            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % maxSize;
                if (data[index].soLuotXem == maxLuotXem)
                {
                    data[index].XuatThongTin();
                }
            }
            Console.WriteLine(new string('-', 76));
        }
    }

    // =========================================================================
    // YÊU CẦU 6, 7: CẤU TRÚC NGĂN XẾP (STACK - Lưu trữ tối đa 30 bộ phim)
    // =========================================================================
    public class NganXepPhim
    {
        private PhimAnh[] data;
        private int top;
        private int maxSize;

        public NganXepPhim(int size = 30)
        {
            maxSize = size;
            data = new PhimAnh[maxSize];
            top = -1;
        }

        public bool IsEmpty() => top == -1;
        public bool IsFull() => top == maxSize - 1;

        public bool Push(PhimAnh phim)
        {
            if (IsFull())
            {
                Console.WriteLine("Ngăn xếp đã đầy!");
                return false;
            }
            data[++top] = phim;
            return true;
        }

        public PhimAnh Pop()
        {
            if (IsEmpty()) return null;
            return data[top--];
        }

        // Yêu cầu 7: Xuất các bộ phim có lượt đánh giá dưới 3 sao trong ngăn xếp
        public void XuatPhimDanhGiaDuoi3Sao()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Ngăn xếp rỗng!");
                return;
            }

            Console.WriteLine("\n--- CÁC BỘ PHIM CÓ ĐÁNH GIÁ DƯỚI 3 SAO TRONG NGĂN XẾP ---");
            Console.WriteLine(new string('-', 76));
            Console.WriteLine($"| {"Mã số",-6} | {"Tên phim",-22} | {"Lượt xem",-12} | {"Đánh giá",-8} | {"Năm SX",-12} |");
            Console.WriteLine(new string('-', 76));

            bool coPhim = false;
            // Duyệt từ đỉnh ngăn xếp (top) xuống đáy
            for (int i = top; i >= 0; i--)
            {
                if (data[i].danhGia < 3)
                {
                    data[i].XuatThongTin();
                    coPhim = true;
                }
            }

            if (!coPhim)
            {
                Console.WriteLine("| Không có bộ phim nào đánh giá dưới 3 sao.                                |");
            }
            Console.WriteLine(new string('-', 76));
        }
    }

    // =========================================================================
    // CHƯƠNG TRÌNH CHÍNH (MAIN)
    // =========================================================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            DanhSachLienKet dslk = new DanhSachLienKet();
            HangDoiPhim queue = new HangDoiPhim(30);
            NganXepPhim stack = new NganXepPhim(30);

            // Tạo dữ liệu mẫu cho 30 bộ phim ngẫu nhiên để kiểm tra
            Random rand = new Random();
            int[] danhSachMaSo = new int[30];
            for (int i = 0; i < 30; i++) danhSachMaSo[i] = 101 + i;

            // Trộn ngẫu nhiên mã số để test tính năng sắp xếp
            for (int i = 0; i < 30; i++)
            {
                int r = rand.Next(i, 30);
                int temp = danhSachMaSo[i];
                danhSachMaSo[i] = danhSachMaSo[r];
                danhSachMaSo[r] = temp;
            }

            // Đưa 30 bộ phim vào cả 3 cấu trúc: DSLK, Hàng đợi, Ngăn xếp
            for (int i = 0; i < 30; i++)
            {
                PhimAnh phim = new PhimAnh(
                    maSo: danhSachMaSo[i],
                    tenPhim: $"Phim Điện Ảnh Số {i + 1}",
                    soLuotXem: rand.Next(1000, 100000), // Lượt xem từ 1.000 đến 99.999
                    danhGia: rand.Next(1, 6),           // Đánh giá từ 1 đến 5 sao
                    namSanXuat: rand.Next(2000, 2026)   // Năm SX từ 2000 đến 2025
                );

                dslk.ThemCuoi(phim); // Yêu cầu 1
                queue.Enqueue(phim); // Yêu cầu 4
                stack.Push(phim);    // Yêu cầu 6
            }

            // 1. Xuất danh sách 30 bộ phim ban đầu trong DSLK
            Console.WriteLine("=== YÊU CẦU 1: DANH SÁCH LIÊN KẾT 30 BỘ PHIM BAN ĐẦU ===");
            dslk.XuatDanhSach();

            // 2. Tìm kiếm phim theo tên
            Console.Write("\n=== YÊU CẦU 2: NHẬP TÊN PHIM CẦN TÌM (VD: Phim Điện Ảnh Số 5): ");
            string tenCanTim = Console.ReadLine();
            dslk.TimPhimTheoTen(tenCanTim);

            // 3a. Sắp xếp theo Mã Số tăng dần
            Console.WriteLine("\n=== YÊU CẦU 3a: DANH SÁCH SAU KHI SẮP XẾP MÃ SỐ TĂNG DẦN ===");
            dslk.SapXepMaSoTangDan();
            dslk.XuatDanhSach();

            // 3b. Sắp xếp theo Năm Sản Xuất giảm dần
            Console.WriteLine("\n=== YÊU CẦU 3b: DANH SÁCH SAU KHI SẮP XẾP NĂM SẢN XUẤT GIẢM DẦN ===");
            dslk.SapXepNamSXGiamDan();
            dslk.XuatDanhSach();

            // 4 & 5. Hàng đợi và xuất phim có lượt xem cao nhất
            Console.WriteLine("\n=== YÊU CẦU 4 & 5: HÀNG ĐỢI (QUEUE) ===");
            queue.XuatPhimLuotXemCaoNhat();

            // 6 & 7. Ngăn xếp và xuất phim đánh giá dưới 3 sao
            Console.WriteLine("\n=== YÊU CẦU 6 & 7: NGĂN XẾP (STACK) ===");
            stack.XuatPhimDanhGiaDuoi3Sao();

            Console.WriteLine("\nNhấn phím bất kỳ để kết thúc...");
            Console.ReadKey();
        }
    }
}