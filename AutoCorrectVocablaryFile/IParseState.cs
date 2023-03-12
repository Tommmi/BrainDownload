using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCorrectVocablaryFile
{
    internal interface IParseState
    {
        void ProcessCharacter(ParseContext context, char c);
    }
}
