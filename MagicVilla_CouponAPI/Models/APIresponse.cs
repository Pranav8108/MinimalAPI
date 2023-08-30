using System.Net;

namespace MagicVilla_CouponAPI.Models
{
    public class APIresponse
    {
        public APIresponse() { 
            ErrorMessages = new List<string>();
        
        }
        public bool IsSuccess { get; set; }
        public object result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
