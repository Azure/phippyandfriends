using System;

namespace parrot.Models
{
    public class Pod
    {
        public string Name { get; set; }
        public string Container { get; set; }
        public string NameSpace { get; set; }
        public string ContainerImage { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public string CardImageUrl
        {
            get { return string.Format("/media/{0}.png", Container); }
        }

        public override string ToString() {
            return $"Name: {Name}\nContainer: {Container}\nNameSpace: {NameSpace}\nContainerImage: {ContainerImage}\nStatus: {Status}\nAction: {Action}\nCardImageUrl: {CardImageUrl}";
        }
    }
}