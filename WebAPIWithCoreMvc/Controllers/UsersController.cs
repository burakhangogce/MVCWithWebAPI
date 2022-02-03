﻿using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAPIWithCoreMvc.ViewModels;

namespace WebAPIWithCoreMvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private string url = "http://localhost:31527/api/";

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<UserDetailDto>>(url + "Users/GetList");
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.GenderList = GenderFill();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDto userAddDto = new UserAddDto()
            {
                FirstName = userAddViewModel.FirstName,
                Gender = userAddViewModel.GenderID == 1 ?true :false,
                LastName = userAddViewModel.LastName,
                Adress = userAddViewModel.Adress,
                DateOfBirth = userAddViewModel.DateOfBirth,
                Email = userAddViewModel.Email,
                Password = userAddViewModel.Password,
                UserName = userAddViewModel.UserName,
            };
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url + "Users/Add", userAddDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        private List<Gender> GenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender() { Id = 1, GenderName = "Erkek" });
            genders.Add(new Gender() { Id = 2, GenderName = "Kadın" });
            return genders;
        }

        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }
    }
}
