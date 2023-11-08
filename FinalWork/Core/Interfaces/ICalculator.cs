using FinalWork.Core.Base;

namespace FinalWork.Core.Interfaces;

public interface ICalculator
{
    public char SupportedOperation { get; }
    public int RequiredCountOperators { get; }
    public bool CanProcessOperation(string operation);

    public CalculatorResult ProcessOperation(List<dynamic> operators);
}