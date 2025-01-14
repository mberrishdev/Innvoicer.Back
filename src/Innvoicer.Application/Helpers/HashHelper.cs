using System.Security.Cryptography;
using System.Text;

namespace Innvoicer.Application.Helpers;

public static class HashHelper
{
    public static string Hash(string str, string? salt = null)
    {
        if (!string.IsNullOrEmpty(salt))
        {
            str += salt;
        }

        var bytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = MD5.HashData(bytes);

        StringBuilder sb = new();
        foreach (var t in hashBytes)
            sb.Append(t.ToString("X2"));

        return sb.ToString();
    }
    
    public static string HashFNV1a(string str, string? salt = null)
    {
        if (!string.IsNullOrEmpty(salt))
        {
            str += salt;
        }

        const uint fnvPrime = 0x811C9DC5;
        var hash = 0x811C9DC5;

        foreach (var c in str)
        {
            hash ^= c;
            hash *= fnvPrime;
        }

        return hash.ToString("X");
    }
}