using Microsoft.Ajax.Utilities;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;

namespace FrameworkAuthentication.Controllers
{
    public class AccountController : ApiController
    {

        private readonly string UserGmail;
        string Msg = string.Empty;
        // Rfc Execution Method For Authenticating Valid User
        [HttpGet]
        public JsonResult<object> ExecuteRFC_Call(int ParamId)
        {
            try
            {
                string url = string.Empty;
                // Configure SAP connection
                RfcDestination designation = null;
                HANADesignationConfig config = new HANADesignationConfig();
                // Register the SAP destination configuration
                RfcDestinationManager.RegisterDestinationConfiguration(config);
                designation = RfcDestinationManager.GetDestination("SHX");
                // Get the SAP repository
                RfcRepository repo = designation.Repository;

              
                IRfcFunction rfcFunction = designation.Repository.CreateFunction("ZMM_PO_APPR");
                rfcFunction.SetValue("IM_USRID", ParamId);
                // Call the SAP function for this single row
                rfcFunction.Invoke(designation);
                  url =    rfcFunction.GetValue("EX_RE_URL").ToString();
                    
                try
                {
                    designation.Ping();
                    Msg = "Connected To SAP";
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;

                    RfcDestinationManager.UnregisterDestinationConfiguration(config);
                    repo = null;
                    designation = null;
                }


                var obj = new
                {
                    Id = 1,
                    Message = Msg,
                    Timestamp = DateTime.Now,
                    SapUserEmail = "Kunalpawar.net@gmail.com"


                };
                RfcDestinationManager.UnregisterDestinationConfiguration(config);
                repo = null;
                designation = null;
                return Json<object>(obj);
            }
            catch (Exception ex)
            {
                {
                    var obj = new
                    {
                        Id = 0,
                        Message = ex.Message,
                        Timestamp = DateTime.Now,

                    };
                    return Json<object>(obj);
                }
            }

        }
    }
}

 

   