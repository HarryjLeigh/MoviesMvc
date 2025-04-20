using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Recipes.Validation;

[AttributeUsage(AttributeTargets.Property |
                AttributeTargets.Field |
                AttributeTargets.Parameter)]
public class AllowedWordsOnlyAttribute : ValidationAttribute
{
    private readonly string[] _allowedWords;

    public AllowedWordsOnlyAttribute(params string[] allowedWords)
    {
        if (allowedWords == null || allowedWords.Length == 0)
            throw new ArgumentException("You must supply at least one allowed word.");

        _allowedWords = allowedWords
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .Select(w => w.Trim().ToLowerInvariant()).ToArray();
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var input = value?.ToString()?.Trim().ToLower();
        if (input != null && !_allowedWords.Contains(input))
            return new ValidationResult(
                $"Only the following words are allowed: {string.Join(", ", ArrayToTitleCase(_allowedWords))}");
        return ValidationResult.Success!;
    }

    private string[] ArrayToTitleCase(string[] phrases)
    {
        var capitaliseAllowedWords = new List<string>();
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        for (var i = 0; i < phrases.Length; i++) capitaliseAllowedWords.Add(textInfo.ToTitleCase(phrases[i]));
        return capitaliseAllowedWords.ToArray();
    }
}