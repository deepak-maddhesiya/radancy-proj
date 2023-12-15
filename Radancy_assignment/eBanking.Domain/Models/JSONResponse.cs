using System.Net;

namespace eBanking.Domain
{
    public class JSONResponse
    {
        /// <summary>
        /// Success flag of response
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Optional response data
        /// </summary>
        public dynamic? Body { get; set; }
    }
}
