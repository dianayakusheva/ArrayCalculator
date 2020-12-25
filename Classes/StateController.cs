using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ArrayCalculator.Interfaces;

namespace ArrayCalculator.Classes
{
    public class StateController: IStateController
    {
        private List<CalculatorState> _history = new List<CalculatorState>();
        private int _currentIndex;
        
        public CalculatorState GetPrevious()
        {
            if (_currentIndex - 2 < 0) return GetCurrent();
            _currentIndex -= 1;
            return new CalculatorState(_history[_currentIndex - 1]);
        }

        public CalculatorState GetCurrent()
        {
            return new CalculatorState(_history[_currentIndex - 1]);
        }

        public StateControllerProgress GetCurrentProgress()
        {
            return new StateControllerProgress
            {
                max = _history.Count,
                current = _currentIndex
            };
        }
        
        public void Append(CalculatorState state)
        {
            if (_currentIndex != _history.Count)
            {
                _history = _history.GetRange(0, _currentIndex);
            }
            _history.Add(state);
            _currentIndex = _history.Count;
        }

        public CalculatorState GetByIndex(int idx)
        {
            _currentIndex = idx + 1;
            return new CalculatorState(_history[idx]);
        }

        public void Save()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "calc_data.txt";

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileName = saveFileDialog.FileName;
                    
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, _history);
                }
            }
        }

        public void Read()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileStream = openFileDialog.OpenFile();
                var formatter = new BinaryFormatter();
                _history = (List<CalculatorState>)formatter.Deserialize(fileStream);
            }
        }

        public IEnumerable<CalculatorAction> GetHistoryData()
        {
            return _history.Select(GetStateAction).ToArray();
        }

        private CalculatorAction GetStateAction(CalculatorState state) => state.Action;
    }
}