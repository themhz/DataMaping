using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1.classes
{
    public static class JsonHandler
    {
        public static JObject ReadJsonFile(string jsonFilePath)
        {
            // Read the content of the JSON file
            string jsonFileContent = File.ReadAllText(jsonFilePath);

            // Parse the JSON content into a JObject
            JObject jsonObject = JObject.Parse(jsonFileContent);

            return jsonObject;
        }
        public static void CopyJsonFile(string inputFilePath, string outputFilePath)
        {
            // Read the content of the input JSON file
            string inputFileContent = File.ReadAllText(inputFilePath);

            // Write the content to the output file
            File.WriteAllText(outputFilePath, inputFileContent);
        }
        public static JObject AddFieldToTableInFile(JObject jsonObject, string tableName, JObject newField)
        {

            // Iterate through the Datasets and Tables
            foreach (JObject dataset in jsonObject["Datasets"])
            {
                foreach (JObject table in dataset["Tables"])
                {
                    // Check if the table name matches the one you want to modify
                    if (table["table"].ToString() == tableName)
                    {
                        // Add the new field to the table
                        JArray fields = (JArray)table["Fields"];
                        fields.Add(newField);
                        break;
                    }
                }
            }


            return jsonObject;
        }

        public static JObject AddTableToSchemaInFile(JObject jsonObject, string xsdName, JObject newTable)
        {            
            foreach (JObject dataset in jsonObject["Datasets"])
            {                                
                if (dataset["Name"].ToString() == xsdName)
                {
                    // Add the new table in xsd
                    JArray tables = (JArray)dataset["Tables"];
                    tables.Add(newTable);
                    break;
                }                
            }


            return jsonObject;
        }

        public static JObject AddSchemaInFile(JObject jsonObject, JObject newSchema)
        {
            
            JArray datasets = (JArray)jsonObject["Datasets"];
            datasets.Add(newSchema);                                        

            return jsonObject;
        }




        public static Boolean CheckIfSchemaExistInFile(JObject jsonObject, string jsonFile)
        {
            foreach (JObject dataset in jsonObject["Datasets"])
            {
                if (jsonFile == dataset["Name"].ToString())
                {
                    return true;
                }

            }
            return false;
        }
        public static Boolean CheckIfTableExistsInFile(JObject jsonObject, string tableName)
        {
            foreach (JObject dataset in jsonObject["Datasets"])
            {
                foreach (JObject table in dataset["Tables"])
                {
                    // Check if the table name matches the one you want to modify
                    if (table["table"].ToString() == tableName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static Boolean CheckIfFieldExistsInFile(JObject jsonObject, string fielName)
        {

            foreach (JObject dataset in jsonObject["Datasets"])
            {
                foreach (JObject table in dataset["Tables"])
                {
                    foreach(JObject field in table["Fields"])
                    {
                        // Check if the field name matches 
                        if (field["name"].ToString() == fielName)
                        {
                            return true;
                        }
                    }                    
                }
            }

            return false;
        }


        public static void WriteFile(string newinputJsonFilePath, JObject newJsonObject)
        {
            File.WriteAllText(newinputJsonFilePath, FormatFieldsInline(newJsonObject.ToString()));
        }
        public static string FormatFieldsInline(string jsonInput)
        {
            JObject jsonObj = JObject.Parse(jsonInput);

            FormatFields(jsonObj);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(jsonObj, settings);
        }

        private static void FormatFields(JToken token)
        {
            if (token is JObject obj)
            {
                if (obj.ContainsKey("Fields")&& obj["Fields"] != null && obj["Fields"].HasValues)
                {
                    JArray fieldsArray = (JArray)obj["Fields"];
                    string inlineFields = string.Join(",\n            ", fieldsArray.Select(x => x.ToString(Formatting.None)));
                    obj["Fields"] = new JRaw($"[\n            {inlineFields}\n          ]");
                }
                else
                {
                    foreach (var property in obj.Properties())
                    {
                        FormatFields(property.Value);
                    }
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    FormatFields(item);
                }
            }
        }
    }
}
