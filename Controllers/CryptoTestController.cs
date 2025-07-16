using CryptoInfoApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfoApi.Controllers
{
    /// <summary>
    /// 加解密測試 API，提供 AES/RSA 加解密與金鑰產生。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoTestController : ControllerBase
    {
        /// <summary>
        /// 使用 AES 加密明文。
        /// </summary>
        /// <param name="key">加密金鑰</param>
        /// <param name="plainText">明文</param>
        /// <returns>加密後字串</returns>
        [HttpPost("aes/encrypt")]
        public IActionResult AesEncrypt([FromQuery] string key, [FromBody] string plainText)
        {
            var cipher = AesEncryptionHelper.Encrypt(plainText, key);
            return Ok(cipher);
        }

        /// <summary>
        /// 使用 AES 解密密文。
        /// </summary>
        /// <param name="key">加密金鑰</param>
        /// <param name="cipherText">密文</param>
        /// <returns>解密後明文</returns>
        [HttpPost("aes/decrypt")]
        public IActionResult AesDecrypt([FromQuery] string key, [FromBody] string cipherText)
        {
            var plain = AesEncryptionHelper.Decrypt(cipherText, key);
            return Ok(plain);
        }

        /// <summary>
        /// 產生一組 RSA 公私鑰。
        /// </summary>
        /// <returns>RSA 公私鑰</returns>
        [HttpGet("rsa/generate")]
        public IActionResult RsaGenerate()
        {
            var (pub, priv) = RsaEncryptionHelper.GenerateKeys();
            return Ok(new { publicKey = pub, privateKey = priv });
        }

        /// <summary>
        /// 使用 RSA 公鑰加密明文。
        /// </summary>
        /// <param name="publicKey">RSA 公鑰</param>
        /// <param name="plainText">明文</param>
        /// <returns>加密後字串</returns>
        [HttpPost("rsa/encrypt")]
        public IActionResult RsaEncrypt([FromQuery] string publicKey, [FromBody] string plainText)
        {
            var cipher = RsaEncryptionHelper.Encrypt(plainText, publicKey);
            return Ok(cipher);
        }

        /// <summary>
        /// 使用 RSA 私鑰解密密文。
        /// </summary>
        /// <param name="privateKey">RSA 私鑰</param>
        /// <param name="cipherText">密文</param>
        /// <returns>解密後明文</returns>
        [HttpPost("rsa/decrypt")]
        public IActionResult RsaDecrypt([FromQuery] string privateKey, [FromBody] string cipherText)
        {
            var plain = RsaEncryptionHelper.Decrypt(cipherText, privateKey);
            return Ok(plain);
        }
    }
}
