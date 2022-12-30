using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext dataContext;
            public Handler(DataContext dataContext)
            {
            this.dataContext = dataContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.Id).ConfigureAwait(false);

                this.dataContext.Remove(activity);

                await this.dataContext.SaveChangesAsync().ConfigureAwait(false);

                return Unit.Value;
            }
        }
    }
}