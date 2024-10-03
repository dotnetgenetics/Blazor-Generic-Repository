using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorGenericRepository.Model
{
    public interface IEntity
    {
        [NotMapped]
        public int Id { get; }
    }
}
