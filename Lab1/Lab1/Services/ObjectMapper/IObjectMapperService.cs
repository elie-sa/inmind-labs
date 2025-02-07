namespace Lab1.Services.ObjectMapper;

public interface IObjectMapperService
{
    public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
}