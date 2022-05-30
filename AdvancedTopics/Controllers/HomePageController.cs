using AdvancedTopics.Helpers;
using CustomModules.Models;
using CustomModules.Modules;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ErrorViewModel = AdvancedTopics.Models.ErrorViewModel;

namespace AdvancedTopics.Controllers
{
    public class HomePageController : Controller
    {
        #region Fields

        private readonly ILogger<HomePageController> _logger;
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public HomePageController(ILogger<HomePageController> logger,
                                  IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveAthlete(Athlete athlete)
        {
            Publisher publisher = new Publisher();
            SportsSelectionHelper sportsHelper = new SportsSelectionHelper(_configuration, publisher);

            athlete = await sportsHelper.ProcessAthleteRegistration(athlete);

            return Json(new
            {
                eligibleFor = athlete.EligibleForSports
            });
        }

        [HttpGet]
        public PartialViewResult GetReflectionTabContent()
        {
            var model = new DynamicModel
            { 
                IsValidData = true,
                ModelType = typeof(Employee),
                DataItems = new List<object>() {
                    new Employee() { Name = "Tonmoy", EmpId = "DEV-1005", Email = "tanvir@gmail.com", Tenure = 11 }, 
                    new Employee() { Name = "Saif", EmpId = "QA-5112", Email = "saif@yahoo.com", Tenure = 5 }, 
                    new Employee() { Name = "Rin", EmpId = "OP-3101", Email = "rin.12@gmail.com", Tenure = 1 }, 
                    new Employee() { Name = "Saad", EmpId = "ACC-5443", Email = "sasd@gmail.com", Tenure = 2 }, 
                },
                NewData = new Employee()
            };

            return PartialView("_ReflectionAttributes", model);
        }

        [HttpPost]
        public ActionResult AddNewDataInReflectionTab(SubmittedModel sModel)  
        {
            DynamicModel dynamicModel = new DynamicModel();
            dynamicModel.DataItems = new List<object>();
            dynamicModel.DataItems.AddRange(sModel.DataItems);
            dynamicModel.ModelType = Type.GetType(sModel.ModelType.ToString());
            
            if (ModelState.IsValid)
            {
                dynamicModel.IsValidData = true;
                dynamicModel.DataItems.Add(sModel.NewData);
                dynamicModel.NewData = new Employee();
            }
            else
            {
                dynamicModel.IsValidData = false;
                dynamicModel.NewData = sModel.NewData;  // Keep the input section open
            }

            return PartialView("_ReflectionAttributes", dynamicModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Private Methods



        #endregion

    }
}