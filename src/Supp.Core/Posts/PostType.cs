using System.ComponentModel.DataAnnotations;

namespace Supp.Core.Posts
{
    public enum PostType
    {
        [Display(Name = "Błąd")]
        Error,
        [Display(Name = "Ulepszenie")]
        Enhancement,
        [Display(Name = "Zadanie")]
        Task
    }
}