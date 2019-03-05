using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Data.Models;
using static TestMakerFreeWebApp.Enums.Enums;

namespace TestMakerFreeWebApp.Data
{
    public class DbSeeder
    {
        #region Public Methods
        public static void Seed(ApplicationDbContext dbContext) {
            // Create default Users if there are none
            if (!dbContext.Users.Any()) CreateUsers(dbContext);

            // Create default quizzes if there are none
            if (!dbContext.Quizzes.Any()) CreateQuizzes(dbContext);
        }

        #endregion

        #region Seed Methods
        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            // local Variables
            DateTime createdDate = new DateTime(2019, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            // Create admin  user
            var user_Admin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Role = RoleType.Admin,
                UserName = "Admin",
                Email = "admin@testmakefree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate           
            };

            // insert the admin user into the DB
            dbContext.Users.Add(user_Admin);

#if DEBUG
            // Create some sample accounts if they don't exist already

            var user_Ryan = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Role = RoleType.User,
                UserName = "Ryan",
                Email = "ryan@testmakefree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var user_Imogen = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Role = RoleType.User,
                UserName = "Imogen",
                Email = "Imogen@testmakefree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var user_Spock = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Role = RoleType.User,
                UserName = "Spock",
                Email = "Spock@testmakefree.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            // Add sample users
            dbContext.Users.AddRange(user_Ryan, user_Imogen, user_Spock);
#endif
            dbContext.SaveChanges();
        }

        private static void CreateQuizzes(ApplicationDbContext dbContext)
        {
            // local Variables
            DateTime createdDate = new DateTime(2019, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            // retrieve admin user, which we will use as the default author
            var authorId = dbContext.Users.Where(u => u.Role == RoleType.Admin).FirstOrDefault().Id;

#if DEBUG
            // create 47 sample quizzes qith auto-generated data
            var num = 47;
            for (int i = 1; i <= num; i++)
            {
                CreateSampleQuiz(
                    dbContext,
                    i,
                    authorId,
                    num - i,
                    3,
                    3,
                    3,
                    createdDate.AddDays(-num));
            }
#endif
            // Add some "real" quizzes
            EntityEntry<Quiz> e1 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Are you More Light or Dark Side of the Force?",
                Description = "Star Wars Personality Test",
                Text = "Choose wisely you must, young padawan",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Quiz> e2 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "GenX, GenY, or Genz?",
                Description = "Find out ehich decade most represents you",
                Text = "Do you feel comfortable in your generation?",
                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Quiz> e3 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Which Office character are you?",
                Description = "Paper Salesman personality test",
                Text = "Do you love to play pranks on co-workers?",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            dbContext.SaveChanges();
        }
      
        #endregion

        #region Utility Methods

        private static void CreateSampleQuiz(
            ApplicationDbContext dbContext,
            int num,
            string authorId,
            int viewCount,
            int numberOfQuestions,
            int numberOfAnswersPerQuestion,
            int numberOfResults,
            DateTime createdDate)
        {
            var quiz = new Quiz()
            {
                UserId = authorId,
                Title = String.Format("Quiz {0} Title", num),
                Description = String.Format("This is a sample description for quiz {0}", num),
                Text = "This is a sample quiz created by DbSeeder class for testing purposes.",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                var question = new Question()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample questions created by DbSeeder",
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                };
                dbContext.Questions.Add(question);
                dbContext.SaveChanges();

                for (int i2 = 0; i2 < numberOfAnswersPerQuestion; i2++)
                {
                    var e2 = dbContext.Answers.Add(new Answer()
                    {
                        QuestionId = question.Id,
                        Text = "This is a sample answer created by DbSeeder",
                        Value = i2,
                        CreatedDate = createdDate,
                        LastModifiedDate = createdDate
                    });
                }
            }

            for (int i = 0; i < numberOfResults; i++)
            {
                dbContext.Results.Add(new Result()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample result created by DbSeeser",
                    MinValue = 0,
                    // Max Value is equal to number of answers per question * max answer value
                    MaxValue = numberOfAnswersPerQuestion * 2,
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                });
            }
            dbContext.SaveChanges();
        }
        #endregion
    }
}
