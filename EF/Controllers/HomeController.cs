using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;

namespace EF.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public void AddDisk(int disk) {
        //    this.disks.push(disk);
        //}
        //public void MoveTo(Tower t)
        //{
        //    int disk = this.disks.pop();
        //    this.AddDisk(disk);
        //}
        //public void MoveDIsks(int n, Tower t1, Tower t2) {
        //    MoveDIsks(n - 1, t1, t2);
        //    MoveTo(t2);
        //    t2.MoveDIsks(n - 1, t1, t2);
        //}
    }
}
