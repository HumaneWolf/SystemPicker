using System.Threading.Tasks;
using SystemPicker.Matcher.Models;

namespace SystemPicker.Matcher.SystemApis
{
    public interface ISystemApi
    {
        Task<SystemMatch> GetKnownMatch(string systemName);
    }
}