namespace BusinessLogic.Responses
{
    public class Response<T> where T : class
    {
        public T Data { get; protected set; }
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        private Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        private Response(bool success, string message, T data) : this(success, message)
        {
            Data = data;
        }

        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="data">Newly created item</param>
        public Response(T data) : this(true, string.Empty, data)
        {

        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message">Error message</param>
        public Response(string message) : this(false, message, null)
        {

        }
    }
}