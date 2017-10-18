namespace JCE.Utils.Encrypts.Internal
{
    /// <summary>
    /// AES秘钥
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class AESKey
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 秘钥偏移量
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string IV { get; set; }
    }
}
