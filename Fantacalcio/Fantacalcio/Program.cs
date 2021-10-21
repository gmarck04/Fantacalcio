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

namespace Fantacalcio
{
    class fantacalcio
    {
        //Attributi
        protected string[] Fantaallenatore = new string[0];
        protected int[] Fantaallenatore_crediti = new int[0];
        protected int[] Fantaallenatore_punti = new int[0];
        public int giorni_totali { get; set; }

        //Costruttore
        public fantacalcio()
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
        public void stampa()
        {
            for(int i = 0; i< Fantaallenatore.Length; i++)
            {
                Console.WriteLine(Fantaallenatore[i]);
                Console.WriteLine(Fantaallenatore_crediti[i]);
                Console.WriteLine(Fantaallenatore_punti[i]);
            }      
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            fantacalcio c = new fantacalcio();
            int valore = 1;
            
            Console.WriteLine("Inserisci il credito del fantaallenatore");
            int crediti = Int32.Parse(Console.ReadLine());
            do
            {
                Console.WriteLine("Inserisci il nome del fantaallenatore");
                c.Inserisci_nome_Fantaallenatore(Console.ReadLine());
                c.Inserisci_crediti_Fantaallenatore(crediti);

                Console.WriteLine("Vuoi continuare? Inserisci 1 per stoppare e 0 per continuare");
                valore = Int32.Parse(Console.ReadLine());
                while (valore != 0 && valore != 1)
                {
                    Console.WriteLine("Errato: se vuoi continuare? Inserisci 1 per stoppare e 0 per continuare");
                    valore = Int32.Parse(Console.ReadLine());
                }
            } while (valore == 0);

            

            c.stampa();
        }
    }
}
