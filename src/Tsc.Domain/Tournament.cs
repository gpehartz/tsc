using System;
using System.Collections.Generic;
using System.Linq;
using Tsc.Domain.InternalServices;

namespace Tsc.Domain
{
    public class Tournament
    {
        private readonly List<Team> _participants;
        private List<Round> _rounds;

        private IResultTableEnumeratorService _resultTableEnumeratorService;
        private IRoundCreationService _fixtureCreationService;
        private IParticipantEnlisterService _participantEnlisterService;

        //For testing
        public Tournament(IRoundCreationService fixtureCreationService, IParticipantEnlisterService participantEnlisterService, IResultTableEnumeratorService resultTableEnumeratorService,
            Guid id, string name, DateTime creationDate, IEnumerable<Team> participants)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;

            _participants = new List<Team>();
            _rounds = new List<Round>();

            _resultTableEnumeratorService = resultTableEnumeratorService;
            _fixtureCreationService = fixtureCreationService;
            _participantEnlisterService = participantEnlisterService;

            InitializeTournamentData(participants);          
        }

        public Tournament(string name, IEnumerable<Team> participants) : 
            this(new RoundCreationService(), new ParticipantEnlisterService(), new ResultTableEnumeratorService(), Guid.NewGuid(), name, DateTime.Now, participants)
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public IEnumerable<Team> Participants
        {
            get
            {
                return _participants.AsReadOnly();
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
        