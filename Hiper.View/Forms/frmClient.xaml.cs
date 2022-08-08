using Hiper.Domain.Converters;
using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using Hiper.Domain.Enums;
using Hiper.Domain.ValueObjects;
using Hiper.View.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace Hiper.View.Forms
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class frmClient : Window
    {
        public readonly ClientGateway _clientGateway = new ClientGateway();
        public List<ClientDTO> clientList = new List<ClientDTO>();
        public frmClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frmClientAlter frm = new frmClientAlter();
            frm.ClientBackup = null;
            frm.Owner = this;
            frm.Show();
        }

        private async void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            string document = "";
            int id = 0;
            string name = "";

            if (!string.IsNullOrEmpty(txtPesquisa.Text) && cbTipoPesquisa.SelectedIndex == 0)
                name = txtPesquisa.Text;
            else if (!string.IsNullOrEmpty(txtPesquisa.Text) && cbTipoPesquisa.SelectedIndex == 1)
                document = txtPesquisa.Text;
            else if (!string.IsNullOrEmpty(txtPesquisa.Text) && cbTipoPesquisa.SelectedIndex == 2)
            {
                try
                {
                    id = Convert.ToInt32(txtPesquisa.Text);
                }
                catch (Exception)
                {
                    lblStatus.Content = "Código inválido.";
                    lblStatus.Foreground = Brushes.Red;
                    return;
                }
            }
            clientList = await _clientGateway.GetAll(new ClientFilterDTO() { document = document, id = id, name = name });


            if (!clientList.Any())
            {
                lblStatus.Content = "Nenhum Cliente localizado.";
                lblStatus.Foreground = Brushes.Red;
            }
            lblStatus.Content = "Pesquisa concluida.";
            lblStatus.Foreground = Brushes.Blue;
            grdClientes.ItemsSource = clientList;
        }

        private async void btnAlterarGrid_Click(object sender, MouseButtonEventArgs e)
        {
            var dc = (sender as Image).DataContext;

            if (dc != null)
                grdClientes.SelectedItem = dc;

            var cliAlter = (ClientDTO)grdClientes.SelectedItem;

            frmClientAlter frm = new frmClientAlter();
            frm.ClientBackup = cliAlter;
            frm.Owner = this;
            frm.Show();
        }

        private async void btnExcluirGrid_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dc = (sender as Image).DataContext;

                if (dc != null)
                    grdClientes.SelectedItem = dc;

                var cliRemove = (ClientDTO)grdClientes.SelectedItem;
                var retorno = await _clientGateway.Remove(cliRemove);
                clientList.Remove(cliRemove);
                grdClientes.ItemsSource = null;
                grdClientes.ItemsSource = clientList;
                grdClientes.SelectedItem = retorno;

                lblStatus.Content = "Exclusão efetuada.";
                lblStatus.Foreground = Brushes.Blue;
            }
            catch (Exception)
            {
                lblStatus.Content = "Erro ao excluir.";
                lblStatus.Foreground = Brushes.Red;
                throw;
            }
        }

        private async Task GenerateExcel()
        {
            if(clientList!= null && clientList.Any())
                await _clientGateway.GenerateExcel(clientList);
        }

        private async void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            await GenerateExcel();
        }
    }
}
