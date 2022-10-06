using LanguageLearningApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp.Services
{
    public class GrammarService : IGrammarService
    {
        #region Methods

        public List<GrammarExample> GetGrammarExamples(Assembly assembly, string name)
        {
            //var processedName = selectedUnitName.ToLower();
            //if (processedName.Contains("č"))
            //{
            //    processedName = processedName.Replace("č", "c");
            //}
            var list = new List<GrammarExample>();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.prezent-aktiv.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<GrammarExample>>(json);
            }
            return list;
        }

        public List<Unit> GetSelectedUnits(Assembly assembly, string selectedUnitName)
        {
            var processedName = selectedUnitName.ToLower();
            if (processedName.Contains("č"))
            {
                processedName = processedName.Replace("č", "c");
            }
            var list = new List<Unit>();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.grammarunits-{processedName}.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<Unit>>(json);
            }
            return list;
        }

        public List<Unit> GetUnits(Assembly assembly)
        {
            var list = new List<Unit>();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.grammarunits.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<Unit>>(json);
            }
            return list;
        }

        #endregion Methods
    }
}