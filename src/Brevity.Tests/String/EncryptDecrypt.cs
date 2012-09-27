// ReSharper disable InconsistentNaming

using System.Security.Cryptography;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
	[TestFixture]
	public class EncryptDecrypt
	{
		[Test]
		public void EncryptDecryptText()
		{
			const string message = "secret";
			var asymmetricKey = new RSACryptoServiceProvider();
			var publicKey = asymmetricKey.GetPublicKey();
			var privateKey = asymmetricKey.GetPrivateKey();

			var encrypted = message.Encrypt(publicKey);
			var decrypted = encrypted.Decrypt(privateKey);

			decrypted.ShouldBe(message);
		}
	}
}

// ReSharper restore InconsistentNaming