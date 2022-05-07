using System;
using System.Collections.Generic;
using System.Text;

namespace Zad1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                DateTime date = DateTime.Now;
                PrintCounter++;
                Console.WriteLine($"{date} Print: {document.GetFileName()}");
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            string sType;

            switch (formatType)
            {
                case IDocument.FormatType.TXT:
                    sType = "text";
                    break;
                case IDocument.FormatType.PDF:
                    sType = "pdf";
                    break;
                default:
                    sType = "jpg";
                    break;
            }

            string name = string.Format("{0}Scan{1}.{2}", sType, ScanCounter + 1, formatType.ToString().ToLower());

            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(name);
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(name);
            else
                document = new TextDocument(name);

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{DateTime.Now:dd.mm.yyyy hh:mm:ss} Scan: {document.GetFileName()}");
            }
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument doc);
            Print(doc);
        }
    }
}