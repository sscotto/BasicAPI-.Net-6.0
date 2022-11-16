namespace BasicAPI.Models
{
    public class NumberResponse
    {
        public string Message { get; set; }
        public IList<int> Numbers { get; set; }
        public bool Success { get; set; }
    }
}
