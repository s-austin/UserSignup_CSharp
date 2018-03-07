using System;

namespace user_signup.Models {
    public class User
    {

        // TODO 1
        // create private FIELDS accessed through (protected by) public PROPERTIES 
        private string username;
        private string email;
        private string password;

        // create public PROPERTIES for the FIELDS
        public string Username { 
            get => username;
            set {
                if (value.Length < 5) {
                    // throwing an exception here to be handled at the Controller level
                    // lets us handle various exceptions using one exception handler
                    throw new ArgumentException("Username is too short.");
                }
                if (value.Length > 15) {
                    throw new ArgumentException("Username is too long.");
                }


                // throwing will kick out of the method (like a return statement) so the value
                // will only be set if it passes validation and reaches this line
                username = value;
            }
        }
        public string Email {
            get => email;
            set {
                // email validation here
                    // look for a built-in email validation method or use an external library
                    // never implement your own (insecure) use what the open source community
                    // has created and vetted (thousands of minds vs one)

                email = value; 
            }
        }

        public string Password {
            // the getter is private (only accessable internally)
            private get => password;
            set {
                if (value.Length < 5) {
                    throw new ArgumentException("Password is too short.");
                } 
                if (value.Length > 15) {
                    throw new ArgumentException("Password is too long.");
                }

                password = value;
            }
        }

        // public means of verifying a password (the password itself is never exposed)
        public bool VerifyPassword(string pass) => Password.Equals(pass);


        // counter of all users that have been instantiated (prevents overlapping IDs)
        // static so it is at the Class level (can be counted globally outside of all User instances)
        private static int NextId = 0;

        // can only be set internally (private setter)
        // defaults to the CURRENT VALUE of NextId and THEN increments NextId for the next instantiation
            // POSTfix increment (Variable++) opposed to PREfix increment (++Variable)
        public int UserId { get; } = NextId++;

        // can only be set internally (private setter)
        // defaults to current DateTime in string format
        public string CreateDate { get; } = DateTime.Now.ToString();
        
        // add a constructor to set the CreateDate when a new user is instantiated  

        // empty constructor (this will be BINDED to the Request handler in the Controller)
        // the model will be populated from form data sent in a Request
        public User() {}

        // for creating a user by other means (manually)
        public User(string u, string e, string p) {
            Username = u;
            Email = e;
            Password = p;
        }

        public override string ToString() {
            return string.Format(
                "UserId: {0}\nUsername: {1}\nEmail: {2}\n\n",
                UserId,
                Username,
                Email
            );

        }
    }
}