namespace eSistem.Proxy.Extensions;

public static class ConfigurationExtension
{
    /// <summary>
    /// Obtém uma seção da configuração como um objeto do tipo especificado.
    /// Retorna null se a seção não existir.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto a ser retornado.</typeparam>
    /// <param name="configuration">A instância de configuração.</param>
    /// <param name="sectionName">O nome da seção a ser obtida.</param>
    /// <returns>Um objeto do tipo <typeparamref name="T"/> ou null se a seção não existir.</returns>
    public static T? Get<T>(this IConfiguration configuration, string sectionName)
        where T : class
    {
        var section = configuration.GetSection(sectionName);
        return section.Exists() ? section.Get<T>() : default;
    }

    /// <summary>
    /// Obtém uma seção da configuração como um objeto do tipo especificado.
    /// Lança uma exceção se a seção não existir ou não puder ser mapeada para o tipo especificado.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto a ser retornado.</typeparam>
    /// <param name="configuration">A instância de configuração.</param>
    /// <param name="sectionName">O nome da seção a ser obtida.</param>
    /// <returns>Um objeto do tipo <typeparamref name="T"/>.</returns>
    /// <exception cref="InvalidOperationException">Lançada se a seção não existir ou não puder ser mapeada.</exception>
    public static T GetRequired<T>(this IConfiguration configuration, string sectionName)
        where T : class
    {
        var section = configuration.GetSection(sectionName);
        if (!section.Exists())
        {
            throw new InvalidOperationException(
                $"The section '{sectionName}' does not exist in the configuration."
            );
        }

        var result =
            section.Get<T>()
            ?? throw new InvalidOperationException(
                $"The section '{sectionName}' could not be mapped to type '{typeof(T).Name}'."
            );

        return result;
    }
}
