using DotnetGraphQl.Contracts;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace DotnetGraphQl.GraphQl
{
    public class PersonalInfoInputType : InputObjectGraphType<PersonalInfo>
    {
        public PersonalInfoInputType()
        {
            Name = "PersonalInfoInput";
            var personalInfoType = new PersonalInfoType();
            foreach (var field in personalInfoType.Fields)
            {
                AddField(field);
            }
        }
    }
}