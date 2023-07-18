namespace Utilities
{
    public static class BigNumberExtensions
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

        public static string ToShortString(this long number)
        {
            var postfixIndex = -1;

            while (number > 1000)
            {
                number /= 1000;
                postfixIndex++;
            }

            return postfixIndex == -1 ? number.ToString() : $"{number}{DeclaredPostfix[postfixIndex]}";
        }
    }
}