using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using NetCoreMVCLAB5.Models;

namespace NetCoreMVCLAB5.Controllers
{
    public class AccountController : Controller
    {
        // Lưu tạm trong bộ nhớ (static để giữ dữ liệu giữa các request)
        private static readonly List<Account> accounts = new List<Account>();

        // GET: Account
        public ActionResult Index()
        {
            return View(accounts);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            Account model = new Account();
            return View(model);
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(Account model)
        {
            if (ModelState.IsValid)
            {
                // Tự tăng Id
                model.Id = accounts.Count == 0 ? 1 : accounts.Max(a => a.Id) + 1;
                accounts.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Remote validation cho Phone
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phone)
        {
            Regex _isPhone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            if (!_isPhone.IsMatch(phone))
            {
                return Json($"Số điện thoại {phone} không đúng định dạng, VD: 0986421127 hoặc 098.421.1127");
            }
            return Json(true);
        }
    }
}