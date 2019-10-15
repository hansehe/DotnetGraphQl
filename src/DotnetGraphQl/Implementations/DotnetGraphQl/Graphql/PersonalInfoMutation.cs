using DotnetGraphQl.Abstractions;
using DotnetGraphQl.Contracts;
using GraphQL.Types;

namespace DotnetGraphQl.GraphQl
{
    public class PersonalInfoMutation : ObjectGraphType
    {
        public PersonalInfoMutation(IPersonalInfoHandler personalInfoHandler)
        {
            FieldAsync<PersonalInfoType>("updatePersonalInfo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PersonalInfoInputType>>()
                    {
                        Name = "personalInfo"
                    }
                ), resolve: async context =>
                {
                    var personalInfo = context.GetArgument<PersonalInfo>("personalInfo");
                    return await personalInfoHandler.UpdatePersonalInfo(personalInfo);
                });
        }
    }
}