using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample_Code.Models;
using Sample_Code.Repository;

namespace Sample_Code.Controllers
{
    public class PagingController : Controller
    {
        private readonly IRepository<Employee> _empRepo;
        public PagingController(IRepository<Employee> empRepo)
        {
            _empRepo = empRepo;
        }

        // GET: PagingController
        public ActionResult Index()
        {
            Paging paging = new Paging();
            paging =  GetCustomers(1).Result;
            return View(paging);
        }
       
        [HttpPost]
        public ActionResult Index(int currentPageIndex)
        {
            Paging paging = new Paging();
            paging = GetCustomers(currentPageIndex).Result;
            return View(paging);          
        }

        private async Task<Paging> GetCustomers(int currentPage)
        {
            int maxRows = 4;
            Paging customerModel = new Paging();
            var emp=await _empRepo.GetAll();
            IEnumerable<Employee> employee = emp;
            int val = (currentPage - 1) * maxRows;
            customerModel.items = employee.OrderBy(p=>p.EmpId).Skip(val).Take(maxRows).ToList();
            double pageCount = (double)((decimal)employee.Count() / Convert.ToDecimal(maxRows));     
            customerModel.PageCount = (int)Math.Ceiling(pageCount);
            customerModel.CurrentPageIndex = currentPage;
            return customerModel;
        }

    }
}
