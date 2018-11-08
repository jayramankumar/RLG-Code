using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using RLG.InsuranceUtility.Models;
using RLG.InsuranceUtility.Helpers;

namespace RLG.InsuranceUtility.BusinessLogic
{
    /// <summary>
    /// This class contains buisness logics for policy
    /// </summary>
    public class PolicyBL
    {
        /// <summary>
        /// Call ComputeMaturity to calculate Maturity based on business logic
        /// </summary>
        public void ComputeMaturity(Policy policy)
        {
            try
            {
                DateTime policyCriteriaYear = DateTime.Parse("01/01/1990");
                decimal managementFees = GetManagementFeePercentage(policy);

                decimal managementFeeValue = (policy.Premium * (managementFees / 100));
                decimal upliftPercentageValue = (policy.UpliftPercentage / 100 + 1);

                //The basic calculation for the maturity value is:((premiums – management fee) +discretionary bonus if qualifying) *uplift
                policy.Maturity = (((policy.Premium - managementFeeValue + policy.DiscretionaryBonus)) * upliftPercentageValue);
            }
            catch (Exception ex)
            {
                Log.Error("Individual policy maturity computation: " + ex);
            }
        }

        public decimal GetManagementFeePercentage(Policy policy)
        {
            decimal managementFeesPercentage = decimal.MinValue;

            try
            {
                //Todo: To replace with a pattern 
                //check the criteria to decide ManagementFees
                if (string.Equals(policy.PolicyNumber[0].ToString(), "A", StringComparison.OrdinalIgnoreCase))
                    managementFeesPercentage = Convert.ToInt16(PolicyManagementFee.A);
                else if (string.Equals(policy.PolicyNumber[0].ToString(), "B", StringComparison.OrdinalIgnoreCase))
                    managementFeesPercentage = Convert.ToInt16(PolicyManagementFee.B);
                else if (string.Equals(policy.PolicyNumber[0].ToString(), "C", StringComparison.OrdinalIgnoreCase))
                    managementFeesPercentage = Convert.ToInt16(PolicyManagementFee.C);
            }
            catch (Exception ex)
            {
                Log.Error("Mangement Fee percentage calculation function Exception: " + ex);
            }
            return managementFeesPercentage;
        }

        /// <summary>
        /// This function calculates Maturity based on logic for policy list
        /// </summary>
        /// <param name="policies"></param>
        /// <returns>Success confirmation</returns>
        public bool ComputeMaturity(List<Policy> policies)
        {
            bool success = false;

            try
            {
                foreach (Policy policy in policies)
                {
                    ComputeMaturity(policy);
                }
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception while computing Maturity for all policies", ex);
            }
            return success;
        }


        /// This function reads data from input csv
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputXMLFile"></param>
        /// <returns>Policy list populated by CSV</returns>
        public List<Policy> PopulatePolicyFromCSV(string inputFile)
        {
            List<Policy> policies = new List<Policy>();
            try
            {
                using (var sr = new StreamReader(inputFile, Encoding.GetEncoding("gbk")))
                {
                    var reader = new CsvFileReader(sr);
                    var row = new List<string>();
                    int rowNumber = 1;
                    while (reader.ReadRow(row))
                    {
                        //To avoid Header row to be added as policy record
                        if (rowNumber++ > 1)
                            policies.Add(new Policy(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception while reading input CSV", ex);
            }
            return policies;
        }
    }
}