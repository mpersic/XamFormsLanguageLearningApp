using LanguageLearningApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                var processedName = ToLowerAndNonEnglishCharacters(name);
                processedName = RemoveWhitespace(processedName);

                var grammarExamples = new List<GrammarExample>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Grammar.{processedName}.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    grammarExamples = JsonConvert.DeserializeObject<List<GrammarExample>>(json);
                }
                return grammarExamples;
            }
            catch
            {
                return new List<GrammarExample>();
            }
            
        }

        public List<GrammarExamQuestion> GetGrammarExamQuestions(Assembly assembly, string name)
        {
            try
            {
                var processedName = ToLowerAndNonEnglishCharacters(name);
                processedName = RemoveWhitespace(processedName);

                var grammarExamQuestions = new List<GrammarExamQuestion>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Grammar.exam-{processedName}.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    grammarExamQuestions = JsonConvert.DeserializeObject<List<GrammarExamQuestion>>(json);
                }
                grammarExamQuestions.Shuffle();
                if (grammarExamQuestions.Count < 10)
                {
                    throw new Exception("Not enough items in this unit!");
                }
                var tenExams = grammarExamQuestions.Take(10).ToList();
                return tenExams;
            }
            catch
            {
                return new List<GrammarExamQuestion>();
            }
            
        }

        public List<Unit> GetSelectedUnits(Assembly assembly, string selectedUnitName)
        {
            try
            {
                var processedName = ToLowerAndNonEnglishCharacters(selectedUnitName);

                var units = new List<Unit>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Grammar.grammarunits-{processedName}.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    units = JsonConvert.DeserializeObject<List<Unit>>(json);
                }
                return units;
            }
            catch 
            {
                return new List<Unit>();
            }
           
        }

        public List<Unit> GetUnits(Assembly assembly)
        {
            try
            {
                var units = new List<Unit>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Grammar.grammarunits.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    units = JsonConvert.DeserializeObject<List<Unit>>(json);
                }
                return units;
            }
            catch 
            {
                return new List<Unit>();
            }
     
        }

        private string ToLowerAndNonEnglishCharacters(string name)
        {
            var processedName = name.ToLower();
            processedName = ProcessNonEnglishCharacters(processedName);
            return processedName;
        }

        private string ProcessNonEnglishCharacters(string name)
        {
            return name.Replace("č", "c").Replace("ć", "c").Replace("š", "s");
        }

        private string RemoveWhitespace(string name)
        {
            return name.Replace(" ", string.Empty);
        }

        #endregion Methods
    }
}