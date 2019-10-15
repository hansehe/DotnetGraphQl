using DotnetGraphQl.Contracts;
using GraphQL.Types;

namespace DotnetGraphQl.GraphQl
{
    public class PersonalInfoType : ObjectGraphType<PersonalInfo>
    {
        public PersonalInfoType()
        {
            Field(x => x.Nickname, nullable: true).Description("Personal nickname");
            Field(x => x.Name, nullable: true).Description("Personal name");
            Field(x => x.Lastname, nullable: true).Description("Personal lastname");
        }
    }
}