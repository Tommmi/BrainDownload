namespace AutoCorrectVocablaryFile;

internal class ParseStateQuotationMarkOpened : IParseState
{
    int _quotationMarksCnt = 0;

    public void ProcessCharacter(ParseContext context, char c)
    {
        if (_quotationMarksCnt == 0)
        {
            switch (c)
            {
                case '\r':
                    break;
                case '\"':
                    context.NewContent.Append(c);
                    _quotationMarksCnt++;
                    break;
                default:
                    context.NewContent.Append(c);
                    break;
            }
        }
        else
        {
            switch (c)
            {
                case '\"':
                    context.NewContent.Append(c);
                    _quotationMarksCnt = 0;
                    break;
                default:
                    context.CurrentParseState = new ParseStateColumnStarted();
                    context.CurrentParseState.ProcessCharacter(context, c);
                    break;
            }

        }
    }
}