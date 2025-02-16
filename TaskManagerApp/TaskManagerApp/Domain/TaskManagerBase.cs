namespace TaskManagerApp.Domain
{
    public class TaskManagerBase : TaskManagerDomain
    {
        public int Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public enum TaskManagerState
        {
            Pending = 1,
            OnGoing = 2,
            Concluded = 3
        }

        public TaskManagerBase(string title, string description, TaskManagerState status, DateTime? concludedDate)
        {
            if (string.IsNullOrEmpty(title) || title.Length > 100 || title == "string") 
                throw new Exception("Titulo inválido");

            if (!string.IsNullOrEmpty(description) && description == "string")
                throw new Exception("Descrição inválida");

            if (status != TaskManagerState.Pending && status != TaskManagerState.OnGoing && status != TaskManagerState.Concluded)
                status = TaskManagerState.Pending;

            if (concludedDate.HasValue && concludedDate <= CreationDate) 
                throw new Exception("Data de conclusão menor que a data de criação");

            CreationDate = DateTime.UtcNow;
            Title = title;
            Description = description;
            Status = status;
            ConcludedDate = concludedDate;
        }
    }
}
