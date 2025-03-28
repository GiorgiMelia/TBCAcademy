using Microsoft.AspNetCore.Identity;
using offers.itacademy.ge.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using offers.itacademy.ge.Domain.entities;

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
            var client = new Client
            {
                UserName = createClientDto.Email,
                NormalizedUserName = createClientDto.Email.ToUpper(),
                Email = createClientDto.Email,
                NormalizedEmail = createClientDto.Email.ToUpper(),
                UserType = createClientDto.UserType
            };

            if (createClientDto.UserType == UserType.Company)
            {
                client.Company = createClientDto.Company;
            }
            else if (createClientDto.UserType == UserType.Buyer)
            {
                client.Buyer = createClientDto.Buyer;
            }

            return client;
        }

    }
}
