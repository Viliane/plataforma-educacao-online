using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}