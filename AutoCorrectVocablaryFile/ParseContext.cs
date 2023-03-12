using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCorrectVocablaryFile
{
    internal class ParseContext
    {
        public StringBuilder NewContent { get; } = new StringBuilder();
        public int Column { get; set; } = 0;
        public IParseState CurrentParseState { get; set; }
    }
}
