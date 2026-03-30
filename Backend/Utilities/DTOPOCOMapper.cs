using Newtonsoft.Json;

namespace TimeTable_api
{
    public class DTOPOCOMapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            // Convert source object to dictionary with JSON property names
            string json = JsonConvert.SerializeObject(source);

            // Deserialize JSON into the target type
            return JsonConvert.DeserializeObject<TTarget>(json);
        }

    }
}