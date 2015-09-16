using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Katas.StringCaclulatorApp.Code
{
    public class Calculator
    {
        private string _input;
        private List<int> _numbers;

        public int Add(string input)
        {
            _input = input;

            SetNumbersFromInput();

            return Add();
        }

        private void SetNumbersFromInput()
        {
            _numbers = new List<int> { 0 };
            if (string.IsNullOrEmpty(_input))
                return;

            var result = Regex.Split(_input, @"\D+");

            result.ToList().ForEach(n =>
                _numbers.Add(Convert.ToInt32(n))
            );
        }

        private int Add()
        {
            return _numbers.Sum();
        }
    }
}
