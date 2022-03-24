using System;
using System.Collections.Generic;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class JournalController : Controller
    {
        private readonly IGroupService groupService;

        public JournalController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public IActionResult Journal(int? groupId, int? lessonId)
        {
            ViewData["DisciplineId"] = lessonId;
            Lesson lesson = new Lesson()
            {
                Id = 0,
                Homework = "№123, №126",
                Theme = "§ 1.2. Корень n-ой степени",
                Date = new DateTime(DateTime.Now.Year, 9, 1),
            };

            if (groupId == -1)
            {
                return View(new Group()
                {
                    Id = 1,
                    Name = "PO-2",
                    Students = new List<Student>()
                    {
                        new Student()
                        {
                            Id = 0,
                            User = new User
                            {
                                FirstName = "Иван",
                                LastName = "Иванов",
                                MiddleName = "Иванович",
                            },
                            Grades = new List<Grade>()
                            {
                                new Grade() { Id = 0, Value = 6, BillingDate = new DateTime(DateTime.Now.Year, 9, 1), Lesson = lesson },
                                new Grade() { Id = 1, Value = 4, BillingDate = new DateTime(DateTime.Now.Year, 9, 2), LessonId = 1 },
                                new Grade() { Id = 2, Value = 9, BillingDate = new DateTime(DateTime.Now.Year, 9, 4), Lesson = lesson },
                            },
                        },
                        new Student()
                        {
                            Id = 1,
                            User = new User
                            {
                                FirstName = "Сергей",
                                LastName = "Сидоров",
                            },
                        },
                        new Student()
                        {
                            Id = 2,
                            User = new User
                            {
                                FirstName = "Пётр",
                                LastName = "Петров",
                            },
                        },
                    },
                    Disciplines = new List<Discipline>()
                    {
                        new Discipline()
                        {
                            Id = 0,
                            Title = "Математика",
                            Lessons = new List<Lesson>()
                            {
                               lesson,
                            },
                        },
                        new Discipline() { Id = 1, Title = "Физика" },
                    },
                });
            }

            return View(groupService.GetById(groupId.Value));
        }
    }
}
