using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM02_EJ23_GRUPAL.Models
{
    internal class AudioModel
    {
        [PrimaryKey]
        public int Id { get; set; }
        public String FechaHora { get; set; }
        public string AudioDecod { get; set; }

        public override string ToString()
        {
            return this.Id + " " + this.FechaHora + " | " + this.AudioDecod;
        }
    }
}
