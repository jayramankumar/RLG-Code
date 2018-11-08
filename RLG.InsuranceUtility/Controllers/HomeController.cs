using System;
using System.Collections.Generic;
using System.Web.Mvc;

using RLG.InsuranceUtility.Helpers;
using RLG.InsuranceUtility.Models;
using RLG.InsuranceUtility.BusinessLogic;

namespace RLG.InsuranceUtility.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action does the Policy maturity calculation for input data received by CSV
        /// </summary>
        /// <returns>Confirmation Message</returns>
        public ActionResult Assessment()
        {
            try
            {            
                string inputFile = Server.MapPath(ConstantVariables.inputFilePath);
                string outputXMLFile = Server.MapPath(ConstantVariables.outputXMLFilePath);
                PolicyBL policyBL = new PolicyBL();
                
                //Read input CSV and get the equivalent policy list 
                List<Policy> policies = policyBL.PopulatePolicyFromCSV(inputFile);
                if (policies != null)
                {
                    //Compute Maturity for policy list based on business logic
                    policyBL.ComputeMaturity(policies);
                    //Write a xml file for computed maturity policy list
                    new XMLWriter().WriteXML(policies, outputXMLFile);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on Home controller Assessment action", ex);
            }
            return View();
        }
    }
}