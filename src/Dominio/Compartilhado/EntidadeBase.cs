namespace eAgenda.Dominio.Compartilhado;

public sealed class ErroValidacao
{
    public required string Campo { get; set; }
    public required string Mensagem { get; set; }
}

public abstract class EntidadeBase<T>
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public abstract List<ErroValidacao> Validar();
    public abstract void Atualizar(T entidadeAtualizada);
}
