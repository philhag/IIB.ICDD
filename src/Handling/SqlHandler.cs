namespace IIB.ICDD.Handling
{
    internal class SqlHandler
    {

        
    }

    public class SqlResult
    {
        public string Query;
        public bool Success;
        public string Result;

        public SqlResult(string query, bool success, string result)
        {
            Query = query;
            Success = success;
            Result = result;
        }
    }
}
