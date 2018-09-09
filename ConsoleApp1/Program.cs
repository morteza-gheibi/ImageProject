using ImageProject.DataLayout.Modal;
using ImageProject.DataLayout.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var uow = new GenericUoW();

            //uow.Repository<Work>().Insert(new Work { WorkTitle = "woek test" });
            //uow.SaveChanges();
            string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

            var ss = uid.Length;
            var s = base64Guid.Length;


           //var e= uow.Repository<Category>().GetAll();

            //var tb = new Category() { CategoryId = uid, CategoryName = "test", ParentId = 0 ,CreateDate=DateTime.Now , CreateUserId=1,IsActive=true,IsDelete=true };
            //uow.Repository<Category>().Insert(tb);
            //uow.SaveChanges();




        }
    }
}
