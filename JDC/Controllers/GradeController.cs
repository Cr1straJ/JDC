using System;
using System.Globalization;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService gradeService;

        public GradeController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [HttpPost]
        public async Task<int> SetGrade(int studentId, int lessonId, double value, string date)
        {
            DateTime billingDate = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture);
            Grade grade = new Grade()
            {
                StudentId = studentId,
                LessonId = lessonId,
                Value = value,
                BillingDate = billingDate,
            };

            await gradeService.Add(grade);

            return grade.Id;
        }

        [HttpPost]
        public async Task UpdateGrade(int gradeId, double value)
        {
            Grade grade = await gradeService.GetById(gradeId);
            grade.Value = value;

            await gradeService.Update(grade);
        }
    }
}
