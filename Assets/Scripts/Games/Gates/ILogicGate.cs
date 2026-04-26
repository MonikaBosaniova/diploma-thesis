namespace Gates
{
    /// <summary>
    /// Interface for logic gate evaluation with two boolean inputs
    /// </summary>
    public interface ILogicGate
    {
        bool Evaluate(bool input1, bool input2);
    }
}