using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer
{
    public  class Messages
    {
        public static string UserLoginmessage = "Your Username Or Password Is Wrong";
        public static string FargotPasswordEmailMessage = "The E-mail Address You Entered Is Incorrect";
        public static string ResetPasswordTokenNullError = "An Unknown Error Has Occurred, Please Contact the Company";
        public static string ResetPasswordSuccessMessage = "Your Password Change Has Been Successfully Made";
        public static string PageNotFound = "Page Not Found";
        public static string Unauthorized = "You have logged in without permission, please go to the previous page";
        public static string ServicessError = "An unknown error has occurred, then try again";
        public static string RequiredMessage = "Zorunlu Alan";
    }
}
