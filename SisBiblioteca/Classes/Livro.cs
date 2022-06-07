using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBiblioteca.Classes
{
    internal class Livro
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Titulo {get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public int Paginas { get; set; }
    }
}
