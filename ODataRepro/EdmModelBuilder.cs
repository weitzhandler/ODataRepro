using Microsoft.OData.ModelBuilder;

namespace ODataRepro
{
    public class EdmModelBuilder : ODataConventionModelBuilder
    {
        public EdmModelBuilder()
        {
            //EntitySet<Entity>("Entities")
            //    .EntityType
            //    .HasKey(entity => entity.Id);
        }

    }
}