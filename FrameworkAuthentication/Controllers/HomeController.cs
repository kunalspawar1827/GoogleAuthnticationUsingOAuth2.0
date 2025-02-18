using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace FrameworkAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly string UserGmail;
        string Msg = string.Empty;
        public ActionResult Index(int? ParamId)
        {

            //avd
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

                designation.Ping();



                IRfcFunction rfcFunction = designation.Repository.CreateFunction("ZMM_PO_APPR");
                rfcFunction.SetValue("IM_USRID", ParamId);
                // Call the SAP function for this single row
                rfcFunction.Invoke(designation);
                url = rfcFunction.GetValue("EX_RE_URL").ToString();

                try
                {
               
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
                // return Json<object>(obj);
                return View();
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
                    return View();
                }
            }

        }

    }
}
