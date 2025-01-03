namespace Workout.Domain.Core.Responses
{
    public class Response<T>
    {
        public string Message { get;  set; }
        public DateTime UpdateDate { get; set; }
        public IEnumerable<T> Data { get;  set; }
        public Response(string message,
                        IEnumerable<T> data)
        {
            Message = message;
            Data = data;
            UpdateDate = DateTime.Now;
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        public ErrorResponse(string message)
        {
            Message = message;
        }
        public static ErrorResponse Error(string message) => new ErrorResponse(message);
    }
}
