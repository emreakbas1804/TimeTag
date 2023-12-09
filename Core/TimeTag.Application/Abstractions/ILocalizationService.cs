using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace TimeTag.Application.Abstractions;
public interface ILocalizationService
{
    Task<bool> addLocalization(int rlt_Language_Id, string tagName, string value);
    Task<string> getLocalization(string tagName, string defoultValue);
    Task<int> getLanguageIdByLangCode(string langCode);

}