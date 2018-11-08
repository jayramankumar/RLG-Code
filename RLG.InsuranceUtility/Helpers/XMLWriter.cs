using System.Collections.Generic;
using System.Xml;

using RLG.InsuranceUtility.Models;

namespace RLG.InsuranceUtility.Helpers
{
    public class XMLWriter
    {
        public void WriteXML(List<Policy> policies, string outputXMLFile)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(outputXMLFile))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Policies");
                    foreach (Policy policyObject in policies)
                    {
                        writer.WriteStartElement("Policy");
                        writer.WriteElementString("Number", policyObject.PolicyNumber);
                        writer.WriteElementString("Maturity", policyObject.Maturity.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("Exception while writing XML : " + ex);
            }

        }
    }
}