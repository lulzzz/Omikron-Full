using System;
using Omikron.Sync.Model;

namespace Omikron.Sync.Service.Actor.States
{
    [Serializable]
    public sealed class UserSynchronisationGrainState : BaseSynchronisationGrainState<User>
    {
    }
}