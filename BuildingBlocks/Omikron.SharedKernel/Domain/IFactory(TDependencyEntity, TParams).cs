namespace Omikron.SharedKernel.Domain
{
    public interface IFactory<out TFactoryEntity, in TParams>
    {
        TFactoryEntity Factory(TParams @params);
    }
}