using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Domain.InternalServices;

namespace Tsc.Domain
{
    public class Tournament : IIdentifiable
    {
        private List<Team> _participants = new List<Team>();
        private List<Round> _rounds = new List<Round>();

        private readonly IResultTableEnumeratorService _resultTableEnumeratorService;
        private readonly IRoundCreationService _fixtureCreationService;
        private readonly IParticipantEnlisterService _participantEnlisterService;

        /// <summary>
        /// Needed for serialization
        /// </summary>
        private Tournament()
        {
            _fixtureCreationService = new RoundCreationService();
            _participantEnlisterService = new ParticipantEnlisterService();
            _resultTableEnumeratorService = new ResultTableEnumeratorService();
        }

        //For testing
        public Tournament(IRoundCreationService fixtureCreationService, IParticipantEnlisterService participantEnlisterService, IResultTableEnumeratorService resultTableEnumeratorService,
            Guid id, string name, DateTime creationDate, IEnumerable<Team> participants, string logoUrl)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            LogoUrl = logoUrl;

            _participants = new List<Team>();
            _rounds = new List<Round>();

            _resultTableEnumeratorService = resultTableEnumeratorService;
            _fixtureCreationService = fixtureCreationService;
            _participantEnlisterService = participantEnlisterService;

            InitializeTournamentData(participants);          
        }

        public Tournament(string name, IEnumerable<Team> participants, string logoUrl) : 
            this(new RoundCreationService(), new ParticipantEnlisterService(), new ResultTableEnumeratorService(), Guid.NewGuid(), name, DateTime.Now, participants, logoUrl)
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string LogoUrl { get; private set; }

        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// This property is required for the mongodb-rest interface,
        /// since _id must be an Objectid.
        /// https://github.com/tdegrunt/mongodb-rest/issues/11
        /// </summary>
        //[JsonProperty(PropertyName = "_id")]
        //public string TechnicalId { get; private set; }

        public IEnumerable<Team> Participants
        {
            get
            {
                return _participants.AsReadOnly();
            }
            private set
            {
                _participants = new List<Team>(value);
            }
        }

        public IEnumerable<TournamentResultItem> Table
        {
            get
            {
                return _resultTableEnumeratorService.Create(_rounds, _participants);
            }
        }

        public IEnumerable<Round> Rounds
        {
            get
            {
                return _rounds.AsReadOnly();
            }
            private set
            {
                _rounds = new List<Round>(value);
            }
        }

        private void InitializeTournamentData(IEnumerable<Team> participants)
        {
            _participants.AddRange(participants);

            var enlistedParticipants = _participantEnlisterService.GetParticipantsForFixtureCreation(participants);

            var rounds = _fixtureCreationService.CreateRounds(enlistedParticipants);
            _rounds.AddRange(rounds);
        }

        public void SetFixtureResult(Guid fixtureId, IEnumerable<MatchResult> results)
        {
            var fixture = _rounds.SelectMany(item => item.Fixtures).First(item => item.Id == fixtureId);
            fixture.AddResults(results);
        }
    }
}
        