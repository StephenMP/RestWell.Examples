namespace RestWell.Examples.CreatingARequest.Dtos
{
    public class ExampleRequestDto
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
