using System;
using System.Linq;
using System.Windows.Forms;
using ArrayCalculator.Classes;
using ArrayCalculator.Interfaces;

namespace ArrayCalculator
{
    public partial class Form1 : Form
    {
        private readonly CalculatorController _calculatorController = new CalculatorController();
        private readonly CalculatorAction[] _actionsWithoutUserInput = new CalculatorAction[]
        {
            CalculatorAction.Factorial,
            CalculatorAction.Median,
            CalculatorAction.DoublePow,
            CalculatorAction.DoubleSqrt,
            CalculatorAction.AverageDeviation
        };
        public Form1()
        {
            InitializeComponent();
            UpdateProgress();
        }

        private void SaveValueInput()
        {
            if (Input.Text.Length <= 0) return;
            _calculatorController.CurrentState.Input = Input.Text.Split(',').Select(double.Parse).ToArray();
            Input.Clear();
        }

        private void ActionPressed(CalculatorAction action)
        {
            _calculatorController.CurrentState.Action = action;
            SaveValueInput();
            
            if (action == CalculatorAction.DoublePow || action == CalculatorAction.DoubleSqrt)
            {
                _calculatorController.CurrentState.Value = 2;
            }

            if (_actionsWithoutUserInput.Contains(action))
            {
                Calculate(false);
            }
        }

        private void OnUserInput(int n)
        {
            Input.Text += n;
        }

        private void UpdateProgress()
        {
            var progress = _calculatorController.GetCurrentProgress();
            label4.Text = $"Операция {progress.current} из {progress.max}";

            if (progress.current > 0)
            {
                History.SelectedIndex = progress.current - 1;
            }
            

            button13.Enabled = progress.current > 0;
            button14.Enabled = progress.current > 0;
        }

        private void Calculate(bool readFromInput = true, bool saveHistory = true)
        {
            if (readFromInput)
            {
                _calculatorController.CurrentState.Value = double.Parse(Input.Text);
            }

            Input.Text = _calculatorController.Calculate();
            
            if (saveHistory)
            {
                _calculatorController.AppendHistory();
                History.Items.Add(FormatActionToHistoryItem(_calculatorController.CurrentState.Action));
            }
            RenderHistoryItems();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            OnUserInput(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnUserInput(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OnUserInput(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OnUserInput(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OnUserInput(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OnUserInput(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OnUserInput(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OnUserInput(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OnUserInput(9);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Input.Text += ", ";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OnUserInput(0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SaveValueInput();
            Input.Text =_calculatorController.Repeat();
            UpdateProgress();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var state = _calculatorController.GetPreviousState();
            Input.Text = state.Result;
            UpdateProgress();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Add);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Substract);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Multiply);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Divide);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.DoublePow);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.CustomPow);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Input.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.DoubleSqrt);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.CustomSqrt);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Factorial);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Median);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.AverageDeviation);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            ActionPressed(CalculatorAction.Log);
        }

        private void HistoryOnSelectedIndexChanged(object sender, EventArgs e)
        {
            var state = _calculatorController.GetStateByIndex(History.SelectedIndex);
            Input.Text = state.Result;
            UpdateProgress();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Input.Text += ".";
        }

        private void button25_Click(object sender, EventArgs e)
        {
           _calculatorController.Save();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            _calculatorController.Read();
            RenderHistoryItems();
        }

        private void RenderHistoryItems()
        {
            History.Items.Clear();
            var historyActions = _calculatorController.GetHistoryData();
            foreach (var calculatorAction in historyActions)
            {
                History.Items.Add(FormatActionToHistoryItem(calculatorAction));
            }
            UpdateProgress();
        }
        
        private string FormatActionToHistoryItem(CalculatorAction a)
        {
            switch (a)
            {
                case CalculatorAction.Add:
                    return "Add";
                case CalculatorAction.Substract:
                    return "Substract";
                case CalculatorAction.Multiply:
                    return "Multiply";
                case CalculatorAction.Divide:
                    return "Divide";
                case CalculatorAction.DoublePow:
                    return "DoublePow";
                case CalculatorAction.CustomPow:
                    return "CustomPow";
                case CalculatorAction.DoubleSqrt:
                    return "DoubleSqrt";
                case CalculatorAction.CustomSqrt:
                    return "CustomSqrt";
                case CalculatorAction.AverageDeviation:
                    return "AverageDeviation";
                case CalculatorAction.Factorial:
                    return "Factorial";
                case CalculatorAction.Median:
                    return "Median";
                case CalculatorAction.Log:
                    return "Log";
                default:
                    return "Unknown action";
            }
        }

    }
}