using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_document_editor
{
    public class DocumentManager
    {
        private List<IDocument> documents = new List<IDocument>();

        public void CreateDocument(IDocument document, string path)
        {
            document.Create(path);
            documents.Add(document);
        }

        public void OpenDocument(IDocument document, string path)
        {
            document.Open(path);
            documents.Add(document);
        }

        public void SaveDocument(IDocument document, string path)
        {
            document.Save(path);
        }

        public void SaveDocumentAs(IDocument document, string newPath)
        {
            document.SaveAs(newPath);
        }

        public void PrintDocument(IDocument document)
        {
            document.Print();
        }

        public void CloseDocument(IDocument document)
        {
            document.Close();
            documents.Remove(document);
        }

        public IDocument GetDocumentByName(string name)
        {
            return documents.Find(doc => doc.Name == name);
        }
    }
}
