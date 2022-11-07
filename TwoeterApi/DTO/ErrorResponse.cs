

using System.Text.Json;

namespace TwoeterApi.DTO
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}
