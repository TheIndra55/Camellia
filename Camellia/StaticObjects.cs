using System.Net.Http;

namespace Camellia
{
    public static class StaticObjects
    {
        public static HttpClient HttpClient { get; } = new();
    }
}
