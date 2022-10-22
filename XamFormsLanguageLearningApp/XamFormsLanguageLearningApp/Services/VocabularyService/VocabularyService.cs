using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Extensions;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    public class VocabularyService : IVocabularyService
    {
        #region Methods

        public List<VocabularyQuestionAnswerObj> GetQuestions(Assembly assembly, string name)
        {
            try
            {
                var processedName = ProcessNonEnglishCharacterRevision(name);
                processedName = processedName.Replace(" ", string.Empty);
                if (processedName.Contains("revise"))
                {
                    processedName = processedName.Split('-').Last();
                }
                var list = new List<VocabularyQuestionAnswerObj>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Vocabulary.{processedName}.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<VocabularyQuestionAnswerObj>>(json);
                }
                if (list.Count < 10)
                {
                    throw new Exception("Not enough items in this unit!");
                }
                if (!name.Contains("revise"))
                {
                    list.Shuffle();
                    var tenExams = list.Take(10).ToList();
                    return tenExams;
                }
                return list;
            }
            catch
            {
                return new List<VocabularyQuestionAnswerObj>();
            }

        }

        public List<Unit> GetSelectedUnits(Assembly assembly, string name)
        {
            try
            {
                name = name.ToLower();
                var processedName = ProcessNonEnglishCharacterRevision(name);
                processedName = processedName.Split('-').Last();
                processedName = processedName.Replace(" ", string.Empty);

                var list = new List<Unit>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Vocabulary.vocabularyunits-{processedName}.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<Unit>>(json);
                }
                return list;
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
                var list = new List<Unit>();
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Vocabulary.vocabularyunits.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<Unit>>(json);
                }
                return list;
            }
            catch
            {
                return new List<Unit>();
            }
        }

        private string ProcessNonEnglishCharacterRevision(string name)
        {
            return name.Replace("č", "c").Replace("ć", "c").Replace("š", "s");
        }

        #endregion Methods
    }
}