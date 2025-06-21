using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartLGS.Core.Models
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TotalQuestionsAttribute : Attribute
    {
        public int TotalQuestions { get; }

        public TotalQuestionsAttribute(int totalQuestions)
        {
            TotalQuestions = totalQuestions;
        }
    }

    public enum SubjectType
    {
        [TotalQuestions(20)]
        Matematik = 1,

        [TotalQuestions(20)]
        Fen = 2,

        [TotalQuestions(20)]
        Turkce = 3,

        [TotalQuestions(10)]
        Inkilap = 4,

        [TotalQuestions(10)]
        Din = 5,

        [TotalQuestions(10)]
        Ingilizce = 6
    }
    
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TotalQuestions { get; set; }
    }
}