using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using productapp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Controllers
{
    public class productController : Controller
    {
        private readonly productcontext sdb;
        public productController(productcontext sb)
        {
            sdb = sb;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult loginn(IFormCollection fc)
        {
            String uname = fc["us"];
            String pswd = fc["ps"];
            
            var a = sdb.logins.Where(l => l.username.Equals(uname) && l.password.Equals(pswd)).FirstOrDefault();
            if(a!=null)
            {
            if(a.type=="admin")
            {
                    HttpContext.Session.SetInt32("admin", a.u_id);
                    return View("adminpage");
            }
            if (a.type == "user")
            {
                    HttpContext.Session.SetInt32("user", a.u_id);
                    return View("userpage");
            }
            }
            return View(); 
        }
        public IActionResult addcategory(IFormCollection fc)
        {
            return View();
        }
            [HttpPost]
        public IActionResult adddcategory1(IFormCollection fc)
        {
            category de = new category
            {
                category_name = fc["txt1"].ToString()
            };
            sdb.categories.Add(de);
            sdb.SaveChanges();
            return RedirectToAction("displcategory");
        }
        public IActionResult displcategory()
        {
            var d = sdb.categories.ToList();
            return View(d);
        }
        [HttpPost]
        public ActionResult searchcategory(IFormCollection fc)
        {
            String name = fc["aa"];
            var a = from c in sdb.categories where c.category_name.Contains(name) select c;
            ViewBag.msg = a;
            return View("displcategory", a);
        }
        public IActionResult addproduct()
        {
            ViewBag.de = sdb.categories.Select(x => new SelectListItem { Text = x.category_name, Value = x.category_id.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult addproduct1(IFormCollection fc, IFormFile image)
        {
            string ImageName = "";
            if (image != null)
            {

                //Set Key Name
                ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                //Get url To Save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImageName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }
            product s = new product
            {
                prduct_name = fc["txt1"].ToString(),
              
                cat_id = Convert.ToInt32(fc["category"]),
                price = Convert.ToInt32(fc["txt3"]),
                description = fc["txt4"].ToString(),
               
                photo = ImageName
            };
            sdb.products.Add(s);
            sdb.SaveChanges();

            return RedirectToAction("displproduct");
        }
        public IActionResult displproduct()
        {
            var cc = (from doc in sdb.products
                      join de in sdb.categories on doc.cat_id equals de.category_id
                      select new { deid = de.category_id, dename = de.category_name, cid = doc.product_id, cname = doc.prduct_name, pr = doc.price, desc = doc.description, pic = doc.photo }).ToList().
                      Select(x => new product() { cat_id = x.deid, cat_name = x.dename, product_id = x.cid, prduct_name = x.cname, price = x.pr, description = x.desc, photo = x.pic });
            return View(cc);
        }
        public IActionResult deleteproduct(int id)
        {
            product d = sdb.products.Find(id);
            sdb.products.Remove(d);
            sdb.SaveChanges();
            return RedirectToAction("displproduct");
        }
        [HttpPost]
        public IActionResult searchproduct(IFormCollection fc)
        {
            String name = fc["aa"];
            var cc = (from doc in sdb.products
                      join de in sdb.categories on doc.cat_id equals de.category_id where doc.prduct_name.Contains(name)
                      select new { deid = de.category_id, dename = de.category_name, cid = doc.product_id, cname = doc.prduct_name, pr = doc.price, desc = doc.description, pic = doc.photo }).ToList().
                      Select(x => new product() { cat_id = x.deid, cat_name = x.dename, product_id = x.cid, prduct_name = x.cname, price = x.pr, description = x.desc, photo = x.pic });

            return View("displproduct", cc);
        }
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adduser(IFormCollection fc)
        {

            user s = new user
            {
                name = fc["txt1"].ToString(),
                place = fc["txt2"].ToString(),
                phone = Convert.ToInt64(fc["txt3"])
            };

        
           
            sdb.users.Add(s);
            sdb.SaveChanges();
            login l = new login
            {
                username = fc["txt4"].ToString(),
                password = fc["txt5"].ToString(),
                type = "user",
                u_id = s.user_id
            };
            sdb.logins.Add(l);
            sdb.SaveChanges();
            return View("Index");
        }
        public IActionResult productview()
        {
            var cc = (from doc in sdb.products
                      join de in sdb.categories on doc.cat_id equals de.category_id
                      select new { deid = de.category_id, dename = de.category_name, cid = doc.product_id, cname = doc.prduct_name, pr = doc.price, desc = doc.description, pic = doc.photo }).ToList().
                      Select(x => new product() { cat_id = x.deid, cat_name = x.dename, product_id = x.cid, prduct_name = x.cname, price = x.pr, description = x.desc, photo = x.pic });
            return View(cc);
        }
        public IActionResult addcart(int id)
        {
            HttpContext.Session.SetInt32("prdid", id);
            return View();
        }
        [HttpPost]
        public IActionResult adddcart1(IFormCollection fc)
        {
            int pid = Convert.ToInt32(HttpContext.Session.GetInt32("prdid"));
            int uid = Convert.ToInt32(HttpContext.Session.GetInt32("user"));
            cart de = new cart
            {
                prdct_id = pid,
                quantity = fc["txt1"].ToString(),
                u_id= uid
        };
            sdb.carts.Add(de);
            sdb.SaveChanges();
            HttpContext.Session.SetInt32("user", uid);
            return RedirectToAction("displcart");
        }
        public IActionResult displcart()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetInt32("user"));
            
            var cc = (from doc in sdb.carts
                      join de in sdb.products on doc.prdct_id equals de.product_id
                      where doc.u_id==uid
                      select new { deid = de.product_id, dename = de.prduct_name, price = de.price, deph = de.photo }).ToList().
                      Select(x => new product() { product_id = x.deid, prduct_name = x.dename, price = x.price, photo = x.deph});
            return View(cc);
        }
        public IActionResult deletecart(int id)
        {
            product d = sdb.products.Find(id);
            sdb.products.Remove(d);
            sdb.SaveChanges();
            return RedirectToAction("displcart");
        }
    }
}
