using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumeWebApi
{
    internal class Client
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Veuillez fournir un nom")]
        [MaxLength(25), MinLength(3)]
        public string name { get; set; }

        [StringLength(25, ErrorMessage = "la longueur maximale est de 25characteres")]
        public string christianname { get; set; }

        [Range(18, 60, ErrorMessage = "L'age est compris entre 18-60ans")]
        public int age { get; set; }

        //[StringLength(maximumLength:50,ErrorMessage ="50 caracteres grand max")]
        public string email { get; set; }

        public void Show()
        {
            Console.WriteLine($"Le Client est {name}-{christianname} il a {age} et vous pouvez correspondre avec lui a l'email:{email} ");
        }

    }
}
