using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ArrayCalculator.Interfaces;

namespace ArrayCalculator.Classes
{
    public class CalculatorController
    {
        public CalculatorState CurrentState = new CalculatorState();
        private readonly StateController _stateController = new StateController();


        public void AppendHistory()
        {
         _stateController.Append(CurrentState);
         CurrentState = new CalculatorState();
        }
        
        public string Repeat()
        {
            var state = _stateController.GetCurrent();
            CurrentState.Value = state.Value;
            CurrentState.Action = state.Action;
            return Calculate();
        }

        public CalculatorState GetPreviousState()
        {
            return _stateController.GetPrevious();
        }

        public CalculatorState GetStateByIndex(int index)
        {
            return _stateController.GetByIndex(index);
        }

        public void Save()
        {
            _stateController.Save();
        }

        public void Read()
        {
            _stateController.Read();
        }

        public IEnumerable<CalculatorAction> GetHistoryData()
        {
            return _stateController.GetHistoryData();
        }


        public StateControllerProgress GetCurrentProgress()
        {
            return _stateController.GetCurrentProgress();
        }

        public string Calculate()
        {
            switch (CurrentState.Action)
            {
                case CalculatorAction.Add:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => n + CurrentState.Value));
                    break;
                
                case CalculatorAction.Substract:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => n - CurrentState.Value));
                    break;
                
                case CalculatorAction.Multiply:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => n * CurrentState.Value));
                    break;
                
                case CalculatorAction.Divide:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => n / CurrentState.Value));
                    break;
                
                case CalculatorAction.DoublePow:
                case CalculatorAction.CustomPow:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => Math.Pow(n, CurrentState.Value)));
                    break;
                
                case CalculatorAction.DoubleSqrt:
                case CalculatorAction.CustomSqrt:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => Math.Pow(n, 1 / CurrentState.Value)));
                    break;
                
                case CalculatorAction.Factorial:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(CalculateFactorial).ToArray());
                    break;
                
                case CalculatorAction.Log:
                    CurrentState.Result = string.Join(", ",CurrentState.Input.Select(n => Math.Log(n, CurrentState.Value)));
                    break;
                
                case CalculatorAction.AverageDeviation:
                    CurrentState.Result = CalculateAverageDeviation().ToString();
                    break;
                
                case CalculatorAction.Median:
                    CurrentState.Result = CalculateAverageValue().ToString();
                    break;
                
                default:
                    return "Unsupported operation";
            }

            return CurrentState.Result;
        }

        private double CalculateAverageValue()
        {
            var sum = CurrentState.Input.Sum();
            return sum / CurrentState.Input.Length;
        }

        private double CalculateAverageDeviation()
        {
            var avgValue = CalculateAverageValue();
            var prepared = CurrentState.Input.Select(d => Math.Pow(d - avgValue, 2));
            return prepared.Sum() / (CurrentState.Input.Length - 1);
        }

        private double CalculateFactorial(double pow = 1)
        {
            pow++;
            var value = new BigInteger(1);
            for (var i = 2; i < pow; i++)
            {
                value *= i;
            }

            return (double) value;
        }

    }

}