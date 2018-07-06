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
            Dictionary<string, Location> setOfLocations = new Dictionary<string, Location>();
            setOfLocations.Add("1", new Location("1"));

            //Check to see if locations have already been created
            foreach (var item in setOfLocations)
            {
                if (true)
                {
                    
                }
            }
            
            Dictionary<string, User> users = new Dictionary<string, User>();
            //SerializeToFile("data.xml", users[]);

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
            //users.AddRange(result);

            bool running = true;
            //bool stillOrdering = true;
            string FirstName = "";
            string LastName = "";
            int pizzasOrdered = 0;

            Console.WriteLine("Please enter your Fist Name: ");
            FirstName = Console.ReadLine().ToLower();
            Console.WriteLine("Please enter your Last Name: ");
            LastName = Console.ReadLine().ToLower();
            string FirstLast = FirstName + LastName;

            while(!users.ContainsKey(FirstName + LastName))
            {
                Console.WriteLine("Welcome new user. Please enter your preffered store");
                while (true)
                {
                    Console.WriteLine("Stores are: 1, 2, 3, 4");
                    Console.WriteLine("Preffered store:");
                    string input = Console.ReadLine();
                    if (setOfLocations.ContainsKey(input))
                    {
                        User newUser = new User(FirstName, LastName, input);
                        users.Add(FirstName + LastName, newUser);
                        Console.WriteLine("Preferred location has been updated");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid store ID");
                    }
                }
            }

            Console.WriteLine($"Welcome {FirstName} {LastName}. Type a command for what you would like to do.");

            while (running == true)
            {
                string Input = "";
                Console.WriteLine("Commands are: order, finalize order, change location, quit");
                Input = Console.ReadLine();
                switch (Input)
                {
                    case "order":
                        Console.WriteLine("Would you like your preferred order or a new order? (type \"preferred\" for preferred order, or \"new\" for a new order");
                        Input = Console.ReadLine().ToLower();
                        switch (Input)
                        {
                            case "preferred":
                                Console.WriteLine("What size pizza would you like? (S, M, L):");
                                string PizzaSize = Console.ReadLine().ToLower();
                                PizzaPie pref_pizza = new PizzaPie();
                                pref_pizza.MakePizza(users[FirstLast].PrefOrder.Pizza.Sauce, users[FirstLast].PrefOrder.Toppings, PizzaSize);
                                pizzasOrdered += 1;
                                break;
                            case "new":
                                HashSet<string> toppings = new HashSet<string>();
                                Console.Write("What size pizza would you like? (S, M, L):");
                                string pizzaSize = Console.ReadLine().ToLower();
                                Console.Write("Would you like Sauce? y/n:");
                                string sauce = Console.ReadLine().ToLower();
                                while (true)
                                {
                                    Console.WriteLine("Please type the toppings you want one at a time.");
                                    Console.WriteLine("Possible toppings include: Pepperoni, Onion, Ham, Sausage, Chicken, Pepper, Pineapple, and BBQChicken");
                                    Console.Write("When you are done adding your toppings type \"done\":");

                                    string topping = Console.ReadLine().ToLower();
                                    if (topping == "done")
                                    {
                                        break;
                                    }
                                    toppings.Add(topping);

                                }
                                //need to think of a way to count pizzas while still making new orders
                                PizzaPie new_pizza = new PizzaPie();
                                //new_pizza.MakePizza();
                                try
                                {
                                    new Order(1, toppings, users[FirstLast], users[FirstLast].PrefLocation);
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.WriteLine("");

                                //users[FirstName + LastName].SetOrderHistory(new_order);

                                break;
                        }
                        break;

                    case "change location":
                        while(true)
                        {
                            Console.Write("Please enter the store ID you would like to change to, 1, 2, 3, or 4:");
                            string input = Console.ReadLine();
                            if (setOfLocations.ContainsKey(input))
                            {
                                users[FirstLast].PrefLocation.StoreNumber = input;
                                Console.WriteLine("Preferred location has been updated");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("That is not a valid store ID");
                            }
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
    }
}
