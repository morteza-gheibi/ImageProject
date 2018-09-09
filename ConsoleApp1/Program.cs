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
            var base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            var ss = uid.Length;
            var s = base64Guid.Length;
        }
    }
}
