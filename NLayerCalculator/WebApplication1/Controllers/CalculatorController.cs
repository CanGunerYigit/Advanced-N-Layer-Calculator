using BusinessLayer;
using Calculator.CommonLayer.Dto;
using CommonLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;

namespace Calculator.Web.Controllers
{
    public class CalculatorController : Controller
    {
        private CalculationHistory calculationHistory;
        private readonly AddDto addDto;
        private ICalculationHistoryRepository _calculationHistoryRepository;
        private ICalculatorService _calculationHistoryService;


        public CalculatorController(ICalculationHistoryRepository calculationHistoryRepository, ICalculatorService calculationHistoryService)
        {
            addDto = new();
            calculationHistory = new();
            _calculationHistoryRepository = calculationHistoryRepository;
            _calculationHistoryService= calculationHistoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AddDto());  // AddDto'yu view'a gönderir
        }

        [HttpPost]
        public async Task<IActionResult> Index(string expression)
        {

            if (string.IsNullOrEmpty(expression))
            {

                ModelState.AddModelError("", "İfade boş olamaz.");

                return View(addDto);

            }

           
           double value= await  _calculationHistoryService.EvaluateExpression(expression);
            AddDto addDto2= new();
            addDto2.Result = value;

            return View(addDto2);
        }


        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            return View(await _calculationHistoryRepository.GetAllAsync());
        }

       
    }
}
