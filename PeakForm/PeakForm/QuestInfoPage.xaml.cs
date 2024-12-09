namespace PeakForm
{
    using Model;
    using PeakForm.ViewModel;

    public partial class QuestInfoPage : ContentPage
    {
        public QuestInfoPage(Quests quests)
        {
            InitializeComponent();
            // Assign the properties
            FirstDescription = quests.FirstDescription;
            FirstExerciseSet = quests.FirstExerciseSet;
            SecondDescription = quests.SecondDescription;
            SecondExerciseSet = quests.SecondExerciseSet;
            ThirdDescription = quests.ThirdDescription;
            ThirdExerciseSet = quests.ThirdexerciseSet;

            // Bind to the UI (or directly manipulate controls)
            BindingContext = this;
        }

        // These properties are now bindable
        public string FirstDescription { get; set; }
        public int FirstExerciseSet { get; set; }
        public string SecondDescription { get; set; }
        public int SecondExerciseSet { get; set; }
        public string ThirdDescription { get; set; }
        public int ThirdExerciseSet { get; set; }
    }
}
