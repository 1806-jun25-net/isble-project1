using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using PizzaStore.Data;
using PizzaStore.Library;
using PizzaStore.Library.PizzaStoreRepo;

namespace PizzaStore.UI
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Application start");


            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzastoreDB"));
            var options = optionsBuilder.Options;

            var dbContext = new PizzaStoreDbContext(options);
            var PizzaStoreRepository = new PizzaStoreRepository(dbContext);


            var userSerializer = new XmlSerializer(typeof(List<User>));
            var locationSerializer = new XmlSerializer(typeof(List<Location>));
            List<User> UserList = new List<User>();
            List<Location> LocationList = new List<Location>();
            Dictionary<string, User> Users_Dict = new Dictionary<string, User>();
            Dictionary<int, Location> Location_Dict = new Dictionary<int, Location>();


            try
            {
                using (var stream = new FileStream("User_data.xml", FileMode.Open))
                {
                    UserList = (List<User>)userSerializer.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Saved data not found");
            }
            foreach (var item in UserList)
            {
                string firstlast = item.First + item.Last;
                Users_Dict.Add(firstlast, item);
            }


            try
            {
                using (var stream = new FileStream("Location_data.xml", FileMode.Open))
                {
                    LocationList = (List<Location>)locationSerializer.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Saved data not found");
            }
            foreach (var item in LocationList)
            {
                int Location = item.StoreNumber;
                Location_Dict.Add(Location, item);
            }

            if (Location_Dict.Count == 0)
            {
                Location_Dict.Add(1, new Location(1));
                Location_Dict.Add(2, new Location(2));
                Location_Dict.Add(3, new Location(3));
                Location_Dict.Add(4, new Location(4));
            }
            string whichProgram = "";
            Console.WriteLine("Which program would you like to run (DB or XML)");
            whichProgram = Console.ReadLine().ToLower();

            if (whichProgram == "db")
            {
                bool running = true;
                //bool stillOrdering = true;
                string FirstName = "";
                string LastName = "";

                Console.WriteLine("Please enter your Fist Name: ");
                FirstName = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                Console.WriteLine("Please enter your Last Name: ");
                LastName = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                string FirstLast = FirstName + LastName;
                int userstore;

                while (!PizzaStoreRepository.IsUserInDB(FirstName, LastName))
                {
                    Console.WriteLine("Welcome new user. Please enter your preffered store");
                    while (true)
                    {
                        Console.WriteLine("Stores are: 1, 2, 3, 4");
                        Console.WriteLine("Preffered store:");
                        string input = Console.ReadLine();
                        userstore = Convert.ToInt32(input);
                        if (PizzaStoreRepository.IsLocationInDB(userstore))
                        {
                            User newUser = new User(FirstName, LastName, userstore);
                            PizzaStoreRepository.AddUserToDB(newUser);
                            PizzaStoreRepository.Save();
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
                //Console.WriteLine($"There is {Location_Dict["4"].Pepper} left in inventory at location 4");
                while (running == true)
                {
                    User NewUser = PizzaStoreRepository.GetUser(FirstName,LastName,PizzaStoreRepository.GetUserLocation(FirstName, LastName));
                    string Input = "";
                    Console.WriteLine("Commands are: order, Order history, change location, quit");
                    Input = Console.ReadLine().ToLower();
                    switch (Input)
                    {
                        case "order":
                            Console.WriteLine("Would you like your preferred order or a new order? (type \"preferred\" for preferred order, or \"new\" for a new order");
                            Input = Console.ReadLine().ToLower();
                            int NumberOfPizza = 0;
                            switch (Input)
                            {
                                case "preferred":
                                    if (!PizzaStoreRepository.DoesUserHavePreviousOrders(NewUser))
                                    {
                                        Console.WriteLine("You have no previous orders, preferred order is not possible at this time.");
                                        break;
                                    }
                                    Library.PizzaPie newPizza = new Library.PizzaPie();
                                    newPizza.MakePizza(PizzaStoreRepository.GetUserOrders(NewUser).Pizza.Sauce, PizzaStoreRepository.GetUserOrders(NewUser).Pizza.Toppings, PizzaStoreRepository.GetUserOrders(NewUser).Pizza.Size);
                                    Order NewOrder = new Order(PizzaStoreRepository.GetUserOrders(NewUser).HowManyPizzas, PizzaStoreRepository.GetUserOrders(NewUser).Pizza.Toppings, NewUser,NewUser.PrefLocation, PizzaStoreRepository.GetUserOrders(NewUser).Pizza);

                                    NewOrder.AddPizzaToOrder(newPizza);
                                    NewOrder.UpdateToppings(newPizza.Toppings);
                                    NewOrder.UpdatePriceOfOrder(newPizza.Price);
                                    NewOrder.TimepizzaWasOrdered();

                                    int new_user_id = PizzaStoreRepository.GetUserID(NewUser);

                                    NewOrder.UpdateUserId(new_user_id);

                                    PizzaStoreRepository.AddOrderToDB(NewOrder);
                                    PizzaStoreRepository.Save();

                                    Console.WriteLine("Order has been created!");
                                    break;

                                case "new":
                                    Console.WriteLine("How many pizzas will you be ordering?");
                                    string input = Console.ReadLine().ToLower();
                                    NumberOfPizza = Convert.ToInt32(input);
                                    HashSet<string> toppings = new HashSet<string>();
                                    Library.PizzaPie NewPizza = new Library.PizzaPie();
                                    try
                                    {
                                        Order TestOrder = new Order(NumberOfPizza, toppings, Users_Dict[FirstLast], Location_Dict[Users_Dict[FirstLast].PrefLocation].StoreNumber, NewPizza);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        break;
                                    }

                                    Order NewOrder = new Order(NumberOfPizza, toppings, Users_Dict[FirstLast], Location_Dict[Users_Dict[FirstLast].PrefLocation].StoreNumber, NewPizza);

                                    Console.Write("What size pizza would you like? (S, M, L):");
                                    string pizzaSize = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                    Console.Write("Would you like Sauce? y/n:");
                                    string sauceinput = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                    bool sauce;

                                    if (sauceinput == "y")
                                    {
                                        sauce = true;
                                    }
                                    else if (sauceinput == "n")
                                    {
                                        sauce = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("invalid input, please create your order again");
                                        break;
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


                                    try
                                    {
                                        NewPizza.MakePizza(sauce, toppings, pizzaSize);
                                    }
                                    catch (ArgumentException)
                                    {
                                        Console.WriteLine("Invalid topping was removed from order.");
                                    }


                                    NewPizza.PricePizza(pizzaSize, toppings, NumberOfPizza);
                                    if (NewPizza.Price > 500)
                                    {
                                        Console.WriteLine("Price of pizza is too high, canceling order");
                                        break;
                                    }

                                    NewOrder.AddPizzaToOrder(NewPizza);
                                    NewOrder.UpdateToppings(toppings);
                                    NewOrder.UpdatePriceOfOrder(NewPizza.Price);
                                    NewOrder.TimepizzaWasOrdered();

                                    Location_Dict[location].DecreaseInventory(NewOrder);

                                    Users_Dict[FirstLast].SetOrderHistory(NewOrder);


                                    Console.WriteLine("Order has been made");

                                    Location_Dict[location].SetOrderHistory(NewOrder);

                                    Console.WriteLine();

                                    break;
                            }
                            break;

                        case "change location":
                            while (true)
                            {
                                Console.Write("Please enter the store ID you would like to change to, 1, 2, 3, or 4:");
                                string input = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                int IntInput = Convert.ToInt32(input);
                                if (Location_Dict.ContainsKey(IntInput))
                                {
                                    Users_Dict[FirstLast].PrefLocation = IntInput;
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
                            List<Location> locationList = new List<Location>();

                            foreach (KeyValuePair<string, User> item in Users_Dict)
                            {
                                userList.Add(item.Value);
                            }
                            try
                            {
                                using (var stream = new FileStream("User_data.xml", FileMode.Create))
                                {
                                    userSerializer.Serialize(stream, userList);
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine($"Error during save: {ex.Message}");
                            }

                            foreach (KeyValuePair<int, Location> item in Location_Dict)
                            {
                                locationList.Add(item.Value);
                            }
                            try
                            {
                                using (var stream = new FileStream("Location_data.xml", FileMode.Create))
                                {
                                    locationSerializer.Serialize(stream, locationList);
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine($"Error during save: {ex.Message}");
                            }
                            break;
                    }
                }
            }





            //HashSet<string> new_toppings = new HashSet<string>() { "onion", "ham" };


            //User new_user = new User(FirstName, LastName, 1);
            //PizzaStoreRepository.AddUserToDB(new_user);
            //PizzaStoreRepository.Save();
            //Library.PizzaPie new_pizza = new Library.PizzaPie();
            //new_pizza.MakePizza(true, new_toppings, "m");
            //new_pizza.PricePizza("m", new_toppings, 10);
            //Order new_order = new Order(10, new_toppings, new_user, new_user.PrefLocation, new_pizza);



            //new_order.AddPizzaToOrder(new_pizza);
            //new_order.UpdateToppings(new_toppings);
            //new_order.UpdatePriceOfOrder(new_pizza.Price);
            //new_order.TimepizzaWasOrdered();

            //int new_user_id = PizzaStoreRepository.GetUserID(new_user);

            //new_order.UpdateUserId(new_user_id);

            ////PizzaStoreRepository.;
            ////PizzaStoreRepository.AddUserToDB(new_user);
            ////PizzaStoreRepository.Save();
            //PizzaStoreRepository.AddOrderToDB(new_order);
            //PizzaStoreRepository.Save();
            //var ListOfUsers = PizzaStoreRepository.GetUsers();

            //foreach (var item in ListOfUsers)
            //{
            //    Console.WriteLine(item);

            //}
            //Console.ReadLine();

            else
            {


                bool running = true;
                //bool stillOrdering = true;
                string FirstName = "";
                string LastName = "";

                Console.WriteLine("Please enter your Fist Name: ");
                FirstName = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                Console.WriteLine("Please enter your Last Name: ");
                LastName = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                string FirstLast = FirstName + LastName;

                while (!Users_Dict.ContainsKey(FirstLast))
                {
                    Console.WriteLine("Welcome new user. Please enter your preffered store");
                    while (true)
                    {
                        Console.WriteLine("Stores are: 1, 2, 3, 4");
                        Console.WriteLine("Preffered store:");
                        string input = Console.ReadLine();
                        int loc = Convert.ToInt32(input);
                        if (Location_Dict.ContainsKey(loc))
                        {
                            User newUser = new User(FirstName, LastName, loc);
                            Users_Dict.Add(FirstLast, newUser);
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
                //Console.WriteLine($"There is {Location_Dict["4"].Pepper} left in inventory at location 4");
                while (running == true)
                {
                    int location = Users_Dict[FirstLast].PrefLocation;
                    string Input = "";
                    Console.WriteLine("Commands are: order, Order history, change location, quit");
                    Input = Console.ReadLine().ToLower();
                    switch (Input)
                    {
                        case "order":
                            Console.WriteLine("Would you like your preferred order or a new order? (type \"preferred\" for preferred order, or \"new\" for a new order");
                            Input = Console.ReadLine().ToLower();
                            int NumberOfPizza = 0;
                            switch (Input)
                            {
                                case "preferred":
                                    if (Users_Dict[FirstLast].OrderHistory.Count < 1)
                                    {
                                        Console.WriteLine("You have no previous orders, preferred order is not possible at this time.");
                                        break;
                                    }
                                    Library.PizzaPie PrefOrderPizza = new Library.PizzaPie();
                                    string OrderToppings = "";
                                    foreach (var item in Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Toppings)
                                    {
                                        OrderToppings += item + ", ";
                                    }
                                    Console.WriteLine($"Your preferred order is size:{Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Size} Sauce: {Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Sauce} Toppings: {OrderToppings} Cost: {Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Price}");
                                    Order PrefOrder = new Order(Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].HowManyPizzas, Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Toppings, Users_Dict[FirstLast], Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Location, PrefOrderPizza);

                                    PrefOrderPizza.MakePizza(Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Sauce, Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Toppings, Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Size);

                                    PrefOrder.AddPizzaToOrder(PrefOrderPizza);

                                    PrefOrder.UpdateToppings(Users_Dict[FirstLast].OrderHistory[Users_Dict[FirstLast].OrderHistory.Count - 1].Pizza.Toppings);
                                    PrefOrder.Price = PrefOrderPizza.Price;
                                    PrefOrder.TimepizzaWasOrdered();

                                    Location_Dict[location].DecreaseInventory(PrefOrder);


                                    Users_Dict[FirstLast].SetOrderHistory(PrefOrder);

                                    Console.WriteLine("Order has been created!");
                                    break;

                                case "new":
                                    Console.WriteLine("How many pizzas will you be ordering?");
                                    string input = Console.ReadLine().ToLower();
                                    NumberOfPizza = Convert.ToInt32(input);
                                    HashSet<string> toppings = new HashSet<string>();
                                    Library.PizzaPie NewPizza = new Library.PizzaPie();
                                    try
                                    {
                                        Order TestOrder = new Order(NumberOfPizza, toppings, Users_Dict[FirstLast], Location_Dict[Users_Dict[FirstLast].PrefLocation].StoreNumber, NewPizza);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        break;
                                    }

                                    Order NewOrder = new Order(NumberOfPizza, toppings, Users_Dict[FirstLast], Location_Dict[Users_Dict[FirstLast].PrefLocation].StoreNumber, NewPizza);

                                    Console.Write("What size pizza would you like? (S, M, L):");
                                    string pizzaSize = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                    Console.Write("Would you like Sauce? y/n:");
                                    string sauceinput = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                    bool sauce;

                                    if (sauceinput == "y")
                                    {
                                        sauce = true;
                                    }
                                    else if (sauceinput == "n")
                                    {
                                        sauce = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("invalid input, please create your order again");
                                        break;
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


                                    try
                                    {
                                        NewPizza.MakePizza(sauce, toppings, pizzaSize);
                                    }
                                    catch (ArgumentException)
                                    {
                                        Console.WriteLine("Invalid topping was removed from order.");
                                    }


                                    NewPizza.PricePizza(pizzaSize, toppings, NumberOfPizza);
                                    if (NewPizza.Price > 500)
                                    {
                                        Console.WriteLine("Price of pizza is too high, canceling order");
                                        break;
                                    }

                                    NewOrder.AddPizzaToOrder(NewPizza);
                                    NewOrder.UpdateToppings(toppings);
                                    NewOrder.UpdatePriceOfOrder(NewPizza.Price);
                                    NewOrder.TimepizzaWasOrdered();

                                    Location_Dict[location].DecreaseInventory(NewOrder);

                                    Users_Dict[FirstLast].SetOrderHistory(NewOrder);


                                    Console.WriteLine("Order has been made");

                                    Location_Dict[location].SetOrderHistory(NewOrder);

                                    Console.WriteLine();

                                    break;
                            }
                            break;

                        case "change location":
                            while (true)
                            {
                                Console.Write("Please enter the store ID you would like to change to, 1, 2, 3, or 4:");
                                string input = Console.ReadLine().ToLower().Replace(" ", string.Empty);
                                int IntInput = Convert.ToInt32(input);
                                if (Location_Dict.ContainsKey(IntInput))
                                {
                                    Users_Dict[FirstLast].PrefLocation = IntInput;
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
                            List<Location> locationList = new List<Location>();

                            foreach (KeyValuePair<string, User> item in Users_Dict)
                            {
                                userList.Add(item.Value);
                            }
                            try
                            {
                                using (var stream = new FileStream("User_data.xml", FileMode.Create))
                                {
                                    userSerializer.Serialize(stream, userList);
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine($"Error during save: {ex.Message}");
                            }

                            foreach (KeyValuePair<int, Location> item in Location_Dict)
                            {
                                locationList.Add(item.Value);
                            }
                            try
                            {
                                using (var stream = new FileStream("Location_data.xml", FileMode.Create))
                                {
                                    locationSerializer.Serialize(stream, locationList);
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine($"Error during save: {ex.Message}");
                            }
                            break;
                    }
                }
            }
        } 
    }
}
