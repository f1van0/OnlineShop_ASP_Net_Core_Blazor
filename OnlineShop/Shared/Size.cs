using System;

namespace OnlineShop.Shared
{
    public class Size
    {
        private int _x;
        private int _y;

        public Size()
        {
            _x = 0;
            _y = 0;
        }

        public Size(string size)
        {
            string parsedNumeric = "";
            foreach (char ch in size)
            {
                if (ch == 'x')
                {
                    _x = Convert.ToInt32(parsedNumeric);
                    parsedNumeric = "";
                    continue;
                }

                parsedNumeric += ch;
            }
            _y = Convert.ToInt32(parsedNumeric);
        }

        public Size(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return _x + "x" + _y;
        }
    }
}