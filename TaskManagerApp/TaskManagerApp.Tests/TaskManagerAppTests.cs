using Moq;
using TaskManagerApp.Domain;
using TaskManagerApp.Repository;
using TaskManagerApp.Service;

namespace TaskManagerApp.Tests
{
    public class TaskManagerAppTests
    {
        private readonly ITaskManagerService _service;
        private readonly Mock<ITaskManagerRepository> _taskRepositoryMock;

        public TaskManagerAppTests()
        {
            _taskRepositoryMock = new Mock<ITaskManagerRepository>();

            _service = new TaskManagerService(_taskRepositoryMock.Object);
        }

        [Fact(DisplayName = "Passar os dados válidos para a criação de uma tarefa")]
        public async Task CreateAsync_ShouldReturnTrue_WhenTaskIsValid()
        {
            // Arrange
            var task = new TaskManagerDomain
            {
                Title = "Nova Tarefa",
                Description = "Descrição da tarefa",
                Status = TaskManagerBase.TaskManagerState.Pending,
                ConcludedDate = DateTime.UtcNow.AddDays(1)
            };

            _taskRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<TaskManagerBase>()))
                .ReturnsAsync(new TaskManagerBase(task.Title, task.Description, task.Status, task.ConcludedDate));

            // Act
            var result = await _service.CreateAsync(task);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<TaskManagerBase>()), Times.Once);
        }

        [Fact(DisplayName = "Listar todas as tarefas")]
        public async Task GetAsync_ShouldReturnListOfTasks()
        {
            // Arrange
            var tasks = new List<TaskManagerBase>
            {
                new TaskManagerBase("Tarefa 1", "Descrição 1", TaskManagerBase.TaskManagerState.Pending, DateTime.UtcNow.AddDays(2)),
                new TaskManagerBase("Tarefa 2", "Descrição 2", TaskManagerBase.TaskManagerState.OnGoing, DateTime.UtcNow.AddDays(5))
            };

            _taskRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(tasks);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Tarefa 1", result[0].Title);
            Assert.Equal("Tarefa 2", result[1].Title);
            _taskRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact(DisplayName = "Listar todas as tarefas para determinado tipo de status")]
        public async Task FindAsync_ShouldReturnFilteredTasks()
        {
            // Arrange
            int pageNumber = 1;
            TaskManagerBase.TaskManagerState status = TaskManagerBase.TaskManagerState.Pending;

            var filteredTasks = new List<TaskManagerBase>
            {
                new TaskManagerBase("Tarefa 1", "Descrição 1", status, DateTime.UtcNow.AddDays(2)),
                new TaskManagerBase("Tarefa 2", "Descrição 2", status, DateTime.UtcNow.AddDays(5))
            };

            _taskRepositoryMock.Setup(repo => repo.FindByStatusAndPaginationAsync(pageNumber, 20, status))
                .ReturnsAsync(filteredTasks);

            // Act
            var result = await _service.FindAsync(pageNumber, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, task => Assert.Equal(status, task.Status));
            _taskRepositoryMock.Verify(repo => repo.FindByStatusAndPaginationAsync(pageNumber, 20, status), Times.Once);
        }

        [Fact(DisplayName = "Buscar uma tarefa com base em seu número de id")]
        public async Task FilterTasksByIdAsync_ShouldReturnTask_WhenIdIsValid()
        {
            // Arrange
            int taskId = 1;
            var expectedTask = new TaskManagerBase("Tarefa Teste", "Descrição Teste", TaskManagerBase.TaskManagerState.OnGoing, DateTime.UtcNow.AddDays(3));
            typeof(TaskManagerBase).GetProperty("Id")?.SetValue(expectedTask, 1);

            _taskRepositoryMock.Setup(repo => repo.FindByIdAsync(taskId))
                .ReturnsAsync(new List<TaskManagerBase> { expectedTask });

            // Act
            var result = await _service.FilterTasksByIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(taskId, result.First().Id);
            _taskRepositoryMock.Verify(repo => repo.FindByIdAsync(taskId), Times.Once);
        }

        [Fact(DisplayName = "Atualizar uma tarefa existente")]
        public async Task UpdateAsync_ShouldReturnTrue_WhenTaskExists()
        {
            // Arrange
            var taskId = 1;
            var existingTask = new TaskManagerBase("Tarefa Antiga", "Descrição Antiga", TaskManagerBase.TaskManagerState.Pending, null);
            var updatedTaskParams = new TaskManagerParams
            {
                Id = taskId,
                Title = "Nova Tarefa",
                Description = "Nova Descrição",
                Status = TaskManagerBase.TaskManagerState.OnGoing,
                ConcludedDate = DateTime.UtcNow.AddDays(5)
            };

            _taskRepositoryMock.Setup(repo => repo.FindByIdAsync(taskId))
                .ReturnsAsync(new List<TaskManagerBase> { existingTask });

            _taskRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<TaskManagerBase>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(updatedTaskParams);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.FindByIdAsync(taskId), Times.Once);
            _taskRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<TaskManagerBase>()), Times.Once);
        }

        [Fact(DisplayName = "Deletar uma tarefa existente")]
        public async Task DeleteTaskAsync_ShouldReturnTrue_WhenTaskExists()
        {
            // Arrange
            int taskId = 1;

            _taskRepositoryMock.Setup(repo => repo.DeleteAsync(taskId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteTaskAsync(taskId);

            // Assert
            Assert.True(result);
            _taskRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
        }
    }
}