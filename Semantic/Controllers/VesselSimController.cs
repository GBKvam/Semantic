using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Semantic.Sparql;

namespace Semantic.Controllers
{
    [Route("api/sim")]
    [ApiController]
    public class VesselSimController : ControllerBase
    {

        SimFusekiAPI simfusekiapi = new SimFusekiAPI();

        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(simfusekiapi.GetAllAltReferences("no"));
        }


        [HttpGet("{id}")]
        public ActionResult ReturnAction(string id)
        {
            if(id == "RadioAreas")
            {
                return Ok((simfusekiapi.GetAllRadioCoverageAreaScopes("no")));
            }
            else if (id == "AllReqWithSub")
            {
                return Ok((simfusekiapi.GetAllRequirementsWithSubRequirements("no")));
            }
            else if (id == "AllReqWithSubIFP")
            {
                return Ok(simfusekiapi.GetAllRequirementsWithSubRequirementsIffPresent("no"));
            }
            else if (id == "TradeAreas")
            {
                return Ok(simfusekiapi.GetAllTradeAreaScopes("no"));
            }
            else if (id == "VesselTypes")
            {
                return Ok(simfusekiapi.GetAllVesselTypeScopes("no"));
            }
            else if (id == "ReqBuiltDate")
            {
                return Ok(simfusekiapi.GetRequirementsBasedOnBuiltDate("no", "1998-05-02T00:00:00"));
            }
            else if (id == "ReqBuiltDateSubIFP")
            {
                return Ok(simfusekiapi.GetRequirementsBasedOnBuiltDateWithSubRequirementsIfPresent("no", "1998-05-02T00:00:00"));
            }
            else if (id == "ReqBasedOnRadioArea")
            {
                return Ok(simfusekiapi.GetRequirementsBasedOnRadioArea("no"));
            }
            else if (id == "ReqBasedOnRadioWSubModel")
            {
                return Ok(simfusekiapi.GetRequirementsBasedOnRadioAreaWithSubRequirementsIfPresent("no", "A1"));
            }
            else if (id == "2")
            {
                return Ok(simfusekiapi.GetRequirementsBasedOnBuiltDateWithSubRequirementsIfPresent("no", "1998-05-02T00:00:00"));
            }
            else if (id == "3")
            {
                return Ok(simfusekiapi.GetRequirementsBasedOnBuiltDateWithSubRequirementsIfPresent("no", "1998-05-02T00:00:00"));
            }
            else
            {
                return Ok(simfusekiapi.GetAllAltReferences("no"));
            }
            
        }


        



    }
}
