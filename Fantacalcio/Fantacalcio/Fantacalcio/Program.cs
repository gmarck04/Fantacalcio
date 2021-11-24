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
        public static string file_fantacalciatore = AppDomain.CurrentDomain.BaseDirectory + "/Fantacalciatore_elenco.JSON";
        public static List<fantaallenatore> Lista_di_fantaallenatori = new List<fantaallenatore>();
        public static List<fantacalciatore> Lista_di_tutti_i_fantacalciatori = new List<fantacalciatore>();
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
            Console.WriteLine("Benvenuto, carico i file di salvataggio..."); //Scrive su console la stringa.
            deserializza_JSON();
            menù();
            serializza_JSON();
        }

        public static void File_non_trovato()
        {
            Console.WriteLine("Benvenuto nel Fantacalcio");
            Inserisci_fantaallenatore();
            menù();
            serializza_JSON();
        }
        
        public static void Inserisci_fantaallenatore()
        {
            int fantamilioni_iniziali = Richiesta_credito();
            string Scelta = "";
            int squadre = 0;
            while (Scelta != "FERMA" || squadre < 2)
            {
                Console.WriteLine("Inserisci il nome del fantaallenatore da aggiungere");
                string nome = Console.ReadLine().ToUpper();               
                
                for(int i = 0; i < Lista_di_fantaallenatori.Count; i++)
                {
                    while (Lista_di_fantaallenatori[i].ToString() == nome)
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

            do //Ciclo do while, che continua se la variabile di tipo int è diversa da 6.
            {
                scelta = menu_scelta();
                switch (scelta) //Switch con la variabile scelta.
                {
                    case 1: //Se la variabile scelta è uguale a 1.
                        {
                            Inserisci_fantacalciatore();
                        }
                        break; //Chiudo.    
                    case 2: //Se la variabile scelta è uguale a 2.
                        {
                            Menù_aggiungi_punti();
                        }
                        break; //Chiudo.    
                    case 3: //Se la variabile scelta è uguale a 3.
                        {
                            Banca_vendia(Id_Fantaallenatori(Squadra()));
                        }
                        break; //Chiudo.    
                    case 4: //Se la variabile scelta è uguale a 4.
                        {
                            Classifica_Punti();
                        }
                        break; //Chiudo.    
                }
            } while (scelta != 5);
        }

        public static int menu_scelta() //Funzione che visualizza il menu.
        {
            Console.Clear(); //Funzione, che pulisce la console.
            int scelta; //Inizzializzo la avriabile di tipo int numero_squadre.
            Console.WriteLine("Menù:\n -1 Inserisci fanatcalciatori.\n -2 Inserisci punti.\n -3 Vendi fantacalciatore.\n -4 Mostra classifica.\n -5 Chiudi programma."); //Scrive su console la stringa.
            bool controllo = int.TryParse(Console.ReadLine(), out scelta); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in scelta.
            while (!controllo || (scelta < 0 || scelta > 6)) //Ciclo while, che si avvia se controllo è falso o se la variabile numero_squadre è minore di 0 o maggiore di 14.
            {
                Console.WriteLine("Errato. Menù:\n -1 Inserisci fanatcalciatori.\n -2 Inserisci punti.\n -3 Vendi fantacalciatore.\n -4 Mostra classifica.\n -5 Chiudi programma."); //Scrive su console la stringa.
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
            while (continuo_ciclo == true)
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
                    Console.WriteLine("Per risolvere la situazione dovrai vendere uno dei fantacalciatori, dovrai scegliere se:\n -1 vendi alla banca, ma vi sara' una decurtazione del 20% del prezzo di acquasto del fantacalciatore,\n -2 vendi ad un fantaallenatore al prezzo pattuito. (Opzione non disponibile)");
                    bool controllo_3 = int.TryParse(Console.ReadLine(), out scelta_vendita); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in gol.
                    while (!controllo_3 || (scelta_vendita < 0 || scelta_vendita > 3))
                    {
                        Console.WriteLine("Errato. Per risolvere la situazione dovrai vendere uno dei fantacalciatori, dovrai scegliere se:\n -1 vendi alla banca, ma vi sara' una decurtazione del 20% del prezzo di acquasto del fantacalciatore,\n -2 vendi ad un fantaallenatore al prezzo pattuito. (Opzione non disponibile)"); //Scrive su console la stringa.
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
                                Console.WriteLine("Opzione non disponibile, sarai reidirizzato verso la banca");
                                Banca_vendia(numero);
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
                    if (Lista_di_fantaallenatori[i].ToString() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
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
                if (Lista_di_fantaallenatori[i].ToString() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
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
                    if (Lista_di_fantaallenatori[i].ToString() == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
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

        public static int Richiesta_credito()
        {
            int fantamilioni_iniziali = 0;
            Console.WriteLine("Inserisci i fantamilioni per ogni fantaallentore");
            bool controllo = int.TryParse(Console.ReadLine(), out fantamilioni_iniziali);
            while (!controllo || fantamilioni_iniziali < 1)
            {
                Console.WriteLine("Errato. Inserisci i fantamilioni per ogni fantaallentore");
                controllo = int.TryParse(Console.ReadLine(), out fantamilioni_iniziali);
            }
            return fantamilioni_iniziali;
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
            Console.WriteLine("Premi:\n -1 Per gol segnato.\n -2 Per ogni rigore parato (portiere).\n -3 Per ogni rigore segnato.\n -4 Per ogni assist effettuato.\n " +
                "-5 Per ogni ammonizione.\n -6 Per portiere che non ha preso gol in porta.\n -7 Per ogni gol subito dal portiere.\n -8 Per ogni espulsione.\n -9 Per ogni autorete.\n " +
                "-10 Per rigore sbagliato,\n -11 Per bonus titolare,\n -12 Per un’ammonizione,\n -13 Per un’espulsione,\n -14 Per un rigore guadagnato,\n " +
                "-15 Per un rigore causato,\n -16 Per una rete inviolata(almeno 60' giocati),\n -17 Per una vittoria squadra appartenenza,\n " +
                "-18 Per un pareggio squadra appartenenza,\n -19 Per un tiro fuori porta,\n -20 Per un tiro in porta(pali e traverse).");
            bool controllo = int.TryParse(Console.ReadLine(), out scelta);
            while (!controllo || (scelta < 0 || scelta > 21))
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
                    Aggiungi_punti_portiere(3);                   
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
                case 6:
                    Aggiungi_punti_portiere(1);
                    break;
                case 7:
                    Aggiungi_punti_portiere(-1);
                    break;
                case 8:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-1);
                    break;
                case 9:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-2);
                    break;
                case 10:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-3);
                    break;
                case 11:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(5);
                    break;
                case 12:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-2);
                    break;
                case 13:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-5);
                    break;
                case 14:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(3);
                    break;
                case 15:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(-3);
                    break;
                case 16:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(6);
                    break;
                case 17:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(3);
                    break;
                case 18:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(1);
                    break;
                case 19:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(1);
                    break;
                case 20:
                    Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(3);
                    break;
            }
        }

        public static void Aggiungi_punti_portiere(int punti)
        {
            string nome_squadra = Squadra();
            int scelta_interna = Id_Fantaallenatori(nome_squadra);
            if (Lista_di_fantaallenatori[scelta_interna].Mostra_Calciatori()[Id_Fantacalciatori(nome_squadra, scelta_interna)].Get_ruolo() == "portiere")
            {
                Lista_di_fantaallenatori[scelta_interna].Inserisci_punti(punti);
            }
            else
            {
                Console.WriteLine("Questa scelta e' disonibile solo per i portieri");
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

        public static void Classifica_Punti() //Funzione Classifica_Punti, che mostra la classifica dei punti, attraverso la funzione InsertionSort.
        {
            for (int i = 0; i < InsertionSort(Lista_di_fantaallenatori).Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza del valore restituito dalla funzione InsertionSort, a cui invio la lista Lista_di_squadre e 0, la i ad ogni ciclo viene incrementata di 1.
            {
                Console.WriteLine(i + 1 + "-" + InsertionSort(Lista_di_fantaallenatori)[i].ToString()); //Mostra su console il valore di i incrementato di 1, un "-" e il valore restituito dalla funzione InsertionSort, a cui invio la lista Lista_di_squadre e 0.
            }
            Console.WriteLine("Premi un tasto qualsiasi per continuare");
            Console.ReadLine();
        }

        static List<fantaallenatore> InsertionSort(List<fantaallenatore> inputArray) //Funzione InsertionSort, che richiede in ingresso una lista di tipo Squadra inputArray e un codice (0,1,2).
        {
            for (int i = 0; i < inputArray.Count - 1; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista inputArray, la i ad ogni ciclo viene incrementata di 1.
            {
                for (int j = i + 1; j > 0; j--) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome j e la pongo pari a i + 1, che continua fino a quando il valore della j è maggiore di 0, la j ad ogni ciclo viene decrementata di 1.
                {
                    if (inputArray[j - 1].CompareTo(inputArray[j]) == -1) //Se il valore di inputArray[j - 1].CompareTo(inputArray[j], codice) è uguale a -1, allora...
                    {
                        fantaallenatore temp = inputArray[j - 1]; //inizializzo la variabile di tipo Squadra temp e le assegnio il valore dell'array inputArray[j - 1].
                        inputArray[j - 1] = inputArray[j]; //Do all'array inputArray[j - 1] il valore dell'array inputArray[j].
                        inputArray[j] = temp; //Do all'array inputArray[j] il valore della variabile di tipo Squadra temp.
                    }
                }
            }
            return inputArray; //Ritorna la variabile inputArray.
        }
    }
}
