using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_document_editor
{
    public interface IDocument
    {
        string Name { get; set; }
        void Create(string path);
        void Open(string path);
        void Save(string path);
        void SaveAs(string newPath);
        void Print();
        void Close();
    }
}
