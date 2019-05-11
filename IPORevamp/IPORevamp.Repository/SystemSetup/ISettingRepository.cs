using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using IPORevamp.Data.TempModel;

namespace IPORevamp.Repository.Event
{
    public interface ISettingRepository: IAutoDependencyRegister
    {
        #region Country Respository

        Task<Country> SaveCountry(Country country);
        Task<Country> GetCountryById(int CountryId);

        Task<List<Country>> GetCountries();
        #endregion

        Task<UserVerificationTemp> SaveUserVerification(UserVerificationTemp userverificationTemp);



        #region AccountType Respository

        Task<AccountType> AccountType(string AccountTypeCode);

        #endregion

        #region Lookup Respository
        Task<Setting> SaveSetting(Setting setting);   
        Task<List<Setting>> GetSettings();
        Task<List<Setting>> GetSettingsByCode(string SetSettingCode);
        #endregion

        #region State Respository
        Task<State> SaveState(State state);    
        Task<List<State>> GetStates();
        Task<State> GetStatesById(int Id);

       
        #endregion

        #region LGA Respository
        Task<lga> SaveLocalGovtArea(lga state);
        Task<State> GetLgaById(int Id);
        #endregion

        #region PTApplication Respository
        Task<PTApplicationStatus> SavePatentApplicationStatus(PTApplicationStatus pTApplicationStatus);
        Task<PTApplicationStatus> GetPatentApplicationStatusById(int Id);
        #endregion


        #region TMApplication Respository
        Task<TMApplicationStatus> SaveTradeMarkApplicationStatus(TMApplicationStatus tMApplicationStatus);
        Task<TMApplicationStatus> GetTradeMarkApplicationStatusById(int Id);
        #endregion

        #region DSApplication Respository
        Task<DSApplicationStatus> SaveDesignApplicationStatus(DSApplicationStatus dSApplicationStatus);
        Task<DSApplicationStatus> GetDesignApplicationStatusById(int Id);
        #endregion

        #region MyRegion

        #endregion

        //Task<List<EventInfo>> FetchOrganizedEvents(int organizerId);
        //Task<string> SaveAttendeeAsync(Attendee attendee);
        //Task SaveSession(int eventId, EventSessions session);
        //Task<Attendee> RegisterForEvent(Attendee attendee);
        //Task SaveSponsorAsync(Sponsors sponsors);
        //Task<bool> PersonalizeAgenda(int attendeeId, int[] selectedSessions);
        //Task<List<Attendee>> FetchSessionParticipant(int sessionId);
        //Task SavePoll(int eventId, Poll poll);
        //Task<bool> AttendeeVote(int pollId, int attendeeId, int optionId);

    }
}
