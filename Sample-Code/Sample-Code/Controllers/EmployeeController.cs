using Sample_Code.Models;
using Sample_Code.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample_Code.Enums;
using Sample_Code.Services;


namespace Sample_Code.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _empRepo;
        public EmployeeController(IRepository<Employee> empRepo)
        {
            _empRepo = empRepo;
        }

        // GET: EmployeeController
        public async Task< ActionResult> Index(string? alert)
        {
            var data =await _empRepo.GetAll();
            ViewBag.Alert=alert;
            return View(data);
        }     

        // GET: EmployeeController/Create
        #pragma warning disable CS1998 //Async method lacks 'await' operators and will run synchronously
        public async Task<ActionResult> Create()
        {          
            return PartialView("_Create");
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] EmployeeViewModel collection)
        {
            try
            {
                int result = 0;
                if (ModelState.IsValid)
                {                   
                        Employee objEmp = new Employee();
                        objEmp.EmpName = collection.EmpName;
                        objEmp.Address = collection.Address;
                        objEmp.Email = collection.Email;
                        objEmp.Phone = collection.Phone;
                        objEmp.BankAccountNo = collection.BankAccountNo;
                        objEmp.CreatedOn = DateTime.Now;
                        objEmp.CreatedBy = "SYSTEM";
                        objEmp.ModifiedOn = null;
                        objEmp.ModifiedBy = null;
                        objEmp.IsDeleted = false;
                        result =await _empRepo.Add(objEmp);
                        //return RedirectToAction("Index");
                        if (result > 0)
                        {
                          ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Employee added");
                        }
                        else
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown error");

                }
                return PartialView("_Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task< ActionResult> Edit(int id)
        {
            EmployeeViewModel objViewModel = new EmployeeViewModel();
            Employee objEmp = new Employee();
            if (id > 0) {
                objEmp =await _empRepo.Get(id);              
                objViewModel.EmpName = objEmp.EmpName;
                objViewModel.Address = objEmp.Address;
                objViewModel.Email = objEmp.Email;
                objViewModel.Phone = objEmp.Phone;
                objViewModel.BankAccountNo = objEmp.BankAccountNo;
                objViewModel.EmpId = objEmp.EmpId;                                
            }
            return PartialView("_Edit", objViewModel);            
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] EmployeeViewModel collection)
        {
            try
            {             
                int result = 0;
                if (ModelState.IsValid)
                {
                    Employee objEmp = new Employee();
                    objEmp.EmpId =(int) collection.EmpId;
                    objEmp.EmpName = collection.EmpName;
                    objEmp.Address = collection.Address;
                    objEmp.Email = collection.Email;
                    objEmp.Phone = collection.Phone;
                    objEmp.BankAccountNo = collection.BankAccountNo;                 
                    objEmp.ModifiedOn = DateTime.Now;
                    objEmp.ModifiedBy = "SYSTEM";
                    objEmp.IsDeleted = false;
                    result =await _empRepo.Update(objEmp);                  
                    if (result > 0)
                    {
                        ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Record updated");
                    }
                    else
                        ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown error");

                    return RedirectToAction(nameof(Index), new { alert = ViewBag.Alert });
                } 
                else
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "There is something wrong");
                               
                return RedirectToAction(nameof(Index), new { alert = ViewBag.Alert });
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Employee objEmp = new Employee();
            EmployeeViewModel objViewModel = new EmployeeViewModel();
            if (id > 0)
            {
                objEmp = await _empRepo.Get(id);
                objViewModel.EmpName = objEmp.EmpName;
            }
            return PartialView("_Delete", objViewModel);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EmployeeViewModel collection)
        {
            int result = 0;
            Employee objEmp = new Employee();
            EmployeeViewModel objViewModel = new EmployeeViewModel();
            try
            {
                if (id> 0)
                {
                    objEmp = await _empRepo.Get(id);
                    if (!objEmp.IsDeleted)
                    {
                        result = await _empRepo.Delete(objEmp);
                        if (result > 0)
                        {
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Record Deleted");
                        }
                        else
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown error");
                    }
                }               
                return RedirectToAction(nameof(Index), new { alert = ViewBag.Alert });              
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
