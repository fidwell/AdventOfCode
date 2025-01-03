using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoCMD5 = AdventOfCode.Core.Cryptography.MD5;
using NetMD5 = System.Security.Cryptography.MD5;

namespace AdventOfCode.Tests.UtilityTests
{
    [TestClass]
    public class CryptographyTests
    {
        [DataTestMethod]
        [DataRow("")]
        [DataRow("hello world")]
        [DataRow("rareskills")]
        [DataRow("3fNwi87YaCiA.*2Hy7qD")]
        public void CryptographyTest(string value)
        {
            var valueBytes = value.ToCharArray().Select(c => (byte)c).ToArray();
            var expected = NetMD5.HashData(valueBytes);
            var actual = AoCMD5.HashData(valueBytes);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
