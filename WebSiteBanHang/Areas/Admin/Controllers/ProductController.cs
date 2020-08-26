using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Script.Serialization;
using System.Xml.Linq;
using WebSiteBanHang.DAO;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(string searchString,int phanLoaiSanPhamID=0, int page = 1, int pageSize = 10)
        {
            ViewBag.PhanLoaiSanPhamID = phanLoaiSanPhamID;
            var dao = new ProductDAO();
            var danhMucSanPham = dao.getDanhMucSanPham();
            var model = dao.ListAllpaging(searchString,phanLoaiSanPhamID, page, pageSize);
            ViewData["danhMucSanPham"] = danhMucSanPham;
            ViewBag.SearchString = searchString;
            ViewBag.PhanLoaiSanPhamID = phanLoaiSanPhamID;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(bool? choose)
        {
            SetViewBag(null, choose);
            return View();
        }
        [HttpGet]
        public ActionResult CreateFollowCategory()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateDetailsProduct(int sanPhamID)
        {
            ViewBag.ID = sanPhamID;
            return View();
        }
        [HttpPost]
        public ActionResult CreateDetailsProduct(CTSanPham ctSanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                if (file != null)
                {
                    ctSanPham.AnhSanPham = file.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImage/"), myfile);
                        file.SaveAs(path);
                        var pathImages = ("~/Images/ProductImage/" + myfile).ToString();
                        ctSanPham.AnhSanPham = pathImages;
                    }
                    else
                    {
                        ViewBag.message = "Làm ơn chọn tệp ảnh";
                    }
                }
                var dao = new CTSanPhamDAO();

                long id = dao.Insert(ctSanPham);
                if (id > 0)
                {
                    SetAlert("Thêm ct sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult EditDetailProduct(CTSanPham ctSanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                if (file != null)
                {
                    ctSanPham.AnhSanPham = file.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImage/"), myfile);
                        file.SaveAs(path);
                        var pathImages = ("~/Images/ProductImage/" + myfile).ToString();
                        ctSanPham.AnhSanPham = pathImages;
                    }
                    else
                    {
                        ViewBag.message = "Làm ơn chọn tệp ảnh";
                    }
                }
                var dao = new CTSanPhamDAO();

                var result = dao.Update(ctSanPham);
                if (result)
                {
                    SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult DeleteDetailProduct(int ctSanPhamID)
        {
            new CTSanPhamDAO().Delete(ctSanPhamID);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditDetailsProduct(int id)
        {
            return View("Index");
        }
        [HttpGet]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var sanPham = new ProductDAO().ViewDetail(id);
            bool? choose = null;
            PhanLoaiSanPham plsp = db.PhanLoaiSanPhams.SingleOrDefault(x => x.PhanLoaiSanPhamID == sanPham.LoaiSanPhamID);
            if (plsp != null)
            {
                bool? isFale, isFemale, isChildren;
                isFale = db.DanhMucSanPhams.SingleOrDefault(x => x.DanhMucSanPhamID == plsp.DanhMucSanPhamID).LaDoNam;
                isFemale = db.DanhMucSanPhams.SingleOrDefault(x => x.DanhMucSanPhamID == plsp.DanhMucSanPhamID).LaDoNu;
                isChildren = db.DanhMucSanPhams.SingleOrDefault(x => x.DanhMucSanPhamID == plsp.DanhMucSanPhamID).LaDoTreEm;
                if (isFale == true && isFemale == null && isChildren == null)
                {
                    choose = true;
                }
                else if (isFale == null && isFemale == true && isChildren == null)
                {
                    choose = false;
                }
                else if (isFale == true && isFemale == true && isChildren == true)
                {
                    choose = null;
                }
            }
            SetViewBag(sanPham.LoaiSanPhamID, choose);
            return View(sanPham);
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(SanPham sanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                if (file != null)
                {
                    sanPham.AnhSanPham = file.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImage/"), myfile);
                        file.SaveAs(path);
                        var pathImages = ("~/Images/ProductImage/" + myfile).ToString();
                        sanPham.AnhSanPham = pathImages;
                    }
                    else
                    {
                        ViewBag.message = "Làm ơn chọn tệp ảnh";
                    }
                }
                var dao = new ProductDAO();

                var result = dao.Update(sanPham);
                if (result)
                {
                    SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            return View("Index");
        }
        //public JsonResult LoadImages(int sanPhamID)
        //{
        //    ProductDAO dao = new ProductDAO();
        //    var sanPham = dao.ViewDetail(sanPhamID);
        //    var anhSanPham = sanPham.AnhSanPham;
        //    XElement xImages = XElement.Parse(anhSanPham);
        //    List<string> listImagesReturn = new List<string>();

        //    foreach (XElement element in xImages.Elements())
        //    {
        //        listImagesReturn.Add(element.Value);
        //    }
        //    return Json(new
        //    {
        //        data = listImagesReturn
        //    }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(SanPham sanPham, HttpPostedFileBase file)
        {
            //sanPham.AnhSanPham = System.IO.Path.GetFileName(images.FileName);
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                if (file != null)
                {
                    sanPham.AnhSanPham = file.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImage/"), myfile);
                        file.SaveAs(path);
                        var pathImages = ("~/Images/ProductImage/" + myfile).ToString();
                        sanPham.AnhSanPham = pathImages;
                    }
                    else
                    {
                        ViewBag.message = "Làm ơn chọn tệp ảnh";
                    }
                }
                var dao = new ProductDAO();

                long id = dao.Insert(sanPham);
                if (id > 0)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);

            return RedirectToAction("Index");
        }

        public void SetViewBag(int? selectedId = null, bool? choose = null)
        {
            var dao = new PhanLoaiSanPhamDAO();
            ViewBag.LoaiSanPhamID = new SelectList(dao.ListAll(choose), "PhanLoaiSanPhamID", "TenPhanLoaiSanPham", selectedId);
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public ActionResult ShowModal(int sanPhamID)
        {
            var dao = new ProductDAO();
            CTSanPham ctSanPham = dao.ViewDetailProduct(sanPhamID);
            ViewBag.SanPhamID = sanPhamID;
            return View(ctSanPham);
        }
    }
}