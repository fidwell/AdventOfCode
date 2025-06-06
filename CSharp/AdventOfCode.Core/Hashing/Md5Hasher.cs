using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Core.Hashing;

public static class Md5Hasher
{
    public static byte[] Hash(this string input) => MD5.HashData(Encoding.ASCII.GetBytes(input));
}
