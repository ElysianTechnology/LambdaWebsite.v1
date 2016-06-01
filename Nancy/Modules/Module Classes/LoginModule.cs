using System.Collections.Generic;
using MachSecure.SEMS.DataObjects;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace MachSecure.SVM.OverviewService.Nancy.Modules.Module_Classes
{
    public class LoginModule : NancyModule
    {
        public LoginModule() : base("/api")
        {
            //logs the user in using sems data object
            Post["/login"] = _ =>
            {
                //bind to object model
                var loginParams = this.Bind<Login>();
                //pass sems data obejct user method the username
                var user = User.LoadUser(loginParams.Username);
                //if no user, return 401
                if (user == null)
                    return HttpStatusCode.Unauthorized;
                //if incorrect password, return 401
                if (user.Password != loginParams.Password) return HttpStatusCode.Unauthorized;
                var token = user.RecordID;
                //login but dont redirect, this is handled front end by angular
                return this.LoginWithoutRedirect(token);
            };

            //log the user out and redirect to home
            Get["/logout"] = _ => this.LogoutAndRedirect("../#/");

            //Gets the current logged in user
            Get["/user"] = _ =>
            {
                var user = Context.CurrentUser;
                return Response.AsJson(user);
            };

            Get["/test"] = _ =>
            {
                List<string> s = new List<string>();

                for (int i = 0; i < 1000000; i++)
                {
                    s.Add("test");
                }



                return Response.AsJson(s);
            };

        }
    }
}
