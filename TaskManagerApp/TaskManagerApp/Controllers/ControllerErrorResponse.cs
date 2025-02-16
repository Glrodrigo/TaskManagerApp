using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Domain;

namespace TaskManagerApp.Controllers
{
    public static class ControllerErrorResponse
    {
        public static IActionResult CreateExceptionResponse(this ControllerBase controller, Exception error)
        {
            var errors = new List<ErrorDomain>
            {
                new ErrorDomain("00001", error.Message)
            };

            if (error.Message == "Titulo inválido" || error.Message == "Descrição inválida" || error.Message == "Status inválido" ||
                error.Message == "Data de conclusão menor que a data de criação" || error.Message == "Identificação inválida" ||
                error.Message == "Pesquisa inválida, selecione um status")
            {
                errors[0].ErrorCode = controller.StatusCode(400).StatusCode.ToString();
                return controller.StatusCode(400, new { messages = errors });
            }

            if (error.Message == "Ocorreu um erro ao tentar salvar no banco de dados" || error.Message == "Ocorreu um erro ao tentar obter as tarefas do banco de dados" ||
                error.Message == "Ocorreu um erro ao tentar obter a tarefa do banco de dados" || error.Message == "Ocorreu um erro ao tentar obter filtro do banco de dados" ||
                error.Message == "Erro ao tentar atualizar a tarefa" || error.Message == "Ocorreu um erro ao tentar excluir a tarefa")
            {
                errors[0].ErrorCode = controller.StatusCode(422).StatusCode.ToString();
                return controller.StatusCode(422, new { messages = errors });
            }

            errors[0].ErrorCode = controller.StatusCode(500).StatusCode.ToString();
            return controller.StatusCode(500, new { messages = errors });
        }
    }
}
