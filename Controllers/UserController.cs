using Microsoft.AspNetCore.Mvc;
using RiraTest.Core.ViewModels;
using RiraTest.Services.Interfaces;

namespace RiraTest.Controllers
{
    public class UserController : Controller
    {
        private readonly IMyUserService _myUserService;
                                                            
        public UserController(IMyUserService  myUserService)
        {
            _myUserService = myUserService;
        }

        public IActionResult Index()
        {
            var model = _myUserService.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new MyUserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyUserViewModel model)
        {
            var result = _myUserService.Save(model);
            var msg = result ? "رکورد با موفقیت ذخیره شد" : "متاسفانه خطایی در ذخیره رکورد رخ داد";
             return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var meta = _myUserService.GetById(id);
            var model = new MyUserViewModel()
            {
                FirstName = meta.FirstName,
                LastName = meta.LastName,
                NationalCode = meta.NationalCode,
                DateBirth = meta.DateBirth,
         
                Id = meta.Id,
             };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MyUserViewModel model)
        {
            var result = _myUserService.Update(model);
            var msg = result ? "رکورد با موفقیت ویرایش شد" : "متاسفانه خطایی در ویرایش رکورد رخ داد";
             return RedirectToAction("Index");
        }

    }
}
