using Microsoft.AspNetCore.Identity;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.services
{
    public class UserRegistrationService
    {
        private readonly UserManager<Client> _userManager;

        public UserRegistrationService(UserManager<Client> userManager )
        {
            _userManager = userManager;
        }

        public async Task<CreateClientResult> Registration(CreateClientDto createClientDto)
        {
            Client client = CreateClient(createClientDto);
            var res = await _userManager.CreateAsync(client, createClientDto.Password);
            CreateClientResult createClientResult = new CreateClientResult
            {
                Client = client,
                IdentityResult = res,
            };
            return createClientResult;
        }

        private Client CreateClient(CreateClientDto createClientDto)
        {
            Client client = new Client();
            client.UserName = createClientDto.Email;
            client.NormalizedUserName = createClientDto.Email.ToUpper();

            client.Email = createClientDto.Email;
            client.NormalizedEmail = createClientDto.Email.ToUpper();
            if (createClientDto.UserType == UserType.Company)
            {
                client.UserType = UserType.Company;
                client.Company = new Company();
            }
            else if (createClientDto.UserType == UserType.Buyer)
            {
                client.UserType = UserType.Buyer;
                client.Buyer = new Buyer();
            }
            return client;
        }
    }
}
