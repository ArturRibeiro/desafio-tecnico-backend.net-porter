namespace Meeting.Hub.Web.Api.EndPoints.Documentacao;

internal class CriarReservaDocumentacao
{
    public static OpenApiOperation CreateSummay(OpenApiOperation op)
    {
        op.Summary = "Criação de Reserva";
        op.Description = """
                         Cria uma nova reserva para uma sala específica.

                         Requer os seguintes campos no corpo da requisição (JSON):

                         - `Nome` (string, obrigatório): Nome da sala a ser reservada.
                         - `Inicio` (DateTime, obrigatório): Data e hora de início da reserva.
                         - `Fim` (DateTime, obrigatório): Data e hora de término da reserva.
                         - `ReservadoPor` (string, obrigatório): Nome do responsável pela reserva.

                         A API retorna 200 OK em caso de sucesso ou 400 Bad Request se os dados forem inválidos.
                         """;
        
        op.RequestBody = new OpenApiRequestBody
        {
            Required = true,
            Content = {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = {
                            ["nome"] = new OpenApiSchema { Type = "string" },
                            ["inicio"] = new OpenApiSchema { Type = "string", Format = "date-time" },
                            ["fim"] = new OpenApiSchema { Type = "string", Format = "date-time" },
                            ["reservadoPor"] = new OpenApiSchema { Type = "string" }
                        },
                        Required = new HashSet<string> { "nome", "inicio", "fim", "reservadoPor" }
                    }
                }
            }
        };

        return op;
    }
}