// ReSharper disable InconsistentNaming

using System.Security.Cryptography;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.ByteArray
{
	[TestFixture]
	public class EncryptDecrypt
	{
		[Test]
		public void EncryptDecryptBinary()
		{
			var message = "Brevity.Tests.Data.Secret.txt".OpenEmbeddedResource().ToBinary();
			var asymmetricKey = new RSACryptoServiceProvider(); 
			var publicKey = asymmetricKey.GetPublicKey();
			var privateKey = asymmetricKey.GetPrivateKey();

			var encrypted = message.Encrypt(publicKey);
			var decrypted = encrypted.Decrypt(privateKey);

			decrypted.ShouldBe(message);
		}

		[Test]
		public void Specify_symmetric_key_algorithm()
		{
			var message = "Brevity.Tests.Data.Secret.txt".OpenEmbeddedResource().ToBinary();
			var rsa = new RSACryptoServiceProvider(); 
			var publicKey = rsa.GetPublicKey();
			var privateKey = rsa.GetPrivateKey();

			var aesEncrypted = message.Encrypt(publicKey, "AES"); //for valid values, see http://msdn.microsoft.com/en-us/library/k74a682y(v=vs.100).aspx.
			var aesDecrypted = aesEncrypted.Decrypt(privateKey, "AES");

			var rijnadelEncrypted = message.Encrypt(publicKey, "Rijndael");
			var rijnadelDecrypted = aesEncrypted.Decrypt(privateKey, "Rijndael");

			aesDecrypted.ShouldBe(message);
			rijnadelDecrypted.ShouldBe(message);
			aesEncrypted.ShouldNotBe(rijnadelEncrypted);
		}
	}
}

// ReSharper restore InconsistentNaming