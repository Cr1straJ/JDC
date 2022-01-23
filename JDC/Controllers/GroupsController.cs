using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JDC.Controllers
{
    /// <summary>
    /// Provides group endpoints.
    /// </summary>
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IGradeService gradeService;
        private readonly ILessonService lessonService;
        private readonly ITeacherService teacherService;
        private readonly ISpecialityService specialityService;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsController"/> class.
        /// </summary>
        public GroupsController(
            UserManager<User> userManager,
            IGroupService groupService,
            IGradeService gradeService,
            ILessonService lessonService,
            ITeacherService teacherService,
            ISpecialityService specialityService)
        {
            this.userManager = userManager;
            this.groupService = groupService;
            this.gradeService = gradeService;
            this.lessonService = lessonService;
            this.teacherService = teacherService;
            this.specialityService = specialityService;
        }

        /// <summary>
        /// Gets all groups of the institution whose director is the beneficiary.
        /// </summary>
        /// <param name="userId">User id.</param>
        public async Task<IActionResult> Index(int? userId)
        {
            bool canEditGroup = false;

            if (userId is null)
            {
                User user = await this.userManager.GetUserAsync(this.User);

                if (user is not null && await this.userManager.IsInRoleAsync(user, "Director"))
                {
                    canEditGroup = true;
                    userId = user.InstitutionId;
                }
            }

            this.ViewData["CanEditGroup"] = canEditGroup;
            List<Group> groups = await this.groupService.GetInstitutionGroups(userId);

            if (groups is null)
            {
                return this.View("Error");
            }

            return this.View(groups);
        }

        /// <summary>
        /// Displays the creation page of the group.
        /// </summary>
        public async Task<IActionResult> Create()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            this.ViewData["Specialities"] = new SelectList(await this.specialityService.GetInstitutionSpecialities(currentUser.InstitutionId));
            this.ViewData["Teachers"] = new SelectList(await this.teacherService.GetInstitutionTeachers(currentUser.InstitutionId), "Id", "User.ShortName");

            return this.View();
        }

        /// <summary>
        /// Creates group.
        /// </summary>
        /// <param name="group">Create group request information.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TeacherId")] Group group)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (this.ModelState.IsValid)
            {
                group.InstitutionId = currentUser.InstitutionId.Value;
                await this.groupService.Add(group);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(group);
        }

        /// <summary>
        /// Displays the group editing page.
        /// </summary>
        /// <param name="groupId">Group id.</param>
        public async Task<IActionResult> Edit(int? groupId)
        {
            var group = this.groupService.GetById(groupId);

            if (group is null)
            {
                return this.View("Error");
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);

            this.ViewData["Specialities"] = new SelectList(await this.specialityService.GetInstitutionSpecialities(currentUser.InstitutionId));
            this.ViewData["Teachers"] = new SelectList(await this.teacherService.GetInstitutionTeachers(currentUser.InstitutionId), "Id", "User.ShortName");

            return this.View(group);
        }

        /// <summary>
        /// Edits group.
        /// </summary>
        /// <param name="group">Edit group request information.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,TeacherId")] Group group)
        {
            if (this.ModelState.IsValid)
            {
                await this.groupService.Update(group);

                return this.RedirectToAction(nameof(this.Index));
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);

            this.ViewData["Specialities"] = new SelectList(await this.specialityService.GetInstitutionSpecialities(currentUser.InstitutionId));
            this.ViewData["Teachers"] = new SelectList(await this.teacherService.GetInstitutionTeachers(currentUser.InstitutionId), "Id", "User.ShortName");

            return this.View(group);
        }

        /// <summary>
        /// Deletes group.
        /// </summary>
        /// <param name="groupId">Group id.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? groupId)
        {
            var group = await this.groupService.GetById(groupId);

            if (group is null)
            {
                return this.View("Error");
            }

            await this.groupService.Remove(group);

            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Displays the group view page.
        /// </summary>
        /// <param name="groupId">Group id.</param>
        public IActionResult Details(int? groupId)
        {
            var group = this.groupService.GetById(groupId);

            if (group is null)
            {
                return this.View("Error");
            }

            return this.View(group);
        }

        /// <summary>
        /// Displays a page for viewing the group's grades for the selected lesson.
        /// </summary>
        /// <param name="groupId">Group id.</param>
        /// <param name="lessonId">Lesson id.</param>
        public IActionResult Journal(int? groupId, int? lessonId)
        {
            this.ViewData["DisciplineId"] = lessonId;
            Lesson lesson = new Lesson()
            {
                Id = 0,
                Homework = "№123, №126",
                Theme = "§ 1.2. Корень n-ой степени",
                Date = new DateTime(DateTime.Now.Year, 9, 1),
            };

            if (groupId == -1)
            {
                return this.View(new Group()
                {
                    Id = 1,
                    Name = "PO-2",
                    Students = new List<Student>()
                    {
                        new Student()
                        {
                            Id = 0,
                            User = new User() { FirstName = "Захар", LastName = "Кошин", MiddleName = "Михайлович" },
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
                            User = new User() { FirstName = "Иван", LastName = "Иванов" },
                        },
                        new Student()
                        {
                            Id = 2,
                            User = new User() { FirstName = "Сергей", LastName = "Сидоров" },
                        },
                        new Student()
                        {
                            Id = 3,
                            User = new User() { FirstName = "Пётр", LastName = "Петров" },
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

            return this.View(this.groupService.GetById(groupId));
        }

        /// <summary>
        /// Sets a grade.
        /// </summary>
        /// <param name="studentId">Student id.</param>
        /// <param name="lessonId">Lesson id.</param>
        /// <param name="value">Value of the grade.</param>
        /// <param name="date">Grading date.</param>
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

            await this.gradeService.Add(grade);

            return grade.Id;
        }

        /// <summary>
        /// Updates a grade.
        /// </summary>
        /// <param name="gradeId">Grade id.</param>
        /// <param name="value">Value of the grade.</param>
        [HttpPost]
        public async Task UpdateGrade(int gradeId, double value)
        {
            Grade grade = await this.gradeService.GetById(gradeId);
            grade.Value = value;

            await this.gradeService.Update(grade);
        }

        /// <summary>
        /// Creates a lesson.
        /// </summary>
        /// <param name="theme">Lesson theme.</param>
        /// <param name="homework">Lesson homework.</param>
        /// <param name="lessonDuration">Lesson duration.</param>
        /// <param name="disciplineId">Discipline id.</param>
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

            await this.lessonService.Create(lesson);

            return lesson.Id;
        }
    }
}
