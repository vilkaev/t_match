// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Newtonsoft.Json;

using System.Text.Unicode;
using t_match;

//Console.WriteLine("Started calculation");
//string r = File.ReadAllText(@"D:\gt_loc\t_match\t_match\example.json");
//Request Request = new Request() { Body = r };
//string r1 = JsonConvert.SerializeObject(Request);
//File.WriteAllText(@"D:\gt_loc\t_match\t_match\exampleFull.json", r1);
//return;

Handler handler = new Handler();
Request req = new Request() { Body = "" }; // zero body = generate random
var response = handler.FunctionHandler(req); ;
Console.WriteLine(response);

// test iterations
//RequestSet set = new RequestSet() { Players = Player.CreateTestBasket(6), TeamsCount = 2 };
