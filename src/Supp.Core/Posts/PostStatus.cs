using System.ComponentModel.DataAnnotations;

namespace Supp.Core.Posts
{
    public enum PostStatus
    {
        [Display(Name = "Nowy")]
        New,
        [Display(Name = "Przydzielony")]
        Assigned,
        [Display(Name = "Zrobiony")]
        Done,
        [Display(Name = "Nieprawidłowy")]
        Incorrect,
        [Display(Name = "Niewykonalny")]
        Unworkable,
        [Display(Name = "Usunięty")]
        Removed
    }
}