using System.Security.Cryptography;
using System.Text;

namespace Application.Common;

public static class CipherService
{
    private static string _chave;
    public static string Chave
    {
        get
        {
            if (string.IsNullOrEmpty(_chave))
            {
                return "000nova-chave-de-32-caracteres00";
            }
            else
            {
                return _chave;
            }
        }
        set
        {
            _chave = value;
        }
    }

    private readonly static byte[] bIV = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

    public static string Criptografar(string input)
    {
        try
        {
            if (!string.IsNullOrEmpty(input))
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Chave);
                byte[] bText = Encoding.UTF8.GetBytes(input);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = bKey;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.IV = bIV;

                using MemoryStream mStream = new();
                using (CryptoStream encryptor = new(mStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    encryptor.Write(bText, 0, bText.Length);
                    encryptor.FlushFinalBlock();
                }
                return Convert.ToBase64String(mStream.ToArray());
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Erro ao criptografar", ex);
        }
    }

    public static string Descriptografar(string valor)
    {
        try
        {
            if (!string.IsNullOrEmpty(valor))
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Chave);
                byte[] bText = Convert.FromBase64String(valor);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = bKey;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.IV = bIV;

                using MemoryStream mStream = new();
                using (CryptoStream decryptor = new(mStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    decryptor.Write(bText, 0, bText.Length);
                    decryptor.FlushFinalBlock();
                }
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Erro ao descriptografar", ex);
        }
    }
}
