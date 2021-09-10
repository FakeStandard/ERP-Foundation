using ERP_DataAccess;
using ERP_Foundation.Models.DTO;
using ERP_Foundation.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Controllers
{
    public class BasicInfoController : Controller
    {
        private ERPFoundationContext _context;

        public BasicInfoController(ERPFoundationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCustomer(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<CustomerVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    data = _context.Customers.Where(c => c.Deleted == false).Select(
                    c => new CustomerVM()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        EnglishName = c.EnglishName,
                        Address = c.Address,
                        Tel = c.Tel,
                        Fax = c.Fax,
                        ContactPerson = c.ContactPerson,
                        ContactTel = c.ContactTel,
                        PaymentRule = c.PaymentRule
                    }).ToList();
                }
                else
                {
                    data = _context.Customers.Where(
                        c => c.Deleted == false && c.Name.ToUpper().Contains(SearchText.ToUpper())).Select(
                        c => new CustomerVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            EnglishName = c.EnglishName,
                            Address = c.Address,
                            Tel = c.Tel,
                            Fax = c.Fax,
                            ContactPerson = c.ContactPerson,
                            ContactTel = c.ContactTel,
                            PaymentRule = c.PaymentRule
                        }).ToList();
                }

                int total = data.Count();

                return Json(new CustomerDTO()
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCustomer(
            string name, string englishName, string address, string tel, string fax, string contactPerson, string contactTel, int paymentRule)
        {
            try
            {
                _context.Customers.Add(
                    new Customer()
                    {
                        Name = name,
                        EnglishName = englishName,
                        Address = address,
                        Tel = tel,
                        Fax = fax,
                        ContactPerson = contactPerson,
                        ContactTel = contactTel,
                        PaymentRule = paymentRule,
                        CreateTime = DateTime.Now,
                        AddedBy = 0
                    });

                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditCustomer(
            int id, string name, string englishName, string address, string tel, string fax, string contactPerson, string contactTel, int paymentRule)
        {
            try
            {
                var obj = _context.Customers.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Customers.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.EnglishName = englishName;
                    obj.Address = address;
                    obj.Tel = tel;
                    obj.Fax = fax;
                    obj.ContactPerson = contactPerson;
                    obj.ContactTel = contactTel;
                    obj.PaymentRule = paymentRule;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var obj = _context.Customers.Where(c => c.ID == id).FirstOrDefault();
                var b = _context.Customers.Contains(obj);

                if (b)
                {
                    obj.Deleted = true;
                    obj.DeleteTime = DateTime.Now;
                    obj.DeletedBy = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetVendor(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<VendorVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    data = _context.Vendors.Where(c => c.Deleted == false).Select(
                    c => new VendorVM()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        EnglishName = c.EnglishName,
                        Address = c.Address,
                        Tel = c.Tel,
                        Fax = c.Fax,
                        ContactPerson = c.ContactPerson,
                        ContactTel = c.ContactTel,
                        PaymentRule = c.PaymentRule
                    }).ToList();
                }
                else
                {
                    data = _context.Vendors.Where(
                        c => c.Deleted == false && c.Name.ToUpper().Contains(SearchText.ToUpper())).Select(
                        c => new VendorVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            EnglishName = c.EnglishName,
                            Address = c.Address,
                            Tel = c.Tel,
                            Fax = c.Fax,
                            ContactPerson = c.ContactPerson,
                            ContactTel = c.ContactTel,
                            PaymentRule = c.PaymentRule
                        }).ToList();
                }

                int total = data.Count();

                return Json(new VendorDTO()
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddVendor(
            string name, string englishName, string address, string tel, string fax, string contactPerson, string contactTel, int paymentRule)
        {
            try
            {
                _context.Vendors.Add(
                    new Vendor()
                    {
                        Name = name,
                        EnglishName = englishName,
                        Address = address,
                        Tel = tel,
                        Fax = fax,
                        ContactPerson = contactPerson,
                        ContactTel = contactTel,
                        PaymentRule = paymentRule,
                        CreateTime = DateTime.Now,
                        AddedBy = 0
                    });

                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditVendor(
            int id, string name, string englishName, string address, string tel, string fax, string contactPerson, string contactTel, int paymentRule)
        {
            try
            {
                var obj = _context.Vendors.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Vendors.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.EnglishName = englishName;
                    obj.Address = address;
                    obj.Tel = tel;
                    obj.Fax = fax;
                    obj.ContactPerson = contactPerson;
                    obj.ContactTel = contactTel;
                    obj.PaymentRule = paymentRule;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteVendor(int id)
        {
            try
            {
                var obj = _context.Vendors.Where(c => c.ID == id).FirstOrDefault();
                var b = _context.Vendors.Contains(obj);

                if (b)
                {
                    obj.Deleted = true;
                    obj.DeleteTime = DateTime.Now;
                    obj.DeletedBy = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUser(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<UserVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    data = _context.Users.Select(
                    c => new UserVM()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        EnglishName = c.EnglishName,
                        Department = c.Department,
                        Title = c.Title,
                        Tel = c.Tel,
                        Address = c.Address,
                        ContactPerson = c.ContactPerson,
                        ContactTel = c.ContactTel,
                        Status = c.Status
                    }).ToList();
                }
                else
                {
                    data = _context.Users.Where(
                        c => c.Name.ToUpper().Contains(SearchText.ToUpper()) || c.EnglishName.ToUpper().Contains(SearchText)).Select(
                        c => new UserVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            EnglishName = c.EnglishName,
                            Department = c.Department,
                            Title = c.Title,
                            Tel = c.Tel,
                            Address = c.Address,
                            ContactPerson = c.ContactPerson,
                            ContactTel = c.ContactTel,
                            Status = c.Status
                        }).ToList();
                }

                int total = data.Count();

                return Json(new UserDTO()
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddUser(
            string name, string englishName, int department, int title, string tel, string address, string fax, string contactPerson, string contactTel, int status)
        {
            try
            {
                _context.Users.Add(
                    new User()
                    {
                        Name = name,
                        EnglishName = englishName,
                        Department = department,
                        Title = title,
                        Tel = tel,
                        Address = address,
                        ContactPerson = contactPerson,
                        ContactTel = contactTel,
                        Status = status,
                        CreateTime = DateTime.Now,
                        AddedBy = 0
                    });

                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditUser(
            int id, string name, string englishName, int department, int title, string tel, string address, string fax, string contactPerson, string contactTel, int status)
        {
            try
            {
                var obj = _context.Users.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Users.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.EnglishName = englishName;
                    obj.Department = department;
                    obj.Title = title;
                    obj.Tel = tel;
                    obj.Address = address;
                    obj.ContactPerson = contactPerson;
                    obj.ContactTel = contactTel;
                    obj.Status = status;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetDepartment(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<DepartmentVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    data = _context.Departments.Where(c => c.Deleted == false && c.ID != 1)
                        .Join(_context.Departments, c => c.ParentID, s => s.ID, (c, s) => new DepartmentVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            ParentName = s.Name
                        }).ToList();
                }
                else
                {
                    data = _context.Departments.Where(c => c.Deleted == false && c.ID != 1)
                        .Join(_context.Departments, c => c.ParentID, s => s.ID, (c, s) => new DepartmentVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            ParentName = s.Name
                        }).Where(m => m.Name.ToUpper().Contains(SearchText.ToUpper()) || m.ParentName.ToUpper().Contains(SearchText)).ToList();
                }

                int total = data.Count();

                return Json(new DepartmentDTO()
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
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetDepartmentItem()
        {
            try
            {
                List<DepartmentItemVM> data = _context.Departments.Select(
                    c => new DepartmentItemVM { key = c.ID, value = c.Name }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddDepartment(
            string name, string englishName, int parentID)
        {
            try
            {
                _context.Departments.Add(
                    new Department()
                    {
                        Name = name,
                        ParentID = parentID,
                        CreateTime = DateTime.Now,
                        AddedBy = 0
                    });

                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditDepartment(
            int id, string name, int parentID)
        {
            try
            {
                var obj = _context.Departments.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Departments.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.ParentID = parentID;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var obj = _context.Departments.Where(c => c.ID == id).FirstOrDefault();
                var b = _context.Departments.Contains(obj);

                if (b)
                {
                    obj.Deleted = true;
                    obj.DeleteTime = DateTime.Now;
                    obj.DeletedBy = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetTitle(int Page, int PageSize, string SearchText)
        {
            try
            {
                List<TitleVM> data;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    data = _context.Titles.Where(c => c.Deleted == false)
                        .Join(_context.Departments.Where(m => m.ParentID == 1), c => c.DepartmentID, x => x.ID, (c, x) => new TitleVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            DepartmentName = x.Name,
                            UnitName = _context.Departments.Where(y => x.ParentID == y.ID).Select(y => y.Name).FirstOrDefault()
                        }).ToList();

                    var unit = _context.Titles.Where(c => c.Deleted == false)
                        .Join(_context.Departments.Where(m => m.ParentID != 0 && m.ParentID != 1), c => c.DepartmentID, x => x.ID, (c, x) => new TitleVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            DepartmentName = _context.Departments.Where(y => y.ID == x.ParentID).Select(y => y.Name).FirstOrDefault(),
                            UnitName = x.Name,
                        }).ToList();

                    data.AddRange(unit);
                    data = data.OrderBy(c => c.ID).ToList();
                }
                else
                {
                    data = _context.Titles.Where(c => c.Deleted == false)
                        .Join(_context.Departments.Where(m => m.ParentID == 1), c => c.DepartmentID, x => x.ID, (c, x) => new TitleVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            DepartmentName = x.Name,
                            UnitName = _context.Departments.Where(y => x.ParentID == y.ID).Select(y => y.Name).FirstOrDefault()
                        }).Where(z => z.Name.ToUpper().Contains(SearchText) || z.DepartmentName.ToUpper().Contains(SearchText)).ToList();

                    var unit = _context.Titles.Where(c => c.Deleted == false)
                        .Join(_context.Departments.Where(m => m.ParentID != 0 && m.ParentID != 1), c => c.DepartmentID, x => x.ID, (c, x) => new TitleVM()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            DepartmentName = _context.Departments.Where(y => y.ID == x.ParentID).Select(y => y.Name).FirstOrDefault(),
                            UnitName = x.Name,
                        }).Where(z => z.Name.ToUpper().Contains(SearchText) || z.DepartmentName.ToUpper().Contains(SearchText)).ToList();

                    data.AddRange(unit);
                    data = data.OrderBy(c => c.ID).ToList();
                }

                int total = data.Count();

                return Json(new TitleDTO()
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
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetDepartmentParentItem()
        {
            try
            {
                List<DepartmentItemVM> data;

                data = _context.Departments.Where(c => c.Deleted == false && c.ParentID == 1)
                    .Select(c => new DepartmentItemVM()
                    {
                        key = c.ID,
                        value = c.Name,
                        units = _context.Departments.Where(s => s.ParentID == c.ID || s.ID == 1).Select(
                            s => new DepartmentItemVM()
                            {
                                key = s.ID,
                                value = s.Name
                            }).ToList()
                    }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddTitle(
            string name, int departmentID, int unitID)
        {
            try
            {
                _context.Titles.Add(
                    new Title()
                    {
                        Name = name,
                        DepartmentID = unitID == 1 ? departmentID : unitID,
                        CreateTime = DateTime.Now,
                        AddedBy = 0
                    });

                _context.SaveChanges();

                return Json("OK");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditTitle(
            int id, string name, int departmentID, int unitID)
        {
            try
            {
                var obj = _context.Titles.Where(c => c.ID == id).FirstOrDefault();
                bool b = _context.Titles.Contains(obj);

                if (b)
                {
                    obj.Name = name;
                    obj.DepartmentID = unitID == 1 ? departmentID : unitID;
                    obj.UpdateTime = DateTime.Now;
                    obj.Modifier = 1;

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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteTitle(int id)
        {
            try
            {
                var obj = _context.Titles.Where(c => c.ID == id).FirstOrDefault();
                var b = _context.Titles.Contains(obj);

                if (b)
                {
                    obj.Deleted = true;
                    obj.DeleteTime = DateTime.Now;
                    obj.DeletedBy = 1;

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
                return Json(ex.Message);
            }
        }
    }
}
