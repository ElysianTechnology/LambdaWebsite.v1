using System;
using MachSecure.SEMS.DataObjects;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

// ReSharper disable once CheckNamespace
namespace MachSecure.SVM.OverviewService
{
    public class UserObject : IUserMapper
    {
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var data = User.LoadUser(identifier);

            if (data == null) return null;

            return new AuthenticatedUser
            {
                UserName = data.UserName,
                DisplayName = data.DisplayName
            };
        }
    }
}
