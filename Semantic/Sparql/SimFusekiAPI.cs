using Newtonsoft.Json.Linq;
using RestSharp;
using Semantic.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Sparql
{
    public class SimFusekiAPI
    {
        Sim_Queries sparql = new Sim_Queries();
        private RestClient client = new RestClient("http://localhost:3030/ET/query");
        public SimFusekiAPI()
        {

        }
        private string GetAPSQueryData(string query)
        {

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("query", query);
            IRestResponse response = client.Execute(request);
            return response.Content;

        }

        private JToken GetAPSFusekiQueryData(string query)
        {
            JToken rtnToke = null;
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("query", query);
            IRestResponse response = client.Execute(request);
            JObject o = JObject.Parse(response.Content);

            rtnToke = o.Last.Last.Last.Last;

            return rtnToke;

        }



        public List<AltReferences> GetAllAltReferences(string language)
        {
            AltReferences altRef = new AltReferences();

            string response = GetAPSQueryData(sparql.getAllAltReferences(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_paragraph = "";
            string c_altReference = "";
            List<AltReferences> rtn = new List<AltReferences>();

            foreach (var item in tokens)
            {
                c_paragraph = item["paragraph"]["value"].ToString().Split('#')[1];
                c_altReference = item["altReference"]["value"].ToString().Split('#')[1];

                rtn.Add(new AltReferences() { Paragraph = c_paragraph, AltReference = c_altReference });
            }

            return rtn;

        }

        public List<RadioAreaModel> GetAllRadioCoverageAreaScopes(string language)
        {
            RadioAreaModel radioArea = new RadioAreaModel();

            string response = GetAPSQueryData(sparql.getAllRadioCoverageAreaScopes(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_scope = "";
            string c_radioArea = "";
            string c_radioAreaLabel = "";
            List<RadioAreaModel> rtn = new List<RadioAreaModel>();

            foreach (var item in tokens)
            {
                c_scope = item["scope"]["value"].ToString().Split('#')[1];
                c_radioArea = item["radioArea"]["value"].ToString().Split('#')[1];
                c_radioAreaLabel = item["radioAreaLabel"]["value"].ToString();

                rtn.Add(new RadioAreaModel() { Scope = c_scope, RadioArea = c_radioArea, RadioAreaLabel = c_radioAreaLabel });
            }

            return rtn;

        }

        public List<RequirementModel> GetAllRequirementsWithSubRequirements(string language)
        {

            string response = GetAPSQueryData(sparql.getAllRequirementsWithSubRequirements(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_requirement = "";
            string c_requirementLabel = "";
            string c_scopeDescription = "";
            string c_regulationReference = "";
            string c_theme = "";
            string c_subRequirement = "";
            string c_subRequirementLabel = "";
            string c_subRequirementScopeDescription = "";
            string c_subRequirementRegulationReference = "";
            string c_subRequirementTheme = "";
            List<RequirementModel> rtn = new List<RequirementModel>();

            foreach (var item in tokens)
            {
                c_requirement = item["requirement"]["value"].ToString().Split('#')[1];
                c_requirementLabel = item["requirementLabel"]["value"].ToString();
                c_scopeDescription = item["scopeDescription"]["value"].ToString();
                c_regulationReference = item["regulationReference"]["value"].ToString();
                c_theme = item["theme"]["value"].ToString();
                c_subRequirement = item["subRequirement"]["value"].ToString().Split('#')[1];
                c_subRequirementLabel = item["subRequirementLabel"]["value"].ToString();
                c_subRequirementScopeDescription = item["subRequirementScopeDescription"]["value"].ToString();
                c_subRequirementRegulationReference = item["subRequirementRegulationReference"]["value"].ToString();
                c_subRequirementTheme = item["subRequirementTheme"]["value"].ToString();



                rtn.Add(new RequirementModel() { 
                    requirement = c_requirement, 
                    requirementLabel = c_requirementLabel,
                    scopeDescription = c_scopeDescription,
                    regulationReference = c_regulationReference,
                    theme = c_theme,
                    subRequirement = c_subRequirement,
                    subRequirementLabel = c_subRequirementLabel,
                    subRequirementScopeDescription = c_subRequirementScopeDescription,
                    subRequirementRegulationReference = c_subRequirementRegulationReference,
                    subRequirementTheme = c_subRequirementTheme
                });
            }

            return rtn;

        }

        public List<SimpleRequirementModel> GetAllRequirementsWithSubRequirementsIffPresent(string language)
        {

            string response = GetAPSQueryData(sparql.getAllRequirementsWithSubRequirementsIffPresent(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_requirement = "";
            string c_requirementLabel = "";
            string c_scopeDescription = "";
            string c_regulationReference = "";
            string c_theme = "";

            List<SimpleRequirementModel> rtn = new List<SimpleRequirementModel>();

            foreach (var item in tokens)
            {
                c_requirement = item["requirement"]["value"].ToString().Split('#')[1];
                c_requirementLabel = item["requirementLabel"]["value"].ToString();
                c_scopeDescription = item["scopeDescription"]["value"].ToString();
                c_regulationReference = item["regulationReference"]["value"].ToString();
                c_theme = item["theme"]["value"].ToString();




                rtn.Add(new SimpleRequirementModel()
                {
                    requirement = c_requirement,
                    requirementLabel = c_requirementLabel,
                    scopeDescription = c_scopeDescription,
                    regulationReference = c_regulationReference,
                    theme = c_theme
                });
            }

            return rtn;

        }

        public List<TradeArea> GetAllTradeAreaScopes(string language)
        {


            string response = GetAPSQueryData(sparql.getAllTradeAreaScopes(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_scope = "";
            string c_tradeArea = "";
            string c_tradeAreaLabel = "";
            List<TradeArea> rtn = new List<TradeArea>();

            foreach (var item in tokens)
            {
                c_scope = item["scope"]["value"].ToString().Split('#')[1];
                c_tradeArea = item["tradeArea"]["value"].ToString().Split('#')[1];
                c_tradeAreaLabel = item["tradeAreaLabel"]["value"].ToString();

                rtn.Add(new TradeArea() { scope = c_scope, tradeArea = c_tradeArea, tradeAreaLabel = c_tradeAreaLabel });
            }

            return rtn;

        }

        public List<VesselTypeModel> GetAllVesselTypeScopes(string language)
        {


            string response = GetAPSQueryData(sparql.getAllVesselTypeScopes(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_scope = "";
            string c_vesselType = "";
            string c_vesselTypeLabel = "";
            List<VesselTypeModel> rtn = new List<VesselTypeModel>();

            foreach (var item in tokens)
            {
                c_scope = item["scope"]["value"].ToString().Split('#')[1];
                c_vesselType = item["vesselType"]["value"].ToString().Split('#')[1];
                c_vesselTypeLabel = item["vesselTypeLabel"]["value"].ToString();

                rtn.Add(new VesselTypeModel() { scope = c_scope, vesselType = c_vesselType, vesselTypeLabel = c_vesselTypeLabel });
            }

            return rtn;

        }

        public List<RequirementByBuildDate> GetRequirementsBasedOnBuiltDate(string language, string builddate)
        {


            string response = GetAPSQueryData(sparql.getRequirementsBasedOnBuiltDate(language, builddate));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_paragraph = "";
            string c_label = "";
            string c_regulationReference = "";
            string c_theme = "";
            string c_numOfAdditionalInfoLinks = "";
            List<RequirementByBuildDate> rtn = new List<RequirementByBuildDate>();

            foreach (var item in tokens)
            {
                c_paragraph = item["paragraph"]["value"].ToString().Split('#')[1];
                c_label = item["label"]["value"].ToString();
                c_regulationReference = item["regulationReference"]["value"].ToString();
                c_theme = item["theme"]["value"].ToString();
                c_numOfAdditionalInfoLinks = item["numOfAdditionalInfoLinks"]["value"].ToString();

                rtn.Add(new RequirementByBuildDate() { 
                    paragraph = c_paragraph, 
                    label = c_label,
                    regulationReference = c_regulationReference,
                    theme = c_theme,
                    numOfAdditionalInfoLinks = c_numOfAdditionalInfoLinks
                });
            }

            return rtn;

        }

        public List<SimpleRequirementByBuildDate> GetRequirementsBasedOnBuiltDateWithSubRequirementsIfPresent(string language, string builddate)
        {

            string response = GetAPSQueryData(sparql.getRequirementsBasedOnBuiltDateWithSubRequirementsIfPresent(language, builddate));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_paragraph = "";
            string c_label = "";
            string c_regulationReference = "";
            string c_theme = "";
            List<SimpleRequirementByBuildDate> rtn = new List<SimpleRequirementByBuildDate>();

            foreach (var item in tokens)
            {
                c_paragraph = item["paragraph"]["value"].ToString().Split('#')[1];
                c_label = item["label"]["value"].ToString();
                c_regulationReference = item["regulationReference"]["value"].ToString();
                c_theme = item["theme"]["value"].ToString();

                rtn.Add(new SimpleRequirementByBuildDate()
                {
                    paragraph = c_paragraph,
                    label = c_label,
                    regulationReference = c_regulationReference,
                    theme = c_theme
                });
            }

            return rtn;

        }

        public List<RequirementRadioAreaModel> GetRequirementsBasedOnRadioArea(string language)
        {

            string response = GetAPSQueryData(sparql.getRequirementsBasedOnRadioArea(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_requirement = "";
            string c_scope = "";
            string c_radioArea = "";
            string c_radioAreaLabel = "";
            List<RequirementRadioAreaModel> rtn = new List<RequirementRadioAreaModel>();

            foreach (var item in tokens)
            {
                c_requirement = item["requirement"]["value"].ToString().Split('#')[1];
                c_scope = item["scope"]["value"].ToString().Split('#')[1];
                c_radioArea = item["radioArea"]["value"].ToString().Split('#')[1];
                c_radioAreaLabel = item["radioAreaLabel"]["value"].ToString();

                rtn.Add(new RequirementRadioAreaModel()
                {
                    requirement = c_requirement,
                    scope = c_scope,
                    radioArea = c_radioArea,
                    radioAreaLabel = c_radioAreaLabel
                });
            }

            return rtn;

        }


        public List<ReqBasedOnRadioWSubModel> GetRequirementsBasedOnRadioAreaWithSubRequirementsIfPresent(string language, string radioArea)
        {

            string response = GetAPSQueryData(sparql.getRequirementsBasedOnRadioAreaWithSubRequirementsIfPresent(language, radioArea));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_paragraph = "";
            string c_label = "";
            string c_radioAreaLabel = "";
            string c_regulationReference = "";
            string c_theme = "";
            List<ReqBasedOnRadioWSubModel> rtn = new List<ReqBasedOnRadioWSubModel>();

            foreach (var item in tokens)
            {
                c_paragraph = item["paragraph"]["value"].ToString().Split('#')[1];
                c_label = item["label"]["value"].ToString();
                c_radioAreaLabel = item["radioAreaLabel"]["value"].ToString();
                c_regulationReference = item["regulationReference"]["value"].ToString();
                c_theme = item["theme"]["value"].ToString();

                rtn.Add(new ReqBasedOnRadioWSubModel()
                {
                    paragraph = c_paragraph,
                    label = c_label,
                    radioAreaLabel = c_radioAreaLabel,
                    regulationReference = c_regulationReference,
                    theme = c_theme
                });
            }

            return rtn;

        }










    }
}
