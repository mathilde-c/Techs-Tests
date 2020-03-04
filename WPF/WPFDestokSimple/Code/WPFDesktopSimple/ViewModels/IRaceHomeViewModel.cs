using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDesktopSimple.Events;
using WPFDesktopSimple.Helpers;
using WPFDesktopSimple.Models;

namespace WPFDesktopSimple.ViewModels
{
    public interface IRaceHomeViewModel
    {
        event EventHandler<RaceEventArg> RaceStartEvent;
        event EventHandler<RaceEventArg> RaceEndEvent;

        ObservableCollection<Person> Persons { get; }
        ObservableCollection<Participant> Participants { get; }
        ObservableCollection<ParticipantResult> Results { get; }

        bool LoadPersons(string filepath, FileFormatAccepted format);
        void AddParticipants(IList selectedPersons);
        void RemoveParticipant(IList selectedParticipants);

        void StartRace();
        void ExportDataRace(string filePath, FileFormatAccepted format);
    }
}
