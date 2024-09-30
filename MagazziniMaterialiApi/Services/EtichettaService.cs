using iTextSharp.text;
using iTextSharp.text.pdf;
using MagazziniMaterialiAPI.Models.Entity;
using System.IO;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;

namespace MagazziniMaterialiAPI.Services
{
    public class EtichettaService
    {
        public byte[] GeneraEtichetta(Materiale materiale)
        {
            var immaginePrincipale = materiale.Immagini.FirstOrDefault(i => i.IsPrincipale);

            if (immaginePrincipale == null)
            {
                throw new Exception("Nessuna immagine principale trovata per il materiale.");
            }

            string qrCodeData = immaginePrincipale.QRCodeData;

            return GeneraEtichettaPDF(materiale.CodiceMateriale, materiale.Descrizione, immaginePrincipale.UrlImmagine, qrCodeData);
        }

        private byte[] GeneraEtichettaPDF(string codiceMateriale, string descrizione, string urlImmagine, string qrCodeData)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                if (!string.IsNullOrEmpty(urlImmagine))
                {
                    Image img = Image.GetInstance(urlImmagine);
                    img.ScaleToFit(100f, 100f);
                    document.Add(img);
                }

                document.Add(new Paragraph("Codice Materiale: " + codiceMateriale));
                document.Add(new Paragraph("Descrizione: " + descrizione));

                BarcodeQRCode qrCode = new BarcodeQRCode(qrCodeData, 150, 150, null);
                Image qrCodeImage = qrCode.GetImage();
                document.Add(qrCodeImage);

                document.Close();
                return stream.ToArray();
            }
        }

        public byte[] GeneraEtichettaMinimale(Materiale materiale)
        {
            var immaginePrincipale = materiale.Immagini.FirstOrDefault(i => i.IsPrincipale);

            if (immaginePrincipale == null)
            {
                throw new Exception("Nessuna immagine principale trovata per il materiale.");
            }

            string qrCodeData = immaginePrincipale.QRCodeData;

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                BarcodeQRCode qrCode = new BarcodeQRCode(qrCodeData, 150, 150, null);
                Image qrCodeImage = qrCode.GetImage();
                document.Add(qrCodeImage);

                document.Close();
                return stream.ToArray();
            }
        }
    }
}
