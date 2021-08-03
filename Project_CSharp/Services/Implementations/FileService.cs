using Project_CSharp.Models;
using Project_CSharp.Repositories.Implementations;
using Project_CSharp.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Project_CSharp.Services
{
    public class FileService : IFileService
    {  
        public readonly static string UsersFileName = "Data/Users.csv";

        public static void ReadUsersFromFile()
        {
            var fileInfo = new FileInfo(UsersFileName);
            if (fileInfo.Exists)
            {
                using var streamReader = new StreamReader(fileInfo.OpenRead());

                string line;
                while (null != (line = streamReader.ReadLine()))
                {
                    string[] properties = line.Split(",");
                    var user = new User()
                    {
                        Email = properties[0],
                        Password = properties[1],
                        Salt = properties[2],
                        RefreshToken = properties[3],
                        RefreshTokenExpiration = DateTime.Parse(properties[4])
                    };

                    UserRepository.Users.Add(user);
                }
            }
        }

        //Overwriting is a slightly time-consuming thing if list of users is huge.
        public async Task OverwriteFileAsync()
        {
            using var streamWriter = new StreamWriter(UsersFileName);
            foreach (User user in UserRepository.Users)
                await streamWriter.WriteLineAsync(user.ToString());
        }

        public async Task WriteUserToFileAsync(User user)
        {
            using var streamWriter = new StreamWriter(UsersFileName, true);
            await streamWriter.WriteLineAsync(user.ToString());
        }
    }
}
