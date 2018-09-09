using ImageProject.BLL;
using ImageProject.DataLayout.Modal;
using ImageProject.DataLayout.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Image.Web.Controllers
{
    
    public class AdminController : Controller
    {
        Bll bll = new Bll();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult Category()
        {
            var RowCount = 0;
            var cat=bll.CategoryItemGetAll(ref RowCount);
            ViewBag.RowCount = RowCount;
            return View(cat);
        }
        public Category CategoryInsert(Category category )=> bll.CategoryInsert(category);

        //==================================== categoryItem ==========================================

        public async Task<ActionResult> CategoryItem()=> View();
        public async Task<int> CategoryItemInsert(CategoryItem categoryItem)=>await bll.CategoryItemInsertAsync(categoryItem);
        public async Task<int> CategoryItemUpdate(CategoryItem categoryItem)=> await bll.CategoryItemUpdateAsync(categoryItem);
        public async Task<bool> CategoryItemDelete(int Id)=>await bll.CategoryItemDeleteAsync(Id);
        public JsonResult CategoryItemList(Paging paging, CategoryItem categoryItem = null)
        {
            var total = 0;
            var records = bll.CategoryItemGetFilter(categoryItem,paging, ref total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

    }
}