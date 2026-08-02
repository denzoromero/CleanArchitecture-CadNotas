using Microsoft.Data.SqlClient;

namespace CleanCadNotas.Configurations
{
    public class TimeoutExceptionHandler
    {
        public static bool IsSqlTimeout(Exception ex)
        {
            while (ex != null)
            {
                if (ex is TimeoutException)
                    return true;

                if (ex is SqlException sqlEx &&
                    sqlEx.Message.Contains(
                        "Execution Timeout Expired",
                        StringComparison.OrdinalIgnoreCase))
                    return true;

                ex = ex.InnerException!;
            }

            return false;
        }

    }
}
