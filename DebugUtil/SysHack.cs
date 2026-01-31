using System.Text;

namespace System
{
    public static class Fmt
    {
        public static string Format(string stmt, params object[] inputs)
        {
            if (stmt.Length == 0 || inputs.Length == 0)
            {
                return stmt;
            }

            return FormatImpl(stmt, inputs);
        }

        private static string FormatImpl(string stmt, object[] injects)
        {
            StringBuilder builder = new StringBuilder();
            Peekable<char> str = new Peekable<char>(stmt);

            bool isEnded = false;
            int argIndex = 0;

            do
            {
                char c = str.MoveNext(out isEnded);

                if (c == '{' && str.Peek() == '}')
                {
                    str.MoveNext();
                    builder.Append(injects[argIndex++]);
                }
                else
                {
                    builder.Append(c);
                }
            }
            while (isEnded);

            return builder.ToString();
        }
    }
}
