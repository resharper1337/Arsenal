using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.ServerHubs;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private IHubContext<MainHub> hubContext;

        public HomeController(ILogger<HomeController> _logger, IHubContext<MainHub> _hubContext)
        {
            hubContext = _hubContext;
            logger = _logger;
        }

        public IActionResult Index()
        {
            //Отображаем подключенных пользователей
            return View(StaticData.UsersList);
        }

        [AcceptVerbs("GET")]
        [Route("GetScreen")]
        public IActionResult GetScreen(string id) //Get ScreenShot
        {
            //Создаем путь и название для будущего скрина
            string fileName = Path.GetTempFileName().Replace(".tmp", ".png").Replace(".TMP", ".png");
            hubContext.Clients.Client(id).SendAsync("GetScreen", fileName); //Отправялем клиенту запрос на скрин
            Thread.Sleep(6000); //Ожидаем пока клиент загрузит скриншот HTTP POST запросом. Костыль(
            //Загруженный на сервер файл возвращаем админу
            var image = System.IO.File.OpenRead(fileName);
            return File(image, "image/png");
        }


        [AcceptVerbs("POST")]
        [Route("UploadScreen")]
        //POST запрос для загрузки скрина на сервер
        public async Task<IActionResult> UploadScreen(IFormFile uploadedFile, string fileName)
        {
            if (uploadedFile != null)
            {
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }
            return new JsonResult(fileName);

        }
    }
}