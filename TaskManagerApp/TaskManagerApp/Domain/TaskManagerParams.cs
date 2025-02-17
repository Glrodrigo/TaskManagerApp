using System.Threading.Tasks;
using static TaskManagerApp.Domain.TaskManagerBase;

namespace TaskManagerApp.Domain
{
    public class TaskManagerParams : TaskManagerDomain
    {
        public int Id { get; set; }
        public new string? Title { get; set; }
        public new TaskManagerState? Status { get; set; }


        public TaskManagerBase ValidateUpdate(string? title, string? description, TaskManagerState? status, DateTime? concludedDate, TaskManagerBase oldTask)
        {
            if (!string.IsNullOrEmpty(title))
            {
                if (title.Length > 100 || title == "string")
                    throw new Exception("Título inválido");

                oldTask.Title = title;
            }

            if (!string.IsNullOrEmpty(description) && description.Length < 300 && description != "string")
                oldTask.Description = description;

            if ((status.HasValue) && status == TaskManagerState.Pending || status == TaskManagerState.OnGoing || status == TaskManagerState.Concluded)
                oldTask.Status = status.Value;

            if (concludedDate.HasValue)
            {
                if (concludedDate <= oldTask.CreationDate)
                    throw new Exception("A data de conclusão deve ser maior que a data de criação.");

                if (status == TaskManagerState.Concluded)
                    oldTask.ConcludedDate = concludedDate;
            }

            if (status == TaskManagerState.Concluded && !concludedDate.HasValue)
                oldTask.ConcludedDate = DateTime.UtcNow;

            if (status != TaskManagerState.Concluded && concludedDate.HasValue)
                oldTask.ConcludedDate = null;

            return oldTask;
        }
    }
}
