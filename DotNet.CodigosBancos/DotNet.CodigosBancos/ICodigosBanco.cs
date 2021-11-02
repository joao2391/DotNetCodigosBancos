using System.Collections.Generic;

namespace DotNet.CodigosBancos
{
    /// <summary>
    /// Interface para a classe CodigosBanco
    /// </summary>
    public interface ICodigosBanco
    {
        /// <summary>
        /// Busca o banco de acordo com o nome
        /// </summary>
        /// <param name="nomeProcurado">Nome do Banco</param>
        /// <returns>Lista de Bancos</returns>
        List<Banco> GetBancosByNome(string nomeProcurado);
        /// <summary>
        /// Busca o banco de acordo com o código
        /// </summary>
        /// <param name="codigoBanco">Código de Compensação</param>
        /// <returns>Objeto Banco</returns>
        Banco GetBancoByCodigo(int codigoBanco);
        /// <summary>
        /// Busca todos bancos
        /// </summary>
        /// <returns>Array de Bancos</returns>
        Banco[] GetBancos();
    }
}
