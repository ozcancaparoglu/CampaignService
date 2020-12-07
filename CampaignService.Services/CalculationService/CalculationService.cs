using CampaignService.Services.CalculationService;
using Jace;

namespace CampaignService.Services.CalculationService
{
    public class CalculationService : ICalculationService
    {
        #region Fields

        #endregion

        #region Ctor
        public CalculationService()
        {

        }
        #endregion

        #region Methods

        
        public string Calculate(string formula)
        {
            //TODO 
            var calcEngine = new CalculationEngine();
            var result =calcEngine.Calculate(formula);
            return result.ToString();
        }

        #endregion


        #region Validations

        #endregion
    }
}
