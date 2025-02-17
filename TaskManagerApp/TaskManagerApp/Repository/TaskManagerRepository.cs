using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Domain;

namespace TaskManagerApp.Repository
{
    public class TaskManagerRepository : ITaskManagerRepository
    {
        private readonly AppDbContext _context;

        public TaskManagerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskManagerBase> InsertAsync(TaskManagerBase task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return task;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<TaskManagerBase>> GetAllAsync()
        {
            try
            {
                return await _context.Tasks.ToListAsync() ?? new List<TaskManagerBase>();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter as tarefas do banco de dados", ex);
            }
        }

        public async Task<List<TaskManagerBase>> FindByIdAsync(int id)
        {
            try
            {
                var task = await _context.Tasks
                    .Where(t => t.Id == id)
                    .ToListAsync();

                return task;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a tarefa do banco de dados", ex);
            }
        }

        public async Task<List<TaskManagerBase>> FindByStatusAndPaginationAsync(int pageNumber, int pageSize, TaskManagerBase.TaskManagerState status)
        {
            try
            {
                return await _context.Tasks
                                     .Where(t => t.Status == status)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(TaskManagerBase task)
        {
            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar a tarefa", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                    return false;

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar excluir a tarefa", ex);
            }
        }
    }
}
