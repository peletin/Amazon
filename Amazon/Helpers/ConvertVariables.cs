using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.Helpers
{
    abstract class ConvertVariables
    {
        public abstract decimal StringToInt(string text);

        public string IntToString(int convert)
        {
            return convert.ToString();
        }
    }
}
