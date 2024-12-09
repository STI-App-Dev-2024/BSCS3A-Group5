
using PeakForm.Model;
using System.Collections;

namespace PeakForm.Services;

public class GenerateExercises {
    private int[] JoggingKM = { 1, 2, 3, 4, 5 };
    private int[] RunningKM = { 5, 10, 13, 15 };
    private int[] PushUp = { 5, 10, 15, 20, 25 };
    private int[] Jumpingjacks = { 20, 30, 40, 50 }; // added for variety
    private string[] ExerciseName = { "Jogging", "Running", "Push Up", "Curl Ups", "Jumping Jacks" };
    private int jogging, pushup, running, jumpingJacks;
    private readonly Random random = new Random(0);

    private readonly INavigation _navigationFireServices;
    public GenerateExercises(INavigation navigationFireServices) { 
        _navigationFireServices = navigationFireServices;
    }

    public  Quests GenerateExercise(string UserBodyType)
    {
        int ran = random.Next(1, 5);
        int ran2, ran3;
        if (UserBodyType.Equals("Normalweight"))
        {
            // For Normalweight users, choose random exercises with moderate intensity
            // Random index for exercises
            var exercise = new Quests
            {
                Title = "Moderate Quest",
                Description = "A balanced workout for maintaining fitness.",
                FirstExerciseSet = JoggingKM[ran % JoggingKM.Length],
                SecondExerciseSet = PushUp[ran % PushUp.Length],
                ThirdexerciseSet = Jumpingjacks[ran % Jumpingjacks.Length],
                FirstDescription = ExerciseName[0],
                SecondDescription = ExerciseName[2],
                ThirdDescription = ExerciseName[4]
            };
            return exercise;
        }
        else if (UserBodyType.Equals("Underweight"))
        {
            // For Underweight users, provide a more intense workout to gain strength
           // More intense exercises
            do
            {
                ran2 = random.Next(1, 5);
            } while (ran != ran2);
            do { 
                ran3 = random.Next(1, 5);
            }while (ran3 != ran2 && ran3 != ran);
            var exercise = new Quests
            {
                Title = "Medium Quest",
                Description = "An intense workout to build strength.",
                FirstExerciseSet = JoggingKM[ran % JoggingKM.Length],
                SecondExerciseSet = PushUp[ran2 % PushUp.Length],
                ThirdexerciseSet = Jumpingjacks[ran3 % Jumpingjacks.Length],
                FirstDescription = ExerciseName[ran],
                SecondDescription = ExerciseName[ran2],
                ThirdDescription = ExerciseName[ran3]
            };
            return exercise;
        }
        else {
            var exercise = new Quests
            {
                Title = "Light Quest",
                Description = "A light workout to stay active.",
                FirstExerciseSet = JoggingKM[0],
                SecondExerciseSet = PushUp[0],
                ThirdexerciseSet = Jumpingjacks[0],
                FirstDescription = ExerciseName[0],
                SecondDescription = ExerciseName[4],
                ThirdDescription = ExerciseName[2]
            };
            return exercise;


        }
    }

}
