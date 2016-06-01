using Nancy;

// ReSharper disable once CheckNamespace
namespace MachSecure.SVM.OverviewService
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["index"];
        }
    }
}
