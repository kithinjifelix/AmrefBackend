using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using PG.Management.Core.Application.Consultation.Events;
using PG.Management.Core.Domain.Consultation;
using Serilog;

namespace PG.Management.Core.Application.Consultation.Commands
{
    public class SampleCommand : IRequest<Result>
    {
        public Guid Id { get; }

        public SampleCommand(Guid id)
        {
            Id = id;
        }
    }

    public class SampleCommandHandler : IRequestHandler<SampleCommand, Result>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _projectRepository;

        public SampleCommandHandler(IMediator mediator, IProjectRepository projectRepository)
        {
            _mediator = mediator;
            _projectRepository = projectRepository;
        }


        public Task<Result> Handle(SampleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _projectRepository.DeleteById(request.Id);

                _mediator.Publish(new SampleEvent(request.Id), cancellationToken);

                return Task.FromResult(Result.Success());
            }
            catch (Exception e)
            {
                Log.Error("Error occured", e);
                return Task.FromResult(Result.Failure(e.Message));
            }
        }
    }
}
