namespace Codoxide
{
    public delegate void OutAction<T>(out T output);

    public delegate void OutAction<T, U>(T input, out U output);

    public delegate ResultType OutFunc<OutType, ResultType>(out OutType output);

    public delegate ResultType ParameterziedOutFunc<InputType, OutputType, ResultType>(InputType input, out OutputType output);
}
