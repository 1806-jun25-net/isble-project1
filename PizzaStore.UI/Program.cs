using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PizzaStore.Library;

namespace PizzaStore.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<User>();
            SerializeToFile("data.xml", users);
            //IEnumerable<User> desList = DeserializeFromFile("data.xml");
            IEnumerable<User> result = new List<User>();
            //This is for Async way, need to ask about regular way.
            try
            {
                //result = desList.Result;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("File wasn't found");
            }

            //adds results of deserialization to a list of users, need to change how to
            //extract specific information from XML file.
            users.AddRange(result);

            bool running = true;
            bool stillOrdering = true;
            string FirstName = "";
            string LastName = "";

            Console.WriteLine("Please enter your Fist Name: ");
            FirstName = Console.ReadLine().ToLower();
            Console.WriteLine("Please enter your Last Name: ");
            LastName = Console.ReadLine().ToLower();

            //while loop to run program till user is done
            //user ends by typing quit
            while (running == true)
            {
                string Input = "";
                Console.WriteLine($"Welcome {FirstName} {LastName}. Type a command for what you would like to do.");
                Console.WriteLine("Commands are: order, change location, quit");
                Input = Console.ReadLine();
                switch (Input)
                {
                    case "order":
                        Console.WriteLine("Would you like your prefered order or a new order? (type \"prefered\" for prefered order, or \"new\"for a new order");
                        string Input2 = Console.ReadLine().ToLower();
                        switch (Input2)
                        {
                            case "prefered":
                                Console.WriteLine("What size pizza would you like?");
                                String PizzaSize = Console.ReadLine();
                                break;
                            case "new":
                                while (stillOrdering)
                                {

                                    try
                                    {
                                        new Order(13, null, null, null);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                Console.WriteLine("")
                                
                                break;
                        }
                        break;

                    case "quit":
                        running = false;
                        break;


                }
            }
        }
        //code to serialize data to XML file
        private static void SerializeToFile(string fileName, List<User> user)
        {
            var serializer = new XmlSerializer(typeof(List<User>));
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fileStream, user);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Path {fileName} was too long! {ex.Message}");
            }
            finally
            {
                fileStream.Dispose();
            }
        }
        //code to deserialize XML file but need to ask about none async way
        private static IEnumerable<User> DeserializeFromFile(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<User>));
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
                var result = (IEnumerable<User>)serializer.Deserialize(fileStream);
                return result;
            }
            finally
            {
                fileStream.Dispose();
            }
        }
        //used to add first user, myself, to the user list.
        //private void FillList(List<User> list)
        //{
        //    list.Add(new User
        //    {
        //        First = "Joseph",
        //        Last = "Isble",
        //        PrefLocation = new Location
        //        {
        //            OrderHistory = new List<Order>
        //        },
        //            new Order
        //            {

        //            }
        //        );
        //}

    }
}
