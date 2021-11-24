/** 
 * \file Program.cs
 * \author @gmarck04
 * \date 21/10/2021
 * \brief Il programma è un gestionale per il gioco fantacalcio
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
    /**
    * \class Program
    * \brief Classe principale del programma, che ha il compito di interfacciarsi con l'utente
    */
    class Program
    {
        /// \brief Grazie alla funzione AppDomain.CurrentDomain.BaseDirectory, che ottiene la directory di base, a cui aggiungo il nome del file, che è Fantaallenatore.JSON
        public static string file_fantaallenatore = AppDomain.CurrentDomain.BaseDirectory + "/Fantaallenatore.JSON";
        /// \brief Grazie alla funzione AppDomain.CurrentDomain.BaseDirectory, che ottiene la directory di base, a cui aggiungo il nome del file, che è Fantacalciatore_elenco.JSON
        public static string file_fantacalciatore = AppDomain.CurrentDomain.BaseDirectory + "/Fantacalciatore_elenco.JSON";
        /// \brief Lista di tipo fantaallenatore
        public static List<fantaallenatore> Lista_di_fantaallenatori = new List<fantaallenatore>();
        /// \brief Lista di tipo fantacalciatore
        public static List<fantacalciatore> Lista_di_tutti_i_fantacalciatori = new List<fantacalciatore>();

        /**
         * \fn      static void Main(string[] args)
         * \brief   Eseguo il codice richiamando una serie di funzioni
         * \details La funzione Main chiama la funzione Data(), poi se il valore della funzione Controllo_salvataggio è uguale a 0 allora richiamo la funzione File_trovato, 
         * se il valore della funzione Controllo_salvataggio è uguale a -1 allora richiamo la funzione File_non_trovato e alla fine utilizzo una funzione Console.ReadKey()
         */
        static void Main(string[] args)
        {
            Data();

            if (Controllo_salvataggio() == 0)
            {
                File_trovato();
            }
            else if (Controllo_salvataggio() == -1) 
            {
                File_non_trovato();
            }
            Console.ReadKey();
        }

        /**
         * \fn      public static void File_trovato()
         * \brief   Eseguo il codice richiamando una serie di funzioni
         * \details La funzione all'inzio scrive in console le stringhe "Benvenuto, carico i file di salvataggio..." e "Premi un tasto qualsiasi per continuare", poi utilizzo una funzione Console.ReadKey(),
         * poi richiamo la funzione deserializza_JSON(), menù() ed infine la funzione serializza_JSON()
         */
        public static void File_trovato()
        {
            Console.WriteLine("Benvenuto, carico i file di salvataggio..."); //Scrive su console la stringa.
            Console.WriteLine("Premi un tasto qualsiasi per continuare");
            Console.ReadKey();
            deserializza_JSON();
            menù();
            serializza_JSON();
        }

        /**
         * \fn      public static void File_non_trovato()
         * \brief   Eseguo il codice richiamando una serie di funzioni
         * \details La funzione all'inzio scrive in console le stringhe "Benvenuto nel Fantacalcio", poi richiamo la funzione Inserisci_fantaallenatore(), menù() ed infine la funzione serializza_JSON()
         */
        public static void File_non_trovato()
        {
            Console.WriteLine("Benvenuto nel Fantacalcio");
            Inserisci_fantaallenatore();
            menù();
            serializza_JSON();
        }

        /**
         * \fn      public static void Inserisci_fantaallenatore()
         * \param   int fantamilioni_iniziali: Serve per inserire il numero dei fantamilioni iniziali, viene posta uguale al valore di ritorno della funzione Richiesta_credito()
         * \param   string Scelta: Serve per far iniziare o terminare il ciclo while
         * \param   int squadre: contatore del numero di squadre inserite
         * \param   string nome: nome del fantaallenatore
         * \brief   La funzione serve per inserire i fantaallenatori nella lista Lista_di_fantaallenatori
         * \details Inizia un while all'inzio, che inizia se Scelta è diverso dalla stringa "FERMA" o se squadre è minore di 2, nel ciclo viene stampata la stringa "Inserisci il nome del fantaallenatore da aggiungere",
         * letta da un Console.ReadLine().ToUpper() e il valore viene inserito in nome, poi inizia un ciclo di controllo, per controllare se il nome è gia presente nella lista Lista_di_fantaallenatori, 
         * nello specifico con un while si controlla se Lista_di_fantaallenatori[i].ToString() == nome), se è il ciclo inizia allora viene stampata a schermo la stringa "Il nome del fantaallenatore e' già stato inserito. \n",
         * poi viene richiamata la funzione Mostra_fantaallenatori(), poi viene stampata la stringa Inserisci il nome del fantaallenatore da aggiungere, per poi finire il secondo ciclo con lettura da parte di 
         * un Console.ReadLine().ToUpper() e il valore viene inserito in nome. Viene incrementata la variabile squadre e viene aggiunta un'istanza nella lista Lista_di_fantaallenatori, con la funzione Lista.Add,
         * inviando al costruttore nome e fantamilioni_iniziali, poi viene stampata la stringa "Se non vuoi più inserire fantaallenatori scrivi FERMA, se vuoi ancora inserirne premi invio.", 
         * poi avviene la lettura da parte di un Console.ReadLine().ToUpper() e il valore viene inserito in Scelta, se scelta di Scelta è uguale a "FERMA" e squadre è maggiore di 2 allora scrivi la stringa 
         * "Il numero di fantaallenatori DEVE essere maggiore o uguale a 2" e viene posto il valore di scelta pari a "", se squadre è uguale a 10 allora viene scritta la stringa "Hai raggiunto il massimo numero di fantaallenatori"
         * e viene posto il valore di Scelta uguale a "FERMA".
         */
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

        /**
         * \fn      public static void menù()
         * \param   scelta: serve per inserire la scelta fatta dall'utente, viene posta pari a -1
         * \brief   Va ad indirizzare la scelta fatta con la funzione menu_scelta.
         * \details Viene utilizzata all'inzio un Console.Clear(), poi inizia un ciclo do while, che si ripete fino a quando scelta non è uguale a 5, poi viene inserito in scelta il valore di ritorno della funzione menu_scelta(),
         * poi viene fatto uno switch case con la variablie scelta, se scelta è uguale a 1 allora si richiama la funzione Inserisci_fantacalciatore(), se scelta è uguale a 2 allora si richiama la funzione Menù_aggiungi_punti(),
         * se scelta è uguale a 3 allora si richiama la funzione Banca_vendita(), a cui passo il valore di ritorno della funzione Id_Fantaallenatori(), a cui a sua volta passo il valore di ritorno della funzione Squadra() 
         * e se scelta è uguale a 4 allora si richiama la funzione Classifica_Punti()
         */
        public static void menù()
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
                            Banca_vendita(Id_Fantaallenatori(Squadra()));
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

        /**
         * \fn      public static int menu_scelta()
         * \param   scelta: variabile inserita dall'utente
         * \param   controllo: valore per il controllo, successivo al tentativo con una funzione TryParse
         * \brief   Viene visualizzato un menù per scelgliere le azioni da compiere
         * \details Viene pulita la console, con un Console.Clear(), poi viene visualizzata la stringa "Menù:\n -1 Inserisci fanatcalciatori.\n -2 Inserisci punti.\n -3 Vendi fantacalciatore.\n -4 Mostra classifica.\n 
         * -5 Chiudi programma.", do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine,
         * restituisce il valore convertito in int e lo inserisce in scelta, inizia un ciclo while se controllo è falso o se la variabile numero_squadre è minore di 0 o maggiore di 14, se inizia allora viene mostrata
         * la stringa "Errato. Menù:\n -1 Inserisci fanatcalciatori.\n -2 Inserisci punti.\n -3 Vendi fantacalciatore.\n -4 Mostra classifica.\n -5 Chiudi programma.",
         * poi do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore 
         * convertito in int e lo inserisce in scelta, se il ciclo finisce allora viene ritornata la variabile scelta
         * \return  scelta: valore inserito dall'utente.
         */
        public static int menu_scelta()
        {
            Console.Clear(); //Funzione, che pulisce la console.
            int scelta;
            Console.WriteLine("Menù:\n -1 Inserisci fanatcalciatori.\n -2 Inserisci punti.\n -3 Vendi fantacalciatore.\n -4 Mostra classifica.\n -5 Chiudi programma.");
            bool controllo = int.TryParse(Console.ReadLine(), out scelta); //Inizializzo la variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in scelta.
            while (!controllo || (scelta < 0 || scelta > 6)) //Ciclo while, che si avvia se controllo è falso o se la variabile numero_squadre è minore di 0 o maggiore di 14.
            {
                Console.WriteLine("Errato. Menù:\n -1 Inserisci fanatcalciatori.\n -2 Inserisci punti.\n -3 Vendi fantacalciatore.\n -4 Mostra classifica.\n -5 Chiudi programma."); //Scrive su console la stringa.
                controllo = int.TryParse(Console.ReadLine(), out scelta); //Do alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso dalla funzione Console.ReadLine, restituisce il valore convertito in int e lo inserisce in scelta.
            }
            return scelta; //Ritorna la variabile scelta.
        }

        /**
        * \fn      public static void Inserisci_fantacalciatore()
        * \param   int numero: Variabile che contene il numero della squadra per la lista Lista_di_fantaallenatori
        * \param   int credito: Prezzo fantacalciatore
        * \param   int scelta_vendita: valore per sceglire l'opzione nel menù di vendità
        * \param   int scelta: valore per continuare il ciclo while
        * \param   int scelta_per_ruolo: scelta per scegliere il ruolo del fantacalciatore
        * \param   string ruolo: ruolo fantacalciatore
        * \param   string nome_calciatore: nome fantacalciatore
        * \param   string cognome_calciatore: cognome fantacalciatore
        * \param   bool continuo_ciclo: valore per far iniziare o far smettere il primo while
        * \param   bool controllo_1: Controllo uscito da un tryparse, nella scelta del ruolo
        * \param   bool controllo_2: Controllo uscito da un tryparse, nella scelta del credito
        * \param   bool controllo_3: Controllo uscito da un tryparse, nella scelta del tipo di vendita
        * \param   bool controllo_4: Controllo uscito da un tryparse, nella scelta per terminare il ciclo while principale
        * \brief   Funzione per inserire il fantacalciatore ed assegnarlo ad un fantaallenatore
        * \details Viene pulita la console, con un Console.Clear(), poi viene posto continuo_ciclo a true, poi inizia il while se continuo_ciclo è true, poi viene richiesto il nome del fantacalciatore ed inserito nella string 
        * nome_calciatore, poi poi viene richiesto il nome del fantacalciatore ed inserito nella string cognome_calciatore, poi inizia un for, che continua fino a quando i è minore della lunghezza della lista Lista_di_tutti_i_fantacalciatori,
        * viene poi incrementata i di 1, fino a quando Lista_di_tutti_i_fantacalciatori[i].Get_nome() è uguale al valore di nome_calciatore e Lista_di_tutti_i_fantacalciatori[i].Get_cognome() è uguale al valore di
        * cognome_calciatore allora viene mostrata la stringha "Il calciatore e' già stato preso." e viene richiesto il nome e il cognome del fantacalciatore, viene poi chiesto di inserire il ruolo del calciatore,
        * viene effettuato un contollo, che continua fino a quando controllo_1 è false o scelta_per_ruolo è minore di 0 o maggiore di 5, avviene poi uno switch case, che incrementa assegna alla variabile ruolo il ruolo e che 
        * incrementa il numero di calciatori facenti parte di quel ruolo, vengono poi chiesti il numero di fantamilioni da impiegare e se il credito è sufficente allora si diminuisce il credito del fantaalenatore,
        * se non è sufficente si dovrà vendere un fantacalciatore alla banca, avviene poi un controllo che continua fino a quando il numero di portieri è minore di 2, il numero di difensori è minore di 7, 
        * il numero di centrocampisti è minore di 7 e il numero di attaccanti è minore di 5, viene poi chiesto all'utente se vuole continuare ad inserire o meno fatacalciatori
        */
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
                                Banca_vendita(numero);
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("Opzione non disponibile, sarai reidirizzato verso la banca");
                                Banca_vendita(numero);
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

        /**
        * \fn       public static void Banca_vendita(int numero)   
        * \param    bool controllo: controllo per il ciclo while
        * \param    string nome_fantacalciatore: nome del fantacalciatore da vendere (controllato)
        * \param    string nome: nome del fantacalciatore da vendere
        * \param    int numero: numero inserito alla chiamata
        * \param    int i: variablie ciclo for
        * \param    int sottrazione: soldi da sottrarre al fantaallenatore
        * \brief    Funzione per vendere i fantacalciatori alla banca
        * \details  La funzione inizia con un ciclo do while, che continua fino a quando controllo è false, poi viene richiesto il nome del fantacalciatore da vendere, il valore viene inserito in nome, avviene poi un controllo 
        * con un ciclo for che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1, se la stringa contenuta Lista_di_squadre[i].Nome_squadra 
        * è uguale alla stringa nome allora pongo la stringa nome_squadra uguale al valore della stringa nome e pongo controllo uguale a true. Dopo il ciclo for viene effetuato un if, se controllo è false allora
        * pulisco la console, scrivo a schermo la stringa "La sqaudra inserita non esiste" e poi scrivo le squadre disponibili, scritte grazie alla funzione Mostra_Calciatori().
        * Dopo il ciclo do while, viene inserito in sottrazione il valore preso dal 20% del prezzo pagato per il fantacalciatore e viene aggiunto al credito del fantaallenatore
        */
        public static void Banca_vendita(int numero)
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

        /**
        * \fn       public static int Id_Fantaallenatori(string nome)   
        * \param    int i: Contatore ciclo for  
        * \param    string nome: nome fantaallenatore, preso alla chiamata della funzione
        * \brief    Funzione Id_Fantaallenatori, che ritorna la lunghezza della lista Lista_di_fantaallenatori
        * \details  Inizia un ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, 
        * la i ad ogni ciclo viene incrementata di 1, se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome allora ritorna i, oppure ritorna -1
        * \return   i: numero della squadra nella lista Lista_di_fantaallenatori
        */
        public static int Id_Fantaallenatori(string nome)
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

        /**
        * \fn 
        * \param    string nome: nome fantaallenatore, preso alla chiamata della funzione
        * \param    int numero: numero della lista della Lista_di_fantaallenatori
        * \param    int i: Contatore ciclo for  
        * \brief    Funzione Id_Fantacalciatori, che ritorna la posizione nella lista Lista_Calciatori
        * \details  Inizia un ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, 
        * la i ad ogni ciclo viene incrementata di 1, se la stringa contenuta Lista_di_squadre[i].Nome_squadra è uguale alla stringa nome, allora ritorna i, oppure ritorna -1
        * \return   i: numero della lista della Lista_di_fantaallenatori
        */
        public static int Id_Fantacalciatori(string nome, int numero) 
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

        /**
        * \fn       public static string Squadra()
        * \param    string nome_squadra: nome della squadra
        * \param    bool controllo: controllo per il ciclo do while
        * \brief    Richiede all'utente il nome della squadra, controlla che esista e restituisce il valore.
        * \details  Il ciclo do while, continua se la variabile di tipo bool è false, si richiede all'inizio di inserire il nome del fantaalenatore, viene inserito in nome e messo in maiuscolo, inizia un ciclo for , 
        * che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1, se la stringa contenuta Lista_di_squadre[i].Nome_squadra 
        * è uguale alla stringa nome allora pongo la stringa nome_squadra uguale al valore della stringa nome e pongo controllo uguale a true. Dopo il for faccio un if, se controllo è uguale a false allora
        * pulisco la console, scrivo che la squadra non esiste e richiamo la funzione Mostra_fantaallenatori() e pongo controllo a false
        * \return   nome_squadra: nome della squadra
        */
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

        /**
        * \fn       public static void Mostra_fantaallenatori()  
        * \param    int i: Contatore ciclo for     
        * \brief    Funzione che mostra un elenco di fantaallenatori
        * \details  Scrive a schermo la stringa "Le squadre inserite sono:", poi con un ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando 
        * il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1, scrive a schermo Lista_di_fantaallenatori[i].ToString()
        */
        public static void Mostra_fantaallenatori()
        {
            Console.WriteLine("Le squadre inserite sono:"); //Scrive su console la stringa.
            for (int i = 0; i < Lista_di_fantaallenatori.Count; i++)  //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza della lista Lista_di_squadre, la i ad ogni ciclo viene incrementata di 1.
            {
                Console.WriteLine(Lista_di_fantaallenatori[i].ToString());
            }
        }

        /**
        * \fn       public static int Richiesta_credito()  
        * \param    int fantamilioni_iniziali: numero di fantamilioni di inizio
        * \param    bool controllo: controllo ritornato da una funzione TryParse()
        * \brief    Funzione che richiede il credito per i fantaallenatori
        * \details  Viene richiesto al fantaallenatore di inserire i fantamilioni inziali, che poi saranno inseriti in fantamilioni_iniziali se il valore uscito dalla funzione TryParse() è posistivo e se fantamilioni_iniziali
        * è maggiore di 0
        * \return   fantamilioni_iniziali: numero di fantamilioni di inizio
        */
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

        /**
        * \fn      public static int Controllo_salvataggio()  
        * \brief   Funzione Controllo_salvataggio, che controlla che il file esista.
        * \details All'inizio viene utilizzato un if, se il file contenuto nella stringa file_fantaallenatore esiste allora viene ritornato il valore -1, se non esiste viene ritornato il valore 0
        */
        public static int Controllo_salvataggio()
        {
            if (!File.Exists(file_fantaallenatore)) //Se il file esiste allora...
            {
                //Console.WriteLine("File di salvataggio non trovato"); //Scrive su console la stringa.
                return -1; //Ritorna -1.
            }
            return 0; //Ritorna 0.
        }

        /**
        * \fn      public static void Data()
        * \brief   Stampa la data presa dal sistema
        * \details Utilizzando la funzione DateTime, stampo a schermo la data presa dal sistema
        */
        public static void Data()
        {
            DateTime thisDay = DateTime.Today;
            Console.WriteLine(thisDay.ToString("D"));
        }

        /**
        * \fn      public static void Menù_aggiungi_punti()
        * \param   int scelta: scelta da parte dell'utente nel menù
        * \param   bool controllo: valore ritornato da una funzione TryParse
        * \brief   Funzione che mostra il menù per inserire i punti e che poi li aggiunge ai punti del fantaallenatore
        * \details All'inizio stampo la stringa per il menù, viene dato alla variabile di tipo bool controllo e le do valore pari al valore ritornato dalla funzione TryParse, che prende in entrata il valore preso 
        * dalla funzione Console.ReadLine, restituisco il valore convertito in int e lo inserisco in scelta, se contollo è falso o se scelta è minore di 0 o maggiore di 21, allora rimostro la stringa menu e ripeto l'asseganzione
        * dei valori della variablie scelta. poi viene effettuato uno switch case, che in base alla scelta dell'utente inserisce in Lista_di_fantaallenatori[Id_Fantaallenatori(Squadra())].Inserisci_punti(punti
        * (valore a scelta))
        */
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
                Console.WriteLine("Errato. Premi:\n -1 Per gol segnato.\n -2 Per ogni rigore parato (portiere).\n -3 Per ogni rigore segnato.\n -4 Per ogni assist effettuato.\n " +
                "-5 Per ogni ammonizione.\n -6 Per portiere che non ha preso gol in porta.\n -7 Per ogni gol subito dal portiere.\n -8 Per ogni espulsione.\n -9 Per ogni autorete.\n " +
                "-10 Per rigore sbagliato,\n -11 Per bonus titolare,\n -12 Per un’ammonizione,\n -13 Per un’espulsione,\n -14 Per un rigore guadagnato,\n " +
                "-15 Per un rigore causato,\n -16 Per una rete inviolata(almeno 60' giocati),\n -17 Per una vittoria squadra appartenenza,\n " +
                "-18 Per un pareggio squadra appartenenza,\n -19 Per un tiro fuori porta,\n -20 Per un tiro in porta(pali e traverse).");
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

        /**
        * \fn       public static void Aggiungi_punti_portiere(int punti) 
        * \param    int punti: valore inserito dall'utente
        * \param    int scelta_interna: valore preso dalla funzione Id_Fantaallenatori(), a cui passo il valore della stringa nome_squadra
        * \param    string nome_squadra: valore preso dalla funzione Squadra()
        * \brief    Funzione che aggiunge i punti per i portieri
        * \details  Se Lista_di_fantaallenatori[valore di scelta_interna].Mostra_Calciatori()[Id_Fantacalciatori(), a cui passo nome_squadra e scelta_interna].Get_ruolo() è uguale a "portiere" 
        * allora aggiungi a Lista_di_fantaallenatori il valore di punti, se non è così allora scrivi "Questa scelta e' disonibile solo per i portieri"
        */
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

        /**
        * \fn       public static void serializza_JSON()
        * \param    string output: valore di output per la lista Lista_di_fantaallenatori
        * \param    string output_fantacalciatore: valore di output per la lista Lista_di_tutti_i_fantacalciatori
        * \brief    Funzione che serializza le liste in file JSON
        * \details  Viene inserita nella stringa output il valore serializzato della lista Lista_di_fantaallenatori e formattato, viene poi scritto con un File.WriteAllText(), a cui passo i valori file_fantaallenatoree e output
        * Viene inserita nella stringa output_fantacalciatore il valore serializzato della lista Lista_di_tutti_i_fantacalciatori e formattato, viene poi scritto con un File.WriteAllText(),
        * a cui passo i valori file_fantacalciatore e output_fantacalciatore
        */
        public static void serializza_JSON()
        {
            string output = JsonConvert.SerializeObject(Lista_di_fantaallenatori, Formatting.Indented);
            File.WriteAllText(file_fantaallenatore, output);

            string output_fantacalciatore = JsonConvert.SerializeObject(Lista_di_tutti_i_fantacalciatori, Formatting.Indented);
            File.WriteAllText(file_fantacalciatore, output_fantacalciatore);
        }

        /**
        * \fn       public static void deserializza_JSON()   
        * \param    string input: viene inserito tutto il contenuto nel file contenuto in file_fantaallenatore
        * \param    string input_fantacalciatore: viene inserito tutto il contenuto nel file contenuto in file_fantacalciatore
        * \brief    Funzione che deserializza i file JSON in liste
        * \details  Viene inserito in input il contenuto del file letto dalla funzione File.ReadAllText(), a cui passo il valore file_fantaallenatore, poi deserializzo il contenuto di input e 
        * lo inserisco in Lista_di_fantaallenatori, poi viene inserito in input_fantacalciatore il contenuto del file letto dalla funzione File.ReadAllText(), 
        * a cui passo il valore file_fantacalciatore, poi deserializzo il contenuto di input_fantacalciatore e lo inserisco in Lista_di_tutti_i_fantacalciatori,
        */
        public static void deserializza_JSON()
        {
            string input = File.ReadAllText(file_fantaallenatore);
            Lista_di_fantaallenatori = JsonConvert.DeserializeObject<List<fantaallenatore>>(input);

            string input_fantacalciatore = File.ReadAllText(file_fantacalciatore);
            Lista_di_tutti_i_fantacalciatori = JsonConvert.DeserializeObject<List<fantacalciatore>>(input_fantacalciatore);
        }

        /**
        * \fn      public static void Classifica_Punti()
        * \param   int i: contatore ciclo for
        * \brief   Funzione Classifica_Punti, che mostra la classifica dei punti, attraverso la funzione InsertionSort.
        * \details All'inizio parte un ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza 
        * del valore restituito dalla funzione InsertionSort, a cui invio la lista Lista_di_squadre e 0, la i ad ogni ciclo viene incrementata di 1, mostro su console il valore di i incrementato di 1,
        * un "-" e il valore restituito dalla funzione InsertionSort, a cui invio la lista Lista_di_squadre e 0, poi mostro a schermo la stringa "Premi un tasto qualsiasi per continuare"
        * ed infine utilizzo un Console.ReadLine()
        */
        public static void Classifica_Punti() //
        {
            for (int i = 0; i < InsertionSort(Lista_di_fantaallenatori).Count; i++) //Ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, che continua fino a quando il valore della i è minore della lunghezza del valore restituito dalla funzione InsertionSort, a cui invio la lista Lista_di_squadre e 0, la i ad ogni ciclo viene incrementata di 1.
            {
                Console.WriteLine(i + 1 + "-" + InsertionSort(Lista_di_fantaallenatori)[i].ToString()); //Mostra su console il valore di i incrementato di 1, un "-" e il valore restituito dalla funzione InsertionSort, a cui invio la lista Lista_di_squadre e 0.
            }
            Console.WriteLine("Premi un tasto qualsiasi per continuare");
            Console.ReadLine();
        }

        /**
        * \fn      static List<fantaallenatore> InsertionSort(List<fantaallenatore> inputArray)
        * \param   int i: contatore primo ciclo for
        * \param   int j: contatore secondo ciclo for
        * \param   fantaallenatore temp: variabile temporanea
        * \param   List<fantaallenatore> inputArray: Lista di tipo fantaallenatore
        * \brief   Funzione InsertionSort, che richiede in ingresso una lista di tipo fantaallenatore inputArray.
        * \details Viene utilizzato un algoritmo di riordinamento, l'InsertionSort, inizia un ciclo for che inizializza la variabile di tipo int a cui assegnio il nome i e la pongo pari a 0, 
        * che continua fino a quando il valore della i è minore della lunghezza della lista inputArray, la i ad ogni ciclo viene incrementata di 1, inizia un secondo ciclo for che inizializza 
        * la variabile di tipo int a cui assegnio il nome j e la pongo pari a i + 1, che continua fino a quando il valore della j è maggiore di 0, la j ad ogni ciclo viene decrementata di 1,
        * alla variabile di tipo Squadra temp e le assegnio il valore dell'array inputArray[j - 1], do all'array inputArray[j - 1] il valore dell'array inputArray[j],
        * do all'array inputArray[j] il valore della variabile di tipo Squadra temp
        * \return  inputArray: Lista fantaallenatore riordianta
        */
        static List<fantaallenatore> InsertionSort(List<fantaallenatore> inputArray) //
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
