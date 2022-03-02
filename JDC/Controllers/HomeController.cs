using System;
using System.Collections.Generic;
using System.Linq;
using AwcBll.Adapters;
using AwcBll.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    /// <summary>
    /// Provides home endpoints.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Displays the documentation page.
        /// </summary>
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

        /// <summary>
        /// Displays the documantation page.
        /// </summary>
        [HttpPost]
        public IActionResult Contact()
        {
            return this.RedirectToAction("Documentation");
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
