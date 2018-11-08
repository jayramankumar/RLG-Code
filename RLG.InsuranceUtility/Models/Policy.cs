using System;
using System.Collections.Generic;
using RLG.InsuranceUtility.Helpers;

namespace RLG.InsuranceUtility.Models
{
    public class Policy
    {
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Premium { get; set; }
        public bool Membership { get; set; }
        public decimal DiscretionaryBonus { get; set; }
        public decimal UpliftPercentage { get; set; }
        public decimal Maturity { get; set; }
        public decimal managementFees { get; set; }

        public Policy(){}

        public Policy(List<string> PolicyData)
        {
            try
            {
                if (PolicyData != null && PolicyData.Count >= 6)
                {
                    PolicyNumber = PolicyData[0];
                    StartDate = Convert.ToDateTime(PolicyData[1]);
                    Premium = Convert.ToDecimal(PolicyData[2]);
                    Membership = string.Equals(PolicyData[3], "Y", StringComparison.OrdinalIgnoreCase);
                    DiscretionaryBonus = Convert.ToDecimal(PolicyData[4]);
                    UpliftPercentage = Convert.ToDecimal(PolicyData[5]);
                }
            }
            catch(Exception ex)
            {
                Log.Error("Policy class populating policy from string List: " + ex);
            }
        }

    }
}