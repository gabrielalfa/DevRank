namespace DevRank.Models
{
    public class AiEvaluationPolicy
    {
        public bool IsEnabled { get; set; }
        public string Mode { get; set; }
        public int MonthlyTokenBudget { get; set; }
        public int TokensUsed { get; set; }
        public int EstimatedCostInCents { get; set; }
        public string FundingStatus { get; set; }
        public string Strategy { get; set; }
    }
}
