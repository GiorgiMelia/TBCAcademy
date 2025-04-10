namespace ITAcademy.Offers.Application.Exceptions
{
    public class WrongRequestException : Exception
    {

        public WrongRequestException(string resource) : base(resource) { }
    }
}
