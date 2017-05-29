using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TESTINGFACT.CLASES
{
    public class NOTIFIACIONES
    {
        public bool NotificacionPorCorreo(string Correo, List<string> Archivos)
        {
            bool res = false;
            CONFIGURACIONES c = new CONFIGURACIONES();
            var fromAddress = new MailAddress(c.CorreoDeAdministracion.Item2, c.CorreoDeAdministracion.Item2);
            var toAddress = new MailAddress(Correo, "Cliente");
            string fromPassword = c.CorreoDeAdministracion.Item3;
            string subject = c.AsuntoDeFacturacion;
            string body = "Le anexo su factura electrónica. Gracias por su preferencia.";
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    foreach (var bcc in c.CorreosDeSupervision.Where(x => !string.IsNullOrEmpty(x)).ToList())
                        message.Bcc.Add(bcc);
                    foreach (var files in Archivos)
                        message.Attachments.Add(new Attachment(files));
                    smtp.Send(message);
                }
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
    }
}