using TaskManagerApp.Domain;

namespace TaskManagerApp.Repository
{
    public interface ITaskManagerRepository
    {
        Task<TaskManagerBase> InsertAsync(TaskManagerBase task);
        Task<List<TaskManagerBase>> GetAllAsync();
        Task<List<TaskManagerBase>> FindByIdAsync(int id);
        Task<List<TaskManagerBase>> FindByStatusAndPaginationAsync(int pageNumber, int pageSize, TaskManagerBase.TaskManagerState status);
        Task<bool> UpdateAsync(TaskManagerBase task);
        Task<bool> DeleteAsync(int id);
    }
}
