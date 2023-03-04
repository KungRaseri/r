using System.Net;

namespace OpenRP.Framework.Common.Classes
{
    public struct RequestResponse
    {
        public HttpStatusCode Status;
        public WebHeaderCollection Headers;
        public string Content;
    }
}