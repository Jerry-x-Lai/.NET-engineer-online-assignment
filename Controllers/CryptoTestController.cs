using CryptoInfoApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoTestController : ControllerBase
    {
        [HttpPost("aes/encrypt")]
        public IActionResult AesEncrypt([FromQuery] string key, [FromBody] string plainText)
        {
            var cipher = AesEncryptionHelper.Encrypt(plainText, key);
            return Ok(cipher);
        }

        [HttpPost("aes/decrypt")]
        public IActionResult AesDecrypt([FromQuery] string key, [FromBody] string cipherText)
        {
            var plain = AesEncryptionHelper.Decrypt(cipherText, key);
            return Ok(plain);
        }

        [HttpGet("rsa/generate")]
        public IActionResult RsaGenerate()
        {
            var (pub, priv) = RsaEncryptionHelper.GenerateKeys();
            return Ok(new { publicKey = pub, privateKey = priv });
        }

        [HttpPost("rsa/encrypt")]
        public IActionResult RsaEncrypt([FromQuery] string publicKey, [FromBody] string plainText)
        {
            var cipher = RsaEncryptionHelper.Encrypt(plainText, publicKey);
            return Ok(cipher);
        }

        [HttpPost("rsa/decrypt")]
        public IActionResult RsaDecrypt([FromQuery] string privateKey, [FromBody] string cipherText)
        {
            var plain = RsaEncryptionHelper.Decrypt(cipherText, privateKey);
            return Ok(plain);
        }
    }
}
