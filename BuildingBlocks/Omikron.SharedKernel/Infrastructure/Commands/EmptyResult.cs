using System;
using SystemThreadingTasks = System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public struct EmptyResult : IEquatable<EmptyResult>, IComparable<EmptyResult>, IComparable
    {
        public static readonly EmptyResult Value = new();

        public static readonly SystemThreadingTasks.Task<EmptyResult> Task = SystemThreadingTasks.Task.FromResult(Value);

        public int CompareTo(EmptyResult other) => 0;

        int IComparable.CompareTo(object? obj) => 0;

        public override int GetHashCode() => 0;

        public bool Equals(EmptyResult other) => true;

        public override bool Equals(object? obj) => obj is EmptyResult;

        public static bool operator ==(EmptyResult first, EmptyResult second) => true;

        public static bool operator !=(EmptyResult first, EmptyResult second) => false;

        public override string ToString() => "()";
    }
}
