using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class LicanceService : ILicanceService
{
    private readonly EntityDbContext _context;
    public LicanceService(EntityDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddLicance()
    {
        var serialNumber = Guid.NewGuid().ToString();        
        Licance licance = new()
        {
            IsAdded = false,
            SerialNumber = serialNumber,            
        };
        await _context.Licances.AddAsync(licance);
        await _context.SaveChangesAsync();
        return serialNumber;
    }

    public async Task<int> GetLicanceId(string serialNumber)
    {
        var licanceId = await _context.Licances.Where(q=> q.SerialNumber == serialNumber).Select(q=> q.Id).FirstOrDefaultAsync();
        return licanceId;
    }

    
}