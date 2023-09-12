using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace AP.ChevronCoop.Commons;

public static class StringHelper
{

    public static string GenerateApplicationNumber()
    {
        return Guid.NewGuid().ToString().GetHashCode().ToString("x").ToUpper().ToString();
    }
    public static string GenerateRandomString(int length)
    {
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        RNGCryptoServiceProvider rng = new();
        var result = new StringBuilder();
        var buffer = new byte[4];

        while (result.Length < length)
        {
            rng.GetBytes(buffer);
            var randomInt = BitConverter.ToUInt32(buffer, 0);
            var idx = randomInt % allowedChars.Length;

            result.Append(allowedChars[(int)idx]);
        }

        return result.ToString();
    }
    public static string GenerateRandomNumberOTP(int length)
    {
        const string allowedChars = "1234567890";
        var rng = new RNGCryptoServiceProvider();
        var result = new StringBuilder();
        var buffer = new byte[4];

        while (result.Length < length)
        {
            rng.GetBytes(buffer);
            var randomInt = BitConverter.ToUInt32(buffer, 0);
            var idx = randomInt % allowedChars.Length;

            result.Append(allowedChars[(int)idx]);
        }

        return result.ToString();
    }
    public static string ToJsonString(this object data)
    {
        try
        {
            if (data.GetType() == typeof(string))
            {
                return (string)data;
            }
            // params JsonConverter[] converters

            return JsonSerializer.Serialize(data);
        }
        catch { return ""; }

    }
    public static string Mocked_Nhilo_GetNextKey(int length)
    {
        //To be replaced by Nhilo Key generator

        const string allowedChars = "1234567890";
        var rng = new RNGCryptoServiceProvider();
        var result = new StringBuilder();
        var buffer = new byte[4];

        while (result.Length < length)
        {
            rng.GetBytes(buffer);
            var randomInt = BitConverter.ToUInt32(buffer, 0);
            var idx = randomInt % allowedChars.Length;

            result.Append(allowedChars[(int)idx]);
        }

        return result.ToString();
    }

}