using System.Collections.Generic;
using DotnetGraphQl.Abstractions;
using DotnetGraphQl.Contracts;
using GraphQL.Types;

namespace DotnetGraphQl.GraphQl
{
    public class PersonalInfoQuery : ObjectGraphType
    {
        public PersonalInfoQuery(IPersonalInfoHandler personalInfoHandler)
        {
            FieldAsync<ListGraphType<PersonalInfoType>>("personalInfos",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<StringGraphType>()
                    {
                        Name = "nickname"
                    },
                    new QueryArgument<StringGraphType>()
                    {
                        Name = "name"
                    },
                    new QueryArgument<StringGraphType>()
                    {
                        Name = "lastname"
                    }
                }), resolve: async context =>
                {
                    var searchParams = new PersonalInfoSearchParams();
                    
                    var nickname = context.GetArgument<string>("nickname");
                    searchParams.Nickname = string.IsNullOrWhiteSpace(nickname) ? searchParams.Nickname : nickname;
                    
                    var name = context.GetArgument<string>("name");
                    searchParams.Name = string.IsNullOrWhiteSpace(name) ? searchParams.Name : name;
                    
                    var lastname = context.GetArgument<string>("lastname");
                    searchParams.Lastname = string.IsNullOrWhiteSpace(lastname) ? searchParams.Lastname : lastname;

                    return await personalInfoHandler.GetPersonalInfos(searchParams);
                });
        }
    }
}