using System;
using DotnetGraphQl.Abstractions;
using DotnetGraphQl.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DotnetGraphQl.Config;
using Newtonsoft.Json;

namespace DotnetGraphQl
{
    public class PersonalInfoHandler : IPersonalInfoHandler
    {
        private readonly ILogger<PersonalInfoHandler> Logger;
        
        // Global static due to scoped instantiation.
        private static readonly ISubject<PersonalInfo> PersonalInfoStream = new ReplaySubject<PersonalInfo>(1);

        public PersonalInfoHandler(
            ILogger<PersonalInfoHandler> logger)
        {
            Logger = logger;
        }

        public async Task<IEnumerable<PersonalInfo>> GetPersonalInfos(PersonalInfoSearchParams @params)
        {
            var dataStr = File.ReadAllText(BaseConfig.GetAllDataFilename);
            var personalInfos = JsonConvert.DeserializeObject<IEnumerable<PersonalInfo>>(dataStr);
            return FilterPersonalInfos(@params, personalInfos);
        }

        public async Task<PersonalInfo> UpdatePersonalInfo(PersonalInfo personalInfo)
        {
            Logger.LogInformation("Updating personal info..");
            // Should update, but kinda don't want to..
            PersonalInfoStream.OnNext(personalInfo);
            return personalInfo;
        }

        public async Task<IObservable<PersonalInfo>> SubscribePersonalInfo(PersonalInfo subscribedPersonalInfo)
        {
            if (subscribedPersonalInfo == null)
            {
                return PersonalInfoStream.AsObservable();
            }

            return PersonalInfoStream.Where(updatedPersonalInfo => Tools.IsEqual(updatedPersonalInfo, subscribedPersonalInfo));
        }    
        
        private static IEnumerable<PersonalInfo> FilterPersonalInfos(PersonalInfoSearchParams @params,
            IEnumerable<PersonalInfo> personalInfos)
        {
            return personalInfos.Where(personalInfo => PersonalInfoInSearchParams(@params, personalInfo));
        }
        
        private static bool PersonalInfoInSearchParams(PersonalInfoSearchParams @params, PersonalInfo personalInfo)
        {
            return true;
        }
    }
}