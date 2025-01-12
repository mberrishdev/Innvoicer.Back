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
}