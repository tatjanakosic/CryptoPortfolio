using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    class Program
    {
        const string emailFilePath = "emails.txt";
        const string userFilePath = "users.txt";

        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();

            string address = "net.tcp://localhost:4008/ISendEmail";
            ServiceHost host = new ServiceHost(typeof(SendEmail));
            host.AddServiceEndpoint(typeof(ISendEmail), binding, address);
            host.Open();

            Console.WriteLine("Welcome to the Email Management System!");

            if (AuthenticateUser())
            {
                DisplayMenu();
            }
            else
            {
                Console.WriteLine("Authentication failed. Exiting the application.");
            }
        }

        static void SendEmailsToListsEmails() {
            var emails = LoadEmails();
            SendEmail se = new SendEmail();

            foreach (var e in emails) {
                //se.Send();
            }

            Console.WriteLine("End");
            
        }


        static bool AuthenticateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var users = LoadUsers();

            return users.Any(u => u.Username == username && u.Password == password);
        }

        static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\n1. Add Email Address");
                Console.WriteLine("2. Update Email Address");
                Console.WriteLine("3. List Email Addresses");
                Console.WriteLine("4. Exit");
                Console.WriteLine("5. Send Emails");

                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddEmailAddress();
                        break;
                    case "2":
                        UpdateEmailAddress();
                        break;
                    case "3":
                        ListEmailAddresses();
                        break;
                    case "4":
                        return;
                    case "5":
                        SendEmailsToListsEmails();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddEmailAddress()
        {
            Console.Write("Enter email address to add: ");
            string email = Console.ReadLine();

            var emails = LoadEmails();
            emails.Add(email);
            SaveEmails(emails);

            Console.WriteLine("Email address added successfully.");
        }

        static void UpdateEmailAddress()
        {
            Console.Write("Enter email address ID to update: ");
            int id = int.Parse(Console.ReadLine());

            var emails = LoadEmails();
            if (id >= 1 && id <= emails.Count)
            {
                Console.Write("Enter new email address: ");
                emails[id - 1] = Console.ReadLine();
                SaveEmails(emails);
                Console.WriteLine("Email address updated successfully.");
            }
            else
            {
                Console.WriteLine("Email address not found.");
            }
        }

        static void ListEmailAddresses()
        {
            var emails = LoadEmails();

            Console.WriteLine("\nEmail Addresses:");
            for (int i = 0; i < emails.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {emails[i]}");
            }
        }

        static List<string> LoadEmails()
        {
            if (!File.Exists(emailFilePath))
            {
                return new List<string>();
            }

            return File.ReadAllLines(emailFilePath).ToList();
        }

        static void SaveEmails(List<string> emails)
        {
            File.WriteAllLines(emailFilePath, emails);
        }

        static List<EmailAdmin> LoadUsers()
        {
            if (!File.Exists(userFilePath))
            {
                return new List<EmailAdmin>();
            }

            return File.ReadAllLines(userFilePath)
                .Select(line => line.Split(','))
                .Select(parts => new EmailAdmin { Username = parts[0], Password = parts[1] })
                .ToList();
        }
    }
}
