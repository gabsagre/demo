using System;
using System.IO;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

class Program
{
    static async Task Main()
    {
        //DeClaraciones de apis y correos
        var apiKey = ""; 
        var client = new SendGridClient(apiKey);
        var correoEmpresa = new EmailAddress("");
        var correoCliente = new EmailAddress("");

        var templateId = "d-c8155d4c864445bcb0b86ca07093986d";

        // Los datos que se insertarán en la plantilla
        var dynamicTemplateData = new
        {
            nombre = "Fernando",
            mensaje = "Has realizados las siguentes compras",
            producto1 = "Refresco",
            producto2 = "Fresas",
            producto3 = "Carnes"
        };

        var msg = MailHelper.CreateSingleTemplateEmail(correoEmpresa, correoCliente, templateId, dynamicTemplateData);
        //Pasar el documento de formato pdf a BASE64
        var filePath = "prueba.pdf"; 
        byte[] fileBytes = File.ReadAllBytes(filePath);
        string fileBase64 = Convert.ToBase64String(fileBytes);

        // Agregar el adjunto
        msg.AddAttachment("documento.pdf", fileBase64);

        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        // Opcional: manejar la respuesta del envío de correo
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(await response.Body.ReadAsStringAsync());
        Console.WriteLine(response.Headers.ToString());
    }
}
