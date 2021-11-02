using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNet.CodigosBancos
{
    /// <summary>
    /// Classe com função que busca códigos de compensação dos Bancos.
    /// </summary>
    public class CodigosBanco : ICodigosBanco
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly Bancos _bancos;
        private readonly Dictionary<int, Banco> _dicionarioBancos;

        /// <summary>
        /// Construtor para instanciar a classe HttpClientWrapper via injeção de dependência.
        /// </summary>
        /// <param name="httpClientWrapper">Objeto HttpClientWrapper</param>
        public CodigosBanco(IHttpClientWrapper httpClientWrapper)
        {
            _httpClient = httpClientWrapper;
            _bancos = new Bancos();
            _dicionarioBancos = new Dictionary<int, Banco>();

            BuildBancoAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Busca o banco de acordo com o código
        /// </summary>
        /// <param name="codigoBanco">Código de Compensação</param>
        /// <returns>Objeto Banco</returns>
        /// <exception cref="KeyNotFoundException">KeyNotFoundException</exception>
        /// <exception cref="ArgumentException">ArgumentNullException</exception>
        public Banco GetBancoByCodigo(int codigoBanco)
        {
            if(codigoBanco <= 0)
            {
                throw new ArgumentException("codigoBanco não pode ser menor ou igual a zero!");
            }

            try
            {
                var banco = new Banco();
                
                banco = _dicionarioBancos[codigoBanco];

                return banco;
            }
            catch (KeyNotFoundException)
            {

                throw;
            }


        }

        /// <summary>
        /// Busca todos bancos
        /// </summary>
        /// <returns>Array de Bancos</returns>
        public Banco[] GetBancos()
        {
            if (_bancos.BancoArray is null)
            {
                return null;
            }

            return _bancos.BancoArray;
        }

        /// <summary>
        /// Busca o banco de acordo com o nome
        /// </summary>
        /// <param name="nomeProcurado">Nome do Banco</param>
        /// <returns>Lista de Bancos</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public List<Banco> GetBancosByNome(string nomeProcurado)
        {
            if (string.IsNullOrEmpty(nomeProcurado))
            {
                throw new ArgumentNullException(nomeProcurado, "nomeProcurado não pode ser vazio!");

            }

            var listaBancos = new List<Banco>();

            for (int i = 0; i < _bancos?.BancoArray?.Length; i++)
            {

                if (_bancos.BancoArray[i].Nome.ContainsInsensitive(nomeProcurado))
                {
                    listaBancos.Add(_bancos.BancoArray[i]);
                }

            }

            return listaBancos;
        }

        private async Task BuildBancoAsync()
        {
            try
            {
                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://www.codigobanco.com/")
                };

                var response = await _httpClient.GetAsync(requestMessage).ConfigureAwait(false);

                if (response is null)
                {
                    return;
                }

                var doc = new HtmlDocument();
                var html = await response.Content.ReadAsStringAsync();
                doc.LoadHtml(html);

                var quantidadeTabela = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/div[1]/div[1]/div[1]/div[1]/article[1]/div[1]/table[1]/tbody[1]").ChildNodes.Count;
                var quantidade = quantidadeTabela / 2;

                _bancos.BancoArray = new Banco[quantidade];

                for (int i = 0; i < quantidade; i++)
                {
                    var codigo = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/div[1]/div[1]/div[1]/div[1]/article[1]/div[1]/table[1]/tbody[1]/tr[{i + 1}]/td[1]").InnerText;
                    var nome = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/div[1]/div[1]/div[1]/div[1]/article[1]/div[1]/table[1]/tbody[1]/tr[{i + 1}]/td[2]").InnerText;
                    var site = doc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/div[1]/div[3]/div[1]/div[1]/div[1]/div[1]/div[1]/article[1]/div[1]/table[1]/tbody[1]/tr[{i + 1}]/td[3]").InnerText;

                    _bancos.BancoArray[i] = new Banco()
                    {
                        Codigo = Convert.ToInt32(codigo),
                        Nome = nome,
                        Site = site
                    };

                    _dicionarioBancos.Add(_bancos.BancoArray[i].Codigo, _bancos.BancoArray[i]);

                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (HtmlWebException)
            {
                throw;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }



        }
    }
}
