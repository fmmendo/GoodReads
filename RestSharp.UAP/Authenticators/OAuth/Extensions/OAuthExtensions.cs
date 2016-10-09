namespace RestSharp.Authenticators.OAuth.Extensions
{
    internal static class OAuthExtensions
    {
        public static string ToRequestValue(this OAuthSignatureMethod signatureMethod)
        {
            var value = signatureMethod.ToString().ToUpper();
            var shaIndex = value.IndexOf("SHA1");
            return shaIndex > -1 ? value.Insert(shaIndex, "-") : value;
        }

        public static OAuthSignatureMethod FromRequestValue(this string signatureMethod)
        {
            switch (signatureMethod)
            {
                case "HMAC-SHA1":
                    return OAuthSignatureMethod.HmacSha1;
                case "RSA-SHA1":
                    return OAuthSignatureMethod.RsaSha1;
                default:
                    return OAuthSignatureMethod.PlainText;
            }
        }
    }
}