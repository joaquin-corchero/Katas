using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Katas.StringCaclulatorApp.Code
{
    //http://osherove.com/tdd-kata-1/
    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            NumberCollector collector = new NumberCollector(input);

            return collector.Values.Sum();
        }

    }

    public class NumberCollector
    {
        private string _input;
        internal List<int> Values { get; private set; }

        public NumberCollector(string input)
        {
            _input = input;
            PopulateValues();
            var validator = new NegativeNumberValidator(Values);
            validator.Validator();
        }

        private void PopulateValues()
        {
            Values = new List<int>();
            var delimiters = new DelimiterExtractor(_input).GetDelimiter();

            var items = _input.Split(delimiters)
                .Where(i => !string.IsNullOrEmpty(i))
                .ToList();

            items.ForEach(i =>
            {
                var value = Convert.ToInt32(i);
                if (value <= 1000)
                    Values.Add(value);
            });
        }

    }

    public class NegativeNumberValidator
    {
        private List<int> _values = new List<int>();

        public NegativeNumberValidator(List<int> values)
        {
            _values = values;
        }

        internal void Validator()
        {
            var negativeNumbers = _values.Where(n => n < 0).ToList();

            if (!negativeNumbers.Any())
                return;

            RaiseNegativeNumberException(negativeNumbers);
        }

        private static void RaiseNegativeNumberException(List<int> negativeNumbers)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("negatives not allowed (");

            negativeNumbers.ForEach(n =>
            {
                builder.AppendFormat(" {0}", n);
            });

            builder.Append(")");
            throw new InvalidOperationException(builder.ToString());
        }
    }

    public class DelimiterExtractor
    {
        private string _input;

        public DelimiterExtractor(string fitsLine)
        {
            _input = fitsLine;
        }

        public char[] GetDelimiter()
        {
            List<char> result = new List<char> { '\r', '\n', ',', '/' };
            if (!_input.StartsWith("//"))
                return result.ToArray();

            var numberOfMinusPosition = 2;
            var lineChars = _input.ToCharArray();
            for (int i = 1; i < lineChars.Length; i++)
            {
                numberOfMinusPosition = i;
                if (lineChars[i] == '-' || Char.IsNumber(lineChars[i]))
                    break;
            }

            var splitters = _input.Substring(2, numberOfMinusPosition - 2).ToCharArray().ToList();

            splitters.ForEach(s =>
            {
                if (!result.Contains(s))
                    result.Add(s);
            });

            return result.ToArray();
        }
    }
}
