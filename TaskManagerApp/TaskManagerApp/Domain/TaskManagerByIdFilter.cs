namespace TaskManagerApp.Domain
{
    public static class TaskManagerByIdFilter
    {
        public static int ValidateId(int id)
        {
            if (id <= 0)
                throw new Exception("Identificação inválida");

            return id;
        }
    }
}
