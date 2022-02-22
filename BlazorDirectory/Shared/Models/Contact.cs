using System.ComponentModel.DataAnnotations;

namespace BlazorDirectory.Shared
{
    public class Contact
    {
        [Key]
        public int ContactId {get; set;}
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MaxLength(10, ErrorMessage = "El nombre no debe contener entre 3 y 10 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre no debe contener entre 3 y 10 caracteres")]
        public string name {get; set;}
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MaxLength(7, ErrorMessage = "Debe ingresar un número de telefono fijo 7 digitos. Gracias")]
        [MinLength(7, ErrorMessage = "Debe ingresar un número de telefono fijo 7 digitos. Gracias")]
        public string phoneNumber {get; set;}
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MaxLength(10, ErrorMessage = "Debe ingresar un número de celular de 10 digitos. Gracias")]
        [MinLength(10, ErrorMessage = "Debe ingresar un número de celular de 10 digitos. Gracias")]
        public string cellPhone {get; set;}

        public Contact()
        {
            this.name = "";
            this.phoneNumber = "";
            this.cellPhone = "";
        }

        public Contact(string _name)
        {
            this.name = _name;
        }

        public Contact(string _name, string _phoneNumber, string _cellPhone)
        {
            this.name = _name;
            this.phoneNumber = _phoneNumber;
            this.cellPhone = _cellPhone;
        }

        public override string ToString()
        {
            return "\n========= Directorio Telefonico =========== \nNombre Contacto : " + name.ToString() + "\nTelefono fijo   : " + phoneNumber.ToString() + "\nCelular         : " + cellPhone.ToString() + "\n============================================\n";
        }

    }
}