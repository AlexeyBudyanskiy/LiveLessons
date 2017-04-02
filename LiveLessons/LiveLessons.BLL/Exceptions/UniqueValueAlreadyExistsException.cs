namespace LiveLessons.BLL.Exceptions
{
    public class UniqueValueAlreadyExistsException : EntityException
    {
        public UniqueValueAlreadyExistsException(string message, string entity) : base(message, entity)
        {
        }
    }
}
