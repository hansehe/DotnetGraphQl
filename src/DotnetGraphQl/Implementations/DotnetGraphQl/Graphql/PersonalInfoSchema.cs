using GraphQL;
using GraphQL.Types;

namespace DotnetGraphQl.GraphQl
{
    public class PersonalInfoSchema : Schema
    {
        public PersonalInfoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<PersonalInfoQuery>();
            Mutation = resolver.Resolve<PersonalInfoMutation>();
            Subscription = resolver.Resolve<PersonalInfoSubscription>();
        }
    }
}