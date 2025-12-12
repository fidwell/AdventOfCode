using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Core.Hashing;

public static class Md5Hasher
{
    public static byte[] Hash(this string input) => MD5.HashData(Encoding.ASCII.GetBytes(input));
    public static byte[] Hash(this byte[] input) => MD5.HashData(input);
    public static string HashToString(this string input) =>
        Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input))).ToLower();
}
