using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken)
        {
           return await _context.Companies.ToListAsync(cancellationToken);
        }

        public async Task<Company?> GetCompanyById(int companyId, CancellationToken cancellationToken)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyId,cancellationToken); 
        }

        public async Task SaveChanges(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveImage(string base64, int compId)
        {
            var company = await GetCompanyById(compId, CancellationToken.None);
            company.PhotoUrl=base64;
           await SaveChanges(CancellationToken.None);
        }
    }
}
