using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TimeTag.Application.Abstractions;
using TimeTag.Domain.Entities;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class LocalizationService : ILocalizationService
{
    private readonly EntityDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalizationService(EntityDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<bool> addLocalization(int rlt_Language_Id, string tagName, string value)
    {
        try
        {
            Localization localization = new()
            {
                rlt_Language_Id = rlt_Language_Id,
                TagName = tagName,
                Value = value
            };
            await _context.Localizations.AddAsync(localization);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }

    }

    public async Task<string> getLocalization(string tagName, string defoultValue)
    {
        try
        {
            var langCode = _httpContextAccessor?.HttpContext?.Items["langCode"] as string ?? "en";
            var localizationEntity = await _context.Localizations.Where(q => q.TagName == tagName && q.Language.LangCode == langCode).FirstOrDefaultAsync();
            if (localizationEntity == null)
            {
                return defoultValue;
            }
            return localizationEntity.Value;

        }
        catch (System.Exception)
        {
            return "";
        }
    }

    public Task<int> getLanguageIdByLangCode(string langCode)
    {
        var langId = _context.Languages.Where(q => q.LangCode == langCode).Select(c => c.Id).FirstOrDefaultAsync();
        return langId;
    }

}