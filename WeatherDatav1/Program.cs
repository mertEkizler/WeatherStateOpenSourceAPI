using System.Net;
using System.Xml.Linq;

var appId = "341cba523364a6bbdb0e5dddf4f909b2";
var city = "izmir";

if (string.IsNullOrEmpty(city))
{
    Console.WriteLine("City can not be null or empty.");
}

if (string.IsNullOrEmpty(appId))
{
    Console.WriteLine("App ID can not be null or empty");
}

try
{
    var connection = $"https://api.openweathermap.org/data/2.5/weather?q={city}&mode=xml&lang=tr&units=metric&appid={appId}";

    XDocument doc = XDocument.Load(connection);

    var temp = doc.Descendants("temperature")?.ElementAt(0)?.Attribute("value")?.Value;
    var weatherState = doc.Descendants("weather")?.ElementAt(0)?.Attribute("value")?.Value;

    if (temp != null && weatherState != null)
    {
        Console.WriteLine($"The temperature for {city} is {temp} degrees and {weatherState}");
        Console.ReadLine();
    }
    else
    {
        throw new ArgumentNullException($"Temp and weatherState should be provide.");
    }
}
catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
{
    Console.WriteLine($"XML File could not be found");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed, reason : {ex.Message}");
    throw;
}