using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFDesktopSimple.Comparers;
using WPFDesktopSimple.Helpers;
using WPFDesktopSimple.Models;
using WPFDesktopSimple.ViewModels;

namespace WPFDesktopSimple.Views
{
    /// <summary>
    /// Interaction logic for RaceHome.xaml
    /// </summary>
    public partial class RaceHome : UserControl
    {
        private IRaceHomeViewModel viewModel;
        private IFileHelperFactory fileHelperFactory;
        private IInteractionHelper interactionHelper;

        public RaceHome()
        {
            InitializeComponent();

            maxParticipant = 10;
            var digitRound = 1;

            fileHelperFactory = new FileHelperFactory();
            interactionHelper = new InteractionHelperDialog();

            viewModel = new RaceHomeViewModel(digitRound, fileHelperFactory) as IRaceHomeViewModel;
            ConfigurePersonsView();
            ConfigureParticipantsView();
            ConfigureResultsView();

            viewModel.RaceStartEvent += RaceStartingEventHandling;
            viewModel.RaceEndEvent += RaceEndingEventHandling; ;

            DataContext = this;
        }

        private void ConfigurePersonsView()
        {
            PersonsView = CollectionViewSource.GetDefaultView(viewModel.Persons);
            PersonsView.Filter = p => !(p as Person).Selected;
        }

        private void ConfigureParticipantsView()
        {
            ParticipantsView = CollectionViewSource.GetDefaultView(viewModel.Participants);
            ParticipantsView.SortDescriptions.Add(new SortDescription(nameof(Participant.Id), ListSortDirection.Ascending));
        }

        private void ConfigureResultsView()
        {
            ResultsView = CollectionViewSource.GetDefaultView(viewModel.Results) as ListCollectionView;
            ResultsView.CustomSort = new ParticipantResultComparer();
            ResultsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ParticipantResult.IrmString)));
            ResultsView.IsLiveGrouping = true;
            ResultsView.IsLiveSorting = true;
        }

        #region participants hanbdling
        private readonly int maxParticipant;
        public ICollectionView PersonsView
        {
            get; private set;
        }
        public ICollectionView ParticipantsView
        {
            get; private set;
        }

        private void LoadPersons(object sender, RoutedEventArgs e)
        {
            string filePath;
            if (interactionHelper.OpenFile(out filePath, new object[] { false, fileHelperFactory.GetFilterFormat(FileFormatAccepted.Xml) }))
            {
                if (!viewModel.LoadPersons(filePath, FileFormatAccepted.Xml))
                {
                    interactionHelper.ProvideFeedback("An error occured while reading \"Person data\", please insure the file is in the correct format and try again");
                }
            }
        }
        private void PersonsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.Participants.Count > maxParticipant
                || personlb.SelectedItems.Count + viewModel.Participants.Count > maxParticipant)

            {
                foreach (var addition in e.AddedItems)
                {
                    personlb.SelectedItems.Remove(addition);
                }
                e.AddedItems.Clear();

                interactionHelper.ProvideFeedback($"No more than {maxParticipant} participants can take part in a race.");
            }
        }
        private void AddParticipant(object sender, RoutedEventArgs e)
        {
            if (personlb.SelectedItems.Count > 0)
            {
                viewModel.AddParticipants(personlb.SelectedItems);

                personlb.SelectedItems.Clear();
                PersonsView.Refresh();
            }
        }
        private void RemoveParticipant(object sender, RoutedEventArgs e)
        {
            if (participantlb.SelectedItems.Count > 0)
            {
                viewModel.RemoveParticipant(participantlb.SelectedItems);
                participantlb.SelectedItems.Clear();
                PersonsView.Refresh();
            }
        }

        #endregion

        #region run race

        private bool raceProcessing = false;
        public ListCollectionView ResultsView
        {
            get; private set;
        }
        private void StartRace(object sender, RoutedEventArgs e)
        {
            if (viewModel.Participants.Count == 0)
            {
                interactionHelper.ProvideFeedback($"The Race needs at least 1 participant and at most {maxParticipant} participants to start.");
                return;
            }

            if (ResultsView.Count > 0)
            {
                var result = interactionHelper.AskQuestion("Previous race data have not been exported yet. Running a new race will erase them. Proceed?");
                if (result == false)
                {
                    return;
                }
            }

            viewModel.StartRace();
        }
        private void ExportDataRace(object sender, RoutedEventArgs e)
        {
            if (raceProcessing)
            {
                interactionHelper.ProvideFeedback("Race is currently running, please wait.");
            }
            else
            {
                string filePath;
                if (interactionHelper.SaveFile(out filePath, new [] { fileHelperFactory.GetFilterFormat(FileFormatAccepted.Xml) }))
                {
                    viewModel.ExportDataRace(filePath, FileFormatAccepted.Xml);
                }
            }
        }
        private void RaceEndingEventHandling(object sender, Events.RaceEventArg e)
        {
            raceProcessing = false;
            startBtn.IsEnabled = !raceProcessing;
            interactionHelper.ProvideFeedback("Race has ended.");
        }
        private void RaceStartingEventHandling(object sender, Events.RaceEventArg e)
        {
            raceProcessing = true;
            startBtn.IsEnabled = !raceProcessing;
        }
        #endregion
    }
}
