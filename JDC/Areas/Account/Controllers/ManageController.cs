using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JDC.Areas.Account.Models;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Account.Controllers
{
    [Area("Account")]
    public class ManageController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public ManageController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID {this.userManager.GetUserId(this.User)}.");
            }

            return this.View(await this.GetInputModelAsync(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexModel indexModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(indexModel);
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            if (indexModel.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, indexModel.Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    indexModel.StatusMessage = "Unexpected error when trying to set phone number.";
                }
            }

            user.LastName = indexModel.Input.LastName;
            user.FirstName = indexModel.Input.FirstName;
            user.MiddleName = indexModel.Input.MiddleName;
            user.Sex = indexModel.Input.Sex.Equals("Мужской", StringComparison.Ordinal);

            await this.userManager.UpdateAsync(user);
            await this.signInManager.RefreshSignInAsync(user);
            indexModel.StatusMessage = "Your profile has been updated";

            return this.View(indexModel);
        }

        public async Task<IActionResult> PersonalData()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.View();
        }

        public async Task<IActionResult> DeletePersonalDataModel()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.View(new DeletePersonalDataModel() { RequirePassword = await this.userManager.HasPasswordAsync(user) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePersonalDataModel(DeletePersonalDataModel deletePersonalDataModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            
            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (deletePersonalDataModel.RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, deletePersonalDataModel.Input.Password))
                {
                    this.ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return this.View(deletePersonalDataModel);
                }
            }

            var result = await this.userManager.DeleteAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await this.signInManager.SignOutAsync();

            return this.Redirect("~/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadPersonalDataModel()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(User).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            
            foreach (var propertyInfo in personalDataProps)
            {
                personalData.Add(propertyInfo.Name, propertyInfo.GetValue(user)?.ToString() ?? "null");
            }

            foreach (var login in await this.userManager.GetLoginsAsync(user))
            {
                personalData.Add($"{login.LoginProvider} external login provider key", login.ProviderKey);
            }

            this.Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
        }

        private async Task<IndexModel> GetInputModelAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            IndexModel indexModel = new IndexModel
            {
                Input = new IndexModel.InputModel()
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    PhoneNumber = phoneNumber,
                },
            };

            return indexModel;
        }
    }
}
