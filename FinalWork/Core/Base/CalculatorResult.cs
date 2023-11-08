namespace FinalWork.Core.Base;

public class CalculatorResult
{
    public dynamic Result { get; }
    public string StringResult { get; }

    public CalculatorResult(dynamic result, string stringResult)
    {
        Result = result;
        StringResult = stringResult;
    }
}