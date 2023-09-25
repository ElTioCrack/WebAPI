namespace WS.Contract
{
	public class Response
	{
		public string ErrorCode { get; set; }
		public string Message { get; set; }
		public string Details { get; set; }

		public Response(string errorCode, string message, string details = null)
		{
			ErrorCode = errorCode;
			Message = message;
			Details = details;
		}
	}
}
