using System.ComponentModel.DataAnnotations;

namespace Supp.Core.Posts
{
    public enum PostPriority
    {
        [Display(Name = "Nieprzydzielony")]
        Unset = 0,
        [Display(Name = "Mało ważny")]
        Unimportant,
        [Display(Name = "Normalny")]
        Normal,
        [Display(Name = "Ważny")]
        Important
    }
}