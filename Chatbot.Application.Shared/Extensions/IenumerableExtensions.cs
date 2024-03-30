namespace Chatbot.Application.Shared.Extensions
{
    public class Enumeration<TValue>
    {
        public TValue Value { get; set; }
        public int index { get; set; }
    }
    public static class IenumerableExtensions
    {

        public static IEnumerable<Enumeration<TValue>> Enumerate<TValue>(this IEnumerable<TValue> values)
        {
            return values.Select((v, i) => new Enumeration<TValue>
            {
                Value = v,
                index = i
            });
        }

    }
}
