using System.Text;
using FinalWork.Core.Calculators;
using FinalWork.Core.Factories;

namespace FinalWork.Core.Base;

public class CalculatorManager
{
    private List<ACalculator> _calculators = new()
    {
        CalculatorFactory.CreateAdditionalCalculator(),
        CalculatorFactory.CreateSubtractionCalculator(),
        CalculatorFactory.CreateDivisionCalculator(),
        CalculatorFactory.CreateMultiplyCalculator(),
    };

    public string GetAllAbleCalcOperationsInString()
    {
        return string.Join(" ", _calculators.Select(s => s.SupportedOperation).ToList());
    }

    public List<char> GetAllAbleCalcOperations()
    {
        return _calculators.Select(s => s.SupportedOperation).ToList();
    }

    public bool CanProcessOperation(char operation)
    {
        if (_calculators.Any(x => x.SupportedOperation == operation))
            return true;
        return false;
    }

    public CalculatorResult ProcessOperation(char? operation, List<double> @params)
    {
        if (_calculators.FirstOrDefault(x => x.SupportedOperation == operation) == null)
            throw new Exception("Cannot process current operation");

        var calc = _calculators.FirstOrDefault(x => x.SupportedOperation == operation)!;

        var parameters = new List<dynamic>();
        for (var i = 0; i < calc.RequiredCountOperators; i++)
        {
            parameters.Add(@params[i]);
        }

        try
        {
            var result = calc.ProcessOperation(parameters);
            return result;
        }
        catch (Exception e)
        {
            using (var sw = new StreamWriter("ErrorLog.txt", true, Encoding.UTF8))
            {
                sw.WriteLine($"[Error]\t{e.Message}");
            }

            return new CalculatorResult(0, e.Message);
        }
    }
}