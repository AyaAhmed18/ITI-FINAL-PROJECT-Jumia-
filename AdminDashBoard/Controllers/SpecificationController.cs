using Jumia.Application.IServices;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDashBoard.Controllers
{
    public class SpecificationController : Controller
    {
        private readonly ISpecificationServices _specificationServices;
        private readonly ISubCategorySpecificationsService _subCategorySpecificationsService;
        public SpecificationController(ISpecificationServices specificationServices, ISubCategorySpecificationsService subCategorySpecificationsService)
        {
            _specificationServices = specificationServices;
            _subCategorySpecificationsService= subCategorySpecificationsService;
        }
        // GET: SpecificationController
        public async Task<ActionResult> Index()
        {
            
           // ViewBag.spec = specifications.Entities.ToList();
            return View();
        }

        // GET: SpecificationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SpecificationController/Create
        public async Task<ActionResult> Create()
        {
           // var specifications = await _subCategorySpecificationsService.GetAll();
            var spec = (await _specificationServices.GetAll()).ToList();
            ViewBag.spec = spec;
            return View("Index");
        }

        // POST: SpecificationController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto,int subCategoryId)
        {
            try
            {
                var spec1 = (await _specificationServices.GetAll()).ToList();
                ViewBag.spec = spec1;
                if (ModelState.IsValid)
                {
                    if (subCategorySpecificationDto.SelectedSpecification != null)
                    {
                        foreach (var specItems in subCategorySpecificationDto.SelectedSpecification)
                        {
                            var selectedSpec = (await _specificationServices.GetAll()).Where(s => s.Name == specItems).FirstOrDefault().Id;
                            subCategorySpecificationDto.SubCategoryId = 1;
                            subCategorySpecificationDto.specificationId = selectedSpec;
                            var subCategorySpecification = new CreateOrUpdateSubCategorySpecificationDto
                            {
                                //SubCategoryId = subCategoryId,
                                SubCategoryId = 1,
                                specificationId = selectedSpec
                            };
                            var res = await _subCategorySpecificationsService.Create(subCategorySpecificationDto);
                            if (res.IsSuccess)
                            {
                                /*var spec1 = (await _specificationServices.GetAll()).ToList();
                                ViewBag.spec = spec1;*/
                                return RedirectToAction("Index");
                            }

                        }
                    }
                    

                   

                }
                // return View(CategoryDto);
                var spec = (await _specificationServices.GetAll()).ToList();
                ViewBag.spec = spec;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var spec = (await _specificationServices.GetAll()).ToList();
                ViewBag.spec = spec;
                return View("Index");
            }
        }

        // GET: SpecificationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SpecificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpecificationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpecificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
