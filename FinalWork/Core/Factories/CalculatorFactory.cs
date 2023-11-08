using FinalWork.Core.Calculators;

namespace FinalWork.Core.Factories;

public class CalculatorFactory
{
    public static ACalculator CreateAdditionalCalculator()
    {
        return new AdditionalCalculator('+');
    }

    public static ACalculator CreateSubtractionCalculator()
    {
        return new SubtractionCalculator('-');
    }

    public static ACalculator CreateMultiplyCalculator()
    {
        return new MultiplyCalculator('*');
    }

    public static ACalculator CreateDivisionCalculator()
    {
        return new DivisionCalculator('/');
    }
}