using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Multi_document_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DocumentManager documentManager;
        private IDocumentFactory documentFactory;

        private string currentFilePath = "";
        private bool isFileSaved;
        public MainWindow()
        {
            InitializeComponent();
            documentManager = new DocumentManager();
            //documentFactory = new TextDocumentFactory();
            documentFactory = new TextDocumentFactory(this);
            isFileSaved = false;
        }

        private void CreateDocument_Click(object sender, RoutedEventArgs e)
        {
            var document = documentFactory.CreateDocument();
            document.Name = "Новый текстовый документ";
            documentManager.CreateDocument(document, "new_document.txt");

            var tabItem = new TabItem
            {
                Header = document.Name,
                Content = new TextBox { Text = "" }
            };

            DocumentsTabControl.Items.Add(tabItem);
            DocumentsTabControl.SelectedItem = tabItem;
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var document = documentFactory.CreateDocument();
                documentManager.OpenDocument(document, openFileDialog.FileName);

                var tabItem = new TabItem
                {
                    Header = document.Name,
                    Content = new TextBox { Text = System.IO.File.ReadAllText(openFileDialog.FileName) }
                };

                DocumentsTabControl.Items.Add(tabItem);
                DocumentsTabControl.SelectedItem = tabItem;
            }

        }

        private void SaveDocument_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            if (!isFileSaved || string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileAs();
            }
            else
            {
                if (DocumentsTabControl.SelectedItem is TabItem tabItem)
                {
                    var document = documentManager.GetDocumentByName((string)tabItem.Header);
                    var textBox = tabItem.Content as TextBox;
                    if (textBox != null && document is TextDocument textDocument)
                    {
                        textDocument.Content = textBox.Text;
                        documentManager.SaveDocument(textDocument, currentFilePath);
                        isFileSaved = true;
                    }
                }
            }
        }

        private void PrintDocument_Click(object sender, RoutedEventArgs e)
        {
            if (DocumentsTabControl.SelectedItem is TabItem tabItem)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    var document = documentFactory.CreateDocument();
                    document.Name = saveFileDialog.FileName;
                    var textBox = tabItem.Content as TextBox;

                    if (textBox != null)
                    {
                        documentManager.SaveDocumentAs(document, saveFileDialog.FileName);
                        System.IO.File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                        tabItem.Header = document.Name;
                    }
                }
            }

        }

        private void CloseDocument_Click(object sender, RoutedEventArgs e)
        {
            if (DocumentsTabControl.SelectedItem is TabItem tabItem)
            {
                var document = documentFactory.CreateDocument();
                document.Name = (string)tabItem.Header;

                documentManager.CloseDocument(document);
                DocumentsTabControl.Items.Remove(tabItem);
            }

        }

        private void SaveDocumentAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }

        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (DocumentsTabControl.SelectedItem is TabItem tabItem)
                {
                    var document = documentManager.GetDocumentByName((string)tabItem.Header);
                    var textBox = tabItem.Content as TextBox;
                    if (textBox != null && document is TextDocument textDocument)
                    {
                        textDocument.Content = textBox.Text;
                        documentManager.SaveDocumentAs(textDocument, saveFileDialog.FileName);
                        currentFilePath = saveFileDialog.FileName;
                        isFileSaved = true;
                        tabItem.Header = System.IO.Path.GetFileName(currentFilePath);
                    }
                }
            }
        }



    }
}