namespace TwoeterApi.DTO
{
    public class Response<T>
    {
        public Response()
        {

        }

        public int Code { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
