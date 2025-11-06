namespace Meeting.Hub.Web.Api.EndPoints;

public static class WebApplicationExtension
{
    public static WebApplication MapEndpointCriarReserva
        (this WebApplication app)
    {
        app.MapPost("/criar", async Task (CriaReservaCommand command, IMediator mediator) 
                => await CriaReserva(command, mediator))
            .WithName("CriarReserva")
            .WithTags("Reservas")
            .Accepts<CriaReservaCommand>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(CriarReservaDocumentacao.CreateSummay);

        return app;
    }


    public static WebApplication MapEndpointConsultarReserva
        (this WebApplication app)
    {
        app.MapGet("/consultar", async ([AsParameters] ConsultaReservaCommand command, IMediator mediator)
                => await ConsultaReserva(command, mediator))
            .WithName("ConsultarReserva")
            .WithTags("Reservas")
            .Produces<IEnumerable<ReservaMessageResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(ConsultarReservaDocumentacao.CreateSummay);

        return app;
    }


    public static WebApplication MapEndpointAlterarReserva
        (this WebApplication app)
    {
        app.MapPut("/alterar", async ([AsParameters] AlteraReservaCommand command, IMediator mediator) 
                => await AlteraReserva(command, mediator))
            .WithName("AlterarReserva")
            .WithTags("Reservas")
            .Accepts<AlteraReservaCommand>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(AlterarReservaDocumentacao.CreateSummay);

        return app;
    }


    public static WebApplication MapEndpointRemoverReserva
        (this WebApplication app)
    {
        app.MapPut("/remover", async ([AsParameters] RemoveReservaCommand command, IMediator mediator)
                => await RemoveReserva(command, mediator))
            .WithName("RemoverReserva")
            .WithTags("Reservas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(RemoveReservaDocumentacao.CreateSummay);

        return app;
    }

    static async Task<IResult> CriaReserva
        (CriaReservaCommand command, IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }
    
    static async Task<IResult> AlteraReserva
        (AlteraReservaCommand command, IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }
    
    static async Task<IResult> RemoveReserva
        (RemoveReservaCommand command, IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }
    
    static async Task<IResult> ConsultaReserva
        (ConsultaReservaCommand command, IMediator mediator) 
            => Results.Ok(await mediator.Send(command));
}