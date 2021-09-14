using ERP_DataAccess;
using ERP_Foundation.Models.DTO;
using ERP_Foundation.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Foundation.Controllers
{
    public class ProductController : Controller
    {
        private ERPFoundationContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private User _user;
        public ProductController(ERPFoundationContext context, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region 產品分類

        [HttpGet]
        public JsonResult GetCategory(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<CategoryVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                    data = _context.Categories.Where(c => c.Deleted == false).OrderBy(c => c.Order).Select(c => new CategoryVM()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        Order = c.Order
                    }).ToList();
                else
                    data = _context.Categories.Where(c => c.Deleted == false && c.Name.ToUpper().Contains(SearchText.ToUpper())).OrderBy(c => c.Order).Select(c => new CategoryVM()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        Order = c.Order
                    }).ToList();

                int total = data.Count();

                return Json(new CategoryInfoDTO()
                {
                    Page = Page,
                    PageSize = PageSize,
                    Total = total,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCategory(string name, int order)
        {
            try
            {
                _context.Categories.Add(
                    new Category()
                    {
                        Name = name,
                        ParentID = 0,
                        Level = 0,
                        Order = order,
                        CreateTime = DateTime.Now,
                        AddedBy = _user.ID
                    });
                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult EditCategory(int id, string name, int order)
        {
            try
            {
                var obj = _context.Categories.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Categories.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.Order = order;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = _user.ID;
                    _context.Entry(obj).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var obj = _context.Categories.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Categories.Contains(obj);

                if (b)
                {
                    obj.Deleted = true;
                    obj.DeleteTime = DateTime.Now;
                    obj.DeletedBy = _user.ID;
                    _context.Entry(obj).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        #endregion

        #region 產品

        [HttpGet]
        public IActionResult GetCategoryItem()
        {
            try
            {
                List<CategoryItemVM> items;

                items = _context.Categories.Where(c => c.Deleted == false).Select(c => new CategoryItemVM()
                {
                    key = c.ID,
                    value = c.Name
                }).ToList();

                return Json(items);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpGet]
        public IActionResult GetProduct(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<ProductVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                    data = _context.Products.Where(c => c.Deleted == false)
                        .Join(_context.Categories, c => c.CategoryID, s => s.ID, (c, s) => new ProductVM
                        {
                            ID = c.ID,
                            Name = c.Name,
                            Category = s.Name,
                            Price = c.Price
                        }).ToList();
                else
                    data = _context.Products.Where(c => c.Deleted == false && c.Name.ToUpper().Contains(SearchText.ToUpper()))
                        .Join(_context.Categories, c => c.CategoryID, s => s.ID, (c, s) => new ProductVM
                        {
                            ID = c.ID,
                            Name = c.Name,
                            Category = s.Name,
                            Price = c.Price
                        }).ToList();

                int total = data.Count();

                //var path = _configuration["imagePath"];

                foreach (var d in data)
                {
                    var indexPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/Product/", d.ID.ToString());
                    if (Directory.Exists(indexPath))
                    {
                        FileInfo[] files = new DirectoryInfo(indexPath).GetFiles();

                        //foreach (var file in files)
                        //    Path.Combine(indexPath, file.Name);
                        if (files.Count() > 0)
                            d.Path = Path.Combine("..\\image\\Product\\", d.ID.ToString(), files[0].Name);
                    }
                }

                return Json(new ProductInfoDTO()
                {
                    Page = Page,
                    PageSize = PageSize,
                    Total = total,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult AddProduct(string name, int category, int price)
        {
            try
            {
                _context.Products.Add(
                    new Product()
                    {
                        Name = name,
                        CategoryID = category,
                        Price = price,
                        CreateTime = DateTime.Now,
                        AddedBy = _user.ID
                    });

                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult EditProduct(int id, string name, int category, int price)
        {
            try
            {
                var obj = _context.Products.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Products.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.CategoryID = category;
                    obj.Price = price;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = _user.ID;
                    _context.Entry(obj).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var obj = _context.Products.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Products.Contains(obj);

                if (b)
                {
                    obj.Deleted = true;
                    obj.DeleteTime = DateTime.Now;
                    obj.DeletedBy = _user.ID;
                    _context.Entry(obj).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult UploadFile()
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    IWorkbook wb;

                    var file = Request.Form.Files[0];

                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    string path = Path.Combine(webRootPath, file.FileName);
                    string fileName = Path.GetFileName(path);
                    string fileExtension = Path.GetExtension(fileName);

                    switch (fileExtension)
                    {
                        case ".xls":
                            wb = new HSSFWorkbook(file.OpenReadStream());
                            break;
                        case ".xlsx":
                            wb = new XSSFWorkbook(file.OpenReadStream());
                            break;
                        default:
                            Response.StatusCode = 500;
                            return Json(Encoding.Unicode.GetBytes("檔案格式錯誤，無法上傳"));
                    }

                    ISheet sheet = wb.GetSheetAt(0);

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        if (sheet.GetRow(i) != null)
                        {
                            var category = sheet.GetRow(i).GetCell(1).ToString();
                            var categoryId = _context.Categories.Where(c => c.Name.ToUpper() == category.Trim().ToUpper()).Select(c => c.ID).FirstOrDefault();

                            if (categoryId == 0)
                            {
                                Response.StatusCode = 500;
                                //return Json(Encoding.Unicode.GetBytes("商品類別錯誤，無法上傳"));                            }
                                return Json("商品類別錯誤，無法上傳");
                            }

                            Product product = new Product()
                            {
                                Name = sheet.GetRow(i).GetCell(0).ToString().Trim(),
                                CategoryID = categoryId,
                                Price = int.Parse(sheet.GetRow(i).GetCell(2).ToString().Trim()),
                                CreateTime = DateTime.Now
                            };

                            _context.Products.Add(product);
                        }
                    }

                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Response.StatusCode = 500;
                    return Json(Encoding.Unicode.GetBytes("檔案內容有誤，請重新檢查再上傳"));
                    //return File(Encoding.Unicode.GetBytes("檔案內容有誤，請重新檢查再上傳"), "application/x-unknown", "error.txt");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                //return Content(ex.InnerException.Message, "application/json", Encoding.Unicode);
                return Json(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult UploadImage(int id)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    //var physicalPath = _configuration["imagePath"];
                    var indexDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "image/Product/", id.ToString());

                    if (!Directory.Exists(indexDirectory))
                        Directory.CreateDirectory(indexDirectory);

                    var images = Request.Form.Files;

                    foreach (var image in images)
                    {
                        var fullPath = Path.Combine(indexDirectory, image.FileName);

                        try
                        {
                            using (var stream = System.IO.File.Create(fullPath))
                            {
                                image.CopyTo(stream);
                                stream.Flush();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.StatusCode = 500;
                            return Json("有些錯誤導致無法完成上傳，檢查檔名是否有重複");
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.InnerException.Message);
            }
        }

        #endregion
    }
}
