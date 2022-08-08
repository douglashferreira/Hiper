using Hiper.View.Forms;
using Hiper.View.Gateway;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Shapes;

namespace Hiper.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly ClientGateway _clientGateway = new ClientGateway();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCadCliente_Click(object sender, RoutedEventArgs e)
        {
            frmClient frm = new frmClient();
            frm.Owner = this;
            frm.ShowDialog();

        }

        private void btnImpCliente_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\Douglas\source\Hiper\Hiper.View";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {   
                string path = openFileDialog.FileName;// @"C:\Users\Douglas\source\Hiper\Hiper.View\teste.csv";
                _clientGateway.ImportarClientes(path);
            }
        }

        private void btnConsumeCliente_Click(object sender, RoutedEventArgs e)
        {
            _clientGateway.ComsumirClientes();
        }
    }
}
