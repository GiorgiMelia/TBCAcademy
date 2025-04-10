using ITAcademy.Offers.Application.Dtos;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<CreateClientResult> Registration(CreateClientDto createClientDto);
        Task<CreateClientResult> RegisterBuyer(RegisterBuyerDto dto);
        Task<CreateClientResult> RegisterCompany(RegisterCompanyDto dto);
    }
}