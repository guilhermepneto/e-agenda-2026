using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Categorias;

public interface IRepositorioCategoria : IRepositorio<Categoria>
{
    bool ExisteCategoriaPorTitulo(string tituloCategoria, Guid? idIgnorado = null);
}
