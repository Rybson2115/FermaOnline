using FermaOnline.Data;
using FermaOnline.Models;

namespace FermaOnline.Facades
{
    public class SurveyFacade
    {
        private readonly ApplicationDbContext _db;//dostęp do bazy danych 
        public SurveyFacade(ApplicationDbContext db)
        {
            _db = db;
        }

        public Survey create(SurveyDTO surveyDTO) { 
        
        }
    }
}
