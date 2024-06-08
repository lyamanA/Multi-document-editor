using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_document_editor
{
    public abstract class DocumentBase : IDocument
    {
        public string Name { get; set; }

        public abstract void Create(string path);
        public abstract void Open(string path);
        public abstract void Save(string path);
        public abstract void SaveAs(string newPath);
        public abstract void Print();
        public abstract void Close();
    }
}
