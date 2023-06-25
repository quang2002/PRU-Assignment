namespace Utilities
{
    using System;

    public static class AbcIntExtensions
    {
        public static string LevelToAbc(ushort level)
        {
            var result = "";

            while (level > 0)
            {
                var remainder = level % 26;
                result =  (char)('A' + remainder) + result;
                level  /= 26;
            }

            return result;
        }

        public static ushort AbcToLevel(string abc)
        {
            ushort result = 0;

            foreach (var c in abc)
            {
                if (c < 'A' || c > 'Z')
                {
                    throw new ArgumentException($"Invalid character '{c}' in '{abc}'");
                }

                result *= 26;
                result += (ushort)(c - 'A');
            }

            return result;
        }

        public static bool TryParseAbcLevel(string abc, out ushort level)
        {
            try
            {
                level = AbcToLevel(abc);
                return true;
            }
            catch (Exception)
            {
                level = default;
                return false;
            }
        }
    }
}