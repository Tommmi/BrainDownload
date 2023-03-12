using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCorrectVocablaryFile
{
    internal class ParseStateColumnStarted : IParseState
    {
        public void ProcessCharacter(ParseContext context, char c)
        {
            switch (c)
            {
                case '\n':
                    if (context.Column == 2)
                    {
                        context.NewContent.AppendLine();
                        context.Column = 0;
                    }
                    else
                    {
                        context.NewContent.Append(c);
                        context.NewContent.AppendLine("ERROR - 276574");
                    }
                    break;
                case '\r':
                    if (context.Column < 2)
                    {
                        context.NewContent.Append("ERROR - 9037453");
                    }
                    break;
                case ';':
                    context.NewContent.Append(c);
                    context.Column++;
                    if (context.Column > 2)
                    {
                        context.NewContent.AppendLine("ERROR - n56z54");
                    }
                    break;
                case '\"':
                    context.NewContent.Append(c);
                    context.CurrentParseState = new ParseStateQuotationMarkOpened();
                    break;
                default:
                    context.NewContent.Append(c);
                    break;
            }
        }
    }
}
