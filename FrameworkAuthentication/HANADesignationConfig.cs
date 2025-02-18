using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameworkAuthentication
{
    public class HANADesignationConfig : IDestinationConfiguration
    {
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public bool ChangeEventsSupported()
        {
            return true;
        }
        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters parameters = new RfcConfigParameters();
            if (destinationName.Equals("SHX"))
            {

                parameters.Add(RfcConfigParameters.AppServerHost, "10.10.207.104");
                parameters.Add(RfcConfigParameters.SystemNumber, "01");
                parameters.Add(RfcConfigParameters.SystemID, destinationName);
                parameters.Add(RfcConfigParameters.User, "SUPPORT1");
                parameters.Add(RfcConfigParameters.Password, "Ecozen@123");
                parameters.Add(RfcConfigParameters.Client, "100");
                parameters.Add(RfcConfigParameters.Language, "EN");
                parameters.Add(RfcConfigParameters.PoolSize, "5");
                //parameters.Add(RfcConfigParameters.SAPRouter, "/H/203.112.134.24");

            }
            return parameters;
        }
    }
}
