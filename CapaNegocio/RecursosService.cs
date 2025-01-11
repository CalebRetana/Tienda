using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace CapaNegocio
{
    public class RecursosService
    {

        public static string generarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
            
        }

        public static string Encriptar256(string texto)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();

        }
        
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                // Configuración del correo a enviar
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("retanacaleb29@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;
                // Configuración del servidor Gmail
                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("retanacaleb29@gmail.com", "jnvy vxxd ztfh ggyr"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex) { 
                resultado = false;
            }
            return resultado;
        }
    }
}
