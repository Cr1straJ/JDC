namespace JDC.BusinessLogic.Utilities.AutoMapper
{
    public interface IMapper<out TOut>
    {
        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <typeparam name="TOut">Destination type to create.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        TOut Map(object source);
    }
}
