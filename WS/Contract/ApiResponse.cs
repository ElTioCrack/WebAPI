namespace WS.Contract
{
	public class ApiResponse
	{
		public string StatusCode { get; set; }  // Código de estado HTTP
		public string Message { get; set; }     // Mensaje descriptivo
		public string Data { get; set; }        // Datos adicionales (opcional)

		public ApiResponse(string statusCode, string message, string data = null)
		{
			StatusCode = statusCode;
			Message = message;
			Data = data;
		}
	}
}
