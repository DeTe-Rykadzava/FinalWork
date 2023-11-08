using System.Numerics;
using FinalWork.Core.Base;
using FinalWork.Core.Interfaces;

namespace FinalWork.Core.Calculators;

public class MultiplyCalculator : ACalculator
{
    public MultiplyCalculator(char supportedOperation) : base(supportedOperation, 2) { }

    public override CalculatorResult ProcessOperation(List<dynamic> operators)
    {
        if (operators.Count() < RequiredCountOperators)
            throw new Exception("For process this operation need more operators");
        if (operators.Any(x => x is not double and not int))
            throw new Exception("Wrong type of operators");
        var result = operators[0] * operators[1];
        return new CalculatorResult(result, $"{operators[0]} * {operators[1]} = {result}");
    }
}