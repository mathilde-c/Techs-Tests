using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPFDesktopSimple.Events;
using WPFDesktopSimple.Helpers;
using WPFDesktopSimple.Models;
using ThirdPartySimulator;
using ThirdPartySimulator.Events;

namespace WPFDesktopSimple.ViewModels
{
    public class RaceHomeViewModel : IRaceHomeViewModel
    {
        public event EventHandler<RaceEventArg> RaceStartEvent;
        public event EventHandler<RaceEventArg> RaceEndEvent;

        private readonly int digitRounding;
        private readonly IFileHelperFactory fileHelperFactory;
        private readonly string deserializeRootAttribute;

        public RaceHomeViewModel(int digitRound, IFileHelperFactory fileHelperFacto) {
            digitRounding = digitRound;
            fileHelperFactory = fileHelperFacto;
            deserializeRootAttribute = "EntryList";
        }

        #region participants hanbdling
        public ObservableCollection<Person> persons = new ObservableCollection<Person>();
        public ObservableCollection<Person> Persons
        {
            get { return persons; }
        }

        public ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> Participants { get { return participants; } }


        LinkedList<int> availableId = new LinkedList<int>();

        public bool LoadPersons(string filepath, FileFormatAccepted format)
        {
            List<Person> personsFromXml;
            IFileHelper fileHelper = fileHelperFactory.GetFileHelper(format);

            if (!fileHelper.DeserializeFromFile<List<Person>>(filepath, out personsFromXml, deserializeRootAttribute))
            {
                return false;
            }

            Persons.Clear();
            Participants.Clear();
            availableId.Clear();
            foreach (var person in personsFromXml)
            {
                Persons.Add(person);
            }

            return true;
        }
        public void AddParticipants(IList selectedPersons)
        {
            foreach (var person in selectedPersons)
            {
                var personSelected = person as Person;
                personSelected.Selected = true;
                var p = new Participant() { PersonInfo = personSelected, Id = GetNextAvailableId() };
                Participants.Add(p);
            } 
        }
        private int GetNextAvailableId()
        {
            if (availableId.Count == 0)
            {
                return Participants.Count + 1;
            }

            var available = availableId.First.Value;
            availableId.RemoveFirst();

            return available;
        }
        public void RemoveParticipant(IList selectedParticipants)
        {
            for (int i = selectedParticipants.Count - 1; i >= 0; i--)
            {
                var participant = selectedParticipants[i] as Participant;
                participant.PersonInfo.Selected = false;
                availableId.AddLast(participant.Id);
                Participants.Remove(participant);
            }
        }

        #endregion

        #region run race

        private DateTime startTime;
        private BackgroundWorker worker = null;
        private IDictionary<int, Participant> raceParticipants = new Dictionary<int, Participant>();

        public ObservableCollection<ParticipantResult> results = new ObservableCollection<ParticipantResult>();
        public ObservableCollection<ParticipantResult> Results { get { return results; } }

        public void StartRace()
        {
            Results.Clear();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += RunRace;

            worker.RunWorkerCompleted += (Obj, eventArg) =>
            {
                RaceEndEvent?.Invoke(this, new RaceEventArg(startTime));
            };

            worker.RunWorkerAsync();
        }
        private void RunRace(object sender, DoWorkEventArgs e)
        {
            try
            {
                raceParticipants.Clear();
                IList<Individual> individuals = new List<Individual>();
                var sortedParticipant = Participants.ToList();
                sortedParticipant.Sort((p1, p2) => p1.Id < p2.Id ? -1 : 1);
                foreach (var p in sortedParticipant)
                {
                    var participant = p as Participant;
                    individuals.Add(new Individual(participant.PersonInfo.Name, participant.Id));
                    raceParticipants[participant.Id] = participant;
                }

                Simulator simulator = new Simulator();
                simulator.Initialize(individuals as List<Individual>);
                simulator.IndividualChanged += IndividualEndingEvent;

                simulator.RunCompleted += IndividualRaceEnded;
                startTime = DateTime.Now;
                Application.Current.Dispatcher.Invoke(new Action(() =>RaceStartEvent?.Invoke(this, new RaceEventArg(startTime))));
                simulator.Start();

                while (simulator.IsRunning) { /* Make last as it cannot be awaited */ }
            }
            catch (Exception exc)
            {
                // Log maybe
            }
        }
        private void IndividualRaceEnded(object sender, RunCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var participant = raceParticipants[e.Indetifier];
                var finalTime = e.EventTime;
                TimeSpan interval = finalTime - startTime;
                double netTime = Math.Round(interval.TotalSeconds, digitRounding, MidpointRounding.AwayFromZero);

                var presult = new ParticipantResult(participant, EndingEventType.None, netTime);
                Results.Add(presult);
            }));
        }
        private void IndividualEndingEvent(object sender, IndividualChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var participant = raceParticipants[e.Indetifier];

                var presult = new ParticipantResult(participant, e.EndingEvent);
                Results.Add(presult);
            }));
        }
        public void ExportDataRace(string filePath, FileFormatAccepted format)
        {
            RaceResult raceResult = new RaceResult() { participantResults = Results.ToList(), StartTime = startTime };
            IFileHelper fileHelper = fileHelperFactory.GetFileHelper(format);
            fileHelper.SerializeToFile(filePath, raceResult);
        }


        #endregion
    }
}
