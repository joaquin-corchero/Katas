using Katas.StringCaclulatorApp.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;
using System;

namespace Katas.StringCalculator.Tests.Code
{
    /// <summary>
    /// Summary description for CalculatorTests
    /// </summary>
    [TestClass]
    public class when_working_with_the_calculator : SpecBase
    {
        protected Calculator _calculator;

        protected override void Establish_context()
        {
            base.Establish_context();
            _calculator = new Calculator();
        }
    }

    [TestClass]
    public class and_adding_numbers : when_working_with_the_calculator
    {
        private string _input;
        private int _actual;
        private int _expected;

        private void Execute()
        {
            _actual = _calculator.Add(_input);
        }

        [TestMethod]
        public void then_if_no_numbers_are_passed_0_is_returned()
        {
            _expected = 0;

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_if_a_single_number_is_passed_then_that_nunber_is_returned()
        {
            _expected = 5;
            _input = _expected.ToString();

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_if_two_numbers_are_added_then_the_result_is_the_sum_of_them()
        {
            _expected = 5;
            _input = "2,3";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_6_numbers_can_be_added()
        {
            _expected = 6;
            _input = "1,1,1,1,1,1";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_if_there_is_a_new_line_on_the_input_the_values_are_added()
        {
            _expected = 33;
            _input = "1\n2,30";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_numbers_can_be_added_when_the_first_line_has_the_delimiter()
        {
            _expected = 3;
            _input = "//;\n1;2";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_a_string_with_negatives_throws_exception()
        {
            _input = "-20";
            var expected = "negatives not allowed ( -20)";
            var actual = string.Empty;
            try
            {
                Execute();
            }
            catch (InvalidOperationException e)
            {
                actual = e.Message;
            }
            actual.ShouldContain(expected);
        }

        [TestMethod]
        public void then_if_there_are_negative_numbers_they_are_displayed_on_the_exception()
        {
            _input = "-20,-3,4";
            var expected = "negatives not allowed ( -20 -3)";
            var actual = string.Empty;
            try
            {
                Execute();
            }
            catch (InvalidOperationException e)
            {
                actual = e.Message;
            }
            actual.ShouldEqual(expected);
        }

        [TestMethod]
        public void then_if_there_is_prefix_and_negative_numbers_they_are_displayed_on_the_exception()
        {
            _input = "//;-20;-3;4";
            var expected = "negatives not allowed ( -20 -3)";
            var actual = string.Empty;
            try
            {
                Execute();
            }
            catch (InvalidOperationException e)
            {
                actual = e.Message;
            }
            actual.ShouldEqual(expected);
        }

        [TestMethod]
        public void numbers_bigger_than_one_thousand_are_ignored()
        {
            _expected = 5;
            _input = "1001,5";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_the_delimiter_can_be_of_any_length()
        {
            _expected = 6;
            _input = "//[***]\n1***2***3";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_multiple_delimiters_are_allowed()
        {
            _expected = 6;
            _input = "//[*][%]\n1*2%3";

            Execute();

            _actual.ShouldEqual(_expected);
        }

        [TestMethod]
        public void then_multiple_delimiters_longer_than_one_char_are_allowed()
        {
            _expected = 6;
            _input = "//[lkjl*][$$$%]\n1*2%3";

            Execute();

            _actual.ShouldEqual(_expected);
        }
    }
}
