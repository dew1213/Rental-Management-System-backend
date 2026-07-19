using Microsoft.EntityFrameworkCore;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Interfaces;
using RentalManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Infrastructure.Repositories
{
    public class HouseRepository : GenericRepository<House>, IHouseRepository
    {
        public HouseRepository(AppDbContext context)
            : base(context)
        { 
        }
            public async Task<bool> ExistsByHouseIdAsync(int houseId)
        {
            return await _context.Contracts
                .AnyAsync(c => c.HouseId == houseId);
        }
    }
}
