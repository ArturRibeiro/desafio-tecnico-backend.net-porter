namespace Meeting.Hub.Web.Api.EndPoints.Documentacao;

internal class ConsultarReservaDocumentacao
{
    public static OpenApiOperation CreateSummay(OpenApiOperation op)
    {
        op.Summary = "Consultar reservas com filtros";
        op.Description = """
                         Consulta uma ou mais reservas com base em filtros opcionais informados via query string.

                         Parâmetros disponíveis:
                         - `reservaId` (long, opcional): ID da reserva específica.
                         - `dataInicio` (DateTime, opcional): Data/hora inicial para o filtro de intervalo.
                         - `dataFim` (DateTime, opcional): Data/hora final para o filtro de intervalo.

                         Exemplo de chamada:
                         GET /consultar?reservaId=123
                         GET /consultar?dataInicio=2025-11-04T08:00:00Z&dataFim=2025-11-04T18:00:00Z
                         """;
        return op;
    }
}