using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Status
{
    public class Status : Entity
    {
        private Status(int key, string value)
        {
            Key = key;
            Value = value;
        }

       public int Key { get; private set; }
       public string Value { get; private set; }

        public static Result<Status> Create(int key, string value)
        {
            if (value == null)
            {
                return Result.Failure<Status>(Error.NullValue);
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<Status>(Error.Empty);
            }

            var status = new Status(key, value);
            return status;

        }

    }
}
