using System.Threading.Tasks;
using TaskManagerApp.Domain;
using TaskManagerApp.Repository;

namespace TaskManagerApp.Service
{
    public class TaskManagerService
    {
        private readonly TaskManagerRepository _taskRepository;

        public TaskManagerService(TaskManagerRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskManagerBase> CreateAsync(TaskManagerDomain task)
        {
            var receivedTask = new TaskManagerBase(task.Title, task.Description, task.Status, task.ConcludedDate);

            if (receivedTask != null)
                receivedTask = await _taskRepository.InsertAsync(receivedTask);

            return receivedTask;
        }

        public async Task<List<TaskManagerBase>> GetAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks;
        }

        public async Task<List<TaskManagerBase>> FindAsync(int pageNumber, TaskManagerBase.TaskManagerState status)
        {
            List<TaskManagerBase> tasks = new List<TaskManagerBase>();
            var filter = new TaskManagerFilter(pageNumber, status);
            var pageSize = 20;

            if (filter.Status != null)
                tasks = await _taskRepository.FindByStatusAndPaginationAsync(pageNumber, pageSize, status);

            return tasks;
        }

        public async Task<List<TaskManagerBase>> FilterTasksByIdAsync(int id)
        {
            List<TaskManagerBase> tasks = new List<TaskManagerBase>();
            var taskId = TaskManagerByIdFilter.ValidateId(id);

            tasks = await _taskRepository.FindByIdAsync(taskId);

            return tasks;
        }

        public async Task<bool> UpdateAsync(TaskManagerParams task)
        {
            var taskId = TaskManagerByIdFilter.ValidateId(task.Id);
            var oldTask = await _taskRepository.FindByIdAsync(taskId);

            if (oldTask.Count == 0)
                return false;

            var updatedtask = task.ValidateUpdate(task.Title, task.Description, task.Status, task.ConcludedDate, oldTask[0]);

            await _taskRepository.UpdateAsync(updatedtask);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var taskId = TaskManagerByIdFilter.ValidateId(id);

            var result = await _taskRepository.DeleteAsync(taskId);

            return result;
        }
    }
}
