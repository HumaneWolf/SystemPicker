using System.Threading.Tasks;

namespace SystemPicker.Matcher.SystemApis
{
    public interface ISystemApi
    {
        Task<SystemMatch> GetKnownMatch(string systemName);
    }
}