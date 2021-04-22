using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.Common.Models
{
    /// <summary>
    /// Primer Parametro:Array: Nombre Propiedad, Tipo de consulta = Contains, Equals
    /// Segundo Parametro: Valor
    /// </summary>
    public class PaginatorRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public List<PaginatorRequestQuery> Query { get; set; }

    }

    public class PaginatorRequestQuery
    {
        public string ProperyName { get; set; }
        public string Sentence { get; set; }
        public string ProperyValue { get; set; }

    }

    public class PaginatorViewModel<T>
    {
        public PaginatorResponse<T> QueryResult { get; set; }
        public int ActualPage { get; set; }
        public int TotalPage { get; set; }
        public T Object { get; set; }
    }
    public class PaginatorResponse<T>
    {

        public IList<T> Results { get; set; }
        public int TotalRows { get; set; }
    }
}
