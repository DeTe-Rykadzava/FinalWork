using FinalWork.Core.Base;
using FinalWork.Core.Interfaces;

namespace FinalWork.Core.Calculators;

public abstract class ACalculator : ICalculator
{
    public char SupportedOperation { get; }
    public int RequiredCountOperators { get; }
    
    public ACalculator(char supportedOperation, int requiredCountOperators)
    {
        SupportedOperation = supportedOperation;
        RequiredCountOperators = requiredCountOperators;
    }
    
    public bool CanProcessOperation(string operation)
    {
        return SupportedOperation.Equals(operation);
    }

    public abstract CalculatorResult ProcessOperation(List<dynamic> operators);
}