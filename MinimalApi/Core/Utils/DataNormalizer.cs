namespace MinimalApi.Core.Utils;

public static class DataNormalizer
{
    public static string NormalizePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return phone;
        
        var digits = new string(phone.Where(char.IsDigit).ToArray());
        
        if ((digits.StartsWith("8") || digits.StartsWith("7")) && digits.Length == 11)
            return digits[1..]; 

        return digits;
    }
}