using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constants.SongNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }

        [ForeignKey(nameof(WriterId))]
        [Required]
        public int WriterId { get; set; }
        public virtual Writer Writer { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<SongPerformer> SongPerformers { get; set; }
    }
}