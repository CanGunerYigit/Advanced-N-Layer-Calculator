using BusinessLayer;
using CommonLayer.Entities;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Calculator.Business
{
    public class CalculatorService : ICalculatorService
    {
        private readonly AppDbContext _context;

        public CalculatorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<double> EvaluateExpression(string expression)
        {
            var cleanedExpression = CleanExpression(expression);
            return await ParseAndEvaluate(cleanedExpression);
        }

        public string CleanExpression(string expression)
        {
            return expression.Replace(" ", "");
        }

        public async Task<double> ParseAndEvaluate(string expression)
        {
            var numbers = new List<double>();
            var operators = new List<char>();

            for (int i = 0; i < expression.Length;)
            {
                if (char.IsDigit(expression[i]) || expression[i] == '.')
                {
                    var numberString = GetNextNumber(expression, ref i);
                    numbers.Add(double.Parse(numberString, CultureInfo.InvariantCulture));
                }
                else
                {
                    operators.Add(expression[i]);
                    i++;
                }
            }

            return await EvaluateOperators(numbers, operators, expression);
        }
        private async Task<double> EvaluateOperators(List<double> numbers, List<char> operators, string expression)
        {
            await _context.Set<CalculationHistory>().AddAsync(new() { Expression = expression, Result = 0 });
            await _context.SaveChangesAsync();
            //first process multiplication and division
            for (int i = 0; i < operators.Count;)
            {
                if (operators[i] == '*' || operators[i] == '/')
                {
                    double result = operators[i] == '*'
                        ? numbers[i] * numbers[i + 1]
                        : numbers[i] / numbers[i + 1];
                    await _context.Set<CalculationHistory>().AddAsync(new() { Expression = $"{numbers[i]} {operators[i]} {numbers[i + 1]} = {result}", Result = 0 });
                    await _context.SaveChangesAsync();

                    numbers[i] = result;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            //second process addition and extraction
            double finalResult = numbers[0];
            for (int i = 0; i < operators.Count; i++)
            {
                finalResult = operators[i] == '+'
                    ? finalResult + numbers[i + 1]
                    : finalResult - numbers[i + 1];
                await _context.Set<CalculationHistory>().AddAsync(new() { Expression = $"{finalResult} {operators[i]} {numbers[i + 1]} = {finalResult}" });
                await _context.SaveChangesAsync();
            }

            return finalResult;
        }

        private string GetNextNumber(string expression, ref int index)
        {
            var numberString = string.Empty;
            while (index < expression.Length && (char.IsDigit(expression[index]) || expression[index] == '.'))
            {
                numberString += expression[index];
                index++;
            }
            return numberString;
        }

        public async Task<List<CalculationHistory>> GetAllHistory()
        {
            return await _context.Set<CalculationHistory>().ToListAsync();
        }
    }
}
