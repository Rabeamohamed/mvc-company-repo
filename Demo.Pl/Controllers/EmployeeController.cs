using Demo.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Demo.BLL.Repositrories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using AutoMapper;
using Demo.Pl.VeiwModels;
using System.Collections.Generic;
using Demo.Pl.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;



namespace Demo.Pl.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task <IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
                employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchValue);
           var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
           return View(MappedEmployees);
        }

        //[HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                await _unitOfWork.CompeleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel  >(employee);
            return View(ViewName, MappedEmployee);
        }

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    return Details(id, "Edit");
        //}


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(MappedEmployee);

            ///var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            ///if (employee is null)
            ///    return NotFound();
            ///ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            /// ✅ correctly map to view model
            ///var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            ///return View(mappedEmployee);


        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Employee employee, [FromRoute] int id)
        //{
        //    if (id != employee.Id)
        //        return BadRequest();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _unitOfWork.EmployeeRepository.Update(employee);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, ex.Message);
        //        }
        //    }
        //    return View(employee);
        //}

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(EmployeeViewModel employeeVm, [FromRoute] int id)
            {
                if (id != employeeVm.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    try
                    {
                    if (employeeVm.ImageName is not null)
                    {
                        employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                    }
                        var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                        _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                        await _unitOfWork.CompeleteAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                // ✅ This is important: ViewBag MUST be filled before rendering view
                ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();

                return View(employeeVm);
            } 


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);

                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                var Result =await _unitOfWork.CompeleteAsync();
                if (Result > 0 && employeeVm.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVm);
            }
        }

    }
}
