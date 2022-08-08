using Hiper.Domain.DTO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Hiper.View.HttpClients
{
    public class HttpPatternAPI
    {
        private static Lazy<HttpPatternAPI> _Lazy = new Lazy<HttpPatternAPI>(() => new HttpPatternAPI());
        private readonly HttpClient _HttpClient;

        public static HttpPatternAPI Current { get => _Lazy.Value; }

        private HttpPatternAPI()
        {
            if (String.IsNullOrEmpty(ServiceConfiguration.ServicePath) || String.IsNullOrWhiteSpace(ServiceConfiguration.ServicePath))
                throw new Exception("Caminho do serviço não informado.");

            _HttpClient = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 5, 0, 0),
                BaseAddress = new Uri($"{ServiceConfiguration.ServicePath}")
            };
        }

        public HttpRequestMessage ReturnInstanceByTypeRequest<URequisicao>(String addressAPI, URequisicao requisition, HttpMethod requisitionTypeHttp) where URequisicao : class
        {
            StringContent contentString = null;

            if (requisition != null)
            {
                var jsonContent = JsonSerializer.Serialize(requisition);
                contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }
            else
            {
                contentString = null;
            }

            return new HttpRequestMessage
            {
                Method = requisitionTypeHttp,
                RequestUri = new Uri($"{ServiceConfiguration.ServicePath}{addressAPI}"),
                Content = contentString,
            };
        }

        public async Task<ReturnRequisitionAPIDTO<TRetorno>> MakeGetAPIRequest<TRetorno>(String addressAPI) where TRetorno : class
        {
            ReturnRequisitionAPIDTO<TRetorno> ReturnRequisitionAPIDTO = new ReturnRequisitionAPIDTO<TRetorno>();

            try
            {
                using (var response = await _HttpClient.GetAsync(addressAPI))
                {
                    string errorMessagerAPI;
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        throw new Exception(errorMessage);
                    }

                    if (response.Content == null)
                        throw new Exception($"Não foram efetuadas respostas na chamada do endereço {addressAPI}.");

                    var retornoAPI = await response.Content.ReadAsStringAsync();
                    ReturnRequisitionAPIDTO.returnRequisitionAPI = JsonSerializer.Deserialize<TRetorno>(retornoAPI);


                    if (ReturnRequisitionAPIDTO.returnRequisitionAPI == null)
                        errorMessagerAPI = retornoAPI;

                    ReturnRequisitionAPIDTO.returnSucessRequisition = true;
                }
            }
            catch (Exception ex)
            {
                ReturnRequisitionAPIDTO.returnSucessRequisition = false;
                ReturnRequisitionAPIDTO.ErrorMessage = ex.Message;
            }

            return ReturnRequisitionAPIDTO;
        }

        public async Task<ReturnRequisitionAPIDTO<TRetorno>> MakeAPIRequest<URequisicao, TRetorno>(String addressAPI, URequisicao requisition, HttpMethod requisitionTypeHttp)
            where URequisicao : class
            where TRetorno : class
        {
            ReturnRequisitionAPIDTO<TRetorno> ReturnRequisitionAPIDTO = new ReturnRequisitionAPIDTO<TRetorno>();

            try
            {
                HttpRequestMessage request = ReturnInstanceByTypeRequest(addressAPI, requisition, requisitionTypeHttp);

                using (var response = await _HttpClient.SendAsync(request))
                {
                    var retornoAPI = await response.Content.ReadAsStringAsync();
                    string errorMessagerAPI;
                    try
                    {
                        ReturnRequisitionAPIDTO.returnRequisitionAPI = JsonSerializer.Deserialize<TRetorno>(retornoAPI);


                        ResponseDTO retornoDTO;
                        try
                        {
                            retornoDTO = JsonSerializer.Deserialize<ResponseDTO>(retornoAPI);
                        }
                        catch (Exception)
                        {
                            retornoDTO = null;
                        }

                        if (ReturnRequisitionAPIDTO.returnRequisitionAPI == null)
                            errorMessagerAPI = retornoAPI;
                        else if (retornoDTO != null)
                            errorMessagerAPI = retornoDTO.Mensagem;
                    }
                    catch (Exception)
                    {
                        throw new Exception($"Erro ao efetuar a chamada da api. ({response.ReasonPhrase}) \nDescrição: {retornoAPI}");
                    }

                    ReturnRequisitionAPIDTO.returnSucessRequisition = true;
                }
            }
            catch (Exception ex)
            {
                ReturnRequisitionAPIDTO.returnSucessRequisition = false;
                ReturnRequisitionAPIDTO.ErrorMessage = ex.Message;
            }

            return ReturnRequisitionAPIDTO;
        }

        public async Task<ReturnRequisitionAPIDTO<TRetorno>> MakeAPIRequest<URequisicao, TRetorno>(String addressAPI, string requisition, HttpMethod requisitionTypeHttp)
           where URequisicao : class
           where TRetorno : class
        {
            ReturnRequisitionAPIDTO<TRetorno> ReturnRequisitionAPIDTO = new ReturnRequisitionAPIDTO<TRetorno>();

            try
            {
                HttpRequestMessage request = ReturnInstanceByTypeRequest(addressAPI, requisition, requisitionTypeHttp);

                using (var response = await _HttpClient.SendAsync(request))
                {
                    string errorMessagerAPI;
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        throw new Exception(errorMessage);
                    }

                    if (response.Content == null)
                        throw new Exception($"Não foram efetuadas respostas na chamada do endereço {addressAPI}.");

                    var retornoAPI = await response.Content.ReadAsStringAsync();
                    ReturnRequisitionAPIDTO.returnRequisitionAPI = JsonSerializer.Deserialize<TRetorno>(retornoAPI);


                    if (ReturnRequisitionAPIDTO.returnRequisitionAPI == null)
                        errorMessagerAPI = retornoAPI;

                    ReturnRequisitionAPIDTO.returnSucessRequisition = true;
                }
            }
            catch (Exception ex)
            {
                ReturnRequisitionAPIDTO.returnSucessRequisition = false;
                ReturnRequisitionAPIDTO.ErrorMessage = ex.Message;
            }

            return ReturnRequisitionAPIDTO;
        }
    }
}
