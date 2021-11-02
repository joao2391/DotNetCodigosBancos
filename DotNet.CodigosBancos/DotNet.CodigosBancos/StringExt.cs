using System.Globalization;

namespace DotNet.CodigosBancos
{
    /// <summary>
    /// Classe de extensão
    /// </summary>
    public static class StringExt
    {
        /// <summary>
        /// Método de extensão para auxiliar na busca
        /// por nome do Banco
        /// </summary>
        /// <param name="source">origem</param>
        /// <param name="search">palavra buscada</param>
        /// <returns>booleano</returns>
        public static bool ContainsInsensitive(this string source, string search)
        {
            return (new CultureInfo("pt-BR").CompareInfo).IndexOf(source, search, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
        }
    }
}
