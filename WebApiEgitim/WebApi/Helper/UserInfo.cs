using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WebApi.DataModel;

namespace WebApi.Helper
{
    public class UserInfo
    {
        public static Users CurrentUser()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;

            Users currentUser = new Users();
            currentUser.UserName = claims.First(k => k.Type == "UserName").Value;
            currentUser.Name = claims.First(k => k.Type == "Name").Value;
            currentUser.Surname = claims.First(k => k.Type == "Surname").Value;
            currentUser.RegisterNo = Convert.ToDecimal(claims.First(k => k.Type == "RegisterNo").Value);
            currentUser.Birthday = Convert.ToDateTime(claims.First(k => k.Type == "Birthday").Value);
            currentUser.Phone = claims.First(k => k.Type == "Phone").Value;
            currentUser.Mail = claims.First(k => k.Type == "Mail").Value;
            currentUser.Department = claims.First(k => k.Type == "Department").Value;
            currentUser.UserId = Convert.ToInt32(claims.First(k => k.Type == "UserId").Value);

            return currentUser;
        }
    }
}