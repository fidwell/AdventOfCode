namespace AdventOfCode.Core.Cryptography;

public class MD5
{
    // MD5 uses 32-bit unsigned data types, or uints in C#.
    // Input:  512 bits
    // Output: 128 bits, or 16 bytes, or 4 uints.

    const int OutputBits = 128;
    const int BitsInAByte = 8;
    const int BytesInAWord = 4;
    const int OutputBytes = OutputBits / BitsInAByte;

    public static byte[] HashData(byte[] source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var paddedInput = PadInput(source);

        var a = A0;
        var b = B0;
        var c = C0;
        var d = D0;

        for (var chunk = 0; chunk < paddedInput.Length / 64; chunk++)
        {
            var chunkData = paddedInput.AsSpan(chunk * 64, 64).ToArray();
            ProcessChunk(chunkData, ref a, ref b, ref c, ref d);
        }

        var result = new byte[OutputBytes];
        BitConverter.GetBytes(a).CopyTo(result, BytesInAWord * 0);
        BitConverter.GetBytes(b).CopyTo(result, BytesInAWord * 1);
        BitConverter.GetBytes(c).CopyTo(result, BytesInAWord * 2);
        BitConverter.GetBytes(d).CopyTo(result, BytesInAWord * 3);
        return result;
    }

    private static byte[] PadInput(byte[] source)
    {
        var originalLength = source.Length * 8; // Length in bits
        var paddingLength = (56 - (source.Length % 64) + 64) % 64; // Padding to 448 mod 512

        var paddedInput = new byte[source.Length + paddingLength + 8];
        Array.Copy(source, paddedInput, source.Length);

        paddedInput[source.Length] = 0x80; // Append a single 1 bit

        // Append original length as a 64-bit little-endian integer
        var lengthBytes = BitConverter.GetBytes(originalLength);
        Array.Copy(lengthBytes, 0, paddedInput, paddedInput.Length - 8, 8);
        return paddedInput;
    }

    private static void ProcessChunk(byte[] chunk, ref uint a, ref uint b, ref uint c, ref uint d)
    {
        var m = new uint[16];
        for (var i = 0; i < 16; i++)
        {
            m[i] = BitConverter.ToUInt32(chunk, i * 4);
        }

        uint aa = a, bb = b, cc = c, dd = d;

        for (var i = 0; i < 64; i++)
        {
            var f = F(bb, cc, dd, i);
            var g = GetInputIndex(i);

            f += aa + K[i] + m[g];
            aa = dd;
            dd = cc;
            cc = bb;
            bb += Rotate(f, R[i]);
        }

        a += aa;
        b += bb;
        c += cc;
        d += dd;
    }

    private static readonly uint A0 = 0x67452301u;
    private static readonly uint B0 = 0xefcdab89u;
    private static readonly uint C0 = 0x98badcfeu;
    private static readonly uint D0 = 0x10325476u;

    private static int GetInputIndex(int i)
    {
        if (i < 16)
            return i;
        if (i < 32)
            return (5 * i + 1) % 16;
        if (i < 48)
            return (3 * i + 5) % 16;
        if (i < 64)
            return (7 * i) % 16;
        throw new ArgumentOutOfRangeException(nameof(i));
    }

    private static uint F(uint b, uint c, uint d, int i)
    {
        if (i < 16)
            return (b & c) | (~b & d);
        if (i < 32)
            return (d & b) | (~d & c);
        if (i < 48)
            return b ^ c ^ d;
        if (i < 64)
            return c ^ (b | ~d);
        throw new ArgumentOutOfRangeException(nameof(i));
    }

    private readonly static uint[] K =
    [
        0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
        0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
        0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
        0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
        0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
        0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
        0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
        0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
        0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
        0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
        0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
        0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
        0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
        0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
        0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
        0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
    ];

    private static uint Rotate(uint input, int amount) =>
        (input << amount) | (input >> (32 - amount));

    private readonly static int[] R =
    [
        7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,
        5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
        4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
        6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21
    ];
}
