using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NLog;
using PizzaStore.Library;

namespace PizzaStore.UI
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Application start");

            var serializer = new XmlSerializer(typeof(List<User>));
            List<User> UserList = new List<User>();
            Dictionary<string, User> users = new Dictionary<string, User>();
            Dictionary<string, Location> Location_dict = new Dictionary<string, Location>();


            try
            {
                using (var stream = new FileStream("data.xml", FileMode.Open))
                {
                    UserList = (List<User>)serializer.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Saved data not found");
            }

            foreach (var item in UserList)
            {
                string firstlast = item.First + item.Last;
                users.Add(firstlast, item);
            }
            
            //try
            //{
            //    Location_dict = DeserializeLocationsFromFile("UserData.xml");
            //}
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine("Data not fount.");
            //}
            //catch(IOException ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

         
            

            //try
            //{
            //    users = DeserializeUsersFromFile(@"data.xml");
            //}
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine("Data not found.");
            //}
            //catch(IOException ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            if (Location_dict.Count == 0)
            {
                Location_dict.Add("1", new Location("1"));
                Location_dict.Add("2", new Location("2"));
                Location_dict.Add("3", new Location("3"));
                Location_dict.Add("4", new Location("4"));
            }

            bool running = true;
            //bool stillOrdering = true;
            string FirstName = "";
            string LastName = "";

            Console.WriteLine("Please enter your Fist Name: ");
            FirstName = Console.ReadLine().ToLower().Replace(" ",string.Empty);
            Console.WriteLine("Please enter your Last Name: ");
            LastName = Console.ReadLine().ToLower().Replace(" ", string.Empty);
            string FirstLast = FirstName + LastName;

            while(!users.ContainsKey(FirstName + LastName))
            {
                Console.WriteLine("Welcome new user. Please enter your preffered store");
                while (true)
                {
                    Console.WriteLine("Stores are: 1, 2, 3, 4");
                    Console.WriteLine("Preffered store:");
                    string input = Console.ReadLine();
                    if (Location_dict.ContainsKey(input))
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
                        int numberofpizza = 0;
                        switch (Input)
                        {
                            case "preferred":
                                if (users[FirstLast].OrderHistory.Count < 1)
                                {
                                    Console.WriteLine("You have no previous orders, preferred order is not possible at this time.");
                                    break;
                                }
                                PizzaPie PrefOrderPizza = new PizzaPie();
                                string OrderToppings = "";
                                foreach (var item in users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Pizza.Toppings)
                                {
                                    OrderToppings = OrderToppings + ", " + item;
                                }
                                Console.WriteLine($"Your preferred order is size:{users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count-1].Pizza.Size} Sauce: {users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count-1].Pizza.Sauce} Toppings: {OrderToppings} Cost: {users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Pizza.Price}");
                                Order PrefOrder = new Order(users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].HowManyPizzas, users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Toppings, users[FirstLast], users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Location);

                                PrefOrderPizza.MakePizza(users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Pizza.Sauce, users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Pizza.Toppings, users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Pizza.Size);

                                PrefOrder.AddPizzaToOrder(PrefOrderPizza);

                                PrefOrder.UpdateToppings(users[FirstLast].OrderHistory[users[FirstLast].OrderHistory.Count - 1].Pizza.Toppings);

                                users[FirstName + LastName].SetOrderHistory(PrefOrder);

                                Console.WriteLine("Order has been created!");
                                break;

                            case "new":
                                Console.WriteLine("How many pizzas will you be ordering?");
                                string input = Console.ReadLine();
                                numberofpizza = Convert.ToInt32(input);
                                HashSet<string> toppings = new HashSet<string>();
                                try
                                {
                                    Order TestOrder = new Order(numberofpizza, toppings, users[FirstLast], users[FirstLast].PrefLocation);
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    break;
                                }

                                Order New_Order = new Order(numberofpizza, toppings, users[FirstLast], users[FirstLast].PrefLocation);

                                Console.Write("What size pizza would you like? (S, M, L):");
                                string pizzaSize = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                Console.Write("Would you like Sauce? y/n:");
                                string sauceinput = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                bool sauce;

                                if (sauceinput == "y")
                                {
                                    sauce = true;
                                }
                                else
                                {
                                    sauce = false;
                                }
                                while (true)
                                {
                                    Console.WriteLine("Please type the toppings you want one at a time.");
                                    Console.WriteLine("Possible toppings include: Pepperoni, Onion, Ham, Sausage, Chicken, Pepper, Pineapple, and BBQChicken");
                                    Console.Write("When you are done adding your toppings type \"done\":");

                                    string topping = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                    if (topping == "done")
                                    {
                                        break;
                                    }
                                    toppings.Add(topping);

                                }

                                PizzaPie new_pizza = new PizzaPie();
                                new_pizza.MakePizza(sauce, toppings, pizzaSize);

                                new_pizza.PricePizza(pizzaSize, toppings);

                                New_Order.AddPizzaToOrder(new_pizza);
                              
                                New_Order.UpdateToppings(toppings);

                                users[FirstName + LastName].SetOrderHistory(New_Order);

                                
                                Console.WriteLine();

                                
                                break;
                        }
                        break;

                    case "change location":
                        while(true)
                        {
                            Console.Write("Please enter the store ID you would like to change to, 1, 2, 3, or 4:");
                            string input = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                            if (Location_dict.ContainsKey(input))
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
                        List<User> userList = new List<User>();

                        foreach (KeyValuePair<string, User> item in users)
                        {
                            userList.Add(item.Value);
                        }
                        try
                        {
                            using (var stream = new FileStream("data.xml", FileMode.Create))
                            {
                                serializer.Serialize(stream, userList);
                            }
                        }
                        catch(IOException ex)
                        {
                            Console.WriteLine($"Error during save: {ex.Message}");
                        }

                        //SerializeUsersToFile(@"data.xml", users);
                        //SerializeLocationsToFile("LocationData.xml", Location_dict);
                        break;
                }
            }

        }



        ////code to serialize data to XML file
        //private static void SerializeUsersToFile(string fileName, Dictionary<string, User> user)
        //{
        //    List<User> userList = new List<User>();

        //    foreach (KeyValuePair<string, User> item in user)
        //    {
        //        userList.Add(item.Value);
        //    }

        //    var serializer = new XmlSerializer(typeof(List<User>));
        //    FileStream fileStream = null;
        //    try
        //     {
        //        fileStream = new FileStream(fileName, FileMode.Create);
        //        serializer.Serialize(fileStream, userList);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine($"Path {fileName} was too long! {ex.Message}");
        //    }
        //    finally
        //    {
        //        fileStream.Dispose();
        //    }
        //}
        ////code to deserialize XML file but need to ask about none async way
        //private static Dictionary<string, User> DeserializeUsersFromFile(string fileName)
        //{
        //    var serializer = new XmlSerializer(typeof(List<User>));
        //    FileStream fileStream = null;
        //    Dictionary<string, User> users = new Dictionary<string, User>();
        //    try
        //    {
        //        fileStream = new FileStream(fileName, FileMode.Open);
        //        List<User> result = (List<User>)serializer.Deserialize(fileStream);
        //        foreach (var item in result)
        //        {
        //            string firstlast = item.First + item.Last;
        //            users.Add(firstlast, item);
        //        }
        //        return users;
        //    }
        //    finally
        //    {
        //        fileStream.Dispose();
        //    }
        //}



        //private static void SerializeLocationsToFile(string fileName, Dictionary<string, Location> user)
        //{
        //    List<Location> LocationList = new List<Location>();

        //    foreach (KeyValuePair<string, Location> item in user)
        //    {
        //        LocationList.Add(item.Value);
        //    }

        //    var serializer = new XmlSerializer(typeof(List<User>));
        //    FileStream fileStream = null;
        //    try
        //    {
        //        fileStream = new FileStream(fileName, FileMode.Create);
        //        serializer.Serialize(fileStream, LocationList);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine($"Path {fileName} was too long! {ex.Message}");
        //    }
        //    finally
        //    {
        //        fileStream.Dispose();
        //    }
        //}
        ////code to deserialize XML file but need to ask about none async way
        //private static Dictionary<string, Location> DeserializeLocationsFromFile(string fileName)
        //{
        //    var serializer = new XmlSerializer(typeof(List<Location>));
        //    FileStream fileStream = null;
        //    Dictionary<string, Location> Locations = new Dictionary<string, Location>();
        //    try
        //    {
        //        fileStream = new FileStream(fileName, FileMode.Open);
        //        List<Location> result = (List<Location>)serializer.Deserialize(fileStream);
        //        foreach (var item in result)
        //        {
        //            string storenumber = item.StoreNumber;
        //            Locations.Add(storenumber, item);
        //        }
        //        return Locations;
        //    }
        //    finally
        //    {
        //        fileStream.Dispose();
        //    }
        //}
    }
}
