using Alura.Adopet.Console.Modelos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console.Util
{
    public class SuccessWithPets:Success
    {
        public IEnumerable<Pet> Data { get;  }
        public SuccessWithPets(IEnumerable<Pet> data,string mensagem)
        {
            Data = data;
            this.Message = mensagem;
        }      

    }
}
