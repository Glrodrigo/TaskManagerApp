using TaskManagerApp.Domain;

namespace TaskManagerApp.Service
{
    public interface ITaskManagerService
    {
        Task<bool> CreateAsync(TaskManagerDomain task);
        Task<List<TaskManagerBase>> GetAsync();
        Task<List<TaskManagerBase>> FindAsync(int pageNumber, TaskManagerBase.TaskManagerState status);
        Task<List<TaskManagerBase>> FilterTasksByIdAsync(int id);
        Task<bool> UpdateAsync(TaskManagerParams task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
