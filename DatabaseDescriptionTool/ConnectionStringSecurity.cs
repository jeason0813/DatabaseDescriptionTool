/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of Database Description Tool

	Database Description Tool is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Database Description Tool is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Database Description Tool. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class ConnectionStringSecurity
{
	private const string _saltKey = "C38453C497144BC5";
	private const string _cipherKey = "F20009B3-5D07-4E07-B310-21C7D255FF1B";

	public static string Encode(string connectionString, string keyName)
	{
		if (connectionString == "")
		{
			return "";
		}

		SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder(connectionString);

		string regKeyName = string.Format("Password_{0}", keyName);

		if (!connBuilder.IntegratedSecurity)
		{
			string clearTextPassword = connBuilder.Password;
			string ecryptedPassword = AesEncrypt(clearTextPassword, _cipherKey);
			byte[] protectedPassword = ProtectedData.Protect(Encoding.UTF8.GetBytes(ecryptedPassword), null, DataProtectionScope.CurrentUser);
			RegistryHandler.SaveByte(regKeyName, protectedPassword);
			connBuilder.Password = "";
		}
		else
		{
			RegistryHandler.Delete(regKeyName);
		}

		return connBuilder.ToString();
	}

	public static string Decode(string connectionString, string keyName)
	{
		if (connectionString == "")
		{
			return "";
		}

		SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder(connectionString);

		string regKeyName = string.Format("Password_{0}", keyName);

		if (!connBuilder.IntegratedSecurity)
		{
			byte[] protectedPassword = RegistryHandler.ReadByte(regKeyName);

			if (protectedPassword != null)
			{
				string unprotectedPassword = Encoding.UTF8.GetString(ProtectedData.Unprotect(protectedPassword, null, DataProtectionScope.CurrentUser));
				string clearTextPassword = AesDecrypt(unprotectedPassword, _cipherKey);
				connBuilder.Password = clearTextPassword;
			}
		}

		return connBuilder.ToString();
	}

	private static string AesEncrypt(string plainText, string password)
	{
		if (plainText == "")
		{
			return "";
		}

		byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

		RijndaelManaged symmetricKey = new RijndaelManaged();
		symmetricKey.Mode = CipherMode.CBC;

		byte[] salt = Encoding.ASCII.GetBytes(_saltKey);

		Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
		byte[] keyBytes = key.GetBytes(symmetricKey.KeySize / 8);

		ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, salt);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

		cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
		cryptoStream.FlushFinalBlock();
		byte[] cipherTextBytes = memoryStream.ToArray();

		memoryStream.Close();
		cryptoStream.Close();

		return Convert.ToBase64String(cipherTextBytes);
	}

	private static string AesDecrypt(string cipherText, string password)
	{
		if (cipherText == "")
		{
			return "";
		}

		byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

		RijndaelManaged symmetricKey = new RijndaelManaged();
		symmetricKey.Mode = CipherMode.CBC;

		byte[] salt = Encoding.ASCII.GetBytes(_saltKey);

		Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
		byte[] keyBytes = key.GetBytes(symmetricKey.KeySize / 8);

		ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, salt);
		MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
		CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

		byte[] plainTextBytes = new byte[cipherTextBytes.Length];
		int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

		memoryStream.Close();
		cryptoStream.Close();

		return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
	}
}
