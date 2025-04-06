namespace offers.itacademy.ge.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resource) : base(resource)
        {

        }
    }
}
