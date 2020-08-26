using CMS.Web.Controllers.Export;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Export
{
    public class ExportSanPhamDonHangController : Controller
    {
        public FileResult SanPhamDonHang(int DonHangID)
        {
            List<SanPhamDonHang> lstSanPhamDonHang = new List<SanPhamDonHang>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var lstResult = db.SanPhamDonHangs
                .AsQueryable();
                if (DonHangID !=0)
                {
                    lstResult = lstResult.Where(x => x.DonHangID ==DonHangID);
                }
                lstSanPhamDonHang = lstResult.ToList();
            }
            //
            string sWebRootFolder = HostingEnvironment.ApplicationPhysicalPath + "\\FilesUpload";
            CreateExport(lstSanPhamDonHang, sWebRootFolder,DonHangID);
            string fPath = sWebRootFolder + "\\" + "DanhSachSanPhamDonHang.xlsx";
            FileInfo fi = new FileInfo(fPath);
            return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachSanPhamDonHang" + fi.Extension);
        }
        public void CreateExport(List<SanPhamDonHang> lst, string sWebRootFolder,int DonhHangID)
        {
            int donhangID = DonhHangID;
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "SanPhamID", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "SanPham.TenSanPham", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "SoLuong", WidthSize = 30 });
            lstProperty.Add(new ProperTiesName { PropsName = "SanPham.GiaSanPham", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "GhiChu", WidthSize = 20 });
            //Tạo đối tượng dùng để Export
            ExportCore2<SanPhamDonHang> exh = new ExportCore2<SanPhamDonHang>(20)
            {
                FileName = "DanhSachSanPhamDonHang",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Danh sách sản phẩm đơn hàng"
            };
            exh.HeaderInput = CreateHeader(donhangID);
            exh.RunExport();
            //
        }
        // Tạo header
        public HeaderInputs CreateHeader(int DonHangID)
        {
            var maDonHang = DonHangID;
            HeaderInputs headInput = new HeaderInputs();
            // Tạo danh sách header với các đầu vào(row, colum,size,text)
            List<HeaderLocation> lstHeaderLocation = new List<HeaderLocation>()
            {
                new HeaderLocation(1,1,20,"Mã đơn hàng: " +maDonHang),
                new HeaderLocation(2,1,20,"Mã sản phẩm"),new HeaderLocation(2,2,20,"Tên sản phẩm"),
                new HeaderLocation(2,3,30,"Số lượng"),new HeaderLocation(2,4,20,"Giá sản phẩm"),
                new HeaderLocation(2,5,20,"Ghi chú")
            };
            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
            List<MergeTo> lstMerge = new List<MergeTo>()
            {
                new MergeTo(1,1,1,5)
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
