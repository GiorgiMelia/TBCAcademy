namespace ITAcademy.Offers.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resource) : base(resource)
        {

        }
    }
}
