namespace Hubtel.ProducerMessaging.Api.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse(string message, T data)
        {
            Message = message;
            Data = data;
        }

        public ApiResponse(string code, string status, string message, T data)
        {
            Code = code;
            Status = status;
            Message = message;
            Data = data;
        }

        public ApiResponse()
        {
        }

        public string Status { get; set; }
        public string Message { get; set; }

        public string Code { get; set; }
        public T Data { get; set; }
    }
}