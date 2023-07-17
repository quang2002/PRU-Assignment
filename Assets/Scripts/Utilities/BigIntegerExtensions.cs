namespace Utilities
{
    using System.Numerics;

    public static class BigIntegerExtensions
    {
        public static readonly string[] DeclaredPostfix =
        {
            "K",
            "M",
            "B",
            "T",
            "Qa",
            "Qi",
            "Sx",
            "Sp",
            "Oc",
            "No",
            "Dc",
            "UDc",
            "DDc",
            "TDc",
            "QaDc",
            "QiDc",
            "SxDc",
            "SpDc",
            "OcDc",
            "NoDc",
            "Vg",
            "UVg",
            "DVg",
            "TVg",
            "QaVg",
            "QiVg",
            "SxVg",
            "SpVg",
            "OcVg",
        };

        public static string ToShortString(this BigInteger bigInteger)
        {
            var postfixIndex = -1;

            while (bigInteger > 1000)
            {
                bigInteger /= 1000;
                postfixIndex++;
            }

            return postfixIndex == -1 ? bigInteger.ToString() : $"{bigInteger}{DeclaredPostfix[postfixIndex]}";
        }
    }

}