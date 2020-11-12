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
    public class APSFusekiAPI
    {
        APS_Queries sparql = new APS_Queries();
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

        public APSFusekiAPI()
        {

        }
               
        public List<Certificates> GetAllPersonalCertificates(string language)
        {
            Certificates certificates = new Certificates();

            string response = GetAPSQueryData(sparql.getAllPersonalCert(language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = o.Last.Last.Last.Last.Children().ToList();

            string c_code = "";
            string c_name = "";
            string c_type = "";
            List<Certificates> rtn = new List<Certificates>();

            foreach (var item in tokens)
            {
                c_code = item["Certificate"]["value"].ToString().Split('#')[1];
                c_type = item["CertificateType"]["value"].ToString().Split('#')[1];
                c_name = item["label"]["value"].ToString();
                rtn.Add(new Certificates() { Code = c_code, Name = c_name, CertificateType = c_type });
            }

            return rtn;
            
        }

        public CertificateRequirements GetPersonalCertificateById2(string id, string language)
        {
            string tmp_code = "";
            string tmp_lbl = "";

            CertificateRequirements certReq = new CertificateRequirements();
            certReq.Code = id;

            string response = GetAPSQueryData(sparql.requiredAge(id, language));
            JObject o = JObject.Parse(response);

            List<JToken> tokens = GetAPSFusekiQueryData(sparql.requiredAge(id, language)).Children().ToList();
            
            try
            {
                certReq.RequiredAge = tokens[0]["ageDescription"]["value"].ToString();
            }
            catch(Exception e)
            {

            }

            tokens = GetAPSFusekiQueryData(sparql.requiredEducation(id, language)).Children().ToList();

            try
            {
                
                List<Education> edu = new List<Education>();
                
                foreach (var item in tokens)
                {
                    tmp_code = "";
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["education"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {

                    }
                    try
                    {
                        tmp_lbl = item["educationLabel"]["value"].ToString();
                    }
                    catch(Exception e)
                    {
                        
                    }


                    if(tmp_code.Length > 0 && tmp_lbl.Length > 0)
                    {
                        edu.Add(new Education() { Code = tmp_code, Name = tmp_lbl });
                    }
                    


                }
                certReq.Educations = edu;
            }
            catch (Exception e)
            {

            }

            tokens = GetAPSFusekiQueryData(sparql.requiredCertificates(id, language)).Children().ToList();

            try
            {

                List<Certificate> cer = new List<Certificate>();


                foreach (var item in tokens)
                {
                    tmp_code = "";
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["certificate"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {
                        tmp_code = "";
                    }
                    try
                    {
                        tmp_lbl = item["certificateLabel"]["value"].ToString();
                    }
                    catch (Exception e)
                    {
                        tmp_lbl = "";
                    }


                    if (tmp_code.Length > 0 && tmp_lbl.Length > 0)
                    {
                        cer.Add(new Certificate() { Code = tmp_code, Name = tmp_lbl });
                    }

                    


                }
                certReq.Certificate = cer;
            }
            catch (Exception e)
            {

            }
            tokens = GetAPSFusekiQueryData(sparql.requiredCommonCourses(id, language)).Children().ToList();

            try
            {

                List<Courses> cour = new List<Courses>();

                foreach (var item in tokens)
                {
                    tmp_code = "";
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["course"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {
                        tmp_code = "";
                    }
                    try
                    {
                        tmp_lbl = item["courseLabel"]["value"].ToString();
                    }
                    catch (Exception e)
                    {
               
                        tmp_lbl = "";
                    }


                    if (tmp_code.Length > 0 && tmp_lbl.Length > 0)
                    {
                        cour.Add(new Courses() { Code = tmp_code, Name = tmp_lbl });
                    }

                }
                certReq.Courses = cour;
            }
            catch (Exception e)
            {

            }

            tokens = GetAPSFusekiQueryData(sparql.requiredCourseOptions(id, language)).Children().ToList();

            try
            {

                List<Courses> cour = new List<Courses>();

                foreach (var item in tokens)
                {
                    tmp_code = "";
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["course"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {
                        tmp_code = "";
                    }
                    try
                    {
                        tmp_lbl = item["courseLabel"]["value"].ToString();
                    }
                    catch (Exception e)
                    {

                        tmp_lbl = "";
                    }


                    if (tmp_code.Length > 0 && tmp_lbl.Length > 0)
                    {
                        cour.Add(new Courses() { Code = tmp_code, Name = tmp_lbl });
                    }

                }
                certReq.Courses = cour;
            }
            catch (Exception e)
            {

            }

            tokens = GetAPSFusekiQueryData(sparql.requiredSGSFirstAlternative(id, language)).Children().ToList();

            try
            {

                List<string> sgs1 = new List<string>();

                foreach (var item in tokens)
                {
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["sgsDescription"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {
                        tmp_code = e.Message;
                    }
     
                  

                    if (tmp_lbl.Length > 0)
                    {
                        sgs1.Add(tmp_lbl);
                    }

                }
                certReq.sgsAlt1 = sgs1;
            }
            catch (Exception e)
            {

            }

            tokens = GetAPSFusekiQueryData(sparql.requiredSGSSecondAlternative(id, language)).Children().ToList();

            try
            {

                List<string> sgs2 = new List<string>();

                foreach (var item in tokens)
                {
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["sgsDescription"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {
                        tmp_code = e.Message;
                    }



                    if (tmp_lbl.Length > 0)
                    {
                        sgs2.Add(tmp_lbl);
                    }

                }
                certReq.sgsAlt2 = sgs2;
            }
            catch (Exception e)
            {

            }

            tokens = GetAPSFusekiQueryData(sparql.requiredSGSThirdAlternative(id, language)).Children().ToList();

            try
            {

                List<string> sgs3 = new List<string>();

                foreach (var item in tokens)
                {
                    tmp_lbl = "";
                    try
                    {
                        tmp_code = item["sgsDescription"]["value"].ToString().Split('#')[1];
                    }
                    catch (Exception e)
                    {
                        tmp_code = e.Message;
                    }



                    if (tmp_lbl.Length > 0)
                    {
                        sgs3.Add(tmp_lbl);
                    }

                }
                certReq.sgsAlt3 = sgs3;
            }
            catch (Exception e)
            {

            }

            return certReq;

        }
        
        public StringBuilder GetPersonalCertificateById(string id, string language)
        {

            // CUT AND PASTE JOB GONE ROUGE  

            StringBuilder sb = new StringBuilder();

            // sb.Append(GetAPSQueryData(sparql.requiredAge(id, language)));
            // sb.AppendLine(",");
            // sb.Append(GetAPSQueryData(sparql.requiredEducation(id, language)));
            // sb.AppendLine(",");
            // sb.Append(GetAPSQueryData(sparql.requiredCertificates(id, language)));
            // sb.AppendLine(",");
            // sb.Append(GetAPSQueryData(sparql.requiredCommonCourses(id, language)));
            // sb.AppendLine(",");
            // sb.Append(GetAPSQueryData(sparql.requiredCourseOptions(id, language)));
            // sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredSGSFirstAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredSGSSecondAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredSGSThirdAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCompetancyFirstSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCompetancySecondSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCompetancyThirdSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificatesFirstSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificatesFirstSGSAlternativeSecondList(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredCertificatesSecondSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredAttachmentFirstSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredAttachmentSecondSGSAlternative(id, language)));
            sb.AppendLine(",");
            sb.Append(GetAPSQueryData(sparql.requiredAttachmentThirdSGSAlternative(id, language)));


            return sb;
            
        }


    }
}
