using offers.itacademy.ge.Application.Dtos;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<CreateClientResult> Registration(CreateClientDto createClientDto);

    }
}