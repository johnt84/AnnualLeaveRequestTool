using AnnualLeaveRequest.Data;
using Castle.Core.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;

namespace AnnualLeaveRequestUnitTest
{
    [TestClass]
    public class AnnualLeaveRequestServiceTest
    {
        [TestMethod]
        public void GetYearsTest()
        {
            var sqlConfiguration = GetTestSqlConfiguration();

            var mockSqlConfiguration = new Mock<SqlConnectionConfiguration>("Server=;Database=AnnualLeave;Trusted_Connection=True;MultipleActiveResultSets=true");

            var mockIDbConnection = new Mock<SqlConnection>(sqlConfiguration);

            mockIDbConnection.Setup(x => x.)
            
            //mockSqlConfiguration.Setup(x => x.Value )

            var mockAnnualLeaveRequestService = new Mock<IAnnualLeaveRequestService>();

            var testYears = new List<int>() { 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 309, 2020, 2021, 2022, 2023 };

            mockAnnualLeaveRequestService.Setup(x => x.GetYears()).Returns(testYears);

            var annualLeaveRequestService = new AnnualLeaveRequestService(mockSqlConfiguration.Object);

            var years = annualLeaveRequestService.GetYears();

            Assert.AreEqual(testYears, years);
        }

        private SqlConnectionConfiguration GetTestSqlConfiguration()
        {
            return new SqlConnectionConfiguration("Server=;Database=AnnualLeave;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
