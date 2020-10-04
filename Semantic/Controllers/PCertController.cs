using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Semantic.Sparql;

namespace Semantic.Controllers
{
    [Route("api/cert")]
    [ApiController]
    public class PCertController : ControllerBase
    {
        Queries sparql = new Queries();
        private RestClient client = new RestClient("http://localhost:3030/APS/query");

        private string GetAPSQueryData(string query)
        {

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("query", query);
            IRestResponse response = client.Execute(request);
            return response.Content;
            
        }


        private StringBuilder getPCertByID(string id)
        {

            // CUT AND PASTE JOB GONE ROUGE  

            StringBuilder sb = new StringBuilder();

            sb.Append(GetAPSQueryData(sparql.requiredAge(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredEducation(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificates(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCommonCourses(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCourseOptions(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredSGSFirstAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredSGSSecondAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredSGSThirdAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCompetancyFirstSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCompetancySecondSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCompetancyThirdSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificatesFirstSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificatesFirstSGSAlternativeSecondList(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificatesSecondSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredAttachmentFirstSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredAttachmentSecondSGSAlternative(id, "no")));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredAttachmentThirdSGSAlternative(id, "no")));
           

            return sb;
        }

        [HttpGet]
        public String Get()
        {
            return GetAPSQueryData(sparql.getAllPersonalCert("no"));
        }



        [HttpGet("{id}")]
        public String GetCertById(string id)
        {
            return getPCertByID(id).ToString();
        }
    }
}

