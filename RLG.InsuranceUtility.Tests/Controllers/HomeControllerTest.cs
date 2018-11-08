using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RLG.InsuranceUtility.Controllers;
using RLG.InsuranceUtility.Models;
using RLG.InsuranceUtility.BusinessLogic;

namespace RLG.InsuranceUtility.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

       
        [TestMethod]
        public void Assessment()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Assessment() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Check Policy number start with A return Management Fee 3.
        /// </summary>
        [TestMethod]
        public void CheckPolicyTypeA()
        {
            PolicyBL policyBL = new PolicyBL();
            Policy policy = new Policy();

            policy.PolicyNumber = "A1200";
            Assert.AreEqual(policyBL.GetManagementFeePercentage(policy), 3);
        }

        /// <summary>
        /// Check Policy number start with B return Management Fee 5.
        /// </summary>
        [TestMethod]
        public void CheckPolicyTypeB()
        {
            PolicyBL policyBL = new PolicyBL();
            Policy policy = new Policy();

            policy.PolicyNumber = "B1200";
            Assert.AreEqual(policyBL.GetManagementFeePercentage(policy), 5);
        }
        
        /// <summary>
        /// Check Policy number start with C return Management Fee 7.
        /// </summary>
        [TestMethod]
        public void CheckPolicyTypeC()
        {
            PolicyBL policyBL = new PolicyBL();
            Policy policy = new Policy();

            policy.PolicyNumber = "C1200";
            Assert.AreEqual(policyBL.GetManagementFeePercentage(policy), 7);
        }

        [TestMethod]
        public void CheckComputeMaturity()
        {
            PolicyBL policyBL = new PolicyBL();
            List<Policy> policies = new List<Policy>()
            {
              new Policy(){PolicyNumber="A1200",StartDate =DateTime.Parse("01/06/1986"),Premium=1000,Membership= true,DiscretionaryBonus=1000,UpliftPercentage=40}
            };

            policyBL.ComputeMaturity(policies);
            Assert.AreNotEqual(policies[0].Maturity, decimal.MinValue);
        }

    }
}
