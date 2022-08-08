using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using Hiper.Domain.Utils;
using Hiper.View.Gateway;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Hiper.View.Forms
{
    /// <summary>
    /// Interaction logic for frmClientAlter.xaml
    /// </summary>
    public partial class frmClientAlter : Window
    {
        public readonly ClientGateway _clientGateway = new ClientGateway();
        public ClientDTO ClientBackup;
        public ClientDTO Client;

        frmClient formPai;
        public frmClientAlter()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            if(ClientBackup != null && ClientBackup.Id > 0)
            {
                formPai.lblStatus.Content = "Alteração cancelada.";
                formPai.lblStatus.Foreground = Brushes.Red;
            }
            Close();
        }

        private async void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            ClientDTO retorno;
            if (Client != null)
            {
                if (Client.Id == 0)
                    retorno = await _clientGateway.Add(Client);
                else
                    retorno = await _clientGateway.Alter(Client);

                if (ClientBackup != null)
                {
                    var client = formPai.clientList.FirstOrDefault(f => f.Id == ClientBackup.Id);
                    Functions.CopyPropertyValues(retorno, client);
                    formPai.lblStatus.Content = "Alteração efetuada.";
                    formPai.lblStatus.Foreground = Brushes.Blue;
                }
                else
                {
                    formPai.clientList.Add(retorno);
                    formPai.lblStatus.Content = "Inclusão efetuada.";
                    formPai.lblStatus.Foreground = Brushes.Blue;
                }
                formPai.clientList = formPai.clientList.OrderBy(o => o.Id).ToList();
                formPai.grdClientes.ItemsSource = null;
                formPai.grdClientes.ItemsSource = formPai.clientList;
                formPai.grdClientes.SelectedItem = retorno;
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            formPai = (frmClient)Owner;
            Client = new ClientDTO();
            Client.State = "SP";
            Client.Country = "Brasil";
            Client.Birthday = DateTime.Today;
            if (ClientBackup != null)
                Functions.CopyPropertyValues(ClientBackup, Client);
            ClientWindow.DataContext = Client;
        }
    }
}
