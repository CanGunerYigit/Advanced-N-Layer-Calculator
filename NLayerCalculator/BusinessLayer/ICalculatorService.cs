using CommonLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ICalculatorService
    {
        Task<List<CalculationHistory>> GetAllHistory();
        Task<double>  EvaluateExpression(string expression);

        public string CleanExpression(string expression);

        public Task<double> ParseAndEvaluate(string expression);
    }
}
