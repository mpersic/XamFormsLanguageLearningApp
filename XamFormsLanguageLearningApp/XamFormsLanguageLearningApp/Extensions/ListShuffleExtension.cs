using System;
using System.Collections.Generic;
using System.Text;

namespace XamFormsLanguageLearningApp.Extensions
{
    public static class ListShuffleExtension
    {
        #region Fields
        private static Random rng = new Random();

        #endregion Fields

        #region Methods

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        #endregion Methods
    }
}
