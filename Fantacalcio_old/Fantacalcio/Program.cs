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
    class fantacalciatore
    {
        //Attributi
        string nome, ruolo;
        int prezzo, id;
        //Costruttore
        public fantacalciatore(string nome, string ruolo, int prezzo, int id)
        {
            this.nome = nome;
            this.ruolo = ruolo;
            this.prezzo = prezzo;
            this.id = id;
        }
        public override string ToString()
        {
            return $"il nome {nome}, il ruolo {ruolo}, il prezzo {prezzo}, l'id {id}";
        }
    }
    class fantaallenatore
    {
        //Attributi
        protected string[] Fantaallenatore = new string[0];
        protected int[] Fantaallenatore_crediti = new int[0];
        protected int[] Fantaallenatore_punti = new int[0];
        public int giorni_totali { get; set; }
        public List<fantacalciatore> Lista_Calciatori = new List<fantacalciatore>();

        //Costruttore
        public fantaallenatore() //inserisci la data
        {
            this.giorni_totali = 0;
        }
        //Metodi
        public void Inserisci_nome_Fantaallenatore(string nome) //Metodo che inserisce i nomi nell'array Fantaallenatore
        {
            Array.Resize(ref Fantaallenatore, Fantaallenatore.Length + 1);
            Array.Resize(ref Fantaallenatore_punti, Fantaallenatore_punti.Length + 1);
            Fantaallenatore[Fantaallenatore.Length - 1] = nome;
        }
        public void Inserisci_crediti_Fantaallenatore(int crediti) //Metodo che inserisce i crediti nell'array Fantaallenatore_crediti
        {
            Array.Resize(ref Fantaallenatore_crediti, Fantaallenatore_crediti.Length + 1);
            Fantaallenatore_crediti[Fantaallenatore_crediti.Length - 1] = crediti;
        }
        public void Inserisci_punti()
        {
            
        }
        public void Riordina_per_punti()
        {
            Array.Sort(Fantaallenatore_punti);
            Array.Reverse(Fantaallenatore_punti);
            for (int i = 0; i < Fantaallenatore_punti.Length; i++)
            {
                Console.WriteLine(Fantaallenatore_punti[i]);
            }
        }
        public void Aggiungi_Fantagiocatore(fantacalciatore calciatore)
        {
            Lista_Calciatori.Add(calciatore);
        }
        public void stampa()
        {
            for(int i = 0; i< Fantaallenatore.Length; i++)
            {
                Console.WriteLine(Fantaallenatore[i]);
                Console.WriteLine(Fantaallenatore_crediti[i]);
                Console.WriteLine(Fantaallenatore_punti[i]);
            }
            foreach (fantacalciatore calciatore in Lista_Calciatori)
            {
                Console.WriteLine(calciatore.ToString());
            }          
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string salvataggio = AppDomain.CurrentDomain.BaseDirectory + @"File_LOG.txt";
            StreamWriter file_log = new StreamWriter(salvataggio);
            fantaallenatore c = new fantaallenatore();
            int valore = 1;
            int valore_calciatore = 1;
            int id = 0;
            
            Console.WriteLine("Inserisci il credito del fantaallenatore");
            file_log.WriteLine("Inserisci il credito del fantaallenatore");
            int crediti = Int32.Parse(Console.ReadLine());
            file_log.WriteLine(crediti);
            do
            {
                Console.WriteLine("Inserisci il nome del fantaallenatore");
                c.Inserisci_nome_Fantaallenatore(Console.ReadLine());
                c.Inserisci_crediti_Fantaallenatore(crediti);

                do
                {
                    Console.WriteLine("nome giocatore");
                    string nome = Console.ReadLine();
                    Console.WriteLine("ruolo giocatore");
                    string ruolo = Console.ReadLine();
                    Console.WriteLine("prezzo giocatore");
                    int prezzo = Int32.Parse(Console.ReadLine());

                    c.Aggiungi_Fantagiocatore(new fantacalciatore(nome, ruolo, prezzo, id));

                    Console.WriteLine("Vuoi continuare? Inserisci 1 per stoppare e 0 per continuare");
                    valore_calciatore = Int32.Parse(Console.ReadLine());
                    while (valore_calciatore != 0 && valore_calciatore != 1)
                    {
                        Console.WriteLine("Errato: se vuoi continuare? Inserisci 1 per stoppare e 0 per continuare");
                        valore_calciatore = Int32.Parse(Console.ReadLine());
                    }
                } while (valore_calciatore == 0);

                Console.WriteLine("Vuoi continuare? Inserisci 1 per stoppare e 0 per continuare");
                valore = Int32.Parse(Console.ReadLine());
                while (valore != 0 && valore != 1)
                {
                    Console.WriteLine("Errato: se vuoi continuare? Inserisci 1 per stoppare e 0 per continuare");
                    valore = Int32.Parse(Console.ReadLine());
                }
                id++;
            } while (valore == 0);


            c.Riordina_per_punti(); //Sistemare la console
            c.stampa(); //Sistemare la console
            file_log.Close();
        }
    }
}
