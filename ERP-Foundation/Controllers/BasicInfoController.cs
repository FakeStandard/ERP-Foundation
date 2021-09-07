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
                    data = _context.Customers.Where(c => c.Deleted == false && c.Name.ToUpper().Contains(SearchText.ToUpper())).Select(
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
                    data = _context.Vendors.Where(c => c.Deleted == false && c.Name.ToUpper().Contains(SearchText.ToUpper())).Select(
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
    }
}
