using System;
using System.Collections.Generic;

namespace aera_core
{
    public class ListaPaginada<T> : List<T>
    {
        public int Página { get; }
        public int TotalDePáginas { get; }
        public ListaPaginada(List<T> itens, int total, int página, int quantidade)
        {
            Página = página;
            TotalDePáginas = (int) Math.Ceiling(total / (double) quantidade);
            AddRange(itens);
        }

        public bool TemMaisItens => Página < TotalDePáginas;
    }
}