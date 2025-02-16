
namespace TaskManagerApp.Domain
{
    public class TaskManagerFilter
    {
        public int PageNumber { get; set; }
        public TaskManagerBase.TaskManagerState? Status { get; set; }

        public TaskManagerFilter(int pageNumber, TaskManagerBase.TaskManagerState status) 
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (status == null || status == 0)
                throw new Exception("Pesquisa inválida, selecione um status");

            PageNumber = pageNumber;
            Status = status;
        }
    }
}
