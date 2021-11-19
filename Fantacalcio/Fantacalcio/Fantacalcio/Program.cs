/** 
 * \file Fantacalcio.cs
 * \author @gmarck04
 * \date 21/10/2021
 * \brief Il programma è un gestionale per il gioco fantacalcio
*/

/* Nickname: @gmarck04
 * Data: 21/10/2021
 * Consegna:
    La consegna dovrà contenere:    
        - Relazione (requisiti, funzionale, tecnica) entro il 22/10/21
        - Codice e revisione relazione entro il 4/11/21

    Progettare un sistema di gestione del FANTACALCIO.
    Il livello di complessità del regolamento dovrà essere gestito autonomamente e giustificato nella relazione.

    Funzionalità minime
        - Almeno 2 giocatori
        - gestione dei crediti per l'acquisto giocatori (all'inizio X crediti, ogni giocatore vale y1, y2, y3..yn crediti
        - gestione settimanale con inserimento punteggio singolo giocatori.
        - gestione della classifica parziale al termine di ogni aggiornamento settimanale.

    Il progetto DEVE essere svolto in modalità CONSOLE. */

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Fantacalcio
{
    class Program
    {
        public static string file_fantaallenatore = AppDomain.CurrentDomain.BaseDirectory + "/Fantaallenatore.JSON";
        public static string file_fantacalciatore = AppDomain.CurrentDomain.BaseDirectory + "/Fantacalciatore.JSON";
        public static List<fantaallenatore> Lista_di_fantaallenatori = new List<fantaallenatore>();
        public static List<fantacalciatore> Lista_di_tutti_i_fantacalciatori = new List<fantacalciatore>();
        public static int fantamilioni_iniziali = 0;
        static void Main(string[] args)
        {
            Data();

            if (Controllo_salvataggio() == 0) //Se il valore della funzione Controllo_salvataggio è uguale a 0 allora...
            {
                File_trovato(); //Richiamo la funzione File_trovato.
            }
            else if (Controllo_salvataggio() == -1) //Se il valore della funzione Controllo_salvataggio è uguale a -1 allora...
            {
                File_non_trovato(); //Richiamo la funzione File_non_trovato.
            }
            Console.ReadKey(); //Blocco il programma, fino a pressione del tasto invio.
        }

        public static void File_trovato() //Funzione File_trovato (da fare...)
        {
            Console.WriteLine("Benvenuto, vuoi caricare i file di salvataggio o creare una nuovo campionato?\n (se selezioni si allora perderai tutti i file)"); //Scrive su console la stringa.
            deserializza_JSON();
            Menù_aggiungi_punti();
            serializza_JSON();
        }

        public static void File_non_trovato()
        {
            Console.WriteLine("Benvenuto nel Fantacalcio");
            Richiesta_credito();
            Inserisci_fantaallenatore();

            for (int i = 0; i < Lista_di_fantaallenatori.Count; i++)
            {
                Inserisci_fantacalciatore();
            }

            Menù_aggiungi_punti();
            serializza_JSON();
        }
        
        public static void Inserisci_fantaallenatore()
        {
            string Scelta = "";
            int squadre = 0;
            while (Scelta != "FERMA" || squadre < 2)
            {
                Console.WriteLine("Inserisci il nome del fantaallenatore da aggiungere");
                string nome = Console.ReadLine().ToUpper();               
                
                for(int i = 0; i < Lista_di_fantaallenatori.Count; i++)
                {
                    while (Lista_di_fantaallenatori[i].Get_nome() == nome)
                    {
                        Console.WriteLine("Il nome del fantaallenatore e' già stato inserito. \n");
                        Mostra_fantaallenatori();
                        Console.WriteLine("Inserisci il nome del fantaallenatore da aggiungere");
                        nome = Console.ReadLine().ToUpper();
                    }
                }
                squadre++;
                Lista_di_fantaallenatori.Add(new fantaallenatore(nome, fantamilioni_iniziali));

                Console.WriteLine("Se non vuoi più inserire fantaallenatori scrivi FERMA, se vuoi ancora inserirne premi invio.");
                Scelta = Console.ReadLine().ToUpper();

                if (Scelta == "FERMA" && squadre < 2) //sistema
                {
                    Console.WriteLine("Il numero di fantaallenatori DEVE essere maggiore o uguale a 2");
                    Scelta = "";
                }
                if (squadre == 10)
                {
                    Console.WriteLine("Hai raggiunto il massimo numero di fantaallenatori");
                    Scelta = "FERMA";
                }
            }        
        }

        public static void menù() //Funzione menù, che va a indirizzare la scelta fatta con la funzione menu_scelta.
        {
            int scelta = -1; //Inizzializzo la avriabile di tipo int scelta e la pongo uguale a -1.
            Console.Clear(); //Funzione, che pulisce la console.

            do //Ciclo do while, che continua se la variabile di tipo int è diversa da 0.
            {
                scelta = menu_scelta(); //Assegno il valore restituito dalla funzione menu_scelta alla variabile scelta.

                switch (scelta) //Switch con la variabile scelta.
                {
                    case 1: //Se la variabile scelta è uguale a 1.
                        {
                            
                        }
                        break; //Chiudo.                
                }
            } while (scelta != 13); //era uguale a 12 (+1).
        }

        public static int menu_scelta() //Funzione che visualizza il menu.
        {
            int scelta; //Inizzializzo la avriabile di tipo int numero_squadre.
            Console.WriteLine("Menù:\n -1"); //Scrive su console la stringa.
            bool controllo = int.TryParse(Console.ReadLine(), out scelta); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in scelta.
            
            
            
            while (!controllo || (scelta < 0 || scelta > 14)) //Ciclo while, che si avvia se controllo è falso o se la variabile numero_squadre è minore di 0 o maggiore di 14.



            {
                Console.WriteLine("Errato. Menù:\n -1 "); //Scrive su console la stringa.
                controllo = int.TryParse(Console.ReadLine(), out scelta); //Do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in scelta.
            }
            return scelta; //Ritorna la variabile scelta.
        }

        public static void Inserisci_fantacalciatore()
        {
            Console.Clear();
            Mostra_fantaallenatori();
            int numero = Id_Fantaallenatori(Squadra());
            bool continuo_ciclo = true;
            //continuo_ciclo == true
            while (Lista_di_fantaallenatori[numero].N_portieri <= 1)
            {
                int credito, scelta_per_ruolo;
                string ruolo = "";
                Console.WriteLine("Inserisci il nome del giocatore da aggiungere");
                string nome_calciatore = Console.ReadLine().ToUpper();
                Console.WriteLine("Inserisci il cognome del giocatore da aggiungere");
                string cognome_calciatore = Console.ReadLine().ToUpper();

                for (int i = 0; i < Lista_di_tutti_i_fantacalciatori.Count; i++)
                {
                    while (Lista_di_tutti_i_fantacalciatori[i].Get_nome() == nome_calciatore && Lista_di_tutti_i_fantacalciatori[i].Get_cognome() == cognome_calciatore)
                    {
                        Console.WriteLine("Il calciatore e' già stato preso.");
                        Console.WriteLine("Inserisci il nome del giocatore da aggiungere");
                        nome_calciatore = Console.ReadLine().ToUpper();
                        Console.WriteLine("Inserisci il cognome del giocatore da aggiungere");
                        cognome_calciatore = Console.ReadLine().ToUpper();
                    }
                }

                Console.WriteLine("Ruoli:\n -1 portiere,\n -2 difensore,\n -3 centrocampista,\n -4 Attacante.");
                bool controllo_1 = int.TryParse(Console.ReadLine(), out scelta_per_ruolo); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                while (!controllo_1 || (scelta_per_ruolo < 0 || scelta_per_ruolo > 5))
                {
                    Console.WriteLine("Errato. Ruoli:\n -1 portiere,\n -2 difensore,\n -3 centrocampista,\n -4 Attacante."); //Scrive su console la stringa.
                    controllo_1 = int.TryParse(Console.ReadLine(), out scelta_per_ruolo); //Do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                }

                switch (scelta_per_ruolo)
                {
                    case 1:
                        {
                            ruolo = "portiere";
                            Lista_di_fantaallenatori[numero].N_portieri++;
                        }
                        break;
                    case 2:
                        {
                            ruolo = "difensore";
                            Lista_di_fantaallenatori[numero].N_difensori++;
                        }
                        break;
                    case 3:
                        {
                            ruolo = "centrocampista";
                            Lista_di_fantaallenatori[numero].N_centrocampisti++;
                        }
                        break;
                    case 4:
                        {
                            ruolo = "attacante";
                            Lista_di_fantaallenatori[numero].N_attacanti++;
                        }
                        break;
                }

                Console.WriteLine("Inserisci il numero di fantamilioni da usare"); //Scrive su console la stringa.
                bool controllo_2 = int.TryParse(Console.ReadLine(), out credito); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                while (!controllo_2)
                {
                    Console.WriteLine("Errato. Inserisci il numero di fantamilioni da usare"); //Scrive su console la stringa.
                    controllo_2 = int.TryParse(Console.ReadLine(), out credito); //Do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                }                
                if (credito <= Lista_di_fantaallenatori[numero].Visualizza_credito())
                {
                    Lista_di_fantaallenatori[numero].Aggiungi_Calciatore(new fantacalciatore(nome_calciatore, cognome_calciatore, ruolo, credito));
                    Lista_di_tutti_i_fantacalciatori.Add(new fantacalciatore(nome_calciatore, cognome_calciatore, ruolo, credito));
                }
                else if (credito > Lista_di_fantaallenatori[numero].Visualizza_credito())
                {
                    int scelta_vendita;
                    Console.WriteLine("Il credito e' insufficiente");
                    Console.WriteLine("Per risolvere la situazione dovrai vendere uno dei fantacalciatori, dovrai scegliere se:\n -1 vendi alla banca, ma vi sara' una decurtazione del 20% del prezzo di acquasto del fantacalciatore,\n -2 vendi ad un fantaallenatore al prezzo pattuito.");
                    bool controllo_3 = int.TryParse(Console.ReadLine(), out scelta_vendita); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                    while (!controllo_3 || (scelta_vendita < 0 || scelta_vendita > 3))
                    {
                        Console.WriteLine("Errato. Per risolvere la situazione dovrai vendere uno dei fantacalciatori, dovrai scegliere se:\n -1 vendi alla banca, ma vi sara' una decurtazione del 20% del prezzo di acquasto del fantacalciatore,\n -2 vendi ad un fantaallenatore al prezzo pattuito."); //Scrive su console la stringa.
                        controllo_3 = int.TryParse(Console.ReadLine(), out scelta_vendita); //Do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                    }
                    switch (scelta_vendita)
                    {
                        case 1:
                            {
                                Banca_vendia(numero);
                            }
                            break;
                        case 2:
                            {
                                //più tardi
                            }
                            break;
                    }
                }

                

                if (Lista_di_fantaallenatori[numero].N_portieri <= 2 && Lista_di_fantaallenatori[numero].N_difensori <= 7 && Lista_di_fantaallenatori[numero].N_centrocampisti <= 7 && Lista_di_fantaallenatori[numero].N_attacanti <= 5)
                {
                    int scelta = 0;
                    Console.WriteLine("Vuoi contiunuare ? \n premi 1 per il si, premi 2 per il no.");
                    bool controllo_4 = int.TryParse(Console.ReadLine(), out scelta); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                    while (!controllo_4 || (scelta < 0 || scelta > 3))
                    {
                        Console.WriteLine("Errato. Vuoi contiunuare ? \n premi 1 per il si, premi 2 per il no."); //Scrive su console la stringa.
                        controllo_4 = int.TryParse(Console.ReadLine(), out scelta); //Do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                    }
                    if (scelta == 1)
                    {
                        continuo_ciclo = true;
                    }   
                    else if (scelta == 2)
                    {
                        continuo_ciclo = false;
                    }   
                    
                }              
            }            
        }

        public static void Banca_vendia(int numero)
        {
            bool controllo = false;
            string nome_fantacalciatore = "";

            do //Ciclo do while, che continua se la variabile di tipo bool è false.
            {
                Console.WriteLine("Scrivi il nome del calciatore da vendere:"); //Scrive su console la stringa.
                string nome = Console.ReadLine(); //Inizializzo la variabile di tipo string nome e le assegno il valore restituito dalla funzione Console.ReadLine. 

                for (int i = 0; i < Lista_di_fantaallenatori[numero].Mostra_Calciatori().Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
                {
                    if (Lista_di_fantaallenatori[i].Get_nome() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
                    {
                        nome_fantacalciatore = nome; //Pongo la stringa nome_squadra uguale al valore della stringa nome.
                        controllo = true; //Pongo controllo uguale a true.
                    }
                }
                if (controllo == false) //Se controllo è uguale a false allora...
                {
                    Console.Clear(); //Pulisci la console.
                    Console.WriteLine("La sqaudra inserita non esiste"); //Scrive su console la stringa.
                    Console.WriteLine("Le squadre disponibili sono:"); //Scrive su console la stringa.
                    for (int i = 0; i < Lista_di_fantaallenatori[numero].Mostra_Calciatori().Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
                    {
                        Console.WriteLine(Lista_di_fantaallenatori[numero].Mostra_Calciatori()[i].Get_nome()); //Scrive su console il valore ritornato da Lista_di_squadre[i].ToString().
                    }
                    controllo = false; //Pongo controllo uguale a false.
                }
            } while (controllo == false);
            int sottrazione = (Lista_di_fantaallenatori[numero].Mostra_Calciatori()[Id_Fantacalciatori(nome_fantacalciatore, numero)].Get_prezzo() * 20) / 100;
            Lista_di_fantaallenatori[numero].Aumenenta_credito((Lista_di_fantaallenatori[numero].Mostra_Calciatori()[Id_Fantacalciatori(nome_fantacalciatore, numero)].Get_prezzo() - sottrazione));
        }

        public static int Id_Fantaallenatori(string nome) //Funzione Id_Squadre, che ritorna la lunghezza della lista Lista_di_squadre
        {
            for (int i = 0; i < Lista_di_fantaallenatori.Count; i++)  //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
            {
                if (Lista_di_fantaallenatori[i].Get_nome() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
                {
                    return i; //Ritorna il valore di i.
                }
            }
            return -1; //Ritorna -1.
        }

        public static int Id_Fantacalciatori(string nome, int numero) //Funzione Id_Squadre, che ritorna la lunghezza della lista Lista_di_squadre
        {
            for (int i = 0; i < Lista_di_fantaallenatori[numero].Mostra_Calciatori().Count; i++)  //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
            {
                if (Lista_di_fantaallenatori[numero].Mostra_Calciatori()[i].Get_nome() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
                {
                    return i; //Ritorna il valore di i.
                }
            }
            return -1; //Ritorna -1.
        }

        public static string Squadra() //Richiede all'utente il nome della squadra, controlla che esista e restituisce il valore.
        {
            string nome_squadra = ""; //Inizializzo la variabile di tipo string nome_squadra e le assegno il valore .
            bool controllo = false; //Inizializzo la variabile di tipo bool e le assegno il valore false.
            do //Ciclo do while, che continua se la variabile di tipo bool è false.
            {
                Console.WriteLine("Inserisci il nome del fantaallenatore"); //Scrive su console la stringa.
                string nome = Console.ReadLine().ToUpper(); //Inizializzo la variabile di tipo string nome e le assegno il valore restituito dalla funzione Console.ReadLine.
                for (int i = 0; i < Lista_di_fantaallenatori.Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
                {
                    if (Lista_di_fantaallenatori[i].Get_nome() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
                    {
                        nome_squadra = nome; //Pongo la stringa nome_squadra uguale al valore della stringa nome.
                        controllo = true; //Pongo controllo uguale a true.
                    }
                }
                if (controllo == false) //Se controllo è uguale a false allora...
                {
                    Console.Clear(); //Pulisci la console.
                    Console.WriteLine("La sqaudra inserita non esiste"); //Scrive su console la stringa.                    
                    Mostra_fantaallenatori();
                    controllo = false; //Pongo controllo uguale a false.
                }
            } while (controllo == false);

            return nome_squadra; //Ritorna la stringa nome_squadra.
        }

        public static void Mostra_fantaallenatori()
        {
            Console.WriteLine("Le squadre inserite sono:"); //Scrive su console la stringa.
            for (int i = 0; i < Lista_di_fantaallenatori.Count; i++)  //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
            {
                Console.WriteLine(Lista_di_fantaallenatori[i].ToString());
            }
        }

        public static void Richiesta_credito()
        {
            Console.WriteLine("Inserisci i fantamilioni per ogni fantaallentore");
            bool controllo = int.TryParse(Console.ReadLine(), out fantamilioni_iniziali);
            while (!controllo || fantamilioni_iniziali < 1)
            {
                Console.WriteLine("Errato. Inserisci i fantamilioni per ogni fantaallentore");
                controllo = int.TryParse(Console.ReadLine(), out fantamilioni_iniziali);
            }
        }

        public static int Controllo_salvataggio() //Funzione Controllo_salvataggio, che controlla che il file esista.
        {
            if (!File.Exists(file_fantaallenatore)) //Se il file esiste allora...
            {
                //Console.WriteLine("File di salvataggio non trovato"); //Scrive su console la stringa.
                return -1; //Ritorna -1.
            }
            return 0; //Ritorna 0.
        }

        public static void Data()
        {
            DateTime thisDay = DateTime.Today;
            Console.WriteLine(thisDay.ToString("D"));
        }

        public static void Menù_aggiungi_punti()
        {
            int scelta;
            Console.WriteLine("Premi: \n-1 Per gol segnato,\n + 3 punti per ogni rigore parato(portiere),\n +2 punti per ogni rigore segnato,\n +1 punto per ogni assist effettuato,\n " +
                "-0,5 punti per ogni ammonizione,\n +1 portiere non ha preso gol in porta,\n -1 punto per ogni gol subito dal portiere,\n -1 punto per ogni espulsione,\n -2 punti per ogni autorete,\n " +
                "-3 punti per un rigore sbagliato,\n +5 punti per bonus titolare,\n -2 punti per un’ammonizione,\n -5 punti per un’espulsione,\n +3 punti per un rigore guadagnato,\n " +
                "-3 punti per un rigore causato,\n +6 punti per una rete inviolata(almeno 60' giocati),\n + 3 punti per una vittoria squadra appartenenza,\n " +
                "+1 punti per un pareggio squadra appartenenza,\n +1 punti per un tiro fuori porta,\n +3 punti per un tiro in porta(pali e traverse).");
            bool controllo = int.TryParse(Console.ReadLine(), out scelta);
            while (!controllo || (scelta < 0 || scelta > 5)) //modifica
            {
                Console.WriteLine("Errato. Inserisci i fantamilioni per ogni fantaallentore");
                controllo = int.TryParse(Console.ReadLine(), out scelta);
            }

            switch (scelta)
            {
                case 1:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(3);
                    break;
                case 2:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(3);
                    break;
                case 3:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(2);
                    break;
                case 4:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(1);
                    break;
                case 5:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-0.5);
                    break;
            }
        }

        public static void serializza_JSON()
        {
            string output = JsonConvert.SerializeObject(Lista_di_fantaallenatori, Formatting.Indented);
            File.WriteAllText(file_fantaallenatore, output);

            string output_fantacalciatore = JsonConvert.SerializeObject(Lista_di_tutti_i_fantacalciatori, Formatting.Indented);
            File.WriteAllText(file_fantacalciatore, output_fantacalciatore);
        }

        public static void deserializza_JSON()
        {
            string input = File.ReadAllText(file_fantaallenatore);
            Lista_di_fantaallenatori = JsonConvert.DeserializeObject<List<fantaallenatore>>(input);

            string input_fantacalciatore = File.ReadAllText(file_fantaallenatore);
            Lista_di_tutti_i_fantacalciatori = JsonConvert.DeserializeObject<List<fantacalciatore>>(input_fantacalciatore);
        }
    }
}
