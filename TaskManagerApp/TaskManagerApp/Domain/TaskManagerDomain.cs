using static TaskManagerApp.Domain.TaskManagerBase;

namespace TaskManagerApp.Domain
{
    public class TaskManagerDomain
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public TaskManagerState Status { get; set; }
        public DateTime? ConcludedDate { get; set; }
    }
}
