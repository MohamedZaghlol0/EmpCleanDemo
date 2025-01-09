namespace MyAppApplication.Shared.Contracts;

public interface ILocalizationService
{
    public string GetLocalizedString(string key, string? language = null);
}