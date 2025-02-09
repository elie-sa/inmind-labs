namespace Lab1.Services.ObjectMapper;

public class ObjectMapperService: IObjectMapperService
{
    // In my implementation I'm first mapping the properties with the same and checking their property types and checking whether the mapping is valid
    // If the other properties have different names, we're mapping them based on property type successively
    
    // In my implementation I only changed type when I have same name attributes else we would be trying to map any property to its adjacent since 
    // in this code instead we would search for compatible properties instead of directly trying to convert property types
    public TDestination Map<TSource, TDestination>(TSource source)
        where TDestination : new()
    {
        var destination = new TDestination();
        
        var sourceProps = typeof(TSource).GetProperties();
        var destinationProps = typeof(TDestination).GetProperties();

        foreach (var sourceProp in sourceProps)
        {
            var destinationProp = destinationProps.FirstOrDefault(x => x.Name == sourceProp.Name);

            try
            {
                if (destinationProp != null)
                {
                    if (destinationProp.PropertyType == sourceProp.PropertyType)
                    {
                        destinationProp.SetValue(destination, sourceProp.GetValue(source));
                    }
                }
               
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("The mapping could not be made successfully.");
            }
        }
        
        var unmatchedSourceProps = sourceProps.Where(src => destinationProps.All(dest => dest.Name != src.Name)).ToList();
        var unmatchedDestinationProps = destinationProps.Where(dest => sourceProps.All(src => dest.Name != src.Name)).ToList();

        foreach (var sourceProp in unmatchedSourceProps)
        {
            // If the names differ I'm mapping the next unassigned same property destination to the same property source
            // In this example I changed the property name of username and email but they still mapped
            // The order can't really be regulated using this implementation (in this case they're created in order)
            var sameTypeDestination = unmatchedDestinationProps
                .FirstOrDefault(dest => dest.PropertyType == sourceProp.PropertyType &&  dest.GetValue(destination) == null);

            if (sameTypeDestination != null)
            {
                var currentValue = sameTypeDestination.GetValue(destination);
                sameTypeDestination.SetValue(destination, sourceProp.GetValue(source));
            }
        }

        return destination;
    }

    
}