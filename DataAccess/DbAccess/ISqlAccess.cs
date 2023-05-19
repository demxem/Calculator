namespace DataAccess.DataAccess
{
    public interface ISqlAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedures, U parameters, string connectionId = "Default");
        Task SafeData<U>(string storedProcedures, U parameters, string connectionId = "Default");
    }
}