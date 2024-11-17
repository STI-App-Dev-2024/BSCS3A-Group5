
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

    public  Exercises GenerateExercise(string UserBodyType)
    {
        if (UserBodyType.Equals("Normalweight"))
        {
            // For Normalweight users, choose random exercises with moderate intensity
            int ran = random.Next(0, 5); // Random index for exercises
            var exercise = new Exercises
            {
                Title = "Moderate Quest",
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
            int ran = random.Next(1, 5); // More intense exercises
            var exercise = new Exercises {
                Title = "Medium Quest",
                FirstExerciseSet = JoggingKM[ran % JoggingKM.Length],
                SecondExerciseSet = PushUp[ran % PushUp.Length],
                ThirdexerciseSet = Jumpingjacks[ran % Jumpingjacks.Length],
                FirstDescription = ExerciseName[ran],
                SecondDescription = ExerciseName[ran],
                ThirdDescription = ExerciseName[ran]
            };
            return exercise;
        }
        else {
            var exercise = new Exercises
            {
                Title = "Light Quest",
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
