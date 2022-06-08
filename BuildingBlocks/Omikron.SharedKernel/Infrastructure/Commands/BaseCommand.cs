using System;
using MediatR;
using Newtonsoft.Json;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public class BaseCommand
    {
        public abstract class Action : IRequest<EmptyResult>
        {
            [JsonIgnore] public Guid PerformedBy { get; set; }
        }

        public abstract class Action<TResponse> : IRequest<TResponse>
        {
            [JsonIgnore] public Guid PerformedBy { get; set; }
        }

        public abstract class Add : Action
        {
        }

        public abstract class Add<TResponse> : Action<TResponse>
        {
        }

        public abstract class Modify : Action
        {
        }

        public abstract class Modify<TResponse> : Action<TResponse>
        {
        }

        public abstract class Archive : Action
        {
        }

        public abstract class Archive<TResponse> : Action<TResponse>
        {
        }

        public abstract class Delete : Action
        {
        }

        public abstract class Delete<TResponse> : Action<TResponse>
        {
        }
    }
}