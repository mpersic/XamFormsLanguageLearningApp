using LanguageLearningApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamFormsLanguageLearningApp.Extensions;
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

        public List<GrammarExamQuestion> GetGrammarExamQuestions(Assembly assembly, string name)
        {
            //var processedName = selectedUnitName.ToLower();
            //if (processedName.Contains("č"))
            //{
            //    processedName = processedName.Replace("č", "c");
            //}
            var list = new List<GrammarExamQuestion>();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Grammar.exam-prezent-aktiv.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<GrammarExamQuestion>>(json);
            }
            list.Shuffle();
            if (list.Count < 10)
            {
                throw new Exception("Not enough items in this unit!");
            }
            var tenExams = list.Take(10).ToList();
            return tenExams;
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