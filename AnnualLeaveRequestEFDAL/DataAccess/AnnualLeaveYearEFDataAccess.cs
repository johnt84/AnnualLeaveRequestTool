using AnnualLeaveRequestEFDAL.DataAccess.Interfaces;
using AnnualLeaveRequestEFDAL.Models;

namespace AnnualLeaveRequestEFDAL.DataAccess
{
    public class AnnualLeaveYearEFDataAccess : IAnnualLeaveRequestYearEFDataAccess
    {
        private readonly AnnualLeaveDbContext _db;

        public AnnualLeaveYearEFDataAccess(AnnualLeaveDbContext db)
        {
            _db = db;
        }

        public List<AnnualLeaveYear> GetAnnualLeaveYears()
        {
            return _db.AnnualLeaveYears
                        .ToList();
        }

        public AnnualLeaveYear GetAnnualLeaveYear(int year)
        {
            return _db.AnnualLeaveYears
                        .Where(x => x.Year == year)
                        .FirstOrDefault();
        }

        public AnnualLeaveYear Create(AnnualLeaveYear model)
        {
            _db.AnnualLeaveYears.Add(model);

            return GetAnnualLeaveYear(model.Year);
        }

        public AnnualLeaveYear Update(AnnualLeaveYear model)
        {
            _db.AnnualLeaveYears.Update(model);

            return GetAnnualLeaveYear(model.Year);
        }

        public void Delete(AnnualLeaveYear model)
        {
            _db.AnnualLeaveYears.Remove(model);
        }
    }
}
