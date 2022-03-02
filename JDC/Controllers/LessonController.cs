using System;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonService lessonService;

        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpPost]
        public async Task<int> CreateLesson(string theme, string homework, int lessonDuration, int disciplineId)
        {
            Lesson lesson = new Lesson()
            {
                Date = DateTime.Now,
                Theme = theme,
                Homework = homework,
                LessonDuration = (LessonDuration)lessonDuration,
                DisciplineId = disciplineId,
            };

            await lessonService.Create(lesson);

            return lesson.Id;
        }
    }
}
