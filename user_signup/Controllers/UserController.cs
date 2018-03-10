﻿using System;
using Microsoft.AspNetCore.Mvc;
using user_signup.Models;
using System.Collections.Generic;
 using System.Linq;
 using SQLitePCL;
 using user_signup.Data;

namespace user_signup.Controllers {
    
    public class UserController : Controller {
        // DbContext
        private UserDbContext context;
        
        // provide a constructor that builds the controller with a context already set

        public UserController(UserDbContext dbContext) {
            context = dbContext;
        }
        
        //---------
        
        
        
        private static UserData Users = UserData.Instance; // get the UserData Singleton Instance
        
        public class ViewContainer {
            public List<User> UserList { get; set; }
            public User User { get; set; }

            public ViewContainer(User user = null) {
                UserList = Users.GetUsers();
                User = user;
            }
        }
        
        public IActionResult Index() {
//            List<User> Users = context.Users.ToList();
            return View("Index", new ViewContainer());
        }

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
            
            context.Users.Add(user); // add to DbSet Users
            context.SaveChanges(); // save the DB changes
                
            var viewContainer = new ViewContainer(user);

            return user.VerifyPassword(userViewModel.Verify) ?
                View("Index", viewContainer) :
                View("Add", userViewModel);
        }   
    }
}