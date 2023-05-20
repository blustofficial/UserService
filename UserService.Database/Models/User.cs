using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Snils { get; set; }
        public DateTime Birthday { get; set; }

        //0 - кандидаты на стажировку
        //1 - стажеры
        //2 - кураторы
        //3 - кадры организаций
        //4 - наставники организаций
        public int? UserType { get; set; }
    }
}
