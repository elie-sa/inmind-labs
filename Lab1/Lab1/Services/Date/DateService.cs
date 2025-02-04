using System.Globalization;

namespace Lab1.Services.Date;

// Question 7 gave us only 3 Accepted-Language Headers however the input validation part
// demanded that we allow all accepted languages so I returned the date whenever the language is accepted
// tested on postman, on swagger the language was not supported
// Console.WriteLine($"Received language: {language}");
// found out that swagger uses a comma separated language preference list so i split it and used the first part
public class DateService : IDateService
{
    public string GetFormattedDate(string language)
    {

        if (string.IsNullOrEmpty(language))
        {
            throw new ArgumentNullException("Accept-Language header is missing");
        }
        
        language = language.Split(',')[0].Trim();
        // only needed for swagger RFC 9110 standard

        var allCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        bool isValidLanguage = allCultures.Any(c => c.Name == language);

        if (!isValidLanguage)
        {
            throw new NotSupportedException("The language provided is not supported.");
        }
        // tested it by providing a non-supported language on postman

        var cultureInfo = new CultureInfo(language);
        return DateTime.Now.ToString("D", cultureInfo);
    }
}
