using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semantic.Sparql
{
    public class Sim_Queries
    {
        public const string prefix = "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                                "PREFIX : <https://www.sdir.no/SDIR_Simulator#>" +
                                "PREFIX scope: <https://www.sdir.no/SDIR_Simulator/shapes/scope#>" +
                                "PREFIX unit: <http://qudt.org/vocab/unit/>" +
                                "PREFIX sh: <http://www.w3.org/ns/shacl#>" +
                                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
                                "PREFIX req: <http://www.sidr.no/SDIR_Simulator/requirements#>" +
                                "PREFIX owl: <http://www.w3.org/2002/07/owl#>";

        public string getAllAltReferences(string language)
        {
            return prefix + "SELECT ?paragraph ?altReference " +
            "        WHERE { " +
            "        	?paragraph :subRequirement ?altReference ." +
            "        }";
        }

        public string getAllRadioCoverageAreaScopes(string language) {

            return prefix + "SELECT DISTINCT * " +
            "    WHERE { " +
            "  ?scope a :Scope; " +
            "            sh:path :radioArea; " +
            "            sh:hasValue ?radioArea.  " +
            "  ?radioArea rdfs:label ?radioAreaLabel . " +
            "  FILTER(LANG(?radioAreaLabel) = '" + language + "') " +
            "}";
        }

        public string getAllRequirementsWithSubRequirements(string language)
        {
            return prefix + "SELECT DISTINCT ?requirement ?requirementLabel ?scopeDescription ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementScopeDescription ?subRequirementRegulationReference ?subRequirementTheme " +
            "        WHERE { " +
            "          ?requirement a :Requirement ; " +
            "                       :subRequirement ?subRequirement . " +
            "          ?requirement rdfs:label ?requirementLabel . " +
            "          FILTER(LANG(?requirementLabel) = '" + language + "') " +
            "          ?requirement sh:property ?scope . " +
            "          ?scope sh:description ?scopeDescription . " +
            "          ?requirement :regulationReference ?regulationReference . " +
            "          ?requirement :theme ?theme . " +
            "          FILTER(LANG(?theme) = '" + language + "') " +
            "          ?subRequirement rdfs:label ?subRequirementLabel . " +
            "          FILTER(LANG(?subRequirementLabel) = '" + language + "') " +
            "          ?subRequirement sh:property ?subRequirementScope . " +
            "          ?subRequirementScope sh:description ?subRequirementScopeDescription . " +
            "          ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "          ?subRequirement :theme ?subRequirementTheme . " +
            "          FILTER(LANG(?subRequirementTheme) = '" + language + "') " +
            "        }";
        }

        public string getAllRequirementsWithSubRequirementsIffPresent(string language)
        {
            return prefix + "SELECT DISTINCT ?requirement ?requirementLabel ?scopeDescription ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementScopeDescription ?subRequirementRegulationReference ?subRequirementTheme " +
            "        WHERE { " +
            "          ?requirement a :Requirement .  " +
            "          ?requirement rdfs:label ?requirementLabel . " +
            "          FILTER(LANG(?requirementLabel) = '" + language + "')  " +
            "          ?requirement sh:property ?scope . " +
            "          ?scope sh:description ?scopeDescription .  " +
            "          ?requirement :regulationReference ?regulationReference . " +
            "          ?requirement :theme ?theme . " +
            "          FILTER(LANG(?theme) = '" + language + "')  " +
            "          OPTIONAL { " +
            "            ?requirement :subRequirement ?subRequirement . " +
            "            ?subRequirement rdfs:label ?subRequirementLabel . " +
            "            FILTER(LANG(?subRequirementLabel) = '" + language + "')  " +
            "            ?subRequirement sh:property ?subRequirementScope . " +
            "            ?subRequirementScope sh:description ?subRequirementScopeDescription . " +
            "            ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "            ?subRequirement :theme ?subRequirementTheme . " +
            "            FILTER(LANG(?subRequirementTheme) = '" + language + "')  " +
            "          } " +
            "        }";

        }

        public string getAllTradeAreaScopes(string language)
        {
            return prefix + "SELECT DISTINCT * " +
            "WHERE { " +
            "  ?scope a :Scope ; " +
            "     sh:path :tradeArea ; " +
            "     sh:hasValue ?tradeArea . " +

            "  ?tradeArea rdfs:label ?tradeAreaLabel . " +
            "  FILTER(LANG(?tradeAreaLabel) = '" + language + "')  " +
            "}";

        }

        public string getAllVesselTypeScopes(string language)
        {
            return prefix + "SELECT DISTINCT * " +
            "WHERE { " +
            "  ?scope a :Scope ; " +
            "     sh:path :vesselType; " +
            "     sh:hasValue ?vesselType. " +

            "  ?vesselType rdfs:label ?vesselTypeLabel . " +
            "  FILTER(LANG(?vesselTypeLabel) = '" + language + "')  " +
            "}";

        }

        // (builddate format: "YYYY-MM-DDTHH:MM:SS").
        public string getRequirementsBasedOnBuiltDate(string language, string builddate)
        {
            return prefix + "SELECT ?paragraph ?label ?regulationReference ?theme (COUNT(?altRef) as ?numOfAdditionalInfoLinks) " +
            "WHERE { " +
            "  ?paragraph sh:property ?scope . " +
            "  ?scope sh:path :builtDate ; " +
            "               ?date ?dateValue . " +
            "  FILTER (?dateValue <= '" + builddate + "')  " +
            // "  FILTER (?dateValue <= "1998 - 05 - 02T00: 00:00")  " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "')  " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  OPTIONAL { ?paragraph :subRequirement ?altRef . } " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "')  " +
            "} " +
            "GROUP BY ?paragraph ?label ?regulationReference ?theme";
        }

        public string getRequirementsBasedOnBuiltDateWithSubRequirementsIfPresent(string language, string builddate)
        {
            return prefix + "SELECT ?paragraph ?label ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementRegulationReference ?subRequirementTheme " +
            "WHERE { " +
            "  ?paragraph sh:property ?scope . " +
            "  ?scope sh:path :builtDate ; " +
            "               ?date ?dateValue .  " +
            "  FILTER (?dateValue <= '" + builddate + "')  " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "') " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "')  " +
            "  OPTIONAL {  " +
            "    ?paragraph :subRequirement ?subRequirement .  " +
            "    ?subRequirement rdfs:label ?subRequirementLabel . " +
            "    FILTER(LANG(?subRequirementLabel) = '" + language + "') " +
            "    ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "    ?subRequirement :theme ?subRequirementTheme . " +
            "    FILTER(LANG(?subRequirementTheme) = '" + language + "')  " +
            "  } " +
            "}";
        }

        public string getRequirementsBasedOnRadioArea(string language)
        {
            return prefix + "SELECT DISTINCT * " +
            "WHERE { " +
            "  ?requirement a :Requirement ; " +
            "               sh:property ?scope . " +
            "  ?scope a :Scope ; " +
            "         sh:path :radioArea ; " +
            "         sh:hasValue ?radioArea . " +
            "  ?radioArea rdfs:label ?radioAreaLabel . " +
            "  FILTER(LANG(?radioAreaLabel) = '" + language + "') " +
            "}";
        }

        // # Change ?radioArea with any radio area e.g. :A1
        public string getRequirementsBasedOnRadioAreaWithSubRequirementsIfPresent(string language, string radioArea)
        {
            return prefix + "SELECT ?paragraph ?label ?radioAreaLabel ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementRegulationReference ?subRequirementTheme " +
            "WHERE { " +
            "  ?paragraph sh:property ?scope . " +
            "  ?scope sh:path :radioArea ; " +
            "               sh:hasValue ?radioArea .  " +
            "  ?radioArea rdfs:label ?radioAreaLabel . " +
            "  FILTER(LANG(?radioAreaLabel) = '" + language + "') " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "') " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  OPTIONAL {  " +
            "    ?paragraph :subRequirement ?subRequirement .  " +
            "    ?subRequirement rdfs:label ?subRequirementLabel . " +
            "    FILTER(LANG(?subRequirementLabel) = '" + language + "') " +
            "    ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "    ?subRequirement :theme ?subRequirementTheme . " +
            "    FILTER(LANG(?subRequirementTheme) = '" + language + "') " +
            "  } " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "') " +
            "}";
        }

        // # Change ?tradeArea with any trade area e.g. :BankFishingI
        public string getRequirementsBasedOnTradeAreaWithSubRequirementIfPresent(string language, string tradeArea)
        {
            return prefix + "SELECT ?paragraph ?label ?tradeAreaLabel ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementRegulationReference ?subRequirementTheme " +
            "WHERE { " +
            "  ?paragraph a :Requirement . " +
            "  OPTIONAL { " +
            "    ?paragraph sh:property ?scope . " +
            "    ?scope sh:path :tradeArea ; " +
            "                 sh:hasValue ?tradeArea .  " +
            "    ?tradeArea rdfs:label ?tradeAreaLabel . " +
            "  } " +
            "  OPTIONAL { " +
            "  	?paragraph sh:or/rdf:rest*/rdf:first ?orItem . " +
            "    ?orItem sh:and/rdf:rest*/rdf:first ?scope . " +
            "    ?scope sh:path :tradeArea ; " +
            "                 sh:hasValue ?tradeArea . " +
            "    ?tradeArea rdfs:label ?tradeAreaLabel . " +
            "  } " +
            "  FILTER(LANG(?tradeAreaLabel) = '" + language + "') " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "') " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  OPTIONAL {  " +
            "    ?paragraph :subRequirement ?subRequirement .  " +
            "    ?subRequirement rdfs:label ?subRequirementLabel . " +
            "    FILTER(LANG(?subRequirementLabel) = '" + language + "') " +
            "    ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "    ?subRequirement :theme ?subRequirementTheme . " +
            "    FILTER(LANG(?subRequirementTheme) = '" + language + "')  " +
            "  } " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "') " +
            "}";
        }

        public string getRequirementsBasedOnVesselLengthOverall(string language, string lengthValue)
        {
            return prefix + "SELECT ?paragraph ?label ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementRegulationReference ?subRequirementTheme " +
            "WHERE { " +
            "  ?paragraph sh:property ?scope . " +
            "  ?scope sh:path :vesselLengthOverall ; " +
            "         ?length ?lengthValue . " +
            "  FILTER (?lengthValue >= " + lengthValue + ")  " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "')  " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  OPTIONAL {  " +
            "    ?paragraph :subRequirement ?subRequirement . " +
            "    ?subRequirement rdfs:label ?subRequirementLabel . " +
            "    FILTER(LANG(?subRequirementLabel) = '" + language + "')  " +
            "    ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "    ?subRequirement :theme ?subRequirementTheme . " +
            "    FILTER(LANG(?subRequirementTheme) = '" + language + "')  " +
            "  } " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "') " +
            "}";

        }

        // # Change ?vesselType with any vessel type e.g. :FishingVessel
        public string getRequirementsBasedOnVesselType(string language, string vesselType)
        {
            return prefix + "SELECT DISTINCT ?vesselLabel ?paragraph ?label ?regulationReference ?theme  " +
            "WHERE { " +
            " ?paragraph sh:property ?scope . " +
            "  ?scope sh:path :vesselType ; " +
            "               sh:hasValue ?vesselType .  " +
            "  ?vesselType rdfs:label ?vesselLabel . " +
            "  FILTER(LANG(?vesselLabel) = '" + language + "')  " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "') " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  OPTIONAL { ?paragraph :subRequirement ?altRef . } " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "') " +
            "}";

        }

        // # Change ?vesselType with any vessel type e.g. :FishingVessel
        public string getRequirementsBasedOnVesselTypeWithSubRequirementIfPresent(string language, string vesselType)
        {
            return prefix + "SELECT ?paragraph ?label ?vesselLabel ?regulationReference ?theme ?subRequirement ?subRequirementLabel ?subRequirementRegulationReference ?subRequirementTheme " +
            "WHERE { " +
            "  ?paragraph sh:property ?scope . " +
            "  ?scope sh:path :vesselType ; " +
            "	       sh:hasValue ?vesselType .  " +
            "  ?vesselType rdfs:label ?vesselLabel . " +
            "  FILTER(LANG(?vesselLabel) = '" + language + "') " +
            "  ?paragraph rdfs:label ?label . " +
            "  FILTER(LANG(?label) = '" + language + "')  " +
            "  ?paragraph :regulationReference ?regulationReference . " +
            "  OPTIONAL {  " +
            "    ?paragraph :subRequirement ?subRequirement .  " +
            "    ?subRequirement rdfs:label ?subRequirementLabel . " +
            "    FILTER(LANG(?subRequirementLabel) = '" + language + "')  " +
            "    ?subRequirement :regulationReference ?subRequirementRegulationReference . " +
            "    ?subRequirement :theme ?subRequirementTheme . " +
            "    FILTER(LANG(?subRequirementTheme) = '" + language + "') " +
            "  } " +
            "  ?paragraph :theme ?theme . " +
            "  FILTER(LANG(?theme) = '" + language + "') " +
            "}";

        }



    }

}
