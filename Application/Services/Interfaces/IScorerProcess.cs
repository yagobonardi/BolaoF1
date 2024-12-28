public interface IScorerProcess {
    Task<Tuple<bool, string>> ProcessGrandPrixPoints(int idGrandPrix);
}