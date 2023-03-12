namespace AutoCorrectVocablaryFile;

internal class ParseStateQuotationMarkOpened : IParseState
{
    int _quotationMarksCnt = 0;

    public void ProcessCharacter(ParseContext context, char c)
    {
        switch (c)
        {
            case '\n':
                context.NewContent.Append(c);
                break;
            case '\r':
                break;
            case ';':
                context.NewContent.Append(c);
                break;
            case '\"':
                if (_quotationMarksCnt == 0)
                {
                    _quotationMarksCnt++;
                }
                else
                {
                    context.NewContent.Append(c);
                    _quotationMarksCnt = 0;
                }
                break;
            default:
                if (_quotationMarksCnt == 0)
                {
                    context.NewContent.Append(c);
                }
                else
                {
                    context.CurrentParseState = new ParseStateColumnStarted();
                }

                break;
        }
    }
}