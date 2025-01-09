using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace MyAppPresentaionAPI
{
    public class LocalizationService
    {
        private readonly string _resourcesPath;
        private readonly string _defaultLanguage;

        public LocalizationService(string resourcesPath, string defaultLanguage = "en")
        {
            _resourcesPath = resourcesPath;
            _defaultLanguage = defaultLanguage;
        }

        public string GetLocalizedString(string key, string language = null)
        {
            language ??= _defaultLanguage;
            var filePath = Path.Combine(_resourcesPath, $"{language}.json");

            if (!File.Exists(filePath))
            {
                // Log the error and return a fallback message
                Console.WriteLine($"Localization file not found: {filePath}");
                return $"Localization file for '{language}' not found.";
            }

            var jsonContent = File.ReadAllText(filePath);
            var localizedStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);

            return localizedStrings != null && localizedStrings.TryGetValue(key, out var value) ? value : key;
        }

    }
}
