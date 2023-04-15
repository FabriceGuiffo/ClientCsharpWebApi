using System.Net;

namespace consumeWebApi
{
    internal class Program
    {
        //the instanciation of a httpclient

        static HttpClient httpclient = new HttpClient();

        //la methode post
        static async Task<Uri> CreateClient(Client client)
        {
            HttpResponseMessage response =await httpclient.PostAsJsonAsync("api/Clients1", client);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        static async Task<List<Client>> GetClients()
        {
            List<Client> allclients = new List<Client>();
            HttpResponseMessage response = await httpclient.GetAsync("api/Clients1");
            if (response.IsSuccessStatusCode)
            {
                allclients = await response.Content.ReadAsAsync<List<Client>>();
            }
            return allclients;

        }

        //la methode get
        static async Task<Client> GetClient(string clientpath)
        {
            Client client = null;
            HttpResponseMessage response = await httpclient.GetAsync(clientpath);
            if (response.IsSuccessStatusCode)
            {
                //deserialise the Client object
                client =await response.Content.ReadAsAsync<Client>();
            }
            return client;
        }

        //la methode update/put
        static async Task UpdateClient(Client client)
        {
            HttpResponseMessage response = await httpclient.PutAsJsonAsync<Client>($"api/Clients1/{client.ClientID}", client);
            response.EnsureSuccessStatusCode();

           //we return void
            return;
        }

        //la methode delete
        static async Task<HttpStatusCode> DeleteClient(int id)
        {
            HttpResponseMessage response = await httpclient.DeleteAsync($"api/Clients1/{id}");
            return response.StatusCode;
        }
        static void Main(string[] args)
        {
            //on lance la methode commandline runner qui gere l'execution de notre systeme
            //il s'agira d'une methode async mais qui retourne void Task parce quelle manipule des methodes async
            RunOps().GetAwaiter().GetResult();
        }

        static async Task RunOps()
        {
            //we start by base config of the http client
            //first is the baseIP
            httpclient.BaseAddress = new Uri("https://localhost:7085/");
            //then we define data format as json in header
            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                /* //we start by creating a client
                 Client testclient = new Client() { name = "Sophie", christianname = "Ngo Bicec", age = 23, email = "ngomalep@yahoo.com" };

                 var urlclient = await CreateClient(testclient);
                 Console.WriteLine($"nouveau client creer a l'url: {urlclient}");
                 Console.WriteLine();

                 var taillechaine=urlclient.ToString().Length;
                 int lastelem = Int32.Parse(urlclient.ToString().Substring(taillechaine - 1));
                 Console.WriteLine($"l'identifiants de l'elememt qu'on vient d'ajouter {lastelem}");


                 //on requete le client actuel a partir de l'api
                 Client querriedclient = await GetClient($"api/Clients1/{lastelem}");
                 querriedclient.Show();
                 Console.WriteLine();

                 //on mets a jour l'element qu'on viens de lire
                 Console.WriteLine("Mise a jour de l'elems obtenu au niveau du nom fixe a : rodrigues");
                 querriedclient.name = "rodrigues";
                 await UpdateClient(querriedclient);
                 Console.WriteLine("confirmons la MAJ");
                 querriedclient = await GetClient($"api/Clients1/{lastelem}");
                 querriedclient.Show();                
                 Console.WriteLine();

                 //delete the initially added elem

                 var statuscode = await DeleteClient(lastelem);                
                 Console.WriteLine($"Deleted(HTTP Status={statuscode}");*/

                List<Client> clients = await GetClients();
                foreach(Client c in clients)
                {
                    c.Show();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();//pour garder la fenetre ouverte


        }


    }
}