using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // you need this library for data validation

namespace user_signup.Models {
    public class AddUserViewModel {
        /**
         * the [] "annotations" above each PROPERTY server as validation
         * think of them like "decorators" from Python. except they "wrap" around a property and control
         * the behavior during the "setting" of the value
         *
         * 
         * an instance of the AddUserViewModel is BINDED to the /Add [POST] route
         * in the VIEW for /Add the form elements are all tied explicitly to this model
         *
         * what these annotations do is perform validation on each property when it is "set"
         * by the form elements the user submits. pretty neat!
         *
         * here is a great example of when to use the core functionality of a library or framework
         * instead of writing a half-baked solution like i did originally
         *
         * Thanks Vic!
         */
        
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Invalid username. Must be between 5 and 20 characters long.")]
        public string Username { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Invalid password. Must be between 5 and 20 characters long.")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string Verify { get; set; }
        
        // using the annotations rids you of using half-baked solutions like this Error dictionary!
//        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}