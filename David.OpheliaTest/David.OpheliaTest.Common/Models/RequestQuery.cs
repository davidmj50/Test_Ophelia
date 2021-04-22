using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.Common.Models
{
    public class RequestQuery
    {
        public List<RequestQueryContent> Query { get; set; }
    }
    public class RequestQueryContent
    {
        public string ProperyName { get; set; }
        public string PropertyType { get; set; }
        public string Sentence { get; set; }
        public string ProperyValue { get; set; }

    }
}
