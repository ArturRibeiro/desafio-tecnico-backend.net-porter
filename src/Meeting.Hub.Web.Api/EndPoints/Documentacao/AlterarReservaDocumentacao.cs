namespace Meeting.Hub.Web.Api.EndPoints.Documentacao;

internal class AlterarReservaDocumentacao
{
    public static OpenApiOperation CreateSummay(OpenApiOperation op)
    {
        op.Summary = "Alterar uma reserva existente";
        op.Description = """
                         Altera os dados de uma reserva previamente cadastrada.

                         Campos esperados no corpo da requisição:

                         - `reservaId` (long, obrigatório): ID da reserva que será modificada.
                         - `inicio` (DateTime, obrigatório): Nova data/hora de início da reserva.
                         - `fim` (DateTime, obrigatório): Nova data/hora de término da reserva.

                         A reserva deve existir no sistema para que a alteração seja aplicada.
                         """;

        op.RequestBody = new OpenApiRequestBody
        {
            Required = true,
            Content =
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = {
                            ["reservaId"] = new OpenApiSchema { Type = "integer", Format = "int64" },
                            ["inicio"] = new OpenApiSchema { Type = "string", Format = "date-time" },
                            ["fim"] = new OpenApiSchema { Type = "string", Format = "date-time" }
                        },
                        Required = new HashSet<string> { "reservaId", "inicio", "fim" }
                    }
                }
            }
        };

        return op;
    }
}