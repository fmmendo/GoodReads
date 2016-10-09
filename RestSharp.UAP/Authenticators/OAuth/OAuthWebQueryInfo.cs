namespace RestSharp.Authenticators.OAuth
{
    public sealed class OAuthWebQueryInfo
    {
        public string ConsumerKey { get; set; }
        public string Token { get; set; }
        public string Nonce { get; set; }
        public string Timestamp { get; set; }
        public string SignatureMethod { get; set; }
        public string Signature { get; set; }
        public string Version { get; set; }
        public string Callback { get; set; }
        public string Verifier { get; set; }
        public string ClientMode { get; set; }
        public string ClientUsername { get; set; }
        public string ClientPassword { get; set; }
        public string UserAgent { get; set; }
        public string WebMethod { get; set; }
        public OAuthParameterHandling ParameterHandling { get; set; }
        public OAuthSignatureTreatment SignatureTreatment { get; set; }
        internal string ConsumerSecret { get; set; }
        internal string TokenSecret { get; set; }
    }
}