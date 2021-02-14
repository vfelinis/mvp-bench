using System;

namespace mvp.bench
{
    public class InliningTest
    {
        public void Start1(string text)
        {
            var result = Operation1(text, false);
            Console.WriteLine(result);
        }

        private string Operation1(string text, bool replaceSpace)
        {
            if (text == null) throw new ArgumentException(nameof(text));

            if (replaceSpace)
            {
                text = text.Replace(' ', '-');
            }
            return text.ToLower();
        }

        public void Start2(string text)
        {
            var result = Operation2(text, false);
            Console.WriteLine(result);
        }

        private string Operation2(string text, bool replaceSpace)
        {
            if (text == null) ThrowArgEx(nameof(text));

            if (replaceSpace)
            {
                text = text.Replace(' ', '-');
            }
            return text.ToLower();
        }

        private void ThrowArgEx(string message)
        {
            throw new ArgumentException(message);
        }
    }
}
