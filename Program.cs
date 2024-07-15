using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;
using UbiqSecurity;

namespace UbiqMicroService
{
    // Example controller for encryption and decryption
    public class EncryptionController : ControllerBase
    {
        private readonly IUbiqCredentials _ubiqCredentials;

        public EncryptionController(IUbiqCredentials ubiqCredentials)
        {
            _ubiqCredentials = ubiqCredentials;
        }

        [HttpPost("encrypt")]
        public async Task<ActionResult<string>> Encrypt([FromBody] string plaintext)
        {
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedBytes = await UbiqEncrypt.EncryptAsync(_ubiqCredentials, plainBytes);
                string encryptedData = Convert.ToBase64String(encryptedBytes);
                return Ok(encryptedData);
            }
            catch (Exception ex)
            {
                return BadRequest($"Encryption failed: {ex.Message}");
            }
        }

        [HttpPost("decrypt")]
        public async Task<ActionResult<string>> Decrypt([FromBody] string encryptedData)
        {
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] plainBytes = await UbiqDecrypt.DecryptAsync(_ubiqCredentials, encryptedBytes);
                string plaintext = Encoding.UTF8.GetString(plainBytes);
                return Ok(plaintext);
            }
            catch (Exception ex)
            {
                return BadRequest($"Decryption failed: {ex.Message}");
            }
        }
    }

    // Main program entry point
    public class Program
    {
        private static IUbiqCredentials _ubiqCredentials;

        public static void Main(string[] args)
        {
            _ubiqCredentials = UbiqFactory.CreateCredentials(
                accessKeyId: "YourAccessKeyId",
                secretSigningKey: "YourSecretSigningKey",
                secretCryptoAccessKey: "YourSecretCryptoAccessKey"
            );

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IUbiqCredentials>(_ubiqCredentials);
                    services.AddControllers();
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
    }
}




