using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Domain;
using TaskManagerApp.Service;

namespace TaskManagerApp.Controllers
{
    [Route("v1/Task")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly ITaskManagerService _taskManagerService;

        public TaskManagerController(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService;
        }

        [HttpPost("Create", Name = "CreateTask")]
        public async Task<IActionResult> CreateAsync([FromBody] TaskManagerDomain task)
        {
            try
            {
                var result = await _taskManagerService.CreateAsync(task);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return ControllerErrorResponse.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("All", Name = "AllTasks")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _taskManagerService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return ControllerErrorResponse.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("Filter", Name = "FilterTasks")]
        public async Task<IActionResult> FindAsync(int pageNumber, TaskManagerBase.TaskManagerState status)
        {
            try
            {
                var result = await _taskManagerService.FindAsync(pageNumber, status);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return ControllerErrorResponse.CreateExceptionResponse(this, exception);
            }
        }

        [HttpGet("Filter/Id", Name = "FilterByIdTasks")]
        public async Task<IActionResult> FilterTasksByIdAsync(int id)
        {
            try
            {
                var result = await _taskManagerService.FilterTasksByIdAsync(id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return ControllerErrorResponse.CreateExceptionResponse(this, exception);
            }
        }

        [HttpPut("Update", Name = "UpdateTask")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaskManagerParams task)
        {
            try
            {
                var result = await _taskManagerService.UpdateAsync(task);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return ControllerErrorResponse.CreateExceptionResponse(this, exception);
            }
        }

        [HttpDelete("Delete", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTaskAsync(int id)
        {
            try
            {
                var result = await _taskManagerService.DeleteTaskAsync(id);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return ControllerErrorResponse.CreateExceptionResponse(this, exception);
            }
        }
    }
}
