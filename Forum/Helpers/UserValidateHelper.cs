using Forum.Models;
using System.Text.RegularExpressions;

namespace Forum.Helpers
{
    public static class UserValidateHelper
    {
        public static bool ValidateUserCreateInput(UserInput userInput)
        {
            return ValidateUsername(userInput.Username) && ValidateEmail(userInput.Email) && ValidatePassword(userInput.Password);
        }
        public static bool ValidateUserUpdateInput(UserInput userInput)
        {
            return ValidateUsername(userInput.Username) && ValidateEmail(userInput.Email) && ValidateBio(userInput.Bio);
        }
        public static bool ValidateUsername(string username)
        {
            return Regex.IsMatch(username.ToLower().Trim(),
              @"^[a-z0-9_.]+$");
        }
        public static bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email.ToLower().Trim(),
              @"^(?=.{0,64}$)[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }
        public static bool ValidateBio(string bio)
        {
            return bio.Length <= 100;
        }
        public static bool ValidatePassword(string password)
        {
            return Regex.IsMatch(password.ToLower().Trim(),
              @"^(?=.*\d)(?=.*[a-z])(?=.*[a-zA-Z]).{8,21}$");
        }
    }
}
