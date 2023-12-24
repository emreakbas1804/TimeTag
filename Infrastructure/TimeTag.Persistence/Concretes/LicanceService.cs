using System;
using System.Collections.Generic;
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

    public async Task<string> AddLicance(List<string> tokens)
    {
        var serialNumber = Guid.NewGuid().ToString();        
        Licance licance = new()
        {
            IsAdded = false,
            SerialNumber = serialNumber,            
        };
        await _context.Licances.AddAsync(licance);        
        await _context.SaveChangesAsync();

        List<Company_Department_Employee_Token> tokenList = new();
        
        foreach (var token in tokens)
        {
            Company_Department_Employee_Token newToken = new()
            {
                rlt_Licance_Id = licance.Id,
                Token = token
            };
            tokenList.Add(newToken);
        }
        await _context.Employee_Tokens.AddRangeAsync(tokenList);

        await _context.SaveChangesAsync();
        return serialNumber;
    }

    public async Task<int> GetLicanceId(string serialNumber)
    {
        var licanceId = await _context.Licances.Where(q=> q.SerialNumber == serialNumber).Select(q=> q.Id).FirstOrDefaultAsync();
        return licanceId;
    }

    
}