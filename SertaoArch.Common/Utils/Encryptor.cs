namespace SertaoArch.Common.Utils
{
    public static class Encryptor
    {
        public static string Encrypt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            var encrypted = System.Convert.ToBase64String(bytes);
            return encrypted;
        }
        public static string Decrypt(this string encryptedValue)
        {
            if (string.IsNullOrEmpty(encryptedValue))
                return encryptedValue;
           
            var bytes = System.Convert.FromBase64String(encryptedValue);
            var decrypted = System.Text.Encoding.UTF8.GetString(bytes);
            return decrypted;
        }
    }
}
