using System.Collections.Generic;
using Nancy.Security;

// ReSharper disable once CheckNamespace
namespace MachSecure.SVM.OverviewService
{
    public class AuthenticatedUser : IUserIdentity
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<string> Claims { get; }
    }
}
