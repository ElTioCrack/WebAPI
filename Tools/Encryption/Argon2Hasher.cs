using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace Tools.Encryption
{
	// Clase abstracta para realizar hashing utilizando el algoritmo Argon2
	public abstract class Argon2Hasher
	{
		private const int DefaultHashSize = 32;				// Tamaño predeterminado del hash (en bytes)
		private const int DefaultMemorySize = 65536;		// Tamaño de memoria predeterminado utilizado por Argon2 (en bytes)
		private const int DefaultIterations = 4;            // Número de iteraciones predeterminado para el algoritmo Argon2
		private const int DefaultDegreeOfParallelism = 1;   // Grado de paralelismo predeterminado (cantidad de hilos utilizados)

		// Método para generar un salt (valor aleatorio) para usar en el hashing
		public static byte[] GenerateSalt()
		{
			// Usando RNGCryptoServiceProvider para generar un salt aleatorio
			using (var rng = new RNGCryptoServiceProvider())
			{
				byte[] salt = new byte[DefaultHashSize]; // Tamaño del salt (ajusta según tus necesidades)
				rng.GetBytes(salt);
				return salt;
			}
		}

		// Método para generar un hash a partir de una contraseña
		public static string GenerateHash(string password)
		{
			return GenerateHash(password, null);
		}

		// Método para generar un hash a partir de una contraseña y un salt específico
		public static string GenerateHash(string password, byte[] salt)
		{
			using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
			{
				hasher.Salt = salt;
				hasher.MemorySize = DefaultMemorySize;
				hasher.Iterations = DefaultIterations;
				hasher.DegreeOfParallelism = DefaultDegreeOfParallelism;

				byte[] hashBytes = hasher.GetBytes(DefaultHashSize);
				return Convert.ToBase64String(hashBytes);
			}
		}

		// Método para verificar si una contraseña coincide con un hash almacenado
		public static bool VerifyHash(string password, string storedHash)
		{
			return VerifyHash(password, storedHash, null);
		}

		// Método para verificar si una contraseña coincide con un hash almacenado y un salt específico
		public static bool VerifyHash(string password, string storedHash, byte[] salt)
		{
			try
			{
				using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
				{
					hasher.Salt = salt;
					hasher.MemorySize = DefaultMemorySize;
					hasher.Iterations = DefaultIterations;
					hasher.DegreeOfParallelism = DefaultDegreeOfParallelism;

					byte[] expectedHashBytes = hasher.GetBytes(DefaultHashSize); // Tamaño del hash (ajusta según tus necesidades)
					string expectedHash = Convert.ToBase64String(expectedHashBytes);

					// Compara el hash calculado con el hash almacenado
					return storedHash.Equals(expectedHash);
				}
			}
			catch (Exception)
			{
				// Manejar cualquier excepción que pueda ocurrir durante la verificación
				return false;
			}
		}
	}
}
