using System;
using System.Collections.Generic;
using System.Linq;
using AwcBll.Adapters;
using AwcBll.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Documentation()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult DownloadPersonalData()
        {
            string fileName = "galynets_nikita.docx";

            var characteristic = new Characteristic()
            {
                FileName = fileName,
                BoolValues = Enumerable.Range(1, 50).Select(t => true).ToList(),
                DateCreated = DateTime.Now.ToString(),
                StringValues = Enumerable.Range(1, 50).Select(t => "Город").ToList(),
            };

            var institutionConfig = new InstitutionConfig()
            {
                AssistantName = "AssistantName",
                DirectorName = "DirectorName",
                IstitutionName = "IstitutionName",
                Specialities = new List<string>()
                {
                    "PO",
                },
            };

            var memoryStream = new JDCAdapter().CreateCharacteristic(fileName, institutionConfig, characteristic);

            return this.File(memoryStream.GetBuffer(), "application/docx", fileName);
        }

        [HttpPost]
        public IActionResult Contact()
        {
            return this.RedirectToAction("Documentation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
