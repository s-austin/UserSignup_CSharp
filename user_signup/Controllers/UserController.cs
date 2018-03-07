using System;
using Microsoft.AspNetCore.Mvc;
using user_signup.Models;
using System.Collections.Generic;

namespace user_signup.Controllers {
    
    public class UserController : Controller {
// DATA
        public static UserData Users = UserData.Instance; // get the UserData Singleton Instance
        
        public class ViewContainer {
            public List<User> UserList { get; set; }
            public User User { get; set; }

            public ViewContainer() {
                UserList = Users.GetUsers();
                User = null;
            }

            public ViewContainer(User user) {
                UserList = Users.GetUsers();
                User = user;
            }
        }

        // "localhost:5000/user/"
        public IActionResult Index(User user) {
            ViewContainer viewContainer = new ViewContainer(user);
            return View("Index", viewContainer);
        }

        [Route("/user/userpage/{userId?}")] // this is a Dynamic Route example for displaying an individual user page
        public IActionResult UserPage(int userId) {
            // the userId parameter is extracted from the route
            // we could have also used a query string parameter (remember the url.com/userpage?userId=value style)
            // as an exercise try changing this route to use a query string parameter instead. good simple practice
            User user = Users.GetUserById(userId);
            return View(user);
        }

        [HttpGet] // redundant -> defaults to GET
        public IActionResult Add() {
            AddUserViewModel userViewModel = new AddUserViewModel();
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel userViewModel) {
            // This line will check if the ModelState (with the model being the userViewModel that was bound to the route)
            // is invalid then return the Add view with the user
            if (!ModelState.IsValid) return View("Add", userViewModel); 
            // perform validation before creating user -> pass errors into userViewModel.Errors as necessary
            User user = new User {
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                Password = userViewModel.Password
            };
                
            ViewContainer viewContainer = new ViewContainer(user);
                
            if (!user.VerifyPassword(userViewModel.Verify)) {
                userViewModel.Errors["verify"] = "Passwords do not match";
                return View("Add", userViewModel);
            }

            return View("Index", viewContainer);

        }
        
    

//       
        
        
    }
}