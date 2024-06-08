using System.IO;

using System.Windows.Controls;
using System.Windows.Documents;

namespace Multi_document_editor
{
    public class TextDocument : DocumentBase
    {
        private string content;
       
        public TextDocument()
        {
            content = string.Empty;
        }

        public string Content
        {
            get => content;
            set => content = value;
        }

        public override void Create(string path)
        {
            content = ""; // Начальное содержание документа
            File.WriteAllText(path, content);
            Name = Path.GetFileName(path);
           
        }

        public override void Open(string path)
        {
            if (File.Exists(path))
            {
                content = File.ReadAllText(path);
                Name = Path.GetFileName(path);
            }
            
        }

        public override void Save(string path)
        {
            File.WriteAllText(path, content);
           
        }

        public override void SaveAs(string newPath)
        {
            File.WriteAllText(newPath, content);
            Name = Path.GetFileName(newPath);
           
        }

        public override void Print()
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                FlowDocument doc = new FlowDocument(new Paragraph(new Run(content)));
                IDocumentPaginatorSource idpSource = doc;
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Printing document");
            }
        }

        
        public override void Close()
        {
            content = null;
            Name = null;
        }

    }
}
