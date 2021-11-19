using Newtonsoft.Json;

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
        [JsonProperty]
        protected string nome;
        [JsonProperty]
        protected string ruolo;
        [JsonProperty]
        protected int prezzo;
        //Costruttore.
        public fantacalciatore(string nome, string ruolo, int prezzo)
        {
            this.nome = nome;
            this.ruolo = ruolo;
            this.prezzo = prezzo;
        }
        //Metodi
        public string Get_nome()
        {
            return nome;
        }

        public string Get_ruolo()
        {
            return ruolo;
        }

        public int Get_prezzo()
        {
            return prezzo;
        }

        public override string ToString() //Metodo ToString, che ritorna la stringa nome.
        {
            return $"nome: {nome}, ruolo: {ruolo}, prezzo: {prezzo}";
        }
    }
}
