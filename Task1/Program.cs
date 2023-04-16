using System;
using System.Collections.Generic;

// implement all S.O.L.I.D. principals,
// add Custom delegate,
// add Multicast delegate,
// add Generic delegate,
// add Func<>, Action<> built-in delegate,
// add EventHandler<TEventArgs> delegate which produces some data,
// add EventHandler without parameters which is not produce data.

namespace QuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Quiz quiz = new Quiz();
            quiz.QuizCompleted += OnQuizCompleted;
            quiz.Start();
        }

        private static void OnQuizCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Quiz completed!");
        }
    }

    public class Quiz
    {
        private List<Question> questions;
        private int score;

        public delegate void QuestionAnsweredEventHandler(object sender, string answer);
        public event QuestionAnsweredEventHandler QuestionAnswered;

        public event EventHandler QuizCompleted;

        public Quiz()
        {
            questions = new List<Question> { 
                new Question("Where is the Great Barrier Reef located?", "Australia"),
                new Question("In Greek Mythology, who is the Queen of the Underworld and wife of Hades?", "Persephone"),
                new Question("Which house was Harry Potter almost sorted into?", "Slytherin"),
                new Question("Which country gifted the Statue of Liberty to the US?", "France"),
                new Question("What was the name of the Robin Williams film where he dresses up as an elderly British nanny?", "Mrs. Doubtfire")};
            // Questions from - https://thoughtcatalog.com/january-nelson/2020/04/easy-trivia-questions-answers/
        }
        public void Start()
        {
            score = 0;

            foreach (Question question in questions)
            {
                Console.WriteLine(question.Text);
                string answer = Console.ReadLine();

                if (QuestionAnswered != null)
                {
                    QuestionAnswered(this, answer);
                }

                if (question.IsCorrect(answer))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }
            }

            Console.WriteLine("Your score is {0} out of {1}.", score, questions.Count);
            Console.ReadLine();

            if (QuizCompleted != null)
            {
                QuizCompleted(this, EventArgs.Empty);
            }
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public string Answer { get; set; }

        public Question(string text, string answer)
        {
            Text = text;
            Answer = answer;
        }

        public bool IsCorrect(string response)
        {
            return response.Equals(Answer, StringComparison.OrdinalIgnoreCase);
        }
    }
}
