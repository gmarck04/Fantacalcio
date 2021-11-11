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

namespace Fantacalcio
{
    /**
     * \brief La classe fantacalciatore chiede in entrata un nome, un ruolo e un prezzo per il calciatore.
     * \param string nome: nome del calciatore
     * \param string ruolo: ruolo del calciatore
     * \param int prezzo: prezzo del calciatore
     */
    class fantacalciatore
    {
        //Attributi
        public string nome, ruolo;
        public int prezzo;
        //Costruttore.
        public fantacalciatore(string nome, string ruolo, int prezzo) 
        {
            this.nome = nome;
            this.ruolo = ruolo;
            this.prezzo = prezzo;
        }
        public override string ToString() //Metodo ToString, che ritorna la stringa nome.
        {
            return $"nome: {nome}, ruolo: {ruolo}, prezzo: {prezzo}";
        }
    }

    /**
     * \brief La classe fantaallenatore chiede in entrata un nome del fantaallenatore, i crediti del fantaallenatore.
     * \param string nome: nome del calciatore
     * \param string ruolo: ruolo del calciatore
     * \param int prezzo: prezzo del calciatore
     */
    class fantaallenatore
    {
        //Attributi
        public string Fantaallenatore_nome;
        protected int Fantaallenatore_crediti, Fantaallenatore_punti;
        public int N_portieri, N_difensori, N_centrocampisti, N_attacanti;
        List<fantacalciatore> Lista_Calciatori = new List<fantacalciatore>();

        //Costruttore
        public fantaallenatore(string Fantaallenatore_nome, int Fantaallenatore_crediti)
        {
            this.Fantaallenatore_nome = Fantaallenatore_nome;
            this.Fantaallenatore_crediti = Fantaallenatore_crediti;
            this.Fantaallenatore_punti = 0;
        }

        //Metodi
        public void Diminuisci_credito(int credito)
        {
            Fantaallenatore_crediti -= credito;
        }

        public void Aumenenta_credito(int credito)
        {
            Fantaallenatore_crediti += credito;
        }

        public int Visualizza_credito()
        {
            return Fantaallenatore_crediti;
        }

        public void Inserisci_punti(int punti)
        {
            Fantaallenatore_punti += punti;
        }

        public int Mostra_punti()
        {
            return Fantaallenatore_punti;
        }

        public List<fantacalciatore> Mostra_Calciatori() //Metodo che ritorna la Lista_calciatori.
        {
            return Lista_Calciatori;
        }

        public void Aggiungi_Calciatore(fantacalciatore calciatori) //Metodo che aggiunge i calciatori alla lista Lista_calciatori.
        {
            Lista_Calciatori.Add(calciatori);
        }

        public void Rimuovi_Calciatore(string nome) //Metodo che rimuove i calciatori dalla Lista_calciatori.
        {
            for (int i = 0; i < Lista_Calciatori.Count; i++)
            { 
                if (Lista_Calciatori[i].nome == nome) 
                {
                    Lista_Calciatori.Remove(Lista_Calciatori[i]); 
                }
            }
        }

        public override string ToString() //Metodo ToString, che ritorna la stringa nome.
        {
            return $"nome: {Fantaallenatore_nome}";
        }
    }

    class Program
    {
        public static string file = AppDomain.CurrentDomain.BaseDirectory + "/Fantaallenatore.JSON";
        public static List<fantaallenatore> Lista_di_fantaallenatori = new List<fantaallenatore>();
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
        }

        public static void File_non_trovato()
        {
            Console.WriteLine("Benvenuto nel Fantacalcio");
            Richiesta_credito();
            Inserisci_fantaallenatore();
        }
        
        public static void Inserisci_fantaallenatore()
        {
            string Scelta;
            int squadre = 0;
            do
            {
                Console.WriteLine("Inserisci il nome del fantaallenatore");
                string nome = Console.ReadLine();
                Console.WriteLine("Se non vuoi più inserire fantaallenatori scrivi FERMA, se vuoi ancora inserirne premi invio.");
                Scelta = Console.ReadLine().ToUpper();
                Lista_di_fantaallenatori.Add(new fantaallenatore(nome, fantamilioni_iniziali));
                squadre++;
                if(squadre == 10)
                {
                    Console.WriteLine("Hai raggiunto il massimo numero di fantaallenatori");
                    Scelta = "FERMA";
                }
            } while (Scelta == "FERMA" || squadre <= 2);
            Mostra_fantaallenatori();
        }

        public static void Inserisci_fantacalciatore()
        {
            int numero = Id_Fantaallenatori(Squadra());

            while(Lista_di_fantaallenatori[numero].N_portieri <= 3 && Lista_di_fantaallenatori[numero].N_difensori <= 8 && Lista_di_fantaallenatori[numero].N_centrocampisti <= 8 && Lista_di_fantaallenatori[numero].N_attacanti <= 6)
            {
                int credito, scelta_per_ruolo;
                string ruolo = "";
                Console.WriteLine("Inserisci nome del giocatore da aggiungere");
                string nome_calciatore = Console.ReadLine();
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

                if (credito >= Lista_di_fantaallenatori[numero].Visualizza_credito())
                {
                    Lista_di_fantaallenatori[numero].Aggiungi_Calciatore(new fantacalciatore(nome_calciatore, ruolo, credito));
                }
                else if (credito < Lista_di_fantaallenatori[numero].Visualizza_credito())
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
                                int sottrazione = (Lista_di_fantaallenatori[numero].Mostra_Calciatori()[0].prezzo * 20) / 100;
                                bool controllo = false;
                                string nome_fantacalciatore = "";

                                do //Ciclo do while, che continua se la variabile di tipo bool è false.
                                {
                                    Console.WriteLine("Scrivi il nome del calciatore da vendere:"); //Scrive su console la stringa.
                                    string nome = Console.ReadLine(); //Inizializzo la variabile di tipo string nome e le assegno il valore restituito dalla funzione Console.ReadLine.
                                    for (int i = 0; i < Lista_di_fantaallenatori[numero].Mostra_Calciatori().Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
                                    {
                                        if (Lista_di_fantaallenatori[i].Fantaallenatore_nome == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
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
                                            Console.WriteLine(Lista_di_fantaallenatori[numero].Mostra_Calciatori()[i].nome); //Scrive su console il valore ritornato da Lista_di_squadre[i].ToString().
                                        }
                                        controllo = false; //Pongo controllo uguale a false.
                                    }
                                } while (controllo == false);

                                Lista_di_fantaallenatori[numero].Aumenenta_credito((Lista_di_fantaallenatori[numero].Mostra_Calciatori()[Id_Fantacalciatori(nome_fantacalciatore, numero)].prezzo - sottrazione));
                            }
                            break;
                        case 2:
                            {
                                //più tardi
                            }
                            break;
                    }
                }
            }            
        }

        public static int Id_Fantaallenatori(string nome) //Funzione Id_Squadre, che ritorna la lunghezza della lista Lista_di_squadre
        {
            for (int i = 0; i < Lista_di_fantaallenatori.Count; i++)  //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
            {
                if (Lista_di_fantaallenatori[i].Fantaallenatore_nome == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
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
                if (Lista_di_fantaallenatori[numero].Mostra_Calciatori()[i].nome == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
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
                string nome = Console.ReadLine(); //Inizializzo la variabile di tipo string nome e le assegno il valore restituito dalla funzione Console.ReadLine.
                for (int i = 0; i < Lista_di_fantaallenatori.Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
                {
                    if (Lista_di_fantaallenatori[i].Fantaallenatore_nome == nome) //Se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome.
                    {
                        nome_squadra = nome; //Pongo la stringa nome_squadra uguale al valore della stringa nome.
                        controllo = true; //Pongo controllo uguale a true.
                    }
                }
                if (controllo == false) //Se controllo è uguale a false allora...
                {
                    Console.Clear(); //Pulisci la console.
                    Console.WriteLine("La sqaudra inserita non esiste"); //Scrive su console la stringa.
                    Console.WriteLine("Le squadre disponibili sono:"); //Scrive su console la stringa.
                    for (int i = 0; i < Lista_di_fantaallenatori.Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
                    {
                        Console.WriteLine(Lista_di_fantaallenatori[i].ToString()); //Scrive su console il valore ritornato da Lista_di_squadre[i].ToString().
                    }
                    controllo = false; //Pongo controllo uguale a false.
                }
            } while (controllo == false);

            return nome_squadra; //Ritorna la stringa nome_squadra.
        }

        public static void Mostra_fantaallenatori()
        {
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
            if (!File.Exists(file)) //Se il file esiste allora...
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
            Console.WriteLine("+3 punti per ogni gol segnato,\n + 3 punti per ogni rigore parato(portiere),\n +2 punti per ogni rigore segnato,\n +1 punto per ogni assist effettuato,\n -0,5 punti per ogni ammonizione,\n +1 portiere non ha preso gol in porta,\n -1 punto per ogni gol subito dal portiere,\n -1 punto per ogni espulsione,\n -2 punti per ogni autorete,\n -3 punti per un rigore sbagliato,\n +5 punti per bonus titolare,\n -2 punti per un’ammonizione,\n -5 punti per un’espulsione,\n +3 punti per un rigore guadagnato,\n -3 punti per un rigore causato,\n +6 punti per una rete inviolata(almeno 60' giocati),\n + 3 punti per una vittoria squadra appartenenza,\n +1 punti per un pareggio squadra appartenenza,\n +1 punti per un tiro fuori porta,\n +3 punti per un tiro in porta(pali e traverse).");
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
            }
        }
    }
}
