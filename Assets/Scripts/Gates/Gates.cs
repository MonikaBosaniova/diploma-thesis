namespace Gates
{
    public class AndGate : ILogicGate
    {
        public bool Evaluate(bool input1, bool input2) => input1 && input2;
    }

    public class OrGate : ILogicGate
    {
        public bool Evaluate(bool input1, bool input2) => input1 || input2;
    }

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

    public class XorGate : ILogicGate
    {
        public bool Evaluate(bool input1, bool input2) => input1 ^ input2;
    }
}