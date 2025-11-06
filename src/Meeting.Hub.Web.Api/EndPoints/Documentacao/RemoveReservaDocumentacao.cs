namespace Meeting.Hub.Web.Api.EndPoints.Documentacao;

internal class RemoveReservaDocumentacao
{
    public static OpenApiOperation CreateSummay(OpenApiOperation op)
    {
        op.Summary = "Remove uma reserva";
        op.Description = """
                         Remove uma reserva existente com base no ID informado na query string.

                         Parâmetros esperados:

                         - `reservaId` (long, obrigatório): Identificador da reserva a ser removida.

                         Exemplo:
                         PUT /remover?reservaId=123

                         A operação retornará 200 OK se a remoção for bem-sucedida.
                         """;
        return op;
    }
}