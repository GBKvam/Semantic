using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semantic.Sparql
{
    public class APS_Queries  
    {
        public APS_Queries()
        {

        }



        public const string prefix = "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                                "PREFIX : <https://www.sdir.no/SDIR_Simulator#>" +
                                "PREFIX course: <https://www.sdir.no/SDIR_Simulator/shapes/course#>" +
                                "PREFIX cert: <https://www.sdir.no/SDIR_Simulator/shapes/certificate#>" +
                                "PREFIX sh: <http://www.w3.org/ns/shacl#>" +
                                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
                                "PREFIX req: <http://www.sidr.no/SDIR_Simulator/requirements#>" +
                                "PREFIX owl: <http://www.w3.org/2002/07/owl#>";


        public string getAllPersonalCert(string language)
        {
            return prefix + "SELECT DISTINCT ?label ?Certificate ?CertificateType " +
            "        WHERE { " +
            "        	?Certificate a ?CertificateType; " +
            "                      rdfs:label ?label . " +
            "        FILTER(CONTAINS(LCASE(str(?CertificateType)), \"certificate\")) " +
            "  		FILTER(?CertificateType NOT IN (:CertificateRequirement)) " +
            "  		FILTER(lang(?label) = '" + language + "') " +
            "        } " +
            "ORDER BY ?Certificate";
        }

        public string requiredAge(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?ageDescription " +
            "WHERE {" +
            "?nodeShape sh:targetClass ?certClass . " +
            "FILTER(REGEX(STR(?certClass), \"" + id + "\"))" +
            "?nodeShape ?p ?propertyShape . " +
           "?propertyShape sh:path :age ;" +
             "sh:minInclusive ?age ;" +
             "sh:description ?ageDescription ." +
           "FILTER (LANG(?ageDescription) = '" + language + "')" +
            "}";
        }

        public string requiredAttachmentFirstSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?attachment ?attachmentLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:or/rdf:rest{0}/rdf:first ?orListItem . " +
            "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
            "    ?andListItem sh:path :requiresAttachment ; " +
            "        sh:hasValue ?attachment . " +
            "    ?attachment rdfs:label ?attachmentLabel . " +
            "    FILTER (LANG(?attachmentLabel) = '" + language + "')  " +
            "}";
        }

        public string requiredAttachmentSecondSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?attachment ?attachmentLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:or/rdf:rest{1}/rdf:first ?orListItem . " +
            "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
            "    ?andListItem sh:path :requiresAttachment ; " +
            "        sh:hasValue ?attachment . " +
            "    ?attachment rdfs:label ?attachmentLabel . " +
            "    FILTER (LANG(?attachmentLabel) = '" + language + "')  " +
            "}";
        }

        public string requiredAttachmentThirdSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?attachment ?attachmentLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:or/rdf:rest{2}/rdf:first ?orListItem . " +
            "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
            "    ?andListItem sh:path :requiresAttachment ; " +
            "        sh:hasValue ?attachment . " +
            "    ?attachment rdfs:label ?attachmentLabel . " +
            "    FILTER (LANG(?attachmentLabel) = '" + language + "')  " +
            "}";
        }

        public string requiredCertificates(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?certificate ?certificateLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:and/rdf:rest*/rdf:first ?certItem  " +
            "    FILTER(STRSTARTS(STR(?certItem), \"https://www.sdir.no/SDIR_Simulator/shapes/certificate#\")) " +
            "    ?certItem sh:hasValue ?certificate . " +
            "    OPTIONAL { " +
            "        ?certificate rdfs:label ?certificateLabel . " +
            "        FILTER (LANG(?certificateLabel) = '" + language + "')  " +
            "    } " +
            "}";
        }

        public string requiredCertificatesFirstSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?certificate ?certificateLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:or/rdf:rest{0}/rdf:first ?orListItem . " +
            "    ?orListItem sh:and/rdf:rest{0}/rdf:first ?andListItem . " +
            "    ?andListItem sh:or/rdf:rest*/rdf:first ?certificateShape . " +
            "    ?certificateShape sh:hasValue ?certificate . " +
            "    OPTIONAL { " +
            "        ?certificate rdfs:label ?certificateLabel . " +
            "        FILTER (LANG(?certificateLabel) = '" + language + "')  " +
            "    } " +
            "}";
        }

        public string requiredCertificatesFirstSGSAlternativeSecondList(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?certificate ?certificateLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:or/rdf:rest{0}/rdf:first ?orListItem . " +
            "    ?orListItem sh:and/rdf:rest{1}/rdf:first ?andListItem . " +
            "    ?andListItem sh:or/rdf:rest*/rdf:first ?certificateShape .  " +
            "    ?certificateShape sh:hasValue ?certificate . " +
            "    OPTIONAL { " +
            "        ?certificate rdfs:label ?certificateLabel . " +
            "        FILTER (LANG(?certificateLabel) = '" + language + "')  " +
            "    } " +
            "} ";
        }

        public string requiredCertificatesSecondSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?certificate ?certificateLabel " +
            "WHERE { " +
            "    ?nodeShape sh:targetClass ?certClass . " +
            "    FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "    ?nodeShape sh:or/rdf:rest{1}/rdf:first ?orListItem . " +
            "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
            "    ?andListItem sh:or/rdf:rest*/rdf:first ?certificateShape . " +
            "    ?certificateShape sh:hasValue ?certificate . " +
            "    ?certificate rdfs:label ?certificateLabel . " +
            "    FILTER (LANG(?certificateLabel) = '" + language + "')  " +
            "} ";
        }

        public string requiredCommonCourses(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?course ?courseLabel " +
            "WHERE { " +
            "  ?nodeShape sh:targetClass ?certClass . " +
            "  FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "  ?nodeShape sh:property ?propertyShape . " +
            "  ?propertyShape sh:path :requiresCourse ; " +
            "     sh:hasValue ?course . " +
            "  OPTIONAL { " +
            "    ?course rdfs:label ?courseLabel . " +
            "    FILTER (LANG(?courseLabel) = '" + language + "') " +
            "  } " +
            "}";
        }

        public string requiredCompetancyFirstSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?competancy ?competancyLabel " +
                "WHERE { " +
                "    ?nodeShape sh:targetClass ?certClass . " +
                "    FILTER(REGEX(STR(?certClass), \"" + id + "\"))    " +
                "    ?nodeShape sh:or/rdf:rest{0}/rdf:first ?orListItem . " +
                "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
                "    ?andListItem sh:path :requiresCompetancy ; " +
                "        sh:hasValue ?competancy . " +
                "    ?competancy rdfs:label ?competancyLabel . " +
                "    FILTER (LANG(?competancyLabel) = '" + language + "')  " +
                "} ";
        }

        public string requiredCompetancySecondSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?competancy ?competancyLabel " +
                "WHERE { " +
                "    ?nodeShape sh:targetClass ?certClass . " +
                "    FILTER(REGEX(STR(?certClass), \"" + id + "\"))    " +
                "    ?nodeShape sh:or/rdf:rest{1}/rdf:first ?orListItem . " +
                "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
                "    ?andListItem sh:path :requiresCompetancy ; " +
                "        sh:hasValue ?competancy . " +
                "    ?competancy rdfs:label ?competancyLabel . " +
                "    FILTER (LANG(?competancyLabel) = '" + language + "')  " +
                "} ";
        }

        public string requiredCompetancyThirdSGSAlternative(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?competancy ?competancyLabel " +
                "WHERE { " +
                "    ?nodeShape sh:targetClass ?certClass . " +
                "    FILTER(REGEX(STR(?certClass), \"" + id + "\"))    " +
                "    ?nodeShape sh:or/rdf:rest{2}/rdf:first ?orListItem . " +
                "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
                "    ?andListItem sh:path :requiresCompetancy ; " +
                "        sh:hasValue ?competancy . " +
                "    ?competancy rdfs:label ?competancyLabel . " +
                "    FILTER (LANG(?competancyLabel) = '" + language + "')  " +
                "} ";
        }

        public string requiredCourseOptions(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?course ?courseLabel " +
            "WHERE { " +
            "  ?nodeShape sh:targetClass ?certClass . " +
            "  FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
            "  ?nodeShape sh:or/rdf:rest*/rdf:first ?courseItem  " +
            "  FILTER(STRSTARTS(STR(?courseItem), +\"https://www.sdir.no/SDIR_Simulator/shapes/course#\")) " +
                        "  ?courseItem sh:hasValue ?course . " +
                        "  OPTIONAL {  " +
                        "    ?course rdfs:label ?courseLabel . " +
                        "    FILTER (LANG(?courseLabel) = '" + language + "')  " +
                        "  } " +
                        "} ";
        }

        public string requiredEducation(string id, string language)
        {
            return prefix + "SELECT DISTINCT ?education ?educationLabel " +
                "WHERE { " +
                "  ?nodeShape sh:targetClass ?certClass . " +
                "  FILTER(REGEX(STR(?certClass), \"" + id + "\")) " +
                "  OPTIONAL { " +
                "  	?nodeShape sh:or/rdf:rest*/rdf:first ?orListItem . " +
                "    ?orListItem sh:path :requiresEducation ; " +
                "                sh:hasValue ?education . " +
                "    ?education rdfs:label ?educationLabel . " +
                "    FILTER(LANG(?educationLabel) = 'no') " +
                "  } " +
                "  OPTIONAL { " +
                "  	?nodeShape sh:property ?propertyShape . " +
                "    ?propertyShape sh:path :requiresEducation ; " +
                "                   sh:hasValue ?education . " +
                "    ?education rdfs:label ?educationLabel . " +
                "    FILTER(LANG(?educationLabel) = '" + language + "') " +
                "  } " +
                "} ";

        }

        public string requiredSGSFirstAlternative(string id, string language)
        {

            return prefix + "SELECT DISTINCT ?sgsDescription " +
                "WHERE { " +
                "    ?nodeShape sh:targetClass ?certClass . " +
                "    FILTER(REGEX(STR(?certClass), \"" + id + "\"))   " +
                "    ?nodeShape sh:or/rdf:rest{0}/rdf:first ?orListItem . " +
                "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem  ." +
                "    ?andListItem sh:path :hasSeagoingServiceRequirement ; " +
                "        sh:hasValue ?seagoingservice ; " +
                "        sh:order 1 .  " +
                "    ?sgsNodeShape sh:targetClass ?seagoingservice ; " +
                "        sh:description ?sgsDescription . " +
                "    FILTER (LANG(?sgsDescription) = '" + language + "') " +
                "}";

        }

        public string requiredSGSSecondAlternative(string id, string language)
        {

            return prefix + "SELECT DISTINCT ?sgsDescription " +
                "WHERE { " +
                "    ?nodeShape sh:targetClass ?certClass . " +
                "    FILTER(REGEX(STR(?certClass), \"" + id + "\"))   " +
                "    ?nodeShape sh:or/rdf:rest{1}/rdf:first ?orListItem . " +
                "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem  ." +
                "    ?andListItem sh:path :hasSeagoingServiceRequirement ; " +
                "        sh:hasValue ?seagoingservice ; " +
                "        sh:order 2 .  " +
                "    ?sgsNodeShape sh:targetClass ?seagoingservice ; " +
                "        sh:description ?sgsDescription . " +
                "    FILTER (LANG(?sgsDescription) = '" + language + "') " +
                "}";

        }

        public string requiredSGSThirdAlternative(string id, string language)
        {

            return prefix + "SELECT DISTINCT ?sgsDescription " +
                "WHERE { " +
                "    ?nodeShape sh:targetClass ?certClass . " +
                "    FILTER(REGEX(STR(?certClass), \"" + id + "\"))   " +
                "    ?nodeShape sh:or/rdf:rest{2}/rdf:first ?orListItem . " +
                "    ?orListItem sh:and/rdf:rest*/rdf:first ?andListItem . " +
                "    ?andListItem sh:path :hasSeagoingServiceRequirement ; " +
                "        sh:hasValue ?seagoingservice ; " +
                "        sh:order 3 .  " +
                "    ?sgsNodeShape sh:targetClass ?seagoingservice ; " +
                "            sh:description ?sgsDescription . " +
                "    FILTER (LANG(?sgsDescription) = '" + language + "')  " +
                "} ";

        }
    }
}