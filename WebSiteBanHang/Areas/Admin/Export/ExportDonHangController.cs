using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace CMS.Web.Controllers.Export
{
    public class ExportDonHangController : Controller
    {
        // GET: ExportDinHang
        public FileResult DonHang(string searchString,  DateTime? tuNgayNhan, DateTime? denNgayNhan, DateTime? tuNgayXuat,DateTime? denNgayXuat)
        {
            List<DonHang> lstDonHang = new List<DonHang>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var lstResult = db.DonHangs
                .Include(x => x.NhanVien)
                .Include(x => x.KhachHang)
                .AsQueryable();
                if (tuNgayNhan != null)
                    lstResult = lstResult.Where(x => x.NgayNhan >= tuNgayNhan);
                if (denNgayNhan != null)
                    lstResult = lstResult.Where(x => x.NgayNhan <= denNgayNhan);
                if (tuNgayXuat != null)
                    lstResult = lstResult.Where(x => x.NgayXuat >= tuNgayXuat);
                if (denNgayXuat != null)
                    lstResult = lstResult.Where(x => x.NgayXuat >= denNgayXuat);
                //Laays thang gan nhat
                if (tuNgayNhan == null && denNgayNhan == null && tuNgayXuat == null && denNgayXuat == null)
                {

                    DateTime today = DateTime.Now;
                    tuNgayNhan = new DateTime(today.Year, today.Month, 1);
                    denNgayNhan = tuNgayNhan.Value.AddMonths(1).AddDays(-1);
                    lstResult = lstResult.Where(x => x.NgayNhan >= tuNgayNhan && x.NgayNhan <= denNgayNhan);
                }
                if (!string.IsNullOrEmpty(searchString))
                {
                    lstResult = lstResult.Where(x => x.NhanVien.HoTen.Contains(searchString) || x.KhachHang.HoTen.Contains(searchString));
                }
                lstDonHang = lstResult.ToList();
            }
            //
            string sWebRootFolder = HostingEnvironment.ApplicationPhysicalPath + "\\FilesUpload";
            CreateExport(lstDonHang, sWebRootFolder);
            string fPath = sWebRootFolder + "\\" + "DanhSachDonHang.xlsx";
            FileInfo fi = new FileInfo(fPath);
            return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachDonHang" + fi.Extension);
        }
        public void CreateExport(List<DonHang> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "DonHangID", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "GiaTriDonHang", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "KhachHang.HoTen", WidthSize = 30 });
            lstProperty.Add(new ProperTiesName { PropsName = "NgayNhan", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "NgayXuat", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Status", WidthSize = 20 });
            //Tạo đối tượng dùng để Export
            ExportCore2<DonHang> exh = new ExportCore2<DonHang>(20)
            {
                FileName = "DanhSachDonHang",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Danh sách đơn hàng"
            };
            exh.HeaderInput = CreateHeader();
            exh.RunExport();
            //
        }
        // Tạo header
        public HeaderInputs CreateHeader()
        {
            HeaderInputs headInput = new HeaderInputs();
            // Tạo danh sách header với các đầu vào(row, colum,size,text)
            List<HeaderLocation> lstHeaderLocation = new List<HeaderLocation>()
            {
                new HeaderLocation(1,1,20,"Mã đơn hàng"),new HeaderLocation(1,2,20,"Giá trị đơn hàng"),
                new HeaderLocation(1,3,30,"Khách hàng"),new HeaderLocation(1,4,20,"Ngày nhận đơn"),
                new HeaderLocation(1,5,20,"Ngày xuất đơn"),new HeaderLocation(1,6,20,"Trạng thái")
            };
            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
            List<MergeTo> lstMerge = new List<MergeTo>()
            {
            };
            // gán các tham số cho headInput
            headInput.ListHeader = lstHeaderLocation;
            headInput.ListMergeIndex = lstMerge;
            // số hàng mà header chiếm trong excell
            headInput.HeaderHeight = 2;
            return headInput;
        }
    }
}