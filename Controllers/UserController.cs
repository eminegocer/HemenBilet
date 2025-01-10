using HemenBiletProje.Entities;
using HemenBiletProje.Services;
using HemenBiletProje.Repositories;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HemenBiletProje.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService(_userRepository);
        }

        // Login sayfasına yönlendirir
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Login işlemi
        [HttpPost]
        public async Task<ActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.LoginUserAsync(user.UserName, user.Password);
                if (existingUser != null)
                {
                    Session["UserId"] = existingUser.UserId.ToString();
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }

            return View(user);
        }

        // Signup sayfasına yönlendirir
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        // Kullanıcı kaydetme işlemi
        [HttpPost]
        public async Task<ActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
            {
                var isRegistered = await _userService.RegisterUserAsync(user);
                if (isRegistered)
                {
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Bu kullanıcı adı zaten kullanılıyor.");
            }

            return View(user);
        }

        // Kullanıcı çıkış işlemi (Logout)
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
