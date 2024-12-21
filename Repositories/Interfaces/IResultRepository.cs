public interface IResultRepository {
    Task<List<Result>> GetAllResults();
    Task<Result> CreateResult(Result result);
    Task<Result?> GetResultById(int id);

    Task<Result?> UpdateResult(Result updatedresult);
    Task<bool> DeleteResult(int id);

    Task<Result?> GetResultByGrandPrixId(int grandprixid);
}