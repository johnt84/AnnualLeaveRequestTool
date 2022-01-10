using SimpleInjector;
using System.Net.Http;

namespace AnnualLeaveRequestToolWebForms
{
    public static class GlobalSettings
    {
        public static Container Container;
        public static HttpClient HttpClient;
    }
}