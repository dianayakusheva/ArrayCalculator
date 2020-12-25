using System;
using ArrayCalculator.Interfaces;

namespace ArrayCalculator.Classes
{
    [Serializable]
    public class CalculatorState
    {
        public double[] Input;
        public string Result;
        public CalculatorAction Action;
        public double Value;

        public CalculatorState(CalculatorState state = null)
        {
            if (state == null) return;
            Input = state.Input;
            Action = state.Action;
            Value = state.Value;
            Result = state.Result;

        }
    }
}