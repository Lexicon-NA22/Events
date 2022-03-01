using Events.Core.Entities;

namespace Events.Core.Repositories
{
    public interface IEventRepository
    {
        Task AddAsync(CodeEvent codeEvent);
        Task<IEnumerable<CodeEvent>> GetAsync(bool includeLectures);
        Task<CodeEvent> GetAsync(string name, bool includeLectures);
    }
}