using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetGraphQl.Contracts;

namespace DotnetGraphQl.Abstractions
{
    public interface IPersonalInfoHandler
    {
        Task<IEnumerable<PersonalInfo>> GetPersonalInfos(PersonalInfoSearchParams @params);
        Task<PersonalInfo> UpdatePersonalInfo(PersonalInfo personalInfo);
        Task<IObservable<PersonalInfo>> SubscribePersonalInfo(PersonalInfo subscribedPersonalInfo);
    }
}
