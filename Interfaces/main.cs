using ArrayCalculator.Classes;

namespace ArrayCalculator.Interfaces
{
    public interface IStateController
    {
        CalculatorState GetByIndex(int idx);
        CalculatorState GetPrevious();
        CalculatorState GetCurrent();
        void Append(CalculatorState state);
        void Save();
        void Read();
    }
    
    public enum CalculatorAction {
        Add,
        Substract,
        Multiply,
        Divide,
        DoublePow,
        CustomPow,
        DoubleSqrt,
        CustomSqrt,
        Log,
        Factorial,
        Median,
        AverageDeviation
    }
}