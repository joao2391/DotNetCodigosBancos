using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Xunit;

namespace DotNet.CodigosBancos.Test
{
    public class CodigosBancoTests
    {
        private readonly string fakeContent = string.Empty;
        private CodigosBanco _codigosBancos;

        public CodigosBancoTests()
        {
            fakeContent = File.ReadAllText(@"./FakeData.html");
        }

        [Fact]
        public void Should_Return_Bancos_From_GetBancosByNome_When_All_Params_is_Full()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(response);

            _codigosBancos = new CodigosBanco(mockHttp.Object);

            var result = _codigosBancos.GetBancosByNome(Constants.ITAU);

            Assert.NotNull(result);
            Assert.IsType<List<Banco>>(result);

        }

        [Fact]
        public void Should_Return_Bancos_From_GetBancoByCodigo_When_All_Params_is_Full()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(response);

            _codigosBancos = new CodigosBanco(mockHttp.Object);

            var result = _codigosBancos.GetBancoByCodigo(Constants.CODIGO_RENNER);

            Assert.NotNull(result);
            Assert.IsType<Banco>(result);

        }

        [Fact]
        public void Should_Return_Bancos_From_GetBancos()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(response);

            _codigosBancos = new CodigosBanco(mockHttp.Object);

            var result = _codigosBancos.GetBancos();

            Assert.NotNull(result);
            Assert.IsType<Banco[]>(result);

        }

        [Fact]
        public void Should_Return_ArgumentNullException_From_GetBancosByNome_When_NomeProcurado_Is_Empty()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(response);

            _codigosBancos = new CodigosBanco(mockHttp.Object);

            Assert.Throws<ArgumentNullException>(() => _codigosBancos.GetBancosByNome(""));

        }

        [Fact]
        public void Should_Return_ArgumentException_From_GetBancoByCodigo_When_Codigo_Is_Empty()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(response);

            _codigosBancos = new CodigosBanco(mockHttp.Object);

            Assert.Throws<ArgumentException>(() => _codigosBancos.GetBancoByCodigo(0));

        }
    }
}
