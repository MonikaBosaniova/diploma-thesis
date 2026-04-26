namespace Gates
{
    /// <summary>
    /// AND logic gate: returns true only when both inputs are true
    /// </summary>
    public class AndGate : ILogicGate
    {
        public bool Evaluate(bool input1, bool input2) => input1 && input2;
    }

    /// <summary>
    /// OR logic gate: returns true when at least one input is true
    /// </summary>
    public class OrGate : ILogicGate
    {
        public bool Evaluate(bool input1, bool input2) => input1 || input2;
    }

    /// <summary>
    /// NOT logic gate: returns the inverse of the first input (second input unused)
    /// </summary>
    public class NotGate : ILogicGate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2">not used</param>
        /// <returns></returns>
        public bool Evaluate(bool input1, bool input2 = false) => !input1;
    }

    /// <summary>
    /// XOR logic gate: returns true when inputs differ
    /// </summary>
    public class XorGate : ILogicGate
    {
        public bool Evaluate(bool input1, bool input2) => input1 ^ input2;
    }
}