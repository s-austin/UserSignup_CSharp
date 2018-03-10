﻿using System;
using Microsoft.AspNetCore.Mvc;
using user_signup.Models;
using System.Collections.Generic;

namespace user_signup.Controllers {
    
    public class UserController : Controller {
        private static UserData Users = UserData.Instance; // get the UserData Singleton Instance
        
        public class ViewContainer {
            public List<User> UserList { get; set; }
            public User User { get; set; }

            public ViewContainer(User user = null) {
                UserList = Users.GetUsers();
                User = user;
            }
        }
        
        public IActionResult Index() => View("Index", new ViewContainer());

        [Route("/user/userpage/{userId?}")]
        public IActionResult UserPage(int userId) => View(Users.GetUserById(userId));

        [HttpGet] // redundant -> defaults to GET
        public IActionResult Add() => View(new AddUserViewModel());

        [HttpPost]
        public IActionResult Add(AddUserViewModel userViewModel) {
            if (!ModelState.IsValid) return View("Add", userViewModel); 
            
            User user = new User {
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                Password = userViewModel.Password
            };
            
            Users.AddUser(user);
                
            var viewContainer = new ViewContainer(user);

            return user.VerifyPassword(userViewModel.Verify) ?
                View("Index", viewContainer) :
                View("Add", userViewModel);
        }   
    }
}