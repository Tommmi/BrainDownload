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
                        context.Guid = string.Empty;
                    }
                    else
                    {
                        context.NewContent.Append(c);
                        context.NewContent.AppendLine("ERROR - 276574");
                        throw new Exception("line feed at wrong column");
                    }
                    break;
                case '\r':
                    if (context.Column < 2)
                    {
                        context.NewContent.Append("ERROR - 9037453");
                        throw new Exception("line feed at wrong column");
                    }
                    break;
                case ';':
                    if(context.Column==0)
                    {
                        if(context.Guid!="ID"
                           && !Guid.TryParse(context.Guid, out var guid))
                        {
                            context.NewContent.AppendLine("ERROR - 8793fj834");
                            throw new Exception("missing guid");
                        }
                    }
                    context.NewContent.Append(c);
                    context.Column++;
                    if (context.Column > 2)
                    {
                        context.NewContent.AppendLine("ERROR - n56z54");
                        throw new Exception("too much columns");
                    }
                    break;
                case '\"':
                    context.NewContent.Append(c);
                    context.CurrentParseState = new ParseStateQuotationMarkOpened();
                    break;
                default:
                    context.NewContent.Append(c);
                    if(context.Column == 0)
                    {
                        context.Guid += c;
                    }
                    break;
            }
        }
    }
}
