using Katas.StringCaclulatorApp.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;

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
    }
}
