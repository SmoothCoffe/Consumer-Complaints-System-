namespace consumer_complaints.Services
{
    public static class PhoneNormalizer
    {
        public static string Normalize(string? phone)
        {
            if (string.IsNullOrEmpty(phone)) return string.Empty;
            var digits = phone.Where(char.IsDigit).ToArray();
            return new string(digits);
        }
    }
}
