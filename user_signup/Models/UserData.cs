using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace user_signup.Models {
    public class UserData
    {
        //TODO 2: instantiate a few new users in code here and add them to your users list
        //then write methods to add users to your list, return all users and return a user by UserId


        // static 
        private List<User> _users = new List<User> {
            new User("Vampiire", "vamp@gmail.com", "testPassword"),
            new User("NotVamp", "notvamp@gmail.com", "passwordTest")
        };

        public void AddUser(User user) {
            _users.Add(user);
        }

        public void RemoveUser(User user) {
            // check if user exists first?
            _users.Remove(user);
        }

        public List<User> GetUsers() => _users;
        
        /*
         *
         * This is called a Lambda Expression
         * These are equivalent
         * public List<User> GetUsers() { return _users; }
         * 
         */

        // this is equivalent to the GetUser() method down below on line 52
//        public User GetUserById(int userId) {
//            User foundUser = null;
//            foreach (User u in _users) {
//                if (u.UserId.Equals(userId)) {
//                    foundUser = u;
//                    break;
//                }
//            }
//
//            return foundUser;
//        }
        
        public User GetUserById(int userId) => _users.Find(u => u.UserId.Equals(userId)); // get by User instance UserId value


    // Singleton design pattern to enforce a single list of users

        // private constructor
        private UserData() {}
        // static instance of the class (starts off as null)
        private static UserData instance = null;

        // this is a simplified Singleton that is NOT thread safe
        // see this article for more detail http://csharpindepth.com/Articles/General/Singleton.aspx
        public static UserData Instance {
            get {
                if (instance == null) {
                    instance = new UserData();  
                }
                return instance;
            }
        }

        // UserData.Instance.ToString()
        public override string ToString() {
            string outputString = string.Format(
                "\nUSER LIST [{0} users]\n\n",
                _users.Count
            );
            
            _users.ForEach(user => outputString += user.ToString());
            return outputString;


            /*
            This is the same as 
            for(int i = 0; i < Users.Length; ++i) {
                OutputString = OutputString + Users[i].ToString()
            }

            Concatenates an output string with the User string of each User object in the list
            remember that User.ToString() has been overriden in the User.cs Class file
            to give a String representation of the User instance
            */
        }
	}
}