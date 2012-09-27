using System;
using System.Security.Cryptography;

namespace Brevity
{
	/// <summary>
	/// Extension methods to retrieve public/private key.
	/// </summary>
	public static class AsymmetricAlgorithmExtensions
	{
		/// <summary>
		/// Get the public key.
		/// </summary>
		/// <param name="provider"></param>
		/// <returns></returns>
		public static TAsymmetricAlgorithm GetPublicKey<TAsymmetricAlgorithm>(this TAsymmetricAlgorithm provider) where TAsymmetricAlgorithm : AsymmetricAlgorithm
		{
			return GetKey<TAsymmetricAlgorithm>(provider, false);
		}

		/// <summary>
		/// Get the private key.
		/// </summary>
		/// <param name="provider"></param>
		/// <returns></returns>
		public static TAsymmetricAlgorithm GetPrivateKey<TAsymmetricAlgorithm>(this TAsymmetricAlgorithm provider) where TAsymmetricAlgorithm : AsymmetricAlgorithm
		{
			return GetKey<TAsymmetricAlgorithm>(provider, true);
		}

		private static TAsymmetricAlgorithm GetKey<TAsymmetricAlgorithm>(AsymmetricAlgorithm provider, bool includePrivateKey) where TAsymmetricAlgorithm : AsymmetricAlgorithm
		{
			var xml = provider.ToXmlString(includePrivateKey);
			var algorithm = provider.ToString();
			var result = AsymmetricAlgorithm.Create(algorithm) as TAsymmetricAlgorithm;
			if(result == null)
				throw new ArgumentException("Failed to create AsymmetricAlgorithm '{0}' (type {1}).".FormatWith(algorithm, provider.GetType().Name), "provider");
			result.FromXmlString(xml);
			return result;
		}
	}
}