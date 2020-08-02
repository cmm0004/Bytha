using Bytha.Models;
using HotChocolate.Types;

namespace Bytha.Types
{
    public class StuffType : ObjectType<Stuff>
    {
        protected override void Configure(IObjectTypeDescriptor<Stuff> descriptor)
        {
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Tags).Type<ListType<StringType>>();
        }
    }
}
