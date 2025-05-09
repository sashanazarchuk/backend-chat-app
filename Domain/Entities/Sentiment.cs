using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sentiment
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Emoji { get; set; }

        public int ChatMessageId { get; set; }
    }
}
