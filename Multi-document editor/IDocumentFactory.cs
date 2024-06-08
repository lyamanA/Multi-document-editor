using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_document_editor
{
    public interface IDocumentFactory
    { 
        IDocument CreateDocument();
    }

    public class TextDocumentFactory : IDocumentFactory
    {
        private readonly MainWindow mainWindow;

        public TextDocumentFactory(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public IDocument CreateDocument()
        {
            return new TextDocument();
        }

    }
}
