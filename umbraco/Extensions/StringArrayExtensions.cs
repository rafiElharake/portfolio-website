using Examine;
using Examine.Search;

namespace umbraco.Extensions;

public static class StringArrayExtensions
{
    public static IExamineValue[]? Fuzzy(this string[] terms,float fuzziness=0.5f)
    {
        if (terms == null) return null;
        List<IExamineValue> values = [];
        foreach (string term in terms)
        {
            values.Add(term.Fuzzy(fuzziness));
        }
        return [.. values];
    }
    public static IExamineValue[]? Boost(this string[] terms, float boost)
    {
        if (terms == null) return null;
        List<IExamineValue> values = [];
        foreach (string term in terms)
        {
            values.Add(term.Boost(boost));
        }
        return [.. values];
    }
    public static IExamineValue[]? MultipleCharacterWildcard(this string[] terms)
    {
        if (terms == null) return null;

        List<IExamineValue> values = [];
        foreach (var item in terms)
        {
            values.Add(item.MultipleCharacterWildcard());
        }

        return [.. values];
    }

}