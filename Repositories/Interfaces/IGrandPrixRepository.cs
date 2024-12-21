public interface IGrandPrixRepository {
    Task<List<GrandPrix>> GetAllGrandPrix();

    Task<GrandPrix> CreateGrandPrix(GrandPrix grandprix);

    Task<List<GrandPrix>> CreatePrixes(List<GrandPrix> grandprixes);

    Task<GrandPrix?> GetGrandPrixById(int id);

    Task<GrandPrix?> UpdateGrandPrix(GrandPrix updatedgrandprix);

    Task<bool> DeleteGrandPrixById(int id);
}