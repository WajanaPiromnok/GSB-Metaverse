using System;
using System.Text;
using System.Security.Cryptography;

using UnityEngine;

public class Header
{
    public string alg;
    public string typ;
}

public class JWT
{
    public static string GetJwtUserClass(string claimset)
    {
        // header
        Header header = new Header();
        header.alg = "HS256";
        header.typ = "JWT";

        // encoded header
        var headerSerialized = JsonUtility.ToJson(header);
        var headerBytes = Encoding.UTF8.GetBytes(headerSerialized);
        var headerEncoded = Base64UrlEncode(headerBytes);

        // encoded claimset
        var claimsetSerialized = claimset;//JsonUtility.ToJson(claimset);
        var claimsetBytes = Encoding.UTF8.GetBytes(claimsetSerialized);
        var claimsetEncoded = Base64UrlEncode(claimsetBytes);

        // input
        var input = headerEncoded + "." + claimsetEncoded;
        var messageBytes = Encoding.UTF8.GetBytes(input);

        // certificate
        string client_secret = "JtqtDoS7jNBeqDpHIjm}3afdo4tBuBnV7{O5c+.Wvb?_zyJaM/?1qQc&/h";

        var encoding = new System.Text.ASCIIEncoding();

        byte[] keyByte = encoding.GetBytes(client_secret);

        var hmacsha256 = new HMACSHA256(keyByte);

        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

        var signatureEncoded = Base64UrlEncode(hashmessage);

        //var signatureBytes = hmac.SignData(inputBytes, "HS256");

        // jwt
        var jwt = headerEncoded + "." + claimsetEncoded + "." + signatureEncoded;

        return jwt;
    }
    /*
    public static string GetJwtUserSave(SendItem claimset)
    {
        // header
        Header header = new Header();
        header.alg = "HS256";
        header.typ = "JWT";

        // encoded header
        var headerSerialized = JsonUtility.ToJson(header);
        var headerBytes = Encoding.UTF8.GetBytes(headerSerialized);
        var headerEncoded = Base64UrlEncode(headerBytes);

        // encoded claimset
        var claimsetSerialized = JsonUtility.ToJson(claimset);
        var claimsetBytes = Encoding.UTF8.GetBytes(claimsetSerialized);
        var claimsetEncoded = Base64UrlEncode(claimsetBytes);

        // input
        var input = headerEncoded + "." + claimsetEncoded;
        var messageBytes = Encoding.UTF8.GetBytes(input);

        // certificate
        string client_secret = "ea3dc08d-9af4-4cd2-9833-1a1092e93f87";

        var encoding = new System.Text.ASCIIEncoding();

        byte[] keyByte = encoding.GetBytes(client_secret);

        var hmacsha256 = new HMACSHA256(keyByte);

        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

        var signatureEncoded = Base64UrlEncode(hashmessage);

        //var signatureBytes = hmac.SignData(inputBytes, "HS256");

        // jwt
        var jwt = headerEncoded + "." + claimsetEncoded + "." + signatureEncoded;

        return jwt;
    }

    public static string GetJwtAchievement(Achievement claimset)
    {
        // header
        Header header = new Header();
        header.alg = "HS256";
        header.typ = "JWT";

        // encoded header
        var headerSerialized = JsonUtility.ToJson(header);
        var headerBytes = Encoding.UTF8.GetBytes(headerSerialized);
        var headerEncoded = Base64UrlEncode(headerBytes);

        // encoded claimset
        var claimsetSerialized = JsonUtility.ToJson(claimset);
        var claimsetBytes = Encoding.UTF8.GetBytes(claimsetSerialized);
        var claimsetEncoded = Base64UrlEncode(claimsetBytes);

        // input
        var input = headerEncoded + "." + claimsetEncoded;
        var messageBytes = Encoding.UTF8.GetBytes(input);

        // certificate
        string client_secret = "ea3dc08d-9af4-4cd2-9833-1a1092e93f87";


        var encoding = new System.Text.ASCIIEncoding();

        byte[] keyByte = encoding.GetBytes(client_secret);

        var hmacsha256 = new HMACSHA256(keyByte);

        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

        var signatureEncoded = Base64UrlEncode(hashmessage);

        //var signatureBytes = hmac.SignData(inputBytes, "HS256");

        // jwt
        var jwt = headerEncoded + "." + claimsetEncoded + "." + signatureEncoded;

        return jwt;
    }

    public static string GetJwt(Submit claimset)
    {
        // header
        Header header = new Header();
        header.alg = "HS256";
        header.typ = "JWT";

        // encoded header
        var headerSerialized = JsonUtility.ToJson(header);
        var headerBytes = Encoding.UTF8.GetBytes(headerSerialized);
        var headerEncoded = Base64UrlEncode(headerBytes);

        // encoded claimset
        var claimsetSerialized = JsonUtility.ToJson(claimset);
        var claimsetBytes = Encoding.UTF8.GetBytes(claimsetSerialized);
        var claimsetEncoded = Base64UrlEncode(claimsetBytes);

        // input
        var input = headerEncoded + "." + claimsetEncoded;
        var messageBytes = Encoding.UTF8.GetBytes(input);

        // certificate
        string client_secret = "ea3dc08d-9af4-4cd2-9833-1a1092e93f87";


        var encoding = new System.Text.ASCIIEncoding();

        byte[] keyByte = encoding.GetBytes(client_secret);

        var hmacsha256 = new HMACSHA256(keyByte);

        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

        var signatureEncoded = Base64UrlEncode(hashmessage);

        //var signatureBytes = hmac.SignData(inputBytes, "HS256");

        // jwt
        var jwt = headerEncoded + "." + claimsetEncoded + "." + signatureEncoded;

        return jwt;
    }*/

    // from JWT spec
    private static string Base64UrlEncode(byte[] input)
    {
        var output = Convert.ToBase64String(input);
        output = output.Split('=')[0]; // Remove any trailing '='s
        output = output.Replace('+', '-'); // 62nd char of encoding
        output = output.Replace('/', '_'); // 63rd char of encoding
        return output;
    }

    // from JWT spec
    public static byte[] Base64UrlDecode(string input)
    {
        var output = input;
        output = output.Replace('-', '+'); // 62nd char of encoding
        output = output.Replace('_', '/'); // 63rd char of encoding
        switch (output.Length % 4) // Pad with trailing '='s
        {
            case 0: break; // No pad chars in this case
            case 2: output += "=="; break; // Two pad chars
            case 3: output += "="; break; // One pad char
            default: throw new System.Exception("Illegal base64url string!");
        }
        var converted = Convert.FromBase64String(output); // Standard base64 decoder
        return converted;
    }

    public static int[] GetExpiryAndIssueDate()
    {
        var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        DateTime nowInThailand = DateTime.UtcNow;

        var iat = (int)nowInThailand.Subtract(utc0).TotalSeconds + 25200;
        var exp = (int)nowInThailand.AddMinutes(55).Subtract(utc0).TotalSeconds;

        return new[] { iat, exp };
    }
}