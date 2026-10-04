using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using QRCoder;

namespace CityTransport.Utils.TicketUtils
{
    public class QRCodeGeneratorService
    {
        // Для AES-256 довжина ключа повинна бути рівно 32 байти (символи)
        private  readonly byte[] _encryptionKey;

        public QRCodeGeneratorService(IOptions<CryptoSettings> options)
        {
            var encryptionKey = options.Value.EncryptionKey;
            if (encryptionKey.Length != 32)
            {
                throw new ArgumentException("Encryption key must be 32 characters long for AES-256.");
            }
            _encryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
        }

        /// <summary>
        /// Генерує зашифрований payload (на основі унікального токена квитка/сесії) та створює Base64-рядок PNG зображення QR-коду.
        /// </summary>
        public string GenerateEncryptedQRCode(string ticketGuid)
        {
            string rawPayload = $"{ticketGuid}|{DateTime.UtcNow:O}";

            string encryptedPayload = EncryptString(rawPayload, _encryptionKey);

            using (var qrGenerator = new QRCoder.QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(encryptedPayload, QRCoder.QRCodeGenerator.ECCLevel.Q);
                var qrCode = new PngByteQRCode(qrCodeData);
                var qrCodeBytes = qrCode.GetGraphic(20);

                return Convert.ToBase64String(qrCodeBytes);
            }
        }

        /// <summary>
        /// Симетричне шифрування AES
        /// </summary>
        private string EncryptString(string plainText, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var msEncrypt = new MemoryStream();

            msEncrypt.Write(aes.IV, 0, aes.IV.Length);

            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }
    }
}