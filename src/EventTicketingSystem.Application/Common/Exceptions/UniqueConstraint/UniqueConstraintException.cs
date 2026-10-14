namespace EventTicketingSystem.Application.Common.Exceptions.UniqueConstraint
{
    public class UniqueConstraintException : Exception
    {
        public UniqueConstraintException(UniqueConstraintType constraintType, Exception innerException) : base($"Unique constraint '{constraintType}' violated.", innerException)
        {
            ConstraintType = constraintType;
        }

        public UniqueConstraintType ConstraintType { get; }
    }
}
