using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Brevity
{
	/// <summary>
	/// Extension methods for byte[].
	/// </summary>
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// Converts the given binary data to base64.
		/// </summary>
		/// <param name="binary"></param>
		/// <returns></returns>
		public static string ToBase64(this byte[] binary)
		{
			if (binary == null)
				return null;

			return Convert.ToBase64String(binary);
		}

		/// <summary>
		/// Encrypt binary.
		/// </summary>
		/// <param name="binary">The data to encrypt.</param>
		/// <param name="publicKey">The public key.</param>
		/// <param name="symmetricAlgorithmName">Optional. The name of the symmetric algorithm to use. Defaults to "Rijndael" (128 bits AES). See http://msdn.microsoft.com/en-us/library/k74a682y(v=vs.100).aspx for a list of valid values.</param>
		/// <returns></returns>
		public static byte[] Encrypt(this byte[] binary, RSA publicKey, string symmetricAlgorithmName = "Rijndael")
		{
			if (binary == null) throw new ArgumentNullException("binary");
			if (publicKey == null) throw new ArgumentNullException("publicKey");

			//create sym key of given type
			var symmetricKey = SymmetricAlgorithm.Create(symmetricAlgorithmName);

			if (symmetricKey == null)
				throw new ArgumentException("Unsupported symmetricAlgorithmName: '{0}'".FormatWith(symmetricAlgorithmName), "symmetricAlgorithmName");

			//encrypt input using the sym key
			var encryptedBinary = symmetricKey.CreateEncryptor().TransformFinalBlock(binary, 0, binary.Length);

			//encrypt sym key using asym key
			var encryptedSymmetricKey = new RSAOAEPKeyExchangeFormatter(publicKey).CreateKeyExchange(symmetricKey.Key);

			//concat encrypted sym key, IV (public) and encrypted binary
			return Concatenate(encryptedSymmetricKey, symmetricKey.IV, encryptedBinary);
		}

		/// <summary>
		/// Decrypt binary.
		/// </summary>
		/// <param name="binary">The data to encrypt.</param>
		/// <param name="privateKey">The private key.</param>
		/// <param name="symmetricAlgorithmName">Optional. The name of the symmetric algorithm to use. Defaults to "Rijndael" (128 bits AES). See http://msdn.microsoft.com/en-us/library/k74a682y(v=vs.100).aspx for a list of valid values.</param>
		/// <returns></returns>
		public static byte[] Decrypt(this byte[] binary, RSA privateKey, string symmetricAlgorithmName = "Rijndael")
		{
			if (binary == null) throw new ArgumentNullException("binary");
			if (privateKey == null) throw new ArgumentNullException("privateKey");

			//create sym key of given type
			var symmetricKey = SymmetricAlgorithm.Create(symmetricAlgorithmName);

			if(symmetricKey == null)
				throw new ArgumentException("Unsupported symmetricAlgorithmName: '{0}'".FormatWith(symmetricAlgorithmName), "symmetricAlgorithmName");

			//retrieve encrypted sym key
			var encryptedSymmetricKey = new byte[privateKey.KeySize >> 3];
			Buffer.BlockCopy(binary, 0, encryptedSymmetricKey, 0, encryptedSymmetricKey.Length);

			//decrypt sym key using asym key
			var key = new RSAOAEPKeyExchangeDeformatter(privateKey).DecryptKeyExchange(encryptedSymmetricKey);

			//get IV (public)
			var iv = new byte[symmetricKey.IV.Length];
			Buffer.BlockCopy(binary, encryptedSymmetricKey.Length, iv, 0, iv.Length);

			//decrypt binary using sym key and IV
			return symmetricKey.CreateDecryptor(key, iv).TransformFinalBlock(binary, encryptedSymmetricKey.Length + iv.Length, binary.Length - (encryptedSymmetricKey.Length + iv.Length));
		}

		private static byte[] Concatenate(params byte[][] data)
		{
			var length = data.Sum(d => d.Length);

			var result = new byte[length];

			var memoryStream = new MemoryStream(result);
			var binaryWriter = new BinaryWriter(memoryStream);

			foreach (var d in data)
				binaryWriter.Write(d);

			return result;
		}
	}
}