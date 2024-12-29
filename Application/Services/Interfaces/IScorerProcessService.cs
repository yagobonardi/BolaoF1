public interface IScorerProcessService {
    Task<Tuple<bool, string>> ProcessGrandPrixPoints(int idGrandPrix);
}