using System;
using System.Threading.Tasks;
using DotnetGraphQl.Abstractions;
using DotnetGraphQl.Contracts;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;

namespace DotnetGraphQl.GraphQl
{
    public class PersonalInfoSubscription : ObjectGraphType<PersonalInfo>
    {
        private readonly IPersonalInfoHandler PersonalInfoHandler;

        public PersonalInfoSubscription(IPersonalInfoHandler personalInfoHandler)
        {
            PersonalInfoHandler = personalInfoHandler;
            
            AddField(new EventStreamFieldType
            {
                Name = "personalInfoUpdated",
                Type = typeof(PersonalInfoType),
                Arguments = new QueryArguments(
                    new QueryArgument<PersonalInfoInputType>
                    {
                        Name = "personalInfo"
                    }
                ),
                Resolver = new FuncFieldResolver<PersonalInfo>(ResolvePersonalInfo),
                AsyncSubscriber = new AsyncEventStreamResolver<PersonalInfo>(Subscribe)
            });
        }
        
        private static PersonalInfo ResolvePersonalInfo(ResolveFieldContext context)
        {
            return context.Source as PersonalInfo;
        }

        private Task<IObservable<PersonalInfo>> Subscribe(ResolveEventStreamContext context)
        {
            var personalInfo = context.GetArgument<PersonalInfo>("personalInfo");
            return PersonalInfoHandler.SubscribePersonalInfo(personalInfo);
        }
    }
}