﻿using Newtonsoft.Json;
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
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    public class VocabularyService : IVocabularyService
    {
        public List<VocabularyQuestionAnswerObj> GetQuestions(Assembly assembly, string name)
        {
            var processedName = ProcessNonEnglishCharacterRevision(name);
            var list = new List<VocabularyQuestionAnswerObj>();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Vocabulary.{processedName}.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<VocabularyQuestionAnswerObj>>(json);
            }
            return list;
        }

        private string ProcessNonEnglishCharacterRevision(string name)
        {
            return name.Replace("č", "c").Replace("ć", "c").Replace("š", "s").Replace(" ","");
        }

        public List<Unit> GetSelectedUnits(Assembly assembly, string selectedUnitName)
        {
            var processedName = selectedUnitName.ToLower();
            if (processedName.Contains("č"))
            {
                processedName = processedName.Replace("č", "c");
            }
            var list = new List<Unit>();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Vocabulary.vocabularyunits-{processedName.FirstOrDefault()}.json");

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
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.JsonAssets.Vocabulary.vocabularyunits.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<Unit>>(json);
            }
            return list;
        }
    }
}
